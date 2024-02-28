using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class ePelicula
    {
        public int IDPelicula { get; set; }
        public string nombre { get; set; }
        public eCategoriaPelicula oCategoria { get; set; }
        public bool vista { get; set; }
        public string observaciones { get; set; }
        public int cliente { get; set; }
    }
}
