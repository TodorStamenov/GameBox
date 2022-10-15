import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/game_comment_model.dart';
import 'package:game_box_mobile_ui/models/game_details_model.dart';
import 'package:game_box_mobile_ui/pages/cart.dart';
import 'package:game_box_mobile_ui/pages/wishlist.dart';
import 'package:game_box_mobile_ui/services/cart_services.dart';
import 'package:game_box_mobile_ui/services/games_service.dart';
import 'package:game_box_mobile_ui/services/wishlist_service.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/spinner.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';
import 'package:game_box_mobile_ui/widgets/embeded_player.dart';
import 'package:game_box_mobile_ui/widgets/game_comments.dart';
import 'package:game_box_mobile_ui/widgets/game_info.dart';
import 'package:game_box_mobile_ui/widgets/primary_action_button.dart';

class GameDetails extends StatefulWidget {
  static const String routeName = '/game-details';

  @override
  State<GameDetails> createState() => _GameDetailsState();
}

class _GameDetailsState extends State<GameDetails> {
  bool _isLoading = true;
  bool _showCommentForm = false;
  List<GameCommentModel> _comments = [];

  final _comment = TextEditingController();
  GameDetailsModel _game = GameDetailsModel(
    id: '',
    title: '',
    price: 0,
    size: 0,
    videoId: '',
    thumbnailUrl: '',
    description: '',
    viewCount: 0,
    releaseDate: '',
  );

  Future<void> getGameAndComments(String gameId) async {
    var results = await Future.wait([
      getGameDetails(gameId),
      getGameComments(gameId),
    ]);

    var gameResult = results[0];
    var commentsResult = results[1];

    if (!gameResult.success) {
      showToast(gameResult.message!);
    }

    if (!commentsResult.success) {
      showToast(commentsResult.message!);
    }

    if (mounted) {
      setState(() {
        _isLoading = false;
        _game = gameResult.data;
        _comments = commentsResult.data;
      });
    }
  }

  Future<void> addWishlistGame(String gameId) async {
    if (mounted) {
      setState(() => _isLoading = true);
    }

    var result = await addWishlistItem(gameId);

    if (!result.success) {
      showToast(result.message!);
      setState(() => _isLoading = false);
      return;
    }

    if (mounted) {
      Navigator.pushReplacementNamed(context, Wishlist.routeName);
    }
  }

  Future<void> addComment() async {
    if (_comment.text.length < 3) {
      showToast('Comment length must be at least three symbols long!');
      return;
    }

    if (mounted) {
      setState(() {
        _isLoading = true;
        _showCommentForm = false;
      });
    }

    var addCommentresult = await addGameComment(_game.id, _comment.text);

    if (!addCommentresult.success) {
      showToast(addCommentresult.message!);
      setState(() => _isLoading = false);
      return;
    }

    var commentsResult = await getGameComments(_game.id);

    if (!commentsResult.success) {
      showToast(commentsResult.message!);
      setState(() => _isLoading = false);
      return;
    }

    if (mounted) {
      setState(() {
        _isLoading = false;
        _showCommentForm = false;
        _comment.clear();
        _comments = commentsResult.data;
      });
    }
  }

  @override
  void initState() {
    super.initState();
    Future.microtask(() async {
      var gameId = ModalRoute.of(context)?.settings.arguments as String;
      await getGameAndComments(gameId);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const Header(
        title: 'Game Details',
      ),
      body: Spinner(
        isLoading: _isLoading,
        child: SingleChildScrollView(
          padding: const EdgeInsets.symmetric(
            vertical: 15,
            horizontal: 15,
          ),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              EmbededPlayer(videoId: _game.videoId),
              const SizedBox(height: 40),
              Text(
                _game.title,
                textAlign: TextAlign.center,
                style: const TextStyle(
                  color: Colors.black,
                  fontWeight: FontWeight.bold,
                  fontSize: 20,
                ),
              ),
              const SizedBox(height: 20),
              Text(
                _game.description,
                style: const TextStyle(
                  color: Colors.black,
                  fontSize: 15,
                ),
              ),
              const Divider(
                height: 30,
                color: Colors.black,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  GameInfo(text: 'Price: \$${_game.price.toStringAsFixed(2)}'),
                  const SizedBox(width: 20),
                  GameInfo(text: 'Size: ${_game.size.toStringAsFixed(1)} GB'),
                ],
              ),
              const SizedBox(height: 10),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  GameInfo(text: 'Views: ${_game.viewCount}'),
                  const SizedBox(width: 20),
                  GameInfo(text: 'Release Date: ${_game.releaseDate}'),
                ],
              ),
              const Divider(
                height: 30,
                color: Colors.black,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  PrimaryActionButton(
                    icon: Icons.shopping_cart,
                    action: () {
                      addItem(_game.id);
                      Navigator.pushReplacementNamed(context, Cart.routeName);
                    },
                  ),
                  const SizedBox(width: 10),
                  PrimaryActionButton(
                    icon: Icons.favorite,
                    action: () => addWishlistGame(_game.id),
                  ),
                  const SizedBox(width: 10),
                  PrimaryActionButton(
                    icon: Icons.comment,
                    action: () => setState(() => _showCommentForm = !_showCommentForm),
                  ),
                ],
              ),
              if (_showCommentForm) ...[
                const SizedBox(height: 20),
                Center(
                  child: PrimaryActionButton(
                    text: 'Save',
                    action: addComment,
                  ),
                ),
                const SizedBox(height: 20),
                TextFormField(
                  minLines: 4,
                  maxLines: null,
                  controller: _comment,
                  textInputAction: TextInputAction.done,
                  keyboardType: TextInputType.multiline,
                  cursorColor: Constants.primaryColor,
                  decoration: const InputDecoration(
                    labelText: 'Comment',
                    labelStyle: TextStyle(
                      color: Constants.primaryColor,
                    ),
                    focusedBorder: UnderlineInputBorder(
                      borderSide: BorderSide(
                        color: Constants.primaryColor,
                        width: 2,
                      ),
                    ),
                  ),
                ),
              ],
              const SizedBox(height: 20),
              GameComments(comments: _comments),
            ],
          ),
        ),
      ),
    );
  }
}
