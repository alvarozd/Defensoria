﻿#pragma checksum "..\..\..\View\11DetallePagoCB.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FD7EEAF5AC9DC401D1915D831AB63C8775894068"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using ENEL.View;
using ENEL.View.Componentes;
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


namespace ENEL.View {
    
    
    /// <summary>
    /// Page_DetallePagoCB
    /// </summary>
    public partial class Page_DetallePagoFactura : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\View\11DetallePagoCB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvas;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\View\11DetallePagoCB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnVolver;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\View\11DetallePagoCB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnPagar;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\View\11DetallePagoCB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LblReferencia;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\View\11DetallePagoCB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LblValorPagar;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\View\11DetallePagoCB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost Mensajes;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\View\11DetallePagoCB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_Error_Titulo;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\View\11DetallePagoCB.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_Error_Campos;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\View\11DetallePagoCB.xaml"
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
            System.Uri resourceLocater = new System.Uri("/FacturasENEL;component/view/11detallepagocb.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\11DetallePagoCB.xaml"
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
            this.canvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 2:
            this.btnVolver = ((System.Windows.Controls.Image)(target));
            
            #line 13 "..\..\..\View\11DetallePagoCB.xaml"
            this.btnVolver.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Botones_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnPagar = ((System.Windows.Controls.Image)(target));
            
            #line 14 "..\..\..\View\11DetallePagoCB.xaml"
            this.btnPagar.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Botones_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.LblReferencia = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.LblValorPagar = ((System.Windows.Controls.Label)(target));
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
            
            #line 37 "..\..\..\View\11DetallePagoCB.xaml"
            this.btn_AceptoErrores.Click += new System.Windows.RoutedEventHandler(this.Btn_AceptoErrores_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
