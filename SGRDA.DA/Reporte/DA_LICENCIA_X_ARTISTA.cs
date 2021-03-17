using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.DA.Reporte
{
    public class DA_LICENCIA_X_ARTISTA
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");
        public List<BE_LICENCIA_X_ARTISTA> LISTAR_LICENCIA_X_ARTISTA(string ARTISTA, string FECHA_INICIO, string FECHA_FIN)
        {
            List<BE_LICENCIA_X_ARTISTA> lista = new List<BE_LICENCIA_X_ARTISTA>();
            BE_LICENCIA_X_ARTISTA item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_REPORTE_LICENCIAS_X_ARTISTA"))
            {
                oDataBase.AddInParameter(cm, "@ARTISTA", DbType.String, ARTISTA);
                oDataBase.AddInParameter(cm, "@FECHA_INICIO", DbType.String, FECHA_INICIO);
                oDataBase.AddInParameter(cm, "@FECHA_FIN", DbType.String, FECHA_FIN);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BE_LICENCIA_X_ARTISTA();
                        if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_LICENCIA")))
                            item.CODIGO_LICENCIA = dr.GetInt32(dr.GetOrdinal("CODIGO_LICENCIA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_EVENTO")))
                            item.FECHA_EVENTO = dr.GetString(dr.GetOrdinal("FECHA_EVENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE_ESTABLECIMIENTO")))
                            item.NOMBRE_ESTABLECIMIENTO = dr.GetString(dr.GetOrdinal("NOMBRE_ESTABLECIMIENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CANTIDAD_DE_ARTISTAS")))
                            item.CANTIDAD_DE_ARTISTAS = dr.GetInt32(dr.GetOrdinal("CANTIDAD_DE_ARTISTAS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_COBRADO")))
                            item.MONTO_COBRADO = dr.GetDecimal(dr.GetOrdinal("MONTO_COBRADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LUGAR")))
                            item.LUGAR = dr.GetString(dr.GetOrdinal("LUGAR"));
                        
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
    }
}
