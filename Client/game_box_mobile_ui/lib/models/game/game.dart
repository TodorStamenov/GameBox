class Game {
  String id;
  String title;
  double price;
  double size;
  String videoId;
  String? thumbnailUrl;
  String description;
  int viewCount;

  Game({
    required this.id,
    required this.title,
    required this.price,
    required this.size,
    required this.videoId,
    required this.thumbnailUrl,
    required this.description,
    required this.viewCount,
  });

  factory Game.fromJson(Map<String, dynamic> json) {
    return Game(
      id: json['id'] as String,
      title: json['title'] as String,
      price: json['price'] as double,
      size: json['size'] as double,
      videoId: json['videoId'] as String,
      thumbnailUrl: json['thumbnailUrl'] == null ? '' : json['thumbnailUrl'] as String,
      description: json['description'] as String,
      viewCount: json['viewCount'] as int,
    );
  }
}
