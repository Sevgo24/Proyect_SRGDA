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
    public class DAAsociado
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEAsociado aso)
        {
            int retorno = 0;

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_ASOCIADO_BPS");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, aso.OWNER);
            oDataBase.AddInParameter(oDbComand, "@ROL_ID", DbType.String, aso.ROL_ID.ToUpper());
            oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.String, aso.BPS_ID);
            oDataBase.AddInParameter(oDbComand, "@BPSA_ID", DbType.String, aso.BPSA_ID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, aso.LOG_USER_CREAT.ToUpper());

            retorno = oDataBase.ExecuteNonQuery(oDbComand);

            return retorno;

        }

        public BEAsociado ObtenerAsoBPS(string owner, decimal idBps, decimal idBpsa)
        {
            BEAsociado Obj = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_ASOCIADO_BPS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                //oDataBase.AddInParameter(cm, "@ROL_ID", DbType.String, idrol);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@BPSA_ID", DbType.Decimal, idBpsa);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEAsociado();

                        Obj.ROL_ID = dr.GetString(dr.GetOrdinal("ROL_ID"));
                        Obj.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                        Obj.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        Obj.BPSA_ID = dr.GetDecimal(dr.GetOrdinal("BPSA_ID"));
                        Obj.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                    }
                }
            }

            return Obj;
        }
        public int Update(BEAsociado par)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ASOCIADO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, par.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@ROL_ID", DbType.String, par.ROL_ID.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, par.BPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@BPSA_ID", DbType.Decimal, par.BPSA_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, par.LOG_USER_UPDATE.ToUpper());

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal idBps,decimal idBpsa, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_ASOCIADO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idBps);
            oDataBase.AddInParameter(oDbCommand, "@BPSA_ID", DbType.Decimal, idBpsa);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public int Activar(string owner, decimal idBps,decimal idBpsa, string user)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("[SGRDASU_ACTIVAR_ASOCIADO]");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idBps);
            oDataBase.AddInParameter(oDbCommand, "@BPSA_ID", DbType.Decimal, idBpsa);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public List<BEAsociado> AsociadoXSocio(decimal idBps, string owner)
        {
            List<BEAsociado> parametros = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_ASOCIADO_BPS"))
            {
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {

                    BEAsociado ObjObs = null;
                    parametros = new List<BEAsociado>();
                    while (dr.Read())
                    {
                        ObjObs = new BEAsociado();

                        ObjObs.ROL_ID = dr.GetString(dr.GetOrdinal("ROL_ID"));
                        ObjObs.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                        ObjObs.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        ObjObs.BPSA_ID = dr.GetDecimal(dr.GetOrdinal("BPSA_ID"));
                        ObjObs.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            ObjObs.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            ObjObs.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                        {
                            ObjObs.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
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
