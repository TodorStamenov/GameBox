import 'package:flutter/material.dart';

class Header extends StatelessWidget with PreferredSizeWidget {
  final String title;

  const Header({required this.title});

  @override
  Widget build(BuildContext context) {
    return AppBar(
      elevation: 0,
      centerTitle: true,
      backgroundColor: Color(0xff343A40),
      title: Text(
        this.title,
        style: TextStyle(
          fontSize: 18,
          color: Colors.white,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
  }

  @override
  Size get preferredSize => Size.fromHeight(kToolbarHeight);
}
