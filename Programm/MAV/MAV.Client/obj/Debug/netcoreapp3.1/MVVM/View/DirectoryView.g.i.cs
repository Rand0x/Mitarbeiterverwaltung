﻿#pragma checksum "..\..\..\..\..\MVVM\View\DirectoryView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6A05CE9EA96B296F322DE8EB3DCD95C7D2FC228E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using MAV.Client.MVVM.View;
using MAV.Client.MVVM.ViewModel;
using ModernWpf;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using ModernWpf.DesignTime;
using ModernWpf.Markup;
using ModernWpf.Media.Animation;
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


namespace MAV.Client.MVVM.View {
    
    
    /// <summary>
    /// DirectoryView
    /// </summary>
    public partial class DirectoryView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbDepartments;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ModernWpf.Controls.AutoSuggestBox controlsSearchBox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvUsers;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MAV.Client;component/mvvm/view/directoryview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cmbDepartments = ((System.Windows.Controls.ComboBox)(target));
            
            #line 27 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            this.cmbDepartments.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbDepartments_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.controlsSearchBox = ((ModernWpf.Controls.AutoSuggestBox)(target));
            
            #line 38 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            this.controlsSearchBox.QuerySubmitted += new ModernWpf.TypedEventHandler<ModernWpf.Controls.AutoSuggestBox, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs>(this.controlsSearchBox_QuerySubmitted);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lvUsers = ((System.Windows.Controls.ListView)(target));
            
            #line 43 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            this.lvUsers.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListView_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 46 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OnCtxMenuInfo);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 51 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OnCtxMenuEdit);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 62 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.sortFirstName);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 67 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.sortLastName);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 72 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.sortDepartment);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 77 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.sortLandlineNbr);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 82 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            ((System.Windows.Controls.GridViewColumnHeader)(target)).Click += new System.Windows.RoutedEventHandler(this.sortMobileNbr);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 92 "..\..\..\..\..\MVVM\View\DirectoryView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

