using SGRDA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Data.Common;
using System.Data;
namespace SGRDA.DA
{
   public class DAShowArtista
    {


       public List<BEShowArtista> ShowsXArtistas(decimal idShow, string owner)
       {
           Database db = new DatabaseProviderFactory().Create("conexion");
           List<BEShowArtista> lst = null;
           using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_ARTISTA_X_SHOW"))
           {
               db.AddInParameter(cm, "@OWNER", DbType.String, owner);
               db.AddInParameter(cm, "@SHOW_ID", DbType.Decimal, idShow);
               using (IDataReader dr = db.ExecuteReader(cm))
               {
                   lst = new List<BEShowArtista>();
                   while (dr.Read())
                   {
                       var obj = new BEShowArtista();
                       obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                       obj.SHOW_ID = dr.GetDecimal(dr.GetOrdinal("SHOW_ID"));
                       obj.ARTIST_ID  = dr.GetDecimal(dr.GetOrdinal("ARTIST_ID"));
                       obj.NAME = dr.GetString(dr.GetOrdinal("NAME"));
                       obj.ARTIST_PPAL = dr.GetString(dr.GetOrdinal("ARTIST_PPAL"));
                       obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                       obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                       obj.IP_NAME = dr.GetString(dr.GetOrdinal("IP_NAME"));

                       if (!dr.IsDBNull(dr.GetOrdinal("ARTIST_ID_SGS")))
                           obj.ARTIST_ID_SGS = dr.GetDecimal(dr.GetOrdinal("ARTIST_ID_SGS"));

                       if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                       {
                           obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                       }
                       if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                       {
                           obj.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                       }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_ID")))
                            {
                                obj.ESTADO_ID = dr.GetDecimal(dr.GetOrdinal("ESTADO_ID"));
                            }
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            {
                                obj.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                            }
                        obj.REPORT_ID = dr.GetDecimal(dr.GetOrdinal("REPORT_ID"));
                            lst.Add(obj);
                   }
               }
           }
           return lst;
       }

    }
}
