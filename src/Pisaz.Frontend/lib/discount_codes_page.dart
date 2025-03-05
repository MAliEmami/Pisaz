import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:pisaz/components/discount_code_card.dart';
import 'package:pisaz/models/discount_code.dart';
import 'package:pisaz/services/backend.dart';

class DiscountCodesPage extends StatelessWidget {
  const DiscountCodesPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Directionality(
      textDirection: TextDirection.rtl,
      child: Scaffold(
        appBar: AppBar(
          title: Text('کدهای تخفیف'),
          elevation: 2,
          shadowColor: Colors.black,
        ),
        body: DiscountCodesList(),
      ),
    );
  }
}

class DiscountCodesList extends StatefulWidget {
  const DiscountCodesList({super.key});

  @override
  State<DiscountCodesList> createState() => _DiscountCodesListState();
}

class _DiscountCodesListState extends State<DiscountCodesList> {
  @override
  void initState() {
    super.initState();
    getDiscountCodes();
  }

  List<DiscountCode>? discounts;
  void getDiscountCodes() async {
    try {
      discounts = await Backend.getDicountCodes();
    } on Exception catch (e) {
      Fluttertoast.showToast(msg: e.toString());
      print(e.toString());
      return;
    }
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    if (discounts != null) {
      return DiscountList(discounts: discounts!);
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

class DiscountList extends StatelessWidget {
  const DiscountList({super.key, required this.discounts});

  final List<DiscountCode> discounts;

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
        padding: const EdgeInsets.all(8),
        itemCount: discounts.length,
        itemBuilder: (BuildContext context, int index) {
          return DiscountCodeCard(
            code: discounts[index].code,
            amount: discounts[index].amount,
            limit: discounts[index].limit,
            usageCount: discounts[index].usageCount,
            expiration: discounts[index].expirationTime,
          );
        });
  }
}
