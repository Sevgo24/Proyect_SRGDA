using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
  public  class BLAdministracion
    {

        public List<BELicencias> ListaLicenciasTrasladar(Decimal BPS_ID, decimal LIC_ID, string NOM_LIC, decimal LIC_MASTER, decimal ID_GROUP, decimal OFF_ID, decimal DIV1, decimal DIV2, decimal DIV3, decimal DIVISION,string MOD_GROUP,decimal AGE_ID, int CON_FECHA_CREACION, string FECHA_CREA_INCIAL, string FECHA_CREA_FINAL)
        {
            return new DAAdministracion().ListaLicenciasTrasladar(BPS_ID, LIC_ID, NOM_LIC, LIC_MASTER, ID_GROUP, OFF_ID, DIV1, DIV2, DIV3, DIVISION, MOD_GROUP, AGE_ID,CON_FECHA_CREACION,FECHA_CREA_INCIAL,FECHA_CREA_FINAL);
        }

        public int ActualizaEstblecimientoActivo(decimal EST_ID_Activo, decimal EST_ID_INACTIVO)
        {
            return new DAAdministracion().ActualizaEstblecimientoActivo(EST_ID_Activo, EST_ID_INACTIVO);
        }
        public int AtualizarFactPendiCancelado(List<BELicencias> listalicenciastraslado, List<BELicenciaDivisionAgente> listaAgentesSeleccionados,int FACT_PENDI, int FACT_HISTO)
        {
            string owner = GlobalVars.Global.OWNER;
            string xmlLicencia = string.Empty; //Se declara la variable xml
            string xmlAgentes = string.Empty; //Se declara la variable xml
            xmlLicencia = Utility.Util.SerializarEntity(listalicenciastraslado); //se instancia con la lista que hayamos creado

            xmlAgentes = Utility.Util.SerializarEntity(listaAgentesSeleccionados); //se instancia con la lista que hayamos creado
            return new DAAdministracion().AtualizarFactPendiCancelado(xmlLicencia, xmlAgentes,FACT_PENDI,FACT_HISTO);

        }
    }
}
