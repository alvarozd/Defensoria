﻿#pragma checksum "..\..\..\View\02MenuInicio.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "19E6A38AD0C44DCB19640B54C038381A8819EA02"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using FacturasEnel.Teclado;
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
    /// MenuInicio
    /// </summary>
    public partial class MenuInicio : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\View\02MenuInicio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnConsultaCodigo;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\View\02MenuInicio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnConsultaTarjeta;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\View\02MenuInicio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnConsultaCliente;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\View\02MenuInicio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnAtras;
        
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
            System.Uri resourceLocater = new System.Uri("/FundacionMujer;component/view/02menuinicio.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\02MenuInicio.xaml"
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
            
            #line 11 "..\..\..\View\02MenuInicio.xaml"
            ((FacturasEnel.View.MenuInicio)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\View\02MenuInicio.xaml"
            ((FacturasEnel.View.MenuInicio)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.Page_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnConsultaCodigo = ((System.Windows.Controls.Image)(target));
            
            #line 15 "..\..\..\View\02MenuInicio.xaml"
            this.btnConsultaCodigo.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Botones_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnConsultaTarjeta = ((System.Windows.Controls.Image)(target));
            
            #line 16 "..\..\..\View\02MenuInicio.xaml"
            this.btnConsultaTarjeta.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Botones_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnConsultaCliente = ((System.Windows.Controls.Image)(target));
            
            #line 17 "..\..\..\View\02MenuInicio.xaml"
            this.btnConsultaCliente.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Botones_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnAtras = ((System.Windows.Controls.Image)(target));
            
            #line 18 "..\..\..\View\02MenuInicio.xaml"
            this.btnAtras.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Botones_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
