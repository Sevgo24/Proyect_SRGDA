using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DALicenciaPlaneamiento
    {
        public List<BELicenciaPlaneamiento> ListarXLicAnio(string owner, decimal idTemp, decimal idLic, decimal anio)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            List<BELicenciaPlaneamiento> lst = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PLANEAMIENTO_X_ANIO"))
            {
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
                oDataBase.AddInParameter(cm, "@RAT_ID", DbType.Decimal, idTemp);
                oDataBase.AddInParameter(cm, "@LIC_YEAR", DbType.Decimal, anio);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    lst = new List<BELicenciaPlaneamiento>();
                    while (dr.Read())
                    {
                        var obj = new BELicenciaPlaneamiento();
                        obj.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                        obj.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        obj.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                        obj.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));
                        obj.FRQ_DESC = dr.GetString(dr.GetOrdinal("FRQ_DESC"));
                        obj.LIC_ORDER = dr.GetDecimal(dr.GetOrdinal("LIC_ORDER"));
                        obj.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("LIC_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_INVOICE")))
                        {
                            obj.LIC_INVOICE = dr.GetString(dr.GetOrdinal("LIC_INVOICE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("BLOCK_ID")))
                        {
                            obj.BLOCK_ID = dr.GetDecimal(dr.GetOrdinal("BLOCK_ID"));
                        }
                        obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            obj.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("PAY_ID")))
                        {
                            obj.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                        {
                            obj.NroSerie = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                        {
                            obj.NroFactura =Convert.ToString( dr.GetDecimal(dr.GetOrdinal("INV_NUMBER")));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                        {
                            obj.ImporteFactura = Convert.ToString( dr.GetDecimal(dr.GetOrdinal("INV_NET")));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_STATUS")))
                        {
                            obj.LIC_PL_STATUS =  dr.GetString(dr.GetOrdinal("LIC_PL_STATUS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_AMOUNT")))
                        {
                            obj.LIC_PL_AMOUNT = dr.GetDecimal(dr.GetOrdinal("LIC_PL_AMOUNT"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_STATUS_FACT")))
                        {
                            obj.LIC_PL_STATUS_FACT = dr.GetString(dr.GetOrdinal("LIC_PL_STATUS_FACT"));
                        }
                        lst.Add(obj); 
                    } 

                }
            }
            return lst;
        }
        public List<BELicenciaPlaneamiento> ListarNuevaPlanificacion(string owner, decimal anio, decimal periodo,decimal mes)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            List<BELicenciaPlaneamiento> lst = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_NUEVAPLANIFICACION"))
            {
                oDataBase.AddInParameter(cm, "@RAT_ID", DbType.Decimal, periodo);
                oDataBase.AddInParameter(cm, "@LIC_YEAR", DbType.Decimal, anio);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@MONTH", DbType.Decimal, mes);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    lst = new List<BELicenciaPlaneamiento>();
                    while (dr.Read())
                    {
                        var obj = new BELicenciaPlaneamiento();
                        obj.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("FRQ_NPER"));
                        obj.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                        obj.LIC_MONTH_DESC = dr.GetString(dr.GetOrdinal("FRQ_DESC"));
                        obj.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("FRQ_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BLOCK_ID")))
                        {
                            obj.BLOCK_ID =Convert.ToDecimal(dr.GetString(dr.GetOrdinal("BLOCK_ID")));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("PAY_ID")))
                        {
                            obj.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));
                        }
                        lst.Add(obj);
                    }

                }
            }
            return lst;
        }
        public int Insertar(string owner, decimal codLic, decimal anio, string mesDesc, decimal licOrd, DateTime fecha, string user, decimal? codBloqueo, string codTipoPago, bool esInsert)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            var r = 0;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASI_PLANIFICACION")){
                db.AddInParameter(cm, "@OWNER",DbType.String,owner);
                db.AddInParameter(cm, "@LIC_ID",DbType.Decimal,codLic);
                db.AddInParameter(cm, "@LIC_YEAR",DbType.Decimal,anio);
                db.AddInParameter(cm, "@LIC_MONTH_DESC",DbType.String,mesDesc);
	            db.AddInParameter(cm, "@LIC_ORDER",DbType.Decimal,licOrd);
                db.AddInParameter(cm, "@LIC_DATE",DbType.DateTime,fecha);
                db.AddInParameter(cm, "@LOG_USER_CREAT",DbType.String,user);
                db.AddInParameter(cm, "@BLOCK_ID", DbType.Decimal, codBloqueo);
                db.AddInParameter(cm, "@PAY_ID", DbType.String, codTipoPago);
                db.AddInParameter(cm, "@IS_INSERT", DbType.Boolean, esInsert);

                r = db.ExecuteNonQuery(cm);
            }
            return r;
        }
        public List<BELicenciaPlaneamiento> ListarFechaPlanificacion(string owner, decimal idLic)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            List<BELicenciaPlaneamiento> lst = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_FECHA_PLANIFICACION"))
            {
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    lst = new List<BELicenciaPlaneamiento>();
                    while (dr.Read())
                    {
                        var LP = new BELicenciaPlaneamiento();
                        LP.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                        LP.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("LIC_DATE"));
                        lst.Add(LP);
                    }
                }
            }
            return lst;
        }
        public List<BELicenciaPlaneamiento> ListarAnio(string owner, decimal lic_id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            List<BELicenciaPlaneamiento> lst = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_ANIO_PLANEAMIENTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, lic_id);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    lst = new List<BELicenciaPlaneamiento>();
                    while (dr.Read())
                    {
                        var LP = new BELicenciaPlaneamiento();
                        LP.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                        lst.Add(LP);
                    }
                }
            }
            return lst;
        }
        public int ValidarPeriodoRepetido(string owner, decimal codLic, decimal anio)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            var r = 0;
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_VALIDAR_PERIODO_PLANIFICACION"))
            {
                db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, codLic);
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@YEAR", DbType.Decimal, anio);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        r = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("RESULT")));
                    }
                }
            }
            return r;
        }

        public List<BELicenciaPlaneamiento> ListarFacturaMasiva_LicPlanemientoSubGrilla(string owner, DateTime fini, DateTime ffin,
               string mogId, decimal modId, decimal dadId, decimal bpsId,
               decimal offId, decimal e_bpsId, decimal tipoEstId, decimal subTipoEstId, decimal licId, string monedaId, decimal LibConfi, int historico, string periodoEstado
               , decimal idBpsGroup, decimal groupfact,int oficina,int EmiMensual)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_DET_LICENCIAS_GRAL_SUBGRILLA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, fini);
            db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, ffin);
            db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, mogId);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, modId);

            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, dadId);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, bpsId);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, offId);
            db.AddInParameter(oDbCommand, "@E_BPS_ID", DbType.Decimal, e_bpsId);
            db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, tipoEstId);

            db.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, subTipoEstId);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, licId);
            db.AddInParameter(oDbCommand, "@VAR_ID", DbType.Decimal, LibConfi);
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, monedaId);
            db.AddInParameter(oDbCommand, "@HISTORICO", DbType.Int32, historico);
            db.AddInParameter(oDbCommand, "@PERIODO_ESTADO", DbType.String, periodoEstado);
            db.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.Decimal, idBpsGroup);
            db.AddInParameter(oDbCommand, "@GROUP_FACT", DbType.Decimal, groupfact);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.Decimal, oficina);
            db.AddInParameter(oDbCommand, "@FACT_EMI_MENSUAL", DbType.Int32, EmiMensual);
            oDbCommand.CommandTimeout = 4000;
            
            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BELicenciaPlaneamiento>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BELicenciaPlaneamiento licPlaneamiento = null;
                while (dr.Read())
                {
                    licPlaneamiento = new BELicenciaPlaneamiento();
                    licPlaneamiento.OWNER = owner;
                    licPlaneamiento.Nro = Convert.ToDecimal(dr.GetInt64(dr.GetOrdinal("Nro")));
                    licPlaneamiento.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                    licPlaneamiento.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    licPlaneamiento.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));
                    licPlaneamiento.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                    licPlaneamiento.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("LIC_DATE"));
                    licPlaneamiento.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                    licPlaneamiento.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                    licPlaneamiento.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                    licPlaneamiento.TAXV_VALUEP = dr.GetDecimal(dr.GetOrdinal("TAXV_VALUEP"));
                    licPlaneamiento.RATE_ID = dr.GetDecimal(dr.GetOrdinal("RATE_ID"));
                    licPlaneamiento.LIC_CREQ = dr.GetString(dr.GetOrdinal("LIC_CREQ"));
                    licPlaneamiento.LIC_PL_STATUS = dr.GetString(dr.GetOrdinal("LIC_PL_STATUS"));
                    licPlaneamiento.LIC_MASTER = dr.GetDecimal(dr.GetOrdinal("LIC_MASTER"));
                    licPlaneamiento.LIC_PL_STATUS_FACT = dr.GetString(dr.GetOrdinal("LIC_PL_STATUS_FACT"));
                    lista.Add(licPlaneamiento);
                }
            }
            return lista;
        }

        /// <summary>
        /// Lista los periodos de la planificacion de facturacion de una Licencia
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idLic"></param>
        /// <returns></returns>
        public List<BESelectListItem> ListarPeriodoPlanificacion(string owner, decimal idLic)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            List<BESelectListItem> lst = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_PERIODO_PLANIF_FACT"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    lst = new List<BESelectListItem>();
                    while (dr.Read())
                    {
                        var LP = new BESelectListItem();
                        LP.Text = dr.GetString(dr.GetOrdinal("TEXT"));
                        LP.Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("VALUE")));
                        lst.Add(LP);
                    }
                }
            }
            return lst;
        }

        /// <summary>
        /// OBTIENE LA PLANIFICACION DE UNA LICENCIA
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idLicPlan"></param>
        /// <returns></returns>
        public BELicenciaPlaneamiento ObtenerPlanificacion(string owner, decimal idLicPlan)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            var entidad = new BELicenciaPlaneamiento();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_PLINVOICE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_PL_ID", DbType.Decimal, idLicPlan);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        entidad.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                        entidad.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("LIC_DATE"));
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        entidad.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                        entidad.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));
                        entidad.LIC_ORDER = dr.GetDecimal(dr.GetOrdinal("LIC_ORDER"));

                        //if (!dr.IsDBNull(dr.GetOrdinal("LIC_INVOICE"))) entidad.LIC_INVOICE = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("LIC_INVOICE")));
                        //if(!dr.IsDBNull(dr.GetOrdinal("BLOCK_ID"))) entidad.BLOCK_ID = dr.GetDecimal(dr.GetOrdinal("BLOCK_ID"));
                        //if(!dr.IsDBNull(dr.GetOrdinal("PAY_ID"))) entidad.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));

                        entidad.LIC_MONTH_DESC = dr.GetString(dr.GetOrdinal("MONTH_NAME"));
                        entidad.LIC_CREQ = dr.GetString(dr.GetOrdinal("LIC_CREQ"));


                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_AMOUNT"))) entidad.LIC_PL_AMOUNT = dr.GetDecimal(dr.GetOrdinal("LIC_PL_AMOUNT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_STATUS"))) entidad.LIC_PL_STATUS = dr.GetString(dr.GetOrdinal("LIC_PL_STATUS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_STATUS_FACT"))) entidad.LIC_PL_STATUS_FACT = dr.GetString(dr.GetOrdinal("LIC_PL_STATUS_FACT"));

                    }
                }
            }
            return entidad;
        }

        public bool ActualizarPlanificacionFacturaXML( string xml)
        {
            bool exito = false;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASU_PLANIFICACION_FACTURA"))
            {
                oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                exito = oDataBase.ExecuteNonQuery(cm) > 0;
            }
            exito = true;
            return exito;
        }

        public bool ActualizarPlanificacion(BELicenciaPlaneamiento detalle)
        {
            bool exito = false;
            //var lista = new List<BEFacturaDetalle>();
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_PLANIFICACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, detalle.LIC_PL_ID);
            db.AddInParameter(oDbCommand, "@LIC_PL_AMOUNT", DbType.Decimal, detalle.LIC_PL_AMOUNT);
            db.AddInParameter(oDbCommand, "@LIC_PL_STATUS", DbType.String, detalle.LIC_PL_STATUS);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, detalle.LOG_USER_UPDATE);
            exito = db.ExecuteNonQuery(oDbCommand) > 0;
            return exito;
        }


        #region cadenas
        public decimal ListaCodigoPLaneamiento(decimal LicId, decimal year, decimal month)
        {
            
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_COD_PLANEAMIENTO_X_LIC_MES_ANIO");
            oDataBase.AddInParameter(oDbCommand, "@CODLI",DbType.Decimal,LicId);
            oDataBase.AddInParameter(oDbCommand, "@year", DbType.Decimal, year);
            oDataBase.AddInParameter(oDbCommand, "@mes", DbType.Decimal, month);
            oDataBase.ExecuteNonQuery(oDbCommand);

            decimal res = 0;


            //Devolver El bucle.
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    var LP = new BELicenciaPlaneamiento();

                        res = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                }
            }
            return res;
        }

        //LISTA TODO EL PLANEAMIENTO DE UNA LICENCIA HIJA PARA SER REUTILIZADO PARA EL INSERT
        public List<BELicenciaPlaneamiento> ListaPlaneamientoxLicHija(string owner, decimal LICID)
        {
            List<BELicenciaPlaneamiento> lista = new List<BELicenciaPlaneamiento>();
            BELicenciaPlaneamiento plan = null;

            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_PLAN_X_LIC_HIJA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LICID", DbType.Decimal, LICID);

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    plan = new BELicenciaPlaneamiento();

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                        plan.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_YEAR")))
                        plan.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ORDER")))
                        plan.LIC_ORDER = dr.GetDecimal(dr.GetOrdinal("LIC_ORDER"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_DATE")))
                    plan.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("LIC_DATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_MONTH")))
                        plan.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));

                    if (!dr.IsDBNull(dr.GetOrdinal("PAY_ID")))
                        plan.PAY_ID = dr.GetString(dr.GetOrdinal("PAY_ID"));

                    //if (!dr.IsDBNull(dr.GetOrdinal("MONTH_NAME")))
                    //plan.LIC_MONTH_DESC = dr.GetString(dr.GetOrdinal("MONTH_NAME"));

                    //if (!dr.IsDBNull(dr.GetOrdinal("LIC_CREQ")))
                    //plan.LIC_CREQ = dr.GetString(dr.GetOrdinal("LIC_CREQ"));


                    //if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_AMOUNT"))) 
                    //    plan.LIC_PL_AMOUNT = dr.GetDecimal(dr.GetOrdinal("LIC_PL_AMOUNT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_STATUS")))
                        plan.LIC_PL_STATUS = dr.GetString(dr.GetOrdinal("LIC_PL_STATUS"));


                    lista.Add(plan);
                }

                
            }

            return lista;

        }

        //INSERTA PLANEAMIENTO CON LISTA XML
        public List<BELicenciaPlaneamiento> InsertaPlaneamientoLicHijaXML(string owner, string xml)
        {
            List<BELicenciaPlaneamiento> lista = null;
            BELicenciaPlaneamiento entidad = null;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_PLANEAMIENTO_LICENCIA_XML");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@xmlLst", DbType.Xml, xml);

            using (IDataReader dr = (db.ExecuteReader(oDbCommand)))
            {
                lista = new List<BELicenciaPlaneamiento>();
                while (dr.Read())
                {
                    entidad = new BELicenciaPlaneamiento();

                    if (!dr.IsDBNull(dr.GetOrdinal("new_LIC_PL_ID")))
                        entidad.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("new_LIC_PL_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("new_LIC_YEAR")))
                        entidad.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("new_LIC_YEAR"));

                    if (!dr.IsDBNull(dr.GetOrdinal("newLIC_ID")))
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("newLIC_ID"));

                    lista.Add(entidad);
                }

            }


            return lista;
        }

        //Actualiza planeamientos de licencia Hija Individual
        public int ActualizaPlaneamientoLicenciaHijaIndividualXML(string owner, string xml)
        {
            //List<BELicenciaPlaneamiento> lista = null;
           // BELicenciaPlaneamiento entidad = null;

            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_PLANEAMIENTO_LICENCIA_HIJA");
            db.AddInParameter(oDbCommand,"@owner",DbType.String,owner);
            db.AddInParameter(oDbCommand,"@xmlLst",DbType.Xml,xml);
            int r = db.ExecuteNonQuery(oDbCommand);

            return r;
        }
        /// <summary>
        /// OBTIENE TODA LA PLANIFICACION DE LA LICENCIA HIJA 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="licid"></param>
        /// <returns></returns>
        public List<BELicenciaPlaneamiento> ListaTodaPlanificacionxLicencia(string owner, decimal licid)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            List<BELicenciaPlaneamiento> lista = null;
            BELicenciaPlaneamiento entidad = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_PLINVOICE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, licid);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    lista = new List<BELicenciaPlaneamiento>();
                   while(dr.Read())
                    {
                        entidad = new BELicenciaPlaneamiento();
                        entidad.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                        entidad.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("LIC_DATE"));
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        entidad.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                        entidad.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));
                        entidad.LIC_ORDER = dr.GetDecimal(dr.GetOrdinal("LIC_ORDER"));
                        entidad.LIC_MONTH_DESC = dr.GetString(dr.GetOrdinal("MONTH_NAME"));
                        entidad.LIC_CREQ = dr.GetString(dr.GetOrdinal("LIC_CREQ"));


                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_AMOUNT"))) entidad.LIC_PL_AMOUNT = dr.GetDecimal(dr.GetOrdinal("LIC_PL_AMOUNT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_STATUS"))) entidad.LIC_PL_STATUS = dr.GetString(dr.GetOrdinal("LIC_PL_STATUS"));

                        lista.Add(entidad);
                    }
                }
            }
            return lista;
        }

        public int BloqueaPlaneamientoLicenciaIndividual(string owner, decimal lic_pl_id, decimal block_id)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_LIC_PL_INVOICE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, lic_pl_id);
            db.AddInParameter(oDbCommand, "@BLOCK_ID", DbType.Decimal, block_id);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        #endregion

        #region Descuentos Plantilla
        public List<BELicencias> ValidaPeriodosLicenciaAlDia(string owner,string xml,DateTime fini)
        {
            List<BELicencias> lista = null;
            BELicencias entidad = null;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_PERIODOS_LICENCIA_AL_DIA");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@xml", DbType.Xml, xml);
            db.AddInParameter(oDbCommand, "@fini", DbType.DateTime, fini);
            using (IDataReader dr= (db.ExecuteReader(oDbCommand)))
            {
                lista=new List<BELicencias>();
                while(dr.Read())
                {
                    entidad=new BELicencias();
                    if(!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        entidad.LIC_ID=dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                    lista.Add(entidad);

                }
            }
            return lista;

        }

        public List<BELicencias> ValidaLicenciaAlDia(string owner, string xml)
        {
            List<BELicencias> lista = null;
            BELicencias entidad = null;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_LICENCIA_AL_DIA");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@xml", DbType.Xml, xml);
            using (IDataReader dr = (db.ExecuteReader(oDbCommand)))
            {
                lista = new List<BELicencias>();
                while (dr.Read())
                {
                    entidad = new BELicencias();
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                    lista.Add(entidad);

                }
            }
            return lista;

        }
        #endregion

        public int AnularFacturaPlanificacion(BEFactura factura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_FACTURA_PLANIFICACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, factura.OWNER);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, factura.INV_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, factura.LOG_USER_UPDATE.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }


        #region INSERTA PLANEAMIENTO ACTUAL
        //INSERTA PLANEAMIENTO CON LISTA XML
        public List<BELicenciaPlaneamiento> InsertaPlaneamientoActual(string owner, decimal LIC_ID,int ANIO,int MES, int DIA, string USUARIO)
        {
            List<BELicenciaPlaneamiento> lista = null;
            BELicenciaPlaneamiento entidad = null;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_PLANEAMIENTO_ACTUAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            db.AddInParameter(oDbCommand, "@ANIO", DbType.Int32, ANIO);
            db.AddInParameter(oDbCommand, "@MES", DbType.Int32, MES);
            db.AddInParameter(oDbCommand, "@DIA", DbType.Int32, DIA);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREATE", DbType.String, USUARIO);

            using (IDataReader dr = (db.ExecuteReader(oDbCommand)))
            {
                lista = new List<BELicenciaPlaneamiento>();
                while (dr.Read())
                {
                    entidad = new BELicenciaPlaneamiento();

                    if (!dr.IsDBNull(dr.GetOrdinal("new_LIC_PL_ID")))
                        entidad.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("new_LIC_PL_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("new_LIC_YEAR")))
                        entidad.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("new_LIC_YEAR"));

                    if (!dr.IsDBNull(dr.GetOrdinal("newLIC_ID")))
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("newLIC_ID"));

                    lista.Add(entidad);
                }

            }


            return lista;
        }
        #endregion


        #region ACTUALIZAR  PERIODOS LICENCIA 

        /// <summary>
        ///  VALIDA SI EL PERIODO PUEDE MODIFICARSE  SI ES QUE SE ENCUENTRA CERRADO O ABIERTO
        /// </summary>
        /// <param name="LIC_PL_ID"> CODIGO DEL PERIODO</param>
        /// <returns></returns>
        public int ValidaPeriodoLicencia(decimal  LIC_PL_ID)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_MODIFICACION_PERIODO");
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, LIC_PL_ID);
            int r =Convert.ToInt32(  db.ExecuteScalar(oDbCommand));
            return r;
        }


        public int ActualizarPeriodoLicenciaAct(decimal LIC_PL_ID,int OPCION, string USUARIO_ACTUAL)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_PERIODO_LICENCIA");
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, LIC_PL_ID);
            db.AddInParameter(oDbCommand, "@OPCION", DbType.Int32, OPCION);
            db.AddInParameter(oDbCommand, "@USUARIO_ACTUAL", DbType.String, USUARIO_ACTUAL);
            int r = Convert.ToInt32(db.ExecuteNonQuery(oDbCommand));
            return r;
        }

        #endregion
    }
}
