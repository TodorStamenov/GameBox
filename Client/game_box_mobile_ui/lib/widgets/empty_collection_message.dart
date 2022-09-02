import 'package:flutter/material.dart';

class EmptyCollection extends StatelessWidget {
  final int _collectionLength;
  final String _emptyCollectionMessage;
  final Widget _child;

  const EmptyCollection({
    required int collectionLength,
    required String emptyCollectionMessage,
    required Widget child,
  })  : _collectionLength = collectionLength,
        _emptyCollectionMessage = emptyCollectionMessage,
        _child = child;

  @override
  Widget build(BuildContext context) {
    if (_collectionLength == 0) {
      return Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          const SizedBox(height: 80),
          Text(
            _emptyCollectionMessage,
            textAlign: TextAlign.center,
            style: const TextStyle(
              fontSize: 20,
              color: Colors.black,
              fontWeight: FontWeight.bold,
            ),
          ),
        ],
      );
    }

    return _child;
  }
}
