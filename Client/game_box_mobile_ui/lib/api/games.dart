import 'dart:convert';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/auth/user.dart';
import 'package:game_box_mobile_ui/models/game/game.dart';
import 'package:game_box_mobile_ui/models/response.dart';
import 'package:game_box_mobile_ui/utils/storage.dart';
import 'package:http/http.dart' as http;

Future<Response> getAllGames(int loadedGames) async {
  var userEncoded = Storage.prefs?.getString('user');
  var token = User.fromJson(jsonDecode(userEncoded!));

  var response = await http.get(
    getUrl('?loadedGames=$loadedGames'),
    headers: {
      'content-type': 'application/json',
      'Authorization': 'Bearer $token',
    },
  );

  if (response.statusCode != 200) {
    return Response(
      success: false,
      message: 'Fetch games failed!',
      data: [],
    );
  }

  var games = jsonDecode(response.body) as List;

  return Response(
    success: true,
    data: games.map((g) => Game.fromJson(g)).toList(),
  );
}

Uri getUrl(String path) {
  return Uri.parse('${Constants.baseGameApiUrl}games/$path');
}
