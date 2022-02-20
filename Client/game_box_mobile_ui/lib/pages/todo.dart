import 'package:flutter/material.dart';
import 'package:game_box_mobile_ui/shared/header.dart';

class Todo extends StatefulWidget {
  static const String routeName = '/todos';

  @override
  State<Todo> createState() => _TodoState();
}

class _TodoState extends State<Todo> {
  bool isLoading = true;
  List<String> todos = [];

  void getTodos() async {
    List<String> todos = await Future.delayed(
      Duration(seconds: 2),
      () => [
        'Eat',
        'Sleep',
        'Code',
        'Repeat',
      ],
    );

    if (mounted) {
      setState(() => this.todos.addAll(todos));
      setState(() => this.isLoading = false);
    }
  }

  @override
  void initState() {
    super.initState();
    this.getTodos();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: Header(
        title: 'Todos',
      ),
      body: this.isLoading
          ? Center(
              child: CircularProgressIndicator(
                color: Color(0xff343A40),
              ),
            )
          : Column(
              children: [
                Expanded(
                  child: ListView.builder(
                    itemCount: this.todos.length,
                    itemBuilder: (context, index) => ListTile(
                      title: Text(
                        todos[index],
                        textAlign: TextAlign.center,
                        style: TextStyle(
                          fontSize: 18,
                          color: Colors.black,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                  ),
                ),
                MaterialButton(
                  color: Color(0xff343A40),
                  padding: EdgeInsets.symmetric(
                    vertical: 10,
                    horizontal: 20,
                  ),
                  child: Text(
                    'Load Todos!',
                    style: TextStyle(
                      fontSize: 18,
                      color: Colors.white,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  onPressed: () {
                    this.setState(() => this.isLoading = true);
                    this.getTodos();
                  },
                ),
              ],
            ),
    );
  }
}
