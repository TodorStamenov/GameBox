import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/models/game_comment_model.dart';
import 'package:game_box_mobile_ui/models/game_details_model.dart';
import 'package:game_box_mobile_ui/services/games_service.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/spinner.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';

class GameDetails extends StatefulWidget {
  static const String routeName = '/game-details';

  @override
  State<GameDetails> createState() => _GameDetailsState();
}

class _GameDetailsState extends State<GameDetails> {
  bool isLoading = true;
  GameDetailsModel? game;
  List<GameCommentModel> comments = [];

  Future<void> getGame(String gameId) async {
    var results = await Future.wait([
      getGameDetails(gameId),
      getGameComments(gameId),
    ]);

    var gameResult = results[0];
    var commentsResult = results[1];

    if (!gameResult.success) {
      showToast(gameResult.message!);
    }

    if (!commentsResult.success) {
      showToast(commentsResult.message!);
    }

    if (mounted) {
      setState(() {
        this.isLoading = false;
        this.game = gameResult.data;
        this.comments = commentsResult.data;
      });
    }
  }

  @override
  void initState() {
    super.initState();
    Future.microtask(() async {
      var gameId = ModalRoute.of(context)?.settings.arguments as String;
      await this.getGame(gameId);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: Header(
        title: 'Game Details',
      ),
      body: Spinner(
        isLoading: this.isLoading,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Text(
              this.game?.title ?? 'Failed!',
              textAlign: TextAlign.center,
            ),
            ...this
                .comments
                .map((c) => Column(
                      children: [
                        SizedBox(height: 20),
                        Text(c.content),
                        SizedBox(height: 20),
                      ],
                    ))
                .toList(),
          ],
        ),
      ),
    );
  }
}
