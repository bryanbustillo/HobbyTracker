using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class eAnimePelicula
    {
        public int IDAnimePelicula { get; set; }
        public string nombre { get; set; }
        public bool estado { get; set; }
        public string observaciones { get; set; }
        public int cliente { get; set; }
    }
}
