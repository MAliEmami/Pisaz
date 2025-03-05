import 'package:flutter/material.dart';
import 'package:pisaz/login_page.dart';

void main() {
  runApp(const Pisaz());
}

class Pisaz extends StatelessWidget {
  const Pisaz({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      theme: ThemeData.light(),
      home: LoginPage(),
    );
  }
}
