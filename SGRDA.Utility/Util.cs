using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Globalization;
using System.Reflection;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Configuration;
using SSEG.UInterfaces;
using System.Security.Cryptography;

namespace SGRDA.Utility
{
    public class Util
    {

        public static string SerializarEntity<T>(T entity)
        {
            XmlSerializer format;
            MemoryStream stream = new System.IO.MemoryStream();
            XmlDocument doc = new XmlDocument();
            string result = string.Empty;
            try
            {
                //serializa el objeto entity.                
                format = new XmlSerializer(typeof(T), "");
                format.Serialize(stream, entity);
                stream.Position = 0;
                doc.Load(stream);
                result = doc.InnerXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
            return FormatXMLString(result);
        }

        /// <summary>
        /// Deserializar un XML a un objeto T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlSerializado"></param>
        /// <returns></returns>
        public static T DeserializarTo<T>(string xmlSerializado)
        {
            try
            {
                XmlSerializer xmlSerz = new XmlSerializer(typeof(T));
                using (StringReader strReader = new StringReader(xmlSerializado))
                {
                    T obj = (T)xmlSerz.Deserialize(strReader);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Aplicar formato a documento XML
        /// </summary>
        /// <param name="sUnformattedXML"></param>
        /// <returns></returns>
        public static string FormatXMLString(string sUnformattedXML)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sUnformattedXML);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xd.WriteTo(xtw);
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
            }

            return sb.ToString();
        }

        /*Validar su un DNI es correcto, se envía por parámetro el número del Dni y el identificador
        I<PER45793939<9<<<<<<<<<<<<<<<
        */
        public static bool ValidarDni(string cDni, int nIdentificador)
        {
            bool exito = false;
            int a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0, h = 0;

            a = Convert.ToInt32(cDni.Substring(0, 1));
            b = Convert.ToInt32(cDni.Substring(1, 1));
            c = Convert.ToInt32(cDni.Substring(2, 1));
            d = Convert.ToInt32(cDni.Substring(3, 1));
            e = Convert.ToInt32(cDni.Substring(4, 1));
            f = Convert.ToInt32(cDni.Substring(5, 1));
            g = Convert.ToInt32(cDni.Substring(6, 1));
            h = Convert.ToInt32(cDni.Substring(7, 1));

            long Suma = (a * 7 + b * 3 + c * 1 + d * 7 + e * 3 + f * 1 + g * 7 + h * 3);

            string sSuma = Convert.ToString(Suma);

            if (Convert.ToInt32(sSuma.Substring(sSuma.Length - 1, 1)) == nIdentificador) exito = true; else exito = false;

            return exito;
        }




        public bool EnviarCorreo(List<string> destinos,string asunto,string cuerpo, bool isBodyHtml=false)
        {
            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            foreach (var destino in destinos)
            {
                mmsg.To.Add(destino);
            }
            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario
            //Asunto
            mmsg.Subject = asunto;
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
          //Direccion de correo electronico que queremos que reciba una copia del mensaje
          //  mmsg.Bcc.Add("destinatariocopia@servidordominio.com"); //Opcional
            //Cuerpo del Mensaje
            mmsg.Body = cuerpo;
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = isBodyHtml; //Si no queremos que se envíe como HTML
            //Correo electronico desde la que enviamos el mensaje
          var remitente=  new UIEncriptador().DesEncriptarCadena(ConfigurationManager.AppSettings["CorreoSalida"]);
          var remitentePwd=new UIEncriptador().DesEncriptarCadena(ConfigurationManager.AppSettings["PwdCorreoSalida"]);
          var HostOut = new UIEncriptador().DesEncriptarCadena(ConfigurationManager.AppSettings["HostCorreo"]);
            mmsg.From = new System.Net.Mail.MailAddress(remitente);
         
           /*-------------------------CLIENTE DE CORREO----------------------*/

            //Creamos un objeto de cliente de correo
           // System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            //cliente.Credentials = new System.Net.NetworkCredential(remitente,remitentePwd);
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient()
            {
                Credentials = new System.Net.NetworkCredential(remitente, remitentePwd),
                Host = HostOut,
                 
            };
           
            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail

            if (Convert.ToString(ConfigurationManager.AppSettings["isGmail"])=="1")
            {
                cliente.Port = 587;
                cliente.EnableSsl = true;
            }
                      
          //  cliente.Host = HostOut; //Para Gmail "smtp.gmail.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
                return true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
                return false;
            }
        }

        public string LeerPlantilla(string ruta, Dictionary<string, string> param)
        {

            string contenido = "";
            
            try
            {
                using (StreamReader sr = new StreamReader(ruta, false))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        contenido = contenido + line;
                    }
                }
                foreach (var item in param)
                {
                    contenido=contenido.Replace(item.Key, item.Value);
                }
            }
            catch (Exception)
            {
                contenido = "El archivo no se puede leer";
            }
            return contenido;


        }
        public string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return System.Text.RegularExpressions.Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     System.Text.RegularExpressions.RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (System.Text.RegularExpressions.RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
        /// <summary>
        /// VERIFICA SI EL ARCHIVO O LA RUTA ENVIADA ES UN ARCHIVO CON LE EXTENSION INDICADA
        /// </summary>
        /// <param name="rutaFile"></param>
        /// <returns></returns>
        public static bool EsExtensionPDF(string rutaFile, string extesion)
        {

            var ruta = rutaFile;
            var isPDFExt = false;
            string extPDF = "-";
            string[] spliter = ruta.Split('\\');
            if (spliter.Length > 0)
            {
                var filepdf = spliter[spliter.Length - 1];
                string[] spliterpdf = filepdf.Split('.');
                if (spliterpdf.Length > 0)
                {
                    extPDF = spliterpdf[spliterpdf.Length - 1];
                }
            }
            if (extPDF.ToUpper() == extesion.ToUpper()) { isPDFExt = true; }

            return isPDFExt;

        }
        /// <summary>
        /// OBTIENE EL NOMBRE DEL ARCHIVO SEGUN UNA RUTA ENVIADA
        /// </summary>
        /// <param name="rutaFile"></param>
        /// <returns></returns>
        public static string ObtieneNombreFile(string rutaFile)
        {

            var ruta = rutaFile;
            var nombreFile = "";
            string extPDF = "-";
            string[] spliter = ruta.Split('\\');
            if (spliter.Length > 0)
            {
                nombreFile = spliter[spliter.Length - 1];

            }
            //  if (extPDF.ToUpper() == "PDF") { isPDFExt = true; }

            return nombreFile;

        }
        public static string CleanInputB(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                var cadena = System.Text.RegularExpressions.Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     System.Text.RegularExpressions.RegexOptions.None, TimeSpan.FromSeconds(1.5));

                cadena = cadena.Replace("á", "a");
                cadena = cadena.Replace("é", "e");
                cadena = cadena.Replace("í", "i");
                cadena = cadena.Replace("ó", "o");
                cadena = cadena.Replace("ú", "u");
                cadena = cadena.Replace("Á", "A");
                cadena = cadena.Replace("É", "E");
                cadena = cadena.Replace("Í", "I");
                cadena = cadena.Replace("Ó", "O");
                cadena = cadena.Replace("Ú", "U");
                cadena = cadena.Replace("ñ", "n");
                cadena = cadena.Replace("Ñ", "N");
                cadena = cadena.Replace("(", "");
                cadena = cadena.Replace(")", "");
                cadena = cadena.Replace(" ", "_");
                cadena = cadena.ToLower();
                cadena = cadena.Trim();
                return cadena;
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (System.Text.RegularExpressions.RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        #region FACTURA TOTAL EN LETRAS

        //MONTO Y TIPO DE MONEDA
        public static string NumeroALetrasFE(string num, string TipoMoneda)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            if (decimales > 0)
                dec = " CON " + decimales.ToString() + "/100 ";
            else
                dec = " CON 00/100 ";
            //string TipoMoneda = " SOLES.";

            res = NumeroALetras(Convert.ToDouble(entero)) + dec + TipoMoneda;
            return res;
        }
        public static string NumeroALetras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            if (decimales > 0)
                dec = " CON " + decimales.ToString() + "/100";
            else
                dec = " CON 00/100";
            string TipoMoneda = " SOLES.";

            res = NumeroALetras(Convert.ToDouble(entero)) + dec+TipoMoneda;
            return res;
        }

        private static string NumeroALetras(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + NumeroALetras(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return Num2Text;
        }

        #endregion

        public static string NextInt()
        {
            int min = 1; 
            int max = 999999999;
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];

            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);

            return new Random(result).Next(min, max).ToString();
        }

        public class TipoMoneda
        {
            public const string SOLES = "PEN";
            public const string DOLARES = "44";
        }

        public class EstadosConfirmacion
        {
            public const string CONFIRMACION = "C";
            public const string SIN_CONFIRMACION = "S";
            public const string RECHAZADO = "R";
        }


    }
}
