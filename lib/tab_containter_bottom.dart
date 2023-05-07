import 'package:flutter/material.dart';

class TabContainerBottom extends StatefulWidget {
  TabContainerBottom({Key key}) : super(key: key);

  @override
  _TabContainerBottomState createState() => _TabContainerBottomState();
}

class _TabContainerBottomState extends State<TabContainerBottom> {
  int tabIndex = 0;
  List<Widget> listScreens;

  @override
  void initState() {
    super.initState();
    listScreens = [
      Tab1(),
      Tab2(),
      Tab3(),
    ]
  }
}
