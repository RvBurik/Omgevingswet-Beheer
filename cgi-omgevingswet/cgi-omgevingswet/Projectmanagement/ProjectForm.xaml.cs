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
        Classes.Projectcoordinator NewProjectcoordinator = new Classes.Projectcoordinator();

        public static SelectProjectHelper.SelectProjectHelper.GetCoordinator Getcoordinator;

        public delegate void GetLicenseFromOtherForm(Classes.License license);
        public static GetLicenseFromOtherForm GetLicensesForInsert;
        public static GetLicenseFromOtherForm GetLicensesForUpdate;
        private Classes.Projects project;
        private List<Classes.License> newlyAddedLicense = new List<Classes.License>();
        private List<Classes.License> DeletedLicense = new List<Classes.License>();
        private List<Classes.License> UpdatedLicense = new List<Classes.License>();
        bool saveCoordinatoren = false;

        public ProjectForm(Classes.Projects project)
        {
            InitializeComponent();
            DataContext = project;
            this.project = project;

            Getcoordinator += Fillcoordinatortextbox;
            GetLicensesForInsert += GetLicense;
            GetLicensesForUpdate += GetLicenseForUpdate;

            lblTitle.Content = "Project '" + project.ProjectTitel + "' beheren";
            // fillTelephoneNumberList();
            fillComplaintDataGrid();
            fillLicenseDataGrid();

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
            NewProjectcoordinator.Gebruikersnaam = gebruikersnaam;
            NewProjectcoordinator._Projectcoordinator = coordinator;
            txtProjectCoördinator.Text = NewProjectcoordinator._Projectcoordinator;
            saveCoordinatoren = true;
        }

        private void GetLicense(Classes.License license)
        {
            newlyAddedLicense.Add(license);
            project.licenses.Add(license);
            refreshLicenseDatagrid();
        }

        private void GetLicenseForUpdate(Classes.License license)
        {
            UpdatedLicense.Add(license);
            refreshLicenseDatagrid();
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

        private void refreshComplaintsDatagrid()
        {
            dgComplaints.Items.Refresh();
        }

        private void fillComplaintDataGrid()
        {
            //dit kan ook nog veranderen
            object[] parameters = new object[1];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new object();
            }

            parameters[0] = project.ProjectID;

            DataTable dt = Classes.Database_Init.SQLQueryReader("select * from bezwaar where projectid = @0", parameters);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var Complaints = new Classes.Complaint
                {
                    Gebruikersnaam = dt.Rows[i]["GEBRUIKERSNAAM"].ToString(),
                    ProjectID = (int)dt.Rows[i]["PROJECTID"],
                    VergunningsID = (int)dt.Rows[i]["VERGUNNINGSID"],
                    Bezwaarreden = dt.Rows[i]["BEZWAARREDEN"].ToString(),
                    Besluit = null,
                    Besluitreden = null
                    //Besluit = dt.Rows[i]["BESLUIT"].ToString(),
                    //Besluitreden = dt.Rows[i]["BESLUITREDEN"].ToString()
                };
                if (dt.Rows[i]["BESLUIT"] != null)
                {
                    Complaints.Besluit = dt.Rows[i]["BESLUIT"].ToString();
                    Complaints.Besluitreden = dt.Rows[i]["BESLUITREDEN"].ToString();
                }

                //  dgComplaints.Items.Add(Complaints);
                project.complaints.Add(Complaints);
            }

            dgComplaints.ItemsSource = project.complaints;

            refreshComplaintsDatagrid();
        }

        private void refreshLicenseDatagrid()
        {
            dgLicenses.Items.Refresh();
        }

        private void fillLicenseDataGrid()
        {            //dit kan ook nog veranderen
            project.licenses.Clear();

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
                    RequestedOn = Convert.ToDateTime(dt.Rows[i]["DATUMAANVRAAG"]),//ik moet de tijd wegknippen
                    Status = dt.Rows[i]["STATUS"].ToString(),

                };

                if (Licenses.Status == "Aangevraagd")
                    Licenses.status = Classes.Status.Aangevraagd;
                else if (Licenses.Status == "Uitgegeven")
                    Licenses.status = Classes.Status.Goedkeuren;
                else if (Licenses.Status == "Afgewezen")
                    Licenses.status = Classes.Status.Afkeuren;

              //  dgLicenses.Items.Add(Licenses);
                project.licenses.Add(Licenses);
            }

            dgLicenses.ItemsSource = null;
            dgLicenses.ItemsSource = project.licenses;

            refreshLicenseDatagrid();
        }

        private void saveCoordinator()
        {
            int count = 0;
            if (project.projectcoordinator.Gebruikersnaam != null) count = 3;
            else count = 2;
            object[] parameters = new object[count];
            string[] parametername = new string[count];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new object();
                parametername[i] = string.Empty;
            }

            parameters[0] = project.ProjectID;
            parameters[1] = NewProjectcoordinator.Gebruikersnaam;

            if (project.projectcoordinator.Gebruikersnaam != null)
                parameters[2] = project.projectcoordinator.Gebruikersnaam;

            parametername[0] = "@_ProjectId";
            parametername[1] = "@_Gebruikersnaam";

            if (project.projectcoordinator.Gebruikersnaam != null)
                parametername[2] = "@_OldGebruikersnaam";

            Classes.Database_Init.SQLExecProcedure("Spoc_Set_ProjectCoordinator", parameters, parametername);

            project.projectcoordinator = NewProjectcoordinator;
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

                    if (newlyAddedLicense[i].status == Classes.Status.Aangevraagd)
                        parameters[3] = "Aangevraagd";
                    else if (newlyAddedLicense[i].status == Classes.Status.Goedkeuren)
                        parameters[3] = "Uitgegeven";
                    else if (newlyAddedLicense[i].status == Classes.Status.Afkeuren)
                        parameters[3] = "Afgewezen";


                    parametername[0] = "@_License";
                    parametername[1] = "@_Description";
                    parametername[2] = "@_ProjectID";
                    parametername[3] = "@_Status";

                    Classes.Database_Init.SQLExecProcedure("spAdd_License", parameters, parametername);
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //De werkzaamheid moet nog wel aangepast kunnen worden!!!!!
            MessageBoxResult result =
                MessageBox.Show("Weet u zeker dat u alle informatie wilt opslaan? Als u ja klikt, wordt alle ingevulde of aangepaste informatie opgeslagen en wordt het scherm afgesloten.", "Opslaan", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                saveLicense();
                if (saveCoordinatoren)
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
            if (dgLicenses.SelectedItem == null)
            {
                MessageBox.Show("U moet een vergunning selecteren wilt u er een openen");
                return;
            }

            Licenses.LicenseForm licenceForm = new Licenses.LicenseForm((dgLicenses.SelectedItem as Classes.License), project.ProjectID);
            licenceForm.ShowDialog();
            fillLicenseDataGrid();
        }

        private void btnOpenComplaint_Click(object sender, RoutedEventArgs e)
        {
            Classes.Complaint selectedComplaint = new Classes.Complaint();
            selectedComplaint = dgComplaints.SelectedItem as Classes.Complaint;
            Complaints.ComplaintsForm OpenComplaint = new Complaints.ComplaintsForm(selectedComplaint);
            OpenComplaint.ShowDialog();
        }
    }
}
