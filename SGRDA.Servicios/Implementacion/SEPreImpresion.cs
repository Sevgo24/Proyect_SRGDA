using SGRDA.BL;
using SGRDA.Entities;
using SGRDA.Servicios.Contrato;
using SGRDA.Servicios.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGRDA.Servicios.Implementacion
{
    public class SEPreImpresion : ISEPreImpresion
    {
        public PreImpresion ObtenerPreImpresion(decimal idActualizar)
        {
            PreImpresion preImpr = new PreImpresion();
            BLPreImpresion srv = new BLPreImpresion();
            var obj = srv.ObtenerPreImpresion(idActualizar);

            preImpr.ID = Convert.ToInt32(obj.CodigoImpresion);
            preImpr.ID_DOCUMENTO = obj.CodigoDocumento;
            preImpr.ID_LOCAL = obj.CodigLocal;
            preImpr.ID_USUARIO = obj.CodigoUsuario;
            preImpr.FECHA_IMP = obj.FechaImp;
            preImpr.FECHA_SEL = obj.FechaSel;
            preImpr.ESTADO = obj.Estado;
            preImpr.HOSTNAME = obj.Host;

            return preImpr;
        }
        public int ActualizarEstado(decimal idActualizar, string pcLocal, string estado)
        {
            return new BLPreImpresion().ActualizarEstado(idActualizar, pcLocal, estado);
        }

         public List<PreImpresion> ListarPendientes(string locahost)
        {
            List<PreImpresion> pend = new List<PreImpresion>();
            var lista= new BLPreImpresion().Pendientes(locahost);

            foreach (var obj in lista)
            {
                PreImpresion preImpr = new PreImpresion();
                preImpr.ID = Convert.ToInt32(obj.CodigoImpresion);
                preImpr.ID_DOCUMENTO = obj.CodigoDocumento;
                preImpr.ID_LOCAL = obj.CodigLocal;
                preImpr.ID_USUARIO = obj.CodigoUsuario;
                preImpr.FECHA_IMP = obj.FechaImp;
                preImpr.FECHA_SEL = obj.FechaSel;
                preImpr.ESTADO = obj.Estado;
                preImpr.HOSTNAME = obj.Host;

                pend.Add(preImpr);
            }
            return pend;
        }

     
    }
}