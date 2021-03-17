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
    public class DALicenciaReporteDeta
    {
        private Database oDatabase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BELicenciaReporteDeta entidad)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_REP_DET");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, entidad.REPORT_ID);
            oDatabase.AddInParameter(oDbCommand, "@REP_TITLE", DbType.String,  entidad.REP_TITLE == null ? null : entidad.REP_TITLE.ToUpper()); 
            oDatabase.AddInParameter(oDbCommand, "@REP_AUTHOR_1", DbType.String, entidad.REP_AUTHOR_1 == null ? null : entidad.REP_AUTHOR_1.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@REP_AUTHOR_2", DbType.String, entidad.REP_AUTHOR_2==null ? null:entidad.REP_AUTHOR_2.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@REP_ARTIST", DbType.String, entidad.REP_ARTIST==null ? null:entidad.REP_ARTIST.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@REP_SHOW", DbType.String, entidad.REP_SHOW == null ? null : entidad.REP_SHOW.ToUpper()); 
            oDatabase.AddInParameter(oDbCommand, "@REP_DATE_EMISION", DbType.DateTime, entidad.REP_DATE_EMISION);
            oDatabase.AddInParameter(oDbCommand, "@REP_HOUR_EMISION", DbType.DateTime, entidad.REP_HOUR_EMISION);
            oDatabase.AddInParameter(oDbCommand, "@REP_DUR_MIN", DbType.Decimal, entidad.REP_DUR_MIN);
            oDatabase.AddInParameter(oDbCommand, "@REP_DUR_SEC", DbType.Decimal, entidad.REP_DUR_SEC);
            oDatabase.AddInParameter(oDbCommand, "@REP_DUR_TSEC", DbType.Decimal, entidad.REP_DUR_TSEC);
            oDatabase.AddInParameter(oDbCommand, "@REP_TIMES", DbType.Decimal, entidad.REP_TIMES);
            oDatabase.AddInParameter(oDbCommand, "@REP_CBASE", DbType.Decimal, entidad.REP_CBASE);
            oDatabase.AddInParameter(oDbCommand, "@ISWC", DbType.String,entidad.ISWC == null ? null : entidad.ISWC.ToUpper());  
            oDatabase.AddInParameter(oDbCommand, "@ISRC", DbType.String, entidad.ISRC == null ? null : entidad.ISRC.ToUpper());   
            oDatabase.AddInParameter(oDbCommand, "@IPI_NAME", DbType.String,  entidad.IPI_NAME == null ? null : entidad.IPI_NAME.ToUpper());   
            oDatabase.AddInParameter(oDbCommand, "@NAME_IP", DbType.String, entidad.NAME_IP == null ? null : entidad.NAME_IP.ToUpper());     
            oDatabase.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, entidad.CUR_ALPHA == null ? null : entidad.CUR_ALPHA.ToUpper());   
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT.ToUpper());
            int n = oDatabase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BELicenciaReporteDeta entidad)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_REP_DET");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_DID", DbType.Decimal, entidad.REPORT_DID);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, entidad.REPORT_ID);
            oDatabase.AddInParameter(oDbCommand, "@REP_TITLE", DbType.String, entidad.REP_TITLE == null ? null : entidad.REP_TITLE.ToUpper()); 
            oDatabase.AddInParameter(oDbCommand, "@REP_AUTHOR_1", DbType.String, entidad.REP_AUTHOR_1 == null ? null : entidad.REP_AUTHOR_1.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@REP_AUTHOR_2", DbType.String, entidad.REP_AUTHOR_2 == null ? null : entidad.REP_AUTHOR_2.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@REP_ARTIST", DbType.String, entidad.REP_ARTIST == null ? null : entidad.REP_ARTIST.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@REP_SHOW", DbType.String, entidad.REP_SHOW == null ? null : entidad.REP_SHOW.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@REP_DATE_EMISION", DbType.DateTime, entidad.REP_DATE_EMISION);
            oDatabase.AddInParameter(oDbCommand, "@REP_HOUR_EMISION", DbType.DateTime, entidad.REP_HOUR_EMISION);
            oDatabase.AddInParameter(oDbCommand, "@REP_DUR_MIN", DbType.Decimal, entidad.REP_DUR_MIN);
            oDatabase.AddInParameter(oDbCommand, "@REP_DUR_SEC", DbType.Decimal, entidad.REP_DUR_SEC);
            oDatabase.AddInParameter(oDbCommand, "@REP_DUR_TSEC", DbType.Decimal, entidad.REP_DUR_TSEC);
            oDatabase.AddInParameter(oDbCommand, "@REP_TIMES", DbType.Decimal, entidad.REP_TIMES);
            oDatabase.AddInParameter(oDbCommand, "@REP_CBASE", DbType.Decimal, entidad.REP_CBASE);
            oDatabase.AddInParameter(oDbCommand, "@ISWC", DbType.String, entidad.ISWC == null ? null : entidad.ISWC.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@ISRC", DbType.String, entidad.ISRC == null ? null : entidad.ISRC.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@IPI_NAME", DbType.String, entidad.IPI_NAME == null ? null : entidad.IPI_NAME.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@NAME_IP", DbType.String, entidad.NAME_IP == null ? null : entidad.NAME_IP.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, entidad.CUR_ALPHA == null ? null : entidad.CUR_ALPHA.ToUpper());   
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, entidad.LOG_USER_UPDATE.ToUpper());
            int n = oDatabase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public List<BELicenciaReporteDeta> Listar(string Owner, decimal IdCab)
        {
            List<BELicenciaReporteDeta> lista = new List<BELicenciaReporteDeta>();
            BELicenciaReporteDeta item = null;

            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASS_REP_DET"))
            {
                oDatabase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                oDatabase.AddInParameter(cm, "@REPORT_ID", DbType.Decimal, IdCab);

                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BELicenciaReporteDeta();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.REPORT_DID = dr.GetDecimal(dr.GetOrdinal("REPORT_DID"));
                        item.REPORT_ID = dr.GetDecimal(dr.GetOrdinal("REPORT_ID"));
                        item.REP_TITLE = dr.GetString(dr.GetOrdinal("REP_TITLE"));
                        item.REP_AUTHOR_1 = dr.GetString(dr.GetOrdinal("REP_AUTHOR_1"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REP_AUTHOR_2")))
                        item.REP_AUTHOR_2 = dr.GetString(dr.GetOrdinal("REP_AUTHOR_2"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REP_ARTIST")))
                        item.REP_ARTIST = dr.GetString(dr.GetOrdinal("REP_ARTIST"));
                        item.REP_SHOW = dr.GetString(dr.GetOrdinal("REP_SHOW"));
                        item.REP_DATE_EMISION = dr.GetDateTime(dr.GetOrdinal("REP_DATE_EMISION"));
                        item.REP_HOUR_EMISION = dr.GetDateTime(dr.GetOrdinal("REP_HOUR_EMISION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REP_DUR_MIN")))
                        item.REP_DUR_MIN = dr.GetDecimal(dr.GetOrdinal("REP_DUR_MIN"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REP_DUR_SEC")))
                        item.REP_DUR_SEC = dr.GetDecimal(dr.GetOrdinal("REP_DUR_SEC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REP_DUR_TSEC")))
                        item.REP_DUR_TSEC = dr.GetDecimal(dr.GetOrdinal("REP_DUR_TSEC"));

                        item.REP_TIMES = dr.GetDecimal(dr.GetOrdinal("REP_TIMES"));
                        item.REP_CBASE = dr.GetDecimal(dr.GetOrdinal("REP_CBASE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ISWC")))
                        item.ISWC = dr.GetString(dr.GetOrdinal("ISWC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ISRC")))
                        item.ISRC = dr.GetString(dr.GetOrdinal("ISRC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("IPI_NAME")))
                        item.IPI_NAME = dr.GetString(dr.GetOrdinal("IPI_NAME"));

                        item.NAME_IP = dr.GetString(dr.GetOrdinal("NAME_IP"));
                        item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public BELicenciaReporteDeta Obtener(string Owner, decimal IdDet, decimal IdCab)
        {
            List<BELicenciaReporteDeta> lista = new List<BELicenciaReporteDeta>();
            BELicenciaReporteDeta item = null;

            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_REP_DET"))
            {
                oDatabase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                oDatabase.AddInParameter(cm, "@REPORT_DID", DbType.Decimal, IdDet);
                oDatabase.AddInParameter(cm, "@REPORT_ID", DbType.Decimal, IdCab);

                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        item = new BELicenciaReporteDeta();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.REPORT_DID = dr.GetDecimal(dr.GetOrdinal("REPORT_DID"));
                        item.REPORT_ID = dr.GetDecimal(dr.GetOrdinal("REPORT_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REP_TITLE"))) 
                        item.REP_TITLE = dr.GetString(dr.GetOrdinal("REP_TITLE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REP_AUTHOR_1")))
                        item.REP_AUTHOR_1 = dr.GetString(dr.GetOrdinal("REP_AUTHOR_1"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REP_AUTHOR_2")))
                        item.REP_AUTHOR_2 = dr.GetString(dr.GetOrdinal("REP_AUTHOR_2"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REP_ARTIST")))
                        item.REP_ARTIST = dr.GetString(dr.GetOrdinal("REP_ARTIST"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REP_SHOW")))
                        item.REP_SHOW = dr.GetString(dr.GetOrdinal("REP_SHOW"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REP_DATE_EMISION")))
                        item.REP_DATE_EMISION = dr.GetDateTime(dr.GetOrdinal("REP_DATE_EMISION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REP_HOUR_EMISION")))
                        item.REP_HOUR_EMISION = dr.GetDateTime(dr.GetOrdinal("REP_HOUR_EMISION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REP_DUR_MIN")))
                        item.REP_DUR_MIN = dr.GetDecimal(dr.GetOrdinal("REP_DUR_MIN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REP_DUR_SEC")))
                        item.REP_DUR_SEC = dr.GetDecimal(dr.GetOrdinal("REP_DUR_SEC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REP_DUR_TSEC")))
                        item.REP_DUR_TSEC = dr.GetDecimal(dr.GetOrdinal("REP_DUR_TSEC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REP_TIMES")))
                        item.REP_TIMES = dr.GetDecimal(dr.GetOrdinal("REP_TIMES"));
                        if (!dr.IsDBNull(dr.GetOrdinal("REP_CBASE")))
                        item.REP_CBASE = dr.GetDecimal(dr.GetOrdinal("REP_CBASE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ISWC")))
                        item.ISWC = dr.GetString(dr.GetOrdinal("ISWC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ISRC")))
                        item.ISRC = dr.GetString(dr.GetOrdinal("ISRC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IPI_NAME")))
                        item.IPI_NAME = dr.GetString(dr.GetOrdinal("IPI_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NAME_IP")))
                        item.NAME_IP = dr.GetString(dr.GetOrdinal("NAME_IP"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_ALPHA")))
                        item.CUR_ALPHA = dr.GetString(dr.GetOrdinal("CUR_ALPHA"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        lista.Add(item);
                    }
                }
            }
            return item;
        }

        public int Eliminar(BELicenciaReporteDeta entidad)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASD_REP_DET");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_DID", DbType.Decimal, entidad.REPORT_DID);
            oDatabase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, entidad.REPORT_ID);
            int n = oDatabase.ExecuteNonQuery(oDbCommand);
            return n;
        }
        public int Activar(string owner, decimal IdDeta, decimal IdRepCab, string usuModi)
        {
           
              DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_ACTIVAR_REP_DET");
              oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
              oDatabase.AddInParameter(oDbCommand, "@REPORT_DID", DbType.Decimal, IdDeta);
              oDatabase.AddInParameter(oDbCommand, "@REPORT_ID", DbType.Decimal, IdRepCab);
              oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuModi);
           return  oDatabase.ExecuteNonQuery(oDbCommand);
           
        }


        public int ValidaLicenciaFactCancelada(decimal LIC_ID)
        {
            int resp = 0;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_VALIDA_LIC_FACT_CANCELADA");
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);

            resp = Convert.ToInt32( oDatabase.ExecuteScalar(oDbCommand));
            return resp;

        }
        public int ValidaLicenciaFactValorizada(decimal LIC_ID)
        {
            int resp = 0;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_VALIDA_LICENCIA_VALORIZADA");
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);

            resp = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return resp;

        }

        public List<BELicenciaReporte> ListarPlaneamientoxLicenciaOpcion(decimal CodigoLicencia, int Opcion)
        {
            List<BELicenciaReporte> lista = new List<BELicenciaReporte>();
            BELicenciaReporte entidad = null;

            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_PLANEAMIENTO_REPORTE");
                oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, CodigoLicencia);
                oDatabase.AddInParameter(oDbCommand, "@OPCION", DbType.Int32, Opcion);

                using (IDataReader dr = (oDatabase.ExecuteReader(oDbCommand)))
                {
                    while (dr.Read())
                    {
                        entidad = new BELicenciaReporte();
                        if (!dr.IsDBNull(dr.GetOrdinal("TEXTO")))
                            entidad.DESCRIPCION = dr.GetString(dr.GetOrdinal("TEXTO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("VALOR")))
                            entidad.VALOR = dr.GetString(dr.GetOrdinal("VALOR"));

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

    }
}
