import 'package:flutter/material.dart';

class EmptyCollection extends StatelessWidget {
  final int collectionLength;
  final String emptyCollectionMessage;
  final Widget child;

  const EmptyCollection({
    required this.collectionLength,
    required this.emptyCollectionMessage,
    required this.child,
  });

  @override
  Widget build(BuildContext context) {
    if (this.collectionLength == 0) {
      return Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          SizedBox(height: 80),
          Text(
            this.emptyCollectionMessage,
            textAlign: TextAlign.center,
            style: TextStyle(
              fontSize: 20,
              color: Colors.black,
              fontWeight: FontWeight.bold,
            ),
          ),
        ],
      );
    }

    return this.child;
  }
}
