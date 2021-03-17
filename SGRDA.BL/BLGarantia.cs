using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLGarantia
    {
        public List<BEGarantia> ListarGarantia(string owner, decimal idLic)
        {
            return new DAGarantia().ListarGarantia(owner, idLic);
        }
        public int Activar(string owner, decimal idGarantia, string usu)
        {
            return new DAGarantia().Activar(owner, idGarantia, usu);
        }
        public int Eliminar(string owner, decimal idGarantia, string usu)
        {
            return new DAGarantia().Eliminar(owner, idGarantia, usu);
        }
        public int Actualizar(string owner, decimal idGarantia, decimal idLic, decimal valor, string moneda, string tipo, string numero, string entidad, DateTime rFecha, DateTime? dFecha, decimal? aValor, decimal? dValor, DateTime? tFecha, string usu)
        {
            return new DAGarantia().Actualizar(owner, idGarantia, idLic, valor, moneda, tipo, numero, entidad, rFecha, dFecha, aValor, dValor, tFecha, usu);
        }
        public int Insertar(string owner, decimal idLic, decimal valor, string moneda, string tipo, string numero, string entidad, DateTime rFecha, DateTime? dFecha, decimal? aValor, decimal? dValor, DateTime? tFecha, string usu)
        {
            return new DAGarantia().Insertar(owner, idLic, valor, moneda, tipo, numero, entidad, rFecha, dFecha, aValor, dValor, tFecha, usu);
        }
        public BEGarantia ObtenerGarantiaXCod(string owner, decimal idGarantia, decimal idLic)
        {
            return new DAGarantia().ObtenerGarantiaXCod(owner, idGarantia, idLic);
        }
        public int Devolver(string owner, decimal idGarantia, decimal idLic,  DateTime dFecha,  string usu)
        {
            return new DAGarantia().ActualizarDevolucion(owner, idGarantia, idLic, dFecha, usu);
        }
    }
}
