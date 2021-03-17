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
    public class DASocioNegocioBanco
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int InsertarSocioNegocioBanco(BEREC_BANKS_BPS en)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_SOCIONEGOCIOBANCO");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@BNK_ID", DbType.String, en.BNK_ID);
                db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, en.BRCH_ID);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.String, en.BPS_ID);
                db.AddInParameter(oDbCommand, "@ROL_ID", DbType.Decimal, en.ROL_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                retorno = db.ExecuteNonQuery(oDbCommand);
                return retorno;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int ActualizarSocioNegocioBanco(BEREC_BANKS_BPS en)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_SOCIONEGOCIOBANCO");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
                db.AddInParameter(oDbCommand, "@BNK_ID", DbType.String, en.BNK_ID);
                db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, en.BRCH_ID);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.String, en.BPS_ID);
                db.AddInParameter(oDbCommand, "@ROL_ID", DbType.Decimal, en.ROL_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                retorno = db.ExecuteNonQuery(oDbCommand);
                return retorno;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int ActualizarSocioNegocioBancoId(BEREC_BANKS_BPS en, decimal BancoId, decimal auxBancoId)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_SOCIONEGOCIOBANCO_ID");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
                db.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, BancoId);
                db.AddInParameter(oDbCommand, "@auxBNK_ID", DbType.Decimal, auxBancoId);
                db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, en.BRCH_ID);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.String, en.BPS_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
                retorno = db.ExecuteNonQuery(oDbCommand);
                return retorno;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<BEREC_BANKS_BPS> SocioNegocioBancoXSucursalesListar(string idSucursal, string owner, decimal idBank)
        {
            List<BEREC_BANKS_BPS> contacto = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_CONTACTO_SUC"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@BRCH_ID", DbType.String, idSucursal);
                oDataBase.AddInParameter(cm, "@BNK_ID", DbType.Decimal, idBank);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEREC_BANKS_BPS ObjCon = null;
                    contacto = new List<BEREC_BANKS_BPS>();
                    while (dr.Read())
                    {
                        ObjCon = new BEREC_BANKS_BPS();
                        ObjCon.Id = dr.GetDecimal(dr.GetOrdinal("Id"));
                        ObjCon.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                        ObjCon.BRCH_ID = dr.GetString(dr.GetOrdinal("BRCH_ID"));
                        ObjCon.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        ObjCon.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                        ObjCon.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        ObjCon.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            ObjCon.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));

                        ObjCon.ROL_ID = dr.GetDecimal(dr.GetOrdinal("ROL_ID"));
                        ObjCon.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjCon.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        contacto.Add(ObjCon);
                    }
                }
            }
            return contacto;
        }


        public List<BEREC_BANKS_BPS> SocioNegocioBancoXSucursalesObtener(string idSucursal, string owner, decimal idBank, decimal idContacto)
        {
            List<BEREC_BANKS_BPS> contacto = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CONTACTO_SUC"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@BRCH_ID", DbType.String, idSucursal);
                oDataBase.AddInParameter(cm, "@BNK_ID", DbType.Decimal, idBank);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.String, idContacto);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEREC_BANKS_BPS ObjCon = null;
                    contacto = new List<BEREC_BANKS_BPS>();
                    while (dr.Read())
                    {
                        ObjCon = new BEREC_BANKS_BPS();
                        if (!dr.IsDBNull(dr.GetOrdinal("BNK_ID")))
                            ObjCon.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                        ObjCon.BRCH_ID = dr.GetString(dr.GetOrdinal("BRCH_ID"));
                        ObjCon.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        ObjCon.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                        ObjCon.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        ObjCon.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        ObjCon.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        ObjCon.ROL_ID = dr.GetDecimal(dr.GetOrdinal("ROL_ID"));
                        ObjCon.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                        contacto.Add(ObjCon);
                    }
                }
            }
            return contacto;
        }

        public int Eliminar(string owner, string idsucursal, string idSocio, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVAR_SOCIONEGOCIOBANCO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, idsucursal);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.String, idSocio);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(string owner, string idsucursal, string idSocio, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_SOCIONEGOCIOBANCO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, idsucursal);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.String, idSocio);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
