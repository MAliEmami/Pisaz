import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:pisaz/components/cart_card.dart';
import 'package:pisaz/models/cart_status.dart';
import 'package:pisaz/services/backend.dart';

class CartsPage extends StatelessWidget {
  const CartsPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Directionality(
      textDirection: TextDirection.rtl,
      child: Scaffold(
        appBar: AppBar(
          title: Text('سبد های خرید'),
          elevation: 2,
          shadowColor: Colors.black,
        ),
        body: CartsList(),
      ),
    );
  }
}

class CartsList extends StatefulWidget {
  const CartsList({super.key});

  @override
  State<CartsList> createState() => _CartsListState();
}

class _CartsListState extends State<CartsList> {
  @override
  void initState() {
    super.initState();
    getCarts();
  }

  List<Cart>? carts;
  void getCarts() async {
    try {
      carts = await Backend.getCarts();
    } on Exception catch (e) {
      Fluttertoast.showToast(msg: e.toString());
      return;
    }
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    if (carts != null) {
      return CartList(carts: carts!);
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

class CartList extends StatelessWidget {
  const CartList({super.key, required this.carts});

  final List<Cart> carts;

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
        padding: const EdgeInsets.all(8),
        itemCount: carts.length,
        itemBuilder: (BuildContext context, int index) {
          return CartCard(
              number: carts[index].number, status: carts[index].status);
        });
  }
}
