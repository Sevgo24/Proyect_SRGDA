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
    public class DAEstadoLicencia
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEEstadoLicencia> Listar_Page_EstadoLicencia(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_ESTADO_LICENCIA");
            oDataBase.AddInParameter(oDbCommand, "@LICS_NAME", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEEstadoLicencia>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEEstadoLicencia(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEEstadoLicencia> Obtener(string owner, decimal id)
        {
            List<BEEstadoLicencia> lst = new List<BEEstadoLicencia>();
            BEEstadoLicencia Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_ESTADO_LICENCIA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LICS_ID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEEstadoLicencia();
                        Obj.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                        Obj.LICS_NAME = dr.GetString(dr.GetOrdinal("LICS_NAME")).ToUpper();
                        Obj.LICS_INI = Convert.ToChar(dr.GetValue(dr.GetOrdinal("LICS_INI")));
                        Obj.LICS_END = Convert.ToChar(dr.GetValue(dr.GetOrdinal("LICS_END")));
                       
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

        public int Insertar(BEEstadoLicencia en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_ESTADO_LICENCIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@LICS_NAME", DbType.String, en.LICS_NAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LICS_INI", DbType.String, en.LICS_INI);
            oDataBase.AddInParameter(oDbCommand, "@LICS_END", DbType.String, en.LICS_END);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Actualizar(BEEstadoLicencia en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ESTADO_LICENCIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@LICS_ID", DbType.Decimal, en.LICS_ID);
            oDataBase.AddInParameter(oDbCommand, "@LICS_NAME", DbType.String, en.LICS_NAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LICS_INI", DbType.String, en.LICS_INI);
            oDataBase.AddInParameter(oDbCommand, "@LICS_END", DbType.String, en.LICS_END);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Eliminar(BEEstadoLicencia del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_ESTADO_LICENCIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@LICS_ID", DbType.Decimal, del.LICS_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEEstadoLicencia> ListarEstado(string owner)
        {
            List<BEEstadoLicencia> lst = new List<BEEstadoLicencia>();
            BEEstadoLicencia Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_ESTLICENCIA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEEstadoLicencia();
                        Obj.LICS_ID = dr.GetDecimal(dr.GetOrdinal("LICS_ID"));
                        Obj.LICS_NAME = dr.GetString(dr.GetOrdinal("LICS_NAME")).ToUpper();

                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }
    }
}
