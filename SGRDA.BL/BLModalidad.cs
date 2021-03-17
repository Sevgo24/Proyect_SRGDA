using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;


namespace SGRDA.BL
{
    public class BLModalidad
    {

        public List<BEModalidad> Listar(BEModalidad modalidad, int pagina, int cantRegxPag, decimal idOficina, string UsuarioActual)
        {
            return new DAModalidad().Listar(modalidad, pagina, cantRegxPag, idOficina, UsuarioActual);
        }

        public int Insertar(BEModalidad modalidad)
        {
            return new DAModalidad().Insertar(modalidad);
        }

        public int Eliminar(BEModalidad modalidad)
        {
            return new DAModalidad().Eliminar(modalidad);
        }

        public BEModalidad Obtener(string owner, decimal id)
        {
            return new DAModalidad().Obtener(owner, id);
        }

        public int Update(BEModalidad modalidad)
        {
            return new DAModalidad().Update(modalidad);
        }

        public List<BEModalidad> ListarReporte(BEModalidad modalidad)
        {
            return new DAModalidad().ListarReporte(modalidad);
        }

        public BEModalidad ObtenerCodigosDatosModalidad(string owner, decimal id)
        {
            return new DAModalidad().ObtenerCodigosDatosModalidad(owner, id);
        }

        public List<BEModalidad> ListarGrupoModXOficina(int? ID_OFF)
        {
            return new DAModalidad().ListarGrupoModXOficina(ID_OFF);
        }

        public List<BEModalidad> ListaModalidadPorGrupoDropDownList(string parametros)
        {
            return new DAModalidad().ListaModalidadPorGrupoDropDownList(parametros);
        }
        
    }
}
