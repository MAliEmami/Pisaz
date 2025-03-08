import 'package:flutter/material.dart';

class AddressCard extends StatelessWidget {
  const AddressCard(
      {super.key, required this.province, required this.remainder});

  final String province;
  final String remainder;

  @override
  Widget build(BuildContext context) {
    return Card(
      child: ListTile(
        leading: Icon(Icons.discount_outlined),
        title: Text('استان : $province'),
        subtitle: Text('محل اقامت : $remainder'),
      ),
    );
  }
}
