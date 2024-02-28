using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class eCliente
    {
        public int IDCliente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string contrasena { get; set; }
        public bool restablecerContrasena { get; set; }
        public string confirmarContrasena { get; set; }
    }
}
