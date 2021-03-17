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
using SGRDA.Entities.FacturaElectronica;
using System.Data.Common;
using System.Data;
using System.Xml;


namespace SGRDA.DA
{
    public class DAAdministracionSolicitudAprobacion
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        #region SOLICITUD DE APROBACION


        public int SolicitudAprobacionDocumento(decimal INV_ID, int TIPO, string DESCRIPCION, string usuario,int RESPT)
        {
            int r = 0;

            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_SOLICITUD_DOCUMENTO"))
            {
                oDataBase.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, INV_ID);
                oDataBase.AddInParameter(oDbCommand, "@TIPO", DbType.Int32, TIPO);
                oDataBase.AddInParameter(oDbCommand, "@DESCRIPCION", DbType.String, DESCRIPCION);
                oDataBase.AddInParameter(oDbCommand, "@USUARIO_ACTUAL", DbType.String, usuario);
                oDataBase.AddInParameter(oDbCommand, "@RESPT", DbType.Int32, RESPT);
                

                r = oDataBase.ExecuteNonQuery(oDbCommand);
            }
            return r;


        }


        public List<BEAdministracionSolicitudAprobacion> listar(decimal INV_ID, decimal SERIE, decimal INV_NUMBER, int CONFECHA, string FECHA_INI, string FECHA_FIN, decimal OFF_ID, int ESTADO_APROB, int TIPO)
        {

            List<BEAdministracionSolicitudAprobacion> lista = new List<BEAdministracionSolicitudAprobacion>();
            BEAdministracionSolicitudAprobacion entidad = null;


            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_SOLICITUDES_APROBACION");

                oDataBase.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, INV_ID);
                oDataBase.AddInParameter(oDbCommand, "@INV_NMR", DbType.Decimal, SERIE);
                oDataBase.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, INV_NUMBER);
                oDataBase.AddInParameter(oDbCommand, "@CON_RANGO", DbType.Int32, CONFECHA);
                oDataBase.AddInParameter(oDbCommand, "@FECHA_INICIAL", DbType.DateTime, FECHA_INI);
                oDataBase.AddInParameter(oDbCommand, "@FECHA_FINAL", DbType.DateTime, FECHA_FIN);
                oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, OFF_ID);
                oDataBase.AddInParameter(oDbCommand, "@ESTADO_APROB", DbType.Int32, ESTADO_APROB);
                oDataBase.AddInParameter(oDbCommand, "@TIPO", DbType.Int32, TIPO);
                //oDbCommand.CommandTimeout = 90;


                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionSolicitudAprobacion();

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            entidad.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            entidad.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO")))
                            entidad.NUMERO = dr.GetDecimal(dr.GetOrdinal("NUMERO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFICINA")))
                            entidad.OFICINA = dr.GetString(dr.GetOrdinal("OFICINA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("USUARIO_SOLICITANTE")))
                            entidad.USUARIO_SOLICITANTE = dr.GetString(dr.GetOrdinal("USUARIO_SOLICITANTE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_QUIEBRA")))
                            entidad.FECHA_QUIEBRA = dr.GetString(dr.GetOrdinal("FECHA_QUIEBRA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO")))
                            entidad.TIPO = dr.GetInt32(dr.GetOrdinal("TIPO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NETO")))
                            entidad.NETO = dr.GetDecimal(dr.GetOrdinal("NETO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_EMISION")))
                            entidad.FECHA_EMISION = dr.GetString(dr.GetOrdinal("FECHA_EMISION"));
                        lista.Add(entidad);

                    }
                }
            }
            catch (Exception EX)
            {
                return null;
            }

            return lista;

    }



        public BEAdministracionSolicitudAprobacion ObtieneSOlicitudAprobacion(decimal INV_ID , string owner)
        {
            BEAdministracionSolicitudAprobacion entidad = null;

            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_SOLICITUD_DE_DOCUMENTO");

                oDataBase.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, INV_ID);
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        entidad = new BEAdministracionSolicitudAprobacion();

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            entidad.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("USUARIO_SOLICITANTE")))
                            entidad.USUARIO_SOLICITANTE = dr.GetString(dr.GetOrdinal("USUARIO_SOLICITANTE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            entidad.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO")))
                            entidad.NUMERO = dr.GetDecimal(dr.GetOrdinal("NUMERO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_QUIEBRA")))
                            entidad.FECHA_QUIEBRA = dr.GetString(dr.GetOrdinal("FECHA_QUIEBRA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO")))
                            entidad.TIPO = dr.GetInt32(dr.GetOrdinal("TIPO"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                        //    entidad.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPCION")))
                            entidad.DESCRIPCION = dr.GetString(dr.GetOrdinal("DESCRIPCION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO_DESC")))
                            entidad.TIPO_DESC = dr.GetString(dr.GetOrdinal("TIPO_DESC"));

                    }

                }
            }catch(Exception ex)
            {

            }

            return entidad;



        }


        public List<BEAdministracionSolicitudAprobacion> ListarTipoSolicitud()
        {

            List<BEAdministracionSolicitudAprobacion> lista = new List<BEAdministracionSolicitudAprobacion>();
            BEAdministracionSolicitudAprobacion entidad = null;


            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPO_SOLICITUD");
                //oDbCommand.CommandTimeout = 90;


                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionSolicitudAprobacion();

                        if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPCION")))
                            entidad.DESCRIPCION = dr.GetString(dr.GetOrdinal("DESCRIPCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("VALUE")))
                            entidad.VALUE = dr.GetString(dr.GetOrdinal("VALUE"));
                        lista.Add(entidad);

                    }
                }
            }
            catch (Exception EX)
            {
                return null;
            }

            return lista;

        }
        #endregion

    }
}
