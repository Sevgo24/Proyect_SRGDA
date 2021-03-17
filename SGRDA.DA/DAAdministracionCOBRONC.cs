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
    public class DAAdministracionCOBRONC
    {

        private Database db = DatabaseFactory.CreateDatabase("conexion");


        public List<BEFactura> ListarCOBRONC(decimal CodigoNC, decimal COdigoSerie, int NUmeroDocumento, int CONFECHA, DateTime FechaEmision, decimal CodigoOficina,int Tipo)
        {
            List<BEFactura> lista = new List<BEFactura>();
            BEFactura entidad = null;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTA_NC_COBROS");

            db.AddInParameter(oDbCommand, "@INV_ID_NC", DbType.Decimal, CodigoNC);
            db.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, COdigoSerie);
            db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Int32, NUmeroDocumento);
            db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, CONFECHA);
            db.AddInParameter(oDbCommand, "@INV_DATE", DbType.DateTime, FechaEmision);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, CodigoOficina);
            db.AddInParameter(oDbCommand, "@TIPO", DbType.Int32, Tipo);


            try
            {
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEFactura();
                        if (!dr.IsDBNull(dr.GetOrdinal("CodigoNc")))
                            entidad.INV_ID = dr.GetDecimal(dr.GetOrdinal("CodigoNc"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Serie")))
                            entidad.NMR_SERIAL = dr.GetString(dr.GetOrdinal("Serie"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Numero")))
                            entidad.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("Numero"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaEmision")))
                            entidad.INV_DATE = dr.GetDateTime(dr.GetOrdinal("FechaEmision"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Monto")))
                            entidad.INV_NET = dr.GetDecimal(dr.GetOrdinal("Monto"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Oficina")))
                            entidad.NombreOficina = dr.GetString(dr.GetOrdinal("Oficina"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Socio")))
                            entidad.SOCIO = dr.GetString(dr.GetOrdinal("Socio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CodigoSocio")))
                            entidad.BPS_ID = dr.GetDecimal(dr.GetOrdinal("CodigoSocio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DocumentoOriginal")))
                            entidad.INV_CN_REF = dr.GetDecimal(dr.GetOrdinal("DocumentoOriginal"));
                        

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


        public List<BEFactura> ListarDetalleCOBRONC(decimal CodigoNC)
        {
            List<BEFactura> lista = new List<BEFactura>();
            BEFactura entidad = null;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_DOCMENTOS_COBRO_NC");

            db.AddInParameter(oDbCommand, "@INV_ID_NC", DbType.Decimal, CodigoNC);


            try
            {
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEFactura();
                        if (!dr.IsDBNull(dr.GetOrdinal("Codigo")))
                            entidad.INV_ID = dr.GetDecimal(dr.GetOrdinal("Codigo"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Serie")))
                            entidad.NMR_SERIAL = dr.GetString(dr.GetOrdinal("Serie"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Numero")))
                            entidad.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("Numero"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaEmision")))
                            entidad.INV_DATE = dr.GetDateTime(dr.GetOrdinal("FechaEmision"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Monto")))
                            entidad.INV_NET = dr.GetDecimal(dr.GetOrdinal("Monto"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Oficina")))
                            entidad.NombreOficina = dr.GetString(dr.GetOrdinal("Oficina"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Socio")))
                            entidad.SOCIO = dr.GetString(dr.GetOrdinal("Socio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CodigoSocio")))
                            entidad.BPS_ID = dr.GetDecimal(dr.GetOrdinal("CodigoSocio"));

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


        public List<BEFactura> ListarDetalleCOBRONCFactSeleccionada(decimal CodigoDocumento)
        {
            List<BEFactura> lista = new List<BEFactura>();
            BEFactura entidad = null;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_DOCUMENTO_SELECCIONADO_NC_COBRO");

            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, CodigoDocumento);


            try
            {
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEFactura();
                        if (!dr.IsDBNull(dr.GetOrdinal("Codigo")))
                            entidad.INV_ID = dr.GetDecimal(dr.GetOrdinal("Codigo"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Serie")))
                            entidad.NMR_SERIAL = dr.GetString(dr.GetOrdinal("Serie"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Numero")))
                            entidad.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("Numero"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaEmision")))
                            entidad.INV_DATE = dr.GetDateTime(dr.GetOrdinal("FechaEmision"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Monto")))
                            entidad.INV_NET = dr.GetDecimal(dr.GetOrdinal("Monto"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Oficina")))
                            entidad.NombreOficina = dr.GetString(dr.GetOrdinal("Oficina"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Socio")))
                            entidad.SOCIO = dr.GetString(dr.GetOrdinal("Socio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CodigoSocio")))
                            entidad.BPS_ID = dr.GetDecimal(dr.GetOrdinal("CodigoSocio"));

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


        public bool InsertarBECNC(decimal CodigoDocumento,decimal CodigoDocumento1,decimal CodigoDocumento2,decimal MontoAplicar,string Usuario)
        {
            bool exito = false;

            try
            {
                using (DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_COBRO_TRANSITORIO"))
                {
                    db.AddInParameter(oDbCommand, "@NC", DbType.Decimal, CodigoDocumento);
                    db.AddInParameter(oDbCommand, "@F1", DbType.Decimal, CodigoDocumento1);
                    db.AddInParameter(oDbCommand, "@F2", DbType.Decimal, CodigoDocumento2);
                    db.AddInParameter(oDbCommand, "@MONTO_APLICAR", DbType.Decimal, MontoAplicar);
                    db.AddInParameter(oDbCommand, "@USUARIO_CREA", DbType.String, Usuario);
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
