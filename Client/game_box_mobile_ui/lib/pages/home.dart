import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/shared/header.dart';

class Home extends StatefulWidget {
  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  int counter = 0;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: Header(
        title: 'Home',
      ),
      body: Column(
        children: [
          Padding(
            padding: EdgeInsets.symmetric(vertical: 50),
            child: Text(
              'Counter Value: ${this.counter}',
              textAlign: TextAlign.center,
              style: TextStyle(
                fontSize: 20,
                color: Colors.black,
                fontWeight: FontWeight.bold,
              ),
            ),
          ),
          Expanded(
            child: Center(
              child: MaterialButton(
                color: Color(0xff343A40),
                padding: EdgeInsets.symmetric(
                  vertical: 10,
                  horizontal: 20,
                ),
                child: Text(
                  'Counter++',
                  style: TextStyle(
                    fontSize: 18,
                    color: Colors.white,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                onPressed: () => setState(() => this.counter++),
              ),
            ),
          ),
        ],
      ),
      drawer: Drawer(
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
              onTap: () => Navigator.pushNamed(context, '/login'),
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
              onTap: () => Navigator.pushNamed(context, '/register'),
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
              onTap: () => Navigator.pushNamed(context, '/todos'),
            ),
          ],
        ),
      ),
    );
  }
}
