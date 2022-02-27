import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/game/game.dart';

class GameItems extends StatelessWidget {
  final List<Game> games;
  final Function loadGames;

  const GameItems({
    required this.games,
    required this.loadGames,
  });

  @override
  Widget build(BuildContext context) {
    return RefreshIndicator(
      color: Constants.primaryColor,
      onRefresh: () => loadGames(true),
      child: ListView.builder(
        itemCount: this.games.length,
        itemBuilder: (context, index) => ListTile(
          title: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              SizedBox(height: 20),
              Text(
                this.games[index].title,
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 18,
                  color: Colors.black,
                  fontWeight: FontWeight.bold,
                ),
              ),
              SizedBox(height: 20),
              Image.network(this.games[index].thumbnailUrl!),
              SizedBox(height: 10),
              Text(
                '\$${this.games[index].price} | ${this.games[index].size}GB | ${this.games[index].viewCount} Views',
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 16,
                  color: Colors.black,
                  fontWeight: FontWeight.bold,
                ),
              )
            ],
          ),
        ),
      ),
    );
  }
}
