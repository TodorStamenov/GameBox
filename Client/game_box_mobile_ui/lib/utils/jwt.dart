import 'dart:convert';
import 'package:game_box_mobile_ui/models/user_model.dart';
import 'package:game_box_mobile_ui/utils/storage.dart';

Map<String, String> getHttpHeaders() {
  var userEncoded = Storage.prefs?.getString('user');
  var user = UserModel.fromJson(jsonDecode(userEncoded!));

  return {
    'content-type': 'application/json',
    'Authorization': 'Bearer ${user.token}',
  };
}
