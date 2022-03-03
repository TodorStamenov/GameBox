class GameCommentModel {
  String id;
  String content;
  String username;
  String dateAdded;

  GameCommentModel({
    required this.id,
    required this.content,
    required this.username,
    required this.dateAdded,
  });

  factory GameCommentModel.fromJson(Map<String, dynamic> json) {
    return GameCommentModel(
      id: json['id'] as String,
      content: json['content'] as String,
      username: json['username'] as String,
      dateAdded: json['dateAdded'] as String,
    );
  }
}
