   M S B A M L   `   `   `  ����  cgi-omgevingswet<    6cgi_omgevingswet.Projectmanagement.Licenses.AddLicense   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
AddLicense��5   	   $	��700��6   $	��450��6)   .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��30 �6   �� 5	      $��* �6   �� 5
      $	��200 �6   �� 5      $��90 �6   .+�5   
   �� $��0��6   $��Vergunning toevoegen��6   $��30(�6<      CommonLabelSize#��[  6J   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $	��120 �6#   �� 5      $	��120 �6#   .+�5      �� #��[  6   $��Vergunning filter: ��6=   �� 5      -   5         ��Name$ txtFiltvergunning��5   A     CommonTextBoxSize#��[ 6   $��1��6\   6l   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgSelectLicense��5       $��2��6      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Vergunningsnaam��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6>   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� 	 �� Path$	 LicenseName�5      �� 5    
   $��3��6   $��Omschrijving:��6   #��[  65   �� 5!   
   -   5        $ txtDescription��5!      $��3��6   $��10,30,10,10q�6   
 �� TextWrapping$

 Wrap=�64   " �� VerticalScrollBarVisibility$ Visible=�6H    �� 	MaxLength$
 4000��6n   6   � 5#   
   $��4��6   $��Right=�6   $��	0,0,10,10q�68    5$      �� 5%      $��80 �6#   �� 5&      $��10 �6#   �� 5'      $��80 �6#   .+�5*      �� -   5         ��Name$ 	btnSelect��5*   9   $��3��6   $��	Selecteer��6%   6L   �� 5+      $��0��6   $��Sluiten��6%   5,      5-      5.      