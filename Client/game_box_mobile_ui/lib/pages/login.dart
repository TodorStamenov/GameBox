import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/shared/header.dart';
import 'package:game_box_mobile_ui/shared/side_drawer.dart';
import 'package:game_box_mobile_ui/style/constants.dart';

class Login extends StatefulWidget {
  static const String routeName = '/login';

  @override
  State<Login> createState() => _LoginState();
}

class _LoginState extends State<Login> {
  TextEditingController username = TextEditingController();
  TextEditingController password = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
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
              Center(
                child: MaterialButton(
                  color: Constants.primaryColor,
                  padding: EdgeInsets.symmetric(
                    vertical: 15,
                    horizontal: 50,
                  ),
                  child: Text(
                    'Log in',
                    style: TextStyle(
                      fontSize: 18,
                      color: Colors.white,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  onPressed: () => print('${this.username.text} - ${this.password.text}'),
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
