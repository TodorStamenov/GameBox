import 'package:flutter/material.dart';

class EmptyCollection extends StatelessWidget {
  final String collectionName;

  const EmptyCollection({
    required this.collectionName,
  });

  @override
  Widget build(BuildContext context) {
    return Text(
      'Your ${this.collectionName} is empty!',
      textAlign: TextAlign.center,
      style: TextStyle(
        fontSize: 20,
        color: Colors.black,
        fontWeight: FontWeight.bold,
      ),
    );
  }
}
