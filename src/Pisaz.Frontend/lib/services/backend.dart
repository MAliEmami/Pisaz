import 'dart:convert';
import 'package:pisaz/models/address.dart';
import 'package:pisaz/models/cart_status.dart';
import 'package:pisaz/models/refereal_data.dart';
import 'package:pisaz/models/user.dart';
import 'package:pisaz/models/discount_code.dart';
import 'package:pisaz/services/network.dart';

class Backend {
  static NetworkHelper network = NetworkHelper();

  static Future<bool> login(String phoneNumber) async {
    return await network.login(phoneNumber);
  }

  static Future<User> getUser() async {
    //     '{"firstName": "علی", "lastName": "احمدی", "walletBalance": 0, "signupDate": "2025-03-04T22:32:52.46"}';
    String responseJson = await network.getUser();

    final userMap = jsonDecode(responseJson)[0] as Map<String, dynamic>;
    final user = User.fromJson(userMap);

    return user;
  }

  static Future<dynamic> getDicountCodes() async {
    String responseJson;
    try {
      responseJson = await network.getDiscountCodes();
    } on Exception catch (e) {
      return Future<List<DiscountCode>>.value([]);
    }

    List<DiscountCode> discounts = (jsonDecode(responseJson) as List<dynamic>)
        .map((jsonString) => DiscountCode.fromJson(jsonString))
        .toList();

    return discounts;
  }

  static Future<dynamic> getAddresses() async {
    String responseJson;
    try {
      responseJson = await network.getAddresses();
    } on Exception catch (e) {
      return Future<List<Address>>.value([]);
    }

    List<Address> addresses = (jsonDecode(responseJson) as List<dynamic>)
        .map((jsonString) => Address.fromJson(jsonString))
        .toList();

    return addresses;
  }

  static Future<dynamic> getCarts() async {
    String responseJson;
    try {
      responseJson = await network.getCarts();
    } on Exception catch (e) {
      return;
    }

    List<Cart> carts = (jsonDecode(responseJson) as List<dynamic>)
        .map((jsonString) => Cart.fromJson(jsonString))
        .toList();

    return carts;
  }

  static Future<ReferralData> getReferralData() async {
    String responseJson = await network.getReferralData();

    final referralMap = jsonDecode(responseJson)[0] as Map<String, dynamic>;
    final referral = ReferralData.fromJson(referralMap);

    return referral;
  }
}
