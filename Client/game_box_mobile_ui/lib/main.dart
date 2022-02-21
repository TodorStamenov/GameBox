import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/pages/games.dart';
import 'package:game_box_mobile_ui/pages/login.dart';
import 'package:game_box_mobile_ui/pages/register.dart';

void main() {
  runApp(
    MaterialApp(
      debugShowCheckedModeBanner: false,
      initialRoute: '/',
      routes: {
        Games.routeName: (context) => Games(),
        Login.routeName: (context) => Login(),
        Register.routeName: (context) => Register(),
      },
    ),
  );
}
