import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/common/constants.dart';

class PrimaryActionButton extends StatelessWidget {
  final String? _text;
  final IconData? _icon;
  final Function _action;

  const PrimaryActionButton({
    String? text,
    IconData? icon,
    required Function action,
  })  : _text = text,
        _icon = icon,
        _action = action;

  Widget getChild() {
    if (_text != null) {
      return Text(
        _text!,
        style: const TextStyle(
          fontSize: 18,
          color: Colors.white,
          fontWeight: FontWeight.bold,
        ),
      );
    } else {
      return Icon(
        _icon,
        color: Colors.white,
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return MaterialButton(
      color: Constants.primaryColor,
      padding: const EdgeInsets.symmetric(
        vertical: 10,
        horizontal: 30,
      ),
      child: getChild(),
      onPressed: () => _action(),
    );
  }
}
