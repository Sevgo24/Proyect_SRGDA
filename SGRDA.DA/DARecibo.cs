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
    public class DARecibo
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BERecibo en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAI_RECIBO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddOutParameter(oDbCommand, "@REC_ID", DbType.Decimal, Convert.ToInt32(en.REC_ID));
            db.AddInParameter(oDbCommand, "@NMR_ID", DbType.String, en.NMR_ID);
            db.AddInParameter(oDbCommand, "@REC_NUMBER", DbType.String, en.REC_NUMBER);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@REC_TBASE", DbType.Decimal, en.REC_TBASE);
            db.AddInParameter(oDbCommand, "@REC_TTOTAL", DbType.Decimal, en.REC_TTOTAL);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@REC_ID"));
            return id;
        }

        public int ActualizarSerie(string owner, decimal? id, string tipo, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_SERIE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@NMR_TYPE", DbType.String, tipo);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, user.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarTotalQuitar(BERecibo en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_RECIBO_TOTAL_QUITAR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@REC_ID", DbType.String, en.REC_ID);
            db.AddInParameter(oDbCommand, "@REC_TBASE", DbType.String, en.REC_TBASE);
            db.AddInParameter(oDbCommand, "@REC_TTOTAL", DbType.String, en.REC_TTOTAL);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarTotalAgregar(BERecibo en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_RECIBO_TOTAL_AGREGAR");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@REC_ID", DbType.String, en.REC_ID);
            db.AddInParameter(oDbCommand, "@REC_TBASE", DbType.String, en.REC_TBASE);
            db.AddInParameter(oDbCommand, "@REC_TTOTAL", DbType.String, en.REC_TTOTAL);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BERecibo> ListarRecibosPendientes(string owner, decimal usuDerecho)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_LISTAR_RECIBOS_PEN");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, usuDerecho);
            db.ExecuteNonQuery(oDbCommand);

            var lista = new List<BERecibo>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BERecibo item = null;
                while (dr.Read())
                {
                    item = new BERecibo();
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_ID")))
                        item.REC_ID = dr.GetDecimal(dr.GetOrdinal("REC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                        item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_DATE")))
                        item.REC_DATE = dr.GetDateTime(dr.GetOrdinal("REC_DATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_TBASE")))
                        item.REC_TBASE = dr.GetDecimal(dr.GetOrdinal("REC_TBASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_TTOTAL")))
                        item.REC_TTOTAL = dr.GetDecimal(dr.GetOrdinal("REC_TTOTAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_OBSERVATION")))
                        item.REC_OBSERVATION = dr.GetString(dr.GetOrdinal("REC_OBSERVATION"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public BERecibo ObtenerDatos(string owner, decimal idRecibo)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_DATOS_RECIBO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@REC_ID", DbType.Decimal, idRecibo);
            db.ExecuteNonQuery(oDbCommand);

            BERecibo item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BERecibo();
                    if (!dr.IsDBNull(dr.GetOrdinal("NMR_ID")))
                        item.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_NUMBER")))
                        item.REC_NUMBER = dr.GetDecimal(dr.GetOrdinal("REC_NUMBER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_TTOTAL")))
                        item.REC_TTOTAL = dr.GetDecimal(dr.GetOrdinal("REC_TTOTAL"));
                }
            }
            return item;
        }

        public int InsertarDetalle(BEReciboDetalle en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAI_RECIBO_DET");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@REC_ID", DbType.Decimal, en.REC_ID);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, en.INV_ID);
            db.AddInParameter(oDbCommand, "@INV_EXPID", DbType.String, en.INV_EXPID);
            db.AddInParameter(oDbCommand, "@REC_BASE", DbType.Decimal, en.REC_BASE);
            db.AddInParameter(oDbCommand, "@REC_TAXES", DbType.Decimal, en.REC_TAXES);
            db.AddInParameter(oDbCommand, "@REC_TOTAL", DbType.Decimal, en.REC_TOTAL);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEReciboDetalle ObtenerDatosDetalle(string owner, decimal idRecibo)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_DETALLE_REC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@REC_ID", DbType.Decimal, idRecibo);
            db.ExecuteNonQuery(oDbCommand);

            BEReciboDetalle item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEReciboDetalle();
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                        item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_BASE")))
                        item.REC_BASE = dr.GetDecimal(dr.GetOrdinal("REC_BASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_TAXES")))
                        item.REC_TAXES = dr.GetDecimal(dr.GetOrdinal("REC_TAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_TOTAL")))
                        item.REC_TOTAL = dr.GetDecimal(dr.GetOrdinal("REC_TOTAL"));
                }
            }
            return item;
        }

        public int InsertarCabRecibo(BERecibo recibo)
        {
            bool exito = false;
            int result = 0;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASI_RECIBO_BEC"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, recibo.OWNER);
                    db.AddInParameter(cm, "@NMR_ID", DbType.Decimal, recibo.NMR_ID);
                    db.AddInParameter(cm, "@BPS_ID", DbType.Decimal, recibo.BPS_ID);
                    db.AddInParameter(cm, "@REC_TBASE", DbType.Decimal, recibo.REC_TBASE);
                    db.AddInParameter(cm, "@REC_TTAXES", DbType.Decimal, recibo.REC_TTAXES);
                    db.AddInParameter(cm, "@REC_TDEDUCTIONS", DbType.Decimal, recibo.REC_TDEDUCTIONS);
                    db.AddInParameter(cm, "@REC_TTOTAL", DbType.Decimal, recibo.REC_TTOTAL);
                    db.AddInParameter(cm, "@REC_OBSERVATION", DbType.String, recibo.REC_OBSERVATION);
                    db.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, recibo.LOG_USER_CREAT);
                    db.AddInParameter(cm, "@CUR_ALPHA", DbType.String, recibo.CUR_ALPHA);
                    db.AddOutParameter(cm, "@REC_ID", DbType.Decimal, Convert.ToInt32(recibo.REC_ID));
                    result = Convert.ToInt32(db.ExecuteScalar(cm));
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw;
            }
            return result;
        }

        public bool ActualizacionCabFact(string xml, string owner)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASU_FACT_CAB_BEC"))
                {
                    db.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    exito = db.ExecuteNonQuery(cm) > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return exito;
        }

        public bool ActualizacionDetFact(string xml, string owner)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASU_FACT_DET_BEC"))
                {
                    db.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    exito = db.ExecuteNonQuery(cm) > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return exito;
        }

        public bool ActualizacionExpFact(string xml, string owner)
        {
            bool exito = false;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASU_FACT_EXP_BEC"))
                {
                    db.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    exito = db.ExecuteNonQuery(cm) > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return exito;
        }

        public int VoucherDuplicidad(string owner, string idBanco, string fechaDeposito, string Voucher, decimal idVoucher)
         {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_VOUCHER_VAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BNK_ID", DbType.String, idBanco);
            db.AddInParameter(oDbCommand, "@FEC_DEPOSITO", DbType.String, fechaDeposito);
            db.AddInParameter(oDbCommand, "@VOUCHER", DbType.String, Voucher);
            db.AddInParameter(oDbCommand, "@REC_PID", DbType.Decimal, idVoucher);            
            BESociedad obj = new BESociedad();
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public SocioNegocio ObtenerCliente(string owner, decimal idBps)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_SOCIO_BEC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idBps);


            SocioNegocio item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new SocioNegocio();
                    //if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                    //    item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    item.ENT_TYPE_NOMBRE = dr.GetString(dr.GetOrdinal("TIPO_PERSONA"));
                    item.TAXN_NAME = dr.GetString(dr.GetOrdinal("TIPO_DOC"));
                    item.TAX_ID = dr.GetString(dr.GetOrdinal("NUM_DOC"));
                    item.BPS_NAME = dr.GetString(dr.GetOrdinal("SOCIO"));
                }
            }
            return item;
        }

        public List<BERecibo> ObtenerRecibosCliente(string owner, decimal idRecibo, string version)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_RECIBO_CLI");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@REC_ID", DbType.Decimal, idRecibo);
            db.AddInParameter(oDbCommand, "@VERSION", DbType.String, version);

            List<BERecibo> Lista = new List<BERecibo>();
            BERecibo item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BERecibo();
                    item.REC_ID = dr.GetDecimal(dr.GetOrdinal("REC_ID"));
                    item.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                    item.SERIE = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                    item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    item.REC_NUMBER = dr.GetDecimal(dr.GetOrdinal("REC_NUMBER"));
                    item.TIPO_PERSONA = dr.GetString(dr.GetOrdinal("TIPO_PERSONA"));
                    item.TIPO_DOC = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                    item.NUM_DOC = dr.GetString(dr.GetOrdinal("TAX_ID"));
                    item.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));

                    item.REC_TBASE = dr.GetDecimal(dr.GetOrdinal("REC_TBASE"));
                    item.REC_TTAXES = dr.GetDecimal(dr.GetOrdinal("REC_TTAXES"));
                    item.REC_TDEDUCTIONS = dr.GetDecimal(dr.GetOrdinal("REC_TDEDUCTIONS"));
                    item.REC_TTOTAL = dr.GetDecimal(dr.GetOrdinal("REC_TTOTAL"));

                    item.REC_OBSERVATION = dr.GetString(dr.GetOrdinal("REC_OBSERVATION"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    Lista.Add(item);
                }
            }
            return Lista;
        }

        public BERecibo ObtenerMultiReciboDetalle(string owner, decimal idMultiRecibo, decimal idRecibo)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDSS_OBTENER_RECIBO_MULTIRECIBO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@MREC_ID", DbType.Decimal, idMultiRecibo);
            db.AddInParameter(oDbCommand, "@REC_ID", DbType.Decimal, idRecibo);
            db.ExecuteNonQuery(oDbCommand);

            BERecibo item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BERecibo();
                    item.REC_ID = dr.GetDecimal(dr.GetOrdinal("REC_ID"));
                    item.REC_TBASE = dr.GetDecimal(dr.GetOrdinal("REC_TBASE"));
                    item.REC_TTOTAL = dr.GetDecimal(dr.GetOrdinal("REC_TTOTAL"));
                }
            }
            return item;
        }

        public int ActualizarCabRecibo(BERecibo recibo)
        {
            int result = 0;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASU_RECIBO_COBRO"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, recibo.OWNER);
                    db.AddInParameter(cm, "@REC_ID", DbType.Decimal, recibo.@REC_ID);
                    db.AddInParameter(cm, "@REC_TBASE", DbType.Decimal, recibo.REC_TBASE);
                    db.AddInParameter(cm, "@REC_TTAXES", DbType.Decimal, recibo.REC_TTAXES);
                    db.AddInParameter(cm, "@REC_TDEDUCTIONS", DbType.Decimal, recibo.REC_TDEDUCTIONS);
                    db.AddInParameter(cm, "@REC_TTOTAL", DbType.Decimal, recibo.REC_TTOTAL);
                    db.AddInParameter(cm, "@LOG_USER_UPDATE", DbType.String, recibo.LOG_USER_UPDATE);
                    result = Convert.ToInt32(db.ExecuteNonQuery(cm));
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw;
            }
            return result;
        }

        public int VoucherRepetidosConfirmados(decimal idVoucher, string nro_confirmacion)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_VOUCHER_REPETIDOS");
            db.AddInParameter(oDbCommand, "@ID_DEPOSTIO", DbType.Decimal, idVoucher);
            db.AddInParameter(oDbCommand, "@NRO_CONFIRMACION", DbType.String, nro_confirmacion);
            BESociedad obj = new BESociedad();
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }


    }
}

