using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class eCategoriaPelicula
    {
        public int IDCategoria { get; set; }
        public string descripcion { get; set; }
        public bool activa { get; set; }
        public int cliente { get; set; }
    }
}
