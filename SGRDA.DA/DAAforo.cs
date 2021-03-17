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
    public class DAAforo
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEAforo> Listar(string owner)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_TIPO_AFORO");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);

            List<BEAforo> lst = new List<BEAforo>();

            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    BEAforo obj = new BEAforo();
                    obj.CAP_ID = dr.GetString(dr.GetOrdinal("CAP_ID"));
                    obj.CAP_DESC = dr.GetString(dr.GetOrdinal("CAP_DESC")).ToUpper();
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public BEAforo ObtenerAforoXCod(string owner, string idAforo)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_TIPO_AFORO_X_COD");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@CAP_ID", DbType.String, idAforo);

            BEAforo obj = null;
            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    obj = new BEAforo();
                    obj.CAP_ID = dr.GetString(dr.GetOrdinal("CAP_ID"));
                    obj.CAP_DESC = dr.GetString(dr.GetOrdinal("CAP_DESC"));
                }
            }
            return obj;

        }
        /// <summary>
        /// PERMITE CALCULAR EL TOTAL DE AFORO POR TIPO DE AFORO
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="CAP_ID"> TIPO DE AFORO</param>
        /// <param name="lic_id">ID DE LA LICENCIA</param>
        /// <returns></returns>
        public decimal CalculaMontoLiquidarAforo(string owner, string CAP_ID, decimal lic_id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_TOTALXAFOROSELEC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, lic_id);
            db.AddInParameter(oDbCommand, "@CAP_ID", DbType.String, CAP_ID);
            decimal total = Convert.ToDecimal( db.ExecuteScalar(oDbCommand));

            return total;
        }

        /// <summary>
        ///  INSERTA EN LA TABLA  REC_CAPACITY_LIC
        /// </summary>
        /// <param name="owner">dueño</param>
        /// <param name="licid">licencia </param>
        /// <param name="capid">tipo de aforo</param>
        /// <param name="cap_iprelq"> PRELIQUIDACION / LIQUIDACION</param>
        /// <param name="total">EL TOTAL </param>
        /// <param name="log_user_create">USUARIO QUE INSERTO</param>
        /// <returns></returns>
        public int INSERTAR_AFORO_LIC(string owner, decimal licid, string capid,string cap_iprelq, decimal total, string log_user_create)
        {
            DbCommand oDbcommand = db.GetStoredProcCommand("SGRDASI_AFORO_LIC");
            db.AddInParameter(oDbcommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbcommand, "@LIC_ID", DbType.Decimal, licid);
            db.AddInParameter(oDbcommand, "@CAP_ID", DbType.String, capid);
            db.AddInParameter(oDbcommand, "@CAP_IPRELQ", DbType.String, cap_iprelq);
            db.AddInParameter(oDbcommand, "@TOTAL_IPRELQ", DbType.Decimal, total);
            db.AddInParameter(oDbcommand, "@LOG_USER_CREATE", DbType.String, log_user_create);
            int r=db.ExecuteNonQuery(oDbcommand);
            return r;

        }
    }
}
