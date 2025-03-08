import 'dart:convert';
import 'package:http/http.dart' as http;

class NetworkHelper {
  final String host = 'http://10.0.2.2:5184';
  String? jwtToken;

  Future<bool> login(String phoneNumber) async {
    var url = Uri.parse('$host/Auth/v1/login');
    var response = await http.post(url,
        headers: <String, String>{
          'Content-Type': 'application/json',
        },
        body: '"$phoneNumber"');

    if (response.statusCode == 200) {
      jwtToken = (jsonDecode(response.body)['token']);
      return true;
    } else {
      if (response.statusCode == 500) {
        return false;
      }
      throw Exception(response.statusCode);
    }
  }

  Future<String> getUser() async {
    var url = Uri.parse('$host/Client/v1/list');
    var response = await http.post(url, headers: <String, String>{
      'Content-Type': 'application/json',
      'Authorization': jwtToken!,
    });

    if (response.statusCode == 200) {
      return response.body;
    } else {
      throw Exception(response.statusCode);
    }
  }

  Future<String> getDiscountCodes() async {
    var url = Uri.parse('$host/Discount/v1/list');
    var response = await http.post(url, headers: <String, String>{
      'Content-Type': 'application/json',
      'Authorization': jwtToken!,
    });

    if (response.statusCode == 200) {
      return response.body;
    } else {
      throw Exception(response.statusCode);
    }
  }

  Future<String> getReferralData() async {
    var url = Uri.parse('$host/RefersSystem/v1/list');
    var response = await http.post(url, headers: <String, String>{
      'Content-Type': 'application/json',
      'Authorization': jwtToken!,
    });

    if (response.statusCode == 200) {
      return response.body;
    } else {
      throw Exception(response.statusCode);
    }
  }
}
