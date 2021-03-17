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
    public class DAModuloSistema
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEModulo> Listar_Page_Modulo_Sistema(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_MODULO_SISTEMA");
            oDataBase.AddInParameter(oDbCommand, "@MOD_DESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEModulo>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEModulo(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEModulo> Obtener(string owner, decimal id)
        {
            List<BEModulo> lst = new List<BEModulo>();
            BEModulo Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_MODULO_SISTEMA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@PROC_MOD", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEModulo();
                        Obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        Obj.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));
                        Obj.MOD_DESC = dr.GetString(dr.GetOrdinal("MOD_DESC")).ToUpper();
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_CLABEL")))
                            Obj.MOD_CLABEL = dr.GetString(dr.GetOrdinal("MOD_CLABEL")).ToUpper();
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_CAPIKEY")))
                            Obj.MOD_CAPIKEY = dr.GetString(dr.GetOrdinal("MOD_CAPIKEY")).ToUpper();
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_CSECRETKEY")))
                            Obj.MOD_CSECRETKEY = dr.GetString(dr.GetOrdinal("MOD_CSECRETKEY")).ToUpper();
                        //if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        //{
                        //    Obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        //}
                        //if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        //{
                        //    Obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        //}

                        //Obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT")).ToUpper();
                        //Obj.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE")).ToUpper();

                        //if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        //{
                        //    Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        //}

                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        public int Insertar(BEModulo en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_MODULO_SISTEMA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_DESC", DbType.String, en.MOD_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_CLABEL", DbType.String, en.MOD_CLABEL.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_CAPIKEY", DbType.String, en.MOD_CAPIKEY != null ? en.MOD_CAPIKEY.ToString().ToUpper() : en.MOD_CAPIKEY);
            oDataBase.AddInParameter(oDbCommand, "@MOD_CSECRETKEY", DbType.String, en.MOD_CSECRETKEY != null ? en.MOD_CSECRETKEY.ToString().ToUpper() : en.MOD_CSECRETKEY);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Actualizar(BEModulo en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_MODULO_SISTEMA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PROC_MOD", DbType.Decimal, en.PROC_MOD);
            oDataBase.AddInParameter(oDbCommand, "@MOD_DESC", DbType.String, en.MOD_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_CLABEL", DbType.String, en.MOD_CLABEL.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_CAPIKEY", DbType.String, en.MOD_CAPIKEY != null ? en.MOD_CAPIKEY.ToString().ToUpper() : en.MOD_CAPIKEY);
            oDataBase.AddInParameter(oDbCommand, "@MOD_CSECRETKEY", DbType.String, en.MOD_CSECRETKEY != null ? en.MOD_CSECRETKEY.ToString().ToUpper() : en.MOD_CSECRETKEY);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Eliminar(BEModulo del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_MODULO_SISTEMA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@PROC_MOD", DbType.Decimal, del.PROC_MOD);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEModulo> ListarModulo(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_MOD");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);

            var lista = new List<BEModulo>();
            BEModulo obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEModulo();
                    obs.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));
                    obs.MOD_DESC = dr.GetString(dr.GetOrdinal("MOD_DESC"));
                    lista.Add(obs);
                }
            }
            return lista;
        }
    }
}
