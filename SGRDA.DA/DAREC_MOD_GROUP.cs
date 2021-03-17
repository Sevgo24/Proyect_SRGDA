using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities;
using System.Data.Common;
using System.Data;

namespace SGRDA.DA
{
    public class DAREC_MOD_GROUP
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_MOD_GROUP> REC_MOD_GROUP_GET()
        {
            List<BEREC_MOD_GROUP> lst = new List<BEREC_MOD_GROUP>();
            BEREC_MOD_GROUP item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIV_TYPE_GET"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_MOD_GROUP();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                            item.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));

                            DateTime LOG_DATE_CREAT;
                            bool isDateCREAT = DateTime.TryParse(dr["LOG_DATE_CREAT"].ToString(), out LOG_DATE_CREAT);
                            item.LOG_DATE_CREAT = LOG_DATE_CREAT;

                            DateTime LOG_DATE_UPDATE;
                            bool isDateUPD = DateTime.TryParse(dr["LOG_DATE_UPDATE"].ToString(), out LOG_DATE_UPDATE);
                            item.LOG_DATE_UPDATE = LOG_DATE_UPDATE;

                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            item.LOG_USER_UPDAT = (item.LOG_USER_UPDAT == null) ? string.Empty : item.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));

                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public List<BEREC_MOD_GROUP> usp_REC_MOD_GROUP_Page(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_MOD_GROUP_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_MOD_GROUP_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREC_MOD_GROUP>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_MOD_GROUP(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_MOD_GROUP_Ins(BEREC_MOD_GROUP en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_MOD_GROUP_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, en.MOG_ID.ToUpper());
                db.AddInParameter(oDbCommand, "@MOG_DESC", DbType.String, en.MOG_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Insertar(BEREC_MOD_GROUP en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_MOD_GROUP_Ins");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@MOG_ID ", DbType.String, en.MOG_ID.ToUpper());
            db.AddInParameter(oDbCommand, "@MOG_DESC", DbType.String, en.MOG_DESC.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int InsertarDetalle(BEREC_MOD_GROUP req)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_FORMATO_GRUP_MOD");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@INVF_ID", DbType.Decimal, req.IdFormato);
            db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, req.MOG_ID.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, req.LOG_USER_CREAT);

            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEREC_MOD_GROUP en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_MOD_GROUP_Upd_by_MOG_ID");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@MOG_ID ", DbType.String, en.MOG_ID.ToUpper());
            db.AddInParameter(oDbCommand, "@MOG_DESC", DbType.String, en.MOG_DESC.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT ", DbType.String, en.LOG_USER_UPDAT.ToUpper());
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int ActualizarDetalle(BEREC_MOD_GROUP req)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_FORMATO_GRUP_MOD");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@INVF_ID", DbType.Decimal, req.IdFormato);
            db.AddInParameter(oDbCommand, "@INVF_ID_ANT", DbType.Decimal, req.IdFormatoAnt);
            db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, req.MOG_ID.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, req.LOG_USER_UPDAT);

            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BEREC_MOD_GROUP del)    
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_FORMATO_GROUP_MOD");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@INVF_ID", DbType.String, del.IdFormato);
            db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, del.MOG_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(BEREC_MOD_GROUP del)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_FORMATO_GROUP_MOD");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@INVF_ID", DbType.String, del.IdFormato);
            db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, del.MOG_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public bool REC_MOD_GROUP_Upd_by_MOG_ID(BEREC_MOD_GROUP en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_MOD_GROUP_Upd_by_MOG_ID");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@MOG_ID ", DbType.String, en.MOG_ID.ToUpper());
                db.AddInParameter(oDbCommand, "@MOG_DESC", DbType.String, en.MOG_DESC.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT ", DbType.String, en.LOG_USER_UPDAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BEREC_MOD_GROUP> REC_MOD_GROUP_GET_by_MOG_ID(string MOG_ID)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REC_MOD_GROUP_GET_by_MOG_ID", MOG_ID);
            var lista = new List<BEREC_MOD_GROUP>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_MOD_GROUP(reader));
            }
            return lista;
        }

        public bool REC_MOD_GROUP_Del(string MOG_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_MOD_GROUP_Del");
                db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, MOG_ID);
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BEREC_MOD_GROUP> ListarTipo(string owner,decimal idOficinaLog=0)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_GRUPO_TIPO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@ID_OFICINA", DbType.Decimal, idOficinaLog);

            var lista = new List<BEREC_MOD_GROUP>();
            BEREC_MOD_GROUP obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEREC_MOD_GROUP();
                    obs.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                    obs.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                    lista.Add(obs);
                }
            }
            return lista;
        }

        public BEREC_MOD_GROUP Obtener(string owner, string id)
        {
            //List<BECaracteristicaPredefinida> lst = new List<BECaracteristicaPredefinida>();
            BEREC_MOD_GROUP Obj = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_GRUPO_MODALIDAD"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@MOG_ID", DbType.String, id);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEREC_MOD_GROUP();
                        Obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        Obj.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                        Obj.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                    }
                }
            }

            return Obj;
        }

        public BEREC_MOD_GROUP ObtenerDetalle(string owner, string id,decimal idFormat)
        {
            BEREC_MOD_GROUP Obj = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_DETALLE_GRUPO_MODALIDAD"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@INVF_ID", DbType.Decimal, idFormat);
                db.AddInParameter(cm, "@MOG_ID", DbType.String, id);              

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEREC_MOD_GROUP();
                        Obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        Obj.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                        Obj.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                        Obj.IdFormato = dr.GetDecimal(dr.GetOrdinal("INVF_ID"));
                        Obj.Formato = dr.GetString(dr.GetOrdinal("INVF_DESC"));

                        Obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        Obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            Obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            Obj.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                }
            }

            return Obj;
        }

    }
}
