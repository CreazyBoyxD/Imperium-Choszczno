﻿#pragma checksum "..\..\..\Logowanie.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7303FA8D953B6D85D27539E26495B249F232541E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using WpfApp1;
using WpfApp1.Properties;


namespace WpfApp1 {
    
    
    /// <summary>
    /// Logowanie
    /// </summary>
    public partial class Logowanie : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\Logowanie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame Main;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Logowanie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button1_login;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Logowanie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBox1;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Logowanie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PassBoxLogin;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Logowanie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock LoginLabel;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Logowanie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Passlabel;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Logowanie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button2_signup;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Logowanie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ErrorLabelLogin;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Logowanie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image LogoImage;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Logowanie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label NameOfStudio;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Logowanie.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ThemeButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/logowanie.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Logowanie.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Main = ((System.Windows.Controls.Frame)(target));
            return;
            case 2:
            this.button1_login = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Logowanie.xaml"
            this.button1_login.Click += new System.Windows.RoutedEventHandler(this.Button_Click_signin);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtBox1 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.PassBoxLogin = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.LoginLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.Passlabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.button2_signup = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\Logowanie.xaml"
            this.button2_signup.Click += new System.Windows.RoutedEventHandler(this.Button_Click_signup);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ErrorLabelLogin = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.LogoImage = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            this.NameOfStudio = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.ThemeButton = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\Logowanie.xaml"
            this.ThemeButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

