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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Diagnostics;
using System.Windows.Threading;
using cgi_omgevingswet.Projectmanagement.Complaints;
using cgi_omgevingswet.Classes;

namespace cgi_omgevingswet.Projectmanagement
{
    /// <summary>
    /// Interaction logic for toevoegen_gezaghebber.xaml
    /// </summary>
    public partial class toevoegen_gezaghebber : Window
    {
        private DispatcherTimer _timer;
        private TimeSpan _time;
        public delegate void GetBevoegdGezag(Gezaghebber gezaghebbers);

        public toevoegen_gezaghebber()
        {
            InitializeComponent();

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                _time = _time.Add(TimeSpan.FromSeconds(-1));
                if (_time <= TimeSpan.Zero)
                {
                    FillDataBase();
                    _timer.Stop();
                }
            }, Application.Current.Dispatcher);

            CoolDownFillDataBase(50);
        }

        private void CoolDownFillDataBase(int Miniseconds)
        {
            _time = new TimeSpan(0, 0, 0, 0, Miniseconds);
            _timer.Start();
        }

        private void FillDataBase()
        {
            dgtoevoegenGezaghebber.Items.Clear();

            object[] Parameters = new object[4];

            for (int i = 0; i < Parameters.Length; i++)
            {
                Parameters[i] = new object();
            }

            Parameters[0] = "%" + txtFiltGebruikersnaam.Text + "%";
            Parameters[1] = "%" + txtFiltVoornaam.Text + "%";
            Parameters[2] = "%" + txtFiltAchternaam.Text + "%";
            Parameters[3] = "%" + txtFiltMailadres.Text + "%";

            string sqlquery = @"SELECT G.GEBRUIKERSNAAM, P.VOORNAAM, P.TUSSENVOEGSEL, P.ACHTERNAAM, G.MAILADRES
                                FROM GEBRUIKER G 
                                INNER JOIN PARTICULIER P ON G.GEBRUIKERSNAAM = P.GEBRUIKERSNAAM
                                INNER JOIN GEMEENTE_GEBRUIKER GG ON GG.GEBRUIKERSNAAM = G.GEBRUIKERSNAAM
                                      WHERE LOWER(g.gebruikersnaam) LIKE @0
                                      AND LOWER(p.voornaam) LIKE @1
                                      AND LOWER(p.achternaam) LIKE @2
                                      AND g.mailadres LIKE @3
                                      AND GG.RECHTNAAM = 'Gezaghebber'";

            DataTable dt = Classes.Database_Init.SQLQueryReader(sqlquery, Parameters);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var gezaghebbers = new Classes.Gezaghebber
                {
                    Gebruikersnaam = dt.Rows[i]["gebruikersnaam"].ToString(),
                    Voornaam = dt.Rows[i]["voornaam"].ToString(),
                    Achternaam = dt.Rows[i]["achternaam"].ToString(),
                    Mailadres = dt.Rows[i]["mailadres"].ToString()
                };

                dgtoevoegenGezaghebber.Items.Add(gezaghebbers);
            }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CoolDownFillDataBase(1000);
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (dgtoevoegenGezaghebber.SelectedItem == null)
            {
                MessageBox.Show("U moet een gebruiker selecteren in de database om bevoegd gezaghebber te maken!");
                return;
            }

            Gezaghebber gezaghebber = dgtoevoegenGezaghebber.SelectedItem as Classes.Gezaghebber;
            ProjectForm.Getbevoegdgezag(gezaghebber);
            Close();
        }
    }
}
