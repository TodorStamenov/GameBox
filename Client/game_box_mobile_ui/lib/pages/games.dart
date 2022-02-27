import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/api/games.dart';
import 'package:game_box_mobile_ui/models/game/game.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';

class Games extends StatefulWidget {
  static const String routeName = '/games';

  @override
  _GamesState createState() => _GamesState();
}

class _GamesState extends State<Games> {
  List<Game> games = [];

  void getGames() async {
    var result = await getAllGames(this.games.length);

    if (!result.success) {
      showToast(result.message!);
      return;
    }

    if (mounted) {
      setState(() => this.games.addAll(result.data));
    }
  }

  @override
  void initState() {
    super.initState();
    this.getGames();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: Header(
        title: 'Games',
      ),
      body: Column(
        children: [
          Expanded(
            child: ListView.builder(
              itemCount: this.games.length,
              itemBuilder: (context, index) => ListTile(
                title: Text(
                  this.games[index].title,
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    fontSize: 18,
                    color: Colors.black,
                    fontWeight: FontWeight.bold,
                  ),
                ),
              ),
            ),
          ),
        ],
      ),
      drawer: SideDrawer(),
    );
  }
}
