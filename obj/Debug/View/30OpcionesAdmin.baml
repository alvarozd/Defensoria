   M S B A M L   `   `   `  ����F  BComultrasan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:FacturasEnel.ViewFacturasEnel.View  5      M+clr-namespace:FacturasEnel.View.ComponentesFacturasEnel.View.Componentes  5      ;"clr-namespace:FacturasEnel.TecladoFacturasEnel.Teclado  5      B >Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null2&clr-namespace:Library;assembly=LibraryLibrary 5
      )    #FacturasEnel.View.PageOpcionesAdmin   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:FacturasEnel.View     5      A	Controles+clr-namespace:FacturasEnel.View.Componentes     5      6Teclado"clr-namespace:FacturasEnel.Teclado     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	      2contr&clr-namespace:Library;assembly=Library  5
      -   5          �� IsFlicksEnabled  .   N� Title$ Opciones administración��5      .��5      �� -   5         ��Name$ Canvas��5      $��0,0,0,0q�6   $	��768��6.   $
��1024��6;   .+�5   
   �� -   5        $��	btnVolver��5      
 � Tag$ Volver��6!   6.   �� 5   
   -   5        $
��btn1��5      $ 	MenuAdmin��6   6,   �� 5   
   -   5        $
��btn2��5      $ Reimpresion��6   6.   �� 5   
   -   5        $
��btn3��5      $ Imprimir��6   6+   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ FechaConsulta��5      $y�Hidden=�6,   $��30(�6@   5      �� 5   
   -   5         ��Name$ RecibosView��5      
���   �$��1q�6:   $y�Hidden=�6N   ._�5   
   �� -	   5         ��Name$ 
LstRecibos��5      $��Stretch=�6'   $��Stretch=�6E   $��0q�6a   $��1q�6m   $y�Visible=�6x   .  (MaterialDesignThemes.Wpf.ScrollBarAssist   ButtonsVisibility$ Visible=�6�   -  'MaterialDesignThemes.Wpf.ListViewAssist   ListViewItemPadding$ 1q�6�   $��Disabled=�6�   	 �� View	 5      �� 5      
 �� AllowsColumnReorder
 .  .P�5      ��  �� Width$ 80��6%    �� HeaderTemplate 5      �� 5      .R�5    "   �� $��Recibo��6,   $��80��6:   $��-5q�6E    �� Padding$ 0q�6Q   $��Center=�6]    �� TextAlignment$ Center=�6z   $��Center=�6�   $
��Bold"�6�   $��14(�6�   5!       �� CellTemplate 5#      �� 5$      .R�5%   &   �� $��80��6G   $��25��6R   $��0,8,0,0q�6^   $ 0q�6o   $ Center=�6{   $��Center=�6�   $��12(�6�   ��60   �� *idVPS+6�   5&      5(      �� 5)      $ 80��6%    5*      �� 5+      .R�5,   "   �� $��ID Pago��6,   $��80��6;   $��-5q�6F   $ 0q�6R   $��Center=�6^   $ Center=�6{   $��Center=�6�   $
��Bold"�6�   $��14(�6�   5-       5/      �� 50      .R�51   &   �� $��65��6F   $��25��6Q   $��0,8,0,0q�6]   $ 0q�6n   $��Center=�6z   $ Center=�6�   $��Center=�6�   $��12(�6�   ��60   �� *idVC+6�   52      54      �� 55      $ 90��6%    56      �� 57      .R�58   "   �� $
��Hora��6,   $��90��68   $��-5q�6C   $ 0q�6O   $��Center=�6[   $ Center=�6x   $��Center=�6�   $
��Bold"�6�   $��14(�6�   59       5;      �� 5<      .R�5=   &   �� $��75��6F   $��25��6Q   $��0,8,0,0q�6]   $ 0q�6n   $ Center=�6z   $��Center=�6�   $��Center=�6�   $��12(�6�   ��60   �� *Hora+6�   5>      5@      �� 5B      $	 100��6%    5C      �� 5D      .R�5E   "   �� $��Acción��6,   $	��100��6:   $��-5q�6F   $ 0q�6R   $��Center=�6^   $ Center=�6{   $��Center=�6�   $
��Bold"�6�   $��14(�6�   5F       5H      �� 5I      .R�5J   "   �� -
   5        $��0,2,0,2q�5J   _   $��Center=�6�   $��Center=�6�   ��6(   �� *Boton+ 6A   �� *NombreArchivo+6�   5K      5M      5N      5P      5Q      �� 5S   
   -   5        $��btnAyuda��5S      $ Ayuda��6    6,   )  #MaterialDesignThemes.Wpf.DialogHost  5U   
   -   5          Name$ Mensajes��5U   $      IsOpen .  $��0,0,0,0q�6E   $y�Visible=�6V      DialogContent 5V      �� 5W      $
��Auto��6   $
��Auto��6+   $
��1000��68   $	��700��6H   .+�5X      �� -   5         ��Name$ txt_Error_Titulo��5X       $��40(�6:   $��Center=�6H   $��0,15,0,0q�6e   $
��Bold"�6w      AzulBordeExterior#��[  5Y       5[      �� 5\      -   5        $ txt_Error_Campos��5\       $
��Wrap=�6:   $��30(�6N   $��20q�6\   
�������$��Center=�6z   5]      �� 5_      -   5        $��btn_AceptoErrores��5`        btn_Template#��[ 5a       �� IsCancel .    CloseDialogCommand#��Z 5c      $��Sample2Cancel��5d      $��Center=�5e      $��20q�5f      $��Aceptar��5g      5j      5k      5m      5p      5q      