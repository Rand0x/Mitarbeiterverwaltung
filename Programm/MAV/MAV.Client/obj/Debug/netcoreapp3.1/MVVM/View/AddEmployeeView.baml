   M S B A M L   `   `   `  ����E  AMAV.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MAV.Client.MVVM.ViewMAV.Client.MVVM.View  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      =#clr-namespace:MAV.Client.MVVM.ModelMAV.Client.MVVM.Model  5      *    $MAV.Client.MVVM.View.AddEmployeeView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:MAV.Client.MVVM.View     5      9System&clr-namespace:System;assembly=mscorlib     5      5model#clr-namespace:MAV.Client.MVVM.Model     5      D @ModernWpf, Version=0.9.4.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.4.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5	      .��5      �� .+�5   
   �� $#��Neuen Mitarbeiter hinzufügen��6   
���   �$��30(�6K   $��0,0,0,20q�6Y   �� 5   
   $��Vorname��6   ���#5B5B5B$��16(�68   �� 5   
   -   5          ��Name$  	FirstName��5      $	��300��6%   $
��Left=�61   $��	0,0,10,10q�6L   $��16(�6`   ��6n   �� *	FirstName+�� 5   
   $��Nachname��6   ���#5B5B5B$��16(�69   �� 5   
   -   5        $  LastName��5      $	��300��6$   $
��Left=�60   $��	0,0,10,10q�6K   $��16(�6_   ��6m   �� *
LastName+�� 5   
   $��
Geschlecht��6   ���#5B5B5B$��16(�6;   �� 5   
   -   5         ��Name$ cbxSex��5      $	��300��6#   $��	0,0,10,10q�6/   $��16(�6C   �� 5   
   $��
Geburtstag��6   ���#5B5B5B$��16(�6;   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
dpBirthday��5      $	��300��6*   $��	0,0,10,10q�66   1  +ModernWpf.Controls.Primitives.ControlHelper   PlaceholderText$ Datum auswählen��6J   �� 5   
   $��Mitarbeiternummer��6   ���#5B5B5B$��16(�6B   �� 5   
   -   5         ��Name$ CheckboxPersNr��5      $��Automatisch generiert��6+    d� 	IsChecked$
 TrueY�6K   $��	0,0,10,10q�6y   �� 5   
   -   5        $  TextBoxPersNr��5      $	��300��6)   $
��Left=�65   $��	0,0,10,10q�6P   $��16(�6e   $y�	Collapsed=�6s   �� 5   
   $��	Abteilung��6   ���#5B5B5B$��16(�6:   �� 5   
   -   5        $ cmbDepartments��5      $��	0,0,10,10q�6X   $��16(�6l   $	��300��6z    �� SelectedItem 6+   �� *SelectedDepartement+��6�   �� *Departements+��5      �� 5      .R�5      �� $��16(�66   $��Center=�6D   ��6    �� *Name+6D   5      5       �� 5!   
   $��Eintrittsdatum��6   ���#5B5B5B$��16(�6?     5"   
   -   5        $ 
dpHireDate��5"      $	��300��6*   $��	0,0,10,10q�66      IsTodayHighlighted$
 True��6J   $ Datum auswählen��6d   �� 5#   
   $��15q�6   $	��250��6      AccentButtonStyle#��[  6P   ��6)   �� *AddEmployeeCommand+��5$      �� 5%       �� Orientation$ 
Horizontal=�6   .+�5&      �� $	����6   
�������$��18(�6B   $��Segoe MDL2 Assets)�6P   $��Center=�5'   $   $��0,0,15,0q�6?   �� 5(      $��Mitarbeiter hinzufügen��6   
�������$��18(�6P   $��Center=�6^   5)      5+      5,      5-      