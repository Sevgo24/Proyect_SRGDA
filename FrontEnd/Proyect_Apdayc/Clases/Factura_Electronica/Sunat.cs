using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Proyect_Apdayc.Clases.Factura_Electronica
{
    public class Sunat
    {

        public class Variables
        {

            public const int SunatService = 1;

            public const int ExternalService = 2;
        }

        public class Myclass2
        {
            public string status { get; set; }
            public string msg { get; set; }
            public string ruc { get; set; }
            public string razonSocial { get; set; }
            public string direccion { get; set; }
            public string ubigeo { get; set; }

            public int tipo { get; set; } // variable para controlar de donde viene 
        }

        public class Myclass
        {
            public string ruc { get; set; }
            public string razon_social { get; set; }
            public string ciiu { get; set; }
            public string fecha_actividad { get; set; }
            public string contribuyente_condicion { get; set; }
            public string contribuyente_tipo { get; set; }
            public string contribuyente_estado { get; set; }
            public string nombre_comercial { get; set; }
            public string fecha_inscripcion { get; set; }
            public string domicilio_fiscal { get; set; }
            public string sistema_emision { get; set; }
            public string sistema_contabilidad { get; set; }
            public string actividad_exterior { get; set; }
            public string emision_electronica { get; set; }
            public string fecha_inscripcion_ple { get; set; }
            public string Oficio { get; set; }
            public string fecha_baja { get; set; }

            public int tipo { get; set; }

            public string UBIGEO { get; set; }
        }
        public Myclass ValidarRUC_Sunat(string RUC)
        {
            Myclass resultJson = new Myclass();
            try
            {

                var result = "";
                //"10700377811"

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.sunat.cloud/ruc/" + RUC);
                //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ruc.com.pe/api/v1/ruc " + RUC);

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "POST";


                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                resultJson = JsonConvert.DeserializeObject<Myclass>(result);
                resultJson.tipo = Variables.SunatService;
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
            }
            return resultJson;
        }

        public Myclass2 ValidarRUC_Sunat2(string RUC)
        {
            Myclass2 resultJson2 = new Myclass2();

            try
            {

                var result = "";
                //"10700377811"
                var httpWebRequest2 = (HttpWebRequest)WebRequest.Create("http://services.wijoata.com/consultar-ruc/api/ruc/" + RUC);

                httpWebRequest2.ContentType = "application/json";
                httpWebRequest2.Accept = "application/json";
                httpWebRequest2.Method = "POST";

                var httpResponse = (HttpWebResponse)httpWebRequest2.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                resultJson2 = JsonConvert.DeserializeObject<Myclass2>(result);
                resultJson2.tipo = Variables.ExternalService;

            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
            }
            return resultJson2;
        }

        public class MyClassDNI
        {
            public string Nombre_Completo { get; set; }
            public string Apellido_Paterno { get; set; }
            public string Apellido_Materno { get; set; }
        }
        public MyClassDNI ValidarDNI_Sunat(string DNI)
        {
            MyClassDNI resultJson = new MyClassDNI();
            try
            {
                var result = "";
                //"10700377811"
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://aplicaciones007.jne.gob.pe/srop_publico/Consulta/Afiliado/GetNombresCiudadano?DNI=" + DNI);
                //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://services.wijoata.com/consultar-ruc/api/ruc/" + RUC);
                //httpWebRequest.ContentType = "application/json";
                //httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                String[] parts = result.Split('|');
                resultJson.Nombre_Completo = parts[2].ToString();
                resultJson.Apellido_Paterno = parts[0].ToString();
                resultJson.Apellido_Materno = parts[1].ToString();

            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
            }
            return resultJson;
        }


    }
}