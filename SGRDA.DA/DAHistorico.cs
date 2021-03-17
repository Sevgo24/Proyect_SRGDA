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
    public class DAHistorico
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        public List<BEHistorico> HistoricoXSocio(decimal codigoBps, string owner)
        {
            List<BEHistorico> Historicos = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_HISTORICO_BPS"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, codigoBps);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        BEHistorico obj = null;
                        Historicos = new List<BEHistorico>();
                        while (dr.Read())
                        {
                            obj = new BEHistorico();

                            obj.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            obj.OFF_ID = Convert.ToInt32(dr["OFF_ID"]);
                            if (!dr.IsDBNull(dr.GetOrdinal("LEVEL_ID")))
                            obj.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                            obj.START = dr.GetDateTime(dr.GetOrdinal("START"));

                            obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT"))) 
                            {
                                obj.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                            }
                            
                            if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            {
                                obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                            }
                            
                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                            Historicos.Add(obj);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Historicos;
        }
    }
}
