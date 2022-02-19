import 'package:flutter/material.dart';

class Todo extends StatefulWidget {
  @override
  State<Todo> createState() => _TodoState();
}

class _TodoState extends State<Todo> {
  List<String> todos = [];

  void getData() async {
    List<String> todos = await Future.delayed(
      Duration(seconds: 2),
      () => [
        'Eat',
        'Sleep',
        'Code',
        'Repeat',
      ],
    );

    setState(() => this.todos = todos);
  }

  @override
  void initState() {
    super.initState();
    this.getData();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        elevation: 0,
        centerTitle: true,
        backgroundColor: Color(0xff343A40),
        title: Text(
          'Todos',
          style: TextStyle(
            fontSize: 18,
            color: Colors.white,
            fontWeight: FontWeight.bold,
          ),
        ),
      ),
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: this
            .todos
            .map((todo) => Padding(
                  padding: const EdgeInsets.only(top: 80),
                  child: Text(
                    todo,
                    textAlign: TextAlign.center,
                    style: TextStyle(
                      fontSize: 18,
                      color: Colors.black,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ))
            .toList(),
      ),
    );
  }
}
