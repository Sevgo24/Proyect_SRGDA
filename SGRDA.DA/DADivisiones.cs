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
    public class DADivisiones
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEDivision> Listar()
        {
            List<BEDivision> lst = new List<BEDivision>();
            BEDivision item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIVISIONES_VALUES_Get"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEDivision();
                            item.DADV_ID = dr.GetDecimal(dr.GetOrdinal("DADV_ID"));
                            item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                            item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                            item.DAD_STYPE = dr.GetDecimal(dr.GetOrdinal("DAD_STYPE"));
                            item.DAD_VCODE = dr.GetString(dr.GetOrdinal("DAD_VCODE"));
                            item.DAD_VNAME = dr.GetString(dr.GetOrdinal("DAD_VNAME"));
                            item.DAD_BELONGS = dr.GetString(dr.GetOrdinal("DAD_BELONGS"));
                            item.DAD_CODE = dr.GetString(dr.GetOrdinal("DAD_CODE"));
                            item.DAD_SNAME = dr.GetString(dr.GetOrdinal("DAD_SNAME"));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dcodigo"></param>
        /// <returns></returns>
        public List<BEDivision> ListarPorCodigo(decimal dcodigo)
        {
            List<BEDivision> lst = new List<BEDivision>();
            BEDivision item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIVISIONES_VALUES_GET_by_DADV_ID"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                    db.AddInParameter(cm, "@DADV_ID", DbType.Decimal, dcodigo);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEDivision();
                            item.DADV_ID = dr.GetDecimal(dr.GetOrdinal("DADV_ID"));
                            item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                            item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                            item.DAD_STYPE = dr.GetDecimal(dr.GetOrdinal("DAD_STYPE"));
                            item.DAD_VCODE = dr.GetString(dr.GetOrdinal("DAD_VCODE"));
                            item.DAD_VNAME = dr.GetString(dr.GetOrdinal("DAD_VNAME"));
                            item.DAD_BELONGS = dr.GetString(dr.GetOrdinal("DAD_BELONGS"));
                            item.DAD_CODE = dr.GetString(dr.GetOrdinal("DAD_CODE"));
                            item.DAD_SNAME = dr.GetString(dr.GetOrdinal("DAD_SNAME"));
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

        public List<BEDivision> usp_Get_DivisionPage(string param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_DIVISIONES_VALUES_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REF_DIVISIONES_VALUES_GET_Page", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEDivision>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEDivision(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEDivision> ListarPorSubtipo(decimal dSubTipoDivision)
        {
            List<BEDivision> lst = new List<BEDivision>();
            BEDivision item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REF_DIVISIONES_VALUES_GET_by_DAD_STYPE"))
                {
                    db.AddInParameter(cm, "@DAD_STYPE", DbType.Decimal, dSubTipoDivision);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEDivision();
                            item.DAD_VCODE = dr.GetString(dr.GetOrdinal("DAD_VCODE"));
                            item.DAD_VNAME = dr.GetString(dr.GetOrdinal("DAD_VNAME"));
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

        public int Insertar(BEDivision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIVISIONES_VALUES_Ins");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, en.DAD_ID);
            db.AddInParameter(oDbCommand, "@DAD_STYPE", DbType.String, en.DAD_STYPE);
            db.AddInParameter(oDbCommand, "@DAD_VCODE", DbType.String, en.DAD_VCODE.ToUpper());
            db.AddInParameter(oDbCommand, "@DAD_VNAME", DbType.String, en.DAD_VNAME.ToUpper());
            db.AddInParameter(oDbCommand, "@DAD_BELONGS", DbType.String, en.DAD_BELONGS.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Update(BEDivision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIVISIONES_VALUES_Upd");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@DADV_ID", DbType.String, en.DADV_ID);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, en.DAD_ID);
            db.AddInParameter(oDbCommand, "@DAD_STYPE", DbType.String, en.DAD_STYPE);
            db.AddInParameter(oDbCommand, "@DAD_VCODE", DbType.String, en.DAD_VCODE.ToUpper());
            db.AddInParameter(oDbCommand, "@DAD_VNAME", DbType.String, en.DAD_VNAME.ToUpper());
            db.AddInParameter(oDbCommand, "@DAD_BELONGS", DbType.String, en.DAD_BELONGS.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(decimal dCodigo)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REF_DIVISIONES_VALUES_Del");
            db.AddInParameter(oDbCommand, "@DADV_ID", DbType.Decimal, dCodigo);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ValidarUbigeoXOficina(string owner, int oficina, int ubigeo)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SP_VALIDAR_UBIGEOS_X_OFICINA");
            db.AddInParameter(oDbCommand, "@OFICNA", DbType.Int32, oficina);
            db.AddInParameter(oDbCommand, "@UBIGEO", DbType.Int32, ubigeo);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ObtenerUbigeoXEstablecimiento(string owner, int est_id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SP_OBTENER_UBIGEOS_X_EST");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Int32, est_id);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }



        public List<BEREF_DIV_SUBTYPE> ListarSubTipoDivisiones(decimal idDivision)
        {
            List<BEREF_DIV_SUBTYPE> lst = new List<BEREF_DIV_SUBTYPE>();
            BEREF_DIV_SUBTYPE item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_DIV_SUBTIPO"))
                {
                    db.AddInParameter(cm, "@DAD_ID", DbType.Decimal, idDivision);
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIV_SUBTYPE();
                            item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                            item.DAD_STYPE = dr.GetDecimal(dr.GetOrdinal("DAD_STYPE"));
                            item.DAD_SNAME = dr.GetString(dr.GetOrdinal("DAD_SNAME"));
                            item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
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

        public List<BEDivision> ListarValoresXsubtipo_Division(decimal idDivision, decimal idSubTipo, decimal idBelong)
        {
            List<BEDivision> lst = new List<BEDivision>();
            BEDivision item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_DIV_VALORES_X_SUBTIPO"))
                {
                    db.AddInParameter(cm, "@DAD_ID", DbType.Decimal, idDivision);
                    db.AddInParameter(cm, "@DAD_STYPE", DbType.Decimal, idSubTipo);
                    db.AddInParameter(cm, "@DAD_BELONGS", DbType.Decimal, idBelong);
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEDivision();
                            item.DADV_ID = dr.GetDecimal(dr.GetOrdinal("DADV_ID"));
                            item.DAD_VNAME = dr.GetString(dr.GetOrdinal("DAD_VNAME"));
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


    }
}
