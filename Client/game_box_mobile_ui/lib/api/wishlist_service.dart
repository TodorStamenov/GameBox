import 'dart:convert';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/response_model.dart';
import 'package:game_box_mobile_ui/models/wishlist_item_model.dart';
import 'package:game_box_mobile_ui/utils/jwt.dart';
import 'package:http/http.dart' as http;

Future<ResponseModel> getAllItems() async {
  var token = getUserToken();
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
    body: jsonEncode({
      'operationName': 'wishlist',
      'query': query,
      'variables': {},
    }),
    headers: {
      'content-type': 'application/json',
      'Authorization': 'Bearer $token',
    },
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Fetch wishlist items failed!',
      data: List<WishlistItemModel>.empty(),
    );
  }

  var items = jsonDecode(response.body)['data']['wishlist'] as List;

  return ResponseModel(
    success: true,
    data: items.map((wi) => WishlistItemModel.fromJson(wi)).toList(),
  );
}

Future<ResponseModel> removeListItem(String gameId) async {
  var token = getUserToken();
  var mutation = '''mutation removeGame(\$input: RemoveGameInput) {
      removeGame(input: \$input)
    }''';

  var response = await http.post(
    getUrl(),
    body: jsonEncode({
      'operationName': 'removeGame',
      'query': mutation,
      'variables': {
        'input': {'gameId': gameId}
      },
    }),
    headers: {
      'content-type': 'application/json',
      'Authorization': 'Bearer $token',
    },
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Remove wishlist item failed!',
      data: List<WishlistItemModel>.empty(),
    );
  }

  return await getAllItems();
}

Future<ResponseModel> removeAllItems() async {
  var token = getUserToken();
  var mutation = '''mutation clearGames {
      clearGames
    }''';

  var response = await http.post(
    getUrl(),
    body: jsonEncode({
      'operationName': 'clearGames',
      'query': mutation,
      'variables': {},
    }),
    headers: {
      'content-type': 'application/json',
      'Authorization': 'Bearer $token',
    },
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Clear wishlist items failed!',
      data: List<WishlistItemModel>.empty(),
    );
  }

  return await getAllItems();
}

Uri getUrl() {
  return Uri.parse(Constants.baseGraphQlUrl);
}
