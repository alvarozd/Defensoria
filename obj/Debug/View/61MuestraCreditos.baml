   M S B A M L   `   `   `  ����F  BComultrasan, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:FacturasEnel.ViewFacturasEnel.View  5      M+clr-namespace:FacturasEnel.View.ComponentesFacturasEnel.View.Componentes  5      '    !FacturasEnel.View.MuestraCreditos   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:FacturasEnel.View     5      A	Controles+clr-namespace:FacturasEnel.View.Componentes     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          �� IsFlicksEnabled  .   N� Title$ MuestraCreditos��5
      .��5      �� -   5         ��Name$ Mpagos��5      $��0,0,0,0q�6   $	��768��6.   $
��1024��6;   .+�5   
   �� -   5        $��btnCancelar��5      
 � Tag$ Cancelar��6#   62   �� 5   
   -   5        $
��btn1��5      $ Consulta��6   6+   �� 5   
   -   5        $
��btn2��5      $ 	Consulta1��6   6,   �� 5   
   -   5        $
��btn3��5      $ 	Consulta2��6   6,   �� 5   
   -   5        $��Consultando1��5      $y�Hidden=�6$   �� 5   
   -   5         ��Name$ Paneldescrip��5       �� Left$	 430��6+   
 �� Top$	 322��6=   $��3��6N   �� 5   
   -	   5        $��Lpaneld1NCre��5      �� 5   
   -
   5        $��Lpaneld1NuCli��5      �� 5   
   -   5        $��Lpaneld1ValorPagar��5      �� 5   
   -   5        $��Pagar��5      $ Pagar1��6   6*   �� 5   
   -   5        $��Ocultar��5      $ Ocultar2��6   6.   �� 5   
   -   5        $ Mipanel��5      $	 364��6&   $	 194��68   $��3��6I   .+�5   
   �� -   5        $��Ipanel��5      5      �� 5    
   -   5        $��Lpanel1��5       �� 5!   
   -   5        $��Lpanel2��5!      �� 5"   
   -   5        $��Lpanel3��5"      �� 5#   
   -   5        $��Lpanel4��5#      �� 5$   
   -   5        $��Lpanel5��5$      )  #MaterialDesignThemes.Wpf.DialogHost  5'   
   -   5          Name$ Mensajes��5'   $      IsOpen .  $��0,0,0,0q�6E   $y�Visible=�6V   	   DialogContent	 5(      �� 5)      $
��Auto��6   $
��Auto��6+   $
��1000��68   $	��700��6H   .+�5*      �� -   5        
 ��Name$
 txt_Error_Titulo��5*       $��40(�6:   $��Center=�6H   $��0,15,0,0q�6e   $
��Bold"�6w      AzulBordeExterior#��[  5+       5-      �� 5.      -   5        $
 txt_Error_Campos��5.       $
��Wrap=�6:   $��30(�6N   $��20q�6\   
�������$��Center=�6z   5/      �� 51      -   5        $��btn_AceptoErrores��52        btn_Template#��[ 53       �� IsCancel .    CloseDialogCommand#��Z 55      $��Sample2Cancel��56      $��Center=�57      $��20q�58      $��Aceptar��59      5<      5=      5?      5A      5B      