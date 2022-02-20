import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/shared/header.dart';

class Login extends StatelessWidget {
  static const String routeName = '/login';

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: Header(
        title: 'Login',
      ),
      body: Center(
        child: Text(
          'Login page',
        ),
      ),
    );
  }
}
