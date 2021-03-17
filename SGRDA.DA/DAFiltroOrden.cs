using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SGRDA.Entities;
using System.Data.Common;


namespace SGRDA.DA
{
    public class DAFiltroOrden
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");


        public List<BeFiltroOrden> Listar(string owner)
        {
            DbCommand OdbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_FILTRO_ORDEN");
            db.AddInParameter(OdbCommand, "@owner", DbType.String, owner);

            List<BeFiltroOrden> lst = new List<BeFiltroOrden>();

            using (IDataReader dr = db.ExecuteReader(OdbCommand))
            {
                while (dr.Read())
                {
                    BeFiltroOrden obj = new BeFiltroOrden();
                    obj.ID_VALUE = dr.GetDecimal(dr.GetOrdinal("ID_VALUE"));
                    obj.DESCRIPCION = dr.GetString(dr.GetOrdinal("DESCRIPCION")).ToUpper();
                    lst.Add(obj);
                }
            }
            return lst;
        }
    }
}
