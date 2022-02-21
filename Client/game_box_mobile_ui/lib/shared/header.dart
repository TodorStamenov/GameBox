import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/style/constants.dart';

class Header extends StatelessWidget with PreferredSizeWidget {
  final String title;

  Header({required this.title});

  @override
  Widget build(BuildContext context) {
    return AppBar(
      elevation: 0,
      centerTitle: true,
      backgroundColor: Constants.primaryColor,
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
