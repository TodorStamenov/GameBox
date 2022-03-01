import 'dart:convert';
import 'package:game_box_mobile_ui/models/auth/user.dart';
import 'package:game_box_mobile_ui/utils/storage.dart';

String getUserToken() {
  var userEncoded = Storage.prefs?.getString('user');
  var user = User.fromJson(jsonDecode(userEncoded!));

  return user.token;
}
