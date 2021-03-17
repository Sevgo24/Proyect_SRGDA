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
    public class DAGarantia
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Activar(string owner, decimal idGarantia, string usu)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASU_ACTIVAR_GARANTIA");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@GUAR_ID", DbType.String, idGarantia);
            db.AddInParameter(cm, "@LOG_USER_UPDATE", DbType.String, usu);

            int r = db.ExecuteNonQuery(cm);
            return r;
        }

        public int Eliminar(string owner, decimal idGarantia, string usu)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASU_ELIMINAR_GARANTIA");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@GUAR_ID", DbType.String, idGarantia);
            db.AddInParameter(cm, "@LOG_USER_UPDATE", DbType.String, usu);

            int r = db.ExecuteNonQuery(cm);
            return r;
        }

        public int Actualizar(string owner, decimal idGarantia, decimal idLic, decimal valor, string moneda, string tipo, string numero, string entidad, DateTime rFecha, DateTime? dFecha, decimal? aValor, decimal? dValor, DateTime? tFecha, string usu)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASU_GARANTIA");

            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@LIC_ID", DbType.String, idLic);
            db.AddInParameter(cm, "@GUAR_ID", DbType.Decimal, idGarantia);
            db.AddInParameter(cm, "@GUAR_VAL", DbType.String, valor);
            db.AddInParameter(cm, "@CUR_ALPHA", DbType.String, moneda);
            db.AddInParameter(cm, "@GUAR_TYPE", DbType.String, tipo);
            db.AddInParameter(cm, "@GUAR_NRO", DbType.String, numero);
            db.AddInParameter(cm, "@GUAR_ENTITY", DbType.String, entidad);
            db.AddInParameter(cm, "@GUAR_RDATE", DbType.DateTime, rFecha);
            db.AddInParameter(cm, "@GUAR_DDATE", DbType.DateTime, dFecha);
            db.AddInParameter(cm, "@GUAR_AVAL", DbType.Decimal, aValor);
            db.AddInParameter(cm, "@GUAR_DVAL", DbType.Decimal, dValor);
            db.AddInParameter(cm, "@GUAR_TDATE", DbType.DateTime, tFecha);
            db.AddInParameter(cm, "@LOG_USER_UPDATE", DbType.String, usu);

            int r = db.ExecuteNonQuery(cm);
            return r;
        }

        public int Insertar(string owner,decimal idLic,decimal valor,string moneda,string tipo,string numero,string entidad,DateTime rFecha,DateTime? dFecha,decimal? aValor,decimal? dValor,DateTime? tFecha,string usu)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASI_GARANTIA");

            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@LIC_ID", DbType.String, idLic);
            db.AddInParameter(cm, "@GUAR_VAL", DbType.String, valor);
            db.AddInParameter(cm, "@CUR_ALPHA", DbType.String, moneda);
            db.AddInParameter(cm, "@GUAR_TYPE", DbType.String, tipo);
            db.AddInParameter(cm, "@GUAR_NRO", DbType.String, numero);
            db.AddInParameter(cm, "@GUAR_ENTITY", DbType.String, entidad);
            db.AddInParameter(cm, "@GUAR_RDATE", DbType.DateTime, rFecha);
            db.AddInParameter(cm, "@GUAR_DDATE", DbType.DateTime, dFecha);
            db.AddInParameter(cm, "@GUAR_AVAL", DbType.Decimal, aValor);
            db.AddInParameter(cm, "@GUAR_DVAL", DbType.Decimal, dValor);
            db.AddInParameter(cm, "@GUAR_TDATE", DbType.DateTime, tFecha);
            db.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, usu);

            int r = db.ExecuteNonQuery(cm);
            return r;
        }
        public BEGarantia ObtenerGarantiaXCod(string owner, decimal idGarantia, decimal idLic)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_GARANTIA_X_CODIGO");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@GUAR_ID", DbType.Decimal, idGarantia);
            db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

            BEGarantia obj = null;
            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    obj = new BEGarantia();
                    obj.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    obj.GUAR_ID = dr.GetDecimal(dr.GetOrdinal("GUAR_ID"));
                    obj.GUAR_VAL = dr.GetDecimal(dr.GetOrdinal("GUAR_VAL"));
                    obj.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                    obj.GUAR_TYPE = dr.GetString(dr.GetOrdinal("GUAR_TYPE"));
                    obj.GUAR_NRO = dr.GetString(dr.GetOrdinal("GUAR_NRO"));
                    obj.GUAR_ENTITY = dr.GetString(dr.GetOrdinal("GUAR_ENTITY"));
                    obj.GUAR_RDATE = dr.GetDateTime(dr.GetOrdinal("GUAR_RDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("GUAR_DDATE")))
                    obj.GUAR_DDATE = dr.GetDateTime(dr.GetOrdinal("GUAR_DDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("GUAR_AVAL")))
                    obj.GUAR_AVAL = dr.GetDecimal(dr.GetOrdinal("GUAR_AVAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("GUAR_DVAL")))
                    obj.GUAR_DVAL = dr.GetDecimal(dr.GetOrdinal("GUAR_DVAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("GUAR_TDATE")))
                    obj.GUAR_TDATE = dr.GetDateTime(dr.GetOrdinal("GUAR_TDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                    {
                        obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                }
            }
            return obj;
        }

        public List<BEGarantia> ListarGarantia(string owner, decimal idLic)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_GARANTIA_X_LICENCIA");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
            db.ExecuteNonQuery(cm);

            List<BEGarantia> lst = new List<BEGarantia>();

            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while(dr.Read())
                {
                    BEGarantia obj = new BEGarantia();
                    obj.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    obj.GUAR_ID = dr.GetDecimal(dr.GetOrdinal("GUAR_ID"));
                    obj.GUAR_VAL = dr.GetDecimal(dr.GetOrdinal("GUAR_VAL"));
                    obj.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                    obj.GUAR_TYPE = dr.GetString(dr.GetOrdinal("GUAR_TYPE"));
                    obj.GUAR_NRO = dr.GetString(dr.GetOrdinal("GUAR_NRO"));
                    obj.GUAR_ENTITY = dr.GetString(dr.GetOrdinal("GUAR_ENTITY"));
                    obj.GUAR_RDATE = dr.GetDateTime(dr.GetOrdinal("GUAR_RDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("GUAR_DDATE")))
                        obj.GUAR_DDATE = dr.GetDateTime(dr.GetOrdinal("GUAR_DDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("GUAR_AVAL")))
                        obj.GUAR_AVAL = dr.GetDecimal(dr.GetOrdinal("GUAR_AVAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("GUAR_DVAL")))
                        obj.GUAR_DVAL = dr.GetDecimal(dr.GetOrdinal("GUAR_DVAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("GUAR_TDATE")))
                        obj.GUAR_TDATE = dr.GetDateTime(dr.GetOrdinal("GUAR_TDATE"));

 

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                    {
                        obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }

        public int ActualizarDevolucion(string owner, decimal idGarantia, decimal idLic, DateTime dFecha, string usu)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASU_DEVOLVER_GARANTIA");

            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@LIC_ID", DbType.String, idLic);
            db.AddInParameter(cm, "@GUAR_ID", DbType.Decimal, idGarantia);
            db.AddInParameter(cm, "@GUAR_DDATE", DbType.DateTime, dFecha);
            db.AddInParameter(cm, "@LOG_USER_UPDATE", DbType.String, usu);

            int r = db.ExecuteNonQuery(cm);
            return r;
        }
    }
}
