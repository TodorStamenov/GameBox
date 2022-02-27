import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/api/games.dart';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/game/game.dart';
import 'package:game_box_mobile_ui/shared/header_tab.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
import 'package:game_box_mobile_ui/shared/spinner.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';
import 'package:game_box_mobile_ui/widgets/game_items.dart';

class Games extends StatefulWidget {
  static const String routeName = '/games';

  @override
  _GamesState createState() => _GamesState();
}

class _GamesState extends State<Games> {
  bool isLoading = true;
  List<Game> games = [];
  List<Game> owned = [];

  Future<void> getGames(bool resetState) async {
    if (mounted && resetState) {
      this.setState(() {
        this.games = [];
        this.owned = [];
        this.isLoading = true;
      });
    }

    var results = await Future.wait([
      getAllGames(this.games.length),
      getOwnedGames(this.games.length),
    ]);

    var gamesResult = results[0];
    var ownedResult = results[1];

    if (!gamesResult.success || !ownedResult.success) {
      showToast('Fetch games failed!');
      return;
    }

    if (mounted) {
      setState(() {
        this.games.addAll(gamesResult.data);
        this.owned.addAll(ownedResult.data);
        this.isLoading = false;
      });
    }
  }

  @override
  void initState() {
    super.initState();
    this.getGames(true);
  }

  @override
  Widget build(BuildContext context) {
    return DefaultTabController(
      length: 2,
      child: Scaffold(
        drawer: SideDrawer(),
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
            indicatorColor: Colors.white,
            tabs: [
              HeaderTab(title: 'Games'),
              HeaderTab(title: 'Owned'),
            ],
          ),
        ),
        body: Spinner(
          isLoading: this.isLoading,
          child: TabBarView(
            children: [
              GameItems(
                games: this.games,
                loadGames: this.getGames,
              ),
              GameItems(
                games: this.owned,
                loadGames: this.getGames,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
