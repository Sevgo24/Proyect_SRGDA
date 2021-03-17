using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLAdministracionMatrizLicencia
    {
        public List<BEAdministracionMatrizLicencia> lista (int skip, int take, int page, int pageSize, decimal LIC_ID, decimal BPS_ID, string RAZ_SOC, string NUM_IDE, string NOM_SOC, string APE_SOC, string MAT_SOC, string EST_NAM, string MOG_ID, int CON_FEC, string FEC_INI, string FEC_FIN, decimal DIV_ID, decimal DEP_ID, decimal PROV_ID, decimal DIST_ID, decimal OFF_ID, int ESTADO_LIC, int ESTADO_LIC_FACT,int ESTADO_PL_BLOQ,decimal CODIGO_AGENTE,int OPCION,string FEC_INI_BUS,string FEC_FIN_BUS)
        {
            return new DAAdministracionMatrizLicencia().listarLicencia(skip, take, page, pageSize, LIC_ID, BPS_ID, RAZ_SOC, NUM_IDE, NOM_SOC, APE_SOC, MAT_SOC, EST_NAM, MOG_ID, CON_FEC, FEC_INI, FEC_FIN, DIV_ID, DEP_ID, PROV_ID, DIST_ID, OFF_ID, ESTADO_LIC, ESTADO_LIC_FACT, ESTADO_PL_BLOQ, CODIGO_AGENTE, OPCION, FEC_INI_BUS, FEC_FIN_BUS);
        }
        public List<BEAdministracionMatrizLicencia> listaHuecos(int skip, int take, int page, int pageSize, decimal LIC_ID, decimal BPS_ID, string RAZ_SOC, string NUM_IDE, string NOM_SOC, string APE_SOC, string MAT_SOC, string EST_NAM, string MOG_ID, int CON_FEC, string FEC_INI, string FEC_FIN, decimal DIV_ID, decimal DEP_ID, decimal PROV_ID, decimal DIST_ID, decimal OFF_ID, int ESTADO_LIC, int ESTADO_LIC_FACT, int ESTADO_PL_BLOQ, decimal CODIGO_AGENTE, int OPCION, string FEC_INI_BUS, string FEC_FIN_BUS)
        {
            return new DAAdministracionMatrizLicencia().listaHuecos(skip, take, page, pageSize, LIC_ID, BPS_ID, RAZ_SOC, NUM_IDE, NOM_SOC, APE_SOC, MAT_SOC, EST_NAM, MOG_ID, CON_FEC, FEC_INI, FEC_FIN, DIV_ID, DEP_ID, PROV_ID, DIST_ID, OFF_ID, ESTADO_LIC, ESTADO_LIC_FACT, ESTADO_PL_BLOQ, CODIGO_AGENTE, OPCION, FEC_INI_BUS, FEC_FIN_BUS);
        }

        public List<BEAdministracionMatrizLicencia> listaLicenciasValidacionMensual(int skip, int take, int page, int pageSize, decimal LIC_ID, decimal BPS_ID, string RAZ_SOC, string NUM_IDE, string NOM_SOC, string APE_SOC, string MAT_SOC, string EST_NAM, string MOG_ID, int CON_FEC, string FEC_INI, string FEC_FIN, decimal DIV_ID, decimal DEP_ID, decimal PROV_ID, decimal DIST_ID, decimal OFF_ID, int ESTADO_LIC, int ESTADO_LIC_FACT, int ESTADO_PL_BLOQ, decimal CODIGO_AGENTE, int OPCION, string FEC_INI_BUS, string FEC_FIN_BUS)
        {
            return new DAAdministracionMatrizLicencia().listaLicenciasValidacionMensual(skip, take, page, pageSize, LIC_ID, BPS_ID, RAZ_SOC, NUM_IDE, NOM_SOC, APE_SOC, MAT_SOC, EST_NAM, MOG_ID, CON_FEC, FEC_INI, FEC_FIN, DIV_ID, DEP_ID, PROV_ID, DIST_ID, OFF_ID, ESTADO_LIC, ESTADO_LIC_FACT, ESTADO_PL_BLOQ, CODIGO_AGENTE, OPCION, FEC_INI_BUS, FEC_FIN_BUS);
        }
        public List<BEAdministracionMatrizLicencia> listaLicenciasValidacionMensualPaso(int skip, int take, int page, int pageSize, decimal LIC_ID, decimal BPS_ID, string RAZ_SOC, string NUM_IDE, string NOM_SOC, string APE_SOC, string MAT_SOC, string EST_NAM, string MOG_ID, int CON_FEC, string FEC_INI, string FEC_FIN, decimal DIV_ID, decimal DEP_ID, decimal PROV_ID, decimal DIST_ID, decimal OFF_ID, int ESTADO_LIC, int ESTADO_LIC_FACT, int ESTADO_PL_BLOQ, decimal CODIGO_AGENTE, int OPCION, string FEC_INI_BUS, string FEC_FIN_BUS)
        {
            return new DAAdministracionMatrizLicencia().listaLicenciasValidacionMensualPaso(skip, take, page, pageSize, LIC_ID, BPS_ID, RAZ_SOC, NUM_IDE, NOM_SOC, APE_SOC, MAT_SOC, EST_NAM, MOG_ID, CON_FEC, FEC_INI, FEC_FIN, DIV_ID, DEP_ID, PROV_ID, DIST_ID, OFF_ID, ESTADO_LIC, ESTADO_LIC_FACT, ESTADO_PL_BLOQ, CODIGO_AGENTE, OPCION, FEC_INI_BUS, FEC_FIN_BUS);
        }

        public int ObtieneValidacionOficinaPadre(decimal CodigoOficina)
        {
            return new DAAdministracionMatrizLicencia().ObtieneValidacionOficinaPadre(CodigoOficina);
        }
    }
}
