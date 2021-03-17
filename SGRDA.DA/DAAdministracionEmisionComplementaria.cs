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
    public class DAAdministracionEmisionComplementaria
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEAdministracionEmisionComplementaria> Listar(decimal COdigoEmision, decimal CodigoLicencia, decimal CodigoOficina, int Estado, int ConFecha, string FechaInicial, string FechaFinal)
        {
            List<BEAdministracionEmisionComplementaria> lista = new List<BEAdministracionEmisionComplementaria>();
            BEAdministracionEmisionComplementaria entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_EMISION_COMPLEMENTARIA_CAB");
                db.AddInParameter(oDbCommand, "@PROC_ID_EMI_CAB", DbType.Decimal, COdigoEmision);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, CodigoLicencia);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, CodigoOficina);
                db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, Estado);
                db.AddInParameter(oDbCommand, "@CONFECHA", DbType.Decimal, ConFecha);
                db.AddInParameter(oDbCommand, "@FECINICIO", DbType.String, FechaInicial);
                db.AddInParameter(oDbCommand, "@FECFIN", DbType.String, FechaFinal);
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionEmisionComplementaria();

                        if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_PROC_EMI_CAB")))
                            entidad.CodigoEmisionComplementaria = dr.GetDecimal(dr.GetOrdinal("CODIGO_PROC_EMI_CAB"));

                        if (!dr.IsDBNull(dr.GetOrdinal("PROC_NAME")))
                            entidad.NombreEmisionComplementaria = dr.GetString(dr.GetOrdinal("PROC_NAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("PROC_LAUNCH")))
                            entidad.FechaProcesado = dr.GetString(dr.GetOrdinal("PROC_LAUNCH"));

                        if (!dr.IsDBNull(dr.GetOrdinal("DESCRIP_ESTADO")))
                            entidad.DescripcionEstado = dr.GetString(dr.GetOrdinal("DESCRIP_ESTADO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.Estado = dr.GetInt32(dr.GetOrdinal("ESTADO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("Oficina")))
                            entidad.NombreOficina = dr.GetString(dr.GetOrdinal("Oficina"));

                        if (!dr.IsDBNull(dr.GetOrdinal("CANTIDAD_PERIODOS")))
                            entidad.CantidadPeriodos = dr.GetInt32(dr.GetOrdinal("CANTIDAD_PERIODOS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_DE_BAJA")))
                            entidad.Ends = dr.GetDateTime(dr.GetOrdinal("FECHA_DE_BAJA"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CREACION")))
                            entidad.FechaCreacion = dr.GetDateTime(dr.GetOrdinal("FECHA_CREACION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("USUARIO_DE_CREACION")))
                            entidad.UsuarioCreacion = dr.GetString(dr.GetOrdinal("USUARIO_DE_CREACION"));


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

        public List<BEAdministracionEmisionComplementariaDetalle> ListarDetalle(decimal COdigoEmision)
        {
            List<BEAdministracionEmisionComplementariaDetalle> lista = new List<BEAdministracionEmisionComplementariaDetalle>();
            BEAdministracionEmisionComplementariaDetalle entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_EMISION_COMPLEMENTARIA_DET");
                db.AddInParameter(oDbCommand, "@PROC_ID_EMI_CAB", DbType.Decimal, COdigoEmision);
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionEmisionComplementariaDetalle();

                        if (!dr.IsDBNull(dr.GetOrdinal("PROC_ID_EMI_DET")))
                            entidad.CodigoEmisionComplementariaDet = dr.GetDecimal(dr.GetOrdinal("PROC_ID_EMI_DET"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.CodigoLicencia = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.Socio = dr.GetString(dr.GetOrdinal("SOCIO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            entidad.Documento = dr.GetString(dr.GetOrdinal("TAX_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                            entidad.periodo = dr.GetString(dr.GetOrdinal("PERIODO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_LIRICS_NETO")))
                            entidad.MontoPeriodo = dr.GetDecimal(dr.GetOrdinal("MONTO_LIRICS_NETO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_DET")))
                            entidad.EstadoDet = dr.GetInt32(dr.GetOrdinal("ESTADO_DET"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            entidad.EndsDet = dr.GetInt32(dr.GetOrdinal("ENDS"));

                        if (!dr.IsDBNull(dr.GetOrdinal("PROC_ID_EMI_CAB")))
                            entidad.CodigoEmisionComplementariaCab = dr.GetDecimal(dr.GetOrdinal("PROC_ID_EMI_CAB"));

                        if(!dr.IsDBNull(dr.GetOrdinal("ESTADO_CAB")))
                            entidad.EstadoCab = dr.GetInt32(dr.GetOrdinal("ESTADO_CAB"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INVG_DESC")))
                            entidad.GrupoFacturacion = dr.GetString(dr.GetOrdinal("INVG_DESC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("OBS_ESTADO")))
                            entidad.EstadoObsDetalle = dr.GetInt32(dr.GetOrdinal("OBS_ESTADO"));

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

        public int ActualizaEstadoDetallEmisionComplementaria(decimal IdEmisionComplementariaDet)
        {
            int res = 0;
            try
            {


                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_ACTUALIZAR_ESTADO_DET");
                db.AddInParameter(oDbCommand, "@PROC_ID_EMI_DET", DbType.Decimal, IdEmisionComplementariaDet);
                res = db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                res = 0;
            }

            return res;

        }

        public decimal InsertaActualizaCabEmiComplementaria(BEAdministracionEmisionComplementaria entidad)
        {
            decimal res = 0;
            try
            {


                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_INSERTAR_CAB_EMI_COMPLEMENTARIA");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@CAB_ID", DbType.Decimal, entidad.CodigoEmisionComplementaria);
                db.AddInParameter(oDbCommand, "@NOMBRE_PROC", DbType.String, entidad.NombreEmisionComplementaria);
                db.AddInParameter(oDbCommand, "@DESCRIPCION", DbType.String, entidad.RespuestaEmisionComplementaria);
                db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, entidad.UsuarioCreacion);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, entidad.CodigoOficina);

                
                res = Convert.ToDecimal( db.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                res = 0;
            }

            return res;

        }


        public List<BEAdministracionEmisionComplementariaDetalle> ListarConsultaLicenciaDetalle(decimal codLicencia,decimal CodigoSocio,int mes,int anio,decimal CodigoOficina,decimal codcab)
        {
            List<BEAdministracionEmisionComplementariaDetalle> lista = new List<BEAdministracionEmisionComplementariaDetalle>();
            BEAdministracionEmisionComplementariaDetalle entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTA_CONSULTA_LICENCIAS_COMP");
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, codLicencia);
                db.AddInParameter(oDbCommand, "@ANIO", DbType.Decimal, anio);
                db.AddInParameter(oDbCommand, "@MES", DbType.Decimal, mes);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, CodigoSocio);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, CodigoOficina);
                db.AddInParameter(oDbCommand, "@CAB_ID", DbType.Decimal, codcab);


                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionEmisionComplementariaDetalle();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.CodigoLicencia = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                            entidad.CodigoPeriodo = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ANIO")))
                            entidad.anioperiodo = dr.GetInt32(dr.GetOrdinal("ANIO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MES")))
                            entidad.mesperiodo = dr.GetString(dr.GetOrdinal("MES"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NETO")))
                            entidad.MontoPeriodo = dr.GetDecimal(dr.GetOrdinal("NETO"));
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

        public List<BEAdministracionEmisionComplementariaDetalle> ListarLicenciarRegistradaDetalle(decimal codCab)
        {
            List<BEAdministracionEmisionComplementariaDetalle> lista = new List<BEAdministracionEmisionComplementariaDetalle>();
            BEAdministracionEmisionComplementariaDetalle entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_LICENCIAS_REGISTRADAS_dET_COMP");
                db.AddInParameter(oDbCommand, "@ID_cAB", DbType.Decimal, codCab);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionEmisionComplementariaDetalle();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.CodigoLicencia = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                            entidad.CodigoPeriodo = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ANIO")))
                            entidad.anioperiodo = dr.GetInt32(dr.GetOrdinal("ANIO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MES")))
                            entidad.mesperiodo = dr.GetString(dr.GetOrdinal("MES"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NETO")))
                            entidad.MontoPeriodo = dr.GetDecimal(dr.GetOrdinal("NETO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("PROC_ID_EMI_DET")))
                            entidad.CodigoEmisionComplementariaDet = dr.GetDecimal(dr.GetOrdinal("PROC_ID_EMI_DET"));

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


        public int InsertarLicenciaPlaneamientoDetalle(BEAdministracionEmisionComplementariaDetalle entidad)
        {
            int res = 0;
            try
            {


                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_DETALLE_LIC_PER_COMP");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@PROC_ID_EMI_CAB", DbType.Decimal, entidad.CodigoEmisionComplementariaCab);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, entidad.CodigoLicencia);
                db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, entidad.CodigoPeriodo);
                db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, entidad.UsuarioCreacionCompDet);

                res = db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                res = 0;
            }

            return res;

        }

        public int QuitarLicenciaPlaneamientoDetalle(BEAdministracionEmisionComplementariaDetalle entidad)
        {
            int res = 0;
            try
            {


                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_QUITAR_LICENCIA_PL_COMP");
                db.AddInParameter(oDbCommand, "@PROC_ID_EMI_DET", DbType.Decimal, entidad.CodigoEmisionComplementariaDet);
                db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, entidad.UsuarioCreacionCompDet);

                res = db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                res = 0;
            }

            return res;

        }

        public int ActualizaDefinitivaCabDetComplementario( decimal CodCab,string usuario)
        {
            int res = 0;
            try
            {


                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_GRABA_FINAL_EMI_COMP");
                db.AddInParameter(oDbCommand, "@PROC_ID_EMI_CAB", DbType.Decimal, CodCab);
                db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, usuario);

                res = db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                res = 0;
            }

            return res;

        }

        public int GenerarEmisionComplementaria(decimal CodCab, string usuario)
        {
            var Respuesta = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_EMISION_COMPLEMENTARIA");
                db.AddInParameter(oDbCommand, "@CAB_ID", DbType.Decimal, CodCab);
                db.AddInParameter(oDbCommand, "@USU", DbType.String, usuario);
                oDbCommand.CommandTimeout = 60;
                Respuesta = db.ExecuteNonQuery(oDbCommand);

            }
            catch (Exception ex)
            {
                return 0;
            }
            return Respuesta;
        }

        public int RechazaSolicitudEmisionComplementaria(decimal CodCab, string usuario)
        {
            int res = 0;
            try
            {


                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_RECHAZA_SOLICITUD_EMISION_COMPLEMENTARIA");
                db.AddInParameter(oDbCommand, "@PROC_ID_EMI_CAB", DbType.Decimal, CodCab);
                db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, usuario);

                res = db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                res = 0;
            }

            return res;

        }

        public List<BEFactura> ListaDocumentoGeneradoxEmiComplementaria(decimal CodCab)
        {
            List<BEFactura> lista = new List<BEFactura>();
            BEFactura entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_DOCUMENTOS_X_EMI_COMPLE");
                db.AddInParameter(oDbCommand, "@PROC_ID_EMI_CAB", DbType.Decimal, CodCab);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEFactura();


                        if (!dr.IsDBNull(dr.GetOrdinal("CODDOCUMENTO")))
                            entidad.INV_ID = dr.GetDecimal(dr.GetOrdinal("CODDOCUMENTO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO")))
                            entidad.TIPO_EMI_DOC = dr.GetString(dr.GetOrdinal("TIPO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            entidad.NMR_SERIAL = dr.GetString(dr.GetOrdinal("SERIE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO")))
                            entidad.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("NUMERO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("IDENT")))
                            entidad.INVT_DESC = dr.GetString(dr.GetOrdinal("IDENT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTO")))
                            entidad.TAX_ID = dr.GetString(dr.GetOrdinal("DOCUMENTO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_EMISION")))
                            entidad.INV_DATE = dr.GetDateTime(dr.GetOrdinal("FECHA_EMISION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NETO")))
                            entidad.INV_NET = dr.GetDecimal(dr.GetOrdinal("NETO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.estadoFactura = dr.GetString(dr.GetOrdinal("ESTADO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SUNAT")))
                            entidad.ESTADO_SUNAT = dr.GetString(dr.GetOrdinal("ESTADO_SUNAT"));


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


        public BEAdministracionEmisionComplementaria ObtenerEmisionComplementaria(decimal CodCab)
        {
            BEAdministracionEmisionComplementaria entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_EMI_COMPLEMENTARIA");
                db.AddInParameter(oDbCommand, "@PROC_ID_EMI_CAB", DbType.Decimal, CodCab);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        entidad = new BEAdministracionEmisionComplementaria();


                        if (!dr.IsDBNull(dr.GetOrdinal("PROC_ID_EMI_CAB")))
                            entidad.CodigoEmisionComplementaria = dr.GetDecimal(dr.GetOrdinal("PROC_ID_EMI_CAB"));

                        if (!dr.IsDBNull(dr.GetOrdinal("PROC_NAME")))
                            entidad.NombreEmisionComplementaria = dr.GetString(dr.GetOrdinal("PROC_NAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            entidad.UsuarioCreacion = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_cREAT")))
                            entidad.FechaCreacion = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_cREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ANSWER")))
                            entidad.RespuestaEmisionComplementaria = dr.GetString(dr.GetOrdinal("ANSWER"));

                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }


            return entidad;
        }

        
    }

}

