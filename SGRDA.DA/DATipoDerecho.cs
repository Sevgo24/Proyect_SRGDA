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
    public class DATipoDerecho
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETipoDerecho> Listar_Page_Tipo_Derecho(string owner, string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPO_DERECHOS");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_DESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BETipoDerecho>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BETipoDerecho(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BETipoDerecho> Obtener(string owner, string nombre)
        {
            List<BETipoDerecho> lst = new List<BETipoDerecho>();
            BETipoDerecho Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_DERECHOS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@RIGHT_COD", DbType.String, nombre);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETipoDerecho();
                        Obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        Obj.RIGHT_COD = dr.GetString(dr.GetOrdinal("RIGHT_COD"));
                        Obj.RIGHT_DESC = dr.GetString(dr.GetOrdinal("RIGHT_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WORK_RIGHT_CODE")))
                        {
                            Obj.WORK_RIGHT_CODE = dr.GetString(dr.GetOrdinal("WORK_RIGHT_CODE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("WORK_RIGHT_DESC")))
                        {
                            Obj.WORK_RIGHT_DESC = dr.GetString(dr.GetOrdinal("WORK_RIGHT_DESC"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        lst.Add(Obj);
                    }
                }
            }

            return lst;
        }

        public int ValidacionTipoDerecho(BETipoDerecho en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_VALIDACION_TIPO_DERECHO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_DESC", DbType.String, en.RIGHT_DESC.ToUpper().Trim());
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, en.RIGHT_COD);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public int Insertar(BETipoDerecho en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPO_DERECHOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, en.RIGHT_COD.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_DESC", DbType.String, en.RIGHT_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@WORK_RIGHT_CODE", DbType.String, en.WORK_RIGHT_CODE == null ? string.Empty : en.WORK_RIGHT_CODE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@WORK_RIGHT_DESC", DbType.String, en.WORK_RIGHT_DESC == null ? string.Empty : en.WORK_RIGHT_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BETipoDerecho en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPO_DERECHOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, en.RIGHT_COD.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_DESC", DbType.String, en.RIGHT_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@WORK_RIGHT_CODE", DbType.String, en.WORK_RIGHT_CODE == null ? string.Empty : en.WORK_RIGHT_CODE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@WORK_RIGHT_DESC", DbType.String, en.WORK_RIGHT_DESC == null ? string.Empty : en.WORK_RIGHT_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BETipoDerecho del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TIPO_DERECHOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, del.RIGHT_COD);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BETipoDerecho> ListarCombo(string owner)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPODERECHO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BETipoDerecho>();
            BETipoDerecho obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BETipoDerecho();
                    obs.RIGHT_COD = dr.GetString(dr.GetOrdinal("VALUE"));
                    obs.RIGHT_DESC = dr.GetString(dr.GetOrdinal("TEXT"));
                    lista.Add(obs);
                }
            }
            return lista;
        }
    }
}
