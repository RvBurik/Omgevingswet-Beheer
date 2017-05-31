using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cgi_omgevingswet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            //removes max size and min size of the window
            MaxHeight = Double.PositiveInfinity;
            MaxWidth = Double.PositiveInfinity;

            MinHeight = 100.0;
            MinWidth = 100.0;

            //clear the grid
            GridContent.RowDefinitions.Clear();
            GridContent.ColumnDefinitions.Clear();
            GridContent.Children.Clear();
            
            //fill up the grid with the projectmanagement usercontrol
            Projectmanagement.Projectmanagement projectManagment = new Projectmanagement.Projectmanagement();
            WindowState = System.Windows.WindowState.Maximized;
            GridContent.Children.Add(projectManagment);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
