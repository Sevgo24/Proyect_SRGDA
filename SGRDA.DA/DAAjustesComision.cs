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
    public class DAAjustesComision
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEAjustesComision> ListarPage(string owner, decimal IdAgente, DateTime Fecha, string IdMondeda, decimal IdLicencia, decimal IdModalidad, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_AJUSTE_COM");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, IdAgente);
            db.AddInParameter(oDbCommand, "@FECHA", DbType.DateTime, Fecha);
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, IdMondeda);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, IdLicencia);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, IdModalidad);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);
            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEAjustesComision>();
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEAjustesComision();
                    item.COM_ID = dr.GetDecimal(dr.GetOrdinal("COM_ID"));
                    item.CUR_DESC = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                    item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    item.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    item.COM_VALUE = dr.GetDecimal(dr.GetOrdinal("COM_VALUE"));
                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
                return lista;
            }
        }

        public BEAjustesComision ObtenerDatosGrabar(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_AJUSTE_INS");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, en.LIC_ID);
            db.ExecuteNonQuery(oDbCommand);
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEAjustesComision();
                    if (!dr.IsDBNull(dr.GetOrdinal("COMT_ID")))
                        item.COMT_ID = dr.GetDecimal(dr.GetOrdinal("COMT_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LEVEL_ID")))
                        item.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PERC")))
                        item.COM_PERC = dr.GetDecimal(dr.GetOrdinal("COM_PERC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_BASE")))
                        item.COM_BASE = dr.GetDecimal(dr.GetOrdinal("COM_BASE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PPIND")))
                        item.COM_PPIND = dr.GetString(dr.GetOrdinal("COM_PPIND"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PRIMARY")))
                        item.COM_PRIMARY = dr.GetDecimal(dr.GetOrdinal("COM_PRIMARY"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_EST")))
                        item.COM_EST = dr.GetString(dr.GetOrdinal("COM_EST"));

                    if (!dr.IsDBNull(dr.GetOrdinal("PAY_ID")))
                        item.PAY_ID = dr.GetDecimal(dr.GetOrdinal("PAY_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_INVOICE")))
                        item.COM_INVOICE = dr.GetDecimal(dr.GetOrdinal("COM_INVOICE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_LDATE")))
                        item.COM_LDATE = dr.GetDateTime(dr.GetOrdinal("COM_LDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_RDATE")))
                        item.COM_RDATE = dr.GetDateTime(dr.GetOrdinal("COM_RDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_RDESC")))
                        item.COM_RDESC = dr.GetString(dr.GetOrdinal("COM_RDESC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PDATE")))
                        item.COM_PDATE = dr.GetDateTime(dr.GetOrdinal("COM_PDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PNUM")))
                        item.COM_PNUM = dr.GetDecimal(dr.GetOrdinal("COM_PNUM"));
                }
                return item;
            }
        }

        public BEAjustesComision ObtenerDatos(string Owner, decimal Id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_AJUSTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@COM_ID", DbType.Decimal, Id);
            var lista = new List<BEAjustesComision>();
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEAjustesComision();
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COMT_ORIGEN")))
                        item.COMT_ORIGEN = dr.GetDecimal(dr.GetOrdinal("COMT_ORIGEN"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_VALUE")))
                        item.COM_VALUE = dr.GetDecimal(dr.GetOrdinal("COM_VALUE"));
                }
                return item;
            }
        }

        public int Insertar(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_INSERTAR_AJUSTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, en.COMT_ID);
            db.AddInParameter(oDbCommand, "@COMT_ORIGEN", DbType.Decimal, en.COMT_ORIGEN);
            db.AddInParameter(oDbCommand, "@BPS_ID ", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, en.LEVEL_ID);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, en.LIC_ID);
            db.AddInParameter(oDbCommand, "@COM_VALUE", DbType.Decimal, en.COM_VALUE);
            db.AddInParameter(oDbCommand, "@COM_PERC", DbType.Decimal, en.COM_PERC);
            db.AddInParameter(oDbCommand, "@COM_BASE", DbType.Decimal, en.COM_BASE);
            db.AddInParameter(oDbCommand, "@COM_PPIND", DbType.String, en.COM_PPIND);
            db.AddInParameter(oDbCommand, "@COM_PRIMARY", DbType.Decimal, en.COM_PRIMARY);
            db.AddInParameter(oDbCommand, "@COM_EST", DbType.String, en.COM_EST);
            db.AddInParameter(oDbCommand, "@PAY_ID", DbType.Decimal, en.PAY_ID);
            db.AddInParameter(oDbCommand, "@COM_INVOICE", DbType.Decimal, en.COM_INVOICE);
            db.AddInParameter(oDbCommand, "@COM_LDATE", DbType.DateTime, en.COM_LDATE);
            db.AddInParameter(oDbCommand, "@COM_RDATE", DbType.DateTime, en.COM_RDATE);
            db.AddInParameter(oDbCommand, "@COM_RDESC", DbType.String, en.COM_RDESC);
            db.AddInParameter(oDbCommand, "@COM_PDATE", DbType.DateTime, en.COM_PDATE);
            db.AddInParameter(oDbCommand, "@COM_PNUM", DbType.Decimal, en.COM_PNUM);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTUALIZAR_AJUSTE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@COM_ID", DbType.Decimal, en.COM_ID);
            db.AddInParameter(oDbCommand, "@COMT_ORIGEN", DbType.Decimal, en.COMT_ORIGEN);
            db.AddInParameter(oDbCommand, "@COM_VALUE", DbType.Decimal, en.COM_VALUE);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEAjustesComision TotalValorAjusteComision(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALOR_AJUSTE_COM");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@FECHA", DbType.DateTime, en.LOG_DATE_CREAT);
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, en.CUR_ALPHA);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, en.LIC_ID);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            BEAjustesComision item = null;

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEAjustesComision();
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_VALUE")))
                        item.COM_VALUE = dr.GetDecimal(dr.GetOrdinal("COM_VALUE"));
                }
            }
            return item;
        }

        public int ValidacionAjusteComision(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDAR_AJUSTE_COMISION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, en.LIC_ID);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }


        //LIBERACION RETENCIO DE COMISIONES
        public List<BEAjustesComision> ListarRetLibComisiones(string owner, decimal IdRepresentante, decimal IdTipoComision, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, decimal IdTarifa, decimal IdOficina, string IdMoneda, decimal IdDivAdm, DateTime FechaIni, DateTime FechaFin, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_COMISION_LIBERACION_RETENCION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, IdRepresentante);
            db.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, IdTipoComision);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, IdNivel);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, IdModalidad);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, IdEstablecimiento);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, IdLicencia);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, IdTarifa);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, IdOficina);
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, IdMoneda);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, IdDivAdm);
            db.AddInParameter(oDbCommand, "@Fechaini", DbType.DateTime, FechaIni);
            db.AddInParameter(oDbCommand, "@Fechafin", DbType.DateTime, FechaFin);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);
            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEAjustesComision>();
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEAjustesComision();
                    item.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_INVOICE")))
                        item.COM_INVOICE = dr.GetDecimal(dr.GetOrdinal("COM_INVOICE"));
                    item.COM_VALUE = dr.GetDecimal(dr.GetOrdinal("COM_VALUE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    item.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PERC")))
                        item.COM_PERC = dr.GetDecimal(dr.GetOrdinal("COM_PERC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_RDATE")))
                        item.COM_RDATE = dr.GetDateTime(dr.GetOrdinal("COM_RDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_RDATE")))
                        item.Retencion = true;
                    else
                        item.Retencion = false;

                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
                return lista;
            }
        }

        public BEAjustesComision RetencionLiberacionTotal(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_TOTAL_LIBERACION_RETENCION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, en.COMT_ID);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, en.LEVEL_ID);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, en.LIC_ID);
            db.AddInParameter(oDbCommand, "@RATE_ID", DbType.Decimal, en.RATE_ID);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, en.CUR_ALPHA);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, en.DAD_ID);
            db.AddInParameter(oDbCommand, "@Fechaini", DbType.DateTime, en.FechaIni);
            db.AddInParameter(oDbCommand, "@Fechafin", DbType.DateTime, en.FechaFin);
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEAjustesComision();
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_VALUE")))
                        item.COM_VALUE = dr.GetDecimal(dr.GetOrdinal("COM_VALUE"));
                }
                return item;
            }
        }

        public int ActivarRetencionLiberacion(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_RETENCION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, en.SEQUENCE);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public int InactivarRetencionLiberacion(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVAR_RETENCION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, en.SEQUENCE);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BEAjustesComision> ListarRetenciones(string Owner)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_LISTAR_RETENCION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            var lista = new List<BEAjustesComision>();
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEAjustesComision();
                    item.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                    lista.Add(item);
                }
                return lista;
            }
        }





        //PRE-LIQUIDACION DE COMISIONES
        public List<BEAjustesComision> ListarPreYliquidacionComisiones(string owner, decimal IdRepresentante, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, DateTime FechaIni, DateTime FechaFin, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_COMISION_PRELIQUIDACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, IdRepresentante);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, IdNivel);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, IdModalidad);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, IdEstablecimiento);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, IdLicencia);
            db.AddInParameter(oDbCommand, "@Fechaini", DbType.DateTime, FechaIni);
            db.AddInParameter(oDbCommand, "@Fechafin", DbType.DateTime, FechaFin);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);
            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEAjustesComision>();
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEAjustesComision();
                    item.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_INVOICE")))
                        item.COM_INVOICE = dr.GetDecimal(dr.GetOrdinal("COM_INVOICE"));
                    item.COM_VALUE = dr.GetDecimal(dr.GetOrdinal("COM_VALUE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    item.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PERC")))
                        item.COM_PERC = dr.GetDecimal(dr.GetOrdinal("COM_PERC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_LDATE")))
                        item.COM_LDATE = dr.GetDateTime(dr.GetOrdinal("COM_LDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_LDATE")))
                        item.Liquidacion = true;
                    else
                        item.Liquidacion = false;

                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
                return lista;
            }
        }

        public BEAjustesComision PreYLiquidacionTotal(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_TOTAL_COMISION_PRELIQUIDACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, en.LEVEL_ID);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, en.LIC_ID);
            db.AddInParameter(oDbCommand, "@Fechaini", DbType.DateTime, en.FechaIni);
            db.AddInParameter(oDbCommand, "@Fechafin", DbType.DateTime, en.FechaFin);
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEAjustesComision();
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_VALUE")))
                        item.COM_VALUE = dr.GetDecimal(dr.GetOrdinal("COM_VALUE"));
                }
                return item;
            }
        }

        public int ActivarLiquidacion(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_LIQUIDACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, en.SEQUENCE);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BEAjustesComision> ListarLiquidacion(string Owner)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_LISTAR_LIQUIDACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            var lista = new List<BEAjustesComision>();
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEAjustesComision();
                    item.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                    lista.Add(item);
                }
                return lista;
            }
        }

        public BEAjustesComision obtenerDatosPorId(string Owner, decimal IdSequence)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_LIQUIDACION_ID");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "SEQUENCE", DbType.Decimal, IdSequence);
            var lista = new List<BEAjustesComision>();
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEAjustesComision();
                    item.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_INVOICE")))
                        item.COM_INVOICE = dr.GetDecimal(dr.GetOrdinal("COM_INVOICE"));
                    item.COM_VALUE = dr.GetDecimal(dr.GetOrdinal("COM_VALUE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    item.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PERC")))
                        item.COM_PERC = dr.GetDecimal(dr.GetOrdinal("COM_PERC"));
                }
                return item;
            }
        }


        //PAGO DE COMISIONES
        public List<BEAjustesComision> ListarComisionPago(string owner, decimal IdRepresentante, decimal IdNivel, decimal IdModalidad, decimal IdEstablecimiento, decimal IdLicencia, DateTime FechaIni, DateTime FechaFin, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_COMISION_PAGO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, IdRepresentante);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, IdNivel);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, IdModalidad);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, IdEstablecimiento);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, IdLicencia);
            db.AddInParameter(oDbCommand, "@Fechaini", DbType.DateTime, FechaIni);
            db.AddInParameter(oDbCommand, "@Fechafin", DbType.DateTime, FechaFin);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);
            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEAjustesComision>();
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEAjustesComision();
                    item.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_INVOICE")))
                        item.COM_INVOICE = dr.GetDecimal(dr.GetOrdinal("COM_INVOICE"));
                    item.COM_VALUE = dr.GetDecimal(dr.GetOrdinal("COM_VALUE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    item.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PERC")))
                        item.COM_PERC = dr.GetDecimal(dr.GetOrdinal("COM_PERC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PDATE")))
                        item.COM_PDATE = dr.GetDateTime(dr.GetOrdinal("COM_PDATE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COM_PDATE")))
                        item.Pago = true;
                    else
                        item.Pago = false;

                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
                return lista;
            }
        }

        public BEAjustesComision PagoTotal(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_TOTAL_COMISION_PAGO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, en.LEVEL_ID);
            db.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, en.MOD_ID);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, en.LIC_ID);
            db.AddInParameter(oDbCommand, "@Fechaini", DbType.DateTime, en.FechaIni);
            db.AddInParameter(oDbCommand, "@Fechafin", DbType.DateTime, en.FechaFin);
            var item = new BEAjustesComision();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEAjustesComision();
                    if (!dr.IsDBNull(dr.GetOrdinal("COM_VALUE")))
                        item.COM_VALUE = dr.GetDecimal(dr.GetOrdinal("COM_VALUE"));
                }
                return item;
            }
        }

        public int ActualizarPago(BEAjustesComision en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_PAGO_COMISION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, en.SEQUENCE);
            db.AddInParameter(oDbCommand, "@PAY_ID", DbType.Decimal, en.PAY_ID);
            db.AddInParameter(oDbCommand, "@COM_PDATE", DbType.DateTime, en.COM_PDATE);
            db.AddInParameter(oDbCommand, "@COM_PNUM", DbType.Decimal, en.COM_PNUM);
            int r = Convert.ToInt32(db.ExecuteNonQuery(oDbCommand));
            return r;
        }
    }
}
