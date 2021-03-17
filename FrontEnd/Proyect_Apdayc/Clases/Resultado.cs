using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Clases
{
    public class Resultado
    {
        /// <summary>
        /// COdigo personalizado de retorno para mostrar al usuario
        /// </summary>
        public int TotalFacturas { get; set; }
        public int Code { get; set; }

        public int TotalFacturasValidas { get; set;}
        /// <summary>
        /// Contiene el resultado de la accion 1= exito,  0= error , 2= sesion expirada, 3=no found data
        /// </summary>
        public int result { get; set; }
        /// <summary>
        /// Propiedad para personalizar el mensaje a enviar al usuario desde la controladora.
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Lista de objetos serializados para su manipulacion con Jquery
        /// </summary>
        public JsonResult data { get; set; }

        /// <summary>
        /// Valor para cualquier dato de retorno tipo scalar.
        /// </summary>
        public string valor { get; set; }

        public string valor2 { get; set; }

        public string valor3 { get; set; }

        /// <summary>
        /// Propiedad agregada para la Integracion con SIS SEGURIDAD
        /// isRedirect: indicador si se debe redireccionar al login la pagina.
        /// </summary>
        public bool isRedirect { get; set; }
        /// <summary>
        /// Propiedad agregada para la Integracion con SIS SEGURIDAD
        /// redirectUrl: Url a donde se redireccionara la pagina.
        /// </summary>
        public string redirectUrl { get; set; }
        


    }
}