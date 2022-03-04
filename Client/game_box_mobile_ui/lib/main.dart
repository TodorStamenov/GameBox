import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/pages/cart.dart';
import 'package:game_box_mobile_ui/pages/game_details.dart';
import 'package:game_box_mobile_ui/pages/games.dart';
import 'package:game_box_mobile_ui/pages/login.dart';
import 'package:game_box_mobile_ui/pages/register.dart';
import 'package:game_box_mobile_ui/pages/wishlist.dart';
import 'package:game_box_mobile_ui/utils/storage.dart';
import 'package:shared_preferences/shared_preferences.dart';

Future main() async {
  WidgetsFlutterBinding.ensureInitialized();
  Storage.prefs = await SharedPreferences.getInstance();

  Widget unfocusOnTap(Widget child) {
    return GestureDetector(
      child: child,
      onTap: () => FocusManager.instance.primaryFocus?.unfocus(),
    );
  }

  runApp(
    MaterialApp(
      debugShowCheckedModeBanner: false,
      home: (() {
        if (Storage.prefs?.getString('user') != null) {
          return Games();
        } else {
          return Login();
        }
      }()),
      routes: {
        Games.routeName: (context) => Games(),
        GameDetails.routeName: (context) => unfocusOnTap(GameDetails()),
        Login.routeName: (context) => unfocusOnTap(Login()),
        Register.routeName: (context) => unfocusOnTap(Register()),
        Cart.routeName: (context) => Cart(),
        Wishlist.routeName: (context) => Wishlist(),
      },
    ),
  );
}
