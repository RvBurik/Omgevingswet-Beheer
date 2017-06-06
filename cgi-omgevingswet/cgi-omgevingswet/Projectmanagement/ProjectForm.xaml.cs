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
        public static Licenses.AddLicense.GetLicense GetLicense;
        private Classes.Projects project;
        private List<Classes.License> newlyAddedLicense = new List<Classes.License>();
        private List<Classes.License> DeletedLicense = new List<Classes.License>();

        public ProjectForm(Classes.Projects project)
        {
            InitializeComponent();
            DataContext = project;
            this.project = project;

            Getcoordinator += Fillcoordinatortextbox;
            GetLicense += FillLicense;
            fillTelephoneNumberList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult MbResult = MessageBox.Show("Weet je zeker dat u dit scherm wilt verlaten zonder enige informatie aan te passen? Alle informatie ingevuld in dit scherm gaat verloren.","afsluiten", MessageBoxButton.YesNo);

            if (MbResult == MessageBoxResult.Yes)
                this.Close();
        }

        private void btnGetProjectCoordinator_Click(object sender, RoutedEventArgs e)
        {
            SelectProjectHelper.SelectProjectHelper SelectProject = new SelectProjectHelper.SelectProjectHelper();
            SelectProject.ShowDialog();
        }

        private void Fillcoordinatortextbox(string coordinator, string gebruikersnrolid)
        {
            txtProjectCoördinator.Text = coordinator;
            project.projectcoordinator.ProjectcoordinatorID = gebruikersnrolid;
        }

        private void FillLicense(Classes.License license)
        {
            newlyAddedLicense.Add(license);
            dgLicenses.Items.Add(license);
        }

        private void fillTelephoneNumberList()
        {
            object[] parameters = new object[1];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new object();
            }

            parameters[0] = project.Gebruikersnaam;

            DataTable dt = Classes.Database_Init.SQLQueryReader("SELECT TELEFOONNUMMER FROM Gebruikertel WHERE GEBRUIKERSNAAM = @0", parameters);

            ListTelephoneNumbers.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListTelephoneNumbers.Items.Add(dt.Rows[i]["TELEFOONNUMMER"]);
            }
        }

        private void fillAdressList()
        {
            //dit kan ook nog veranderen
            object[] parameters = new object[1];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new object();
            }

            parameters[0] = project.Gebruikersnaam;

            DataTable dt = Classes.Database_Init.SQLQueryReader("SELECT * FROM ADRESGEGEVENS WHERE adresid in (SELECT adresid FROM ADRES_VAN_GEBRUIKER WHERE GEBRUIKERSNAAM = @0", parameters);

            ListTelephoneNumbers.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListAdress.Items.Add(dt.Rows[i]["POSTCODE"] + " " + dt.Rows[i]["huisnummer"] + " " + dt.Rows[i]["toevoeging"]);
            }
        }

        private void saveCoordinator()
        {
            int count = 2;
            object[] parameters = new object[count];
            string[] parametername = new string[count];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new object();
                parametername[i] = string.Empty;
            }

            parameters[0] = project.ProjectID;
            parameters[1] = project.projectcoordinator.ProjectcoordinatorID;

            parametername[0] = "@_ProjectId";
            parametername[1] = "@_gebruikersrolid";

            Classes.Database_Init.SQLExecProcedure("Spoc_Set_ProjectCoordinator", parameters, parametername);
        }

        private void saveLicense()
        {
            if (newlyAddedLicense.Count > 0)
            {
                int count = 4;
                for (int i = 0; i < newlyAddedLicense.Count; i++)
                {
                    object[] parameters = new object[count];
                    string[] parametername = new string[count];

                    for (int j = 0; j < parameters.Length; j++)
                    {
                        parameters[j] = new object();
                        parametername[j] = string.Empty;
                    }

                    parameters[0] = newlyAddedLicense[i].LicenseName;
                    parameters[1] = newlyAddedLicense[i].Description;
                    parameters[2] = project.ProjectID;
                    parameters[3] = "Aangevraagd";

                    parametername[0] = "@_License";
                    parametername[1] = "@_Description";
                    parametername[2] = "@_ProjectID";
                    parametername[3] = "@_Status";

                    Classes.Database_Init.SQLExecProcedure("Spoc_Add_License", parameters, parametername);
                }
            }

            if (DeletedLicense.Count > 0)
            {
                int count = 1;
                for (int i = 0; i < newlyAddedLicense.Count; i++)
                {
                    object[] parameters = new object[count];
                    string[] parametername = new string[count];

                    for (int j = 0; j < parameters.Length; j++)
                    {
                        parameters[j] = new object();
                        parametername[j] = string.Empty;
                    }

                    parameters[0] = newlyAddedLicense[i].LicenseName;

                    parametername[0] = "@_VergunningsID";

                    Classes.Database_Init.SQLExecProcedure("Spoc_Delete_License", parameters, parametername);
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //De werkzaamheid moet nog wel aangepast kunnen worden!!!!!
            MessageBoxResult result =
                MessageBox.Show("Weet u zeker dat u alle informatie wilt opslaan? Als u ja klikt, wordt alle ingevulde of aangepaste informatie opgeslagen en wordt het scherm afgesloten.", "Opslaan", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                saveLicense();
                saveCoordinator();
                Close();
            }

        }

        private void btnAddLicense_Click(object sender, RoutedEventArgs e)
        {
            Licenses.AddLicense OpenAddLicense = new Licenses.AddLicense();
            OpenAddLicense.ShowDialog();
        }

        private void btnToekennen_Click(object sender, RoutedEventArgs e)
        {
            toevoegen_gezaghebber ToevoegenGezaghebber = new toevoegen_gezaghebber();
            ToevoegenGezaghebber.ShowDialog();
        }
    }
}
