import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:pisaz/models/refereal_data.dart';
import 'package:pisaz/services/backend.dart';

class ReferralPage extends StatefulWidget {
  const ReferralPage({super.key});

  @override
  State<ReferralPage> createState() => _ReferralPageState();
}

class _ReferralPageState extends State<ReferralPage> {
  @override
  void initState() {
    super.initState();
    getReferralData();
  }

  ReferralData? referralData;
  Future<void> getReferralData() async {
    try {
      referralData = await Backend.getReferralData();
    } on Exception catch (e) {
      Fluttertoast.showToast(msg: e.toString());
      return;
    }
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Directionality(
      textDirection: TextDirection.rtl,
      child: Scaffold(
        appBar: AppBar(
          title: Text('سیستم معرفی'),
          elevation: 2,
          shadowColor: Colors.black,
        ),
        body: referralData != null
            ? ReferralColumn(referralData: referralData!)
            : ProgressIndicator(),
      ),
    );
  }
}

class ProgressIndicator extends StatelessWidget {
  const ProgressIndicator({super.key});

  @override
  Widget build(BuildContext context) {
    return Center(child: const CircularProgressIndicator());
  }
}

class ReferralColumn extends StatelessWidget {
  const ReferralColumn({super.key, required this.referralData});

  final ReferralData referralData;

  @override
  Widget build(BuildContext context) {
    return Align(
      alignment: Alignment.topRight,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text('کد معرفی : ${referralData.code}'),
          Text('تعداد افراد معرفی شده : ${referralData.numInvited}'),
          Text(
              'تعداد کد های تخفیف هدیه گرفته شده از سیستم معرفی : ${referralData.numDiscountGift}')
        ],
      ),
    );
  }
}
