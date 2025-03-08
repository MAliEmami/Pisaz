import 'package:flutter/material.dart';

class CartCard extends StatelessWidget {
  const CartCard({super.key, required this.number, required this.status});

  final int number;
  final String status;

  @override
  Widget build(BuildContext context) {
    return Card(
      child: ListTile(
        leading: Icon(Icons.discount_outlined),
        title: Text('شماره : $number'),
        subtitle: Text(' وضعیت : $status'),
      ),
    );
  }
}
