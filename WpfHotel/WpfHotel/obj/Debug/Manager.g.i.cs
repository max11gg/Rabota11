﻿#pragma checksum "..\..\Manager.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DAD65B0FA19134388CC70413000FC8D58EDF372994E1001A9D99577152126DAA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ScottPlot;
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
using WpfHotel;


namespace WpfHotel {
    
    
    /// <summary>
    /// Manager
    /// </summary>
    public partial class Manager : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 61 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgBooking;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgCheckIn;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgCheckOut;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\Manager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgUser;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfHotel;component/manager.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Manager.xaml"
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
            
            #line 35 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_3);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 37 "..\..\Manager.xaml"
            ((System.Windows.Controls.TabItem)(target)).Loaded += new System.Windows.RoutedEventHandler(this.TabItem_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dgBooking = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            
            #line 63 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 64 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_9);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 65 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_18);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 66 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExportToCsv_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 67 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ImportFromCsv_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 71 "..\..\Manager.xaml"
            ((System.Windows.Controls.TabItem)(target)).Loaded += new System.Windows.RoutedEventHandler(this.TabItem_Loaded_1);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dgCheckIn = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 11:
            this.dgCheckOut = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 12:
            
            #line 97 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 98 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_10);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 99 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_19);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 101 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 102 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_11);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 103 "..\..\Manager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_20);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 109 "..\..\Manager.xaml"
            ((System.Windows.Controls.TabItem)(target)).Loaded += new System.Windows.RoutedEventHandler(this.TabItem_Loaded_4);
            
            #line default
            #line hidden
            return;
            case 19:
            this.dgUser = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

