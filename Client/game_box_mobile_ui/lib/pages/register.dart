import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/api/auth.dart';
import 'package:game_box_mobile_ui/pages/games.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
import 'package:game_box_mobile_ui/common/constants.dart';
import 'package:game_box_mobile_ui/utils/toaster.dart';

class Register extends StatefulWidget {
  static const String routeName = '/register';

  @override
  State<Register> createState() => _RegisterState();
}

class _RegisterState extends State<Register> {
  TextEditingController username = TextEditingController();
  TextEditingController password = TextEditingController();
  TextEditingController repeatPassword = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: Header(
        title: 'Register',
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
                cursorColor: Constants.primaryColor,
                decoration: InputDecoration(
                  hintText: 'Username',
                  labelText: 'username',
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
                controller: this.password,
                obscureText: true,
                cursorColor: Constants.primaryColor,
                decoration: InputDecoration(
                  hintText: 'Password',
                  labelText: 'password',
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
                controller: this.repeatPassword,
                obscureText: true,
                cursorColor: Constants.primaryColor,
                decoration: InputDecoration(
                  hintText: 'Repeat Password',
                  labelText: 'repeat password',
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
                child: MaterialButton(
                  color: Constants.primaryColor,
                  padding: EdgeInsets.symmetric(
                    vertical: 15,
                    horizontal: 50,
                  ),
                  child: Text(
                    'Register',
                    style: TextStyle(
                      fontSize: 18,
                      color: Colors.white,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  onPressed: () async {
                    var result = await register(
                        this.username.text, this.password.text, this.repeatPassword.text);

                    if (!result.success) {
                      return;
                    }

                    showToast(result.message!);
                    if (result.success) {
                      Navigator.pushReplacementNamed(context, Games.routeName);
                    }
                  },
                ),
              ),
            ],
          ),
        ),
      ),
      drawer: SideDrawer(),
    );
  }
}
