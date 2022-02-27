import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';

class Cart extends StatefulWidget {
  static const String routeName = '/cart';

  @override
  _CartState createState() => _CartState();
}

class _CartState extends State<Cart> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: SideDrawer(),
      appBar: Header(
        title: 'Cart',
      ),
      body: Center(
        child: Text(
          'Cart Page',
          style: TextStyle(
            color: Colors.black,
          ),
        ),
      ),
    );
  }
}
