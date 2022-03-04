import 'package:flutter/material.dart';
import 'package:youtube_player_flutter/youtube_player_flutter.dart';

class EmbededPlayer extends StatelessWidget {
  final String videoId;

  const EmbededPlayer({required this.videoId});

  @override
  Widget build(BuildContext context) {
    return YoutubePlayer(
      controller: YoutubePlayerController(
        initialVideoId: this.videoId,
        flags: YoutubePlayerFlags(
          autoPlay: false,
          mute: false,
        ),
      ),
      showVideoProgressIndicator: true,
      progressColors: ProgressBarColors(
        playedColor: Colors.red,
        handleColor: Colors.red,
      ),
    );
  }
}
