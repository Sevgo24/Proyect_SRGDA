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
    public class DAUsuarioEmpleado
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        //public List<SocioNegocio> USP_SOCIOS_LISTARPAGE( decimal tipo, string nro_tipo, string nombre, int pagina, int cantRegxPag)
        //{
        //    Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        //    DbCommand oDbCommand = oDataBase.GetStoredProcCommand("USP_SOCIOS_LISTARPAGE");
        //    oDataBase.AddInParameter(oDbCommand, "@TAXT_ID", DbType.Decimal, tipo);
        //    oDataBase.AddInParameter(oDbCommand, "@TAX_ID", DbType.String, nro_tipo);
        //    oDataBase.AddInParameter(oDbCommand, "@BPS_NAME", DbType.String, nombre);
        //    oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
        //    oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
        //    oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
        //    oDataBase.ExecuteNonQuery(oDbCommand);

        //    string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

        //    Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
        //    DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("USP_SOCIOS_LISTARPAGE", tipo, nro_tipo, nombre, pagina, cantRegxPag, ParameterDirection.Output);

        //    var lista = new List<SocioNegocio>();

        //    using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
        //    {
        //        while (reader.Read())
        //            lista.Add(new SocioNegocio(reader, Convert.ToInt32(results)));
        //    }
        //    return lista;
        //}

        //public List<SocioNegocio> UPS_BUSCAR_SOCIOS_X_RAZONSOCIAL(string Owner, string datos)
        //{
        //    List<SocioNegocio> lst = new List<SocioNegocio>();
        //    SocioNegocio item = null;
        //    try
        //    {
        //        using (DbCommand cm = oDataBase.GetStoredProcCommand("UPS_BUSCAR_SOCIOS_X_RAZONSOCIAL"))
        //        {
        //            oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
        //            oDataBase.AddInParameter(cm, "@DATOS", DbType.String, datos);

        //            using (IDataReader dr = oDataBase.ExecuteReader(cm))
        //            {
        //                while (dr.Read())
        //                {
        //                    item = new SocioNegocio();
        //                    item.BPS_ID = dr.GetInt32(dr.GetOrdinal("BPS_ID"));
        //                    item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
        //                    item.BPS_FIRST_NAME = dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME"));
        //                    item.BPS_FATH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME"));
        //                    item.BPS_MOTH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME"));
        //                    lst.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return lst;
        //}

        public int Insertar(UsuarioEmpleado bps)
        {

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_USUARIO_EMPLEADO");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, bps.OWNER);
                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, Convert.ToInt32(bps.BPS_ID));
                oDataBase.AddInParameter(oDbComand, "@EMP_POS", DbType.String, bps.EMP_POS);
                oDataBase.AddInParameter(oDbComand, "@EMP_ENTRY", DbType.DateTime, bps.EMP_ENTRY);
                oDataBase.AddInParameter(oDbComand, "@ENDS", DbType.DateTime, bps.ENDS);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, bps.LOG_USER_CREAT);
                
                int n = oDataBase.ExecuteNonQuery(oDbComand);
            

                return n;
             
        }

        public int Actualizar(SocioNegocio bps)
        {
            try
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
            catch (Exception)
            {
                return 0; ;
            }
        }
        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="bps"></param>
        /// <returns></returns>
        public int Eliminar(SocioNegocio bps)
        {
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASD_BPS_GRAL");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, bps.OWNER);
                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Int32, bps.BPS_ID);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, bps.LOG_USER_UPDATE);
                int r = oDataBase.ExecuteNonQuery(oDbComand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public UsuarioEmpleado  Obtener(decimal codigoBps, string owner)
        {
            UsuarioEmpleado empleado = null;
            
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_USU_EMPLEADO"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, codigoBps);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            empleado = new UsuarioEmpleado();
                            empleado.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            empleado.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            

                            if (!dr.IsDBNull(dr.GetOrdinal("EMP_POS")))
                            {
                                empleado.EMP_POS = dr.GetString(dr.GetOrdinal("EMP_POS"));
                            }
               
                            if (!dr.IsDBNull(dr.GetOrdinal("EMP_ENTRY")))
                            {
                                empleado.EMP_ENTRY = dr.GetDateTime(dr.GetOrdinal("EMP_ENTRY"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                empleado.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                        }
                    }
                }

                return empleado;
        }

        public SocioNegocio BuscarXtipodocumento(decimal idTipoDocumento, string nroDocumento)
        {
            SocioNegocio socio = null;
            try
            {
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
            }
            catch (Exception)
            {
                throw;
            }
            return socio;
        }
        public int CambiarEstado(decimal bpsId, string empresa, string usuModifica)
        {

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_ESTADO_EMPLEADO");

            oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, bpsId);
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, empresa);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, usuModifica);

            int n = oDataBase.ExecuteNonQuery(oDbComand);

            return n;

        }
    }
}
