   M S B A M L   `   `   `  ����  MySnapps    MySnapps.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight  5   	     � Title$  
MainWindow��5   	   $��368.229��6   $��779.994��6-    � 
ResizeMode$ CanMinimize=�5   	    � Icon$ Images/Splash.ico��6"    � WindowStartupLocation$ CenterScreen=�6;    � DataContext 5   	   �� *MainViewModel+   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Closing��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      /  )GalaSoft.MvvmLight.Command.EventToCommand  	   PassEventArgsToCommand	 . 
   Command
 6%   ��  �� Path$  UrlViewModel.WindowClosing�6Y   5      ��5      �� 5   
    �� ImageSource$ Images/Splash.jpg��6   67   .��5      �� -   5         ��Name$ WindowCanvas��5      $��0,0,-0.4,4.2q�6#   .+�5   
   �� -   5         ��Name$ 
UrlTextBox��5       �� Left$ 12��5      
 �� Top$ 37��6#   $��26��63   $	��291��5   &   ��5      �� $ UrlViewModel.UrlContent� �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�.��6s   5      �� 5   
   -   5         ��Name$ UrlListView��5      $ 12��5      $	 106��6$   $	��229��65   $	��291��5   (    �� SelectionMode$ Single=�5        �� SelectedIndex 5!      �� *UrlViewModel.UrlSelectedIndex+$ PropertyChanged=�$ TwoWay=� �� SelectedItem 5"      �� *UrlViewModel.UrlSelectedItem+$ PropertyChanged=�$ TwoWay=���5#      �� *UrlViewModel.Urls+$ PropertyChanged=�$ TwoWay=� 5$        5%      $ SelectionChanged��6!   . 5&      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� *UrlViewModel.SelectionChanged+   CommandParameter 6_   ��  �� ElementName$ UrlListView��$ SelectedItem�5'      ��5)      �� 5*      .R�5+      �� .+�5,      �� ��6    �� *Link+5-      5.      50      �� 51   
   -   5         ��Name$ 	addButton��51      $	 138��6$   $ 70��66   $��25��6F   $��79��6R    �� 	IsDefault . $	��Add��52   Q   ��6   �� *UrlViewModel.AddUrlCommand+6Q   �� 53   
   -   5        $ removeButton��53      $	 224��6'   $ 70��69   $��25��6I   $��79��6U   $��Remove��54   C   ��6   �� *UrlViewModel.RemoveUrlCommand+6C   �� 55   
   -   5        $ okButton��55      $	 312��6#   $ 37��65   $��OK��6E   $��26��6R   $��85��6^   ��56      �� *UrlViewModel.OkButton+�� 57   
   -   5        $ closeButton��57      $	 312��6&   $ 74��68   $��Close��6H   $��26��6X   $��85��6d   ��58      �� *UrlViewModel.CloseButton+�� 59   
   -   5        $ 
saveButton��59      $	 312��6%   $	 110��67   $
��Save��6H   $��26��6W   $��85��6c   ��5:      �� *!UrlViewModel.SaveUrlListCommand+�� 5;   
   -	   5         ��Name$ 	UrlsLabel��5;      $
��Urls��6#   $ 12��62   $ 10��6C   $
��Bold"�6S   �� 5<   
   -
   5         ��Name$ Global��5<      
�������$	 447��66   $ 4��6H   $	��331��6W   $	��317��6d   �� 5=   
   $��39��6   $	 275��6    $��0q�62   $	��321��6=    J� RenderTransformOrigin$ 0.501,0.504&�6I   $	 144��6m   ���	#FFFFF9F9$��5q�6�   }�. $��5��6�    � UseLayoutRounding .  z�5>      V� 5?      .~�5@      �� �� 5A      �� 5B        �� Angle$  90Q�6&   T� 5C      ! T� X$! -1.368Q�6)   " T� Y$" -0.716Q�64   5D      5F      �� 5G   
   -   5        $ FormatGroupBox��5G      $��Format��6+   $	 455��6;   $ 15��6M   $	��288��6]   $	��103��6i   $
��Bold"�6v   
�������.��5H      � -   5        # �Name$# 
FormatGrid��5H      .+�5I      	� -   5        $ 	�Name$$ PdfRadioButton��5I      $	��PDF��66   % 	� 	GroupName$% FormatSelection��6D   $��72,16,122.2,17.6q�6`   $��Normal"�6z   & d� 	IsChecked& 6�   �� *ProcessViewModel.PDFChecked+	� 5J      -   5        $$ JpgRadioButton��5J      $
��JPEG��66   $% FormatSelection��6E   $��165,16,36.2,16.6q�6b   $��Normal"�6|   5K      5L      �� 5M   
   $	 493��6   $ 79��6%   $��39��65   $	��242��6A   
�������.��5N      � -   5        $# ViewPortGrid��5N      $��0,6,-0.2,-6.2q�6)   .+�5O      �� -   5        $ PDFOnlyLable��5O      $��PDF Only��6.   $
��Left=�6A   $	��Top=�6]   $��7(�6u   $��26��6�   $��48��6�   $��182,17,-0.2,-10q�6�   	� 5P      -   5        $$ mobileRadioButton��5P      $��
MobileView��69   $% ViewPortSelection��6N   $��141,0,-20,16q�6l   	� 5Q      -   5        $$ desktopRadioButton��5Q      $��DesktopView��6:   $% ViewPortSelection��6P   $��
10,0,113,0q�6o   & 6�   �� *%#ProcessViewModel.DesktopViewChecked+5R      5S      �� 5T   
   -   5        $ DestinationGroupBox��5T      $��Destination��60   $	 455��6E   $	 123��6W   $��82��6h   $	��288��6t   $
��Bold"�6�   
�������.��5U      � -   5        $# DestinationGrid��5U      .+�5V      �� -   5        $ BrowseButton��5V      $��Browse��6/   $	 467��6@   $	 149��6R   $��0,19,189.2,16.2q�5X      $��Normal"�62   ��5W      �� *ProcessViewModel.BrowseButton+5X   2   �� 5Y      -   5        $ 	tbxFolder��5Y      $��22��6\   $	 561��6h   ' �� TextWrapping$
' Wrap=�6z   $	 149��6�   $	��168��6�   $��107,21,0.2,16.2q�6�   $��Normal"�6�   ��6-   �� *ProcessViewModel.OutputFolder+6�   5Z      5[      �� 5\   
   -   5        $ ViewGroupBox��5\      $��	Schedule ��6)   $	 455��6<   $	 221��6N   $	��288��6_   $��69��6k   $
��Bold"�6w   
�������.��5]      � .+�5^      "  Xceed.Wpf.Toolkit.TimePicker  -   5        (  Name$( 
TimePicker��5^   "   $��99,17,0,10.4q�6e   $��Normal"�6{   $
��Left=�6�   $	��105��6�   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])   Value) 66   �� *ProcessViewModel.TimePicker+6�   �� 5_      -   5        $ RunEveryDayLabel��5_      $��Run Everyday at:��62   $��Normal"�6M   $��0,11,176.2,10.4q�6a   �� 5`      -   5        * ��Name$* RunEveryDayCheckBox��5`      $��Enabled��68   $��209,17,-0.8,8.4q�6J   $��Normal"�6c   & 6w   �� *&$ProcessViewModel.RunEveryDayCheckBox+5a      5b      � 5c   
   $��10��6   $	 351��6   $	 244��6-   .+�5d      � -   5        + �Name$+ pbStatus��5d   f   , � Minimum$, 0Q�6   - � Maximum$	- 100Q�6&   $��-94,-43,-112.8,0q�6x   $��35��6�   $	��Top=�6�   $
��Left=�6�   $	��230��6�   . � Value. 64   �� *!ProcessViewModel.StatusProgress+y�6�   �� *#!ProcessViewModel.StatusVisibility+�� 5e      -   5        / ��Name$/ pbStatusCount��5e      $��Center=�6s   $��Center=�6�   $��22,-35,-20.8,29q�6�   ��6/   �� *Value+$ pbStatus��0 �� StringFormat$0 {0:0}%��y�6�   �� $ pbStatus��$ 
Visibility�5f      �� 5g   
   -   5        $ StartButton��5g      $��Start��6&   $	 468��66   $	 304��6H   $��75��6Y   ���	#FF009131���	#FFF9F6F6$
��Bold"�6�   |�.  ��5h      �� *ProcessViewModel.StartButton+6B   �� 5i   
   -   5        $ 
StopButton��5i      $
��Stop��6%   $	 655��64   $	 304��6F   $��75��5j      $
��Bold"�6   ���	#FFFA0A0A���	#FFF7F0F0|�.  ��5k      �� *ProcessViewModel.StopButton+6A   �� 5l   
   -   5        $ IsDataChangeLabel��5l      $ 86��6+   $ 10��6<   $y�Hidden=�6L   ��5m      �� *UrlViewModel.DataChanged+$ PropertyChanged=�$ TwoWay=�5n      5o      