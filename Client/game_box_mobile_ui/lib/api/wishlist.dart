import 'dart:convert';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/response.dart';
import 'package:game_box_mobile_ui/models/wishlist/wishlist_item.dart';
import 'package:game_box_mobile_ui/utils/jwt.dart';
import 'package:http/http.dart' as http;

Future<Response> getAllItems() async {
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
    return Response(
      success: false,
      message: 'Fetch wishlist items failed!',
      data: [],
    );
  }

  var items = jsonDecode(response.body)['data']['wishlist'] as List;

  return Response(
    success: true,
    data: items.map((wi) => WishlistItem.fromJson(wi)).toList(),
  );
}

Uri getUrl() {
  return Uri.parse(Constants.baseGraphQlUrl);
}
