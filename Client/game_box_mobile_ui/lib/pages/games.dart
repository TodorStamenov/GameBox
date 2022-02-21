import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';

class Games extends StatefulWidget {
  static const String routeName = '/';

  @override
  _GamesState createState() => _GamesState();
}

class _GamesState extends State<Games> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: Header(
        title: 'Games',
      ),
      body: Center(
        child: Text('All Games'),
      ),
      drawer: SideDrawer(),
    );
  }
}
