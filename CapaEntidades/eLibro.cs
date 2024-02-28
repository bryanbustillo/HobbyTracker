using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class eLibro
    {
        public int IDLibro { get; set; }
        public string nombre { get; set; }
        public string autor { get; set; }
        public string editorial { get; set; }
        public bool leido { get; set; }
        public int cliente { get; set; }
    }
}
