using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Documento
{
    public class PlantillaCorreo
    {


        #region "Plantillas Correos - Proceso Licenciamiento"

        public string crearContenidoAceptacion(string rutaPlantilla, decimal idLicencia)
        {
            string contenido;
            try
            {

                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("@usuario", "Miguel angel lopez");
                parametros.Add("@firma", "Gestor de Eventos");
                contenido = LeerPlantilla(rutaPlantilla, parametros);

            }
            catch (Exception ex)
            {
                contenido = "-1";
            }
            return contenido;
        }

        #endregion

        #region "Plantillas Correos - otros procesos"

        #endregion


        public string LeerPlantilla(string ruta, Dictionary<string, string> param)
        {

            string contenido = "";

            //try
            //{
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
                    contenido = contenido.Replace(item.Key, item.Value);
                }
            //}
            //catch (Exception)
            //{
            //    contenido = "El archivo no se puede leer";
            //}
            return contenido;


        }
         
    }
}
