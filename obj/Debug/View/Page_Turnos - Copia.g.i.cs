﻿#pragma checksum "..\..\..\View\Page_Ayuda - Copia.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5D189105D4C764BC477DD4E293A259574482621E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Carulla.View;
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


namespace Carulla.View {
    
    
    /// <summary>
    /// Page_Ayuda
    /// </summary>
    public partial class Page_Ayuda : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\View\Page_Ayuda - Copia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Volver;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\View\Page_Ayuda - Copia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btn_Volver_IMA;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\View\Page_Ayuda - Copia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Turnos;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\View\Page_Ayuda - Copia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Otros_Servicios;
        
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
            System.Uri resourceLocater = new System.Uri("/Carulla;component/view/page_turnos%20-%20copia.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\Page_Ayuda - Copia.xaml"
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
            this.btn_Volver = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\View\Page_Ayuda - Copia.xaml"
            this.btn_Volver.Click += new System.Windows.RoutedEventHandler(this.btn_Volver_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_Volver_IMA = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.btn_Turnos = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\View\Page_Ayuda - Copia.xaml"
            this.btn_Turnos.Click += new System.Windows.RoutedEventHandler(this.btn_Turnos_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_Otros_Servicios = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\View\Page_Ayuda - Copia.xaml"
            this.btn_Otros_Servicios.Click += new System.Windows.RoutedEventHandler(this.btn_Otros_Servicios_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
