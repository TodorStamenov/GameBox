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
  bool _isLoading = true;
  List<GameListItemModel> _games = [];

  Future<void> getGames() async {
    var result = await getAllWishlistItems();

    if (!result.success) {
      showToast(result.message!);
    }

    if (mounted) {
      setState(() {
        _isLoading = false;
        _games = result.data;
      });
    }
  }

  Future<void> removeGame(String gameId) async {
    if (mounted) {
      setState(() => _isLoading = true);
    }

    var result = await removeWishlistItem(gameId);

    if (!result.success) {
      showToast(result.message!);
    }

    if (mounted) {
      setState(() {
        _isLoading = false;
        _games = result.data;
      });
    }
  }

  Future<void> clearGames() async {
    if (mounted) {
      setState(() => _isLoading = true);
    }

    var result = await removeWishlistItems();

    if (!result.success) {
      showToast(result.message!);
    }

    if (mounted) {
      setState(() {
        _isLoading = false;
        _games = result.data;
      });
    }
  }

  @override
  void initState() {
    super.initState();
    getGames();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: SideDrawer(),
      appBar: const Header(
        title: 'Wishlist',
      ),
      body: Spinner(
        isLoading: _isLoading,
        child: EmptyCollection(
          collectionLength: _games.length,
          emptyCollectionMessage: 'Your wishlist is empty!',
          child: ListView.builder(
            itemCount: _games.length,
            itemBuilder: (context, index) => ListTile(
              title: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  index == 0 ? const SizedBox(height: 20) : const SizedBox(height: 0),
                  Text(
                    _games[index].title,
                    textAlign: TextAlign.center,
                    style: const TextStyle(
                      fontSize: 18,
                      color: Colors.black,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  const SizedBox(height: 20),
                  Image.network(_games[index].thumbnailUrl),
                  const SizedBox(height: 20),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.end,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      Expanded(
                        child: Text(
                          '\$${_games[index].price}',
                          textAlign: TextAlign.start,
                          style: const TextStyle(
                            fontSize: 16,
                            color: Colors.black,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ),
                      PrimaryActionButton(
                        text: 'Buy',
                        action: () {
                          var gameId = _games[index].id;
                          removeGame(gameId);
                          addItem(gameId);
                          Navigator.pushReplacementNamed(context, Cart.routeName);
                        },
                      ),
                      const SizedBox(width: 10),
                      PrimaryActionButton(
                        text: 'Remove',
                        action: () => removeGame(_games[index].id),
                      ),
                    ],
                  ),
                  const Divider(
                    height: 20,
                    color: Colors.black,
                  ),
                  if (index == _games.length - 1) ...[
                    Row(
                      mainAxisAlignment: MainAxisAlignment.end,
                      children: [
                        PrimaryActionButton(
                          text: 'Clear',
                          action: clearGames,
                        ),
                      ],
                    ),
                    const SizedBox(height: 25),
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
