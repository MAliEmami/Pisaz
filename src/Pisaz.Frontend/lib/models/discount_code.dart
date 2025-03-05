class DiscountCode {
  final int code;
  final int amount;
  final int limit;
  final int usageCount;
  final DateTime expirationTime;

  DiscountCode(
      {required this.code,
      required this.amount,
      required this.limit,
      required this.usageCount,
      required this.expirationTime});

  factory DiscountCode.fromJson(Map<String, dynamic> json) {
    return DiscountCode(
        code: json['code'] as int,
        amount: json['amount'] as int,
        limit: json['limit'] as int,
        usageCount: json['usage_count'] as int,
        expirationTime: DateTime.parse(json['expiration']));
  }
}
