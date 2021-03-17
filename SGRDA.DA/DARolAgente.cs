using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities;


namespace SGRDA.DA
{
    public class DARolAgente
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BERoles> ListarTipoAgente(string prefijo)
        {
            List<BERoles> lst = new List<BERoles>();
            BERoles item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPOAGENTE"))
            {
                oDataBase.AddInParameter(cm, "@PREFIJO", DbType.String, prefijo);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BERoles();
                        item.CodigoPerfil = dr.GetInt32(dr.GetOrdinal("VALUE"));
                        item.Nombre = dr.GetString(dr.GetOrdinal("TEXT"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }
    }
}
