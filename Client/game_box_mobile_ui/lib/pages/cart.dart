import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/models/game_list_item_model.dart';
import 'package:game_box_mobile_ui/services/cart_services.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
import 'package:game_box_mobile_ui/shared/spinner.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';
import 'package:game_box_mobile_ui/widgets/empty_collection_message.dart';
import 'package:game_box_mobile_ui/widgets/primary_action_button.dart';

class Cart extends StatefulWidget {
  static const String routeName = '/cart';

  @override
  _CartState createState() => _CartState();
}

class _CartState extends State<Cart> {
  bool isLoading = true;
  List<GameListItemModel> games = [];

  Future<void> getGames() async {
    if (mounted) {
      this.setState(() => this.isLoading = true);
    }

    var result = await upsertCart();

    if (!result.success) {
      showToast(result.message!);
    }

    if (mounted) {
      setState(() {
        this.isLoading = false;
        this.games = result.data;
      });
    }
  }

  @override
  void initState() {
    super.initState();
    this.getGames();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: SideDrawer(),
      appBar: Header(
        title: 'Cart',
      ),
      body: Spinner(
        isLoading: this.isLoading,
        child: this.games.isNotEmpty
            ? ListView.builder(
                itemCount: this.games.length + 1,
                itemBuilder: (context, index) => ListTile(
                  title: index == this.games.length
                      ? Column(
                          children: [
                            Row(
                              mainAxisAlignment: MainAxisAlignment.end,
                              children: [
                                PrimaryActionButton(
                                  text: 'Order',
                                  action: () {},
                                ),
                                SizedBox(width: 15),
                                PrimaryActionButton(
                                  text: 'Clear',
                                  action: () {
                                    clearItems();
                                    this.getGames();
                                  },
                                ),
                              ],
                            ),
                            SizedBox(height: 25),
                          ],
                        )
                      : Column(
                          crossAxisAlignment: CrossAxisAlignment.stretch,
                          children: [
                            index == 0 ? SizedBox(height: 20) : SizedBox(height: 0),
                            Text(
                              this.games[index].title,
                              textAlign: TextAlign.center,
                              style: TextStyle(
                                fontSize: 18,
                                color: Colors.black,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                            SizedBox(height: 20),
                            Image.network(this.games[index].thumbnailUrl),
                            SizedBox(height: 20),
                            Text(
                              '\$${this.games[index].price}',
                              textAlign: TextAlign.end,
                              style: TextStyle(
                                fontSize: 16,
                                color: Colors.black,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                            SizedBox(height: 20),
                            Row(
                              mainAxisAlignment: MainAxisAlignment.end,
                              children: [
                                PrimaryActionButton(
                                  text: 'Remove',
                                  action: () {
                                    removeItem(this.games[index].id);
                                    this.getGames();
                                  },
                                ),
                              ],
                            ),
                            Divider(
                              height: 20,
                              color: Colors.black,
                            ),
                          ],
                        ),
                ),
              )
            : Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  SizedBox(height: 80),
                  EmptyCollection(collectionName: 'cart'),
                ],
              ),
      ),
    );
  }
}
