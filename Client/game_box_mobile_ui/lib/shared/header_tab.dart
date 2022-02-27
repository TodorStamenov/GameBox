import 'package:flutter/material.dart';

class HeaderTab extends StatelessWidget {
  final String title;

  const HeaderTab({
    required this.title,
  });

  @override
  Widget build(BuildContext context) {
    return Tab(
      child: Text(
        this.title,
        style: TextStyle(
          fontSize: 16,
          color: Colors.white,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
  }
}
