   M S B A M L   `   `   `  ����F  BComultrasan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:FacturasEnel.ViewFacturasEnel.View  5      M+clr-namespace:FacturasEnel.View.ComponentesFacturasEnel.View.Componentes  5      (    "FacturasEnel.View.RegistroRupHoja2   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:FacturasEnel.View     5      A	Controles+clr-namespace:FacturasEnel.View.Componentes     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          �� IsFlicksEnabled  .   N� Title$ RegistroRupHoja2��5
      .��5      �� $��0,0,0,0q�6   $
��1280��6   $
��1024��6+   .+�5   
   �� -   5         ��Name$ Altocontraste��5      
 � Tag$ altocontraste��6'   6;   �� 5   
   -   5        $
 Zoom��5      $
 Zoom��6   6)   �� 5   
   -   5        $ Silencio��5      $ silencio��6"   61   �� 5   
   -   5        $ Ayuda��5      $ Ayuda��6   6+   �� 5   
   -   5        $ AyudaDiscapa��5      $ AyudaDiscapa��6&   69   �� 5   
   -   5        $ 	Siguiente��5      $	 Sig��6#   6-   �� 5   
   -   5        $ Atras��5      $ Atras��6   6+   r� 5   
   -	   5        $��Video��5   N   $
��1280��6   $	��600��6'    r� LoadedBehavior$ Manual=�64   $y�	Collapsed=�6[    �� Left$ 0��6r   
 �� Top$ 0��6�   $	��Top=�6�   6�   )  #MaterialDesignThemes.Wpf.DialogHost  5   
   -
   5          Name$ Mensajes��5   $      IsOpen .  $��0,0,0,0q�6E   $y�Visible=�6V   	   DialogContent	 5       �� 5!      $
��Auto��6   $
��Auto��6+   $
��1000��68   $	��700��6H   .+�5"      �� -   5        
 ��Name$
 txt_Error_Titulo��5"       $��40(�6:   $��Center=�6H   $��0,15,0,0q�6e   $
��Bold"�6w      AzulBordeExterior#��[  5#       5%      �� 5&      -   5        $
 txt_Error_Campos��5&       $
��Wrap=�6:   $��30(�6N   $��20q�6\   
�������$��Center=�6z   5'      5)      5+      �� 5.   
   -   5        $��	ComboSexo��5.      $��150,360q�6R   $	��300��6c   ���#4F4AE3$��34(�6�   �� 5/   
   -   5        $��ComboDepartamento��5/      $��150,600q�6c   $	��300��6t   ���#4F4AE3$��34(�6�    �� DisplayMemberPath$ Nombre��6�   $
��Bold"�6�   �� 50   
   -   5        $��ComboCiudad��50      $��685,600q�6[   $	��300��6l   ���#4F4AE3$��34(�6�   $ Nombre��6�   $
��Bold"�6�   52      53      