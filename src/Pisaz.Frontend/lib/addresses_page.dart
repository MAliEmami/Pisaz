import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:pisaz/components/address_card.dart';
import 'package:pisaz/models/address.dart';
import 'package:pisaz/services/backend.dart';

class AddressesPage extends StatelessWidget {
  const AddressesPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Directionality(
      textDirection: TextDirection.rtl,
      child: Scaffold(
        appBar: AppBar(
          title: Text('آدرس ها'),
          elevation: 2,
          shadowColor: Colors.black,
        ),
        body: AddressesList(),
      ),
    );
  }
}

class AddressesList extends StatefulWidget {
  const AddressesList({super.key});

  @override
  State<AddressesList> createState() => _AddressesListState();
}

class _AddressesListState extends State<AddressesList> {
  @override
  void initState() {
    super.initState();
    getAddresses();
  }

  List<Address>? addresses;
  void getAddresses() async {
    try {
      addresses = await Backend.getAddresses();
    } on Exception catch (e) {
      Fluttertoast.showToast(msg: e.toString());
      return;
    }
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    if (addresses != null) {
      return AddressList(addresses: addresses!);
    } else {
      return ProgressIndicator();
    }
  }
}

class ProgressIndicator extends StatelessWidget {
  const ProgressIndicator({super.key});

  @override
  Widget build(BuildContext context) {
    return Center(child: const CircularProgressIndicator());
  }
}

class AddressList extends StatelessWidget {
  const AddressList({super.key, required this.addresses});

  final List<Address> addresses;

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
        padding: const EdgeInsets.all(8),
        itemCount: addresses.length,
        itemBuilder: (BuildContext context, int index) {
          return AddressCard(
              province: addresses[index].province,
              remainder: addresses[index].remainder);
        });
  }
}
