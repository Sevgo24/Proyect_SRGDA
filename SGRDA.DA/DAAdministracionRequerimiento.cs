using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DAAdministracionRequerimiento
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEAdministracionRequerimientos> Lista(string OWNER, decimal ID_REQ, int TIPO, int ESTADO, int CON_FECHA, string FECHA_INI, string FECHA_FIN, decimal OFICINA, decimal LIC_ID, decimal INV_ID, decimal EST_ID, decimal BEC_ID, int INACT_TYPE)
        {
            List<BEAdministracionRequerimientos> lista = new List<BEAdministracionRequerimientos>();
            BEAdministracionRequerimientos entidad = null;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_REQUERIMIENTOS");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, OWNER);
                db.AddInParameter(oDbCommand, "@ID_REQ", DbType.Decimal, ID_REQ);
                db.AddInParameter(oDbCommand, "@TIPO", DbType.Int32, TIPO);
                db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, ESTADO);
                db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, CON_FECHA);
                db.AddInParameter(oDbCommand, "@FECHA_INI", DbType.String, FECHA_INI);
                db.AddInParameter(oDbCommand, "@FECHA_FIN", DbType.String, FECHA_FIN);
                db.AddInParameter(oDbCommand, "@OFICINA", DbType.Decimal, OFICINA);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, INV_ID);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, EST_ID);
                db.AddInParameter(oDbCommand, "@BEC_ID", DbType.Decimal, BEC_ID);
                db.AddInParameter(oDbCommand, "@INACT_TYPE", DbType.Int32, INACT_TYPE);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionRequerimientos();

                        if (!dr.IsDBNull(dr.GetOrdinal("ID_REQ")))
                            entidad.ID_REQ = dr.GetDecimal(dr.GetOrdinal("ID_REQ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REQUERIMENTS_DESC")))
                            entidad.REQUERIMENTS_DESC = dr.GetString(dr.GetOrdinal("REQUERIMENTS_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REQ_DESCRIPCION")))
                            entidad.REQ_DESCRIPCION = dr.GetString(dr.GetOrdinal("REQ_DESCRIPCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REQ_DATE")))
                            entidad.REQ_DATE = dr.GetString(dr.GetOrdinal("REQ_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO")))
                            entidad.MONTO = dr.GetDecimal(dr.GetOrdinal("MONTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            entidad.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFICINA")))
                            entidad.OFICINA = dr.GetDecimal(dr.GetOrdinal("OFICINA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFF_NAME")))
                            entidad.DESC_OFICINA = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.ESTADO = dr.GetInt32(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESC_ESTADO")))
                            entidad.DESC_ESTADO = dr.GetString(dr.GetOrdinal("DESC_ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            entidad.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            entidad.LOG_DATE_CREAT = dr.GetString(dr.GetOrdinal("LOG_DATE_CREAT"));

                        lista.Add(entidad);
                    }
                }

            }
            catch (Exception ex)
            {

            }


            return lista;
        }

        public List<BETipoRequerimiento> ListarTipoRequerimiento(int tipo)
        {
            List<BETipoRequerimiento> lista = new List<BETipoRequerimiento>();
            BETipoRequerimiento entidad = null;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_TIPO_REQUERIMIENTO");
            db.AddInParameter(oDbCommand, "@TIPO", DbType.Int32, tipo);

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    entidad = new BETipoRequerimiento();

                    if (!dr.IsDBNull(dr.GetOrdinal("ID_REQ_TYPE")))
                        entidad.ID_REQ_TYPE = dr.GetDecimal(dr.GetOrdinal("ID_REQ_TYPE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("REQUERIMENTS_DESC")))
                        entidad.REQUERIMENTS_DESC = dr.GetString(dr.GetOrdinal("REQUERIMENTS_DESC"));

                    lista.Add(entidad);
                }

            }

            return lista;

        }

        public BEAdministracionRequerimientos ObtieneRequerimientos(string owner, decimal ID)
        {
            BEAdministracionRequerimientos entidad = null;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_REQUERIMIENTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@ID_REQ", DbType.Decimal, ID);
            try
            {
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        entidad = new BEAdministracionRequerimientos();

                        if (!dr.IsDBNull(dr.GetOrdinal("ID_REQ")))
                            entidad.ID_REQ = dr.GetDecimal(dr.GetOrdinal("ID_REQ"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REQUERIMENTS_DESC")))
                            entidad.REQUERIMENTS_DESC = dr.GetString(dr.GetOrdinal("REQUERIMENTS_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REQ_DESCRIPCION")))
                            entidad.REQ_DESCRIPCION = dr.GetString(dr.GetOrdinal("REQ_DESCRIPCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                            entidad.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            entidad.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                            entidad.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                            entidad.SERIE = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                            entidad.NUMERO = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTOANTERIOR")))
                            entidad.INV_NET = dr.GetDecimal(dr.GetOrdinal("MONTOANTERIOR"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTOACT")))
                            entidad.INV_NETACT = dr.GetDecimal(dr.GetOrdinal("MONTOACT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHAANTERIOR")))
                            entidad.INV_DATE = dr.GetString(dr.GetOrdinal("FECHAANTERIOR"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHAACT")))
                            entidad.REQ_DATE = dr.GetString(dr.GetOrdinal("FECHAACT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ACTIVO")))
                            entidad.ACTIVO = dr.GetInt32(dr.GetOrdinal("ACTIVO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            entidad.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.ESTADO = dr.GetInt32(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            entidad.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            entidad.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTO")))
                            entidad.DOCUMENTOSOCIO = dr.GetString(dr.GetOrdinal("DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REQ_DESCRIPCION_RESP")))
                            entidad.REQ_DESCRIPCION_RESP = dr.GetString(dr.GetOrdinal("REQ_DESCRIPCION_RESP"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BEC_ID")))
                            entidad.BEC_ID = dr.GetDecimal(dr.GetOrdinal("BEC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REC_ID")))
                            entidad.REC_ID = dr.GetDecimal(dr.GetOrdinal("REC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INACT_tYPE")))
                            entidad.CodigoTipoInactivacion = dr.GetInt32(dr.GetOrdinal("INACT_tYPE"));
                    }
                }
            }
            catch (Exception EX)
            {

            }
            return entidad;
        }


        public int ActualizaRequerimiento(BEAdministracionRequerimientos ent)
        {
            int res = 0;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_REQUERIMIENTO");
            db.AddInParameter(oDbCommand, "@ID_REQ", DbType.Decimal, ent.ID_REQ);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, ent.ESTADO);
            db.AddInParameter(oDbCommand, "@REQ_DESCRIPCION_RESP", DbType.String, ent.REQ_DESCRIPCION_RESP);

            res = Convert.ToInt32( db.ExecuteScalar(oDbCommand));

            return res;
        }

        public int ActualizaRespuestaRequerimiento(decimal id, string USUARIO)
        {
            int res = 0;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_REQUERIMIENTO_X_TIPO");
            db.AddInParameter(oDbCommand, "@ID_REQ", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, USUARIO);

            res = db.ExecuteNonQuery(oDbCommand);

            return res;
        }

        public decimal RegistraRequerimientoGral(BEAdministracionRequerimientos ent, string owner)
        {
            decimal res = 0;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_REQUERIMIENTO_GRAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@MONTO", DbType.Decimal, ent.MONTO);
            db.AddInParameter(oDbCommand, "@REQ_DATE", DbType.String, ent.REQ_DATE);
            db.AddInParameter(oDbCommand, "@ACTIVO", DbType.Int32, ent.ACTIVO);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, ent.LIC_ID);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, ent.INV_ID);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, ent.EST_ID);
            db.AddInParameter(oDbCommand, "@USUARIO", DbType.String, ent.LOG_USER_CREAT);
            db.AddInParameter(oDbCommand, "@OFICINA", DbType.Decimal, ent.OFICINA);
            db.AddInParameter(oDbCommand, "@REQ_DESCRIPCION", DbType.String, ent.REQ_DESCRIPCION);
            db.AddInParameter(oDbCommand, "@ID_REQ_TYPE", DbType.Int32, ent.ID_REQ_TYPE);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, ent.BPS_ID);
            db.AddInParameter(oDbCommand, "@BEC_ID", DbType.Decimal, ent.BEC_ID);
            db.AddInParameter(oDbCommand, "@TIPO_INACT", DbType.Decimal, ent.CodigoTipoInactivacion);

            try
            {
                res = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {

            }

            return res;
        }

    }
}
