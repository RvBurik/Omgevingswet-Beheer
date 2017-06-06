using System;
using System.Collections.Generic;
using System.Data;
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

namespace cgi_omgevingswet.Projectmanagement.Complaints
{
    /// <summary>
    /// Interaction logic for ComplaintsForm.xaml
    /// </summary>
    public partial class ComplaintsForm : Window
    {
        private Classes.Complaint bezwaar;

        public ComplaintsForm(Classes.Complaint bezwaar)
        {
            InitializeComponent();
            this.bezwaar = bezwaar;
            DataContext = fillComplaintData(bezwaar);
        }

        private ComplaintData fillComplaintData(Classes.Complaint bezwaar)
        {
            int count = 3;
            object[] parameters = new object[count];

            for (int i = 0; i < count; i++)
            {
                parameters[i] = new object();
            }

            parameters[0] = bezwaar.Gebruikersnaam;
            parameters[1] = bezwaar.ProjectID;
            parameters[2] = bezwaar.VergunningsID;

            DataTable dtProject = Classes.Database_Init.SQLQueryReader(@"
                select p.projectid, p.projecttitel, p.aangemaaktop, p.werkzaamheid, p.xcoördinaat, p.ycoördinaat,
                g.gebruikersnaam, g.mailadres,
                part.voornaam, part.tussenvoegsel, part.achternaam
                from project p
                inner join projectrol_van_gebruiker pg on p.projectid = pg.projectid
                inner join gebruiker g on pg.gebruikersnaam = g.gebruikersnaam
                inner join particulier part on g.gebruikersnaam = part.gebruikersnaam
                where p.gebruikersnaam = @0
                and p.projectid = @1
                and pg.rolnaam = 'Initiatiefnemer'
                ", parameters);
            /*
            Voornaam.Clear();
            Tussenvoegsel.Clear();
            Achternaam.Clear();
            Mailadres.Clear();
            Voornaam.Add(dt.Rows[0]["voornaam"]);
            */
            fillTelephoneNumberList();


            for (int i = 0; i < dtProject.Rows.Count; i++)
            {
                Classes.Projects project = new Classes.Projects
                {
                    ProjectID = int.Parse(dtProject.Rows[i]["p.projectid"].ToString()),
                    Gebruikersnaam = dtProject.Rows[i]["g.gebruikersnaam"].ToString(),
                    Aangemaaktop = Convert.ToDateTime(dtProject.Rows[i]["p.aangemaaktop"].ToString()),
                    Werkzaamheid = dtProject.Rows[i]["g.werkzaamheid"].ToString(),
                    Volledigenaam = dtProject.Rows[i]["voornaam"].ToString() + " " + dt.Rows[i]["tussenvoegsel"].ToString() + " " + dt.Rows[i]["achternaam"].ToString(),
                    Voornaam = dtProject.Rows[i]["part.voornaam"].ToString(),
                    Tussenvoegsel = dtProject.Rows[i]["part.tussenvoegsel"].ToString(),
                    Achternaam = dtProject.Rows[i]["part.achternaam"].ToString(),
                    Mailadres = dtProject.Rows[i]["g.mailadres"].ToString(),
                    Bedrijfsnaam = ""
                };

                Classes.License vergunning = new Classes.License
                {
                    LicenseName = "",
                    Description = "",
                    Status = "",
                    RequestedOn = ""
                };

                Classes.Person particulier = new Classes.Person
                {
                    gebruikersnaam = "",
                    voornaam = "",
                    tussenvoegsel = "",
                    achternaam = "",
                    geboortedatum = "",
                    geslacht = ""
                };
            }

            ComplaintData bezwaarData = new ComplaintData
            {
                bezwaar = bezwaar,
                project = project,
                vergunning = vergunning,
                particulier = particulier
            };
            return bezwaarData;
        }

        private class ComplaintData {
            public Classes.Complaint bezwaar {get; set; }
            public Classes.Projects project { get; set; }
            public Classes.License vergunning { get; set; }
            public Classes.Person particulier { get; set; }
        }

        private void fillTelephoneNumberList()
        {
            object[] parameters = new object[1];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = new object();
            }

            parameters[0] = bezwaar.Gebruikersnaam;

            DataTable dt = Classes.Database_Init.SQLQueryReader(@"
                SELECT TELEFOONNUMMER
                FROM telefoon_van_gebruiker
                WHERE GEBRUIKERSNAAM = @0", parameters);

            ListTelephoneNumbers.Items.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListTelephoneNumbers.Items.Add(dt.Rows[i]["TELEFOONNUMMER"]);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult MbResult = MessageBox.Show("Weet je zeker dat u dit scherm wilt verlaten zonder enige informatie aan te passen? Alle informatie ingevuld in dit scherm gaat verloren.", "Afsluiten", MessageBoxButton.YesNo);

            if (MbResult == MessageBoxResult.Yes)
                this.Close();
        }

        private void saveComplaint()
        {
            int count = 5;
            object[] parameters = new object[count];
            string[] parametername = new string[count];

            for (int i = 0; i < count; i++)
            {
                parameters[i] = new object();
                parametername[i] = string.Empty;
            }

            parameters[0] = bezwaar.Gebruikersnaam;
            parameters[1] = bezwaar.ProjectID;
            parameters[2] = bezwaar.VergunningsID;
            parameters[3] = bezwaar.Besluit;
            parameters[4] = bezwaar.Besluittreden;

            parametername[0] = "@_gebruikersnaam";
            parametername[1] = "@_projectid";
            parametername[2] = "@_vergunningsid";
            parametername[3] = "@_besluit";
            parametername[4] = "@_besluitreden";

            Classes.Database_Init.SQLExecProcedure("uspBezwaarBesluit", parameters, parametername);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =
                MessageBox.Show("Weet u zeker dat u alle informatie wilt opslaan? Als u ja klikt, wordt alle ingevulde of aangepaste informatie opgeslagen en wordt het scherm afgesloten.", "Opslaan", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                saveComplaint();
                Close();
            }
        }
    }
}
