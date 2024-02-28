using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class eConcierto
    {
        public int IDConcierto { get; set; }
        public string concierto { get; set; }
        public string lugar { get; set; }        
        public string fecha { get; set; }
        public bool estado { get; set; }
        public string observaciones { get; set; }
        public int cliente { get; set; }
    }
}
