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
    public class DACuentaContable
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BECuentaContable> Listar_Page_Cuentas_Contables(string desc, string cuenta, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_CUENTAS_CONTABLES");
            oDataBase.AddInParameter(oDbCommand, "@LED_DESC", DbType.String, desc);
            oDataBase.AddInParameter(oDbCommand, "@LED_NRO", DbType.String, cuenta);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BECuentaContable>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BECuentaContable(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BECuentaContable> Obtener(string owner, decimal id)
        {
            List<BECuentaContable> lst = new List<BECuentaContable>();
            BECuentaContable Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CUENTAS_CONTABLES"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LED_ID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BECuentaContable();
                        Obj.LED_ID = dr.GetDecimal(dr.GetOrdinal("LED_ID"));
                        Obj.LED_DESC = dr.GetString(dr.GetOrdinal("LED_DESC")).ToUpper();
                        Obj.LED_NRO = dr.GetString(dr.GetOrdinal("LED_NRO")).ToUpper();
                        Obj.START = dr.GetDateTime(dr.GetOrdinal("START"));
                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        public int Insertar(BECuentaContable cont)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_CUENTAS_CONTABLES");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, cont.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@LED_DESC", DbType.String, cont.LED_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LED_NRO", DbType.String, cont.LED_NRO.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@START", DbType.DateTime, cont.START);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, cont.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BECuentaContable cont)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_CUENTAS_CONTABLES");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, cont.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@LED_ID", DbType.Decimal, cont.LED_ID);
            oDataBase.AddInParameter(oDbCommand, "@LED_DESC", DbType.String, cont.LED_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LED_NRO", DbType.String, cont.LED_NRO.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@START", DbType.DateTime, cont.START);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, cont.LOG_USER_UPDATE);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BECuentaContable cont)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_CUENTAS_CONTABLES");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, cont.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@LED_ID", DbType.Decimal, cont.LED_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, cont.LOG_USER_UPDATE);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public List<BECuentaContable> ListarCombo(string owner)
        {
            List<BECuentaContable> lst = new List<BECuentaContable>();
            BECuentaContable Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_TIPO_CUENTA_CONTABLE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BECuentaContable();
                        Obj.LED_ID = dr.GetDecimal(dr.GetOrdinal("LED_ID"));
                        Obj.LED_DESC = dr.GetString(dr.GetOrdinal("LED_DESC")).ToUpper();
                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

    }
}
