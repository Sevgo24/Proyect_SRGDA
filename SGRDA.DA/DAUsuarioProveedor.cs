using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DAUsuarioProveedor
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

       
        public int Insertar(UsuarioProveedor bps)
        {

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_USUARIO_PROVEEDOR");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, bps.OWNER);
                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, Convert.ToInt32(bps.BPS_ID));
                oDataBase.AddInParameter(oDbComand, "@ECON_ID", DbType.String, bps.ECON_ID);
                oDataBase.AddInParameter(oDbComand, "@SUPP_LINE", DbType.String, bps.SUPP_LINE);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, bps.LOG_USER_CREAT);
                
                int n = oDataBase.ExecuteNonQuery(oDbComand);
            

                return n;
             
        }

        public int Actualizar(SocioNegocio bps)
        {
             
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_BPS_GRAL");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, bps.OWNER);
                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Int32, bps.BPS_ID);
                oDataBase.AddInParameter(oDbComand, "@ENT_TYPE", DbType.String, bps.ENT_TYPE);
                oDataBase.AddInParameter(oDbComand, "@BPS_NAME", DbType.String, bps.BPS_NAME);
                oDataBase.AddInParameter(oDbComand, "@BPS_FIRST_NAME", DbType.String, bps.BPS_FIRST_NAME);
                oDataBase.AddInParameter(oDbComand, "@BPS_FATH_SURNAME", DbType.String, bps.BPS_FATH_SURNAME);
                oDataBase.AddInParameter(oDbComand, "@BPS_MOTH_SURNAME", DbType.String, bps.BPS_MOTH_SURNAME);
                oDataBase.AddInParameter(oDbComand, "@TAXT_ID", DbType.Decimal, bps.TAXT_ID);
                oDataBase.AddInParameter(oDbComand, "@TAX_ID", DbType.String, bps.TAX_ID);
                oDataBase.AddInParameter(oDbComand, "@BPS_GROUP", DbType.String, bps.BPS_GROUP);
                oDataBase.AddInParameter(oDbComand, "@BPS_INT", DbType.DateTime, bps.BPS_INT);
                oDataBase.AddInParameter(oDbComand, "@BPS_INT_N", DbType.String, bps.BPS_INT_N);
                oDataBase.AddInParameter(oDbComand, "@BPS_USER", DbType.String, bps.BPS_USER);
                oDataBase.AddInParameter(oDbComand, "@BPS_COLLECTOR", DbType.String, bps.BPS_COLLECTOR);
                oDataBase.AddInParameter(oDbComand, "@BPS_ASSOCIATION", DbType.String, bps.BPS_ASSOCIATION);
                oDataBase.AddInParameter(oDbComand, "@BPS_EMPLOYEE", DbType.String, bps.BPS_EMPLOYEE);
                oDataBase.AddInParameter(oDbComand, "@BPS_SUPPLIER", DbType.String, bps.BPS_SUPPLIER);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, bps.LOG_USER_UPDATE);
                
                int n = oDataBase.ExecuteNonQuery(oDbComand);

                return n;
             
        }
        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="bps"></param>
        /// <returns></returns>
        public int Eliminar(SocioNegocio bps)
        {
            
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASD_BPS_GRAL");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, bps.OWNER);
                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Int32, bps.BPS_ID);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, bps.LOG_USER_UPDATE);
                int r = oDataBase.ExecuteNonQuery(oDbComand);
                return r;
            
        }

        public UsuarioProveedor  Obtener(decimal codigoBps, string owner)
        {
            UsuarioProveedor socio = null;
            
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_USU_PROVEEDOR"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, codigoBps);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            socio = new UsuarioProveedor();
                            socio.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            socio.OWNER =  dr.GetString(dr.GetOrdinal("OWNER"));
                            

                            if (!dr.IsDBNull(dr.GetOrdinal("ECON_ID")))
                            {
                                socio.ECON_ID = dr.GetString(dr.GetOrdinal("ECON_ID"));
                            }
               
                            if (!dr.IsDBNull(dr.GetOrdinal("SUPP_LINE")))
                            {
                                socio.SUPP_LINE = dr.GetString(dr.GetOrdinal("SUPP_LINE"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                socio.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                        }
                    }
                }
            
            return socio;
        }

        public SocioNegocio BuscarXtipodocumento(decimal idTipoDocumento, string nroDocumento)
        {
            SocioNegocio socio = null;
           
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SOCIO_TIPODOC"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                    oDataBase.AddInParameter(cm, "@TAXT_ID", DbType.String, idTipoDocumento);
                    oDataBase.AddInParameter(cm, "@TAX_ID", DbType.String, nroDocumento);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            socio = new SocioNegocio();
                            socio.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            socio.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }
                    }
                }
             
            return socio;
        }

        public int CambiarEstado(decimal bpsId,string empresa,string usuModifica)
        {
            
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_ESTADO_PROVEEDOR");

                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, bpsId);
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, empresa);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, usuModifica);

                int n = oDataBase.ExecuteNonQuery(oDbComand);

                return n;
            
        }
    }
}
