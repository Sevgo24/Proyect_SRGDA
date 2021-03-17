using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities.Reporte;
using SGRDA.DA.Reporte;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.BL.Reporte
{
    public class BLReporte
    {
        public BELicencia ObtenerDatosLicencia(string owner, decimal idLic)
        {
            return new DAReporte().ObtenerDatosLicencia(owner, idLic);
        }

        public BEUbigeoRpt ObtenerUbigeo(string owner, string Tins, string dadvid)
        {
            return new DAReporte().ObtenerUbigeo(owner, Tins, dadvid);
        }

        public BELicencia ValorUnidadMusical(string owner)
        {
            return new DAReporte().ValorUnidadMusical(owner);
        }

        public List<BELicencia> NivelIncidenciaMusical(string owner)
        {
            return new DAReporte().NivelIncidenciaMusical(owner);
        }

        public BELicencia ObtenerDatosDireccionCobranza(string owner, decimal idLic)
        {
            return new DAReporte().ObtenerDatosDireccionCobranza(owner, idLic);
        }

        public string FechaCartaInformativa(string owner, decimal idLicencia)
        {
            return new DAReporte().FechaCartaInformativa(owner, idLicencia);
        }

        public string FechaCartaReiterativa(string owner, decimal idLicencia)
        {
            return new DAReporte().FechaCartaReiterativa(owner, idLicencia);
        }

        public string FechaCartaRequerimientoAutorizacion(string owner, decimal idLicencia)
        {
            return new DAReporte().FechaCartaRequerimientoAutorizacion(owner, idLicencia);
        }

        public string FechaCartaVerificaionUsoAutorizacionObrasMusicales(string owner, decimal idLicencia)
        {
            return new DAReporte().FechaCartaVerificaionUsoAutorizacionObrasMusicales(owner, idLicencia);
        }

        public string FechaActualShort()
        {
            return new DAReporte().FechaActualShort();
        }

        public List<BEModalidadIncidencia> ListarTipo(string owner)
        {
            return new DAReporte().ListarTipo(owner);
        }

        public BEApoderadoLegal ObtenerDatosApoderadoLegal(string owner, decimal idLic)
        {
            return new DAReporte().ObtenerDatosApoderadoLegal(owner, idLic);
        }

        public BEEstablecimientos ObtenerDatosEstablecimiento(string owner, decimal idLic)
        {
            return new DAReporte().ObtenerDatosEstablecimiento(owner, idLic);
        }

        public BEUsuarioDerecho ObtenerUsuarioDerecho(string owner, decimal idLic)
        {
            return new DAReporte().ObtenerUsuarioDerecho(owner, idLic);
        }

        public BEAgenteRecaudo ObtenerAgenteRecaudo(string owner, decimal idLic)
        {
            return new DAReporte().ObtenerAgenteRecaudo(owner, idLic);
        }

        public BERepresentanteLegal ObtenerRepresentanteLegal(string owner, decimal idLic)
        {
            return new DAReporte().ObtenerRepresentanteLegal(owner, idLic);
        }

        public BEUsuarioDerecho ObtenerPartidaZonaSedeUsuarioDerecho(string owner, decimal idLic)
        {
            return new DAReporte().ObtenerPartidaZonaSedeUsuarioDerecho(owner, idLic);
        }

        public BEOficina ObtenerDatosOficina(string owner, decimal idLic)
        {
            return new DAReporte().ObtenerDatosOficina(owner, idLic);
        }

        public List<string> ObtenerArtistas(string owner, decimal idLic)
        {
            return new DAReporte().ObtenerArtistas(owner, idLic);
        }

        /*
         * addon dbs-faltó paametro de id de reporte 20151224
         */
        public BEPlanilla ObtenerDatosPlanilla(string owner, decimal idLic, decimal idReport)
        {
            return new DAReporte().ObtenerDatosPlanilla(owner, idLic, idReport);
        }

        public BEAutoridadPrincipal ObtenerAutoridadPrincipal(string owner, decimal idLic)
        {
            return new DAReporte().ObtenerAutoridadPrincipal(owner, idLic);
        }

        public BEParametro ObtenerParametro(string owner, decimal idLic, decimal varId)
        {
            return new DAReporte().ObtenerParametro(owner, idLic, varId);
        }

        public List<BEPeriodoDeuda> ListaPeriodoDeuda(string owner, decimal idLic)
        {
            return new DAReporte().ListaPeriodoDeuda(owner, idLic);
        }

        public int ObtenerEstadoTrace(string owner, decimal idTrace)
        {
            return new DAReporte().ObtenerEstadoTrace(owner, idTrace);
        }

        public decimal ObtenerPrerequisito(string owner, decimal idLicencia, decimal idTrace)
        {
            return new DAReporte().ObtenerPrerequisito(owner, idLicencia, idTrace);
        }

        public WORKF_TRACES ObtenerFechaCarta(string owner, decimal idPrerequisito, decimal estado)
        {
            return new DAReporte().ObtenerFechaCarta(owner, idPrerequisito, estado);
        }

        public decimal TotalDeudaFactura(string owner, decimal idLicencia)
        {
            return new DAReporte().TotalDeudaFactura(owner, idLicencia);
        }

        public BEContacto ObtenerContacto(string owner, decimal idLicencia)
        {
            return new DAReporte().ObtenerContacto(owner, idLicencia);
        }
        #region cadenas

        public BELicenciaPlaneamiento ObtenerPeriodoMinimo(decimal idlicencia)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAReporte().obtenerperiodominimo(owner,idlicencia);
        }
        public BELicenciaPlaneamiento ObtenerPeriodoMaximo(decimal idlicencia)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAReporte().obtenerperiodomaximo(owner, idlicencia);
        }

        public List<BEAsociado> AsociadoXSocio(decimal idbps)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAReporte().AsociadoXSocio(idbps, owner);
        }

        public List<BEPlanilla> ObtieneFacturasxIdReporte(decimal idreporte)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAReporte().ObtieneFacturasxIdReporte(idreporte, owner);
        }

        public BEPlanilla ObtieneDescripcionxModalidad(String MOG_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAReporte().ObtieneDescripcionxModalidad(MOG_ID, owner);
        }

        public BEPlanilla ObtieneDescripcionxModalidad2(String MOG_ID,decimal MOD_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAReporte().ObtieneDescripcionxModalidad2(MOG_ID, MOD_ID, owner);
        }

        public BEPlanilla ObtieneDescripcionxModalidad3(String MOG_ID,decimal MOD_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAReporte().ObtieneDescripcionxModalidad3(MOG_ID, MOD_ID, owner);
        }

        public string ObtieneDescripcionEstadoLicencia(decimal LIC_ID)
        {
            string OWNER = GlobalVars.Global.OWNER;

            return new DAReporte().ObtieneDescripcionEstadoLicencia(LIC_ID, OWNER);
        }
        #endregion

        #region QR
        public string ObtieneInfoxLicencia(decimal LIC_ID)
        {
            return new DAReporte().ObtieneInfoxLicencia(LIC_ID);
        }


        #endregion

        #region MONTO LICENCIAS
        public decimal ObtieneMontoLicencias(decimal LIC_ID)
        {
            return new DAReporte().ObtieneMontoLicencias(LIC_ID);
        }

        public string ObtienAgenciasLicencia(decimal LIC_ID,int TIPO)
        {
            return new DAReporte().ObtienAgenciasLicencia(LIC_ID, TIPO);
        }
        #endregion

    }
}
