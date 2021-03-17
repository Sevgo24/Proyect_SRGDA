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

    public class DAAdministracionCancelacionDirecta
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEAdministracionCancelacionDirecta> ListarSocioCabezeraCobros(decimal CodigoDocumento,decimal CodigoSerie,decimal NumeroDocumento,decimal CodigoSocio,
            decimal Oficina,int ConFecha,DateTime FechaInicio,DateTime FechaFin)
        {
            List<BEAdministracionCancelacionDirecta> lista = new List<BEAdministracionCancelacionDirecta>();
            BEAdministracionCancelacionDirecta entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CANCELACIONES_DIRECTAS");
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, CodigoDocumento);
                db.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, CodigoSerie);
                db.AddInParameter(oDbCommand, "@NUMERO", DbType.Int32, NumeroDocumento);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, CodigoSocio);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, Oficina);
                db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, ConFecha);
                db.AddInParameter(oDbCommand, "@FEC_INI", DbType.DateTime, FechaInicio);
                db.AddInParameter(oDbCommand, "@FEC_FIN", DbType.DateTime, FechaFin);
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionCancelacionDirecta();

                        if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                            entidad.ID = dr.GetDecimal(dr.GetOrdinal("ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            entidad.FechaCreacion = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("VDESC")))
                            entidad.NombreTipoCancelacion = dr.GetString(dr.GetOrdinal("VDESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID_CANCELAR")))
                            entidad.CodigoDocumento = dr.GetDecimal(dr.GetOrdinal("INV_ID_CANCELAR"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                            entidad.Serie = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                            entidad.NumeroDoc = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_DATE")))
                            entidad.FechaEmision = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.NombreSocio = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                            entidad.DescripcionTipoMoneda = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFF_NAME")))
                            entidad.NombreOficina = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MEMO_NRO")))
                            entidad.Memo = dr.GetString(dr.GetOrdinal("MEMO_NRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOC_REFERENCIAS")))
                            entidad.Referencia = dr.GetString(dr.GetOrdinal("DOC_REFERENCIAS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOC_REFERENCIAS")))
                            entidad.Referencia = dr.GetString(dr.GetOrdinal("DOC_REFERENCIAS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                            entidad.DescripcionTipoDocumento = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                            entidad.NetoDocumento = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET_APPLICATION")))
                            entidad.NetoAplicar = dr.GetDecimal(dr.GetOrdinal("INV_NET_APPLICATION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("OficinaResponsable")))
                            entidad.NombreOficinaResponsable = dr.GetString(dr.GetOrdinal("OficinaResponsable"));

                        if (!dr.IsDBNull(dr.GetOrdinal("Procedencia")))
                            entidad.Procedencia = dr.GetString(dr.GetOrdinal("Procedencia"));

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

        public List<BEAdministracionCancelacionDirecta> ListarTipoCancelacionDirecta()
        {
            List<BEAdministracionCancelacionDirecta> lista = new List<BEAdministracionCancelacionDirecta>();
            BEAdministracionCancelacionDirecta entidad = null;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_TIPOS_CANCELACION_DIRECTA");

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    entidad = new BEAdministracionCancelacionDirecta();

                    if (!dr.IsDBNull(dr.GetOrdinal("ID_TIPO_CANCELACION")))
                        entidad.TipoCancelacion = dr.GetDecimal(dr.GetOrdinal("ID_TIPO_CANCELACION"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPCION_TIPO_CANCELACION")))
                        entidad.NombreTipoCancelacion = dr.GetString(dr.GetOrdinal("DESCRIPCION_TIPO_CANCELACION"));

                    lista.Add(entidad);
                }

            }

            return lista;

        }

        public BEAdministracionCancelacionDirecta ObtieneDocumento(decimal CodigoDocumento)
        {
            BEAdministracionCancelacionDirecta entidad = null;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTIENE_DATOS_FACT_CANC_DIRECT");
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, CodigoDocumento);

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    entidad = new BEAdministracionCancelacionDirecta();

                    if (!dr.IsDBNull(dr.GetOrdinal("DOC")))
                        entidad.Referencia = dr.GetString(dr.GetOrdinal("DOC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                        entidad.NetoAplicar = dr.GetDecimal(dr.GetOrdinal("INV_NET"));

                    if (!dr.IsDBNull(dr.GetOrdinal("OFF_ID")))
                        entidad.CodigoOficinaSeleccionada = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));

                }

            }

            return entidad;

        }


        public List<BEAdministracionCancelacionDirecta> ListarControl()
        {
            List<BEAdministracionCancelacionDirecta> lista = new List<BEAdministracionCancelacionDirecta>();
            BEAdministracionCancelacionDirecta entidad = null;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTIENE_TIPO_CONTROL");

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    entidad = new BEAdministracionCancelacionDirecta();

                    if (!dr.IsDBNull(dr.GetOrdinal("IdControl")))
                        entidad.ControlId = dr.GetDecimal(dr.GetOrdinal("IdControl"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DescripcionControl")))
                        entidad.DescripcionControl = dr.GetString(dr.GetOrdinal("DescripcionControl"));

                    lista.Add(entidad);
                }

            }

            return lista;

        }


        public decimal RegistrarCancelacionDirecta(BEAdministracionCancelacionDirecta ent, string owner)
        {
            decimal res = 0;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_COBRO_TRANSITORIO_DIRECTO");
            db.AddInParameter(oDbCommand, "@INV_ID_CANCELAR", DbType.Decimal, ent.CodigoDocumento);
            db.AddInParameter(oDbCommand, "@TYPE_CANCELLATION_DIRECT", DbType.Decimal, ent.TipoCancelacion);
            db.AddInParameter(oDbCommand, "@INV_NC_REF", DbType.Int32, 0);
            db.AddInParameter(oDbCommand, "@INV_ID_REF", DbType.Int32, 0);
            db.AddInParameter(oDbCommand, "@MONTO_APLICAR", DbType.Decimal, ent.NetoAplicar);
            db.AddInParameter(oDbCommand, "@USUARIO_CREA", DbType.String, ent.Usuario);
            db.AddInParameter(oDbCommand, "@COMMISSION_GENAREC", DbType.Decimal, ent.CodigoOficinaSeleccionada);
            db.AddInParameter(oDbCommand, "@MEMO_NRO", DbType.String, ent.NumMemo);
            db.AddInParameter(oDbCommand, "@MEMO_OFICINA_ABREV", DbType.String, ent.AbrevOfiMemo);
            db.AddInParameter(oDbCommand, "@MEMO_DATE", DbType.Date, ent.MemoDate);
            db.AddInParameter(oDbCommand, "@SOURCE_ORIGIN", DbType.String, ent.ControlId);
            db.AddInParameter(oDbCommand, "@CONCEPT_DISCOUNT", DbType.String, ent.DescripcionControl);
            db.AddInParameter(oDbCommand, "@OFF_RESPOSABLE", DbType.Decimal, ent.CodigoOficinaResponsable);

            try
            {
                res = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {

            }

            return res;
        }


        public List<BEAdministracionCancelacionDirecta> ListarOficinaCancelacionDirecta(decimal CodigoDoc)
        {
            List<BEAdministracionCancelacionDirecta> lista = new List<BEAdministracionCancelacionDirecta>();
            BEAdministracionCancelacionDirecta entidad = null;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_CANC_DIREC_LISTAR_OFF");
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, CodigoDoc);

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    entidad = new BEAdministracionCancelacionDirecta();

                    if (!dr.IsDBNull(dr.GetOrdinal("IdOficina")))
                        entidad.CodigoOficinaSeleccionada = dr.GetDecimal(dr.GetOrdinal("IdOficina"));

                    if (!dr.IsDBNull(dr.GetOrdinal("Oficina")))
                        entidad.NombreOficina = dr.GetString(dr.GetOrdinal("Oficina"));

                    lista.Add(entidad);
                }

            }

            return lista;

        }

    }
}
