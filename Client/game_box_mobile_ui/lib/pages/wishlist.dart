import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/models/game_list_item_model.dart';
import 'package:game_box_mobile_ui/pages/cart.dart';
import 'package:game_box_mobile_ui/services/cart_services.dart';
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
  State<Wishlist> createState() => _WishlistState();
}

class _WishlistState extends State<Wishlist> {
  bool isLoading = true;
  List<GameListItemModel> games = [];

  Future<void> getGames() async {
    var result = await getAllWishlistItems();

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

  Future<void> removeGame(String gameId) async {
    if (mounted) {
      this.setState(() => this.isLoading = true);
    }

    var result = await removeWishlistItem(gameId);

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

  Future<void> clearGames() async {
    if (mounted) {
      this.setState(() => this.isLoading = true);
    }

    var result = await removeWishlistItems();

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
        child: EmptyCollection(
          collectionLength: this.games.length,
          emptyCollectionMessage: 'Your wishlist is empty!',
          child: ListView.builder(
            itemCount: this.games.length,
            itemBuilder: (context, index) => ListTile(
              title: Column(
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
                  Row(
                    mainAxisAlignment: MainAxisAlignment.end,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      Expanded(
                        child: Text(
                          '\$${this.games[index].price}',
                          textAlign: TextAlign.start,
                          style: TextStyle(
                            fontSize: 16,
                            color: Colors.black,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ),
                      PrimaryActionButton(
                        text: 'Buy',
                        action: () {
                          var gameId = this.games[index].id;
                          this.removeGame(gameId);
                          addItem(gameId);
                          Navigator.pushReplacementNamed(context, Cart.routeName);
                        },
                      ),
                      SizedBox(width: 10),
                      PrimaryActionButton(
                        text: 'Remove',
                        action: () => this.removeGame(this.games[index].id),
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
                        PrimaryActionButton(
                          text: 'Clear',
                          action: this.clearGames,
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
