﻿#pragma checksum "C:\Git\Repo\geoauth-client-wp7\Src\GeoAuthAppCS\Pages\RegisterPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "899D97A21A37AAE9388BBECFC1C8DE23"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace GeoAuthApp {
    
    
    public partial class RegisterPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock lblErrors;
        
        internal System.Windows.Controls.TextBlock lblDevNmae;
        
        internal System.Windows.Controls.TextBox txtBoxDevName;
        
        internal System.Windows.Controls.TextBlock lblUsername;
        
        internal System.Windows.Controls.TextBox txtBoxUsername;
        
        internal System.Windows.Controls.TextBlock lblPassword;
        
        internal System.Windows.Controls.PasswordBox txtBoxPassword;
        
        internal System.Windows.Controls.Button Register;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/GeoAuthApp;component/Pages/RegisterPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.lblErrors = ((System.Windows.Controls.TextBlock)(this.FindName("lblErrors")));
            this.lblDevNmae = ((System.Windows.Controls.TextBlock)(this.FindName("lblDevNmae")));
            this.txtBoxDevName = ((System.Windows.Controls.TextBox)(this.FindName("txtBoxDevName")));
            this.lblUsername = ((System.Windows.Controls.TextBlock)(this.FindName("lblUsername")));
            this.txtBoxUsername = ((System.Windows.Controls.TextBox)(this.FindName("txtBoxUsername")));
            this.lblPassword = ((System.Windows.Controls.TextBlock)(this.FindName("lblPassword")));
            this.txtBoxPassword = ((System.Windows.Controls.PasswordBox)(this.FindName("txtBoxPassword")));
            this.Register = ((System.Windows.Controls.Button)(this.FindName("Register")));
        }
    }
}

