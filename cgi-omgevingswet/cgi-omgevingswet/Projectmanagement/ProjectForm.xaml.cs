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
        public static Licenses.AddLicense.GetLicense GetLicenses;
        public static toevoegen_gezaghebber.GetBevoegdGezag Getbevoegdgezag;
        private Classes.Projects project;
        private List<Classes.License> newlyAddedLicense = new List<Classes.License>();
        private List<Classes.License> DeletedLicense = new List<Classes.License>();
        private List<Classes.License> UpdatedLicense = new List<Classes.License>();
        private List<Classes.Gezaghebber> newlyAddedGezaghebber = new List<Classes.Gezaghebber>();
        private List<Classes.Gezaghebber> DeletedGezaghebber = new List<Classes.Gezaghebber>();

        public ProjectForm(Classes.Projects project)
        {
            InitializeComponent();
            DataContext = project;
            this.project = project;

            Getcoordinator += Fillcoordinatortextbox;
            GetLicenses += GetLicense;
            Getbevoegdgezag += getbevoegdgezag;

            lblTitle.Content = "Project '" + project.ProjectTitel + "' beheren";
            // fillTelephoneNumberList();
            fillLicenseDataGrid();
            fillGezaghebberDataGrid();

            if (project.Bedrijfsnaam == string.Empty)
            {
                txtBedrijfsNaam.Visibility = System.Windows.Visibility.Hidden;
                txtVoornaam.Visibility = System.Windows.Visibility.Visible;
                txtTussenvoegsel.Visibility = System.Windows.Visibility.Visible;
                txtAchternaam.Visibility = System.Windows.Visibility.Visible;

                lblAchternaam.Visibility = System.Windows.Visibility.Visible;
                lblVoornaam.Visibility = System.Windows.Visibility.Visible;
                lblTussenvoegsel.Visibility = System.Windows.Visibility.Visible;
                lblBedrijfsnaam.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (project.Bedrijfsnaam != string.Empty)
            {
                txtVoornaam.Visibility = System.Windows.Visibility.Hidden;
                txtTussenvoegsel.Visibility = System.Windows.Visibility.Hidden;
                txtAchternaam.Visibility = System.Windows.Visibility.Hidden;
                txtBedrijfsNaam.Visibility = System.Windows.Visibility.Visible;

                lblAchternaam.Visibility = System.Windows.Visibility.Hidden;
                lblVoornaam.Visibility = System.Windows.Visibility.Hidden;
                lblTussenvoegsel.Visibility = System.Windows.Visibility.Hidden;
                lblBedrijfsnaam.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult MbResult = MessageBox.Show("Weet je zeker dat u dit scherm wilt verlaten zonder enige informatie aan te passen? Alle informatie ingevuld in dit scherm gaat verloren.", "Afsluiten", MessageBoxButton.YesNo);

            if (MbResult == MessageBoxResult.Yes)
                this.Close();
        }

        private void btnGetProjectCoordinator_Click(object sender, RoutedEventArgs e)
        {
            SelectProjectHelper.SelectProjectHelper SelectProject = new SelectProjectHelper.SelectProjectHelper();
            SelectProject.ShowDialog();
        }

        private void Fillcoordinatortextbox(string coordinator, string gebruikersnaam)
        {
            txtProjectCoördinator.Text = coordinator;
            project.projectcoordinator.Gebruikersnaam = gebruikersnaam;
        }

        private void GetLicense(Classes.License license)
        {
            newlyAddedLicense.Add(license);
            project.licenses.Add(license);
            refreshLicenseDatagrid();
        }

        private void getbevoegdgezag(Classes.Gezaghebber gezaghebber)
        {
            project.gezaghebber.Add(gezaghebber);
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

        private void refreshLicenseDatagrid()
        {
            dgLicenses.Items.Refresh();
        }

        private void fillLicenseDataGrid()
        {
            //dit kan ook nog veranderen
            object[] parameters = new object[1];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new object();
            }

            parameters[0] = project.ProjectID;

            DataTable dt = Classes.Database_Init.SQLQueryReader("select * from vergunning where projectid = @0", parameters);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var Licenses = new Classes.License
                {
                    VergunningsID = (int)dt.Rows[i]["VERGUNNINGSID"],
                    LicenseName = dt.Rows[i]["VERGUNNINGSNAAM"].ToString(),
                    Description = dt.Rows[i]["OMSCHRIJVING"].ToString(),
                    Status = dt.Rows[i]["STATUS"].ToString(),
                    RequestedOn = Convert.ToDateTime(dt.Rows[i]["DATUMAANVRAAG"])//ik moet de tijd wegknippen
                };

                //  dgLicenses.Items.Add(Licenses);
                project.licenses.Add(Licenses);
            }

            dgLicenses.ItemsSource = project.licenses;

            refreshLicenseDatagrid();
        }

        private void fillGezaghebberDataGrid()
        {
            //dit kan ook nog veranderen
            object[] parameters = new object[1];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new object();
            }

            parameters[0] = project.ProjectID;

            DataTable dt = Classes.Database_Init.SQLQueryReader(@"SELECT PG.GEBRUIKERSNAAM, P.VOORNAAM, P.ACHTERNAAM, G.MAILADRES, PG.projectid 
                                                                    FROM PROJECTROL_van_GEBRUIKER PG
                                                                    INNER JOIN GEBRUIKER G ON PG.GEBRUIKERSNAAM = G.GEBRUIKERSNAAM
                                                                    INNER JOIN PARTICULIER P ON G.GEBRUIKERSNAAM = P.GEBRUIKERSNAAM
                                                                    WHERE projectid = @0 
                                                                    AND rolnaam = 'GEZAGHEBBER'", parameters);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var gezaghebbers = new Classes.Gezaghebber
                {
                    Gebruikersnaam = dt.Rows[i]["gebruikersnaam"].ToString(),
                    Voornaam = dt.Rows[i]["voornaam"].ToString(),
                    Achternaam = dt.Rows[i]["achternaam"].ToString(),
                    Mailadres = dt.Rows[i]["mailadres"].ToString(),
                    ProjectID = (int)dt.Rows[i]["projectid"]
                };

                dgGezaghebbers.Items.Add(gezaghebbers);
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
            parameters[1] = project.projectcoordinator.Gebruikersnaam;

            parametername[0] = "@_ProjectId";
            parametername[1] = "@_Gebruikersnaam";

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
                for (int i = 0; i < DeletedLicense.Count; i++)
                {
                    object[] parameters = new object[count];
                    string[] parametername = new string[count];

                    for (int j = 0; j < parameters.Length; j++)
                    {
                        parameters[j] = new object();
                    }

                    parameters[0] = DeletedLicense[i].VergunningsID;

                    parametername[0] = "@_VergunningsID";

                    Classes.Database_Init.SQLExecProcedure("Spoc_Delete_License", parameters, parametername);
                }
            }
        }

        private void saveGezaghebber()
        {            
            int count = 2;
            object[] parameters = new object[count];
            string[] parametername = new string[count];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new object();
                parametername[i] = string.Empty;
            }

            for(int i = 0; i < project.gezaghebber.Count; i++) { 
                parameters[1] = project.ProjectID;
                parameters[0] = project.gezaghebber[i].Gebruikersnaam;

                parametername[1] = "@_ProjectID";
                parametername[0] = "@_Gebruikersnaam";

                Classes.Database_Init.SQLExecProcedure("spAddBevoegdGezag", parameters, parametername);
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
                saveGezaghebber();
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

        private void btnDeleteLicense_Click(object sender, RoutedEventArgs e)
        {
            if (dgLicenses.SelectedItem == null)
            {
                MessageBox.Show("U heeft geen vergunning geselecteerd om te verwijderen!");
                return;
            }

            Classes.License tempLic = (dgLicenses.SelectedItem as Classes.License);

            var CountDeletedInNewly = newlyAddedLicense.Count(s => s == tempLic);

            if (CountDeletedInNewly > 0)
            {
                newlyAddedLicense.Remove(tempLic);
            }

            DeletedLicense.Add(tempLic);
            project.licenses.Remove(tempLic);
            refreshLicenseDatagrid();
        }

        private void btnOpenLicense_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOpenComplaint_Click(object sender, RoutedEventArgs e)
        {
            Classes.Complaint bezwaar = new Classes.Complaint();
            Complaints.ComplaintsForm OpenComplaint = new Complaints.ComplaintsForm(bezwaar);
            OpenComplaint.ShowDialog();
        }

        private void btnToevoegenGezaghebber_Click(object sender, RoutedEventArgs e)
        {
            toevoegen_gezaghebber OpenGezaghebber = new toevoegen_gezaghebber();
            OpenGezaghebber.ShowDialog();
        }
    }
}
