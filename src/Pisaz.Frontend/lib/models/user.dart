class User {
  final String firstname;
  final String lastname;
  final int walletBalance;
  final DateTime signupDate;

  User(
      {required this.firstname,
      required this.lastname,
      required this.walletBalance,
      required this.signupDate});

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      firstname: json['firstName'] as String,
      lastname: json['lastName'] as String,
      walletBalance: json['walletBalance'] as int,
      signupDate: DateTime.parse(json['signupDate']),
    );
  }
}
