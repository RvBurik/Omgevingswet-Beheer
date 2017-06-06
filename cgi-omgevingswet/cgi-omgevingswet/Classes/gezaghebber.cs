using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cgi_omgevingswet.Classes
{
    public class Gezaghebber
    {
        public string Gebruikersnaam { get; set; }
        public string Voornaam { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public string Mailadres { get; set; }
        public int ProjectID { get; set; }
        public string Rolnaam = "GEZAGHEBBER";
        public string DatumAanvraag { get; set; }
        public string DatumUitgifte { get; set; }
        public byte AutomatischToegevoegd = 0;
    }
}
