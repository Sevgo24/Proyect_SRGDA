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
    public class DAREF_DIV_SUBTYPE
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREF_DIV_SUBTYPE> REF_DIV_SUBTYPE_Get()
        {
            List<BEREF_DIV_SUBTYPE> lst = new List<BEREF_DIV_SUBTYPE>();
            BEREF_DIV_SUBTYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIV_SUBTYPE_Get"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIV_SUBTYPE();
                            item.DAD_STYPE = dr.GetDecimal(dr.GetOrdinal("DAD_STYPE"));
                            item.DAD_SNAME = dr.GetString(dr.GetOrdinal("DAD_SNAME"));
                            item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                            item.DAD_BELONGS = dr.GetDecimal(dr.GetOrdinal("DAD_BELONGS"));
                            item.DAD_CODE = dr.GetString(dr.GetOrdinal("DAD_CODE"));

                            DateTime LOG_DATE_CREAT;
                            bool isDateCREAT = DateTime.TryParse(dr["LOG_DATE_CREAT"].ToString(), out LOG_DATE_CREAT);
                            item.LOG_DATE_CREAT = LOG_DATE_CREAT;

                            DateTime LOG_DATE_UPDATE;
                            bool isDateUPD = DateTime.TryParse(dr["LOG_DATE_UPDATE"].ToString(), out LOG_DATE_UPDATE);
                            item.LOG_DATE_UPDATE = LOG_DATE_UPDATE;

                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            item.LOG_USER_UPDATE = (item.LOG_USER_UPDATE == null) ? string.Empty : item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

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

        public List<BEREF_DIV_SUBTYPE> REF_DIV_SUBTYPE_GET_by_DAD_STYPE(decimal DAD_STYPE)
        {
            List<BEREF_DIV_SUBTYPE> lst = new List<BEREF_DIV_SUBTYPE>();
            BEREF_DIV_SUBTYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIV_SUBTYPE_GET_by_DAD_STYPE"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                    db.AddInParameter(cm, "@DAD_STYPE", DbType.Decimal, DAD_STYPE);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIV_SUBTYPE();
                            item.DAD_CODE = dr.GetString(dr.GetOrdinal("DAD_CODE"));
                            item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                            item.DAD_STYPE = dr.GetDecimal(dr.GetOrdinal("DAD_STYPE"));
                            item.DAD_SNAME = dr.GetString(dr.GetOrdinal("DAD_SNAME"));
                            item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                            item.DAD_BELONGS = dr.GetDecimal(dr.GetOrdinal("DAD_BELONGS"));

                            DateTime LOG_DATE_CREAT;
                            bool isDateCREAT = DateTime.TryParse(dr["LOG_DATE_CREAT"].ToString(), out LOG_DATE_CREAT);
                            item.LOG_DATE_CREAT = LOG_DATE_CREAT;

                            DateTime LOG_DATE_UPDATE;
                            bool isDateUPD = DateTime.TryParse(dr["LOG_DATE_UPDATE"].ToString(), out LOG_DATE_UPDATE);
                            item.LOG_DATE_UPDATE = LOG_DATE_UPDATE;

                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                            item.LOG_USER_UPDATE = (item.LOG_USER_UPDATE == null) ? string.Empty : item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

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

        public List<BEREF_DIV_SUBTYPE> REF_DIV_SUBTYPE_Page(string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_DIV_SUBTYPE_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REF_DIV_SUBTYPE_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREF_DIV_SUBTYPE>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEREF_DIV_SUBTYPE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEREF_DIV_SUBTYPE> REF_DIV_SUBTYPE_DAD_BELONGS_GET_by_DAD_ID(decimal DAD_ID)
        {
            List<BEREF_DIV_SUBTYPE> lst = new List<BEREF_DIV_SUBTYPE>();
            BEREF_DIV_SUBTYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIV_SUBTYPE_DAD_BELONGS_GET_by_DAD_ID"))
                {
                    db.AddInParameter(cm, "@DAD_ID", DbType.Decimal, DAD_ID);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIV_SUBTYPE();
                            item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                            item.DAD_BELONGS = dr.GetDecimal(dr.GetOrdinal("DAD_BELONGS"));
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

        public List<BEREF_DIV_SUBTYPE> REF_DIV_SUBTYPE_GET_by_DAD_ID(decimal DAD_ID)
        {
            List<BEREF_DIV_SUBTYPE> lst = new List<BEREF_DIV_SUBTYPE>();
            BEREF_DIV_SUBTYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIV_SUBTYPE_GET_by_DAD_ID"))
                {
                    db.AddInParameter(cm, "@DAD_ID", DbType.Decimal, DAD_ID);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIV_SUBTYPE();
                            item.DAD_STYPE = dr.GetDecimal(dr.GetOrdinal("DAD_STYPE"));
                            item.DAD_SNAME = dr.GetString(dr.GetOrdinal("DAD_SNAME"));
                            item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
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

        public bool REF_DIV_SUBTYPE_Ins(BEREF_DIV_SUBTYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_SUBTYPE_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, en.DAD_ID);
                db.AddInParameter(oDbCommand, "@DAD_SNAME", DbType.String, en.DAD_SNAME);
                db.AddInParameter(oDbCommand, "@DAD_NAME", DbType.String, en.DAD_NAME);
                db.AddInParameter(oDbCommand, "@DAD_BELONGS", DbType.String, en.DAD_BELONGS);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_DIV_SUBTYPE_Upd(BEREF_DIV_SUBTYPE en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_SUBTYPE_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, en.DAD_ID);
                db.AddInParameter(oDbCommand, "@DAD_STYPE", DbType.Decimal, en.DAD_STYPE);
                db.AddInParameter(oDbCommand, "@DAD_SNAME", DbType.String, en.DAD_SNAME);
                db.AddInParameter(oDbCommand, "@DAD_NAME", DbType.String, en.DAD_NAME);
                db.AddInParameter(oDbCommand, "@DAD_BELONGS", DbType.String, en.DAD_BELONGS);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REF_DIV_SUBTYPE_Del(string OWNER, decimal DAD_STYPE)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIV_SUBTYPE_Del");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, OWNER);
                db.AddInParameter(oDbCommand, "@DAD_STYPE", DbType.Decimal, DAD_STYPE);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BEREF_DIV_SUBTYPE> USP_BUSCAR_UBIGEO(decimal codigo, string ubigeo)
        {
            List<BEREF_DIV_SUBTYPE> lst = new List<BEREF_DIV_SUBTYPE>();
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("USP_BUSCAR_UBIGEO"))
                {
                    db.AddInParameter(cm, "@TIS_N", DbType.Decimal, codigo);
                    db.AddInParameter(cm, "@DAD_VNAME", DbType.String, ubigeo);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                            lst.Add(new BEREF_DIV_SUBTYPE(dr));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public List<BEREF_DIV_SUBTYPE> Listar(string owner, decimal id, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_SUBDIVISION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_SUBDIVISION", owner, id, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEREF_DIV_SUBTYPE>();
            BEREF_DIV_SUBTYPE div;
            using (IDataReader dr = db.ExecuteReader(oDbCommand1))
            {
                while (dr.Read())
                {
                    div = new BEREF_DIV_SUBTYPE();
                    div.DAD_STYPE = dr.GetDecimal(dr.GetOrdinal("DAD_STYPE"));
                    div.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    div.DAD_SNAME = dr.GetString(dr.GetOrdinal("DAD_SNAME"));
                    div.DAD_BELONGS = dr.GetDecimal(dr.GetOrdinal("DAD_BELONGS"));
                    div.SUBDIVISION = dr.GetString(dr.GetOrdinal("SUBDIVISION")).ToUpper();
                    div.DEPENDENCIA = dr.GetString(dr.GetOrdinal("DEPENDENCIA")).ToUpper();
                    div.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(div);
                }
            }
            return lista;
        }

        

        public int Eliminar(BEREF_DIV_SUBTYPE division)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_SUBDIVISION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, division.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_STYPE", DbType.Decimal, division.DAD_STYPE);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, division.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int insertar(BEREF_DIV_SUBTYPE division)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_SUBDIVISION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, division.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, division.DAD_ID);
            db.AddInParameter(oDbCommand, "@DAD_SNAME", DbType.String, division.DAD_SNAME.ToUpper());
            db.AddInParameter(oDbCommand, "@DAD_NAME", DbType.String, division.DAD_NAME.ToUpper());
            db.AddInParameter(oDbCommand, "@DAD_BELONGS", DbType.Decimal, division.DAD_BELONGS);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, division.LOG_USER_CREAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEREF_DIV_SUBTYPE> ListarSubdivisionDep(string owner, decimal id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_SUBDIVISION_NAME");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, id);

            var lista = new List<BEREF_DIV_SUBTYPE>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEREF_DIV_SUBTYPE obj;
                while (dr.Read())
                {
                    obj = new BEREF_DIV_SUBTYPE();
                    obj.DAD_STYPE = dr.GetDecimal(dr.GetOrdinal("DAD_STYPE"));
                    obj.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                    lista.Add(obj);
                }
            }
            return lista;
        }

        public List<BEREF_DIV_SUBTYPE> ListarSubdivision(string owner, decimal id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_SUBDIVISION_ID");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, id);

            var lista = new List<BEREF_DIV_SUBTYPE>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEREF_DIV_SUBTYPE obj;
                while (dr.Read())
                {
                    obj = new BEREF_DIV_SUBTYPE();
                    obj.DAD_STYPE = dr.GetDecimal(dr.GetOrdinal("DAD_STYPE"));
                    obj.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                    lista.Add(obj);
                }
            }
            return lista;
        }

        public int ObtenerXAbrev(BEREF_DIV_SUBTYPE subdivision)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_SUBDIVISION_ABREV");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, subdivision.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, subdivision.DAD_ID);
            db.AddInParameter(oDbCommand, "@DAD_SNAME", DbType.String, subdivision.DAD_SNAME);
            BESociedad obj = new BESociedad();
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BETreeview> ArbolSubReporte(string owner, decimal id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_SUBDIVISION_REP");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@DAD_ID", DbType.Decimal, id);

            var lista = new List<BETreeview>();
            BETreeview obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BETreeview();
                    obs.cod = Convert.ToInt32(dr["DAD_STYPE"]);
                    obs.text = Convert.ToString(dr["DAD_NAME"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("DAD_BELONGS")))
                        obs.ManagerID = Convert.ToInt32(dr["DAD_BELONGS"]);
                    else
                        obs.ManagerID = 0;
                    lista.Add(obs);
                }
            }
            return lista;
        }

    }
}
