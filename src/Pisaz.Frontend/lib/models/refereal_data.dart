class ReferralData {
  final String code;
  final int numInvited;
  final int numDiscountGift;

  ReferralData(
      {required this.code,
      required this.numInvited,
      required this.numDiscountGift});

  factory ReferralData.fromJson(Map<String, dynamic> json) {
    return ReferralData(
        code: json['referalCode'] as String,
        numInvited: json['numInvited'] as int,
        numDiscountGift: json['numDiscountGift'] as int);
  }
}
