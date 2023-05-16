import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/common/constants.dart';

class Header extends StatelessWidget implements PreferredSizeWidget {
  final String _title;

  const Header({
    required String title,
  }) : _title = title;

  @override
  Widget build(BuildContext context) {
    return AppBar(
      elevation: 0,
      centerTitle: true,
      backgroundColor: Constants.primaryColor,
      title: Text(
        _title,
        style: const TextStyle(
          fontSize: 18,
          color: Colors.white,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
  }

  @override
  Size get preferredSize => const Size.fromHeight(kToolbarHeight);
}
