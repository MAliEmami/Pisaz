import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:pisaz/models/refereal_data.dart';
import 'package:pisaz/models/user.dart';
import 'package:pisaz/models/discount_code.dart';

class Backend {
  late String apiToken;
  final url = 'localhost:C/...';

  static Future<dynamic> getDicountCodes() async {
    const jsonString = '''
    [{"code": 1234, "amount": 15000, "limit": 0, "usage_count": 2, "expiration": "2025-03-04T22:32:52.46"},
    {"code": 1275, "amount": 50, "limit": 2000000, "usage_count": 1, "expiration": "2025-03-04T22:32:52.46"},
    {"code": 5482, "amount": 10, "limit": 1000000, "usage_count": 4, "expiration": "2025-03-04T22:32:52.46"}]''';

    List<DiscountCode> discounts = (jsonDecode(jsonString) as List)
        .map((jsonString) => DiscountCode.fromJson(jsonString))
        .toList();

    return discounts;
  }

  static Future<User> getUser() async {
    http.Request request =
        http.Request("post", Uri.parse('http://localhost:5184/Client/v1/list'));
    request.headers.clear();
    request.headers.addAll({"phone_number": "09123456789"});
    var response = await request.send();

    //const jsonString =
    // '{"firstName": "علی", "lastName": "احمدی", "walletBalance": 0, "signupDate": "2025-03-04T22:32:52.46"}';
    print('json : $response');

    if (response.statusCode != 200) {
      print('faileedd!!!!');
    }
    final userMap = jsonDecode(response.toString()) as Map<String, dynamic>;
    final user = User.fromJson(userMap);

    return user;
  }

  static Future<ReferralData> getReferralData() async {
    const jsonString =
        '{"referralCode": "1204793751", "numInvited": 4, "numDiscountGift": 1}';

    final referralMap = jsonDecode(jsonString) as Map<String, dynamic>;
    final referral = ReferralData.fromJson(referralMap);

    return referral;
  }
}
