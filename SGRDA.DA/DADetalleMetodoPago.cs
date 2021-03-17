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
    public class DADetalleMetodoPago
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEDetalleMetodoPago en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAI_METODOS_PAGO_DET");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@REC_ID", DbType.Decimal, en.REC_ID);
            db.AddInParameter(oDbCommand, "@REC_PWID", DbType.String, en.REC_PWID);
            db.AddInParameter(oDbCommand, "@REC_PVALUE", DbType.Decimal, en.REC_PVALUE);
            db.AddInParameter(oDbCommand, "@REC_CONFIRMED", DbType.String, en.REC_CONFIRMED);
            db.AddInParameter(oDbCommand, "@REC_DATEDEPOSITE", DbType.DateTime, en.REC_DATEDEPOSITE);
            db.AddInParameter(oDbCommand, "@BNK_ID", DbType.String, en.BNK_ID);
            db.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, en.BRCH_ID);
            db.AddInParameter(oDbCommand, "@BACC_NUMBER", DbType.String, en.BACC_NUMBER);
            db.AddInParameter(oDbCommand, "@REC_REFERENCE", DbType.String, en.REC_REFERENCE != null ? en.REC_REFERENCE.ToUpper() : string.Empty);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEDetalleMetodoPago> ListarMetodoPago(string owner, decimal IdRecibo)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_METODO_PAGO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@REC_ID", DbType.Decimal, IdRecibo);

            var lista = new List<BEDetalleMetodoPago>();
            BEDetalleMetodoPago item;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    item = new BEDetalleMetodoPago();
                    if (!reader.IsDBNull(reader.GetOrdinal("REC_ID")))
                        item.REC_ID = Convert.ToDecimal(reader["REC_ID"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("REC_PID")))
                        item.REC_PID = Convert.ToDecimal(reader["REC_PID"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("REC_PWID")))
                        item.REC_PWID = Convert.ToString(reader["REC_PWID"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("REC_PWDESC")))
                        item.REC_PWDESC = Convert.ToString(reader["REC_PWDESC"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("REC_PVALUE")))
                        item.REC_PVALUE = Convert.ToDecimal(reader["REC_PVALUE"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("REC_CONFIRMED")))
                        item.REC_CONFIRMED = Convert.ToString(reader["REC_CONFIRMED"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("REC_DATEDEPOSITE")))
                        item.REC_DATEDEPOSITE = Convert.ToDateTime(reader["REC_DATEDEPOSITE"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("BNK_ID")))
                        item.BNK_ID = Convert.ToString(reader["BNK_ID"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("BNK_NAME")))
                        item.BNK_NAME = Convert.ToString(reader["BNK_NAME"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("BRCH_ID")))
                        item.BRCH_ID = Convert.ToString(reader["BRCH_ID"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("BRCH_NAME")))
                        item.BRCH_NAME = Convert.ToString(reader["BRCH_NAME"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("BACC_NUMBER")))
                        item.BACC_NUMBER = Convert.ToString(reader["BACC_NUMBER"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("REC_REFERENCE")))
                        item.REC_REFERENCE = Convert.ToString(reader["REC_REFERENCE"]);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<BEDetalleMetodoPago> ListarMetodoPagoComoDetalle(string owner, decimal IdRecibo)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            BEDetalleMetodoPago item = null;
            var lista = new List<BEDetalleMetodoPago>();
            try
            {
                using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_LISTAR_METODO_PAGO_DET"))
                {
                    oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(oDbCommand, "@REC_ID", DbType.Decimal, IdRecibo);

                    using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
                    {
                        while (reader.Read())
                        {
                            item = new BEDetalleMetodoPago();
                            if (!reader.IsDBNull(reader.GetOrdinal("REC_ID")))
                                item.REC_ID = Convert.ToDecimal(reader["REC_ID"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("REC_PID")))
                                item.REC_PID = Convert.ToDecimal(reader["REC_PID"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("REC_PWID")))
                                item.REC_PWID = Convert.ToString(reader["REC_PWID"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("REC_PWDESC")))
                                item.REC_PWDESC = Convert.ToString(reader["REC_PWDESC"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("REC_PVALUE")))
                                item.REC_PVALUE = Convert.ToDecimal(reader["REC_PVALUE"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("REC_CONFIRMED")))
                                item.REC_CONFIRMED = Convert.ToString(reader["REC_CONFIRMED"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("REC_DATEDEPOSITE")))
                                item.REC_DATEDEPOSITE = Convert.ToDateTime(reader["REC_DATEDEPOSITE"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("BNK_ID")))
                                item.BNK_ID = Convert.ToString(reader["BNK_ID"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("BNK_NAME")))
                                item.BNK_NAME = Convert.ToString(reader["BNK_NAME"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("BRCH_ID")))
                                item.BRCH_ID = Convert.ToString(reader["BRCH_ID"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("BRCH_NAME")))
                                item.BRCH_NAME = Convert.ToString(reader["BRCH_NAME"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("BACC_NUMBER")))
                                item.BACC_NUMBER = Convert.ToString(reader["BACC_NUMBER"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("REC_REFERENCE")))
                                item.REC_REFERENCE = Convert.ToString(reader["REC_REFERENCE"]);
                            lista.Add(item);
                        }
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public BEMetodoPago ObtenerConfirmed(string owner, string Idpay)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDAS_CONFIRMED_METODPAY");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@REC_PWID", DbType.String, Idpay);

            BEMetodoPago item = null;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                if (reader.Read())
                {
                    item = new BEMetodoPago();
                    item.Confirmed = Convert.ToString(reader["CONFIRMACION"]);
                }
            }
            return item;
        }

        public REF_CURRENCY_VALUES ObtenerTipoCambio(string IdMoneda)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDAS_TIPOCAMBIO");
            oDataBase.AddInParameter(oDbComand, "@CUR_ALPHA", DbType.String, IdMoneda);

            REF_CURRENCY_VALUES item = null;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                if (reader.Read())
                {
                    item = new REF_CURRENCY_VALUES();
                    item.CUR_VALUE = Convert.ToDecimal(reader["CUR_VALUE"]);
                }
            }
            return item;
        }

        public int Eliminar(BEDetalleMetodoPago en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAD_METODOPAGO_DET");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@REC_PID", DbType.Decimal, en.REC_PID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ObtenerDetalleEliminar(BEDetalleMetodoPago en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_METODOPAGO_DET");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@REC_PID", DbType.Decimal, en.REC_PID);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public int ObtenerDetalleValidar(string owner, decimal id)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_OBTENER_MET");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@REC_PID", DbType.Decimal, id);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public int InsertarDetallePagoBEC(BEDetalleMetodoPago voucher)
        {
            int result = 0;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASI_RECIBO_VOUCHER"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, voucher.OWNER);
                    db.AddInParameter(cm, "@MREC_ID", DbType.Decimal, voucher.MREC_ID);
                    db.AddInParameter(cm, "@REC_PWID", DbType.String, voucher.REC_PWID);
                    db.AddInParameter(cm, "@REC_PVALUE", DbType.String, voucher.REC_PVALUE);
                    db.AddInParameter(cm, "@BNK_ID", DbType.String, voucher.BNK_ID);
                    db.AddInParameter(cm, "@BRCH_ID", DbType.String, voucher.BRCH_ID);
                    db.AddInParameter(cm, "@BACC_NUMBER", DbType.String, voucher.BACC_NUMBER);
                    db.AddInParameter(cm, "@REC_REFERENCE", DbType.String, voucher.REC_REFERENCE);
                    db.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, voucher.LOG_USER_CREAT);
                    db.AddInParameter(cm, "@REC_CONFIRMED", DbType.String, voucher.REC_CONFIRMED);
                    //db.AddInParameter(cm, "@REC_CODECONFIRMED", DbType.String, voucher.REC_CODECONFIRMED);
                    db.AddInParameter(cm, "@REC_DATEDEPOSITE", DbType.DateTime, voucher.REC_DATEDEPOSITE);
                    db.AddInParameter(cm, "@CUR_ALPHA", DbType.String, voucher.CUR_ALPHA);
                    result = db.ExecuteNonQuery(cm);
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw;
            }
            return result;
        }

        public List<BEDetalleMetodoPago> ObtenerRecibosVoucher(string owner, decimal idRecibo, string version)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_RECIBO_VOUCHER");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@REC_ID", DbType.Decimal, idRecibo);
            db.AddInParameter(oDbCommand, "@VERSION", DbType.String, version);

            List<BEDetalleMetodoPago> Lista = new List<BEDetalleMetodoPago>();
            BEDetalleMetodoPago item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEDetalleMetodoPago();
                    item.REC_PID = dr.GetDecimal(dr.GetOrdinal("REC_PID"));
                    item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID")).ToString();
                    item.BNK_NAME = dr.GetString(dr.GetOrdinal("BNK_NAME"));
                    item.BPS_ACC_ID = dr.GetDecimal(dr.GetOrdinal("ID_CUENTA"));
                    item.BACC_NUMBER = dr.GetString(dr.GetOrdinal("NUM_CUENTA"));
                    item.REC_PWID = dr.GetString(dr.GetOrdinal("REC_PWID"));
                    item.REC_PWDESC = dr.GetString(dr.GetOrdinal("REC_PWDESC"));
                    item.REC_DATEDEPOSITE = dr.GetDateTime(dr.GetOrdinal("REC_DATEDEPOSITE"));
                    item.REC_REFERENCE = dr.GetString(dr.GetOrdinal("REC_REFERENCE"));
                    item.REC_PVALUE = dr.GetDecimal(dr.GetOrdinal("REC_PVALUE"));
                    item.REC_CONFIRMED = dr.GetString(dr.GetOrdinal("REC_CONFIRMED"));
                    item.ESTADO_DEPOSITO = dr.GetString(dr.GetOrdinal("ESTADO_DEPOSITO"));
                    item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                    item.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                    item.REC_OBSERVATION = dr.GetString(dr.GetOrdinal("REC_OBSERVATION"));
                    item.REC_OBSERVATION_USER = dr.GetString(dr.GetOrdinal("REC_OBSERVATION_USER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_DATECONFIRMED")))
                        item.REC_DATECONFIRMED = dr.GetDateTime(dr.GetOrdinal("REC_DATECONFIRMED"));
                    if (!dr.IsDBNull(dr.GetOrdinal("REC_CODECONFIRMED")))
                        item.REC_CODECONFIRMED = dr.GetString(dr.GetOrdinal("REC_CODECONFIRMED"));
                    item.DOC_ID = dr.GetDecimal(dr.GetOrdinal("DOC_ID"));
                    Lista.Add(item);
                }
            }
            return Lista;
        }

        public BEDetalleMetodoPago ObtenerRecibosVoucherXid(string owner, decimal red_pid)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_RECIBO_VOUCHER_ID");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@REC_PID", DbType.Decimal, red_pid);

            BEDetalleMetodoPago item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEDetalleMetodoPago();
                    item.REC_PID = dr.GetDecimal(dr.GetOrdinal("REC_PID"));
                    item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID")).ToString();
                    item.BNK_NAME = dr.GetString(dr.GetOrdinal("BNK_NAME"));
                    item.BPS_ACC_ID = dr.GetDecimal(dr.GetOrdinal("ID_CUENTA"));
                    item.BACC_NUMBER = dr.GetString(dr.GetOrdinal("NUM_CUENTA"));
                    item.REC_PWID = dr.GetString(dr.GetOrdinal("REC_PWID"));
                    item.REC_PWDESC = dr.GetString(dr.GetOrdinal("REC_PWDESC"));
                    item.REC_DATEDEPOSITE = dr.GetDateTime(dr.GetOrdinal("REC_DATEDEPOSITE"));
                    item.REC_REFERENCE = dr.GetString(dr.GetOrdinal("REC_REFERENCE"));
                    item.REC_PVALUE = dr.GetDecimal(dr.GetOrdinal("REC_PVALUE"));
                    item.REC_CONFIRMED = dr.GetString(dr.GetOrdinal("REC_CONFIRMED"));
                    item.ESTADO_DEPOSITO = dr.GetString(dr.GetOrdinal("ESTADO_DEPOSITO"));
                    item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));
                    item.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                    item.REC_OBSERVATION = dr.GetString(dr.GetOrdinal("REC_OBSERVATION"));
                    item.REC_OBSERVATION_USER = dr.GetString(dr.GetOrdinal("REC_OBSERVATION_USER"));
                    item.FECHA_DEP = dr.GetString(dr.GetOrdinal("FECHA"));
                }
            }
            return item;
        }

        public BEDetalleMetodoPago ObtenerComprobante(string owner, decimal id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_COMPROBANTE");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@REC_PID", DbType.Decimal, id);

            BEDetalleMetodoPago item = null;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                if (reader.Read())
                {
                    item = new BEDetalleMetodoPago();
                    item.OWNER = Convert.ToString(reader["OWNER"]);
                    item.REC_PID = Convert.ToDecimal(reader["REC_PID"]);
                    item.MREC_ID = Convert.ToDecimal(reader["MREC_ID"]);
                    item.BNK_NAME = Convert.ToString(reader["BANCO"]);
                    item.REC_PWDESC = Convert.ToString(reader["TIPO_PAGO"]);
                    item.FECHA_DEP = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(reader["FECHA_DEPOSITO"]));
                    item.REC_REFERENCE = Convert.ToString(reader["VOUCHER"]);
                    item.CUR_ALPHA = Convert.ToString(reader["CUR_ALPHA"]);
                    item.MONEDA = Convert.ToString(reader["MONEDA"]);
                    item.REC_PVALUE = Convert.ToDecimal(reader["MONTO"]);
                    item.ESTADO_DEPOSITO = Convert.ToString(reader["REC_CONFIRMED"]);
                    item.CUR_VALUE = Convert.ToDecimal(reader["CUR_VALUE"]);
                    item.CONVERSION_SOLES = Convert.ToDecimal(reader["CONVERSION_SOLES"]);
                    item.CONVERSION_SOLES_BALANCE = Convert.ToDecimal(reader["CONVERSION_SOLES_BALANCE"]);
                    item.OFF_ID = Convert.ToDecimal(reader["OFF_ID"]);

                    item.BNK_ID = Convert.ToString(reader["ID_BANCO"]);
                    item.BACC_NUMBER = Convert.ToString(reader["ID_CTA"]);
                    item.REC_CODECONFIRMED = Convert.ToString(reader["REC_CODECONFIRMED"]);
                }
            }
            return item;
        }

        public int ActualizarComprobante(BEDetalleMetodoPago comprobante)
        {
            int result = 0;
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_COMPROBANTE");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, comprobante.OWNER);
                oDataBase.AddInParameter(oDbComand, "@REC_PID", DbType.Decimal, comprobante.REC_PID);
                oDataBase.AddInParameter(oDbComand, "@REC_CONFIRMED", DbType.String, comprobante.REC_CONFIRMED);
                oDataBase.AddInParameter(oDbComand, "@REC_USERCONFIRMED", DbType.String, comprobante.REC_USERCONFIRMED);
                oDataBase.AddInParameter(oDbComand, "@REC_CODECONFIRMED", DbType.String, comprobante.REC_CODECONFIRMED);
                oDataBase.AddInParameter(oDbComand, "@REC_OBSERVATION", DbType.String, comprobante.REC_OBSERVATION);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, comprobante.LOG_USER_UPDATE);
                oDataBase.AddInParameter(oDbComand, "@MREC_ID", DbType.String, comprobante.MREC_ID);
                result = oDataBase.ExecuteNonQuery(oDbComand);
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        public int ActualizarComprobanteSaldo(BEDetalleMetodoPago comprobante)
        {
            int result = 0;
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_COMPROBANTE_SALDO");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, comprobante.OWNER);
                oDataBase.AddInParameter(oDbComand, "@REC_PID", DbType.Decimal, comprobante.REC_PID);
                oDataBase.AddInParameter(oDbComand, "@REC_BALANCE", DbType.Decimal, comprobante.REC_BALANCE);
                result = oDataBase.ExecuteNonQuery(oDbComand);
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        public int ModificarDatosVoucher(BEDetalleMetodoPago comprobante)
        {
            int result = 0;
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_COMPROBANTE");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, comprobante.OWNER);
                oDataBase.AddInParameter(oDbComand, "@REC_PID", DbType.Decimal, comprobante.REC_PID);
                oDataBase.AddInParameter(oDbComand, "@REC_CONFIRMED", DbType.String, comprobante.REC_CONFIRMED);
                oDataBase.AddInParameter(oDbComand, "@REC_USERCONFIRMED", DbType.String, comprobante.REC_USERCONFIRMED);
                oDataBase.AddInParameter(oDbComand, "@REC_CODECONFIRMED", DbType.String, comprobante.REC_CODECONFIRMED);
                oDataBase.AddInParameter(oDbComand, "@REC_OBSERVATION", DbType.String, comprobante.REC_OBSERVATION);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, comprobante.LOG_USER_UPDATE);
                oDataBase.AddInParameter(oDbComand, "@MREC_ID", DbType.String, comprobante.MREC_ID);
                result = oDataBase.ExecuteNonQuery(oDbComand);
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        public int ActualizarSinConfirmarVoucher(BEDetalleMetodoPago voucher)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_COMPROBANTE_RESET");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, voucher.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@REC_PID", DbType.Decimal, voucher.REC_PID);
            oDataBase.AddInParameter(oDbCommand, "@REC_PWID", DbType.String, voucher.REC_PWID);
            oDataBase.AddInParameter(oDbCommand, "@REC_PVALUE", DbType.Decimal, voucher.REC_PVALUE);
            oDataBase.AddInParameter(oDbCommand, "@REC_DATEDEPOSITE", DbType.DateTime, voucher.REC_DATEDEPOSITE);
            oDataBase.AddInParameter(oDbCommand, "@BNK_ID", DbType.String, voucher.BNK_ID);
            oDataBase.AddInParameter(oDbCommand, "@BRCH_ID", DbType.String, voucher.BRCH_ID);
            oDataBase.AddInParameter(oDbCommand, "@BACC_NUMBER", DbType.String, voucher.BACC_NUMBER);
            oDataBase.AddInParameter(oDbCommand, "@REC_REFERENCE", DbType.String, voucher.REC_REFERENCE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, voucher.LOG_USER_UPDATE);
            int result = oDataBase.ExecuteNonQuery(oDbCommand);
            return result;
        }



    }
}
