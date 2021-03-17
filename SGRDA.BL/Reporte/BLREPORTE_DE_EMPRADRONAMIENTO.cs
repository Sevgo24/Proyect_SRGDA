using SGRDA.DA.Reporte;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.BL.Reporte
{
    public class BLREPORTE_DE_EMPRADRONAMIENTO
    {
        public List<BEREPORTE_DE_EMPRADRONAMIENTO> ObtenerDatosREPORTE_DE_EMPRADRONAMIENTO(int MES,int ANIO,int ID_OFICINA,int oficina_id)
        {
            return new DAREPORTE_DE_EMPRADRONAMIENTO().ObtenerDatosREPORTE_DE_EMPRADRONAMIENTO(MES,ANIO, ID_OFICINA, oficina_id);
        }
        public List<Be_BecsEspeciales> ListarAniosCierre()
        {
            return new DAREPORTE_DE_EMPRADRONAMIENTO().ListarAniosCierre();
        }
        public List<Be_BecsEspeciales> ListarMesesCierre(int ANIO)
        {
            return new DAREPORTE_DE_EMPRADRONAMIENTO().ListarMesesCierre(ANIO);
        }
        public List<BEREPORTE_DE_EMPRADRONAMIENTO> ObtenerDatosREPORTE_DE_GESTION_EMPADRONAMIENTO(string finicio, string ffin, int ID_OFICINA, int oficina_id, string TIPO_PAGO)
        {
            return new DAREPORTE_DE_EMPRADRONAMIENTO().ObtenerDatosREPORTE_DE_GESTION_EMPADRONAMIENTO(finicio, ffin, ID_OFICINA, oficina_id, TIPO_PAGO);
        }
    }
}
