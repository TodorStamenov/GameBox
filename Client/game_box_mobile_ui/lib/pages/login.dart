import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/pages/games.dart';
import 'package:game_box_mobile_ui/services/auth_service.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';
import 'package:game_box_mobile_ui/widgets/primary_action_button.dart';

class Login extends StatefulWidget {
  static const String routeName = '/login';

  @override
  State<Login> createState() => _LoginState();
}

class _LoginState extends State<Login> {
  TextEditingController username = TextEditingController();
  TextEditingController password = TextEditingController();

  Future<void> loginUser() async {
    var result = await login(this.username.text, this.password.text);
    showToast(result.message!);

    if (mounted && result.success) {
      Navigator.pushReplacementNamed(context, Games.routeName);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: SideDrawer(),
      appBar: Header(
        title: 'Login',
      ),
      body: Center(
        child: SingleChildScrollView(
          padding: EdgeInsets.all(40),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              TextFormField(
                controller: this.username,
                textInputAction: TextInputAction.next,
                cursorColor: Constants.primaryColor,
                decoration: InputDecoration(
                  labelText: 'Username',
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
              SizedBox(height: 50),
              TextFormField(
                obscureText: true,
                controller: this.password,
                textInputAction: TextInputAction.done,
                cursorColor: Constants.primaryColor,
                decoration: InputDecoration(
                  labelText: 'Password',
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
              SizedBox(height: 50),
              Center(
                child: PrimaryActionButton(
                  text: 'Log in',
                  action: this.loginUser,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
