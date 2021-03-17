using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA.WorkFlow;
using SGRDA.Entities.WorkFlow;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_OBJECTS
    {
        public WORKF_OBJECTS ObtenerObjectsXActions(string owner, decimal wrkfaid)
        {
            var obj = new DA_WORKF_OBJECTS().ObtenerObjectsXActions(owner, wrkfaid);

            return obj;
        }
        public WORKF_OBJECTS ObtenerObjects(string owner, decimal? wrkfoid)
        {
            var obj = new DA_WORKF_OBJECTS().ObtenerObjects(owner, wrkfoid);

            if (obj != null)
            {
                var objTO = new DA_WORKF_OBJECTS_TYPE().obtener(owner, Convert.ToDecimal(obj.WRKF_OTID));
                obj.TipoObjeto = objTO;
            }

            return obj;
        }

        public List<WORKF_OBJECTS> Listar(string owner, string nombre, string codInterno, decimal idTipoObjeto, int estado, int pagina, int cantRegxPag)
        {
            return new DA_WORKF_OBJECTS().Listar(owner, nombre, codInterno, idTipoObjeto, estado, pagina, cantRegxPag);
        }
        public decimal Eliminar(WORKF_OBJECTS entidad)
        {
            return new DA_WORKF_OBJECTS().Eliminar(entidad);
        }
        public decimal Insertar(WORKF_OBJECTS entidad)
        {
            return new DA_WORKF_OBJECTS().Insertar(entidad);
        }
        public decimal Actualizar(WORKF_OBJECTS entidad)
        {
            return new DA_WORKF_OBJECTS().Actualizar(entidad);
        }
        public List<WORKF_OBJECTS> ListarObjetosParametros(string owner, decimal wrkfId, decimal wrkfsId)
        {
            return new DA_WORKF_OBJECTS().ListarObjetosParametros(owner, wrkfId, wrkfsId);
        }
        public int ObtenerPlantilla(decimal idModalidad)
        {
            return new DA_WORKF_OBJECTS().ObtenerPlantilla(idModalidad);
        }
    }
}
