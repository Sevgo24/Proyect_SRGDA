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
    public class DADirecciones
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEDireccion> usp_Get_DireccionPage(decimal param, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("REC_ADDRESS_LISTARPAGE");
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, param);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("REC_ADDRESS_LISTARPAGE", param, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEDireccion>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEDireccion(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        //public List<BEDireccion> USP_REC_ADDRESS_LISTAR(decimal BPS_ID)
        //{
        //    BEDireccion be = null;
        //    List<BEDireccion> lista = new List<BEDireccion>();

        //    DbCommand oDbCommand = db.GetStoredProcCommand("USP_REC_ADDRESS_LISTAR");
        //    db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID);
        //    db.ExecuteNonQuery(oDbCommand);

        //    using (IDataReader reader = db.ExecuteReader(oDbCommand))
        //    {
        //        while (reader.Read())
        //        {
        //            be = new BEDireccion();
        //            be.BPS_ID = reader.GetDecimal(reader.GetOrdinal("BPS_ID"));
        //            be.ADD_ID = reader.GetDecimal(reader.GetOrdinal("ADD_ID"));
        //            be.ADD_TYPE = reader.GetDecimal(reader.GetOrdinal("ADD_TYPE"));
        //            be.DESCRIPTION = reader.GetString(reader.GetOrdinal("DESCRIPTION"));
        //            be.ADDRESS = reader.GetString(reader.GetOrdinal("ADDRESS"));

        //            lista.Add(be);
        //        }
        //    }
        //    return lista;
        //}

        public int Insertar(BEDireccion en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_DIRECCION_GRAL");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@ADD_TYPE", DbType.Decimal, en.ADD_TYPE);
                db.AddInParameter(oDbCommand, "@ENT_ID", DbType.Decimal, en.ENT_ID);
                db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
                db.AddInParameter(oDbCommand, "@GEO_ID", DbType.Decimal, en.GEO_ID);
                db.AddInParameter(oDbCommand, "@ROU_ID", DbType.Decimal, en.ROU_ID);
                if (en.ROU_NAME == null)
                    db.AddInParameter(oDbCommand, "@ROU_NAME", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@ROU_NAME", DbType.String, en.ROU_NAME.ToUpper());

                if (en.ROU_NUM == null)
                    db.AddInParameter(oDbCommand, "@ROU_NUM", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@ROU_NUM", DbType.String, en.ROU_NUM.ToUpper());

                if (en.HOU_TURZN == null)
                    db.AddInParameter(oDbCommand, "@HOU_TURZN", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@HOU_TURZN", DbType.String, en.HOU_TURZN.ToUpper());

                if (en.HOU_URZN == null)
                    db.AddInParameter(oDbCommand, "@HOU_URZN", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@HOU_URZN", DbType.String, en.HOU_URZN.ToUpper());

                db.AddInParameter(oDbCommand, "@HOU_NRO", DbType.String, en.HOU_NRO);

                if (en.HOU_MZ == null)
                    db.AddInParameter(oDbCommand, "@HOU_MZ", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@HOU_MZ", DbType.String, en.HOU_MZ.ToUpper());

                if (en.HOU_LOT == null)
                    db.AddInParameter(oDbCommand, "@HOU_LOT", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@HOU_LOT", DbType.String, en.HOU_LOT.ToUpper());

                if (en.HOU_TETP == null)
                    db.AddInParameter(oDbCommand, "@HOU_TETP", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@HOU_TETP", DbType.String, en.HOU_TETP.ToUpper());

                if (en.HOU_NETP == null)
                    db.AddInParameter(oDbCommand, "@HOU_NETP", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@HOU_NETP", DbType.String, en.HOU_NETP.ToUpper());

                if (en.ADD_TINT == null)
                    db.AddInParameter(oDbCommand, "@ADD_TINT", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@ADD_TINT", DbType.String, en.ADD_TINT.ToUpper());

                if (en.ADD_INT == null)
                    db.AddInParameter(oDbCommand, "@ADD_INT", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@ADD_INT", DbType.String, en.ADD_INT.ToUpper());

                if (en.ADD_ADDTL == null)
                    db.AddInParameter(oDbCommand, "@ADD_ADDTL", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@ADD_ADDTL", DbType.String, en.ADD_ADDTL.ToUpper());

                if (en.ADD_REFER == null)
                    db.AddInParameter(oDbCommand, "@ADD_REFER", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@ADD_REFER", DbType.String, en.ADD_REFER.ToUpper());

                if (en.ADDRESS == null)
                    db.AddInParameter(oDbCommand, "@ADDRESS", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@ADDRESS", DbType.String, en.ADDRESS.ToUpper());

                db.AddInParameter(oDbCommand, "@CPO_ID", DbType.Decimal, en.CPO_ID);
                if (en.REMARK == null)
                    db.AddInParameter(oDbCommand, "@REMARK", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@REMARK", DbType.String, en.REMARK.ToUpper());

                if (en.MAIN_ADD == null)
                    db.AddInParameter(oDbCommand, "@MAIN_ADD", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(oDbCommand, "@MAIN_ADD", DbType.String, en.MAIN_ADD);

                db.AddInParameter(oDbCommand, "@ADD_CX", DbType.Decimal, en.ADD_CX);
                db.AddInParameter(oDbCommand, "@ADD_CY", DbType.Decimal, en.ADD_CY);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                db.AddOutParameter(oDbCommand, "@ADD_ID", DbType.Decimal, Convert.ToInt32(en.ADD_ID));

                int n = db.ExecuteNonQuery(oDbCommand);
                int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@ADD_ID"));

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(BEDireccion en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_DIRECCION_GRAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, en.ADD_ID);
            db.AddInParameter(oDbCommand, "@ADD_TYPE", DbType.Decimal, en.ADD_TYPE);
            db.AddInParameter(oDbCommand, "@ENT_ID", DbType.Decimal, en.ENT_ID);
            db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
            db.AddInParameter(oDbCommand, "@GEO_ID", DbType.Decimal, en.GEO_ID);
            db.AddInParameter(oDbCommand, "@ROU_ID", DbType.Decimal, en.ROU_ID);
            db.AddInParameter(oDbCommand, "@ROU_NAME", DbType.String, en.ROU_NAME == null ? string.Empty : en.ROU_NAME.ToUpper());
            db.AddInParameter(oDbCommand, "@ROU_NUM", DbType.String, en.ROU_NUM == null ? string.Empty : en.ROU_NUM.ToUpper());
            //db.AddInParameter(oDbCommand, "@HOU_TURZN", DbType.String, en.HOU_TURZN == null ? string.Empty : en.HOU_TURZN.ToUpper());
            if (en.HOU_TURZN == null)
                db.AddInParameter(oDbCommand, "@HOU_TURZN", DbType.String, DBNull.Value);
            else
                db.AddInParameter(oDbCommand, "@HOU_TURZN", DbType.String, en.HOU_TURZN.ToUpper());

            //db.AddInParameter(oDbCommand, "@HOU_TURZN", DbType.Decimal, en.HOU_TURZN);
            //if (en.HOU_TURZN == null) db.AddInParameter(oDbCommand, "@HOU_TURZN", DbType.Decimal, 0);           
            //else db.AddInParameter(oDbCommand, "@HOU_TURZN", DbType.Decimal, en.HOU_TURZN);

            db.AddInParameter(oDbCommand, "@HOU_URZN", DbType.String, en.HOU_URZN == null ? string.Empty : en.HOU_URZN.ToUpper());

            db.AddInParameter(oDbCommand, "@HOU_NRO", DbType.String, en.HOU_NRO);
            db.AddInParameter(oDbCommand, "@HOU_MZ", DbType.String, en.HOU_MZ == null ? string.Empty : en.HOU_MZ.ToUpper());
            db.AddInParameter(oDbCommand, "@HOU_LOT", DbType.String, en.HOU_LOT == null ? string.Empty : en.HOU_LOT.ToUpper());


            decimal? HOU_TETP;
            if (en.HOU_TETP == null)
            {
                HOU_TETP = null;
            }
            else
            {
                HOU_TETP = Convert.ToDecimal(en.HOU_TETP);
            }



            db.AddInParameter(oDbCommand, "@HOU_TETP", DbType.Decimal, HOU_TETP);

            db.AddInParameter(oDbCommand, "@HOU_NETP", DbType.String, en.HOU_NETP == null ? string.Empty : en.HOU_NETP.ToUpper());
            db.AddInParameter(oDbCommand, "@ADD_TINT", DbType.String, en.ADD_TINT == null ? string.Empty : en.ADD_TINT.ToUpper());
            db.AddInParameter(oDbCommand, "@ADD_INT", DbType.String, en.ADD_INT == null ? string.Empty : en.ADD_INT.ToUpper());
            db.AddInParameter(oDbCommand, "@ADD_ADDTL", DbType.String, en.ADD_ADDTL == null ? string.Empty : en.ADD_ADDTL.ToUpper());
            db.AddInParameter(oDbCommand, "@ADD_REFER", DbType.String, en.ADD_REFER == null ? string.Empty : en.ADD_REFER.ToUpper());
            db.AddInParameter(oDbCommand, "@ADDRESS", DbType.String, en.ADDRESS == null ? string.Empty : en.ADDRESS.ToUpper());
            db.AddInParameter(oDbCommand, "@CPO_ID", DbType.Decimal, en.CPO_ID);
            db.AddInParameter(oDbCommand, "@REMARK", DbType.String, en.REMARK == null ? string.Empty : en.REMARK.ToUpper());
            db.AddInParameter(oDbCommand, "@MAIN_ADD", DbType.String, en.MAIN_ADD);

            db.AddInParameter(oDbCommand, "@ADD_CX", DbType.Decimal, en.ADD_CX);
            db.AddInParameter(oDbCommand, "@ADD_CY", DbType.Decimal, en.ADD_CY);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, Convert.ToString(en.LOG_USER_UPDATE).ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="codigoBps"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public List<BEDireccion> DireccionXSocio(decimal codigoBps, string owner, decimal tipoEntidad)
        {
            List<BEDireccion> direcciones = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DIRECCION_BPS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, codigoBps);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEDireccion ObjDir = null;
                    direcciones = new List<BEDireccion>();
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        ObjDir.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        ObjDir.ROU_ID = dr.GetDecimal(dr.GetOrdinal("ROU_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ROU_NAME")))
                            ObjDir.ROU_NAME = dr.GetString(dr.GetOrdinal("ROU_NAME"));
                        else
                            ObjDir.ROU_NAME = "";


                        ObjDir.ROU_NUM = dr.GetString(dr.GetOrdinal("ROU_NUM"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TURZN")))
                        {
                            ObjDir.HOU_TURZN = dr.GetString(dr.GetOrdinal("HOU_TURZN"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_URZN"))) ObjDir.HOU_URZN = dr.GetString(dr.GetOrdinal("HOU_URZN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NRO"))) ObjDir.HOU_NRO = dr.GetString(dr.GetOrdinal("HOU_NRO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_MZ"))) ObjDir.HOU_MZ = dr.GetString(dr.GetOrdinal("HOU_MZ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_LOT"))) ObjDir.HOU_LOT = dr.GetString(dr.GetOrdinal("HOU_LOT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NETP"))) ObjDir.HOU_NETP = dr.GetString(dr.GetOrdinal("HOU_NETP"));


                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TETP"))) ObjDir.HOU_TETP = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TETP")));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_TINT"))) ObjDir.ADD_TINT = dr.GetString(dr.GetOrdinal("ADD_TINT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_INT"))) ObjDir.ADD_INT = dr.GetString(dr.GetOrdinal("ADD_INT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ADDTL"))) ObjDir.ADD_ADDTL = dr.GetString(dr.GetOrdinal("ADD_ADDTL"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_REFER"))) ObjDir.ADD_REFER = dr.GetString(dr.GetOrdinal("ADD_REFER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS"))) ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CPO_ID"))) ObjDir.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REMARK"))) ObjDir.REMARK = dr.GetString(dr.GetOrdinal("REMARK"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MAIN_ADD"))) ObjDir.MAIN_ADD = Convert.ToChar(dr.GetValue(dr.GetOrdinal("MAIN_ADD")));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CX"))) ObjDir.ADD_CX = dr.GetDecimal(dr.GetOrdinal("ADD_CX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CY"))) ObjDir.ADD_CY = dr.GetDecimal(dr.GetOrdinal("ADD_CY"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjDir.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            ObjDir.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            ObjDir.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            ObjDir.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            ObjDir.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        direcciones.Add(ObjDir);
                    }
                }
            }
            return direcciones;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idDireccion"></param>
        /// <param name="idBps"></param>
        /// <returns></returns>
        public BEDireccion ObtenerDirBPS(string owner, decimal idDireccion, decimal idBps, decimal idEntidad)
        {
            BEDireccion ObjDir = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DIRECCION_BPS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@ADD_ID", DbType.Decimal, idDireccion);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        ObjDir.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        ObjDir.ROU_ID = dr.GetDecimal(dr.GetOrdinal("ROU_ID"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ROU_NAME")))
                            ObjDir.ROU_NAME = dr.GetString(dr.GetOrdinal("ROU_NAME"));
                        else
                            ObjDir.ROU_NAME = "";

                        ObjDir.ROU_NUM = dr.GetString(dr.GetOrdinal("ROU_NUM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TURZN")))
                        {
                            ObjDir.HOU_TURZN = dr.GetString(dr.GetOrdinal("HOU_TURZN"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_URZN")))
                            ObjDir.HOU_URZN = dr.GetString(dr.GetOrdinal("HOU_URZN"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NRO")))
                            ObjDir.HOU_NRO = dr.GetString(dr.GetOrdinal("HOU_NRO"));


                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_MZ")))
                            ObjDir.HOU_MZ = dr.GetString(dr.GetOrdinal("HOU_MZ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_LOT")))
                            ObjDir.HOU_LOT = dr.GetString(dr.GetOrdinal("HOU_LOT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NETP")))
                            ObjDir.HOU_NETP = dr.GetString(dr.GetOrdinal("HOU_NETP"));


                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TETP")))
                            ObjDir.HOU_TETP = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TETP")));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_TINT"))) ObjDir.ADD_TINT = dr.GetString(dr.GetOrdinal("ADD_TINT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_INT"))) ObjDir.ADD_INT = dr.GetString(dr.GetOrdinal("ADD_INT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ADDTL"))) ObjDir.ADD_ADDTL = dr.GetString(dr.GetOrdinal("ADD_ADDTL"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_REFER")))
                            ObjDir.ADD_REFER = dr.GetString(dr.GetOrdinal("ADD_REFER"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                            ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("CPO_ID")))
                            ObjDir.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REMARK")))
                            ObjDir.REMARK = dr.GetString(dr.GetOrdinal("REMARK"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MAIN_ADD"))) ObjDir.MAIN_ADD = Convert.ToChar(dr.GetValue(dr.GetOrdinal("MAIN_ADD")));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CX"))) ObjDir.ADD_CX = dr.GetDecimal(dr.GetOrdinal("ADD_CX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CY"))) ObjDir.ADD_CY = dr.GetDecimal(dr.GetOrdinal("ADD_CY"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjDir.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }

                    }
                }
            }

            return ObjDir;
        }

        public List<BEDireccion> ObtenerDirBPSAll(string owner, decimal idBps)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DIRECCION_BPS_ALL"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);

                List<BEDireccion> lista;
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEDireccion ObjDir;
                    lista = new List<BEDireccion>();
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.ENT_DESC = dr.GetString(dr.GetOrdinal("ENT_DESC"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));

                        lista.Add(ObjDir);
                    }
                }
                return lista;
            }
        }
        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="dirId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Eliminar(string owner, decimal dirId, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_DIRECCION_GRAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, dirId);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="dirId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Activar(string owner, decimal dirId, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_DIRECCION_GRAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, dirId);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEDireccion> DireccionXEstablecimiento(decimal idEstablecimiento, string owner, decimal tipoEntidad)
        {
            List<BEDireccion> direcciones = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DIRECCION_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEstablecimiento);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, tipoEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEDireccion ObjDir = null;
                    direcciones = new List<BEDireccion>();
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        ObjDir.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        ObjDir.ROU_ID = dr.GetDecimal(dr.GetOrdinal("ROU_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ROU_NAME")))
                            ObjDir.ROU_NAME = dr.GetString(dr.GetOrdinal("ROU_NAME"));
                        else
                            ObjDir.ROU_NAME = "";


                        ObjDir.ROU_NUM = dr.GetString(dr.GetOrdinal("ROU_NUM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TURZN")))
                        {
                            ObjDir.HOU_TURZN = dr.GetString(dr.GetOrdinal("HOU_TURZN"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_URZN"))) ObjDir.HOU_URZN = dr.GetString(dr.GetOrdinal("HOU_URZN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NRO"))) ObjDir.HOU_NRO = dr.GetString(dr.GetOrdinal("HOU_NRO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_MZ"))) ObjDir.HOU_MZ = dr.GetString(dr.GetOrdinal("HOU_MZ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_LOT"))) ObjDir.HOU_LOT = dr.GetString(dr.GetOrdinal("HOU_LOT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NETP"))) ObjDir.HOU_NETP = dr.GetString(dr.GetOrdinal("HOU_NETP"));


                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TETP"))) ObjDir.HOU_TETP = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TETP")));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_TINT"))) ObjDir.ADD_TINT = dr.GetString(dr.GetOrdinal("ADD_TINT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_INT"))) ObjDir.ADD_INT = dr.GetString(dr.GetOrdinal("ADD_INT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ADDTL"))) ObjDir.ADD_ADDTL = dr.GetString(dr.GetOrdinal("ADD_ADDTL"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_REFER"))) ObjDir.ADD_REFER = dr.GetString(dr.GetOrdinal("ADD_REFER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS"))) ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CPO_ID"))) ObjDir.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REMARK"))) ObjDir.REMARK = dr.GetString(dr.GetOrdinal("REMARK"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MAIN_ADD"))) ObjDir.MAIN_ADD = Convert.ToChar(dr.GetValue(dr.GetOrdinal("MAIN_ADD")));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CX"))) ObjDir.ADD_CX = dr.GetDecimal(dr.GetOrdinal("ADD_CX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CY"))) ObjDir.ADD_CY = dr.GetDecimal(dr.GetOrdinal("ADD_CY"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjDir.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            ObjDir.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            ObjDir.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            ObjDir.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            ObjDir.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        direcciones.Add(ObjDir);
                    }
                }
            }
            return direcciones;
        }

        public List<BEDireccion> DireccionXSucursales(string owner, string idSucursal, decimal idBank)
        {
            List<BEDireccion> direcciones = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DIRECCION_SUC"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@BRCH_ID", DbType.String, idSucursal);
                oDataBase.AddInParameter(cm, "@BNK_ID", DbType.String, idBank);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEDireccion ObjDir = null;
                    direcciones = new List<BEDireccion>();
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        ObjDir.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        ObjDir.ROU_ID = dr.GetDecimal(dr.GetOrdinal("ROU_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ROU_NAME")))
                            ObjDir.ROU_NAME = dr.GetString(dr.GetOrdinal("ROU_NAME"));
                        else
                            ObjDir.ROU_NAME = "";

                        ObjDir.ROU_NUM = dr.GetString(dr.GetOrdinal("ROU_NUM"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TURZN")))
                        {
                            var HOU_TURZN = dr.GetDecimal(dr.GetOrdinal("HOU_TURZN"));
                            ObjDir.HOU_TURZN = HOU_TURZN.ToString();
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_URZN")))
                        {
                            ObjDir.HOU_URZN = dr.GetString(dr.GetOrdinal("HOU_URZN"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NRO")))
                        {
                            ObjDir.HOU_NRO = dr.GetString(dr.GetOrdinal("HOU_NRO"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_MZ")))
                            ObjDir.HOU_MZ = dr.GetString(dr.GetOrdinal("HOU_MZ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_LOT")))
                            ObjDir.HOU_LOT = dr.GetString(dr.GetOrdinal("HOU_LOT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NETP")))
                            ObjDir.HOU_NETP = dr.GetString(dr.GetOrdinal("HOU_NETP"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TETP")))
                            ObjDir.HOU_TETP = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TETP")));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_TINT"))) ObjDir.ADD_TINT = dr.GetString(dr.GetOrdinal("ADD_TINT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_INT"))) ObjDir.ADD_INT = dr.GetString(dr.GetOrdinal("ADD_INT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ADDTL"))) ObjDir.ADD_ADDTL = dr.GetString(dr.GetOrdinal("ADD_ADDTL"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_REFER")))
                            ObjDir.ADD_REFER = dr.GetString(dr.GetOrdinal("ADD_REFER"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                            ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CPO_ID")))
                            ObjDir.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REMARK")))
                            ObjDir.REMARK = dr.GetString(dr.GetOrdinal("REMARK"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MAIN_ADD"))) ObjDir.MAIN_ADD = Convert.ToChar(dr.GetValue(dr.GetOrdinal("MAIN_ADD")));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CX"))) ObjDir.ADD_CX = dr.GetDecimal(dr.GetOrdinal("ADD_CX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CY"))) ObjDir.ADD_CY = dr.GetDecimal(dr.GetOrdinal("ADD_CY"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjDir.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        direcciones.Add(ObjDir);
                    }
                }
            }
            return direcciones;
        }

        public BEDireccion ObtenerDirEst(string owner, decimal idDireccion, decimal idEst)
        {
            BEDireccion ObjDir = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DIRECCION_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@ADD_ID", DbType.Decimal, idDireccion);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        ObjDir.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        ObjDir.ROU_ID = dr.GetDecimal(dr.GetOrdinal("ROU_ID"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ROU_NAME")))
                            ObjDir.ROU_NAME = dr.GetString(dr.GetOrdinal("ROU_NAME"));
                        else
                            ObjDir.ROU_NAME = "";

                        ObjDir.ROU_NUM = dr.GetString(dr.GetOrdinal("ROU_NUM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TURZN")))
                        {
                            ObjDir.HOU_TURZN = dr.GetString(dr.GetOrdinal("HOU_TURZN"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_URZN")))
                        {
                            ObjDir.HOU_URZN = dr.GetString(dr.GetOrdinal("HOU_URZN"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NRO")))
                        {
                            ObjDir.HOU_NRO = dr.GetString(dr.GetOrdinal("HOU_NRO"));

                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_MZ")))
                            ObjDir.HOU_MZ = dr.GetString(dr.GetOrdinal("HOU_MZ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_LOT")))
                            ObjDir.HOU_LOT = dr.GetString(dr.GetOrdinal("HOU_LOT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NETP")))
                            ObjDir.HOU_NETP = dr.GetString(dr.GetOrdinal("HOU_NETP"));



                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TETP")))
                            ObjDir.HOU_TETP = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TETP")));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_TINT"))) ObjDir.ADD_TINT = dr.GetString(dr.GetOrdinal("ADD_TINT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_INT"))) ObjDir.ADD_INT = dr.GetString(dr.GetOrdinal("ADD_INT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ADDTL"))) ObjDir.ADD_ADDTL = dr.GetString(dr.GetOrdinal("ADD_ADDTL"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_REFER")))
                            ObjDir.ADD_REFER = dr.GetString(dr.GetOrdinal("ADD_REFER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                            ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CPO_ID")))
                            ObjDir.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REMARK")))
                            ObjDir.REMARK = dr.GetString(dr.GetOrdinal("REMARK"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MAIN_ADD"))) ObjDir.MAIN_ADD = Convert.ToChar(dr.GetValue(dr.GetOrdinal("MAIN_ADD")));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CX"))) ObjDir.ADD_CX = dr.GetDecimal(dr.GetOrdinal("ADD_CX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CY"))) ObjDir.ADD_CY = dr.GetDecimal(dr.GetOrdinal("ADD_CY"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjDir.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }

                    }
                }
            }

            return ObjDir;
        }


        public List<BEDireccion> DireccionXOficina(decimal codigoOff, string owner)
        {

            List<BEDireccion> direcciones = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DIRECCION_OFF"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, codigoOff);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEDireccion ObjDir = null;
                    direcciones = new List<BEDireccion>();
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        ObjDir.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        ObjDir.ROU_ID = dr.GetDecimal(dr.GetOrdinal("ROU_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ROU_NAME")))
                            ObjDir.ROU_NAME = dr.GetString(dr.GetOrdinal("ROU_NAME"));
                        else
                            ObjDir.ROU_NAME = "";


                        ObjDir.ROU_NUM = dr.GetString(dr.GetOrdinal("ROU_NUM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TURZN")))
                        {
                            ObjDir.HOU_TURZN = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TURZN")));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_URZN"))) ObjDir.HOU_URZN = dr.GetString(dr.GetOrdinal("HOU_URZN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NRO"))) ObjDir.HOU_NRO = dr.GetString(dr.GetOrdinal("HOU_NRO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_MZ"))) ObjDir.HOU_MZ = dr.GetString(dr.GetOrdinal("HOU_MZ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_LOT"))) ObjDir.HOU_LOT = dr.GetString(dr.GetOrdinal("HOU_LOT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NETP"))) ObjDir.HOU_NETP = dr.GetString(dr.GetOrdinal("HOU_NETP"));


                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TETP"))) ObjDir.HOU_TETP = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TETP")));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_TINT"))) ObjDir.ADD_TINT = dr.GetString(dr.GetOrdinal("ADD_TINT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_INT"))) ObjDir.ADD_INT = dr.GetString(dr.GetOrdinal("ADD_INT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ADDTL"))) ObjDir.ADD_ADDTL = dr.GetString(dr.GetOrdinal("ADD_ADDTL"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_REFER"))) ObjDir.ADD_REFER = dr.GetString(dr.GetOrdinal("ADD_REFER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS"))) ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CPO_ID"))) ObjDir.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REMARK"))) ObjDir.REMARK = dr.GetString(dr.GetOrdinal("REMARK"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MAIN_ADD"))) ObjDir.MAIN_ADD = Convert.ToChar(dr.GetValue(dr.GetOrdinal("MAIN_ADD")));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CX"))) ObjDir.ADD_CX = dr.GetDecimal(dr.GetOrdinal("ADD_CX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CY"))) ObjDir.ADD_CY = dr.GetDecimal(dr.GetOrdinal("ADD_CY"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            ObjDir.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            ObjDir.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            ObjDir.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            ObjDir.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            ObjDir.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        direcciones.Add(ObjDir);
                    }
                }
            }
            return direcciones;

        }

        public List<BEDireccion> DireccionXOficinaHistorial(decimal codigoOff, string owner)
        {

            List<BEDireccion> direcciones = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DIRECCION_OFF_HIST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, codigoOff);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEDireccion ObjDir = null;
                    direcciones = new List<BEDireccion>();
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        ObjDir.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        ObjDir.ROU_ID = dr.GetDecimal(dr.GetOrdinal("ROU_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ROU_NAME")))
                            ObjDir.ROU_NAME = dr.GetString(dr.GetOrdinal("ROU_NAME"));
                        else
                            ObjDir.ROU_NAME = "";


                        ObjDir.ROU_NUM = dr.GetString(dr.GetOrdinal("ROU_NUM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TURZN")))
                        {
                            ObjDir.HOU_TURZN = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TURZN")));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_URZN"))) ObjDir.HOU_URZN = dr.GetString(dr.GetOrdinal("HOU_URZN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NRO"))) ObjDir.HOU_NRO = dr.GetString(dr.GetOrdinal("HOU_NRO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_MZ"))) ObjDir.HOU_MZ = dr.GetString(dr.GetOrdinal("HOU_MZ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_LOT"))) ObjDir.HOU_LOT = dr.GetString(dr.GetOrdinal("HOU_LOT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NETP"))) ObjDir.HOU_NETP = dr.GetString(dr.GetOrdinal("HOU_NETP"));


                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TETP"))) ObjDir.HOU_TETP = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TETP")));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_TINT"))) ObjDir.ADD_TINT = dr.GetString(dr.GetOrdinal("ADD_TINT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_INT"))) ObjDir.ADD_INT = dr.GetString(dr.GetOrdinal("ADD_INT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ADDTL"))) ObjDir.ADD_ADDTL = dr.GetString(dr.GetOrdinal("ADD_ADDTL"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_REFER"))) ObjDir.ADD_REFER = dr.GetString(dr.GetOrdinal("ADD_REFER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS"))) ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CPO_ID"))) ObjDir.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REMARK"))) ObjDir.REMARK = dr.GetString(dr.GetOrdinal("REMARK"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MAIN_ADD"))) ObjDir.MAIN_ADD = Convert.ToChar(dr.GetValue(dr.GetOrdinal("MAIN_ADD")));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CX"))) ObjDir.ADD_CX = dr.GetDecimal(dr.GetOrdinal("ADD_CX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CY"))) ObjDir.ADD_CY = dr.GetDecimal(dr.GetOrdinal("ADD_CY"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            ObjDir.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            ObjDir.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            ObjDir.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            ObjDir.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            ObjDir.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        direcciones.Add(ObjDir);
                    }
                }
            }
            return direcciones;

        }

        public BEDireccion ObtenerDireccionXOficina(decimal addId, string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            BEDireccion ObjDir = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DIRECCION_OFF"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@ADD_ID", DbType.Decimal, addId);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {

                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        ObjDir.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        ObjDir.ROU_ID = dr.GetDecimal(dr.GetOrdinal("ROU_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ROU_NAME")))
                            ObjDir.ROU_NAME = dr.GetString(dr.GetOrdinal("ROU_NAME"));
                        else
                            ObjDir.ROU_NAME = "";


                        ObjDir.ROU_NUM = dr.GetString(dr.GetOrdinal("ROU_NUM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TURZN")))
                        {
                            ObjDir.HOU_TURZN = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TURZN")));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_URZN"))) ObjDir.HOU_URZN = dr.GetString(dr.GetOrdinal("HOU_URZN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NRO"))) ObjDir.HOU_NRO = dr.GetString(dr.GetOrdinal("HOU_NRO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_MZ"))) ObjDir.HOU_MZ = dr.GetString(dr.GetOrdinal("HOU_MZ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_LOT"))) ObjDir.HOU_LOT = dr.GetString(dr.GetOrdinal("HOU_LOT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NETP"))) ObjDir.HOU_NETP = dr.GetString(dr.GetOrdinal("HOU_NETP"));


                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TETP"))) ObjDir.HOU_TETP = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TETP")));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_TINT"))) ObjDir.ADD_TINT = dr.GetString(dr.GetOrdinal("ADD_TINT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_INT"))) ObjDir.ADD_INT = dr.GetString(dr.GetOrdinal("ADD_INT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ADDTL"))) ObjDir.ADD_ADDTL = dr.GetString(dr.GetOrdinal("ADD_ADDTL"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_REFER"))) ObjDir.ADD_REFER = dr.GetString(dr.GetOrdinal("ADD_REFER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS"))) ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CPO_ID"))) ObjDir.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REMARK"))) ObjDir.REMARK = dr.GetString(dr.GetOrdinal("REMARK"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MAIN_ADD"))) ObjDir.MAIN_ADD = Convert.ToChar(dr.GetValue(dr.GetOrdinal("MAIN_ADD")));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CX"))) ObjDir.ADD_CX = dr.GetDecimal(dr.GetOrdinal("ADD_CX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CY"))) ObjDir.ADD_CY = dr.GetDecimal(dr.GetOrdinal("ADD_CY"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            ObjDir.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            ObjDir.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            ObjDir.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            ObjDir.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            ObjDir.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                    }
                }
            }
            return ObjDir;

        }

        public BEDireccion ObtenerDireccionXId(string owner, decimal Id)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_DIRECCIONES_X_ID"))
            {
                BEDireccion ObjDir = null;
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, Id);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        ObjDir.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        ObjDir.ROU_ID = dr.GetDecimal(dr.GetOrdinal("ROU_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ROU_NAME")))
                            ObjDir.ROU_NAME = dr.GetString(dr.GetOrdinal("ROU_NAME"));
                        else
                            ObjDir.ROU_NAME = "";

                        ObjDir.ROU_NUM = dr.GetString(dr.GetOrdinal("ROU_NUM"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TURZN")))
                        {
                            ObjDir.HOU_TURZN = dr.GetString(dr.GetOrdinal("HOU_TURZN"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_URZN"))) ObjDir.HOU_URZN = dr.GetString(dr.GetOrdinal("HOU_URZN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NRO"))) ObjDir.HOU_NRO = dr.GetString(dr.GetOrdinal("HOU_NRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_MZ"))) ObjDir.HOU_MZ = dr.GetString(dr.GetOrdinal("HOU_MZ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_LOT"))) ObjDir.HOU_LOT = dr.GetString(dr.GetOrdinal("HOU_LOT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NETP"))) ObjDir.HOU_NETP = dr.GetString(dr.GetOrdinal("HOU_NETP"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TETP"))) ObjDir.HOU_TETP = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TETP")));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_TINT"))) ObjDir.ADD_TINT = dr.GetString(dr.GetOrdinal("ADD_TINT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_INT"))) ObjDir.ADD_INT = dr.GetString(dr.GetOrdinal("ADD_INT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ADDTL"))) ObjDir.ADD_ADDTL = dr.GetString(dr.GetOrdinal("ADD_ADDTL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_REFER"))) ObjDir.ADD_REFER = dr.GetString(dr.GetOrdinal("ADD_REFER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS"))) ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CPO_ID"))) ObjDir.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REMARK"))) ObjDir.REMARK = dr.GetString(dr.GetOrdinal("REMARK"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MAIN_ADD"))) ObjDir.MAIN_ADD = Convert.ToChar(dr.GetValue(dr.GetOrdinal("MAIN_ADD")));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CX"))) ObjDir.ADD_CX = dr.GetDecimal(dr.GetOrdinal("ADD_CX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CY"))) ObjDir.ADD_CY = dr.GetDecimal(dr.GetOrdinal("ADD_CY"));
                    }
                }
                return ObjDir;
            }
        }

        public List<BEDireccion> DireccionXAgenteRecaudo(string owner, decimal codigoBps)
        {
            List<BEDireccion> direcciones = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DIRECCION_AGENTE_RECAUDO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, codigoBps);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEDireccion ObjDir = null;
                    direcciones = new List<BEDireccion>();
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        ObjDir.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        ObjDir.ROU_ID = dr.GetDecimal(dr.GetOrdinal("ROU_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ROU_NAME")))
                            ObjDir.ROU_NAME = dr.GetString(dr.GetOrdinal("ROU_NAME"));
                        else
                            ObjDir.ROU_NAME = "";


                        ObjDir.ROU_NUM = dr.GetString(dr.GetOrdinal("ROU_NUM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TURZN")))
                        {
                            ObjDir.HOU_TURZN = dr.GetString(dr.GetOrdinal("HOU_TURZN"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_URZN"))) ObjDir.HOU_URZN = dr.GetString(dr.GetOrdinal("HOU_URZN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NRO"))) ObjDir.HOU_NRO = dr.GetString(dr.GetOrdinal("HOU_NRO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_MZ"))) ObjDir.HOU_MZ = dr.GetString(dr.GetOrdinal("HOU_MZ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_LOT"))) ObjDir.HOU_LOT = dr.GetString(dr.GetOrdinal("HOU_LOT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NETP"))) ObjDir.HOU_NETP = dr.GetString(dr.GetOrdinal("HOU_NETP"));


                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TETP"))) ObjDir.HOU_TETP = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TETP")));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_TINT"))) ObjDir.ADD_TINT = dr.GetString(dr.GetOrdinal("ADD_TINT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_INT"))) ObjDir.ADD_INT = dr.GetString(dr.GetOrdinal("ADD_INT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ADDTL"))) ObjDir.ADD_ADDTL = dr.GetString(dr.GetOrdinal("ADD_ADDTL"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_REFER"))) ObjDir.ADD_REFER = dr.GetString(dr.GetOrdinal("ADD_REFER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS"))) ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CPO_ID"))) ObjDir.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REMARK"))) ObjDir.REMARK = dr.GetString(dr.GetOrdinal("REMARK"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MAIN_ADD"))) ObjDir.MAIN_ADD = Convert.ToChar(dr.GetValue(dr.GetOrdinal("MAIN_ADD")));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CX"))) ObjDir.ADD_CX = dr.GetDecimal(dr.GetOrdinal("ADD_CX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CY"))) ObjDir.ADD_CY = dr.GetDecimal(dr.GetOrdinal("ADD_CY"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjDir.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            ObjDir.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            ObjDir.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            ObjDir.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            ObjDir.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        direcciones.Add(ObjDir);
                    }
                }
            }
            return direcciones;
        }

        public BEDireccion ObtenerDirAgenteRecaudo(string owner, decimal idDireccion, decimal idBps, decimal idEntidad)
        {
            BEDireccion ObjDir = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DIRECCION_AGENTE_RECAUDO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@ADD_ID", DbType.Decimal, idDireccion);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@ENT_ID", DbType.Decimal, idEntidad);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        ObjDir.ADD_TYPE = dr.GetDecimal(dr.GetOrdinal("ADD_TYPE"));
                        ObjDir.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                        ObjDir.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        ObjDir.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                        ObjDir.ROU_ID = dr.GetDecimal(dr.GetOrdinal("ROU_ID"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ROU_NAME")))
                            ObjDir.ROU_NAME = dr.GetString(dr.GetOrdinal("ROU_NAME"));
                        else
                            ObjDir.ROU_NAME = "";

                        ObjDir.ROU_NUM = dr.GetString(dr.GetOrdinal("ROU_NUM"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TURZN")))
                        {
                            ObjDir.HOU_TURZN = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TURZN")));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_URZN")))
                            ObjDir.HOU_URZN = dr.GetString(dr.GetOrdinal("HOU_URZN"));

                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NRO")))
                            ObjDir.HOU_NRO = dr.GetString(dr.GetOrdinal("HOU_NRO"));


                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_MZ")))
                            ObjDir.HOU_MZ = dr.GetString(dr.GetOrdinal("HOU_MZ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_LOT")))
                            ObjDir.HOU_LOT = dr.GetString(dr.GetOrdinal("HOU_LOT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_NETP")))
                            ObjDir.HOU_NETP = dr.GetString(dr.GetOrdinal("HOU_NETP"));


                        if (!dr.IsDBNull(dr.GetOrdinal("HOU_TETP")))
                            ObjDir.HOU_TETP = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("HOU_TETP")));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_TINT"))) ObjDir.ADD_TINT = dr.GetString(dr.GetOrdinal("ADD_TINT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_INT"))) ObjDir.ADD_INT = dr.GetString(dr.GetOrdinal("ADD_INT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ADDTL"))) ObjDir.ADD_ADDTL = dr.GetString(dr.GetOrdinal("ADD_ADDTL"));


                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_REFER")))
                            ObjDir.ADD_REFER = dr.GetString(dr.GetOrdinal("ADD_REFER"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                            ObjDir.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("CPO_ID")))
                            ObjDir.CPO_ID = dr.GetDecimal(dr.GetOrdinal("CPO_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REMARK")))
                            ObjDir.REMARK = dr.GetString(dr.GetOrdinal("REMARK"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MAIN_ADD"))) ObjDir.MAIN_ADD = Convert.ToChar(dr.GetValue(dr.GetOrdinal("MAIN_ADD")));

                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CX"))) ObjDir.ADD_CX = dr.GetDecimal(dr.GetOrdinal("ADD_CX"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_CY"))) ObjDir.ADD_CY = dr.GetDecimal(dr.GetOrdinal("ADD_CY"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjDir.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }

                    }
                }
            }

            return ObjDir;
        }


        public BEDireccion ObtenerUbigeoSocio(string owner, decimal Id)
        {
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_UBIGEO_SOCIO"))
            {
                BEDireccion ObjDir = null;
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, Id);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        ObjDir = new BEDireccion();
                        if (!dr.IsDBNull(dr.GetOrdinal("ADD_ID")))
                            ObjDir.ADD_ID = dr.GetDecimal(dr.GetOrdinal("ADD_ID"));
                        else
                            ObjDir.ROU_NAME = "";

                        if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                            ObjDir.DESCRIPTION = dr.GetString(dr.GetOrdinal("UBIGEO"));
                        else
                            ObjDir.DESCRIPTION = "";
                    }
                }
                return ObjDir;
            }
        }

    }
}
