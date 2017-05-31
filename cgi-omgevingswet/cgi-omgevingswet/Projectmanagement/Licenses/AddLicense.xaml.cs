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

namespace cgi_omgevingswet.Projectmanagement.Licenses
{
    /// <summary>
    /// Interaction logic for AddLicense.xaml
    /// </summary>
    public partial class AddLicense : Window
    {
        public delegate void GetLicense(Classes.License license);

        public AddLicense()
        {
            InitializeComponent();
            fillVergunningen();
        }

        private void fillVergunningen()
        {
            dgSelectLicense.Items.Clear();

            DataTable dt = Classes.Database_Init.SQLQueryReader("SELECT * FROM VERGUNNINGSTYPE WHERE VERGUNNINGSNAAM LIKE @0 ", new object[1] { "%" + txtFiltvergunning.Text + "%" });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var item = new License
                {
                    LicenseName = dt.Rows[i]["VERGUNNINGSNAAM"].ToString()
                };

                dgSelectLicense.Items.Add(item);
            }
        }

        public class License
        {
            public string LicenseName { get; set; }
        }

        private void txtFiltvergunning_TextChanged(object sender, TextChangedEventArgs e)
        {
            fillVergunningen();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (txtDescription.Text == string.Empty)
            {
                MessageBox.Show("U Moet een omschrijving toevoegen aan deze vergunningsaanvraag.");
                return;
            }

            if (dgSelectLicense.SelectedItem == null)
            {
                MessageBox.Show("U moet een vergunning kiezen");
                return;
            }

            Classes.License license = new Classes.License
            {
                LicenseName = (dgSelectLicense.SelectedItem as License).LicenseName,
                Description = txtDescription.Text,
                Status = "Aangevraagd",
                RequestedOn = DateTime.Today
            };

            ProjectForm.GetLicense(license);
        }
    }
}
