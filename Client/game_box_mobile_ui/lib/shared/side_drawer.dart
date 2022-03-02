import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/models/user_model.dart';
import 'package:game_box_mobile_ui/pages/cart.dart';
import 'package:game_box_mobile_ui/pages/games.dart';
import 'package:game_box_mobile_ui/pages/login.dart';
import 'package:game_box_mobile_ui/pages/register.dart';
import 'package:game_box_mobile_ui/pages/wishlist.dart';
import 'package:game_box_mobile_ui/utils/storage.dart';

class SideDrawer extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: Column(
        mainAxisSize: MainAxisSize.max,
        children: [
          DrawerHeader(
            decoration: BoxDecoration(
              border: Border(
                bottom: BorderSide(
                  color: Colors.black,
                  width: 0.5,
                ),
              ),
            ),
            child: Center(
              child: Text(
                'Game Box',
                style: TextStyle(
                  fontSize: 21,
                  color: Color(0xff343A40),
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ),
          if (Storage.prefs?.getString('user') == null) ...[
            ListTile(
              leading: Icon(
                Icons.login,
                color: Colors.black,
              ),
              title: Text(
                'Login',
                style: TextStyle(
                  fontSize: 18,
                  color: Color(0xff343A40),
                  fontWeight: FontWeight.bold,
                ),
              ),
              onTap: () {
                Navigator.pushReplacementNamed(context, Login.routeName);
              },
            ),
            ListTile(
              leading: Icon(
                Icons.app_registration,
                color: Colors.black,
              ),
              title: Text(
                'Register',
                style: TextStyle(
                  fontSize: 18,
                  color: Color(0xff343A40),
                  fontWeight: FontWeight.bold,
                ),
              ),
              onTap: () {
                Navigator.pushReplacementNamed(context, Register.routeName);
              },
            ),
          ] else ...[
            ListTile(
              leading: Icon(
                Icons.person,
                color: Colors.black,
              ),
              title: Text(
                UserModel.fromJson(jsonDecode((Storage.prefs?.getString('user'))!)).username,
                style: TextStyle(
                  fontSize: 18,
                  color: Color(0xff343A40),
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
            Divider(
              height: 20,
              color: Colors.black,
            ),
            ListTile(
              leading: Icon(
                Icons.sports_esports,
                color: Colors.black,
              ),
              title: Text(
                'Games',
                style: TextStyle(
                  fontSize: 18,
                  color: Color(0xff343A40),
                  fontWeight: FontWeight.bold,
                ),
              ),
              onTap: () {
                Navigator.pushReplacementNamed(context, Games.routeName);
              },
            ),
            ListTile(
              leading: Icon(
                Icons.shopping_cart,
                color: Colors.black,
              ),
              title: Text(
                'Cart',
                style: TextStyle(
                  fontSize: 18,
                  color: Color(0xff343A40),
                  fontWeight: FontWeight.bold,
                ),
              ),
              onTap: () {
                Navigator.pushReplacementNamed(context, Cart.routeName);
              },
            ),
            ListTile(
              leading: Icon(
                Icons.favorite,
                color: Colors.black,
              ),
              title: Text(
                'Wishlist',
                style: TextStyle(
                  fontSize: 18,
                  color: Color(0xff343A40),
                  fontWeight: FontWeight.bold,
                ),
              ),
              onTap: () {
                Navigator.pushReplacementNamed(context, Wishlist.routeName);
              },
            ),
            Expanded(
              child: Align(
                alignment: Alignment.bottomLeft,
                child: ListTile(
                  leading: Icon(
                    Icons.logout,
                    color: Colors.black,
                  ),
                  title: Text(
                    'Logout',
                    style: TextStyle(
                      fontSize: 18,
                      color: Color(0xff343A40),
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  onTap: () {
                    Storage.prefs?.clear();
                    Navigator.pushReplacementNamed(context, Login.routeName);
                  },
                ),
              ),
            ),
            SizedBox(height: 20),
          ],
        ],
      ),
    );
  }
}
