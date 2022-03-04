import 'package:flutter/material.dart';

class GameInfo extends StatelessWidget {
  final String text;

  const GameInfo({required this.text});

  @override
  Widget build(BuildContext context) {
    return Text(
      this.text,
      style: TextStyle(
        fontSize: 14,
        color: Colors.black,
        fontWeight: FontWeight.bold,
      ),
    );
  }
}
