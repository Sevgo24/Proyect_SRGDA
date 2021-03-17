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
    public class DAProceso
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEProceso> ListarProcesoXEstado(decimal idModulo, decimal idEstado, string owner, decimal idWrkf,decimal idWrkfRef,bool isManual)
        {
            List<BEProceso> lst = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_PROCESO_X_ESTADO"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@LICS_ID", DbType.Decimal, idEstado);
                db.AddInParameter(cm, "@PROC_MOD", DbType.Decimal, idModulo);
                db.AddInParameter(cm, "@WRKF_ID", DbType.Decimal, idWrkf);
                db.AddInParameter(cm, "@WRKF_REF1", DbType.Decimal, idWrkfRef);
                db.AddInParameter(cm, "@IS_MANUAL", DbType.Boolean, isManual);
                
                

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    lst = new List<BEProceso>();
                    BEProceso item = null;
                    while (dr.Read())
                    {
                        item = new BEProceso();
                   
                        item.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));
								 
                        item.PROC_ORDER = dr.GetDecimal(dr.GetOrdinal("PROC_ORDER"));
                        item.MOD_DESC = dr.GetString(dr.GetOrdinal("MOD_DESC"));
                        item.PROC_ID = dr.GetDecimal(dr.GetOrdinal("PROC_ID"));
                        item.PROC_DESC = dr.GetString(dr.GetOrdinal("PROC_DESC"));
                        //item.PROC_TDESC = dr.GetString(dr.GetOrdinal("PROC_TDESC"));
                        item.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                        item.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                        item.WRKF_ANAME = dr.GetString(dr.GetOrdinal("WRKF_ANAME"));
                        item.WRKF_ALABEL = dr.GetString(dr.GetOrdinal("WRKF_ALABEL"));
                        item.WRKF_ODESC = dr.GetString(dr.GetOrdinal("WRKF_ODESC"));
                        item.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                        item.WRKF_OID = dr.GetDecimal(dr.GetOrdinal("WRKF_OID"));
                        item.WRKF_AMID = dr.GetDecimal(dr.GetOrdinal("WRKF_AMID"));


                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AORDER")))
                            item.ORDER = Convert.ToDecimal(dr.GetString(dr.GetOrdinal("WRKF_AORDER")));

                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ADATE")))
                            item.PROC_LAUNCH = dr.GetDateTime(dr.GetOrdinal("WRKF_ADATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_NAME")))
                            item.WRKF_NAME = dr.GetString(dr.GetOrdinal("WRKF_NAME"));

                        
                        
                        
                        lst.Add(item);
                    }
                }
                return lst;
            }
        }

        public List<BEProceso> ListarProceso(string owner)
        {
            List<BEProceso> lst = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_PROCESO_LISTAR"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    lst = new List<BEProceso>();
                    BEProceso item = null;
                    while (dr.Read())
                    {
                        item = new BEProceso();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.PROC_ID = dr.GetDecimal(dr.GetOrdinal("PROC_ID"));
                        item.PROC_DESC = dr.GetString(dr.GetOrdinal("PROC_DESC"));
                        item.PROC_FUCTION = dr.GetString(dr.GetOrdinal("PROC_FUCTION"));
                        item.PROC_TYPE = dr.GetDecimal(dr.GetOrdinal("PROC_TYPE"));
                        item.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                        item.PROC_MOD = dr.GetDecimal(dr.GetOrdinal("PROC_MOD"));
                        item.PROC_JOBS = dr.GetDecimal(dr.GetOrdinal("PROC_JOBS"));

                        item.PROC_TDESC = dr.GetString(dr.GetOrdinal("PROC_TDESC"));
                        item.MOD_DESC = dr.GetString(dr.GetOrdinal("MOD_DESC"));
                        item.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("PROC_LAUNCH")))
                            item.PROC_LAUNCH = dr.GetDateTime(dr.GetOrdinal("PROC_LAUNCH"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        lst.Add(item);
                    }
                }
                return lst;
            }
        }

        public List<BEProceso> usp_Get_ProcesoPage(string owner, string dato, decimal tipo, decimal ciclo, decimal cliente, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_PROCESO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@PROC_NAME", DbType.String, dato);

            db.AddInParameter(oDbCommand, "@PROC_TYPE", DbType.Decimal, tipo);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, ciclo);
            db.AddInParameter(oDbCommand, "@WRKF_CID", DbType.Decimal, cliente);

            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEProceso>();
            var item = new BEProceso();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEProceso();
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    item.PROC_ID = dr.GetDecimal(dr.GetOrdinal("PROC_ID"));
                    item.PROC_TYPE = dr.GetDecimal(dr.GetOrdinal("PROC_TYPE"));
                    item.PROC_NAME = dr.GetString(dr.GetOrdinal("PROC_NAME"));
                    item.PROC_TDESC = dr.GetString(dr.GetOrdinal("PROC_TDESC"));
                    item.WRKF_NAME = dr.GetString(dr.GetOrdinal("WRKF_NAME"));
                    item.MOD_DESC = dr.GetString(dr.GetOrdinal("MOD_DESC"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";
                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Eliminar(BEProceso en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVAR_PROCESO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@PROC_ID", DbType.Decimal, en.PROC_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEProceso Obtener(string owner, decimal id)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_PROCESO");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@PROC_ID", DbType.Decimal, id);

                BEProceso item = null;
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    dr.Read();
                    item = new BEProceso();
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    item.PROC_ID = dr.GetDecimal(dr.GetOrdinal("PROC_ID"));
                    item.PROC_NAME = dr.GetString(dr.GetOrdinal("PROC_NAME")).ToUpper();
                    if (!dr.IsDBNull(dr.GetOrdinal("PROC_DESC")))
                        item.PROC_DESC = dr.GetString(dr.GetOrdinal("PROC_DESC")).ToUpper();
                    item.PROC_TYPE = dr.GetDecimal(dr.GetOrdinal("PROC_TYPE"));
                    item.WRKF_ID = dr.GetDecimal(dr.GetOrdinal("WRKF_ID"));
                    item.WRKF_CID = dr.GetDecimal(dr.GetOrdinal("WRKF_CID"));
                    item.PROC_FUCTION = dr.GetString(dr.GetOrdinal("PROC_FUCTION")).ToUpper();
                    item.PROC_JOBS = dr.GetDecimal(dr.GetOrdinal("PROC_JOBS"));
                    item.PROC_FREQ_TYPE = dr.GetString(dr.GetOrdinal("PROC_FREQ_TYPE"));
                    item.PROC_LAUNCH = dr.GetDateTime(dr.GetOrdinal("PROC_LAUNCH"));
                    item.PROC_SHOW = dr.GetString(dr.GetOrdinal("PROC_SHOW"));

                    item.PROC_ORDER = dr.GetDecimal(dr.GetOrdinal("PROC_ORDER"));
                }
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insertar(BEProceso en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_PROCESO");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
                db.AddInParameter(oDbCommand, "@PROC_NAME", DbType.String, en.PROC_NAME);
                db.AddInParameter(oDbCommand, "@PROC_DESC", DbType.String, en.PROC_DESC);
                db.AddInParameter(oDbCommand, "@PROC_SHOW", DbType.String, en.PROC_SHOW);
                db.AddInParameter(oDbCommand, "@PROC_TYPE", DbType.Decimal, en.PROC_TYPE);
                db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, en.WRKF_ID);
                db.AddInParameter(oDbCommand, "@WRKF_CID", DbType.Decimal, en.WRKF_CID);
                db.AddInParameter(oDbCommand, "@PROC_FUCTION", DbType.String, en.PROC_FUCTION);
                db.AddInParameter(oDbCommand, "@PROC_JOBS", DbType.Decimal, en.PROC_JOBS);
                db.AddInParameter(oDbCommand, "@PROC_FREQ_TYPE", DbType.String, en.PROC_FREQ_TYPE);
                db.AddInParameter(oDbCommand, "@PROC_LAUNCH", DbType.DateTime, en.PROC_LAUNCH);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Actualizar(BEProceso en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_PROCESO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@PROC_ID", DbType.Decimal, en.PROC_ID);
            db.AddInParameter(oDbCommand, "@PROC_NAME", DbType.String, en.PROC_NAME);
            db.AddInParameter(oDbCommand, "@PROC_DESC", DbType.String, en.PROC_DESC);
            db.AddInParameter(oDbCommand, "@PROC_SHOW", DbType.String, en.PROC_SHOW);
            db.AddInParameter(oDbCommand, "@PROC_TYPE", DbType.Decimal, en.PROC_TYPE);
            db.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, en.WRKF_ID);
            db.AddInParameter(oDbCommand, "@WRKF_CID", DbType.Decimal, en.WRKF_CID);
            db.AddInParameter(oDbCommand, "@PROC_FUCTION", DbType.String, en.PROC_FUCTION);
            db.AddInParameter(oDbCommand, "@PROC_JOBS", DbType.Decimal, en.PROC_JOBS);
            db.AddInParameter(oDbCommand, "@PROC_FREQ_TYPE", DbType.String, en.PROC_FREQ_TYPE);
            db.AddInParameter(oDbCommand, "@PROC_LAUNCH", DbType.DateTime, en.PROC_LAUNCH);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;

        }

        public List<BEProceso> ListarItem(string owner)
        {
            List<BEProceso> lst = new List<BEProceso>();
            using (DbCommand cm = db.GetStoredProcCommand("SWFSS_ITEMS_PROCESS"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        BEProceso item = new BEProceso();
                        item.PROC_ID = dr.GetDecimal(dr.GetOrdinal("PROC_ID"));
                        item.PROC_NAME = dr.GetString(dr.GetOrdinal("PROC_NAME")).ToUpper();
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        /// <summary>
        /// funcion de prueba eliminar luego de pruebas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SocioNegocio ObtenerSocio(decimal id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_SOCIO_TEST_REPORTE");
            //db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@CODIGO", DbType.Decimal, id);

            SocioNegocio item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                dr.Read();
                item = new SocioNegocio();
                item.BPS_ID = dr.GetInt32(dr.GetOrdinal("CODIGO"));
                item.BPS_NAME = dr.GetString(dr.GetOrdinal("NOMBRES"));
                item.TAXN_NAME = dr.GetString(dr.GetOrdinal("DOCUMENTO"));
                item.DAD_NAME = dr.GetString(dr.GetOrdinal("TIPO"));
                item.BPS_MOTH_SURNAME = dr.GetString(dr.GetOrdinal("DIRECCION"));

            }
            return item;
        }
    }
}
