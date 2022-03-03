class GameDetailsModel {
  String id;
  String title;
  double price;
  double size;
  String videoId;
  String thumbnailUrl;
  String description;
  int viewCount;
  String releaseDate;

  GameDetailsModel({
    required this.id,
    required this.title,
    required this.price,
    required this.size,
    required this.videoId,
    required this.thumbnailUrl,
    required this.description,
    required this.viewCount,
    required this.releaseDate,
  });

  factory GameDetailsModel.fromJson(Map<String, dynamic> json) {
    var videoId = json['videoId'] as String;

    return GameDetailsModel(
      id: json['id'] as String,
      title: json['title'] as String,
      price: json['price'] as double,
      size: json['size'] as double,
      videoId: json['videoId'] as String,
      viewCount: json['viewCount'] as int,
      description: json['description'] as String,
      releaseDate: json['releaseDate'] as String,
      thumbnailUrl: json['thumbnailUrl'] == null
          ? 'https://i.ytimg.com/vi/$videoId/maxresdefault.jpg'
          : json['thumbnailUrl'] as String,
    );
  }
}
