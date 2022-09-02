import 'package:flutter/material.dart';

class GameInfo extends StatelessWidget {
  final String _text;

  const GameInfo({
    required String text,
  }) : _text = text;

  @override
  Widget build(BuildContext context) {
    return Text(
      _text,
      style: const TextStyle(
        fontSize: 14,
        color: Colors.black,
        fontWeight: FontWeight.bold,
      ),
    );
  }
}
