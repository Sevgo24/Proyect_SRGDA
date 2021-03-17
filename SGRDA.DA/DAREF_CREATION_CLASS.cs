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
    public class DAREF_CREATION_CLASS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREF_CREATION_CLASS> usp_Get_REF_CREATION_CLASS(string CLASS_COD, string CLASS_DESC, string COD_PARENT_CLASS)
        {
            List<BEREF_CREATION_CLASS> lst = new List<BEREF_CREATION_CLASS>();
            BEREF_CREATION_CLASS item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REF_CREATION_CLASS_BY_CLASS_COD_CLASS_DESC_COD_PARENT_CLASS"))
                {
                    db.AddInParameter(cm, "CLASS_COD", DbType.String, CLASS_COD);
                    db.AddInParameter(cm, "CLASS_DESC", DbType.String, CLASS_DESC);
                    db.AddInParameter(cm, "COD_PARENT_CLASS", DbType.String, COD_PARENT_CLASS);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_CREATION_CLASS();
                            item.CLASS_COD = dr.GetString(dr.GetOrdinal("CLASS_COD"));
                            item.CLASS_DESC = dr.GetString(dr.GetOrdinal("CLASS_DESC"));
                            item.COD_PARENT_CLASS = dr.GetString(dr.GetOrdinal("COD_PARENT_CLASS"));
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public List<BEREF_CREATION_CLASS> ListarTipo(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_CREACION_TIPO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BEREF_CREATION_CLASS>();
            BEREF_CREATION_CLASS obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEREF_CREATION_CLASS();
                    obs.CLASS_COD = dr.GetString(dr.GetOrdinal("CLASS_COD"));
                    obs.CLASS_DESC = dr.GetString(dr.GetOrdinal("CLASS_DESC"));
                    lista.Add(obs);
                }
            }
            return lista;
        }

    }
}
