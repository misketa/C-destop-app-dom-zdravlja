﻿#pragma checksum "..\..\..\Forme\FrmAdresa.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DBE477D0D300965A34AC6595A2058132E4FDE027DFF710DD9A584FE589E03B61"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PregledZakazivanje.Forme;
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


namespace PregledZakazivanje.Forme {
    
    
    /// <summary>
    /// FrmAdresa
    /// </summary>
    public partial class FrmAdresa : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Forme\FrmAdresa.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbUlica;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Forme\FrmAdresa.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbGrad;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Forme\FrmAdresa.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbDrzava;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Forme\FrmAdresa.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbBroj;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Forme\FrmAdresa.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDodaj;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Forme\FrmAdresa.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnZatvori;
        
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
            System.Uri resourceLocater = new System.Uri("/PregledZakazivanje;component/forme/frmadresa.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Forme\FrmAdresa.xaml"
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
            this.tbUlica = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.tbGrad = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tbDrzava = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbBroj = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btnDodaj = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\Forme\FrmAdresa.xaml"
            this.btnDodaj.Click += new System.Windows.RoutedEventHandler(this.btnDodaj_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnZatvori = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Forme\FrmAdresa.xaml"
            this.btnZatvori.Click += new System.Windows.RoutedEventHandler(this.btnZatvori_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
