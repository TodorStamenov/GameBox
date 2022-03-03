import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/models/game_list_item_model.dart';
import 'package:game_box_mobile_ui/services/wishlist_service.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
import 'package:game_box_mobile_ui/shared/spinner.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';
import 'package:game_box_mobile_ui/widgets/empty_collection_message.dart';
import 'package:game_box_mobile_ui/widgets/primary_action_button.dart';

class Wishlist extends StatefulWidget {
  static const String routeName = '/wishlist';

  @override
  _WishlistState createState() => _WishlistState();
}

class _WishlistState extends State<Wishlist> {
  bool isLoading = true;
  List<GameListItemModel> games = [];

  Future<void> getGames() async {
    var result = await getAllItems();

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

  Future<void> removeItem(String gameId) async {
    if (mounted) {
      this.setState(() => this.isLoading = true);
    }

    var result = await removeListItem(gameId);

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

  Future<void> clearItems() async {
    if (mounted) {
      this.setState(() => this.isLoading = true);
    }

    var result = await removeAllItems();

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
        title: 'Wishlist',
      ),
      body: Spinner(
        isLoading: this.isLoading,
        child: this.games.isNotEmpty
            ? ListView.builder(
                itemCount: this.games.length + 1,
                itemBuilder: (context, index) => ListTile(
                  title: index == this.games.length
                      ? Column(
                          crossAxisAlignment: CrossAxisAlignment.end,
                          children: [
                            PrimaryActionButton(
                              text: 'Clear',
                              action: this.clearItems,
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
                            SizedBox(height: 15),
                            Row(
                              mainAxisAlignment: MainAxisAlignment.end,
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                PrimaryActionButton(
                                  text: 'Buy',
                                  action: () {},
                                ),
                                SizedBox(width: 10),
                                PrimaryActionButton(
                                  text: 'Remove',
                                  action: () => this.removeItem(this.games[index].id),
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
                  EmptyCollection(collectionName: 'wishlist'),
                ],
              ),
      ),
    );
  }
}
