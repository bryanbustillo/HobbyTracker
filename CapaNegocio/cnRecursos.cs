using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using CapaEntidades;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace CapaNegocio
{
    public class cnRecursos
    {
        //Encriptar contraseña
        public static string encriptarContrasena(string texto)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }

        }

        //método para generar una contraseña aleatoria de 6 dígitos
        public static string generarContrasena()
        {
            string contrasena = Guid.NewGuid().ToString("N").Substring(0, 6);
            return contrasena;
        }

        //método para enviar al correo la contraseña del cliente 
        public static bool enviarContrasenaCorreo(string email, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("hobbytrackerapp@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("hobbytrackerapp@gmail.com", "enkm onlh nnra tbao"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                };

                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;
        }

    }
}
