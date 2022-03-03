import 'dart:convert';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/response_model.dart';
import 'package:game_box_mobile_ui/models/game_list_item_model.dart';
import 'package:game_box_mobile_ui/utils/jwt.dart';
import 'package:http/http.dart' as http;

Future<ResponseModel> getAllWishlistItems() async {
  var query = '''
    query wishlist {
      wishlist {
        id
        title
        price
        thumbnailUrl
        videoId
      }
    }''';

  var response = await http.post(
    getUrl(),
    headers: getHttpHeaders(),
    body: jsonEncode({
      'operationName': 'wishlist',
      'query': query,
      'variables': {},
    }),
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Fetch wishlist items failed!',
      data: List<GameListItemModel>.empty(),
    );
  }

  var items = jsonDecode(response.body)['data']['wishlist'] as List;

  return ResponseModel(
    success: true,
    data: items.map((g) => GameListItemModel.fromJson(g)).toList(),
  );
}

Future<ResponseModel> addWishlistItem(String gameId) async {
  var mutation = '''mutation addGame(\$input: AddGameInput) {
      addGame(input: \$input)
    }''';

  var response = await http.post(
    getUrl(),
    headers: getHttpHeaders(),
    body: jsonEncode({
      'operationName': 'addGame',
      'query': mutation,
      'variables': {
        'input': {'gameId': gameId}
      },
    }),
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Add wishlist item failed!',
      data: List<GameListItemModel>.empty(),
    );
  }

  return await getAllWishlistItems();
}

Future<ResponseModel> removeWishlistItem(String gameId) async {
  var mutation = '''mutation removeGame(\$input: RemoveGameInput) {
      removeGame(input: \$input)
    }''';

  var response = await http.post(
    getUrl(),
    headers: getHttpHeaders(),
    body: jsonEncode({
      'operationName': 'removeGame',
      'query': mutation,
      'variables': {
        'input': {'gameId': gameId}
      },
    }),
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Remove wishlist item failed!',
      data: List<GameListItemModel>.empty(),
    );
  }

  return await getAllWishlistItems();
}

Future<ResponseModel> removeWishlistItems() async {
  var mutation = '''mutation clearGames {
      clearGames
    }''';

  var response = await http.post(
    getUrl(),
    headers: getHttpHeaders(),
    body: jsonEncode({
      'operationName': 'clearGames',
      'query': mutation,
      'variables': {},
    }),
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Clear wishlist items failed!',
      data: List<GameListItemModel>.empty(),
    );
  }

  return await getAllWishlistItems();
}

Uri getUrl() {
  return Uri.parse(Constants.baseGraphQlUrl);
}
