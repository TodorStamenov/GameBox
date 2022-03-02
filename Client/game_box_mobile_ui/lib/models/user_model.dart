class UserModel {
  final String id;
  final String username;
  final String token;
  final DateTime expirationDate;
  final bool isAdmin;

  UserModel({
    required this.id,
    required this.username,
    required this.token,
    required this.expirationDate,
    required this.isAdmin,
  });

  factory UserModel.fromJson(Map<String, dynamic> json) {
    return UserModel(
      id: json['id'] as String,
      token: json['token'] as String,
      isAdmin: json['isAdmin'] as bool,
      username: json['username'] as String,
      expirationDate: DateTime.parse(json['expirationDate'] as String),
    );
  }
}
