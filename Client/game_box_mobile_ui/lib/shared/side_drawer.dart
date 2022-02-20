import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/pages/todo.dart';
import 'package:game_box_mobile_ui/pages/login.dart';
import 'package:game_box_mobile_ui/pages/register.dart';

class SideDrawer extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: [
          DrawerHeader(
            child: Center(
              child: Text(
                'Game Box',
                style: TextStyle(
                  fontSize: 21,
                  color: Color(0xff343A40),
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ),
          ListTile(
            title: Text(
              'Login',
              style: TextStyle(
                fontSize: 18,
                color: Color(0xff343A40),
                fontWeight: FontWeight.bold,
              ),
            ),
            onTap: () {
              Navigator.pop(context);
              Navigator.pushNamed(context, Login.routeName);
            },
          ),
          ListTile(
            title: Text(
              'Register',
              style: TextStyle(
                fontSize: 18,
                color: Color(0xff343A40),
                fontWeight: FontWeight.bold,
              ),
            ),
            onTap: () {
              Navigator.pop(context);
              Navigator.pushNamed(context, Register.routeName);
            },
          ),
          ListTile(
            title: Text(
              'Todos',
              style: TextStyle(
                fontSize: 18,
                color: Color(0xff343A40),
                fontWeight: FontWeight.bold,
              ),
            ),
            onTap: () {
              Navigator.pop(context);
              Navigator.pushNamed(context, Todo.routeName);
            },
          ),
        ],
      ),
    );
  }
}
