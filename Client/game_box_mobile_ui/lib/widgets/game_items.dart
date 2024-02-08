import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/game_model.dart';
import 'package:game_box_mobile_ui/pages/game_details.dart';

class GameItems extends StatefulWidget {
  final List<GameModel> _games;
  final Function _loadGames;
  final Function _loadMoreGames;

  const GameItems({
    required List<GameModel> games,
    required Function loadGames,
    required Function loadMoreGames,
  })  : _games = games,
        _loadGames = loadGames,
        _loadMoreGames = loadMoreGames;

  @override
  State<GameItems> createState() => _GameItemsState();
}

class _GameItemsState extends State<GameItems> {
  final controller = ScrollController();

  @override
  void initState() {
    super.initState();
    controller.addListener(() {
      if (controller.position.atEdge &&
          controller.position.pixels != 0 &&
          widget._games.length >= 9) {
        widget._loadMoreGames();
      }
    });
  }

  String getGameInfo(GameModel game) {
    return '\$${game.price} | ${game.size}GB | ${game.viewCount} Views';
  }

  @override
  Widget build(BuildContext context) {
    return RefreshIndicator(
      color: Constants.primaryColor,
      onRefresh: () => widget._loadGames(),
      child: ListView.builder(
        physics: const AlwaysScrollableScrollPhysics(),
        controller: controller,
        itemCount: widget._games.length,
        itemBuilder: (context, index) => ListTile(
          title: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              if (index == 0)
                const SizedBox(height: 20)
              else
                const SizedBox(height: 0),
              Text(
                widget._games[index].title,
                textAlign: TextAlign.center,
                style: const TextStyle(
                  fontSize: 18,
                  color: Colors.black,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 20),
              GestureDetector(
                onTap: () => Navigator.pushNamed(
                  context,
                  GameDetails.routeName,
                  arguments: widget._games[index].id,
                ),
                child: Image.network(widget._games[index].thumbnailUrl!),
              ),
              const SizedBox(height: 10),
              Text(
                getGameInfo(widget._games[index]),
                textAlign: TextAlign.center,
                style: const TextStyle(
                  fontSize: 16,
                  color: Colors.black,
                  fontWeight: FontWeight.bold,
                ),
              ),
              if (index < widget._games.length - 1)
                const Divider(
                  height: 30,
                  color: Colors.black,
                )
              else
                const SizedBox(height: 30),
            ],
          ),
        ),
      ),
    );
  }
}
