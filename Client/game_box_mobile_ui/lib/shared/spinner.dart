import 'package:flutter/material.dart';

class Spinner extends StatelessWidget {
  final bool isLoading;
  final Widget child;

  const Spinner({
    required this.isLoading,
    required this.child,
  });

  @override
  Widget build(BuildContext context) {
    if (this.isLoading) {
      return Center(
        child: CircularProgressIndicator(
          color: Color(0xff343A40),
        ),
      );
    }

    return this.child;
  }
}
