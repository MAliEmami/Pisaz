import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class CustomTextField extends StatelessWidget {
  const CustomTextField({
    super.key,
    required this.onChanged,
    this.controller,
    this.number,
    this.hint,
    this.errorMessage,
    this.color,
    this.width,
    this.padding,
  });

  final String? hint;
  final String? errorMessage;
  final bool? number;
  final Color? color;
  final Function(String)? onChanged;
  final TextEditingController? controller;
  final double? width;
  final EdgeInsets? padding;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: width ?? 300.0,
      margin: padding ?? const EdgeInsets.all(8.0),
      child: Center(
        child: TextField(
          onChanged: onChanged,
          style: TextStyle(color: Colors.black, fontSize: 17.0),
          maxLength: 30,
          keyboardType: TextInputType.visiblePassword,
          inputFormatters: [
            if (number != null && number!)
              FilteringTextInputFormatter(RegExp("[0-9]"), allow: true)
            else
              FilteringTextInputFormatter(
                  RegExp("[ آابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیئs]"),
                  allow: true)
          ],
          decoration: InputDecoration(
              border: OutlineInputBorder(),
              contentPadding:
                  EdgeInsets.symmetric(horizontal: 10.0, vertical: 20.0),
              hintText: hint,
              hintStyle: TextStyle(color: Colors.black26),
              counterText: '',
              errorText: errorMessage),
          controller: controller,
        ),
      ),
    );
  }
}
