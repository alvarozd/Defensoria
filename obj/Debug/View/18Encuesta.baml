   M S B A M L   `   `   `  ����F  BComultrasan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:FacturasEnel.ViewFacturasEnel.View  5      ;"clr-namespace:FacturasEnel.TecladoFacturasEnel.Teclado  5	      M+clr-namespace:FacturasEnel.View.ComponentesFacturasEnel.View.Componentes  5
      $    FacturasEnel.View.MenuEncuesta   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:FacturasEnel.View     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .views"clr-namespace:FacturasEnel.Teclado  5	      ;	Controles+clr-namespace:FacturasEnel.View.Componentes  5
      -   5          �� IsFlicksEnabled  .   N� Title$ Menú Encuesta��5      .��5      �� $��0,0,0,0q�6   .+�5   
   0   *FacturasEnel.View.Componentes.MiniCargando  -   5          Name$ MiniCargando��5   !   $y�Hidden=�67   6M   �� 5   
   -   5        $��btnMala��5      
 � Tag$ 1��6   6'   �� 5   
   -   5        $��
btnRegular��5      $ 2��6   6*   �� 5   
   -   5        $��btnBuena��5      $ 3��6   6(   �� 5   
   -   5        $��btnFinalizar��5      $ 4��6   6,   �� 5   
   -   5        $��ImagenCarita��5      $y�Hidden=�6$   �� 5   
   -   5        $��Finaliza��5      $y�Hidden=�6    )  #MaterialDesignThemes.Wpf.DialogHost  5   
   -	   5          Name$ 	ModalErro��5   $      IsOpen .  $��0,0,0,0q�6F   $y�	Collapsed=�6W      DialogContent 5      �� 5      $
��Auto��6   $
��Auto��6+   $	��800��68   .+�5      �� -
   5         ��Name$ txt_Error_Titulo��5       $��40(�6:   $��Center=�6H   $��0,15,0,0q�6e   $
��Bold"�6w      AzulBordeExterior#��[  5      5      �� 5      -   5        $ txt_Error_Campos��5       $
��Wrap=�6:   $��30(�6N   $��20q�6\   
�������$��Center=�6z   5      �� 5!      -   5        $��Btn_AceptoErrores��5"        btn_Template#��[ 5#       �� IsCancel . 	   CloseDialogCommand#��Z	 5%      $��Sample2Cancel��5&      $��Center=�5'      $��20q�5(      $��Aceptar��5)      5+      5,      5.      5/      50      