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
    public class DAFormatoFacturaxGrupoModalidad
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEFormatoFacturaxGrupoModalidad req)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_FORMATO_GRUP_MOD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, req.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@INVF_ID", DbType.Decimal, req.INVF_ID);
            oDataBase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, req.MOG_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, req.LOG_USER_CREAT);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@MNR_ID"));
            return n;
        }

        public List<BEFormatoFacturaxGrupoModalidad> Listar(string owner, string id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_FORMATO_GRUP_MOD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, id);
            oDataBase.ExecuteNonQuery(oDbCommand);

            List<BEFormatoFacturaxGrupoModalidad> lista = new List<BEFormatoFacturaxGrupoModalidad>();
            BEFormatoFacturaxGrupoModalidad detalle = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    detalle = new BEFormatoFacturaxGrupoModalidad();
                    detalle.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    detalle.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                    detalle.GRUPO = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                    detalle.INVF_ID = dr.GetDecimal(dr.GetOrdinal("INVF_ID"));
                    detalle.INVF_ID_ANT = dr.GetDecimal(dr.GetOrdinal("INVF_ID"));
                    detalle.FORMATO = dr.GetString(dr.GetOrdinal("INVF_DESC"));
                    detalle.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    detalle.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        detalle.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        detalle.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        detalle.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(detalle);
                }
            }
            return lista;
        }
    }
}
