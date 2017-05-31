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
using System.Windows.Shapes;
using System.Data;

namespace cgi_omgevingswet.Projectmanagement.SelectProjectHelper
{
    /// <summary>
    /// Interaction logic for SelectProjectHelper.xaml
    /// </summary>
    public partial class SelectProjectHelper : Window
    {
        public delegate void GetCoordinator( string coordinator);

        public SelectProjectHelper()
        {
            InitializeComponent();
            fillDataBase();
        }

        private void fillDataBase()
        {
            dgcoordinatorSelecteren.Items.Clear();

            object[] parameters = new object[1];

            for (int i = 0; i < parameters.Length; i++)
			{
			    parameters[i] = new object();
			}

            parameters[0] = "%" + txtFiltCoordinator.Text + "%";

            DataTable dt = Classes.Database_Init.SQLQueryReader(@"SELECT * FROM (SELECT g.voornaam + ' ' + g.tussenvoegsel + ' ' + g.achternaam as naam from gebruiker g) g
                                                                where LOWER(g.naam) like @0", parameters);

            for (int i = 0; i < dt.Rows.Count; i++)
			{
                var coordinatoren = new coordinator
                {
                    coordinatornaam = dt.Rows[i]["naam"].ToString()
                };

                dgcoordinatorSelecteren.Items.Add(coordinatoren);
			}
        }

        public class coordinator
        {
            public string coordinatornaam { get; set; }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            ProjectForm.Getcoordinator((dgcoordinatorSelecteren.SelectedItem as coordinator).coordinatornaam);
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtFiltCoordinator_TextChanged(object sender, TextChangedEventArgs e)
        {
            fillDataBase();
        }
    }
}
