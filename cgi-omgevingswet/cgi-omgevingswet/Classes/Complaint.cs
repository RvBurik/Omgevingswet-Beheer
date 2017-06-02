using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cgi_omgevingswet.Classes
{
    public class Complaint
    {
        public string Gebruikersnaam { get; set; }
        public int ProjectID { get; set; }
        public int VergunningsID { get; set; }
        public string Bezwaarreden { get; set; }
        public string Besluit { get; set; }
        public string Besluittreden { get; set; }
    }
}
