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
  final _formKey = GlobalKey<FormState>();

  final _username = TextEditingController();
  final _password = TextEditingController();

  Future<void> loginUser() async {
    if (!_formKey.currentState!.validate()) {
      return;
    }

    var result = await login(_username.text, _password.text);
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
        title: 'Login',
      ),
      body: Center(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(40),
          child: Form(
            key: _formKey,
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
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Username is required';
                    }

                    return null;
                  },
                ),
                const SizedBox(height: 50),
                TextFormField(
                  obscureText: true,
                  controller: _password,
                  textInputAction: TextInputAction.done,
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
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Password is required';
                    }

                    return null;
                  },
                ),
                const SizedBox(height: 50),
                Center(
                  child: PrimaryActionButton(
                    text: 'Log in',
                    action: loginUser,
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
