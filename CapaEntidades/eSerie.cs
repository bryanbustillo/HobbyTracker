using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class eSerie
    {
        public int IDSerie { get; set; }
        public string nombre { get; set; }
        public bool estado { get; set; }
        public string registro { get; set; }
        public int cliente { get; set; }
    }
}
