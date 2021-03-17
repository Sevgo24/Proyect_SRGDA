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
    public class DAImpuestoValor
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BEImpuestoValor> Listar(string owner, decimal idTax)
        {
            List<BEImpuestoValor> valor = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_VALORES_IMPUESTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@TAX_ID", DbType.Decimal, idTax);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEImpuestoValor ent = null;
                    valor = new List<BEImpuestoValor>();
                    while (dr.Read())
                    {
                        ent = new BEImpuestoValor();

                        ent.TAXV_ID = dr.GetDecimal(dr.GetOrdinal("TAXV_ID"));
                        ent.DIV_ID = dr.GetDecimal(dr.GetOrdinal("DIV_ID"));
                        ent.TAX_ID = dr.GetDecimal(dr.GetOrdinal("TAX_ID"));
                        ent.DIVISION = dr.GetString(dr.GetOrdinal("DIVISION"));
                        ent.TAXV_VALUEP = dr.GetDecimal(dr.GetOrdinal("TAXV_VALUEP"));
                        ent.TAXV_VALUEM = dr.GetDecimal(dr.GetOrdinal("TAXV_VALUEM"));
                        ent.FechaVigencia = dr.GetDateTime(dr.GetOrdinal("START")).ToString("dd/MM/yyyy");
                        ent.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        ent.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                            ent.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            ent.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        valor.Add(ent);
                    }
                }
            }
            return valor;
        }

        public int Insertar(BEImpuestoValor valor)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_VALORES_IMPUESTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DIV_ID", DbType.Decimal, valor.DIV_ID);
            oDataBase.AddInParameter(oDbCommand, "@TAX_ID", DbType.Decimal, valor.TAX_ID);
            oDataBase.AddInParameter(oDbCommand, "@TAXV_VALUEP", DbType.Decimal, valor.TAXV_VALUEP);
            oDataBase.AddInParameter(oDbCommand, "@TAXV_VALUEM", DbType.Decimal, valor.TAXV_VALUEM);
            oDataBase.AddInParameter(oDbCommand, "@START", DbType.DateTime, valor.FechaVigencia);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, valor.LOG_USER_CREAT.ToUpper());
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar(BEImpuestoValor valor)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_VALORES_IMPUESTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TAXV_ID", DbType.Decimal, valor.TAXV_ID);
            oDataBase.AddInParameter(oDbCommand, "@DIV_ID", DbType.Decimal, valor.DIV_ID);
            oDataBase.AddInParameter(oDbCommand, "@TAX_ID", DbType.Decimal, valor.TAX_ID);
            oDataBase.AddInParameter(oDbCommand, "@TAXV_VALUEP", DbType.Decimal, valor.TAXV_VALUEP);
            oDataBase.AddInParameter(oDbCommand, "@TAXV_VALUEM", DbType.Decimal, valor.TAXV_VALUEM);
            oDataBase.AddInParameter(oDbCommand, "@START", DbType.DateTime, valor.FechaVigencia);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, valor.LOG_USER_UPDAT.ToUpper());
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BEImpuestoValor valor)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_VALORES_IMPUESTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TAXV_ID", DbType.Decimal, valor.TAXV_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, valor.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(BEImpuestoValor valor)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_VALORES_IMPUESTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, valor.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TAXV_ID", DbType.Decimal, valor.TAXV_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, valor.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEImpuestoValor Obtener(string owner,decimal id)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_VALORES_IMPUESTO");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@TAXV_ID", DbType.Decimal, id);
                BEImpuestoValor ent = null;
                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        ent = new BEImpuestoValor();
                        ent.TAXV_ID = dr.GetDecimal(dr.GetOrdinal("TAXV_ID"));
                        ent.DIV_ID = dr.GetDecimal(dr.GetOrdinal("DIV_ID"));
                        ent.TAX_ID = dr.GetDecimal(dr.GetOrdinal("TAX_ID"));
                        ent.TAXV_VALUEP = dr.GetDecimal(dr.GetOrdinal("TAXV_VALUEP"));
                        ent.TAXV_VALUEM = dr.GetDecimal(dr.GetOrdinal("TAXV_VALUEM"));
                        ent.FechaVigencia = dr.GetDateTime(dr.GetOrdinal("START")).ToString("dd/MM/yyyy");
                        ent.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        ent.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                            ent.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            ent.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                }
                return ent;
            }catch(Exception ex){

                return null;
            }
        }
    }
}
