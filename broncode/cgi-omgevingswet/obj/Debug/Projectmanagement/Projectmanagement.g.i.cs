﻿#pragma checksum "..\..\..\Projectmanagement\Projectmanagement.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FAAD983ED866AE259330B931D7CCF215"
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


namespace cgi_omgevingswet.Projectmanagement {
    
    
    /// <summary>
    /// Projectmanagement
    /// </summary>
    public partial class Projectmanagement : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\Projectmanagement\Projectmanagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Projectmanagement\Projectmanagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFiltVoornaam;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Projectmanagement\Projectmanagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFiltAchternaam;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Projectmanagement\Projectmanagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFiltGebruikersnaam;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Projectmanagement\Projectmanagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFiltMailadres;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Projectmanagement\Projectmanagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpFiltAanmaakdatumVan;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Projectmanagement\Projectmanagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpFiltAanmaakdatumTot;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Projectmanagement\Projectmanagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbItemsPerPage;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Projectmanagement\Projectmanagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPreviousPage;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Projectmanagement\Projectmanagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNextPage;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\Projectmanagement\Projectmanagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgProject;
        
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
            System.Uri resourceLocater = new System.Uri("/cgi-omgevingswet;component/projectmanagement/projectmanagement.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Projectmanagement\Projectmanagement.xaml"
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
            this.lbl = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.txtFiltVoornaam = ((System.Windows.Controls.TextBox)(target));
            
            #line 43 "..\..\..\Projectmanagement\Projectmanagement.xaml"
            this.txtFiltVoornaam.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtFilter_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtFiltAchternaam = ((System.Windows.Controls.TextBox)(target));
            
            #line 46 "..\..\..\Projectmanagement\Projectmanagement.xaml"
            this.txtFiltAchternaam.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtFilter_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtFiltGebruikersnaam = ((System.Windows.Controls.TextBox)(target));
            
            #line 49 "..\..\..\Projectmanagement\Projectmanagement.xaml"
            this.txtFiltGebruikersnaam.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtFilter_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtFiltMailadres = ((System.Windows.Controls.TextBox)(target));
            
            #line 52 "..\..\..\Projectmanagement\Projectmanagement.xaml"
            this.txtFiltMailadres.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtFilter_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.dpFiltAanmaakdatumVan = ((System.Windows.Controls.DatePicker)(target));
            
            #line 55 "..\..\..\Projectmanagement\Projectmanagement.xaml"
            this.dpFiltAanmaakdatumVan.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dpFilterAanmaakDatum_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dpFiltAanmaakdatumTot = ((System.Windows.Controls.DatePicker)(target));
            
            #line 58 "..\..\..\Projectmanagement\Projectmanagement.xaml"
            this.dpFiltAanmaakdatumTot.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dpFilterAanmaakDatum_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cmbItemsPerPage = ((System.Windows.Controls.ComboBox)(target));
            
            #line 61 "..\..\..\Projectmanagement\Projectmanagement.xaml"
            this.cmbItemsPerPage.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbItemsPerPage_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnPreviousPage = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\..\Projectmanagement\Projectmanagement.xaml"
            this.btnPreviousPage.Click += new System.Windows.RoutedEventHandler(this.btnPreviousPage_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnNextPage = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\..\Projectmanagement\Projectmanagement.xaml"
            this.btnNextPage.Click += new System.Windows.RoutedEventHandler(this.btnNextPage_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.dgProject = ((System.Windows.Controls.DataGrid)(target));
            
            #line 80 "..\..\..\Projectmanagement\Projectmanagement.xaml"
            this.dgProject.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgProject_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 92 "..\..\..\Projectmanagement\Projectmanagement.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

