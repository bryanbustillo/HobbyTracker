using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//agregar la referencia en el proyecto
using System.Configuration;

namespace CapaDatos
{
    public class cdConexion
    {
        public static string conexionBD = ConfigurationManager.ConnectionStrings["cadenaConexion"].ToString();
    }
}
