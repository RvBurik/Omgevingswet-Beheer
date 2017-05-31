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

namespace cgi_omgevingswet.Projectmanagement
{
    /// <summary>
    /// Interaction logic for ProjectForm.xaml
    /// </summary>
    public partial class ProjectForm : Window
    {
        public static SelectProjectHelper.SelectProjectHelper.GetCoordinator Getcoordinator;
        private Classes.Projects project;

        public ProjectForm(Classes.Projects project)
        {
            InitializeComponent();
            DataContext = project;
            this.project = project;

            if (project.Bedrijfsnaam == string.Empty) txtBedrijfsNaam.IsEnabled = false;
            Getcoordinator += Fillcoordinatortextbox;
            fillTelephoneNumberList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult MbResult = MessageBox.Show("Weet je zeker dat je dit scherm wilt verlaten zonder enige informatie aan te passen? Alle informatie ingevuld in dit scherm gaat verloren.","", MessageBoxButton.YesNo);

            if (MbResult == MessageBoxResult.Yes)
                this.Close();
        }

        private void btnGetProjectCoordinator_Click(object sender, RoutedEventArgs e)
        {
            SelectProjectHelper.SelectProjectHelper SelectProject = new SelectProjectHelper.SelectProjectHelper();
            SelectProject.ShowDialog();
        }

        private void Fillcoordinatortextbox(string coordinator)
        {
            txtProjectCoördinator.Text = coordinator;
        }

        private void fillTelephoneNumberList()
        {
            object[] paramters = new object[1];

            for (int i = 0; i < paramters.Length; i++)
            {
                paramters[0] = new object();
            }

            paramters[0] = project.Gebruikersnaam;

            DataTable dt = Classes.Database_Init.SQLQueryReader("SELECT TELEFOONNUMMER FROM Gebruikertel WHERE GEBRUIKERSNAAM = @0", paramters);

            ListTelephoneNumbers.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListTelephoneNumbers.Items.Add(dt.Rows[i]["TELEFOONNUMMER"]);
            }
        }

        private void fillAdressList()
        {
            object[] parameter = new object[1];
        }
    }
}
