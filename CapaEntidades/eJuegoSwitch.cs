using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class eJuegoSwitch
    {
        public int IDJuegoSwitch { get; set; }
        public string juego { get; set; }       
        public int formato { get; set; }
        public int progreso { get; set; }
        public bool estado { get; set; }
        public string observaciones { get; set; }
        public int cliente { get; set; }
    }
}
