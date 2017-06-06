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
        public delegate void GetCoordinator(string coordinator, string gebruikersrolid);

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

            DataTable dt = Classes.Database_Init.SQLQueryReader(@"select * from gemeente_gebruiker g
                                            inner join (select voornaam, tussenvoegsel, achternaam, gebruikersnaam as part_gebruiker from PARTICULIER) p ON g.gebruikersnaam = p.part_gebruiker
                                             where rechtnaam = 'coördinator'");

            for (int i = 0; i < dt.Rows.Count; i++)
			{
                var coordinatoren = new Classes.Projectcoordinator
                {
                    _Projectcoordinator = dt.Rows[i]["voornaam"].ToString() + " " + dt.Rows[i]["tussenvoegsel"].ToString() + " " +  dt.Rows[i]["achternaam"].ToString(),
                    Gebruikersnaam = dt.Rows[i]["gebruikersnaam"].ToString()
                };

                dgcoordinatorSelecteren.Items.Add(coordinatoren);
			}
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dgcoordinatorSelecteren.SelectedItem != null)
            {
                ProjectForm.Getcoordinator((dgcoordinatorSelecteren.SelectedItem as Classes.Projectcoordinator)._Projectcoordinator, (dgcoordinatorSelecteren.SelectedItem as Classes.Projectcoordinator).Gebruikersnaam);
                Close();
            }
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
