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
    public class DADivisionesEst
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEDivisionesEst en)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DIVISIONESTABLECIMIENTO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbComand, "@EST_ID", DbType.Decimal, en.EST_ID);
            oDataBase.AddInParameter(oDbComand, "@DADV_ID", DbType.String, en.idDIVISIONVAL);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbComand);
            return n;
        }

        public int Actualizar(BEDivisionesEst en)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_DIVISIONESTABLECIMIENTO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbComand, "@EST_ID", DbType.Decimal, en.EST_ID);
            oDataBase.AddInParameter(oDbComand, "@DADV_ID", DbType.String, en.idDIVISIONVAL);
            oDataBase.AddInParameter(oDbComand, "@auxDADV_ID", DbType.String, en.auxDADV_ID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = oDataBase.ExecuteNonQuery(oDbComand);
            return n;
        }

        public int Activar(string owner, decimal idEstablecimiento, decimal id, string user)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_DIVISIONESTABLECIMIENTO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@EST_ID", DbType.Decimal, idEstablecimiento);
            oDataBase.AddInParameter(oDbComand, "@ID", DbType.Decimal, id);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, user);
            int n = oDataBase.ExecuteNonQuery(oDbComand);
            return n;
        }

        public int Eliminar(string owner, decimal idEstablecimiento, decimal id, string user)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_INACTIVAR_DIVISIONESTABLECIMIENTO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@EST_ID", DbType.Decimal, idEstablecimiento);
            oDataBase.AddInParameter(oDbComand, "@ID", DbType.Decimal, id);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, user);
            int n = oDataBase.ExecuteNonQuery(oDbComand);
            return n;
        }

        public BEDivisionesEst ObtenerDivEst(string owner, decimal idEst, decimal iddivvalues)
        {
            BEDivisionesEst Obj = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DIVISIONES_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.String, idEst);
                oDataBase.AddInParameter(cm, "@DADV_ID", DbType.Decimal, iddivvalues);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDivisionesEst();
                        Obj.Id = dr.GetDecimal(dr.GetOrdinal("ID"));
                        Obj.idDIVISIONVAL = dr.GetDecimal(dr.GetOrdinal("DADV_ID"));
                        Obj.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                    }
                }
            }
            return Obj;
        }

        public List<BEDivisionesEst> DivisionesXEstablecimiento(string owner, decimal idEst)
        {
            List<BEDivisionesEst> parametros = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DIVISIONES_EST"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BEDivisionesEst ObjObs = null;
                    parametros = new List<BEDivisionesEst>();
                    while (dr.Read())
                    {
                        ObjObs = new BEDivisionesEst();
                        ObjObs.Id = dr.GetDecimal(dr.GetOrdinal("ID"));
                        ObjObs.idTIPODIVISION = dr.GetString(dr.GetOrdinal("DAD_TYPE"));
                        ObjObs.idDIVISION = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                        ObjObs.idSUBTIPODIVISION = dr.GetDecimal(dr.GetOrdinal("DAD_STYPE"));
                        ObjObs.idDIVISIONVAL = dr.GetDecimal(dr.GetOrdinal("DADV_ID"));                      
                        ObjObs.TIPODIVISION = dr.GetString(dr.GetOrdinal("TIPODIVISION"));
                        ObjObs.DIVISION = dr.GetString(dr.GetOrdinal("DIVISION"));
                        ObjObs.SUBTIPODIVISION = dr.GetString(dr.GetOrdinal("SUBDIVISION"));
                        ObjObs.DIVISIONVAL = dr.GetString(dr.GetOrdinal("SUBDIVISIONVAL"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            ObjObs.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            ObjObs.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            ObjObs.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            ObjObs.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        parametros.Add(ObjObs);

                    }
                }
            }
            return parametros;
        }
    }
}
