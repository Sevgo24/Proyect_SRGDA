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
    public class DADerecho
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEDerecho> ListarTipo(string owner,string class_cod)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_DERECHO_TIPO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@CLASS_COD", DbType.String, class_cod);

            var lista = new List<BEDerecho>();
            BEDerecho obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEDerecho();
                    obs.RIGHT_COD = dr.GetString(dr.GetOrdinal("RIGHT_COD"));
                    obs.RIGHT_DESC = dr.GetString(dr.GetOrdinal("RIGHT_DESC"));
                    lista.Add(obs);
                }
            }
            return lista;
        }

        public int Insertar(BEClaseCreacion en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_DERECHO_POR_CREACION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, en.RIGHT_COD.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, en.CLASS_COD.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public List<BEClaseCreacion> Listar(string owner, string clas)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DERECHO_CLASES");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, clas);
            oDataBase.ExecuteNonQuery(oDbCommand);

            List<BEClaseCreacion> lista = new List<BEClaseCreacion>();
            BEClaseCreacion detalle = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    detalle = new BEClaseCreacion();
                    detalle.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("Id"));
                    detalle.RIGHT_COD = dr.GetString(dr.GetOrdinal("RIGHT_COD"));
                    detalle.auxRIGHT_COD = dr.GetString(dr.GetOrdinal("auxRIGHT_COD"));
                    detalle.RIGHT_DESC = dr.GetString(dr.GetOrdinal("RIGHT_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        detalle.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    
                    detalle.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        detalle.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(detalle);
                }
            }
            return lista;
        }
    }
}
