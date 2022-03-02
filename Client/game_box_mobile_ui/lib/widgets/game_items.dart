import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/game_model.dart';

class GameItems extends StatefulWidget {
  final List<GameModel> games;
  final Function loadGames;
  final Function loadMoreGames;

  const GameItems({
    required this.games,
    required this.loadGames,
    required this.loadMoreGames,
  });

  @override
  State<GameItems> createState() => _GameItemsState();
}

class _GameItemsState extends State<GameItems> {
  final controller = ScrollController();

  @override
  void initState() {
    super.initState();
    this.controller.addListener(() {
      if (this.controller.position.atEdge &&
          this.controller.position.pixels != 0 &&
          this.widget.games.length >= 9) {
        this.widget.loadMoreGames();
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
      onRefresh: () => this.widget.loadGames(),
      child: ListView.builder(
        physics: AlwaysScrollableScrollPhysics(),
        controller: this.controller,
        itemCount: this.widget.games.length,
        itemBuilder: (context, index) => ListTile(
          title: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              index == 0 ? SizedBox(height: 20) : SizedBox(height: 0),
              Text(
                this.widget.games[index].title,
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 18,
                  color: Colors.black,
                  fontWeight: FontWeight.bold,
                ),
              ),
              SizedBox(height: 20),
              Image.network(this.widget.games[index].thumbnailUrl!),
              SizedBox(height: 10),
              Text(
                this.getGameInfo(this.widget.games[index]),
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 16,
                  color: Colors.black,
                  fontWeight: FontWeight.bold,
                ),
              ),
              Divider(
                height: 30,
                color: Colors.black,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
