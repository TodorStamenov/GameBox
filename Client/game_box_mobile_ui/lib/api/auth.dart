import 'dart:convert';
import 'package:game_box_mobile_ui/models/auth/user.dart';
import 'package:http/http.dart' as http;

Future<User> login(String username, String password) async {
  var response = await http.post(
    getUrl('login'),
    body: jsonEncode({'username': username, 'password': password}),
    headers: {'content-type': 'application/json'},
  );

  return User.fromJson(jsonDecode(response.body));
}

Future<User> register(String username, String password, String repeatPassword) async {
  var response = await http.post(
    getUrl('register'),
    body: jsonEncode({
      'username': username,
      'password': password,
      'repeatPassword': repeatPassword,
    }),
    headers: {'content-type': 'application/json'},
  );

  return User.fromJson(jsonDecode(response.body));
}

Uri getUrl(String path) {
  return Uri.parse('http://localhost:3334/api/auth/$path');
}
