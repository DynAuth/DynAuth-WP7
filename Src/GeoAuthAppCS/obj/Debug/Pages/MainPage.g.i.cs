﻿#pragma checksum "C:\Git\Repo\geoauth-client-wp7\Src\GeoAuthAppCS\Pages\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "56D889B9AB1F8A221B0E78BB733C3C01"
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
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock StatusLabel;
        
        internal System.Windows.Controls.TextBlock StatusTextBlock;
        
        internal System.Windows.Controls.TextBlock LatLabel;
        
        internal System.Windows.Controls.TextBlock LatitudeTextBlock;
        
        internal System.Windows.Controls.TextBlock LongLabel;
        
        internal System.Windows.Controls.TextBlock LongitudeTextBlock;
        
        internal System.Windows.Controls.TextBlock lblErrors;
        
        internal System.Windows.Controls.Button CheckIn;
        
        internal System.Windows.Controls.Button CreateRegion;
        
        internal System.Windows.Controls.StackPanel pnlRegion;
        
        internal System.Windows.Controls.TextBlock lblLocationName;
        
        internal System.Windows.Controls.TextBox txtBoxLocationName;
        
        internal System.Windows.Controls.TextBlock lblLocationRadius;
        
        internal System.Windows.Controls.TextBox txtBoxLocationRadius;
        
        internal System.Windows.Controls.Button AddRegion;
        
        internal System.Windows.Controls.Button CancelRegion;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/GeoAuthApp;component/Pages/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.StatusLabel = ((System.Windows.Controls.TextBlock)(this.FindName("StatusLabel")));
            this.StatusTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("StatusTextBlock")));
            this.LatLabel = ((System.Windows.Controls.TextBlock)(this.FindName("LatLabel")));
            this.LatitudeTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("LatitudeTextBlock")));
            this.LongLabel = ((System.Windows.Controls.TextBlock)(this.FindName("LongLabel")));
            this.LongitudeTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("LongitudeTextBlock")));
            this.lblErrors = ((System.Windows.Controls.TextBlock)(this.FindName("lblErrors")));
            this.CheckIn = ((System.Windows.Controls.Button)(this.FindName("CheckIn")));
            this.CreateRegion = ((System.Windows.Controls.Button)(this.FindName("CreateRegion")));
            this.pnlRegion = ((System.Windows.Controls.StackPanel)(this.FindName("pnlRegion")));
            this.lblLocationName = ((System.Windows.Controls.TextBlock)(this.FindName("lblLocationName")));
            this.txtBoxLocationName = ((System.Windows.Controls.TextBox)(this.FindName("txtBoxLocationName")));
            this.lblLocationRadius = ((System.Windows.Controls.TextBlock)(this.FindName("lblLocationRadius")));
            this.txtBoxLocationRadius = ((System.Windows.Controls.TextBox)(this.FindName("txtBoxLocationRadius")));
            this.AddRegion = ((System.Windows.Controls.Button)(this.FindName("AddRegion")));
            this.CancelRegion = ((System.Windows.Controls.Button)(this.FindName("CancelRegion")));
        }
    }
}

