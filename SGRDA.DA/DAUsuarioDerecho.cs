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
    public class DAUsuarioDerecho
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

       
        public int Insertar(UsuarioDerecho bps)
        {

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_USUARIO_DERECHO");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, bps.OWNER);
                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, Convert.ToInt32(bps.BPS_ID));
                oDataBase.AddInParameter(oDbComand, "@PAY_ID", DbType.String, bps.PAY_ID);
                oDataBase.AddInParameter(oDbComand, "@BPS_GROUP", DbType.Decimal, bps.BPS_GROUP);
                oDataBase.AddInParameter(oDbComand, "@BPS_DIS_PER", DbType.Decimal, bps.BPS_DIS_PER);
                oDataBase.AddInParameter(oDbComand, "@BPS_DIS_AMO", DbType.Decimal, bps.BPS_DIS_AMO);
                oDataBase.AddInParameter(oDbComand, "@BPS_DIS_REA", DbType.String, bps.BPS_DIS_REA);
                oDataBase.AddInParameter(oDbComand, "@ACC_ID", DbType.String, bps.ACC_ID);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, bps.LOG_USER_CREAT);
                oDataBase.AddInParameter(oDbComand, "@BPS_PARTIDA", DbType.String, bps.BPS_PARTIDA);
                oDataBase.AddInParameter(oDbComand, "@BPS_ZONA", DbType.String, bps.BPS_ZONA);
                oDataBase.AddInParameter(oDbComand, "@BPS_SEDE", DbType.String, bps.BPS_SEDE);
                
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

        public UsuarioDerecho  Obtener(decimal codigoBps, string owner)
        {
            UsuarioDerecho socio = null;
            
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_USU_DERECHO"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, codigoBps);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            socio = new UsuarioDerecho();
                            socio.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            socio.OWNER =  dr.GetString(dr.GetOrdinal("OWNER"));
                            socio.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));

                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_GROUP")))
                            {
                                socio.BPS_GROUP = dr.GetDecimal(dr.GetOrdinal("BPS_GROUP"));
                            }
               
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_DIS_PER")))
                            {
                                socio.BPS_DIS_PER = dr.GetDecimal(dr.GetOrdinal("BPS_DIS_PER"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_DIS_AMO")))
                            {
                                socio.BPS_DIS_AMO = dr.GetDecimal(dr.GetOrdinal("BPS_DIS_AMO"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_DIS_REA")))
                            {
                                socio.BPS_DIS_REA = dr.GetString(dr.GetOrdinal("BPS_DIS_REA"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("ACC_ID")))
                            {
                                socio.ACC_ID = dr.GetString(dr.GetOrdinal("ACC_ID"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS"))) {
                                socio.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_GROUP")))
                            {
                                socio.BPS_GROUP = dr.GetDecimal(dr.GetOrdinal("BPS_GROUP"));
                            }


                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_PARTIDA")))
                            {
                                socio.BPS_PARTIDA = dr.GetString(dr.GetOrdinal("BPS_PARTIDA"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_ZONA")))
                            {
                                socio.BPS_ZONA = dr.GetString(dr.GetOrdinal("BPS_ZONA"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_SEDE")))
                            {
                                socio.BPS_SEDE = dr.GetString(dr.GetOrdinal("BPS_SEDE"));
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
            
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_ESTADO_USUARIO");

                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, bpsId);
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, empresa);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, usuModifica);

                int n = oDataBase.ExecuteNonQuery(oDbComand);

                return n;
            
        }
    }
}
