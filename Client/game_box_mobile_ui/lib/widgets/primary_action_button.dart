import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/common/constants.dart';

class PrimaryActionButton extends StatelessWidget {
  final String text;
  final Function action;

  const PrimaryActionButton({
    required this.text,
    required this.action,
  });

  @override
  Widget build(BuildContext context) {
    return MaterialButton(
      color: Constants.primaryColor,
      padding: EdgeInsets.symmetric(
        vertical: 10,
        horizontal: 30,
      ),
      child: Text(
        this.text,
        style: TextStyle(
          fontSize: 18,
          color: Colors.white,
          fontWeight: FontWeight.bold,
        ),
      ),
      onPressed: () => this.action(),
    );
  }
}
