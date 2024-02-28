using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class eVinilo
    {
        public int IDVinilo { get; set; }
        public string vinilo { get; set; }
        public string banda { get; set; }
        public bool estado { get; set; }
        public string observaciones { get; set; }
        public int cliente { get; set; }
    }
}
