import 'package:flutter/material.dart';
import 'package:pisaz/profile_page.dart';

class MainPage extends StatefulWidget {
  const MainPage({super.key});

  @override
  State<MainPage> createState() => _MainPageState();
}

class _MainPageState extends State<MainPage> {
  int currentPage = 0;
  @override
  Widget build(BuildContext context) {
    return Directionality(
      textDirection: TextDirection.rtl,
      child: Scaffold(
        bottomNavigationBar: NavigationBar(
          onDestinationSelected: (int index) {
            setState(() {
              currentPage = index;
            });
          },
          selectedIndex: currentPage,
          destinations: const <Widget>[
            NavigationDestination(
              selectedIcon: Icon(Icons.home_filled),
              icon: Icon(Icons.home_outlined),
              label: 'خانه',
            ),
            NavigationDestination(
              selectedIcon: Icon(Icons.account_tree),
              icon: Icon(Icons.account_tree_outlined),
              label: 'سازگاریاب',
            ),
            NavigationDestination(
              selectedIcon: Icon(Icons.person),
              icon: Icon(Icons.person_outline),
              label: 'حساب کاربری',
            ),
          ],
        ),
        appBar: AppBar(
          centerTitle: true,
          title: Text('پیساز'),
          titleTextStyle: Theme.of(context).textTheme.bodySmall!.copyWith(
                fontWeight: FontWeight.bold,
                fontSize: 20.0,
              ),
          elevation: 2,
          shadowColor: Colors.black,
        ),
        body: <Widget>[
          //home
          Center(
            child: Text(
              "اوپس! این قسمت هنوز ساخته نشده.",
              textDirection: TextDirection.rtl,
            ),
          ),

          //sazegar yab
          Padding(padding: EdgeInsets.all(8.0)),

          //profile
          ProfilePage()
        ][currentPage],
      ),
    );
  }
}
