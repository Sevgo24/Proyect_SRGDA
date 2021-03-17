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
    public class WorkFlowBotRadio
    {

        public bool CambiarATEstadoWFRadioAsync(string owner)
        {
            using (SGRDA.Servicios.Proxy.SEWF_RadioClient servWFradio=new SGRDA.Servicios.Proxy.SEWF_RadioClient () )
            {
                return servWFradio.CambiarATEstadoWFRadio(owner);

            }
         
        }

    }
}
