import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/common/constants.dart';

class PrimaryActionButton extends StatelessWidget {
  final String? text;
  final IconData? icon;
  final Function action;

  const PrimaryActionButton({
    this.text,
    this.icon,
    required this.action,
  });

  Widget getChild() {
    if (this.text != null) {
      return Text(
        this.text!,
        style: TextStyle(
          fontSize: 18,
          color: Colors.white,
          fontWeight: FontWeight.bold,
        ),
      );
    } else {
      return Icon(
        this.icon,
        color: Colors.white,
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return MaterialButton(
      color: Constants.primaryColor,
      padding: EdgeInsets.symmetric(
        vertical: 10,
        horizontal: 30,
      ),
      child: this.getChild(),
      onPressed: () => this.action(),
    );
  }
}
