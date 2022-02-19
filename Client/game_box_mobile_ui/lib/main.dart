import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/pages/home.dart';
import 'package:game_box_mobile_ui/pages/todo.dart';

void main() {
  runApp(
    MaterialApp(
      initialRoute: '/',
      routes: {
        '/': (context) => Home(),
        '/todos': (context) => Todo(),
      },
    ),
  );
}
