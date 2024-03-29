class GameListItemModel {
  String id;
  String title;
  String videoId;
  String thumbnailUrl;
  double price;

  GameListItemModel({
    required this.id,
    required this.title,
    required this.videoId,
    required this.thumbnailUrl,
    required this.price,
  });

  factory GameListItemModel.fromJson(Map<String, dynamic> json) {
    var videoId = json['videoId'] as String;

    return GameListItemModel(
      id: json['id'] as String,
      title: json['title'] as String,
      videoId: json['videoId'] as String,
      price: json['price'] as double,
      thumbnailUrl: json['thumbnailUrl'] == null
          ? 'https://i.ytimg.com/vi/$videoId/maxresdefault.jpg'
          : json['thumbnailUrl'] as String,
    );
  }
}
