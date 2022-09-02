import 'package:flutter/material.dart';

class HeaderTab extends StatelessWidget {
  final String _title;

  const HeaderTab({
    required String title,
  }) : _title = title;

  @override
  Widget build(BuildContext context) {
    return Tab(
      child: Text(
        _title,
        style: const TextStyle(
          fontSize: 16,
          color: Colors.white,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
  }
}
