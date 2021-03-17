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
    public class DAEmisionMensual
    {

        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEEmisionMensual> ListaEmisionMensual(decimal Oficina, int Mes, int Anio, int Estado, decimal CodigoLicencia, decimal CodigoSocio)
        {
            List<BEEmisionMensual> lista = new List<BEEmisionMensual>();
            BEEmisionMensual entidad = null;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_EMISION_OFICINA");
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, Oficina);
                db.AddInParameter(oDbCommand, "@MES", DbType.Decimal, Mes);
                db.AddInParameter(oDbCommand, "@ANIO", DbType.Decimal, Anio);
                db.AddInParameter(oDbCommand, "@ESTADO", DbType.Decimal, Estado);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, CodigoLicencia);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, CodigoSocio);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEEmisionMensual();

                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            entidad.CodigoSocio = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));


                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.DescripcionSocio = dr.GetString(dr.GetOrdinal("SOCIO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("RUC")))
                            entidad.DescripcionDocumentoSocio = dr.GetString(dr.GetOrdinal("RUC"));


                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_ALPHA")))
                            entidad.DescripcionTipoMoneda = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MONEDA")))
                            entidad.DescripcionMoneda = dr.GetString(dr.GetOrdinal("MONEDA"));


                        if (!dr.IsDBNull(dr.GetOrdinal("GRUPO_FACTURACION")))
                            entidad.DescripcionGrupoFacturacion = dr.GetString(dr.GetOrdinal("GRUPO_FACTURACION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INVG_ID")))
                            entidad.CodigoGrupoFacturacion = dr.GetDecimal(dr.GetOrdinal("INVG_ID"));

                        //if (!dr.IsDBNull(dr.GetOrdinal("LIC_EMI_MEN")))
                        //    entidad.CodigoPermiteFacturacion = dr.GetString(dr.GetOrdinal("LIC_EMI_MEN"));

                        if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                            entidad.DescripcionDireccionSocio = dr.GetString(dr.GetOrdinal("DIRECCION"));


                        if (!dr.IsDBNull(dr.GetOrdinal("BRUTO")))
                            entidad.MontoBrutoTotalSocioGrupo = dr.GetDecimal(dr.GetOrdinal("BRUTO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("DSCTO")))
                            entidad.MontoDesctoTotalSocioGrupo = dr.GetDecimal(dr.GetOrdinal("DSCTO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NETO")))
                            entidad.MontoNetoTotalSocioGrupo = dr.GetDecimal(dr.GetOrdinal("NETO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("OFF_ID")))
                            entidad.CodigoOficina = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO_LICENCIAS")))
                            entidad.CantidadLicenciasSocioGrupo = dr.GetInt32(dr.GetOrdinal("NUMERO_LICENCIAS"));

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

        public List<BEEmisionMensual> ListarLicenciasEmisionMensual(decimal CodigoSocio, decimal CodigoGrupoFacturacion, decimal CodigoOficina, int Mes, int Anio)
        {
            List<BEEmisionMensual> lista = new List<BEEmisionMensual>();
            BEEmisionMensual entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_LICENCIAS_SOCIOS_GRUPO");
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, CodigoSocio);
                db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, CodigoGrupoFacturacion);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, CodigoOficina);
                db.AddInParameter(oDbCommand, "@Mes", DbType.Decimal, Mes);
                db.AddInParameter(oDbCommand, "@Anio", DbType.Decimal, Anio);
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEEmisionMensual();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.CodigoLicencia = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            entidad.DescripcionEstablecimiento = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                            entidad.DescripcionDireccionEstablecimiento = dr.GetString(dr.GetOrdinal("ADDRESS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                            entidad.DescripcionUbigeo = dr.GetString(dr.GetOrdinal("UBIGEO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_LIRICS_BRUTO")))
                            entidad.MontoLicenciaBruto = dr.GetDecimal(dr.GetOrdinal("MONTO_LIRICS_BRUTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_LIRICS_DCTO")))
                            entidad.MontoLicenciaDscto = dr.GetDecimal(dr.GetOrdinal("MONTO_LIRICS_DCTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_LIRICS_NETO")))
                            entidad.MontoLicenciaNeto = dr.GetDecimal(dr.GetOrdinal("MONTO_LIRICS_NETO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_EMI_MEN")))
                            entidad.CodigoPermiteFacturacion = dr.GetString(dr.GetOrdinal("LIC_EMI_MEN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFF_ID")))
                            entidad.CodigoOficina = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            entidad.CodigoSocio = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVG_ID")))
                            entidad.CodigoGrupoFacturacion = dr.GetDecimal(dr.GetOrdinal("INVG_ID"));

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


        public List<BEEmisionMensual> ListarLicenciasPeriodos(decimal CodigoLicencia, int Mes, int Anio)
        {
            List<BEEmisionMensual> lista = new List<BEEmisionMensual>();
            BEEmisionMensual entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_PLANEAMIENTO_LICENCIA_EMISION");
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, CodigoLicencia);
                db.AddInParameter(oDbCommand, "@LIC_MONTH", DbType.Int32, Mes);
                db.AddInParameter(oDbCommand, "@LIC_YEAR", DbType.Int32, Anio);
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEEmisionMensual();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.CodigoLicencia = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                            entidad.DescripcionPeriodo = dr.GetString(dr.GetOrdinal("PERIODO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_LIRICS_BRUTO")))
                            entidad.MontoLicenciaBruto = dr.GetDecimal(dr.GetOrdinal("MONTO_LIRICS_BRUTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_LIRICS_DCTO")))
                            entidad.MontoLicenciaDscto = dr.GetDecimal(dr.GetOrdinal("MONTO_LIRICS_DCTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_LIRICS_NETO")))
                            entidad.MontoLicenciaNeto = dr.GetDecimal(dr.GetOrdinal("MONTO_LIRICS_NETO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_STATUS")))
                            entidad.EstadoPeriodo = dr.GetString(dr.GetOrdinal("LIC_PL_STATUS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                            entidad.CodigoPeriodo = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));

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


        public List<BEEmisionMensual> ListarLicenciasPeriodosActualizar(decimal CodigoLicencia, int Mes, int Anio, decimal Oficina)
        {
            List<BEEmisionMensual> lista = new List<BEEmisionMensual>();
            BEEmisionMensual entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTA_LICENCIAS_PERIODO");
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, CodigoLicencia);
                db.AddInParameter(oDbCommand, "@LIC_MONTH", DbType.Int32, Mes);
                db.AddInParameter(oDbCommand, "@LIC_YEAR", DbType.Int32, Anio);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Int32, Oficina);
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEEmisionMensual();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.CodigoLicencia = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                            entidad.CodigoPeriodo = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));

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

        public int GenerarEmisionMensual(decimal Oficina, int mes, int anio)
        {
            var Respuesta = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_FACTURAR_EMISION_MENSUAL");
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, Oficina);
                db.AddInParameter(oDbCommand, "@MES", DbType.Int32, mes);
                db.AddInParameter(oDbCommand, "@ANIO", DbType.Int32, anio);
                oDbCommand.CommandTimeout = 60;
                Respuesta = db.ExecuteNonQuery(oDbCommand);

            }
            catch (Exception ex)
            {
                return 0;
            }
            return Respuesta;
        }

        public int ActualizarEstadoLicenciaEmision(decimal CodigoLicencia)
        {
            var Respuesta = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_LICENCIA_EMISION_MENSUAL");
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, CodigoLicencia);

                Respuesta = db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return Respuesta;
        }

        public int RecuperaQueModuloUtilizar()
        {
            var Respuesta = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_FACTURAR_MOD_ANTERIOR");
                Respuesta = Convert.ToInt32(db.ExecuteScalar(oDbCommand));

            }
            catch (Exception ex)
            {
                return 0;
            }
            return Respuesta;
        }

    }
}
