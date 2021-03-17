using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLAdministracionSeguimientoLocalPermanente
    {
        public List<BEAdministracionSeguimientoLocalPermanente> ListarLicenciaSeguimiento(string anio,decimal CodigoOficina,string CodigoModalidad,int MesEvaluar)
        {
            return new DAAdministracionSeguimientoLocalPermanente().listarLicenciaSeguimiento(anio,CodigoOficina,CodigoModalidad,MesEvaluar);
        }
        public decimal Recuperar_Lic_PL_ID(decimal lic_id,string anio,int MesEvaluar)
        {
            return new DAAdministracionSeguimientoLocalPermanente().Recuperar_Lic_PL_ID(lic_id,anio, MesEvaluar);
        }
    }
}
