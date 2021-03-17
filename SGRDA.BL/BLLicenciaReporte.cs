using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLLicenciaReporte
    {
        public int Insertar(BELicenciaReporte entidad)
        {
            return new DALicenciaReporte().Insertar(entidad);
        }

        public int Actualizar(BELicenciaReporte entidad)
        {
            return new DALicenciaReporte().Actualizar(entidad);
        }

        public List<BELicenciaReporte> ListarPorLicencia(string Owner, decimal IdLicencia, string ModUso, int anio, int mes)
        {
            return new DALicenciaReporte().ListarPorLicencia(Owner, IdLicencia, ModUso, anio, mes);
        }

        public BELicenciaReporte Obtener(string Owner, decimal Id)
        {
            return new DALicenciaReporte().Obtener(Owner, Id);
        }

        public int Eliminar(BELicenciaReporte entidad)
        {
            return new DALicenciaReporte().Eliminar(entidad);
        }
        public int Activar(string owner, decimal id, string usuModi)
        {
            return new DALicenciaReporte().Activar(owner, id, usuModi);
        }

        public BELicenciaReporte ObtenerSeriePlanilla(string Owner, decimal IdLic, decimal idReport)
        {
            return new DALicenciaReporte().ObtenerSeriePlanilla(Owner, IdLic, idReport);
        }

        public int ActualizarEstadoImpresion(string owner, decimal? idReport)
        {
            return new DALicenciaReporte().ActualizarEstadoImpresion(owner, idReport);
        }

        public BELicenciaReporte ObtenerEstadoImpresion(string owner, decimal idReport)
        {
            return new DALicenciaReporte().ObtenerEstadoImpresion(owner, idReport);
        }

        public BELicenciaReporte ObtenerFacturaNotacreditoAnulada(string owner, decimal idLic, decimal idPerFac)
        {
            return new DALicenciaReporte().ObtenerFacturaNotacreditoAnulada(owner, idLic, idPerFac);
        }

        public int ActualizarNroImpresion(string owner, decimal idReport, string user)
        {
            return new DALicenciaReporte().ActualizarNroImpresion(owner, idReport, user);
        }


        #region plantilla

        public int InsertarPlanillaXML(List<BELicenciaPlaneamientoDetalle> lista)
        {
            string owner = GlobalVars.Global.OWNER;
            string listaxml = Utility.Util.SerializarEntity(lista);

            return new DALicenciaReporte().InsertarPlanillaXML(owner, listaxml);

        }
        #endregion


        public List<BELicenciaReporte> ListarBandejaPlanilla(decimal ID_OFICINA, decimal ID_DIVISION, string GRUPO_MODALIDAD,
                                                            decimal LIC_ID, decimal ID_SOCIO, string FEC_INI, string FEC_FIN, int ESTADO)
        {
            return new DALicenciaReporte().ListarBandejaPlanilla(ID_OFICINA, ID_DIVISION, GRUPO_MODALIDAD, LIC_ID, ID_SOCIO, FEC_INI, FEC_FIN, ESTADO);
        }


        public  int  GenerarPLanillasPendientes (List<BELicenciaReporte> ListaPlanillasPendientes, string usuarioCrea)
        {
            int result = 0;
            string xmlPlanillasPendientes = string.Empty;
            xmlPlanillasPendientes = Utility.Util.SerializarEntity(ListaPlanillasPendientes);
            result = new DALicenciaReporte().GenerarPlanillaoXML(xmlPlanillasPendientes, usuarioCrea);
            return result;
        }


    }
}
