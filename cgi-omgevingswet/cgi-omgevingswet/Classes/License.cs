using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cgi_omgevingswet.Classes
{
    public class License
    {
        public int VergunningsID { get; set; }
        public string LicenseName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime RequestedOn { get; set; }
        public Status status { get; set; }
    }

    public enum Status
    {
        Goedkeuren,
        Afkeuren,
        Aangevraagd
    }
}
