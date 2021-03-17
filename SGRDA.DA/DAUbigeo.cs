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
    public class DAUbigeo
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEUbigeo> Listar_Ubigeo(decimal codigo, string value)
        {
            List<BEUbigeo> lst = new List<BEUbigeo>();
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("USP_BUSCAR_UBIGEO"))
                {
                    db.AddInParameter(cm, "@TIS_N", DbType.Decimal, codigo);
                    db.AddInParameter(cm, "@DAD_VNAME", DbType.String, value);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                            lst.Add(new BEUbigeo(dr));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public BEUbigeo ObtenerDescripcion(decimal idTerritorio, decimal idUbigeo)
        {
            BEUbigeo obj = new BEUbigeo();
            
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_UBIGEO"))
                {
                    db.AddInParameter(cm, "@TIS_N", DbType.Decimal, idTerritorio);
                    db.AddInParameter(cm, "@DADV_ID", DbType.Decimal, idUbigeo);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            obj = new BEUbigeo();
                            obj.ID_UBIGEO = dr.GetDecimal(dr.GetOrdinal("ID_UBIGEO"));
                            obj.NOMBRE_UBIGEO = dr.GetString(dr.GetOrdinal("NOMBRE_UBIGEO"));
                        }
                    }
                }
             
             
            return obj;
        }
    }
}
