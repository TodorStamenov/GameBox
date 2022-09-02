import 'package:flutter/material.dart';

class Spinner extends StatelessWidget {
  final bool _isLoading;
  final Widget _child;

  const Spinner({
    required bool isLoading,
    required Widget child,
  })  : _isLoading = isLoading,
        _child = child;

  @override
  Widget build(BuildContext context) {
    if (_isLoading) {
      return const Center(
        child: CircularProgressIndicator(
          color: Color(0xff343A40),
        ),
      );
    }

    return _child;
  }
}
