class Address {
  final String province;
  final String remainder;

  Address({required this.province, required this.remainder});

  factory Address.fromJson(Map<String, dynamic> json) {
    return Address(
        province: json['province'] as String,
        remainder: json['remainder'] as String);
  }
}
