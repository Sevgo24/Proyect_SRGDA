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
    public class DADocumentoNoIdentificado
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        #region DOCUMENTO
        public int InsertarFacturaCabecera(BEDocumentoNoIdentificado obj)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DNI_FACTURA_CABECERA");
                oDataBase.AddInParameter(oDbComand, "@MONEDA", DbType.String, obj.ID_MONEDA);
                oDataBase.AddInParameter(oDbComand, "@MONTO", DbType.Decimal, obj.MONTO);
                oDataBase.AddInParameter(oDbComand, "@FECHA_CREA", DbType.DateTime, obj.FECHA_CREA);
                oDataBase.AddInParameter(oDbComand, "@USUARIO_CREA", DbType.String, obj.USUARIO_CREA);
                oDataBase.AddInParameter(oDbComand, "@ID_OFICINA", DbType.Decimal, obj.ID_OFICINA);
                int id = Convert.ToInt32(oDataBase.ExecuteScalar(oDbComand));
                retorno = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int InsertarFacturaDetalle(BEDocumentoNoIdentificado obj)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DNI_FACTURA_DETALLE");
                oDataBase.AddInParameter(oDbComand, "@MONTO", DbType.Decimal, obj.MONTO);
                oDataBase.AddInParameter(oDbComand, "@FECHA_CREA", DbType.DateTime, obj.FECHA_CREA);
                oDataBase.AddInParameter(oDbComand, "@USUARIO_CREA", DbType.String, obj.USUARIO_CREA);
                oDataBase.AddInParameter(oDbComand, "@INV_ID", DbType.Decimal, obj.INV_ID);
                int id = Convert.ToInt32(oDataBase.ExecuteScalar(oDbComand));
                retorno = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        #endregion

        #region COBRO
        public int InsertarCobroCabecera(BEDocumentoNoIdentificado obj)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DNI_COBRO_CABECERA");
                oDataBase.AddInParameter(oDbComand, "@MONEDA", DbType.String, obj.ID_MONEDA);
                oDataBase.AddInParameter(oDbComand, "@MONTO_SOLES", DbType.Decimal, obj.MONTO_SOLES);
                oDataBase.AddInParameter(oDbComand, "@FECHA_CREA", DbType.DateTime, obj.FECHA_CREA);
                oDataBase.AddInParameter(oDbComand, "@USUARIO_CREA", DbType.String, obj.USUARIO_CREA);
                oDataBase.AddInParameter(oDbComand, "@ID_OFICINA", DbType.Decimal, obj.ID_OFICINA);
                oDataBase.AddInParameter(oDbComand, "@TIPO_CAMBIO", DbType.Decimal, obj.TIPO_CAMBIO);
                oDataBase.AddInParameter(oDbComand, "@ID_BANCO", DbType.Int32, obj.ID_BANCO);
                oDataBase.AddInParameter(oDbComand, "@ID_CUENTA", DbType.Int32, obj.ID_CUENTA);
                int id = Convert.ToInt32(oDataBase.ExecuteScalar(oDbComand));
                retorno = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int InsertarReciboCabecera(BEDocumentoNoIdentificado obj)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DNI_RECIBO_CABECERA");
                oDataBase.AddInParameter(oDbComand, "@TIPO_CAMBIO", DbType.Decimal, obj.TIPO_CAMBIO);
                oDataBase.AddInParameter(oDbComand, "@MONTO_SOLES", DbType.Decimal, obj.MONTO_SOLES);
                oDataBase.AddInParameter(oDbComand, "@FECHA_CREA", DbType.DateTime, obj.FECHA_CREA);
                oDataBase.AddInParameter(oDbComand, "@USUARIO_CREA", DbType.String, obj.USUARIO_CREA);
                int id = Convert.ToInt32(oDataBase.ExecuteScalar(oDbComand));
                retorno = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int InsertarReciboDetalle(BEDocumentoNoIdentificado obj)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DNI_RECIBO_DETALLE");
                oDataBase.AddInParameter(oDbComand, "@REC_ID", DbType.String, obj.REC_ID);
                oDataBase.AddInParameter(oDbComand, "@INV_ID", DbType.Decimal, obj.INV_ID);
                oDataBase.AddInParameter(oDbComand, "@MONEDA", DbType.String, obj.ID_MONEDA);
                oDataBase.AddInParameter(oDbComand, "@MONTO", DbType.Decimal, obj.MONTO);
                oDataBase.AddInParameter(oDbComand, "@FECHA_CREA", DbType.DateTime, obj.FECHA_CREA);
                oDataBase.AddInParameter(oDbComand, "@USUARIO_CREA", DbType.String, obj.USUARIO_CREA);
                int id = Convert.ToInt32(oDataBase.ExecuteScalar(oDbComand));
                retorno = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int InsertarCobroDetalle(BEDocumentoNoIdentificado obj)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DNI_COBRO_DETALLE");
                oDataBase.AddInParameter(oDbComand, "@MREC_ID", DbType.Decimal, obj.MREC_ID);
                oDataBase.AddInParameter(oDbComand, "@REC_ID", DbType.String, obj.REC_ID);
                oDataBase.AddInParameter(oDbComand, "@FECHA_CREA", DbType.DateTime, obj.FECHA_CREA);
                oDataBase.AddInParameter(oDbComand, "@USUARIO_CREA", DbType.String, obj.USUARIO_CREA);
                int id = oDataBase.ExecuteNonQuery(oDbComand);
                retorno = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int InsertarDeposito(BEDocumentoNoIdentificado obj)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DNI_DEPOSITO");
                oDataBase.AddInParameter(oDbComand, "@MREC_ID", DbType.Decimal, obj.MREC_ID);
                oDataBase.AddInParameter(oDbComand, "@REC_PWID", DbType.String, obj.REC_PWID);
                oDataBase.AddInParameter(oDbComand, "@ID_BANCO", DbType.Decimal, obj.ID_BANCO);
                oDataBase.AddInParameter(oDbComand, "@MONEDA", DbType.String, obj.ID_MONEDA);
                oDataBase.AddInParameter(oDbComand, "@MONTO", DbType.Decimal, obj.MONTO);
                oDataBase.AddInParameter(oDbComand, "@FECHA_DEPOSITO", DbType.DateTime, obj.FECHA_DEPOSITO);
                oDataBase.AddInParameter(oDbComand, "@NRO_CONFIRMACION", DbType.String, obj.NRO_CONFIRMACION);
                oDataBase.AddInParameter(oDbComand, "@FECHA_CREA", DbType.DateTime, obj.FECHA_CREA);
                oDataBase.AddInParameter(oDbComand, "@USUARIO_CREA", DbType.String, obj.USUARIO_CREA);
                int id = Convert.ToInt32(oDataBase.ExecuteScalar(oDbComand));
                retorno = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int InsertaTransaccionRecaudo(BEDocumentoNoIdentificado obj)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DNI_RECAUDACION");
                oDataBase.AddInParameter(oDbComand, "@INV_ID", DbType.Decimal, obj.INV_ID);
                oDataBase.AddInParameter(oDbComand, "@INVL_ID", DbType.Decimal, obj.INVL_ID);
                oDataBase.AddInParameter(oDbComand, "@MREC_ID", DbType.Decimal, obj.MREC_ID);
                oDataBase.AddInParameter(oDbComand, "@REC_ID", DbType.Decimal, obj.REC_ID);
                oDataBase.AddInParameter(oDbComand, "@REC_PID", DbType.Decimal, obj.REC_PID);
                oDataBase.AddInParameter(oDbComand, "@MONEDA", DbType.String, obj.ID_MONEDA);
                oDataBase.AddInParameter(oDbComand, "@MONTO", DbType.Decimal, obj.MONTO);
                oDataBase.AddInParameter(oDbComand, "@MONTO_SOLES", DbType.Decimal, obj.MONTO_SOLES);
                oDataBase.AddInParameter(oDbComand, "@FECHA_CREA", DbType.DateTime, obj.FECHA_CREA);
                oDataBase.AddInParameter(oDbComand, "@USUARIO_CREA", DbType.String, obj.USUARIO_CREA);
                oDataBase.AddInParameter(oDbComand, "@OFF_ID", DbType.Decimal, obj.ID_OFICINA);
                int id = oDataBase.ExecuteNonQuery(oDbComand);
                retorno = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        #endregion

        #region LISTAR
        public List<BEDocumentoNoIdentificado> ListarDNI(decimal bnk_id, string fecha_ini,string fecha_fin,int estado)
        {
            List<BEDocumentoNoIdentificado> Lista = new List<BEDocumentoNoIdentificado>();
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DNI_FILTROS"))
                {
                    oDataBase.AddInParameter(cm, "@BNK_ID", DbType.Decimal, bnk_id);
                    oDataBase.AddInParameter(cm, "@FECHA_DEP_INI", DbType.String, fecha_ini);
                    oDataBase.AddInParameter(cm, "@FECHA_DEP_FIN", DbType.String, fecha_fin);
                    oDataBase.AddInParameter(cm, "@ESTADO", DbType.Int32, estado);
                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        BEDocumentoNoIdentificado obj = null;
                        while (dr.Read())
                        {
                            obj = new BEDocumentoNoIdentificado();
                            obj.REC_PID = dr.GetDecimal(dr.GetOrdinal("REC_PID"));
                            obj.REC_PWID = dr.GetString(dr.GetOrdinal("REC_PWID"));
                            obj.TipoPago = dr.GetString(dr.GetOrdinal("TIPO_PAGO"));
                            obj.ID_BANCO = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                            obj.BANCO_DESTINO = dr.GetString(dr.GetOrdinal("BANCO_DESTINO"));
                            obj.ID_CUENTA = dr.GetDecimal(dr.GetOrdinal("BACC_NUMBER"));
                            obj.CTA_DESTINO = dr.GetString(dr.GetOrdinal("CTA_DESTINO"));
                            obj.FECHA_DEPOSITO = dr.GetDateTime(dr.GetOrdinal("REC_DATEDEPOSITE"));
                            obj.MONTO = dr.GetDecimal(dr.GetOrdinal("MONTO"));
                            obj.NRO_CONFIRMACION = dr.GetString(dr.GetOrdinal("NRO_CONFIRMACION"));
                            obj.FEC_CREA = dr.GetDateTime(dr.GetOrdinal("FEC_CREA"));
                            obj.ESTADO = dr.GetInt32(dr.GetOrdinal("REC_STATUS_INTERFAZ"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                                obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                            if (!dr.IsDBNull(dr.GetOrdinal("FEC_INTERFAZ")))
                                obj.FEC_INTERFAZ = dr.GetDateTime(dr.GetOrdinal("FEC_INTERFAZ"));

                            if (!dr.IsDBNull(dr.GetOrdinal("REC_DATE_INTERFAZ_REVERT")))
                                obj.FEC_INTERFAZ_REVERT = dr.GetDateTime(dr.GetOrdinal("REC_DATE_INTERFAZ_REVERT"));

                            Lista.Add(obj);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Lista;
        }

        public List<BEDocumentoNoIdentificado> ListarDNI_EXCEL(decimal bnk_id, string fecha_ini, string fecha_fin, int estado)
        {
            List<BEDocumentoNoIdentificado> Lista = new List<BEDocumentoNoIdentificado>();
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DNI_EXCEL"))
                {
                    oDataBase.AddInParameter(cm, "@BNK_ID", DbType.Decimal, bnk_id);
                    oDataBase.AddInParameter(cm, "@FECHA_DEP_INI", DbType.String, fecha_ini);
                    oDataBase.AddInParameter(cm, "@FECHA_DEP_FIN", DbType.String, fecha_fin);
                    oDataBase.AddInParameter(cm, "@ESTADO", DbType.Int32, estado);
                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        BEDocumentoNoIdentificado obj = null;
                        while (dr.Read())
                        {
                            obj = new BEDocumentoNoIdentificado();
                            obj.REC_PID = dr.GetDecimal(dr.GetOrdinal("REC_PID"));
                            obj.REC_PWID = dr.GetString(dr.GetOrdinal("REC_PWID"));
                            obj.TipoPago = dr.GetString(dr.GetOrdinal("TIPO_PAGO"));
                            obj.ID_BANCO = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                            obj.BANCO_DESTINO = dr.GetString(dr.GetOrdinal("BANCO_DESTINO"));
                            obj.ID_CUENTA = dr.GetDecimal(dr.GetOrdinal("BACC_NUMBER"));
                            obj.CTA_DESTINO = dr.GetString(dr.GetOrdinal("CTA_DESTINO"));
                            obj.FECHA_DEPOSITO = dr.GetDateTime(dr.GetOrdinal("FECHA_DEPOSITO"));
                            obj.MONTO = dr.GetDecimal(dr.GetOrdinal("MONTO"));
                            obj.NRO_CONFIRMACION = dr.GetString(dr.GetOrdinal("NRO_CONFIRMACION"));
                            obj.FEC_CREA = dr.GetDateTime(dr.GetOrdinal("FEC_CREA"));
                            obj.ESTADO = dr.GetInt32(dr.GetOrdinal("ESTADO"));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                                obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                            if (!dr.IsDBNull(dr.GetOrdinal("FEC_INTERFAZ")))
                                obj.FEC_INTERFAZ = dr.GetDateTime(dr.GetOrdinal("FEC_INTERFAZ"));

                            if (!dr.IsDBNull(dr.GetOrdinal("FEC_INTERFAZ_REVERT")))
                                obj.FEC_INTERFAZ_REVERT = dr.GetDateTime(dr.GetOrdinal("FEC_INTERFAZ_REVERT"));

                            if (!dr.IsDBNull(dr.GetOrdinal("MREC_ID2")))
                                obj.MREC_ID2 = dr.GetDecimal(dr.GetOrdinal("MREC_ID2"));
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                                obj.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("Factura")))
                                obj.Factura = dr.GetString(dr.GetOrdinal("Factura"));



                            Lista.Add(obj);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw ;
            }
            return Lista;
        }

        #endregion

        #region ELIMINAR
        public int EliminarDNI(decimal id)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_DNI_ELIMINAR");
                oDataBase.AddInParameter(oDbComand, "@REC_PID", DbType.Decimal, id);
                int result = Convert.ToInt32(oDataBase.ExecuteScalar(oDbComand));
                retorno = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }
        #endregion

        public int Validar_DNI(BEDocumentoNoIdentificado obj)
        {
            int retorno = 0;
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_VOUCHER_VALIDAR_DNI");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, obj.OWNER);
                oDataBase.AddInParameter(oDbComand, "@BNK_ID", DbType.Decimal, obj.ID_BANCO);
                oDataBase.AddInParameter(oDbComand, "@NRO_CTA", DbType.Decimal, obj.ID_CUENTA);
                oDataBase.AddInParameter(oDbComand, "@FEC_DEPOSITO", DbType.DateTime, obj.FECHA_DEPOSITO);
                oDataBase.AddInParameter(oDbComand, "@VOUCHER", DbType.String, obj.NRO_CONFIRMACION);
                oDataBase.AddInParameter(oDbComand, "@MONEDA", DbType.String, obj.ID_MONEDA);
                oDataBase.AddInParameter(oDbComand, "@MONTO", DbType.Decimal, obj.MONTO);

                int id = Convert.ToInt32(oDataBase.ExecuteScalar(oDbComand));
                retorno = id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

    }
}
