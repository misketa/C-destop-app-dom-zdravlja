﻿#pragma checksum "..\..\..\Forme\FrmDomZdravlja.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8B755FF901C6F1F0E413AB8E4485A4989D10F9151336BB1AE7E3CE6B6277D6C9"
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
    /// FrmDomZdravlja
    /// </summary>
    public partial class FrmDomZdravlja : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Forme\FrmDomZdravlja.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDodaj;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Forme\FrmDomZdravlja.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnZatvori;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Forme\FrmDomZdravlja.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNazivInstitucije;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Forme\FrmDomZdravlja.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDodajAdresu;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Forme\FrmDomZdravlja.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbAdresa;
        
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
            System.Uri resourceLocater = new System.Uri("/PregledZakazivanje;component/forme/frmdomzdravlja.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Forme\FrmDomZdravlja.xaml"
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
            this.btnDodaj = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\Forme\FrmDomZdravlja.xaml"
            this.btnDodaj.Click += new System.Windows.RoutedEventHandler(this.btnDodaj_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnZatvori = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\Forme\FrmDomZdravlja.xaml"
            this.btnZatvori.Click += new System.Windows.RoutedEventHandler(this.btnZatvori_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbNazivInstitucije = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btnDodajAdresu = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Forme\FrmDomZdravlja.xaml"
            this.btnDodajAdresu.Click += new System.Windows.RoutedEventHandler(this.btnDodajAdresu_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lbAdresa = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

