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
    public class DAREF_DIVISIONES
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREF_DIVISIONES> Get_REF_DIVISIONES()
        {
            List<BEREF_DIVISIONES> lst = new List<BEREF_DIVISIONES>();
            BEREF_DIVISIONES item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_Get_REF_DIVISIONES"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIVISIONES();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                            item.DAD_CODE = dr.GetString(dr.GetOrdinal("DAD_CODE"));
                            item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                            item.DAD_TYPE = dr.GetString(dr.GetOrdinal("DAD_TYPE"));
                            item.DAD_TNAME = dr.GetString(dr.GetOrdinal("DAD_TNAME"));
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

        public List<BEREF_DIVISIONES> GET_REF_DIVISIONES_BY_DAD_TYPE(string DAD_TYPE, string OWNER)
        {
            List<BEREF_DIVISIONES> lst = new List<BEREF_DIVISIONES>();
            BEREF_DIVISIONES item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_REF_DIVISIONES"))
                {
                    db.AddInParameter(cm, "@DAD_TYPE", DbType.String, DAD_TYPE);
                    db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIVISIONES();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                            item.DAD_CODE = dr.GetString(dr.GetOrdinal("DAD_CODE"));
                            item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                            item.DAD_TYPE = dr.GetString(dr.GetOrdinal("DAD_TYPE"));
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


        public List<BEREF_DIVISIONES> REF_DIVISIONES_GET_by_DAD_ID(decimal DAD_ID)
        {
            List<BEREF_DIVISIONES> lst = new List<BEREF_DIVISIONES>();
            BEREF_DIVISIONES item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIVISIONES_GET_by_DAD_ID"))
                {
                    db.AddInParameter(cm, "@DAD_ID", DbType.String, DAD_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIVISIONES();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                            item.DAD_CODE = dr.GetString(dr.GetOrdinal("DAD_CODE"));
                            item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                            item.DAD_TYPE = dr.GetString(dr.GetOrdinal("DAD_TYPE"));
                            item.DAD_TNAME = dr.GetString(dr.GetOrdinal("DAD_TNAME"));
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

        public List<BEREF_DIVISIONES> REF_DIVISIONES_Page(string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_DIVISIONES_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REF_DIVISIONES_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREF_DIVISIONES>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREF_DIVISIONES(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REF_DIVISIONES_Ins(BEREF_DIVISIONES en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIVISIONES_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@DAD_CODE", DbType.String, en.DAD_CODE.ToUpper());
                db.AddInParameter(oDbCommand, "@DAD_NAME", DbType.String, en.DAD_NAME.ToUpper());
                db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, en.DAD_TYPE.ToUpper());
                db.AddInParameter(oDbCommand, "@DIV_DESCRIPTION", DbType.String, en.DIV_DESCRIPTION.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_DIVISIONES_Upd(BEREF_DIVISIONES en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIVISIONES_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, en.DAD_ID);
                db.AddInParameter(oDbCommand, "@DAD_CODE", DbType.String, en.DAD_CODE.ToUpper());
                db.AddInParameter(oDbCommand, "@DAD_NAME", DbType.String, en.DAD_NAME.ToUpper());
                db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, en.DAD_TYPE.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_DIVISIONES_Del(decimal DAD_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIVISIONES_Del");
                db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, DAD_ID);
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BEREF_DIVISIONES> ListarTipoDivision(string owner, string tipo)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_DIVISION_TIPO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, tipo);

            var lista = new List<BEREF_DIVISIONES>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEREF_DIVISIONES obj;
                while (dr.Read())
                {
                    obj = new BEREF_DIVISIONES();
                    obj.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    obj.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                    lista.Add(obj);
                }
            }
            return lista;
        }

 

        public List<BEREF_DIVISIONES> Listar(string owner, string tipo, string nombre, int estado, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_DIVISIONES");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, tipo);
            db.AddInParameter(oDbCommand, "@DAD_NAME", DbType.String, nombre);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREF_DIVISIONES>();
            BEREF_DIVISIONES div;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    div = new BEREF_DIVISIONES();
                    div.DAD_TYPE = dr.GetString(dr.GetOrdinal("DAD_TYPE"));
                    div.DAD_TNAME = dr.GetString(dr.GetOrdinal("DAD_TNAME"));
                    div.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                    div.NAME_TER = dr.GetString(dr.GetOrdinal("NAME_TER"));
                    div.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    div.DAD_CODE = dr.GetString(dr.GetOrdinal("DAD_CODE"));
                    div.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME")).ToUpper();
                    if (!dr.IsDBNull(dr.GetOrdinal("DIV_DESCRIPTION")))
                        div.DIV_DESCRIPTION = dr.GetString(dr.GetOrdinal("DIV_DESCRIPTION")).ToUpper();
                    else
                        div.DIV_DESCRIPTION = string.Empty;
                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        div.ESTADO = "ACTIVO";
                    else
                        div.ESTADO = "INACTIVO";
                    div.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(div);
                }
            }
            return lista;

        }

        public BEREF_DIVISIONES Obtener(string owner, decimal id)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_DIVISION");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, id);

            BEREF_DIVISIONES ent = null;
            using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BEREF_DIVISIONES();
                    ent.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    ent.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME")).ToUpper();
                    ent.DAD_TYPE = dr.GetString(dr.GetOrdinal("DAD_TYPE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DIV_DESCRIPTION")))
                        ent.DIV_DESCRIPTION = dr.GetString(dr.GetOrdinal("DIV_DESCRIPTION")).ToUpper();
                }
            }
            return ent;
        }

        public int Actualizar(BEREF_DIVISIONES valores)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_DIVISIONES_VALORES");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, valores.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, valores.DAD_ID);
            db.AddInParameter(oDbCommand, "@DAD_NAME", DbType.String, valores.DAD_NAME.ToUpper());
            db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, valores.DAD_TYPE);
            db.AddInParameter(oDbCommand, "@DIV_DESCRIPTION", DbType.String, valores.DIV_DESCRIPTION.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, valores.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BEREF_DIVISIONES valores)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_DIVISION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, valores.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, valores.DAD_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, valores.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        //LLENA EL COMBO DE LAS DIVISIONES FISCALES SÓLO POR EL TIPO "FIS"
        public List<BEREF_DIVISIONES> ListarDivisionesFiscales(string Owner)
        {
            List<BEREF_DIVISIONES> lst = new List<BEREF_DIVISIONES>();
            BEREF_DIVISIONES item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_DIVISIONES_FISCALES"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, Owner);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIVISIONES();
                            item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                            item.DAD_NAME = dr.GetString(dr.GetOrdinal("TEXT"));
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

        public List<BEREF_DIVISIONES> ListarDivisioneXtipo(string owner, string tipo)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPO_DIVISIONES");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@DAD_TYPE", DbType.String, tipo);

            var lista = new List<BEREF_DIVISIONES>();
            BEREF_DIVISIONES obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEREF_DIVISIONES();
                    obs.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    obs.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                    lista.Add(obs);
                }
            }
            return lista;
        }

    }
}
