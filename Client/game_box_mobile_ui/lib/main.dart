import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/pages/home.dart';
import 'package:game_box_mobile_ui/pages/login.dart';
import 'package:game_box_mobile_ui/pages/register.dart';
import 'package:game_box_mobile_ui/pages/todo.dart';

void main() {
  runApp(
    MaterialApp(
      debugShowCheckedModeBanner: false,
      initialRoute: '/',
      routes: {
        Home.routeName: (context) => Home(),
        Login.routeName: (context) => Login(),
        Register.routeName: (context) => Register(),
        Todo.routeName: (context) => Todo(),
      },
    ),
  );
}
