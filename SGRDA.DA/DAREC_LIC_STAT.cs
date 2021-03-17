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
    public class DAREC_LIC_STAT
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_LIC_STAT> GET_REC_LIC_STAT()
        {
            List<BEREC_LIC_STAT> lst = new List<BEREC_LIC_STAT>();
            BEREC_LIC_STAT item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_REC_LIC_STAT"))
                {

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_LIC_STAT();
                            item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                            item.LICS_NAME = dr.GetString(dr.GetOrdinal("LICS_NAME"));
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
        public BEREC_LIC_STAT GET_REC_LIC_STAT_X_COD(decimal LICS_ID)
        {
            BEREC_LIC_STAT item = null;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_REC_LIC_STAT_X_COD"))
            {
                db.AddInParameter(cm, "@LICS_ID", DbType.Decimal, LICS_ID);
                using(IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BEREC_LIC_STAT();
                        item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                        item.LICS_NAME = dr.GetString(dr.GetOrdinal("LICS_NAME"));

                    }
                }
            }
            return item;
        }

        public List<BEREC_LIC_STAT> EstadoFinPorTipo(decimal tipoLic, string empresa)
        {
            List<BEREC_LIC_STAT> lst = new List<BEREC_LIC_STAT>();
            BEREC_LIC_STAT item = null;
         
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_ESTADO_FIN_X_TIPO_LIC"))
                {
                    db.AddInParameter(cm, "@LIC_TYPE", DbType.Decimal, tipoLic);
                    db.AddInParameter(cm, "@OWNER", DbType.String, empresa);
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_LIC_STAT();
                            item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                            item.LICS_NAME = dr.GetString(dr.GetOrdinal("LICS_NAME"));
                            lst.Add(item);
                        }
                    }
                }
            
            return lst;
        }
         public List<BEREC_LIC_STAT> EstadoIntPorTipo(decimal tipoLic,string empresa)
        {
            List<BEREC_LIC_STAT> lst = new List<BEREC_LIC_STAT>();
            BEREC_LIC_STAT item = null;
           
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_ESTADO_INT_X_TIPO_LIC"))
                {
                    db.AddInParameter(cm, "@LIC_TYPE", DbType.Decimal, tipoLic);
                    db.AddInParameter(cm, "@OWNER", DbType.String, empresa);
                    
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_LIC_STAT();
                            item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                            item.LICS_NAME = dr.GetString(dr.GetOrdinal("LICS_NAME"));
                            lst.Add(item);
                        }
                    }
                }            
            return lst;
        }
         public List<BEREC_LIC_STAT> EstadoIniPorTipo(decimal tipoLic, string empresa)
         {
             List<BEREC_LIC_STAT> lst = new List<BEREC_LIC_STAT>();
             BEREC_LIC_STAT item = null;

             using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_ESTADO_INI_X_TIPO_LIC"))
             {
                 db.AddInParameter(cm, "@LIC_TYPE", DbType.Decimal, tipoLic);
                 db.AddInParameter(cm, "@OWNER", DbType.String, empresa);

                 using (IDataReader dr = db.ExecuteReader(cm))
                 {
                     while (dr.Read())
                     {
                         item = new BEREC_LIC_STAT();
                         item.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                         item.LICS_NAME = dr.GetString(dr.GetOrdinal("LICS_NAME"));
                         lst.Add(item);
                     }
                 }
             }
             return lst;
         }
    }
}
