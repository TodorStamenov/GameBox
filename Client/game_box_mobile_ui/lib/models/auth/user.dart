class User {
  final String id;
  final String username;
  final String token;
  final DateTime expirationDate;
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
      token: json['token'] as String,
      isAdmin: json['isAdmin'] as bool,
      username: json['username'] as String,
      expirationDate: DateTime.parse(json['expirationDate'] as String),
    );
  }
}
