import 'dart:convert';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/auth/user.dart';
import 'package:game_box_mobile_ui/models/response.dart';
import 'package:game_box_mobile_ui/utils/storage.dart';
import 'package:http/http.dart' as http;

Future<Response> login(String username, String password) async {
  var response = await http.post(
    getUrl('login'),
    body: jsonEncode({'username': username, 'password': password}),
    headers: {'content-type': 'application/json'},
  );

  if (response.statusCode != 200) {
    return Response(
      success: false,
      message: 'Login failed!',
    );
  }

  Storage.prefs!.setString('user', response.body);
  var user = User.fromJson(jsonDecode(response.body));

  return Response(
    success: true,
    message: 'Login successful!',
    data: user,
  );
}

Future<Response> register(String username, String password, String repeatPassword) async {
  var response = await http.post(
    getUrl('register'),
    body: jsonEncode({
      'username': username,
      'password': password,
      'repeatPassword': repeatPassword,
    }),
    headers: {'content-type': 'application/json'},
  );

  if (response.statusCode != 200) {
    return Response(
      success: false,
      message: 'Register failed!',
    );
  }

  Storage.prefs!.setString('user', response.body);
  var user = User.fromJson(jsonDecode(response.body));

  return Response(
    success: true,
    message: 'Login successful',
    data: user,
  );
}

Uri getUrl(String path) {
  return Uri.parse('${Constants.baseUserApiUrl}auth/$path');
}
