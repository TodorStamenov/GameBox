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

  runApp(
    MaterialApp(
      debugShowCheckedModeBanner: false,
      home: Storage.prefs?.getString('user') == null ? Login() : Games(),
      routes: {
        Games.routeName: (context) => Games(),
        GameDetails.routeName: (context) => GameDetails(),
        Login.routeName: (context) => Login(),
        Register.routeName: (context) => Register(),
        Cart.routeName: (context) => Cart(),
        Wishlist.routeName: (context) => Wishlist(),
      },
    ),
  );
}
