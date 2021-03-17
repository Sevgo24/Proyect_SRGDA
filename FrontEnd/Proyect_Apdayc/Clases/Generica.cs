using SGRDA.BL.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases
{
    public static class Generica
    {

        /// <summary>
        /// VALIDA SI LA ACCION A EJECUTAR  ESTA CONFIGURADA
        /// PARA QUE EL USUARIO LOGEADO PUEDA REALIZAR LA ACCION
        /// DBS - 20150320
        /// </summary>
        /// <param name="idRol"></param>
        /// <param name="idAcction"></param>
        /// <returns></returns>
        public static bool hasAccess(decimal idRol, decimal idAction)
        {
            BL_WORKF_AGENTS servicio = new BL_WORKF_AGENTS();
            return servicio.TieneRol(GlobalVars.Global.OWNER, idRol, idAction);

        }
    }
}