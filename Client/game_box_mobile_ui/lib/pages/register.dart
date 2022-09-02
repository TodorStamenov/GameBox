import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/pages/games.dart';
import 'package:game_box_mobile_ui/services/auth_service.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';
import 'package:game_box_mobile_ui/widgets/primary_action_button.dart';

class Register extends StatefulWidget {
  static const String routeName = '/register';

  @override
  State<Register> createState() => _RegisterState();
}

class _RegisterState extends State<Register> {
  final TextEditingController _username = TextEditingController();
  final TextEditingController _password = TextEditingController();
  final TextEditingController _repeatPassword = TextEditingController();

  Future<void> registerUser() async {
    var result = await register(_username.text, _password.text, _repeatPassword.text);

    if (!result.success) {
      return;
    }

    showToast(result.message!);
    if (mounted && result.success) {
      Navigator.pushReplacementNamed(context, Games.routeName);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: SideDrawer(),
      appBar: const Header(
        title: 'Register',
      ),
      body: Center(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(40),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              TextFormField(
                controller: _username,
                textInputAction: TextInputAction.next,
                cursorColor: Constants.primaryColor,
                decoration: const InputDecoration(
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
              const SizedBox(height: 50),
              TextFormField(
                obscureText: true,
                controller: _password,
                textInputAction: TextInputAction.next,
                cursorColor: Constants.primaryColor,
                decoration: const InputDecoration(
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
              const SizedBox(height: 50),
              TextFormField(
                obscureText: true,
                controller: _repeatPassword,
                textInputAction: TextInputAction.done,
                cursorColor: Constants.primaryColor,
                decoration: const InputDecoration(
                  labelText: 'Repeat Password',
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
              const SizedBox(height: 50),
              Center(
                child: PrimaryActionButton(
                  text: 'Register',
                  action: registerUser,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
