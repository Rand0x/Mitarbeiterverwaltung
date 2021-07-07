﻿#pragma checksum "..\..\..\..\..\MVVM\View\EmployeeEditView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EFC42BFE4784FC0E9FB7A7354C7D9B06B623E6C8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using MAV.Client;
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
    /// EmployeeEditView
    /// </summary>
    public partial class EmployeeEditView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 72 "..\..\..\..\..\MVVM\View\EmployeeEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbDepartments;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\..\MVVM\View\EmployeeEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxSex;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\..\..\MVVM\View\EmployeeEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox commentBox;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\..\..\..\MVVM\View\EmployeeEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel sensibleData2;
        
        #line default
        #line hidden
        
        
        #line 228 "..\..\..\..\..\MVVM\View\EmployeeEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddBonusPaymentButton;
        
        #line default
        #line hidden
        
        
        #line 267 "..\..\..\..\..\MVVM\View\EmployeeEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button WarningButton;
        
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
            System.Uri resourceLocater = new System.Uri("/MAV.Client;component/mvvm/view/employeeeditview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\EmployeeEditView.xaml"
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
            return;
            case 2:
            this.cbxSex = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.commentBox = ((System.Windows.Controls.RichTextBox)(target));
            return;
            case 4:
            this.sensibleData2 = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.AddBonusPaymentButton = ((System.Windows.Controls.Button)(target));
            
            #line 229 "..\..\..\..\..\MVVM\View\EmployeeEditView.xaml"
            this.AddBonusPaymentButton.Click += new System.Windows.RoutedEventHandler(this.OnAddBonusPayment);
            
            #line default
            #line hidden
            return;
            case 6:
            this.WarningButton = ((System.Windows.Controls.Button)(target));
            
            #line 268 "..\..\..\..\..\MVVM\View\EmployeeEditView.xaml"
            this.WarningButton.Click += new System.Windows.RoutedEventHandler(this.OnAddWarning);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

