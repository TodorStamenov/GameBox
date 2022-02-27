import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/api/games.dart';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/game/game.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';

class Games extends StatefulWidget {
  static const String routeName = '/games';

  @override
  _GamesState createState() => _GamesState();
}

class _GamesState extends State<Games> {
  List<Game> games = [];
  List<Game> owned = [];

  void getGames() async {
    var results = await Future.wait([
      getAllGames(this.games.length),
      getOwnedGames(this.games.length),
    ]);

    var gamesResult = results[0];
    var ownedResult = results[1];

    if (!gamesResult.success) {
      showToast(gamesResult.message!);
      return;
    }

    if (!ownedResult.success) {
      showToast(ownedResult.message!);
      return;
    }

    if (mounted) {
      setState(() {
        this.games.addAll(gamesResult.data);
        this.owned.addAll(ownedResult.data);
      });
    }
  }

  @override
  void initState() {
    super.initState();
    this.getGames();
  }

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 2,
      child: Scaffold(
        appBar: AppBar(
          elevation: 0,
          centerTitle: true,
          backgroundColor: Constants.primaryColor,
          title: Text(
            'Games',
            style: TextStyle(
              fontSize: 18,
              color: Colors.white,
              fontWeight: FontWeight.bold,
            ),
          ),
          bottom: TabBar(
            tabs: [
              Tab(
                child: Text(
                  'Games',
                  style: TextStyle(
                    fontSize: 16,
                    color: Colors.white,
                    fontWeight: FontWeight.bold,
                  ),
                ),
              ),
              Tab(
                child: Text(
                  'Owned',
                  style: TextStyle(
                    fontSize: 16,
                    color: Colors.white,
                    fontWeight: FontWeight.bold,
                  ),
                ),
              ),
            ],
          ),
        ),
        body: TabBarView(
          children: [
            Column(
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
            Column(
              children: [
                Expanded(
                  child: ListView.builder(
                    itemCount: this.owned.length,
                    itemBuilder: (context, index) => ListTile(
                      title: Text(
                        this.owned[index].title,
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
          ],
        ),
        drawer: SideDrawer(),
      ),
    );
  }
}
