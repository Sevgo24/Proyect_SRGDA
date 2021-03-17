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
    public class DATipoReporte
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

      
        public List<BETipoReporte> Obtener(string owner)
        {
            List<BETipoReporte> lst = new List<BETipoReporte>();
            BETipoReporte Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_TIPO_REPORTE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETipoReporte();
                        Obj.REPORT_TYPE = dr.GetDecimal(dr.GetOrdinal("REPORT_TYPE"));
                        Obj.RPT_DESC = dr.GetString(dr.GetOrdinal("RPT_DESC"));
                        Obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            Obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            Obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                            Obj.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));

                        Obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

      
    }
}
