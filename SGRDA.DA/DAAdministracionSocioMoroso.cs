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
    public class DAAdministracionSocioMoroso
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");


        public List<BEAdministracionSocioMoroso>Listar(decimal CodigoSocio , int ConFecha , string FechaInicial, string FechaFinal,int Tipo,int Estado)
        {
            List<BEAdministracionSocioMoroso> lista = new List<BEAdministracionSocioMoroso>();
            BEAdministracionSocioMoroso entidad = null;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_USUARIOS_MOROSOS");
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, CodigoSocio);
            db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, ConFecha);
            db.AddInParameter(oDbCommand, "@FECHA_INI", DbType.String, FechaInicial);
            db.AddInParameter(oDbCommand, "@FECHA_FIN", DbType.String, FechaFinal);
            db.AddInParameter(oDbCommand, "@TIPO", DbType.String, Tipo);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.String, Estado);

            try
            {
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionSocioMoroso();
                        if (!dr.IsDBNull(dr.GetOrdinal("CodigoSocioMoroso")))
                            entidad.CodigoSocioMoroso = dr.GetDecimal(dr.GetOrdinal("CodigoSocioMoroso"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CodigoSocio")))
                            entidad.CodigoSocio = dr.GetDecimal(dr.GetOrdinal("CodigoSocio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Descripcion")))
                            entidad.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DescripcionEstado")))
                            entidad.DescripcionEstado = dr.GetString(dr.GetOrdinal("DescripcionEstado"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Estado")))
                            entidad.DescripcionEst = dr.GetString(dr.GetOrdinal("Estado"));
                        if (!dr.IsDBNull(dr.GetOrdinal("UsuarioCreacion")))
                            entidad.UsuarioCreacion = dr.GetString(dr.GetOrdinal("UsuarioCreacion"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaCreacion")))
                            entidad.FechaCreacion = dr.GetString(dr.GetOrdinal("FechaCreacion"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Socio")))
                            entidad.Socio = dr.GetString(dr.GetOrdinal("Socio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TipoSocio")))
                            entidad.TipoSocio = dr.GetString(dr.GetOrdinal("TipoSocio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DocumentoIdentificacion")))
                            entidad.DocumentoIdentificacion = dr.GetString(dr.GetOrdinal("DocumentoIdentificacion"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RazonSocial")))
                            entidad.RazonSocial = dr.GetString(dr.GetOrdinal("RazonSocial"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EVENTO")))
                            entidad.NombreEvento = dr.GetString(dr.GetOrdinal("EVENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaEvento")))
                            entidad.FechaEvento = dr.GetString(dr.GetOrdinal("FechaEvento"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTABLECIMIENTO")))
                            entidad.Local = dr.GetString(dr.GetOrdinal("ESTABLECIMIENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                            entidad.Direccion = dr.GetString(dr.GetOrdinal("DIRECCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFICINA")))
                            entidad.NombreOficina = dr.GetString(dr.GetOrdinal("OFICINA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Representante")))
                            entidad.Representante = dr.GetString(dr.GetOrdinal("Representante"));


                        lista.Add(entidad);
                    }
                }

            }catch(Exception ex)
            {
                
            }

            return lista;
        }


        public bool InsertarUsuarioMoroso(decimal CodigoSocio, string Descripcion ,string Usuario,decimal CodigoLicencia,decimal CodigoOficina)
        {
            bool exito = false;

            try {
                using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_USUARIO_MOROSO"))
                {
                    db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, CodigoSocio);
                    db.AddInParameter(oDbCommand, "@DESCRIPCION", DbType.String, Descripcion);
                    db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, Usuario);
                    db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, CodigoLicencia);
                    db.AddInParameter(oDbCommand, "@OFI_ID", DbType.Decimal, CodigoOficina);
                    exito = db.ExecuteNonQuery(oDbCommand) > 0;
                }
            }catch(Exception ex)
            {
                throw;
            }
            return exito;
        }

        public bool InactivarUsuarioMoroso(decimal CodigoSocio,string Usuario)
        {
            bool exito = false;

            try
            {
                using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_USUARIO_MOROSO"))
                {
                    db.AddInParameter(oDbCommand, "@BPS_ID_ST", DbType.Decimal, CodigoSocio);
                    db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, Usuario);
                    exito = db.ExecuteNonQuery(oDbCommand) > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return exito;
        }

        public BEAdministracionSocioMoroso Obtener(decimal CodigoSocioT)
        {
            BEAdministracionSocioMoroso entidad = null;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTIENE_SOCIO_RENUENTE");
            db.AddInParameter(oDbCommand, "@BPS_ID_T", DbType.Decimal, CodigoSocioT);

            try
            {
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        entidad = new BEAdministracionSocioMoroso();
                        if (!dr.IsDBNull(dr.GetOrdinal("CodigoSocioMoroso")))
                            entidad.CodigoSocioMoroso = dr.GetDecimal(dr.GetOrdinal("CodigoSocioMoroso"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Socio")))
                            entidad.Socio = dr.GetString(dr.GetOrdinal("Socio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EVENTO")))
                            entidad.NombreEvento = dr.GetString(dr.GetOrdinal("EVENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Descripcion")))
                            entidad.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.Estado = dr.GetInt32(dr.GetOrdinal("ESTADO"));
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return entidad;
        }

        public bool ActualizarEstadoSocioMoroso(decimal CodigoSocio,int Estado, string Usuario)
        {
            bool exito = false;

            try
            {
                using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTUALIZAR_ESTADO_SOCIO"))
                {
                    db.AddInParameter(oDbCommand, "@BPS_IDT", DbType.Decimal, CodigoSocio);
                    db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, Estado);
                    db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, Usuario);
                    exito = db.ExecuteNonQuery(oDbCommand) > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return exito;
        }


    }
}
