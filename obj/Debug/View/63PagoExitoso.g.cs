﻿#pragma checksum "..\..\..\View\63PagoExitoso.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B03D6F4EC125A7CA035715BEF5960803286940BB78CDB26DDAFC28AD91E8DA72"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using FacturasEnel.View;
using FacturasEnel.View.Componentes;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FacturasEnel.View {
    
    
    /// <summary>
    /// PagoExitoso
    /// </summary>
    public partial class PagoExitoso : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\View\63PagoExitoso.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnDocumento;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\View\63PagoExitoso.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnCredito;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\View\63PagoExitoso.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnCancelar;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\View\63PagoExitoso.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Consultando;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\View\63PagoExitoso.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost Mensajes;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\View\63PagoExitoso.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_Error_Titulo;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\View\63PagoExitoso.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_Error_Campos;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\View\63PagoExitoso.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_AceptoErrores;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Comultrasan;component/view/63pagoexitoso.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\63PagoExitoso.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\View\63PagoExitoso.xaml"
            ((FacturasEnel.View.PagoExitoso)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\View\63PagoExitoso.xaml"
            ((FacturasEnel.View.PagoExitoso)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.Page_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnDocumento = ((System.Windows.Controls.Image)(target));
            
            #line 14 "..\..\..\View\63PagoExitoso.xaml"
            this.btnDocumento.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Botones_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnCredito = ((System.Windows.Controls.Image)(target));
            
            #line 15 "..\..\..\View\63PagoExitoso.xaml"
            this.btnCredito.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Botones_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnCancelar = ((System.Windows.Controls.Image)(target));
            
            #line 16 "..\..\..\View\63PagoExitoso.xaml"
            this.btnCancelar.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Botones_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Consultando = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.Mensajes = ((MaterialDesignThemes.Wpf.DialogHost)(target));
            return;
            case 7:
            this.txt_Error_Titulo = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.txt_Error_Campos = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.btn_AceptoErrores = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\View\63PagoExitoso.xaml"
            this.btn_AceptoErrores.Click += new System.Windows.RoutedEventHandler(this.Btn_AceptoErrores_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
