import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/models/game_details_model.dart';
import 'package:game_box_mobile_ui/services/games_service.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
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

  Future<void> getGame(String gameId) async {
    var result = await getGameDetails(gameId);

    if (!result.success) {
      showToast(result.message!);
    }

    if (mounted) {
      setState(() {
        this.isLoading = false;
        this.game = result.data;
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
        child: Center(
          child: Text(this.game?.title ?? 'Failed!'),
        ),
      ),
    );
  }
}
