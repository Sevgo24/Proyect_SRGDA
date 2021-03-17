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
    public class DAMultiRecibo
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEMultiRecibo multiRecibo)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_MULTI_RECIBO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, multiRecibo.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MREC_NMR", DbType.Decimal, multiRecibo.MREC_NMR);
            oDataBase.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, multiRecibo.NMR_ID);
            //oDataBase.AddInParameter(oDbCommand, "@MREC_DATE", DbType.DateTime, multiRecibo.MREC_DATE);
            oDataBase.AddInParameter(oDbCommand, "@MREC_TBASE", DbType.Decimal, multiRecibo.MREC_TBASE);
            oDataBase.AddInParameter(oDbCommand, "@MREC_TTAXES", DbType.Decimal, multiRecibo.MREC_TTAXES);
            oDataBase.AddInParameter(oDbCommand, "@MREC_TTDEDUCTION", DbType.Decimal, multiRecibo.MREC_TTDEDUCTION);
            oDataBase.AddInParameter(oDbCommand, "@MREC_TTOTAL", DbType.Decimal, multiRecibo.MREC_TTOTAL);
            oDataBase.AddInParameter(oDbCommand, "@MREC_OBSERVATION", DbType.String, multiRecibo.MREC_OBSERVATION);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, multiRecibo.LOG_USER_CREAT);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, multiRecibo.BPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, multiRecibo.BNK_ID);
            oDataBase.AddInParameter(oDbCommand, "@BACC_NUMBER", DbType.Decimal, multiRecibo.BACC_NUMBER);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, multiRecibo.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@CUR_VALUE", DbType.Decimal, multiRecibo.CUR_VALUE);
            oDataBase.AddInParameter(oDbCommand, "@MREC_STATUS", DbType.Int32, multiRecibo.ESTADO_MULTIRECIBO);
            oDataBase.AddOutParameter(oDbCommand, "@MREC_ID", DbType.Decimal, Convert.ToInt32(multiRecibo.MREC_ID));

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@MREC_ID"));
            return id;
        }

        public int InsertarDetalle(BEMultiReciboDetalle MultiReciboDetalle)
        {
            int result = 0;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASI_MULTI_RECIBO_DET"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, MultiReciboDetalle.OWNER);
                    oDataBase.AddInParameter(cm, "@MREC_ID", DbType.Decimal, MultiReciboDetalle.MREC_ID);
                    oDataBase.AddInParameter(cm, "@REC_ID", DbType.Decimal, MultiReciboDetalle.REC_ID);
                    oDataBase.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, MultiReciboDetalle.LOG_USER_CREAT);

                    result = oDataBase.ExecuteNonQuery(cm);
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw;

            }
            return result;
        }


        public List<BEMultiRecibo> Listar(string owner, decimal idSerie, string voucher,
                                          decimal idBanco, decimal idCuenta, DateTime? fini, DateTime? ffin,
                                        int conFecha, int idBps, int estado, decimal idOficina, int estadoConfirmacion, decimal numRecibo, decimal idCobro,int estadoCobro,
                                        int pagina, int cantRegxPag)
        {
            try{
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_BEC_CONSULTA");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@SERIE", DbType.Decimal, idSerie);
                oDataBase.AddInParameter(oDbCommand, "@VOUCHER", DbType.String, voucher);
                oDataBase.AddInParameter(oDbCommand, "@BANCO", DbType.Decimal, idBanco);
                oDataBase.AddInParameter(oDbCommand, "@CUENTA", DbType.Decimal, idCuenta);
                oDataBase.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, fini);
                oDataBase.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, ffin);
                oDataBase.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
                oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idBps);
                oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);
                oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
                oDataBase.AddInParameter(oDbCommand, "@ESTADO_CONFIRMACION", DbType.Decimal, estadoConfirmacion);
                oDataBase.AddInParameter(oDbCommand, "@NUM_SERIE", DbType.Decimal, numRecibo);
                oDataBase.AddInParameter(oDbCommand, "@IDCOBRO", DbType.Decimal, idCobro);
                oDataBase.AddInParameter(oDbCommand, "@ESTADO_COBRO", DbType.Int32, estadoCobro);
                oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
                oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
                oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, idBps);
                //oDataBase.ExecuteNonQuery(oDbCommand);
                //string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

                var lista = new List<BEMultiRecibo>();
                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    BEMultiRecibo multiRecibo = null;
                    while (dr.Read())
                    {
                        multiRecibo = new BEMultiRecibo();
                        multiRecibo.MREC_ID = dr.GetDecimal(dr.GetOrdinal("MREC_ID"));
                        multiRecibo.REC_ID = dr.GetDecimal(dr.GetOrdinal("ID"));
                        multiRecibo.SERIAL = dr.GetString(dr.GetOrdinal("SERIE"));
                        multiRecibo.MREC_NUMBER = dr.GetString(dr.GetOrdinal("NUMERADOR"));
                        multiRecibo.MREC_DATE = dr.GetDateTime(dr.GetOrdinal("FECHA_CREACION"));
                        multiRecibo.MREC_TTOTAL = dr.GetDecimal(dr.GetOrdinal("TOTAL_RECIBO"));
                        multiRecibo.FECHA = String.Format("{0:dd/MM/yyyy}", multiRecibo.MREC_DATE);
                        multiRecibo.RECIBO_BEC = multiRecibo.SERIAL + " - " + multiRecibo.MREC_NUMBER;
                        multiRecibo.STRMREC_TTOTAL = multiRecibo.MREC_TTOTAL.ToString("### ##0.000");
                        multiRecibo.VERSION = dr.GetString(dr.GetOrdinal("VERSION"));
                        multiRecibo.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));

                        multiRecibo.ESTADO_MULTIRECIBO = dr.GetInt32(dr.GetOrdinal("ESTADO_MULTIRECIBO"));
                        multiRecibo.ESTADO_MULTIRECIBO_DES = dr.GetString(dr.GetOrdinal("ESTADO_MULTIRECIBO_DES"));
                        multiRecibo.ESTADO_CONFIRMACION = dr.GetInt32(dr.GetOrdinal("ESTADO_CONFIRMACION"));
                        multiRecibo.ESTADO_CONFIRMACION_DES = dr.GetString(dr.GetOrdinal("ESTADO_CONFIRMACION_DES"));
                        multiRecibo.ESTADO_COBRO = dr.GetString(dr.GetOrdinal("ESTADO"));
                        multiRecibo.TotalVirtual = dr.GetInt32(dr.GetOrdinal("RecordCount"));
                        lista.Add(multiRecibo);
                    }
                }
                return lista;
            }catch (Exception ex) {
                return null;
            }
        }

        public BEMultiRecibo ObtenerCab(string owner, decimal Id)
        {
            BEMultiRecibo Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_BEC_CAB"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@MREC_ID", DbType.Decimal, Id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEMultiRecibo();
                        Obj.MREC_ID = dr.GetDecimal(dr.GetOrdinal("MREC_ID"));
                        Obj.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                        Obj.SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        //Obj.MONTO = dr.GetDecimal(dr.GetOrdinal("REC_PVALUE"));

                        Obj.BANCO = dr.GetString(dr.GetOrdinal("BNK_ID"));
                        Obj.SUCURSAL = dr.GetString(dr.GetOrdinal("BRCH_ID"));
                        Obj.CUENTA = dr.GetString(dr.GetOrdinal("BACC_NUMBER"));
                        Obj.VOUCHER = dr.GetString(dr.GetOrdinal("REC_REFERENCE")).ToUpper();
                        Obj.MREC_OBSERVATION = dr.GetString(dr.GetOrdinal("MREC_OBSERVATION")).ToUpper();

                        Obj.NMR_ID_REC = dr.GetDecimal(dr.GetOrdinal("NMR_ID_REC"));
                        Obj.SERIAL_BEC = dr.GetString(dr.GetOrdinal("NMR_SERIAL_REC"));
                        Obj.MREC_DATE = dr.GetDateTime(dr.GetOrdinal("MREC_DATE"));
                        Obj.MREC_NMR = dr.GetDecimal(dr.GetOrdinal("MREC_NMR"));
                        Obj.MREC_TBASE = dr.GetDecimal(dr.GetOrdinal("MREC_TBASE"));
                        Obj.MREC_TTAXES = dr.GetDecimal(dr.GetOrdinal("MREC_TTAXES"));
                        Obj.MREC_TTDEDUCTION = dr.GetDecimal(dr.GetOrdinal("MREC_TTDEDUCTION"));
                        Obj.MREC_TTOTAL = dr.GetDecimal(dr.GetOrdinal("MREC_TTOTAL"));
                        Obj.MREC_TTDISCOUNT = dr.GetDecimal(dr.GetOrdinal("MREC_TTDISCOUNT"));
                    }
                }
            }
            return Obj;
        }

        public List<BEReciboDetalle> ObtenerDet(string owner, decimal Id)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_BEC_DET");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@MREC_ID", DbType.Decimal, Id);
            BEReciboDetalle item = null;
            List<BEReciboDetalle> lista = new List<BEReciboDetalle>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEReciboDetalle();
                    item.REC_DID = dr.GetDecimal(dr.GetOrdinal("REC_DID"));
                    item.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                    item.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                    item.NMR_NOW = dr.GetDecimal(dr.GetOrdinal("NMR_NOW"));
                    item.REC_BASE = dr.GetDecimal(dr.GetOrdinal("REC_BASE"));
                    item.REC_TAXES = dr.GetDecimal(dr.GetOrdinal("REC_TAXES"));
                    item.REC_DEDUCTIONS = dr.GetDecimal(dr.GetOrdinal("REC_DEDUCTIONS"));
                    item.REC_DISCOUNTS = dr.GetDecimal(dr.GetOrdinal("REC_DISCOUNTS"));
                    item.REC_TOTAL = dr.GetDecimal(dr.GetOrdinal("REC_TOTAL"));
                    item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    item.FACTURA = item.NMR_SERIAL + " - " + item.NMR_NOW.ToString();

                    item.NMR_ID_REC = dr.GetDecimal(dr.GetOrdinal("NMR_ID_REC"));
                    item.NMR_SERIAL_REC = dr.GetString(dr.GetOrdinal("NMR_SERIAL_REC"));
                    item.NMR_NOW_REC = dr.GetDecimal(dr.GetOrdinal("REC_NUMBER_REC"));
                    item.RECIBO = item.NMR_SERIAL_REC + " - " + item.NMR_NOW_REC.ToString();

                    lista.Add(item);
                }
            }
            return lista;
        }

        public BEMultiRecibo ObtenerCabecera_PagoXDoc(string owner, decimal idRecibo, string version)
        {
            BEMultiRecibo Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_RECIBO_CABECERA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@REC_ID", DbType.Decimal, idRecibo);
                oDataBase.AddInParameter(cm, "@VERSION", DbType.String, version);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEMultiRecibo();
                        Obj.MREC_ID = dr.GetDecimal(dr.GetOrdinal("MREC_ID"));
                        Obj.REC_ID = dr.GetDecimal(dr.GetOrdinal("REC_ID"));
                        Obj.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                        Obj.SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        Obj.MREC_OBSERVATION = dr.GetString(dr.GetOrdinal("OBSERVACION")).ToUpper();
                        Obj.TIPO = dr.GetString(dr.GetOrdinal("TIPO"));
                        Obj.MREC_DATE = dr.GetDateTime(dr.GetOrdinal("FECHA"));
                        Obj.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                        Obj.BACC_NUMBER = dr.GetDecimal(dr.GetOrdinal("BACC_NUMBER"));
                        Obj.TIPO_MONEDA = dr.GetString(dr.GetOrdinal("TYPO_MONEDA"));
                        Obj.MREC_TFACTURAS = dr.GetDecimal(dr.GetOrdinal("T_FACTURAS"));
                        Obj.MREC_TDEPOSITOS = dr.GetDecimal(dr.GetOrdinal("T_DEPOSITO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_VALUE")))
                            Obj.CUR_VALUE = dr.GetDecimal(dr.GetOrdinal("CUR_VALUE"));
                        Obj.ESTADO_MULTIRECIBO = dr.GetInt32(dr.GetOrdinal("MREC_STATUS"));
                        Obj.ESTADO_MULTIRECIBO_DES = dr.GetString(dr.GetOrdinal("MREC_ESTADO_DES"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            Obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            Obj.FECHA = dr.GetString(dr.GetOrdinal("LOG_DATE_CREAT"));
                    }
                }
            }
            return Obj;
        }

        public List<BEValoresConfig> ValoresConfig(string tipo, string subTipo)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_VALORES_CONFIG");
            db.AddInParameter(oDbCommand, "@VTYPE", DbType.String, tipo);
            db.AddInParameter(oDbCommand, "@VSUB_TYPE", DbType.String, subTipo);
            BEValoresConfig item = null;
            List<BEValoresConfig> lista = new List<BEValoresConfig>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEValoresConfig();
                    item.ID_VALUE = dr.GetDecimal(dr.GetOrdinal("ID_VALUE"));
                    item.VTYPE = dr.GetString(dr.GetOrdinal("VTYPE"));
                    item.VSUB_TYPE = dr.GetString(dr.GetOrdinal("VSUB_TYPE"));
                    item.VDESC = dr.GetString(dr.GetOrdinal("VDESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("VALUE")))
                        item.VALUE = dr.GetString(dr.GetOrdinal("VALUE")); ;
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Actualizar(BEMultiRecibo multiRecibo)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_MULTIRECIBO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, multiRecibo.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MREC_ID", DbType.Decimal, multiRecibo.MREC_ID);
            oDataBase.AddInParameter(oDbCommand, "@MREC_NMR", DbType.Decimal, multiRecibo.MREC_NMR);
            oDataBase.AddInParameter(oDbCommand, "@MREC_TBASE", DbType.Decimal, multiRecibo.MREC_TBASE);
            oDataBase.AddInParameter(oDbCommand, "@MREC_TTAXES", DbType.Decimal, multiRecibo.MREC_TTAXES);
            oDataBase.AddInParameter(oDbCommand, "@MREC_TTDEDUCTION", DbType.Decimal, multiRecibo.MREC_TTDEDUCTION);
            oDataBase.AddInParameter(oDbCommand, "@MREC_TTOTAL", DbType.Decimal, multiRecibo.MREC_TTOTAL);
            oDataBase.AddInParameter(oDbCommand, "@MREC_OBSERVATION", DbType.String, multiRecibo.MREC_OBSERVATION);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, multiRecibo.LOG_USER_UPDAT);
            oDataBase.AddInParameter(oDbCommand, "@MREC_STATUS", DbType.Int32, multiRecibo.ESTADO_MULTIRECIBO);
            int result = oDataBase.ExecuteNonQuery(oDbCommand);
            return result;
        }

        public List<BEMultiRecibo> ObtenerRecibosXIdCobro(string owner, decimal idCobro)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDSS_OBTENER_RECIBOS_MULTIRECIBO_IDCOBRO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@MREC_ID", DbType. Decimal, idCobro);
            BEMultiRecibo item = null;
            List<BEMultiRecibo> lista = new List<BEMultiRecibo>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEMultiRecibo();
                    item.REC_ID = dr.GetDecimal(dr.GetOrdinal("REC_ID"));
                    lista.Add(item);
                }
            }
            return lista;
        }
        public int ActualizarBanco(BEMultiRecibo multiRecibo)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_COBRO_ACT_BANCO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, multiRecibo.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MREC_ID", DbType.Decimal, multiRecibo.MREC_ID);
            oDataBase.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, multiRecibo.BNK_ID);
            oDataBase.AddInParameter(oDbCommand, "@BACC_NUMBER", DbType.Decimal, multiRecibo.BACC_NUMBER);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, multiRecibo.LOG_USER_UPDAT);
            int result = oDataBase.ExecuteNonQuery(oDbCommand);
            return result;
        }
        public int EliminarCobro(BEMultiRecibo multiRecibo)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ELIMINAR_COBRO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, multiRecibo.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MREC_ID", DbType.Decimal, multiRecibo.MREC_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, multiRecibo.LOG_USER_UPDAT);
            int result = oDataBase.ExecuteNonQuery(oDbCommand);
            return result;
        }
        public decimal ObtenerMontoMinimo()
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_DIFERENCIA_MINIMA");
            decimal r = Convert.ToDecimal(oDataBase.ExecuteScalar(oDbComand));

            return r;
        }


        public int ObtenerValidacionMontoBEC(decimal CodigoCobro)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_VALIDACION_COBRO");
            oDataBase.AddInParameter(oDbComand, "@MREC_ID", DbType.Decimal, CodigoCobro);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbComand));

            return r;
        }

        public int ActualizarBancoFecDeposito(BEMultiRecibo multiRecibo)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_COBRO_ACT_BANCO_FEC_DEP");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, multiRecibo.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MREC_ID", DbType.Decimal, multiRecibo.MREC_ID);
            oDataBase.AddInParameter(oDbCommand, "@BNK_ID", DbType.Decimal, multiRecibo.BNK_ID);
            oDataBase.AddInParameter(oDbCommand, "@BACC_NUMBER", DbType.Decimal, multiRecibo.BACC_NUMBER);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, multiRecibo.LOG_USER_UPDAT);
            oDataBase.AddInParameter(oDbCommand, "@DATE_DEPOSITO", DbType.String, multiRecibo.FECH_DEPO);
            oDataBase.AddInParameter(oDbCommand, "@NRO_CONFIRMACION", DbType.String, multiRecibo.VOUCHER);
            int result = oDataBase.ExecuteNonQuery(oDbCommand);
            return result;
        }

    }
}



