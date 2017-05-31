﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cgi_omgevingswet.Classes
{
    /// <summary>
    /// Class used in filling the database
    /// </summary>
    public class Projects
    {
        public int ProjectID { get; set; }
        public string Gebruikersnaam { get; set; }
        public DateTime Aangemaaktop { get; set; }
        public string Werkzaamheid { get; set; }
        public string Volledigenaam { get; set; }
        public string Voornaam { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public string Mailadres { get; set; }
        public string Bedrijfsnaam { get; set; }
    }
}
