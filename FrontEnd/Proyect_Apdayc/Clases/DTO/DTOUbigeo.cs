using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOUbigeo
    {

        public decimal Codigo { get; set; }
        public string Descripcion { get; set; }
        /// <summary>
        /// propiedad para el autocompletar
        /// </summary>
        public string value { get; set; }

    }
}