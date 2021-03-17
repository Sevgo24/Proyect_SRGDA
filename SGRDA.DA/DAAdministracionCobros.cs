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
    public class DAAdministracionCobros
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEAdministracionCobros> Listar(decimal IdCobro,string NumeroOperacion,decimal Monto,decimal IdBancoDestino,decimal IdBancoOrigen,decimal IdCuenta,
                                                                decimal IdOficina,int EstadoCobro,int EstadoConfirmacion,int ConFecha,string FechaInicial,string FechaFinal,
                                                                        decimal IdSocio,decimal IdSerie,decimal NumeroDocumento)
        {
            List<BEAdministracionCobros> lista = new List<BEAdministracionCobros>();
            BEAdministracionCobros entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_COBROS_PARCIALES");
                db.AddInParameter(oDbCommand, "@COD_COBRO", DbType.Decimal, IdCobro);
                db.AddInParameter(oDbCommand, "@NUM_OPERACION", DbType.String, NumeroOperacion);
                db.AddInParameter(oDbCommand, "@MONTO", DbType.Decimal, Monto);
                //db.AddInParameter(oDbCommand, "@PENDIENTE", DbType.Decimal, 0);
                //db.AddInParameter(oDbCommand, "@MONTO_USADO", DbType.Decimal, 0);
                db.AddInParameter(oDbCommand, "@BANCO_DESTINO", DbType.Decimal, IdBancoDestino);
                db.AddInParameter(oDbCommand, "@BANCO_ORIGEN", DbType.Decimal, IdBancoOrigen);
                db.AddInParameter(oDbCommand, "@CUENTA", DbType.Decimal, IdCuenta);
                db.AddInParameter(oDbCommand, "@OFICINA", DbType.Decimal, IdOficina);
                db.AddInParameter(oDbCommand, "@ESTADO_COBRO", DbType.Int32, EstadoCobro);
                db.AddInParameter(oDbCommand, "@ESTADO_CONFIRMACION", DbType.Int32, EstadoConfirmacion);
                db.AddInParameter(oDbCommand, "@FECHA_CONFIRMACION", DbType.String, "");
                db.AddInParameter(oDbCommand, "@SOCIO", DbType.Decimal, IdSocio);
                db.AddInParameter(oDbCommand, "@SERIE", DbType.Decimal, IdSerie);
                db.AddInParameter(oDbCommand, "@NUMERO", DbType.Decimal, NumeroDocumento);
                //db.AddInParameter(oDbCommand, "@IMPORTE_DOCUMENTO", DbType.Decimal, 0);
                //db.AddInParameter(oDbCommand, "@ESTADO_DOCUMENTO", DbType.Int32, 0);
                //db.AddInParameter(oDbCommand, "@OFICINA_LICENCIA", DbType.Decimal, 0);
                db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, ConFecha);
                db.AddInParameter(oDbCommand, "@FECHA_INCIAL", DbType.String, FechaInicial);
                db.AddInParameter(oDbCommand, "@FECHA_FINAL", DbType.String, FechaFinal);


                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionCobros();

                        if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_COBRO")))
                            entidad.CodigoCobro = dr.GetDecimal(dr.GetOrdinal("CODIGO_COBRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_REC")))
                            entidad.CodigoRecCobro = dr.GetDecimal(dr.GetOrdinal("CODIGO_REC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO_OPERACION")))
                            entidad.CodigoReferencia = dr.GetString(dr.GetOrdinal("NUMERO_OPERACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_VOUCHER")))
                            entidad.MontoVoucher = dr.GetDecimal(dr.GetOrdinal("MONTO_VOUCHER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SALDO_PENDIENTE")))
                            entidad.MontoSaldoPendiente = dr.GetDecimal(dr.GetOrdinal("SALDO_PENDIENTE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RECAUDO_MONTO_USADO")))
                            entidad.MontoSaldoUsado = dr.GetDecimal(dr.GetOrdinal("RECAUDO_MONTO_USADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BANCO_DESTINO")))
                            entidad.NombreBancoDestino = dr.GetString(dr.GetOrdinal("BANCO_DESTINO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BANCO_ORIGEN")))
                            entidad.NombreBancoOrigen = dr.GetString(dr.GetOrdinal("BANCO_ORIGEN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CUENTA")))
                            entidad.DescripcionCuentaBanco = dr.GetString(dr.GetOrdinal("CUENTA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFICINA_DE_CREACION")))
                            entidad.NombreOficinaCobro = dr.GetString(dr.GetOrdinal("OFICINA_DE_CREACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_COBRO")))
                            entidad.DescripcionEstadoCobro = dr.GetString(dr.GetOrdinal("ESTADO_COBRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_COBRO_CONFIRMACION")))
                            entidad.DescripcionEstadoCobroConfirmacion = dr.GetString(dr.GetOrdinal("ESTADO_COBRO_CONFIRMACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CONFIRMACION")))
                            entidad.FechaCobroConfirmacion = dr.GetString(dr.GetOrdinal("FECHA_CONFIRMACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("COBRO_ESTADO")))
                            entidad.EstadoCobro = dr.GetInt32(dr.GetOrdinal("COBRO_ESTADO"));

        //if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
        //    entidad.NombreyApelidosSocioCobro = dr.GetString(dr.GetOrdinal("SOCIO"));
        //if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
        //    entidad.DescripcionSerie = dr.GetString(dr.GetOrdinal("SERIE"));
        //if (!dr.IsDBNull(dr.GetOrdinal("NUMERO")))
        //    entidad.NumeroFactura = dr.GetDecimal(dr.GetOrdinal("NUMERO"));
        //if (!dr.IsDBNull(dr.GetOrdinal("IMPORTE")))
        //    entidad.MontoDocumento = dr.GetDecimal(dr.GetOrdinal("IMPORTE"));
        //if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_DOCUMENTO")))
        //    entidad.EstadoDocumento = dr.GetString(dr.GetOrdinal("ESTADO_DOCUMENTO"));
        //if (!dr.IsDBNull(dr.GetOrdinal("OFICINA_DE_LICENCIA")))
        //    entidad.DescripcionOficinaLicencia = dr.GetString(dr.GetOrdinal("OFICINA_DE_LICENCIA"));

        lista.Add(entidad);
                    }
                }

            }catch(Exception ex)
            {
                return null;
            }


            return lista;
        }




        public List<BEAdministracionCobros> ListarSocioCabezeraCobros(decimal IdCobro)
        {
            List<BEAdministracionCobros> lista = new List<BEAdministracionCobros>();
            BEAdministracionCobros entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTA_SOCIOS_CAB_COBROS");
                db.AddInParameter(oDbCommand, "@COD_COBRO", DbType.Decimal, IdCobro);
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionCobros();

                        if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_COBRO")))
                            entidad.CodigoCobro = dr.GetDecimal(dr.GetOrdinal("CODIGO_COBRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_SOCIO")))
                            entidad.CodigoSocioCobro = dr.GetDecimal(dr.GetOrdinal("CODIGO_SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.NombreyApelidosSocioCobro = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTO_IDENTIFICACION")))
                            entidad.DocumentoIdentificacionSocio = dr.GetString(dr.GetOrdinal("DOCUMENTO_IDENTIFICACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CANTIDAD_DOCUMENTOS")))
                            entidad.CantidadDocumentosxSocio = dr.GetInt32(dr.GetOrdinal("CANTIDAD_DOCUMENTOS"));

                        lista.Add(entidad);
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }


            return lista;
        }

        public List<BEAdministracionCobros> ListarSocioDocumentosDetalleCobros(decimal IdCobro,decimal IdSocio)
        {
            List<BEAdministracionCobros> lista = new List<BEAdministracionCobros>();
            BEAdministracionCobros entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTA_DOCUMENTOS_SOCIOS_COBROS");
                db.AddInParameter(oDbCommand, "@COD_COBRO", DbType.Decimal, IdCobro);
                db.AddInParameter(oDbCommand, "@ID_SOCIO", DbType.Decimal, IdSocio);
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionCobros();

                        if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_COBRO")))
                            entidad.CodigoCobro = dr.GetDecimal(dr.GetOrdinal("CODIGO_COBRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_SOCIO")))
                            entidad.CodigoSocioCobro = dr.GetDecimal(dr.GetOrdinal("CODIGO_SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_DOCUMENTO")))
                            entidad.CodigoDocumento = dr.GetDecimal(dr.GetOrdinal("CODIGO_DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            entidad.DescripcionSerie = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO")))
                            entidad.NumeroFactura = dr.GetDecimal(dr.GetOrdinal("NUMERO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO")))
                            entidad.MontoDocumento = dr.GetDecimal(dr.GetOrdinal("MONTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_DOCUMENTO")))
                            entidad.EstadoDocumento = dr.GetString(dr.GetOrdinal("ESTADO_DOCUMENTO"));

                        lista.Add(entidad);
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }


            return lista;
        }

        public List<BEAdministracionCobros> ListarReporte(decimal IdCobro, string NumeroOperacion, decimal Monto, decimal IdBancoDestino, decimal IdBancoOrigen, decimal IdCuenta,
                                                        decimal IdOficina, int EstadoCobro, int EstadoConfirmacion, int ConFecha, string FechaInicial, string FechaFinal,
                                                                decimal IdSocio, decimal IdSerie, decimal NumeroDocumento)
        {
            List<BEAdministracionCobros> lista = new List<BEAdministracionCobros>();
            BEAdministracionCobros entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REP_COBROS_PARCIALES");
                db.AddInParameter(oDbCommand, "@COD_COBRO", DbType.Decimal, IdCobro);
                db.AddInParameter(oDbCommand, "@NUM_OPERACION", DbType.String, NumeroOperacion);
                db.AddInParameter(oDbCommand, "@MONTO", DbType.Decimal, Monto);
                //db.AddInParameter(oDbCommand, "@PENDIENTE", DbType.Decimal, 0);
                //db.AddInParameter(oDbCommand, "@MONTO_USADO", DbType.Decimal, 0);
                db.AddInParameter(oDbCommand, "@BANCO_DESTINO", DbType.Decimal, IdBancoDestino);
                db.AddInParameter(oDbCommand, "@BANCO_ORIGEN", DbType.Decimal, IdBancoOrigen);
                db.AddInParameter(oDbCommand, "@CUENTA", DbType.Decimal, IdCuenta);
                db.AddInParameter(oDbCommand, "@OFICINA", DbType.Decimal, IdOficina);
                db.AddInParameter(oDbCommand, "@ESTADO_COBRO", DbType.Int32, EstadoCobro);
                db.AddInParameter(oDbCommand, "@ESTADO_CONFIRMACION", DbType.Int32, EstadoConfirmacion);
                db.AddInParameter(oDbCommand, "@FECHA_CONFIRMACION", DbType.String, "");
                db.AddInParameter(oDbCommand, "@SOCIO", DbType.Decimal, IdSocio);
                db.AddInParameter(oDbCommand, "@SERIE", DbType.Decimal, IdSerie);
                db.AddInParameter(oDbCommand, "@NUMERO", DbType.Decimal, NumeroDocumento);
                //db.AddInParameter(oDbCommand, "@IMPORTE_DOCUMENTO", DbType.Decimal, 0);
                //db.AddInParameter(oDbCommand, "@ESTADO_DOCUMENTO", DbType.Int32, 0);
                //db.AddInParameter(oDbCommand, "@OFICINA_LICENCIA", DbType.Decimal, 0);
                db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, ConFecha);
                db.AddInParameter(oDbCommand, "@FECHA_INCIAL", DbType.String, FechaInicial);
                db.AddInParameter(oDbCommand, "@FECHA_FINAL", DbType.String, FechaFinal);
                oDbCommand.CommandTimeout = 60;

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionCobros();

                        if (!dr.IsDBNull(dr.GetOrdinal("CodigoCobro")))
                            entidad.CodigoCobro = dr.GetDecimal(dr.GetOrdinal("CodigoCobro"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CodigoRecCobro")))
                            entidad.CodigoRecCobro = dr.GetDecimal(dr.GetOrdinal("CodigoRecCobro"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CodigoReferencia")))
                            entidad.CodigoReferencia = dr.GetString(dr.GetOrdinal("CodigoReferencia"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MontoVoucher")))
                            entidad.MontoVoucher = dr.GetDecimal(dr.GetOrdinal("MontoVoucher"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MontoSaldoPendiente")))
                            entidad.MontoSaldoPendiente = dr.GetDecimal(dr.GetOrdinal("MontoSaldoPendiente"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MontoSaldoUsado")))
                            entidad.MontoSaldoUsado = dr.GetDecimal(dr.GetOrdinal("MontoSaldoUsado"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NombreBancoDestino")))
                            entidad.NombreBancoDestino = dr.GetString(dr.GetOrdinal("NombreBancoDestino"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NombreBancoOrigen")))
                            entidad.NombreBancoOrigen = dr.GetString(dr.GetOrdinal("NombreBancoOrigen"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DescripcionCuentaBanco")))
                            entidad.DescripcionCuentaBanco = dr.GetString(dr.GetOrdinal("DescripcionCuentaBanco"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NombreOficinaCobro")))
                            entidad.NombreOficinaCobro = dr.GetString(dr.GetOrdinal("NombreOficinaCobro"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DescripcionEstadoCobro")))
                            entidad.DescripcionEstadoCobro = dr.GetString(dr.GetOrdinal("DescripcionEstadoCobro"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DescripcionEstadoCobroConfirmacion")))
                            entidad.DescripcionEstadoCobroConfirmacion = dr.GetString(dr.GetOrdinal("DescripcionEstadoCobroConfirmacion"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaCobroConfirmacion")))
                            entidad.FechaCobroConfirmacion = dr.GetString(dr.GetOrdinal("FechaCobroConfirmacion"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NombreyApelidosSocioCobro")))
                            entidad.NombreyApelidosSocioCobro = dr.GetString(dr.GetOrdinal("NombreyApelidosSocioCobro"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DescripcionSerie")))
                            entidad.DescripcionSerie = dr.GetString(dr.GetOrdinal("DescripcionSerie"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NumeroFactura")))
                            entidad.NumeroFactura = dr.GetDecimal(dr.GetOrdinal("NumeroFactura"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MontoDocumento")))
                            entidad.MontoDocumento = dr.GetDecimal(dr.GetOrdinal("MontoDocumento"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EstadoDocumento")))
                            entidad.EstadoDocumento = dr.GetString(dr.GetOrdinal("EstadoDocumento"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("OFICINA_DE_LICENCIA")))
                        //    entidad.DescripcionOficinaLicencia = dr.GetString(dr.GetOrdinal("OFICINA_DE_LICENCIA"));

                        lista.Add(entidad);
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }


            return lista;
        }

        public int ActualizaEstadoCobro(decimal IdCobro,decimal IdRecCobro)
        {
            int res = 0;
            try
            {


                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_COBRO_ESTADO");
                db.AddInParameter(oDbCommand, "@ID_COBRO", DbType.Decimal, IdCobro);
                db.AddInParameter(oDbCommand, "@REC_ID", DbType.Decimal, IdRecCobro);
                res = db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                res = 2; // error
            }

            return res;

        }
    }
}
