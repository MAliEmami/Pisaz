import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class DiscountCodeCard extends StatelessWidget {
  const DiscountCodeCard(
      {super.key,
      required this.code,
      required this.amount,
      required this.limit,
      required this.usageCount,
      required this.expiration});

  final int code;
  final int amount;
  final int limit;
  final int usageCount;
  final DateTime expiration;

  @override
  Widget build(BuildContext context) {
    return Card(
      child: ListTile(
        leading: Icon(Icons.discount_outlined),
        title: Text(
            limit == 0 ? 'کد تخفیف $amount ریالی' : 'کد تخفیف $amount درصدی'),
        subtitle: Text(
            'تاریخ انقضا : ${DateFormat('yyyy-MM-dd – kk:mm').format(expiration)} \n تعداد : $usageCount ${limit != 0 ? '\n سقف : $limit ریال' : ''}'),
        trailing: Container(
          decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.circular(2.0),
            border: Border.all(),
          ),
          padding: EdgeInsets.all(4.0),
          child: Text(
            'کد : $code',
            style: TextStyle(
              fontSize: 15.0,
              fontWeight: FontWeight.bold,
            ),
          ),
        ),
      ),
    );
  }
}
