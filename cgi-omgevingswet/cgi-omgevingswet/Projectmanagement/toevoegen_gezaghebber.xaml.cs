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
    /// Interaction logic for toevoegen_gezaghebber.xaml
    /// </summary>
    public partial class toevoegen_gezaghebber : Window
    {
        public toevoegen_gezaghebber()
        {
            InitializeComponent();
            FillDataBase();
        }

        private void FillDataBase()
        {
            dgtoevoegenGezaghebber.Items.Clear();

            object[] Parameters = new object[5];

            for (int i = 0; i < Parameters.Length; i++)
            {
                Parameters[i] = new object();
            }

            Parameters[0] = "%" + txtFiltGebruikersnaam.Text + "%";
            Parameters[1] = "%" + txtFiltVoornaam.Text + "%";
            Parameters[2] = "%" + txtFiltTussenvoegsel.Text + "%";
            Parameters[3] = "%" + txtFiltAchternaam.Text + "%";
            Parameters[4] = "%" + txtFiltMailadres.Text + "%";
            //verbetering is om deze queries de volgende keer in kleine stukken te hakken voor de informatie behoefte dat ik nodig heb.
            DataTable dt = Classes.Database_Init.SQLQueryReader(@"SELECT * 
                                                                FROM (SELECT G.GEBRUIKERSNAAM, P.VOORNAAM, P.TUSSENVOEGSEL, P.ACHTERNAAM, G.MAILADRES
                                                                        FROM GEBRUIKER G INNER JOIN PARTICULIER P ON G.GEBRUIKERSNAAM = P.GEBRUIKERSNAAM
                                                                        WHERE LOWER(G.GEBRUIKERSNAAM) LIKE @0
                                                                        AND LOWER(P.VOORNAAM) LIKE @1
                                                                            AND LOWER(P.TUSSENVOEGSEL) LIKE @2
                                                                        AND LOWER(P.ACHTERNAAM) LIKE @3
                                                                        AND LOWER(G.MAILADRES) LIKE @4
                                                                        ", Parameters);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var data = new Classes.Gezaghebber
                {
                    Gebruikersnaam = dt.Rows[i]["Gebruikersnaam"].ToString(),
                    Voornaam = dt.Rows[i]["voornaam"].ToString(),
                    Tussenvoegsel = dt.Rows[i]["tussenvoegsel"].ToString(),
                    Achternaam = dt.Rows[i]["achternaam"].ToString(),
                    Mailadres = dt.Rows[i]["mailadres"].ToString()
                };

                dgtoevoegenGezaghebber.Items.Add(data);
            }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (dgtoevoegenGezaghebber.SelectedItem == null)
            {
                MessageBox.Show("U moet een gebruiker selecteren in de database om bevoegd gezaghebber te maken!");
                return;
            }
        }
    }
}
