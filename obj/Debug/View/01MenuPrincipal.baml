   M S B A M L   `   `   `  ����F  BComultrasan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:FacturasEnel.ViewFacturasEnel.View  5      ;"clr-namespace:FacturasEnel.TecladoFacturasEnel.Teclado  5	      M+clr-namespace:FacturasEnel.View.ComponentesFacturasEnel.View.Componentes  5
      %    FacturasEnel.View.MenuPrincipal   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:FacturasEnel.View     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .views"clr-namespace:FacturasEnel.Teclado  5	      ;	Controles+clr-namespace:FacturasEnel.View.Componentes  5
      -   5          �� IsFlicksEnabled  .   N� Title$ MenuPrincipal��5      .��5      �� $��0,0,0,0q�6   .+�5   
   0   *FacturasEnel.View.Componentes.MiniCargando  -   5          Name$ MiniCargando��5   !   $y�Hidden=�67   6M   �� 5   
   -   5        $��Registro��5      
 � Tag$ Registro��6   $��0,150q�6/   6>   �� 5   
   -   5        $��
Defensoria��5   0   $ 
Defensoria��6   $��0,150q�6!   6B   �� 5   
   -   5        $��Videollamada��5   2   $ Videollamada��6   $��0,150q�6#   6G   �� 5   
   -   5        $��AltoContraste��5   %   $ AltoContraste��6   6;   �� 5   
   -   5        $
��Zoom��5      $
 Zoom��6   6'   �� 5   
   -   5        $��Silencio��5   -   $ Silencio��6   $	��5,0q�6   6=   �� 5   
   -	   5        $��Ayuda��5      $ Ayuda��6   6+   r� 5   
   -
   5        $��Video��5   2    r� LoadedBehavior$ Manual=�6   $y�	Collapsed=�6?    �� Left$ 0��6V   
 �� Top$ 0��6f   $	��Top=�6u   6�   r� 5   
   -   5        $��Videoprincipal��5   F   $��150,250,0,0q�6   $ Manual=�6,   $y�	Collapsed=�6\   $ 0��6s   $ 0��6�   $	��Top=�6�   6�   )  #MaterialDesignThemes.Wpf.DialogHost  5   
   -   5          Name$ 	ModalErro��5   $      IsOpen .  $��0,0,0,0q�6F   $y�	Collapsed=�6W   	   DialogContent	 5      �� 5      $
��Auto��6   $
��Auto��6+   $	��800��68   .+�5      �� -   5        
 ��Name$
 txt_Error_Titulo��5       $��40(�6:   $��Center=�6H   $��0,15,0,0q�6e   $
��Bold"�6w      AzulBordeExterior#��[  5      5       �� 5!      -   5        $
 txt_Error_Campos��5!       $
��Wrap=�6:   $��30(�6N   $��20q�6\   
�������$��Center=�6z   5"      �� 5$      -   5        $��Btn_AceptoErrores��5%        btn_Template#��[ 5&       �� IsCancel .    CloseDialogCommand#��Z 5(      $��Sample2Cancel��5)      $��Center=�5*      $��20q�5+      $��Aceptar��5,      5.      50      52      53      54      