import 'package:flutter/material.dart';
import 'package:pisaz/components/custom_text_field.dart';
import 'package:pisaz/components/rounded_button.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  @override
  State<LoginPage> createState() => _LoginPageState();
}

String? errorMessage;

class _LoginPageState extends State<LoginPage> {
  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size;
    TextEditingController controller = TextEditingController();
    return Directionality(
      textDirection: TextDirection.rtl,
      child: Scaffold(
        body: Center(
          child: SingleChildScrollView(
            child: Column(
              children: [
                const Text(
                  'ورود به پیساز',
                  style: TextStyle(fontSize: 30.0),
                ),
                SizedBox(height: 100.0),
                Align(
                  alignment: Alignment.centerRight,
                  child: Padding(
                    padding: EdgeInsets.only(right: size.width * 0.1),
                    child: Text(
                      'شماره همراه : ',
                      style: TextStyle(fontSize: 18),
                    ),
                  ),
                ),
                CustomTextField(
                  controller: controller,
                  width: size.width * 0.8,
                  hint: '09xxxxxxxxx',
                  number: true,
                  onChanged: (v) {},
                  errorMessage: errorMessage,
                ),
                RoundedButton(
                  width: size.width * 0.8,
                  height: size.height * 0.07,
                  color: Colors.deepPurple[300],
                  text: 'ورود',
                  textColor: Colors.white,
                  onPressed: () {
                    if (controller.text.length != 11) {
                      setState(() {
                        errorMessage = 'شماره همراه باید یازده رقمی باشد!';
                      });
                      return;
                    } else {
                      setState(() {
                        errorMessage = null;
                      });
                    }

                    if (controller.text.length > 1 &&
                        (controller.text[0] != '0' ||
                            controller.text[1] != '9')) {
                      setState(() {
                        errorMessage = 'شماره همراه باید با 09 شروع شود';
                      });
                      return;
                    } else {
                      setState(() {
                        errorMessage = null;
                      });
                    }
                  },
                )
              ],
            ),
          ),
        ),
      ),
    );
  }
}
