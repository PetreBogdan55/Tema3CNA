   M S B A M L   `   `   `  ����S  OGrpcWpfSample.Client.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:GrpcWpfSample.Client.Wpf.ViewGrpcWpfSample.Client.Wpf.View  5      c6clr-namespace:GrpcWpfSample.Client.Wpf.ApplicationData(GrpcWpfSample.Client.Wpf.ApplicationData  5      ,    &GrpcWpfSample.Client.Wpf.View.ChatView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:GrpcWpfSample.Client.Wpf.View     5      VBindListToTextBlock6clr-namespace:GrpcWpfSample.Client.Wpf.ApplicationData     5      .��5
      � 
�������  � 	Resources  5   
   D   >GrpcWpfSample.Client.Wpf.ApplicationData.ListToStringConverter   ListToStringConverter��%   &	          5      68   5       � ColumnDefinitions 5   
   �� 5      $	��10* �6   �� 5      $	��10* �6   �� 5      $��1* �6   �� 5      $	��10* �6   �� 5      $	��10* �6   �� 5      $	��10* �6   �� 5      $	��10* �6   �� 5      $	��10* �6    � RowDefinitions 5   
   �� 5      $
��5.5* �6   �� 5      $��1* �6   �� 5      $��7* �6   �� 5      $��7* �6   �� 5      $��7* �6   �� 5      $��7* �6   �� 5      $��7* �6   �� 5       $��7* �6   �� 5!      $��7* �6   �� 5"      $��7* �6   �� 5#      $��7* �6   �� 5$      $��7* �6   .+�5&   
   �� $��	User List��6   �� 5'   
   $��0��6�   $��6��6�   $��2��6�    �� TextAlignment$ Center=�6�   ��6   ��  �� Path$ LocalUsername� �� UpdateSourceTrigger$ PropertyChanged=���6]   �� $ FontSize�6�   �� 5)   
    J� IsHitTestVisible .  $��2��6�   $��2��6�   $��10��6�   
���������6   �� *
UserList+y�6M   �� $ MainChatVisibility���6|   �� $ FontSize�6�   � 5+   
   $��2��6?   $��3��6L   $��5��6\   $��9��6p   y�6   �� $ MainChatVisibility�.+�5,      �� 
������� .  ��6   �� *ChatHistory+��5-      �� 5.       �� 
TargetType" ��6   .�5/      ��  �� Property � ��	 �� Value$	 0q�64   �� 50       � ��$	 0q�63   �� 51       � ��$	 0q�6<   52      ��54      �� 55      .R�56      � 
�������.+�57      �� $
��Wrap=�6a   $
��Bold"�6u   ��6(   �� *User+��6>   �� $ FontSize�6u   �� 58      $��: ��6(   $
��Wrap=�6U   $
��Bold"�6i   ��62   �� $ FontSize�6i   �� 59      $
��Wrap=�6K   $��Stretch=�6_   ��6(   �� $ FontSize��� 5:      
������ $��0q�6`   ��6'   �� *	RunList+��5;   "   �� 5<   '   .R�5=   *   �� 
 �� Orientation$
 
Horizontal=�65   
�������6N   5>   (   5@      5A      �� 5B      $	�� : ��6(   $
��Wrap=�6V   $
��Bold"�6j   ��63   �� $ FontSize�6j   �� 5C      $
��Wrap=�6b   $��Stretch=�6v   $ Right=�6�   ��6)   �� *Date+��6?   �� $ FontSize�6�   5D      5E      ��5G      �� 5H      .R�5I      �� $��Bottom=�6%   
�������6@   5J      5L      5M      �� 5N   
   -   5        $��MessageText��5N   A   $��11��6�   $��3��6�   $��4��6�   
�������y�6   �� $ MainChatVisibility���6T   �� $ 
MessageTxt� �� Mode$ TwoWay=�$ PropertyChanged=���6�   �� $ FontSize�.��6   J� InputBindings 5O      �� 5P      
 �� Key$ Enter��6    �� Command 6)   �� *SendCommand+�� 5Q      $ Return��6    6*   �� *SendCommand+.��5R   %   5S      �� 5T   
   $
��Send��6A   $��11��6s   $��8��6�   
�������y�6   �� $ MainChatVisibility���6P   �� $ FontSize���6�   �� $ SendCommand�5U      5V      