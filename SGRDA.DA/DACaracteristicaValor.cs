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
    public class DACaracteristicaValor
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BECaracteristicaValor ent)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_CARACTERISTICAS_VALOR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, ent.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, ent.DAD_TYPE);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, ent.DAD_ID);
            db.AddInParameter(oDbCommand, "@DAD_STYPE", DbType.Decimal, ent.DAD_STYPE);
            db.AddInParameter(oDbCommand, "@DADV_ID", DbType.Decimal, ent.DADV_ID);
            db.AddInParameter(oDbCommand, "@DAC_ID", DbType.String, ent.DAC_ID);
            db.AddInParameter(oDbCommand, "@VALUE", DbType.String, ent.VALUE.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, ent.LOG_USER_CREAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar(BECaracteristicaValor ent)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDU_CARACTERISTICAS_VALOR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, ent.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, ent.DAD_ID);
            db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, ent.DAD_TYPE);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, ent.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BECaracteristicaValor ent)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_CARACTERISTICAS_VALOR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, ent.OWNER);
            db.AddInParameter(oDbCommand, "@CHARVAL_ID", DbType.Decimal, ent.CHARVAL_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, ent.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BECaracteristicaValor> Listar(string owner,decimal id, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_CARACTERISTICAS_VALOR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, id);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_CARACTERISTICAS_VALOR", owner, id, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BECaracteristicaValor>();
            var obj = new BECaracteristicaValor();
            using (IDataReader dr = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (dr.Read())
                {
                    obj = new BECaracteristicaValor();
                    obj.CHARVAL_ID = dr.GetDecimal(dr.GetOrdinal("CHARVAL_ID"));
                    obj.SUBDIVISION = dr.GetString(dr.GetOrdinal("SUBDIVISION")).ToUpper();
                    obj.VALOR = dr.GetString(dr.GetOrdinal("VALOR")).ToUpper();
                    obj.CARACTERISTICA = dr.GetString(dr.GetOrdinal("CARACTERISTICA")).ToUpper();
                    obj.VALUE = dr.GetString(dr.GetOrdinal("VALUE")).ToUpper();
                    obj.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(obj);
                }
            }
            return lista;
        }

    }
}
