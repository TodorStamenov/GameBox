import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/api/wishlist_service.dart';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/models/wishlist_item_model.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
import 'package:game_box_mobile_ui/shared/spinner.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';

class Wishlist extends StatefulWidget {
  static const String routeName = '/wishlist';

  @override
  _WishlistState createState() => _WishlistState();
}

class _WishlistState extends State<Wishlist> {
  bool isLoading = true;
  List<WishlistItemModel> items = [];

  Future<void> getItems() async {
    var result = await getAllItems();

    if (!result.success) {
      showToast(result.message!);
    }

    if (mounted) {
      setState(() {
        this.isLoading = false;
        this.items = result.data;
      });
    }
  }

  Future<void> removeItem(String gameId) async {
    if (mounted) {
      this.setState(() => this.isLoading = true);
    }

    var result = await removeListItem(gameId);

    if (!result.success) {
      showToast(result.message!);
    }

    if (mounted) {
      setState(() {
        this.isLoading = false;
        this.items = result.data;
      });
    }
  }

  Future<void> clearItems() async {
    if (mounted) {
      this.setState(() => this.isLoading = true);
    }

    var result = await removeAllItems();

    if (!result.success) {
      showToast(result.message!);
    }

    if (mounted) {
      setState(() {
        this.isLoading = false;
        this.items = result.data;
      });
    }
  }

  @override
  void initState() {
    super.initState();
    this.getItems();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: SideDrawer(),
      appBar: Header(
        title: 'Wishlist',
      ),
      body: Spinner(
        isLoading: this.isLoading,
        child: this.items.isNotEmpty
            ? ListView.builder(
                itemCount: this.items.length + 1,
                itemBuilder: (context, index) => ListTile(
                  title: index == this.items.length
                      ? Column(
                          crossAxisAlignment: CrossAxisAlignment.end,
                          children: [
                            SizedBox(height: 20),
                            MaterialButton(
                              color: Constants.primaryColor,
                              padding: EdgeInsets.symmetric(
                                vertical: 10,
                                horizontal: 30,
                              ),
                              child: Text(
                                'Clear',
                                style: TextStyle(
                                  fontSize: 18,
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                ),
                              ),
                              onPressed: () => this.clearItems(),
                            ),
                            SizedBox(height: 20),
                          ],
                        )
                      : Column(
                          crossAxisAlignment: CrossAxisAlignment.stretch,
                          children: [
                            index == 0 ? SizedBox(height: 20) : SizedBox(height: 0),
                            Text(
                              this.items[index].title,
                              textAlign: TextAlign.center,
                              style: TextStyle(
                                fontSize: 18,
                                color: Colors.black,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                            SizedBox(height: 20),
                            Image.network(this.items[index].thumbnailUrl),
                            SizedBox(height: 20),
                            Text(
                              '\$${this.items[index].price}',
                              textAlign: TextAlign.end,
                              style: TextStyle(
                                fontSize: 16,
                                color: Colors.black,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                            SizedBox(height: 15),
                            Row(
                              mainAxisAlignment: MainAxisAlignment.end,
                              crossAxisAlignment: CrossAxisAlignment.center,
                              children: [
                                MaterialButton(
                                  color: Constants.primaryColor,
                                  padding: EdgeInsets.symmetric(
                                    vertical: 10,
                                    horizontal: 30,
                                  ),
                                  child: Text(
                                    'Buy',
                                    style: TextStyle(
                                      fontSize: 18,
                                      color: Colors.white,
                                      fontWeight: FontWeight.bold,
                                    ),
                                  ),
                                  onPressed: () {},
                                ),
                                SizedBox(width: 10),
                                MaterialButton(
                                  color: Constants.primaryColor,
                                  padding: EdgeInsets.symmetric(
                                    vertical: 10,
                                    horizontal: 30,
                                  ),
                                  child: Text(
                                    'Remove',
                                    style: TextStyle(
                                      fontSize: 18,
                                      color: Colors.white,
                                      fontWeight: FontWeight.bold,
                                    ),
                                  ),
                                  onPressed: () => this.removeItem(this.items[index].id),
                                ),
                              ],
                            ),
                            Divider(
                              height: 20,
                              color: Colors.black,
                            ),
                          ],
                        ),
                ),
              )
            : Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  SizedBox(height: 80),
                  Text(
                    'Your wishlist is empty!',
                    textAlign: TextAlign.center,
                    style: TextStyle(
                      fontSize: 20,
                      color: Colors.black,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ],
              ),
      ),
    );
  }
}
