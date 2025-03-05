import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:intl/intl.dart';
import 'package:pisaz/discount_codes_page.dart';
import 'package:pisaz/models/user.dart';
import 'package:pisaz/referral_page.dart';
import 'package:pisaz/services/backend.dart';

class ProfilePage extends StatefulWidget {
  const ProfilePage({
    super.key,
  });

  @override
  State<ProfilePage> createState() => _ProfilePageState();
}

class _ProfilePageState extends State<ProfilePage> {
  @override
  void initState() {
    super.initState();
    getUserData();
  }

  User? user;
  void getUserData() async {
    try {
      user = await Backend.getUser();
    } on Exception catch (e) {
      Fluttertoast.showToast(msg: e.toString());
      print(e.toString());
      return;
    }
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    if (user != null) {
      return Profile(user: user!);
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

class Profile extends StatelessWidget {
  Profile({super.key, required this.user});

  User user;

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Column(
        children: [
          Container(
            padding: EdgeInsets.all(15.0),
            child: Row(
              children: [
                Expanded(
                  child: Text(
                    '${user.firstname} ${user.lastname}',
                    style: Theme.of(context).textTheme.bodySmall!.copyWith(
                          fontSize: 17.0,
                        ),
                  ),
                ),
                Expanded(
                  child: Container(
                    decoration: BoxDecoration(
                        color: Colors.grey[300],
                        borderRadius: BorderRadius.circular(5.0)),
                    height: 30.0,
                    margin: EdgeInsets.symmetric(horizontal: 5),
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Text('زمان ثبت نام  : '),
                        Text(DateFormat('yyyy-MM-dd').format(user.signupDate))
                      ],
                    ),
                  ),
                )
              ],
            ),
          ),
          Container(
            margin: EdgeInsets.all(15.0),
            height: 100.0,
            decoration: BoxDecoration(
              color: Colors.amber,
              borderRadius: BorderRadius.all(Radius.circular(10.0)),
            ),
          ),
          Container(
            padding: EdgeInsets.all(15.0),
            child: Row(
              children: [
                Expanded(
                  child: ListTile(
                    onTap: () {},
                    leading: Icon(
                      Icons.shopping_cart_rounded,
                      color: Colors.green,
                      size: 30.0,
                    ),
                    title: Text(
                      'سبد خرید',
                      style: Theme.of(context).textTheme.bodySmall!.copyWith(
                            fontSize: 17.0,
                          ),
                    ),
                  ),
                ),
                Container(width: 2.0, height: 30.0, color: Colors.grey[350]),
                Expanded(
                  child: ListTile(
                    onTap: () {},
                    leading: Icon(
                      Icons.wallet,
                      color: Colors.purple,
                      size: 30.0,
                    ),
                    title: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          'کیف پول',
                          style:
                              Theme.of(context).textTheme.bodySmall!.copyWith(
                                    fontSize: 17.0,
                                  ),
                        ),
                        Text(
                          'موجودی : ${user.walletBalance}',
                          style:
                              Theme.of(context).textTheme.bodySmall!.copyWith(
                                    fontSize: 12.0,
                                  ),
                        ),
                      ],
                    ),
                  ),
                ),
              ],
            ),
          ),
          Divider(thickness: 2.0),
          ListTile(
            title: Text('کد های تخفیف'),
            leading: Icon(Icons.discount_outlined),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(
                    builder: (context) => const DiscountCodesPage()),
              );
            },
            trailing: Icon(Icons.arrow_forward_ios_rounded),
          ),
          Divider(height: 1, indent: 40),
          ListTile(
            title: Text('آدرس ها'),
            leading: Icon(Icons.location_on_outlined),
            onTap: () {},
            trailing: Icon(Icons.arrow_forward_ios_rounded),
          ),
          Divider(height: 1, indent: 40),
          ListTile(
            title: Text('سیستم معرفی'),
            leading: Icon(Icons.person_add_alt_1_outlined),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => const ReferralPage()),
              );
            },
            trailing: Icon(Icons.arrow_forward_ios_rounded),
          ),
          Divider(height: 1, indent: 40),
          ListTile(
            title: Text('سفارش های اخیر'),
            leading: Icon(Icons.shopping_bag_outlined),
            onTap: () {},
            trailing: Icon(Icons.arrow_forward_ios_rounded),
          ),
          Divider(height: 1),
        ],
      ),
    );
  }
}
