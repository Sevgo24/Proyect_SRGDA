using SGRDA.DA.Reporte;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.BL.Reporte
{
    public class BL_BecEspeciales
    {
       public List<Be_BecsEspeciales> ListarDatosBesEspeciales(int cant, int mes, int anio,int oficina_id)
        {
            return new DA_BecsEspeciales().ListarDatosBesEspeciales(cant, mes, anio, oficina_id);
        }
        public List<Be_BecsEspeciales> ListarDatosBesEspecialesResumen(int cant, int mes, int anio,int oficina_id)
        {
            return new DA_BecsEspeciales().ListarDatosBesEspecialesResumen(cant, mes, anio, oficina_id);
        }
        public List<Be_BecsEspeciales> ListarAniosCierre()
        {
            return new DA_BecsEspeciales().ListarAniosCierre();
        }
        public List<Be_BecsEspeciales> ListarMesesCierre(int ANIO)
        {
            return new DA_BecsEspeciales().ListarMesesCierre(ANIO);
        }

    }
}
