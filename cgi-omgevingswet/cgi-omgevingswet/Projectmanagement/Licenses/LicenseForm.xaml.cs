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

namespace cgi_omgevingswet.Projectmanagement.Licenses
{
    /// <summary>
    /// Interaction logic for LicenseForm.xaml
    /// </summary>
    public partial class LicenseForm : Window
    {
        Classes.License license;
        int projectid;

        public LicenseForm(Classes.License license, int projectid)
        {
            this.license = license;
            this.projectid = projectid;
            DataContext = license;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtOmschrijvingStatus.Text == string.Empty)
            {
                MessageBox.Show("U moet een omschrijving toevoegen");
                return;
            }

            int count = 3;
            object[] parameters = new object[count];
            string[] parametername = new string[count];

            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j] = new object();
                parametername[j] = string.Empty;
            }

            parameters[0] = license.VergunningsID;
            parameters[2] = txtOmschrijvingStatus.Text;

            if (license.status == Classes.Status.Aangevraagd)
                parameters[1] = "Aangevraagd";
            else if (license.status == Classes.Status.Goedkeuren)
                parameters[1] = "Uitgegeven";
            else if (license.status == Classes.Status.Afkeuren)
                parameters[1] = "Afgewezen";

            /*@vergunningsid INTEGER,
            @status VARCHAR(255),
            @omschrijvingNieuw VARCHAR(4000),*/

            parametername[0] = "@vergunningsid";
            parametername[1] = "@status";
            parametername[2] = "@omschrijvingNieuw";

            Classes.Database_Init.SQLExecProcedure("spPermitDecision", parameters, parametername);
            Close();
        }

        private void rbtnGoedkeuren_Checked(object sender, RoutedEventArgs e)
        {
            license.status = Classes.Status.Goedkeuren;
        }

        private void rbtnAfkeuren_Checked(object sender, RoutedEventArgs e)
        {
            license.status = Classes.Status.Afkeuren;
        }

        private void rbtnAangevraagd_Checked(object sender, RoutedEventArgs e)
        {
            license.status = Classes.Status.Aangevraagd;
        }
    }
}
