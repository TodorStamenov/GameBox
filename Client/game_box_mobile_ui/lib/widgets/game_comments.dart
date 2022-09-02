import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/models/game_comment_model.dart';

class GameComments extends StatelessWidget {
  final List<GameCommentModel> _comments;

  const GameComments({
    required List<GameCommentModel> comments,
  }) : _comments = comments;

  Widget buildColumn(GameCommentModel comment) {
    var date = DateTime.parse(comment.dateAdded);

    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        const SizedBox(height: 10),
        Text(
          comment.username,
          style: const TextStyle(
            fontSize: 16,
            color: Colors.black,
            fontWeight: FontWeight.bold,
          ),
        ),
        const SizedBox(height: 10),
        Text(
          comment.content,
          style: const TextStyle(
            fontSize: 14,
            color: Colors.black,
          ),
        ),
        const SizedBox(height: 10),
        Text(
          '${date.month}/${date.day}/${date.year}',
          textAlign: TextAlign.end,
          style: const TextStyle(
            fontSize: 12,
            color: Colors.black,
            fontWeight: FontWeight.bold,
          ),
        ),
        const SizedBox(height: 10),
      ],
    );
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: _comments.map((c) => buildColumn(c)).toList(),
    );
  }
}
