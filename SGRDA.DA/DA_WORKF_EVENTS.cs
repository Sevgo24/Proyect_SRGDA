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
using SGRDA.Entities.WorkFlow;
using System.Data.Common;

namespace SGRDA.DA.WorkFlow
{
    public class DA_WORKF_EVENTS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<WORKF_EVENTS> ListarItems(string owner)
        {
            List<WORKF_EVENTS> lst = new List<WORKF_EVENTS>();
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_ITEMS_EVENTS"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        WORKF_EVENTS item = new WORKF_EVENTS();
                        item.WRKF_EID = dr.GetDecimal(dr.GetOrdinal("WRKF_EID"));
                        item.WRKF_ENAME = dr.GetString(dr.GetOrdinal("WRKF_ENAME"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public List<WORKF_EVENTS> usp_Get_EventoPage(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_EVENTO_PAGE");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@param", DbType.String, param);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<WORKF_EVENTS>();
            var item = new WORKF_EVENTS();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new WORKF_EVENTS();
                    item.WRKF_EID = dr.GetDecimal(dr.GetOrdinal("WRKF_EID"));
                    item.WRKF_ENAME = dr.GetString(dr.GetOrdinal("WRKF_ENAME"));
                    item.WRKF_ELABEL = dr.GetString(dr.GetOrdinal("WRKF_ELABEL"));
                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";
                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public WORKF_EVENTS Obtener(string owner, decimal IdEvento)
        {
            using (DbCommand cm = db.GetStoredProcCommand("SGRDAS_EVENTOS"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WRKF_EID", DbType.Decimal, IdEvento);
                WORKF_EVENTS item = null;
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_EVENTS();
                        item.WRKF_EID = dr.GetDecimal(dr.GetOrdinal("WRKF_EID"));
                        item.WRKF_ENAME = dr.GetString(dr.GetOrdinal("WRKF_ENAME"));
                        item.WRKF_ELABEL = dr.GetString(dr.GetOrdinal("WRKF_ELABEL"));
                    }
                }
                return item;
            }
        }

        public int Eliminar(WORKF_EVENTS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAD_EVENTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_EID", DbType.Decimal, en.WRKF_EID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Insertar(WORKF_EVENTS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAI_EVENTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_ENAME", DbType.String, en.WRKF_ENAME.ToUpper());
            db.AddInParameter(oDbCommand, "@WRKF_ELABEL", DbType.String, en.WRKF_ELABEL.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(WORKF_EVENTS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_WRKFW_EVENTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_EID", DbType.Decimal, en.WRKF_EID);
            db.AddInParameter(oDbCommand, "@WRKF_ENAME", DbType.String, en.WRKF_ENAME.ToUpper());
            db.AddInParameter(oDbCommand, "@WRKF_ELABEL", DbType.String, en.WRKF_ELABEL.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int ValidarDescripcion(string owner, string Descripcion)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_VALIDAR_EVENTO_DESC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_ENAME", DbType.String, Descripcion);
            int n = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return n;
        }
    }
}
