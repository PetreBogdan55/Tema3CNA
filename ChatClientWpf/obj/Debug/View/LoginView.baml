   M S B A M L   `   `   `  ����S  OGrpcWpfSample.Client.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:GrpcWpfSample.Client.Wpf.ViewGrpcWpfSample.Client.Wpf.View  5      -    'GrpcWpfSample.Client.Wpf.View.LoginView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:GrpcWpfSample.Client.Wpf.View     5      .��5	      � 
�������  � ColumnDefinitions  5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��36(�6   $
��Bold"�6"   $��CHAT APP��64   $��2��6D   $��3��6T    �� TextAlignment$ Center=�6h   $��0,25,0,0q�6   �� 5   
   $��36(�6   $
��Bold"�6"   $��	Login as:��64   $��2��6E   $��2��6R   $��3��6b   $ Center=�6v    �� Padding$ 0,16,0,0q�6�   �� 5   
   $��25(�6   $
��Bold"�6    $��3��62   $��2��6?   $��3��6O    �� TextAlignment$ Center=�6c   $��0,0,0,25q�6{    �� TextWrapping$
 Wrap=�6�    �� 	MaxLength$ 20��6�   $��Stretch=�6  $��Stretch=�6  $��Stretch=�6@  $��Center=�6^  ��6�   ��  �� Path$ 	UserLogin� �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�.��6�  
 J� InputBindings
 5      �� 5       
 �� Key$ Enter��6    �� Command 6)   �� *LoginCommand+�� 5!      $ Return��6    6*   �� *LoginCommand+.��5"   %   5#      �� 5$   
   $��15(�6   
���  ��$
��Bold"�63   $��4��6E   $��2��6R   $��3��6b   $ Center=�6v   $��0,0,0,25q�6�   $
��Wrap=�6�   ��6�   �� $ WarningLogin�6�   �� 5%   
   $��Login��6   $��36(�6!   $
��Bold"�6/   $��5��6A   $��2��6N   $��3��6^   $��0,0,0,20q�6�   ��6r   �� $ LoginCommand�6�   5&      5'      