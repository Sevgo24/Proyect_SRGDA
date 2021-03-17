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
    public class DAREC_TAX_ID
    {   
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREC_TAX_ID> Get_REC_TAX_ID()
        {
            List<BEREC_TAX_ID> lst = new List<BEREC_TAX_ID>();
            BEREC_TAX_ID item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_TAX_ID_LISITEM"))
                {
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_TAX_ID();
                            item.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                            item.TAXN_NAME = dr.GetString(dr.GetOrdinal("TEXT"));
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

        public List<BEREC_TAX_ID> REC_TAX_ID_GET_by_TAXT_ID(decimal TAXT_ID)
        {
            List<BEREC_TAX_ID> lst = new List<BEREC_TAX_ID>();
            BEREC_TAX_ID item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("usp_REC_TAX_ID_GET_by_TAXT_ID"))
                {
                    db.AddInParameter(cm, "@TAXT_ID", DbType.Decimal, TAXT_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREC_TAX_ID();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                            item.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                            item.TAXN_POS = dr.GetDecimal(dr.GetOrdinal("TAXN_POS"));
                            item.TEXT_DESCRIPTION = dr.GetString(dr.GetOrdinal("TEXT_DESCRIPTION"));
                            item.NAME_TER = dr.GetString(dr.GetOrdinal("NAME_TER"));
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

        public List<BEREC_TAX_ID> REC_TAX_ID_Page(string param, string owner, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_DOCUMENTOS_IDENTIFICADORES");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREC_TAX_ID>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREC_TAX_ID(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public bool REC_TAX_ID_Ins(BEREC_TAX_ID en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_TAX_ID_Ins");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
                db.AddInParameter(oDbCommand, "@TAXN_NAME", DbType.String, en.TAXN_NAME.ToUpper());
                db.AddInParameter(oDbCommand, "@TAXN_POS", DbType.Decimal, en.TAXN_POS);
                db.AddInParameter(oDbCommand, "@TEXT_DESCRIPTION", DbType.String, en.TEXT_DESCRIPTION.ToUpper());
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_TAX_ID_Upd(BEREC_TAX_ID en)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_TAX_ID_Upd");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@TAXT_ID", DbType.Decimal, en.TAXT_ID);
                db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, en.TIS_N);
                db.AddInParameter(oDbCommand, "@TAXN_NAME", DbType.String, en.TAXN_NAME);
                db.AddInParameter(oDbCommand, "@TAXN_POS", DbType.Decimal, en.TAXN_POS);
                db.AddInParameter(oDbCommand, "@TEXT_DESCRIPTION", DbType.String, en.TEXT_DESCRIPTION != null ? en.TEXT_DESCRIPTION.ToUpper() : string.Empty);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDAT);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool REC_TAX_ID_Del(decimal TAXT_ID)
        {
            bool exito = false;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_TAX_ID_Del");
                db.AddInParameter(oDbCommand, "@TAXT_ID", DbType.Decimal, TAXT_ID);
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                exito = db.ExecuteNonQuery(oDbCommand) > 0;
                return exito;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///  addon dbs 20140727 - OBTIENE UN ITEM DE LA TABLA TIPO DE DOCUMENTO DE IDENTIFICACION (DNI RUC, ENTRE OTROS)
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="idTipo"></param>
        /// <returns></returns>
        public BEREC_TAX_ID Obtener(string Owner, decimal idTipo)
        {

            BEREC_TAX_ID item = null;
            using (DbCommand cm = db.GetStoredProcCommand("USP_REC_OBTIENE_TIPO_DNI"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                db.AddInParameter(cm, "@TAXT_ID", DbType.String, idTipo);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREC_TAX_ID();
                        item.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        item.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        item.TAXN_POS = dr.GetDecimal(dr.GetOrdinal("TAXN_POS"));
                        item.TEXT_DESCRIPTION = dr.GetString(dr.GetOrdinal("TEXT_DESCRIPTION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS"))) item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                }
            }

            return item;
        }
    }
}
