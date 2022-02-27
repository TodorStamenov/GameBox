import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';

class Wishlist extends StatefulWidget {
  static const String routeName = '/wishlist';

  @override
  _WishlistState createState() => _WishlistState();
}

class _WishlistState extends State<Wishlist> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: SideDrawer(),
      appBar: Header(
        title: 'Wishlist',
      ),
      body: Center(
        child: Text(
          'Wishlist Page',
          style: TextStyle(
            color: Colors.black,
          ),
        ),
      ),
    );
  }
}
