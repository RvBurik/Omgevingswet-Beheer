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

            DataTable dt = Classes.Database_Init.SQLQueryReader(@"SELECT *, 
                                                                (SELECT voornaam + ' ' + tussenvoegsel + ' ' + achternaam as naam from gebruiker where gebruiker.gebruikersnaam = r.gebruikersnaam) 
                                                                as naam FROM ROLLENVANGEBRUIKER r 
                                                                where rol = 'Projectcoordinator'");

            for (int i = 0; i < dt.Rows.Count; i++)
			{
                var coordinatoren = new coordinator
                {
                    coordinatornaam = dt.Rows[i]["naam"].ToString(),
                    gebruikersnaam = dt.Rows[i]["gebruikersnaam"].ToString(),
                    gebruikersrolid = dt.Rows[i]["gebruikersrolid"].ToString()
                };

                dgcoordinatorSelecteren.Items.Add(coordinatoren);
			}
        }

        public class coordinator
        {
            public string coordinatornaam { get; set; }
            public string gebruikersnaam { get; set; }
            public string gebruikersrolid { get; set; }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dgcoordinatorSelecteren.SelectedItem != null)
            {
                ProjectForm.Getcoordinator((dgcoordinatorSelecteren.SelectedItem as coordinator).coordinatornaam, (dgcoordinatorSelecteren.SelectedItem as coordinator).gebruikersrolid);
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
