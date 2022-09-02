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
  State<Cart> createState() => _CartState();
}

class _CartState extends State<Cart> {
  bool _isLoading = true;
  List<GameListItemModel> _games = [];

  Future<void> getGames() async {
    if (mounted) {
      setState(() => _isLoading = true);
    }

    var result = await upsertCart();

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

  Future<void> order() async {
    if (mounted) {
      setState(() => _isLoading);
    }

    var result = await createOrder();

    if (!result.success) {
      showToast(result.message!);
    }

    clearItems();
    if (mounted) {
      setState(() => _isLoading = false);
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
        title: 'Cart',
      ),
      body: Spinner(
        isLoading: _isLoading,
        child: EmptyCollection(
          collectionLength: _games.length,
          emptyCollectionMessage: 'Your cart is empty!',
          child: ListView.builder(
            itemCount: _games.length,
            itemBuilder: (context, index) => ListTile(
              title: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  if (index == 0) const SizedBox(height: 20) else const SizedBox(height: 0),
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
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      Text(
                        '\$${_games[index].price}',
                        textAlign: TextAlign.end,
                        style: const TextStyle(
                          fontSize: 16,
                          color: Colors.black,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                      PrimaryActionButton(
                        text: 'Remove',
                        action: () {
                          removeItem(_games[index].id);
                          getGames();
                        },
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
                        Text(
                          'Total: \$${_games.map((g) => g.price).reduce((x, y) => x + y).toStringAsFixed(2)}',
                          style: const TextStyle(
                            fontSize: 16,
                            color: Colors.black,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ],
                    ),
                    const SizedBox(height: 15),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.end,
                      children: [
                        PrimaryActionButton(
                          text: 'Order',
                          action: () async {
                            await order();

                            if (mounted) {
                              Navigator.pushReplacementNamed(context, Games.routeName);
                            }
                          },
                        ),
                        const SizedBox(width: 15),
                        PrimaryActionButton(
                          text: 'Clear',
                          action: () {
                            clearItems();
                            getGames();
                          },
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
