class Cart {
  final int number;
  final String status;

  Cart({required this.number, required this.status});

  factory Cart.fromJson(Map<String, dynamic> json) {
    return Cart(
        number: json['cartNumber'] as int,
        status: json['cartStatus'] as String);
  }
}
