import 'package:flutter/cupertino.dart';

class User {
  final String id;
  final String username;
  final String token;
  final String expirationDate;
  final bool isAdmin;

  User({
    required this.id,
    required this.username,
    required this.token,
    required this.expirationDate,
    required this.isAdmin,
  });

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      id: json['id'] as String,
      username: json['username'] as String,
      token: json['token'] as String,
      expirationDate: json['expirationDate'] as String,
      isAdmin: json['isAdmin'] as bool,
    );
  }
}
