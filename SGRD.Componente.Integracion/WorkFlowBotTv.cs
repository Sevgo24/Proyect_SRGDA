using SGRDA.Servicios.Proxy.Entidad;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SGRD.Componente.Integracion
{
    public class WorkFlowBotTv
    {

        /// <summary>
        /// Ejecuta y busa deudas de licencias de tv para cambiar de estado a moroso
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public bool CambiarATEstadoWFTV(string owner)
        {
            using (SGRDA.Servicios.Proxy.SEWF_TvCableClient servWFradio = new SGRDA.Servicios.Proxy.SEWF_TvCableClient())
            {
                return servWFradio.CambiarEstadoAutomatico(owner);

            }
         
        }

    }
}
