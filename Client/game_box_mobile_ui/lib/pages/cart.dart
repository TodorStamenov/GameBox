import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/models/game_list_item_model.dart';
import 'package:game_box_mobile_ui/pages/games.dart';
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

  Future<void> order() async {
    if (mounted) {
      this.setState(() => this.isLoading);
    }

    var result = await createOrder();

    if (!result.success) {
      showToast(result.message!);
    }

    clearItems();
    if (mounted) {
      setState(() => this.isLoading = false);
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
        child: EmptyCollection(
          collectionLength: this.games.length,
          emptyCollectionMessage: 'Your cart is empty!',
          child: ListView.builder(
            itemCount: this.games.length,
            itemBuilder: (context, index) => ListTile(
              title: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  if (index == 0) SizedBox(height: 20) else SizedBox(height: 0),
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
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      Text(
                        '\$${this.games[index].price}',
                        textAlign: TextAlign.end,
                        style: TextStyle(
                          fontSize: 16,
                          color: Colors.black,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
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
                  if (index == this.games.length - 1) ...[
                    Row(
                      mainAxisAlignment: MainAxisAlignment.end,
                      children: [
                        Text(
                          'Total: \$${this.games.map((g) => g.price).reduce((x, y) => x + y).toStringAsFixed(2)}',
                          style: TextStyle(
                            fontSize: 16,
                            color: Colors.black,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ],
                    ),
                    SizedBox(height: 15),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.end,
                      children: [
                        PrimaryActionButton(
                          text: 'Order',
                          action: () async {
                            await this.order();
                            Navigator.pushReplacementNamed(context, Games.routeName);
                          },
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
                  ]
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
