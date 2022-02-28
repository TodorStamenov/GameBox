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

  Future<void> getGames() async {
    if (mounted) {
      this.setState(() => this.isLoading = true);
    }

    var results = await Future.wait([
      getAllGames(0),
      getOwnedGames(0),
    ]);

    var gamesResult = results[0];
    var ownedResult = results[1];

    if (!gamesResult.success || !ownedResult.success) {
      showToast('Fetch games failed!');
      this.setState(() => this.isLoading = false);

      return;
    }

    if (mounted) {
      setState(() {
        this.games = gamesResult.data;
        this.owned = ownedResult.data;
        this.isLoading = false;
      });
    }
  }

  Future<void> loadAllGames() async {
    var result = await getAllGames(this.games.length);
    if (!result.success) {
      showToast(result.message!);
      return;
    }

    if (mounted) {
      setState(() => this.games.addAll(result.data));
    }
  }

  Future<void> loadOwnedGames() async {
    var result = await getOwnedGames(this.owned.length);

    if (!result.success) {
      showToast(result.message!);
      return;
    }

    if (mounted) {
      setState(() => this.owned.addAll(result.data));
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
                loadMoreGames: this.loadAllGames,
              ),
              GameItems(
                games: this.owned,
                loadGames: this.getGames,
                loadMoreGames: this.loadOwnedGames,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
