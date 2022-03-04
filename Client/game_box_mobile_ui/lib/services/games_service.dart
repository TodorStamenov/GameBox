import 'dart:convert';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/game_comment_model.dart';
import 'package:game_box_mobile_ui/models/game_details_model.dart';
import 'package:game_box_mobile_ui/models/game_model.dart';
import 'package:game_box_mobile_ui/models/response_model.dart';
import 'package:game_box_mobile_ui/utils/jwt.dart';
import 'package:http/http.dart' as http;

Future<ResponseModel> getGameDetails(String gameId) async {
  var response = await http.get(
    getGamesUrl('details/$gameId'),
    headers: getHttpHeaders(),
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Fetch game details failed!',
      data: null,
    );
  }

  var game = GameDetailsModel.fromJson(jsonDecode(response.body));

  return ResponseModel(
    success: true,
    data: game,
  );
}

Future<ResponseModel> getGameComments(String gameId) async {
  var response = await http.get(
    getGameCommentsUrl(gameId),
    headers: getHttpHeaders(),
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Fetch game comments failed!',
      data: List<GameCommentModel>.empty(),
    );
  }

  var comments = jsonDecode(response.body) as List;

  return ResponseModel(
    success: true,
    data: comments.map((c) => GameCommentModel.fromJson(c)).toList(),
  );
}

Future<ResponseModel> addGameComment(String gameId, String content) async {
  var response = await http.post(
    getGameCommentsUrl(''),
    headers: getHttpHeaders(),
    body: jsonEncode({
      'gameId': gameId,
      'content': content,
    }),
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Add comment to game failed!',
    );
  }

  return ResponseModel(
    success: true,
  );
}

Future<ResponseModel> getAllGames(int loadedGames) async {
  var response = await http.get(
    getGamesUrl('?loadedGames=$loadedGames'),
    headers: getHttpHeaders(),
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Fetch all games failed!',
      data: List<GameModel>.empty(),
    );
  }

  var games = jsonDecode(response.body) as List;

  return ResponseModel(
    success: true,
    data: games.map((g) => GameModel.fromJson(g)).toList(),
  );
}

Future<ResponseModel> getOwnedGames(int loadedGames) async {
  var response = await http.get(
    getGamesUrl('owned?loadedGames=$loadedGames'),
    headers: getHttpHeaders(),
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Fetch owned games failed!',
      data: List<GameModel>.empty(),
    );
  }

  var games = jsonDecode(response.body) as List;

  return ResponseModel(
    success: true,
    data: games.map((g) => GameModel.fromJson(g)).toList(),
  );
}

Uri getGamesUrl(String path) {
  return Uri.parse('${Constants.baseGameApiUrl}games/$path');
}

Uri getGameCommentsUrl(String path) {
  return Uri.parse('${Constants.baseGameApiUrl}comments/$path');
}
