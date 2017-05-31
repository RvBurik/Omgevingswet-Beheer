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


namespace cgi_omgevingswet.Projectmanagement
{
    /// <summary>
    /// Interaction logic for Projectbeheer.xaml
    /// </summary>
    public partial class Projectmanagement : UserControl
    {
        private int rowsPerPage;
        private int startNumber;
        private int searchCount;
        private DispatcherTimer _timer;
        private TimeSpan _time;
        private Window window;

        /// <summary>
        /// Constructor for the Projectmanagement.
        /// Initializes components, variables and fills database
        /// </summary>
        public Projectmanagement(Window window)
        {
            InitializeComponent();
            this.window = window;
            //Set datepickers selecteddate
            dpFiltAanmaakdatumVan.SelectedDate = DateTime.Today.AddYears(-1);
            dpFiltAanmaakdatumTot.SelectedDate = DateTime.Today;

            //Fill cmbItemsPerPage
            FillcmbItemsPerPage();

            //Cuts off items from the selectedvalue selected in the cmbItemsPerPage combobox
            rowsPerPage = int.Parse(cmbItemsPerPage.SelectedValue.ToString().Replace("items", ""));
            
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
            startNumber = 0;
        }

        private void CoolDownFillDataBase(int Miniseconds)
        {
            _time = new TimeSpan(0, 0, 0, 0, Miniseconds);
            _timer.Start();
        }

        /// <summary>
        /// Filling the combobox cmbItemsPerPage in a function because it's easier to change if it's neccasary in the future...
        /// </summary>
        private void FillcmbItemsPerPage()
        {
            cmbItemsPerPage.Items.Clear();

            for (int i = 1; i <= 10; i++)
            {
                cmbItemsPerPage.Items.Add((i * 10 + " items"));
            }

            cmbItemsPerPage.SelectedIndex = 0;
        }

        /// <summary>
        /// Fills de database in projectmanagement
        /// </summary>
        private void FillDataBase()
        {
            dgProject.Items.Clear();

            object[] Parameters = new object[8];

            for (int i = 0; i < Parameters.Length; i++)
			{
                Parameters[i] = new object();
			}

            Parameters[0] = "%" + txtFiltVoornaam.Text + "%";
            Parameters[1] = "%" + txtFiltAchternaam.Text + "%";
            Parameters[2] = "%" + txtFiltGebruikersnaam.Text + "%";
            Parameters[3] = "%" + txtFiltMailadres.Text + "%";
            Parameters[4] = dpFiltAanmaakdatumVan.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
            Parameters[5] = dpFiltAanmaakdatumTot.SelectedDate.Value.Date.AddDays(1).ToString("yyyy-MM-dd");
            Parameters[6] = startNumber;
            Parameters[7] = (startNumber + rowsPerPage);
            //verbetering is om deze queries de volgende keer in kleine stukken te hakken voor de informatie behoefte dat ik nodig heb.
            DataTable dt = Classes.Database_Init.SQLQueryReader(@"SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY p.projectid) AS RowIndex, 
                                                        CONVERT(VARCHAR(10),p.aangemaaktop,103) aangemaaktop, p.gebruikersnaam, p.projectid, p.werkzaamheid, 
                                                        g.voornaam, g.achternaam, g.tussenvoegsel, g.mailadres, gc.voornaam as cvoornaam, gc.tussenvoegsel as ctusssenvoegsel, gc.achternaam as cachternaam, rvg.Gebruikersrolid FROM project p
                                                        INNER JOIN (SELECT voornaam, achternaam, tussenvoegsel, mailadres, gebruikersnaam FROM gebruiker) g ON p.gebruikersnaam = g.gebruikersnaam
                                                        INNER JOIN (SELECT GebruikersRolID, projectid FROM GEBRUIKERSROLLENBIJPROJECT) grp ON grp.projectid = p.projectid
                                                        INNER JOIN (SELECT GebruikersRolID, gebruikersnaam FROM ROLLENVANGEBRUIKER) rvg ON grp.GebruikersRolID = rvg.GebruikersRolID
                                                        INNER JOIN (SELECT gebruikersnaam, voornaam, tussenvoegsel, achternaam FROM GEBRUIKER) gc ON gc.gebruikersnaam = rvg.gebruikersnaam
                                                        where LOWER(g.voornaam) like @0
                                                        AND LOWER(g.achternaam) like @1
                                                        AND LOWER(p.gebruikersnaam) like @2
                                                        AND LOWER(g.mailadres) like @3
                                                        AND p.aangemaaktop between @4 AND @5) sub 
                                                        WHERE sub.RowIndex > @6 AND sub.RowIndex <= @7", Parameters);
            
            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                var data = new Classes.Projects
                {
                    ProjectID = int.Parse(dt.Rows[i]["ProjectID"].ToString()),
                    Gebruikersnaam = dt.Rows[i]["Gebruikersnaam"].ToString(),
                    Aangemaaktop = Convert.ToDateTime(dt.Rows[i]["Aangemaaktop"].ToString()),
                    Werkzaamheid = dt.Rows[i]["Werkzaamheid"].ToString(),
                    Voornaam = dt.Rows[i]["voornaam"].ToString(),
                    Tussenvoegsel = dt.Rows[i]["tussenvoegsel"].ToString(),
                    Achternaam = dt.Rows[i]["achternaam"].ToString(),
                    Volledigenaam = dt.Rows[i]["voornaam"].ToString() + " " + dt.Rows[i]["tussenvoegsel"].ToString() + " " + dt.Rows[i]["achternaam"].ToString(),
                    Mailadres = dt.Rows[i]["mailadres"].ToString(),
                    projectcoordinator = new Classes.Projectcoordinator{
                        _Projectcoordinator = dt.Rows[i]["cvoornaam"].ToString() + " " + dt.Rows[i]["ctusssenvoegsel"].ToString() + " " + dt.Rows[i]["cachternaam"].ToString(),
                        ProjectcoordinatorID = dt.Rows[i]["Gebruikersrolid"].ToString()
                    }
                };

                //Dit gaat nog zekerweten veranderd worden met de nieuwe database structuur
                object[] Parametersbedrijf = new object[1];

                for (int k = 0; k < Parametersbedrijf.Length; k++)
			    {
                    Parametersbedrijf[k] = new object();
			    }

                Parametersbedrijf[0] = data.Gebruikersnaam;

                DataTable dtGetBedrijf = Classes.Database_Init.SQLQueryReader(@"SELECT bedrijfsnaam FROM BEDRIJF
                WHERE kvknummer in (SELECT kvknummer from werknemer where gebruikersnaam = @0)", Parametersbedrijf);

                if (dtGetBedrijf.Rows.Count > 0) data.Bedrijfsnaam = dtGetBedrijf.Rows[0][0].ToString();
                else data.Bedrijfsnaam = string.Empty;

                dgProject.Items.Add(data);
            }
        }

        /// <summary>
        /// Sends out a signal to the textboxfilters when their text has been changed to fill up the database again wth the filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CoolDownFillDataBase(1000);
        }

        /// <summary>
        /// Sends out a signal to the datagrid(table) when the selection has been changed, what to do has yet to be implemented
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Sends out a signal to the datepicker filters when their date has been changed to fill up the database again with the filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpFilterAanmaakDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpFiltAanmaakdatumVan.SelectedDate != null &&
            dpFiltAanmaakdatumTot.SelectedDate != null)
                if (_timer != null)
                    CoolDownFillDataBase(50);
        }

        /// <summary>
        /// Change buttontexts and the rowsperpage variable when the cmbItemsPerPage selection has been changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbItemsPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnNextPage.Content = "Volgende " + cmbItemsPerPage.SelectedValue.ToString();
            btnPreviousPage.Content = "Vorige " + cmbItemsPerPage.SelectedValue.ToString();
            rowsPerPage = int.Parse(cmbItemsPerPage.SelectedValue.ToString().Replace("items", ""));
            startNumber = 0;
            if (_timer != null)
                CoolDownFillDataBase(10);
        }

        /// <summary>
        /// When the button btnPreviousPage has been clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (startNumber == 0) return; //Get out of this function if it's not neccasary to execute it

            //startnumber can't be less than 0 but stil has to refill the database if it was previously bigger than 0
            if ((startNumber - rowsPerPage) <= 0)
            {
                startNumber = 0;
                FillDataBase();
                return;
            }

            startNumber -= rowsPerPage;
            FillDataBase();
        }

        /// <summary>
        /// When the button btnNextPAge has been clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            object[] Parameterscount = new object[6];

            for (int i = 0; i < Parameterscount.Length; i++)
            {
                Parameterscount[i] = new object();
            }

            Parameterscount[0] = "%" + txtFiltVoornaam.Text + "%";
            Parameterscount[1] = "%" + txtFiltAchternaam.Text + "%";
            Parameterscount[2] = "%" + txtFiltGebruikersnaam.Text + "%";
            Parameterscount[3] = "%" + txtFiltMailadres.Text + "%";
            Parameterscount[4] = dpFiltAanmaakdatumVan.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
            Parameterscount[5] = dpFiltAanmaakdatumTot.SelectedDate.Value.Date.ToString("yyyy-MM-dd");

            searchCount = int.Parse(Classes.Database_Init.SQLQueryScaler(@"SELECT COUNT(*) countstuff FROM project p
                                                    INNER JOIN (SELECT voornaam, achternaam, tussenvoegsel, mailadres, gebruikersnaam FROM gebruiker) g ON p.gebruikersnaam = g.gebruikersnaam 
                                                    where LOWER(g.voornaam) like @0 
                                                    AND LOWER(g.achternaam) like @1 
                                                    AND LOWER(p.gebruikersnaam) like @2 
                                                    AND LOWER(g.mailadres) like @3
                                                    AND p.aangemaaktop between @4 AND @5", Parameterscount));

            //Don't have to refill database and change the startnumber if the startnumber+rowsPerPage is more than the searchCount
            if ((startNumber + rowsPerPage) >= searchCount)
            {
                return;
            }

            startNumber += rowsPerPage;
            FillDataBase();
        }

        private void btnSluiten_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void btnWijzigen_Click(object sender, RoutedEventArgs e)
        {

            if (dgProject.SelectedItem == null)
            {
                MessageBox.Show("U moet een item selecteren in de database om het item te kunnen wijzigen!");
                return;
            }

            ProjectForm prForm = new ProjectForm(dgProject.SelectedItem as Classes.Projects);
            prForm.ShowDialog();
            FillDataBase();
        }
    }
}