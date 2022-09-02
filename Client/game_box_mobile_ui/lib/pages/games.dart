import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/game_model.dart';
import 'package:game_box_mobile_ui/services/games_service.dart';
import 'package:game_box_mobile_ui/shared/header_tab.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
import 'package:game_box_mobile_ui/shared/spinner.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';
import 'package:game_box_mobile_ui/widgets/game_items.dart';

class Games extends StatefulWidget {
  static const String routeName = '/games';

  @override
  State<Games> createState() => _GamesState();
}

class _GamesState extends State<Games> {
  bool _isLoading = true;
  List<GameModel> _games = [];
  List<GameModel> _owned = [];

  Future<void> getGames() async {
    if (mounted) {
      setState(() => _isLoading = true);
    }

    var results = await Future.wait([
      getAllGames(0),
      getOwnedGames(0),
    ]);

    var gamesResult = results[0];
    var ownedResult = results[1];

    if (!gamesResult.success || !ownedResult.success) {
      showToast('Fetch games failed!');
    }

    if (mounted) {
      setState(() {
        _games = gamesResult.data;
        _owned = ownedResult.data;
        _isLoading = false;
      });
    }
  }

  Future<void> loadAllGames() async {
    var result = await getAllGames(_games.length);
    if (!result.success) {
      showToast(result.message!);
      return;
    }

    if (mounted) {
      setState(() => _games.addAll(result.data));
    }
  }

  Future<void> loadOwnedGames() async {
    var result = await getOwnedGames(_owned.length);

    if (!result.success) {
      showToast(result.message!);
      return;
    }

    if (mounted) {
      setState(() => _owned.addAll(result.data));
    }
  }

  @override
  void initState() {
    super.initState();
    getGames();
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
          title: const Text(
            'Games',
            style: TextStyle(
              fontSize: 18,
              color: Colors.white,
              fontWeight: FontWeight.bold,
            ),
          ),
          bottom: const TabBar(
            indicatorColor: Colors.white,
            tabs: [
              HeaderTab(title: 'Games'),
              HeaderTab(title: 'Owned'),
            ],
          ),
        ),
        body: Spinner(
          isLoading: _isLoading,
          child: TabBarView(
            children: [
              GameItems(
                games: _games,
                loadGames: getGames,
                loadMoreGames: loadAllGames,
              ),
              GameItems(
                games: _owned,
                loadGames: getGames,
                loadMoreGames: loadOwnedGames,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
