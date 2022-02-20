import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/shared/header.dart';

class Register extends StatelessWidget {
  static const String routeName = '/register';

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: Header(
        title: 'Register',
      ),
      body: Center(
        child: Text(
          'Register page',
        ),
      ),
    );
  }
}
