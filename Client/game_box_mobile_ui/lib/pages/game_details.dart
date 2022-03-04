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
  bool isLoading = true;
  bool showCommentForm = false;
  List<GameCommentModel> comments = [];
  TextEditingController comment = TextEditingController();
  GameDetailsModel game = GameDetailsModel(
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
        this.isLoading = false;
        this.game = gameResult.data;
        this.comments = commentsResult.data;
      });
    }
  }

  Future<void> addWishlistGame(String gameId) async {
    if (mounted) {
      this.setState(() => this.isLoading = true);
    }

    var result = await addWishlistItem(gameId);

    if (!result.success) {
      showToast(result.message!);
      this.setState(() => this.isLoading = false);
      return;
    }

    Navigator.pushReplacementNamed(context, Wishlist.routeName);
  }

  Future<void> addComment() async {
    if (this.comment.text.length < 3) {
      showToast('Comment length must be at least three symbols long!');
      return;
    }

    if (mounted) {
      this.setState(() {
        this.isLoading = true;
        this.showCommentForm = false;
      });
    }

    var addCommentresult = await addGameComment(this.game.id, this.comment.text);

    if (!addCommentresult.success) {
      showToast(addCommentresult.message!);
      this.setState(() => this.isLoading = false);
      return;
    }

    var commentsResult = await getGameComments(this.game.id);

    if (!commentsResult.success) {
      showToast(commentsResult.message!);
      this.setState(() => this.isLoading = false);
      return;
    }

    if (mounted) {
      setState(() {
        this.isLoading = false;
        this.showCommentForm = false;
        this.comment.clear();
        this.comments = commentsResult.data;
      });
    }
  }

  @override
  void initState() {
    super.initState();
    Future.microtask(() async {
      var gameId = ModalRoute.of(context)?.settings.arguments as String;
      await this.getGameAndComments(gameId);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: Header(
        title: 'Game Details',
      ),
      body: Spinner(
        isLoading: this.isLoading,
        child: SingleChildScrollView(
          padding: EdgeInsets.symmetric(
            vertical: 15,
            horizontal: 15,
          ),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              EmbededPlayer(videoId: this.game.videoId),
              SizedBox(height: 40),
              Text(
                this.game.title,
                textAlign: TextAlign.center,
                style: TextStyle(
                  color: Colors.black,
                  fontWeight: FontWeight.bold,
                  fontSize: 20,
                ),
              ),
              SizedBox(height: 20),
              Text(
                this.game.description,
                style: TextStyle(
                  color: Colors.black,
                  fontSize: 15,
                ),
              ),
              Divider(
                height: 30,
                color: Colors.black,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  GameInfo(text: 'Price: \$${this.game.price.toStringAsFixed(2)}'),
                  SizedBox(width: 20),
                  GameInfo(text: 'Size: ${this.game.size.toStringAsFixed(1)} GB'),
                ],
              ),
              SizedBox(height: 10),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  GameInfo(text: 'Views: ${this.game.viewCount}'),
                  SizedBox(width: 20),
                  GameInfo(text: 'Release Date: ${this.game.releaseDate}'),
                ],
              ),
              Divider(
                height: 30,
                color: Colors.black,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  PrimaryActionButton(
                    icon: Icons.shopping_cart,
                    action: () {
                      addItem(this.game.id);
                      Navigator.pushReplacementNamed(context, Cart.routeName);
                    },
                  ),
                  SizedBox(width: 10),
                  PrimaryActionButton(
                    icon: Icons.favorite,
                    action: () => this.addWishlistGame(this.game.id),
                  ),
                  SizedBox(width: 10),
                  PrimaryActionButton(
                    icon: Icons.comment,
                    action: () => this.setState(() => this.showCommentForm = !this.showCommentForm),
                  ),
                ],
              ),
              if (this.showCommentForm) ...[
                SizedBox(height: 20),
                Center(
                  child: PrimaryActionButton(
                    text: 'Save',
                    action: this.addComment,
                  ),
                ),
                SizedBox(height: 20),
                TextFormField(
                  minLines: 4,
                  maxLines: null,
                  controller: this.comment,
                  textInputAction: TextInputAction.done,
                  keyboardType: TextInputType.multiline,
                  cursorColor: Constants.primaryColor,
                  decoration: InputDecoration(
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
              SizedBox(height: 20),
              GameComments(comments: this.comments),
            ],
          ),
        ),
      ),
    );
  }
}
