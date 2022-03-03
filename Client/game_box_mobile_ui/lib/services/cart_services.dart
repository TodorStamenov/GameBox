import 'dart:convert';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/game_list_item_model.dart';
import 'package:game_box_mobile_ui/models/response_model.dart';
import 'package:game_box_mobile_ui/utils/jwt.dart';
import 'package:game_box_mobile_ui/utils/storage.dart';
import 'package:http/http.dart' as http;

List<String> getCart() {
  if (Storage.prefs?.getString('cart') == null) {
    Storage.prefs?.setString('cart', jsonEncode([]));
  }

  var cart = jsonDecode((Storage.prefs?.getString('cart'))!) as List;

  return cart.map((c) => c.toString()).toList();
}

void setCart(List<String> items) {
  if (Storage.prefs?.getString('cart') == null) {
    Storage.prefs?.setString('cart', jsonEncode([]));
  }

  Storage.prefs?.setString('cart', jsonEncode(items.toSet().toList()));
}

void addItem(String id) {
  setCart([...getCart(), id]);
}

void removeItem(String id) {
  setCart(getCart().where((i) => i != id).toList());
}

void clearItems() {
  setCart([]);
}

Future<ResponseModel> upsertCart() async {
  var response = await http.post(
    Uri.parse('${Constants.baseGameApiUrl}cart'),
    headers: getHttpHeaders(),
    body: jsonEncode(getCart()),
  );

  if (response.statusCode != 200) {
    return ResponseModel(
      success: false,
      message: 'Fetch cart items failed!',
      data: List<GameListItemModel>.empty(),
    );
  }

  var games = jsonDecode(response.body) as List;

  return ResponseModel(
    success: true,
    data: games.map((g) => GameListItemModel.fromJson(g)).toList(),
  );
}
