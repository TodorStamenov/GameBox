import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/models/game_comment_model.dart';

class GameComments extends StatelessWidget {
  final List<GameCommentModel> comments;

  const GameComments({required this.comments});

  Widget buildColumn(GameCommentModel comment) {
    var date = DateTime.parse(comment.dateAdded);

    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        SizedBox(height: 10),
        Text(
          comment.username,
          style: TextStyle(
            fontSize: 16,
            color: Colors.black,
            fontWeight: FontWeight.bold,
          ),
        ),
        SizedBox(height: 10),
        Text(
          comment.content,
          style: TextStyle(
            fontSize: 14,
            color: Colors.black,
          ),
        ),
        SizedBox(height: 10),
        Text(
          '${date.month}/${date.day}/${date.year}',
          textAlign: TextAlign.end,
          style: TextStyle(
            fontSize: 12,
            color: Colors.black,
            fontWeight: FontWeight.bold,
          ),
        ),
        SizedBox(height: 10),
      ],
    );
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: this.comments.map((c) => this.buildColumn(c)).toList(),
    );
  }
}
