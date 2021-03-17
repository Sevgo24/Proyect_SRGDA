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
    public class DADivisionRecaudador
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BEDivisionRecaudador recaudador)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_DIVISION_SOCIO_RECAUDADOR");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, recaudador.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, recaudador.DAD_ID);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, recaudador.BPS_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, recaudador.LOG_USER_CREAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Actualizar(BEDivisionRecaudador recaudador)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_DIVISION_SOCIO_RECAUDADOR");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, recaudador.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, recaudador.DAD_ID);
            oDataBase.AddInParameter(oDbCommand, "@ANT_DAD_ID", DbType.Decimal, recaudador.DAD_ID_ANT);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, recaudador.LOG_USER_UPDAT);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEDivisionRecaudador Obtener(BEDivisionRecaudador recaudador)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SOCIO_RECAUDADOR");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, recaudador.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, recaudador.DAD_ID);

            BEDivisionRecaudador ent = new BEDivisionRecaudador();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                dr.Read();
                ent.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                ent.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
            }
            return ent;
        }

        public int Eliminar(BEDivisionRecaudador recaudador)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASD_DIVISION_SOCIO_RECAUDADOR");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, recaudador.OWNER);
            oDataBase.AddInParameter(oDbComand, "@DAD_ID", DbType.Decimal, recaudador.DAD_ID);
            oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, recaudador.BPS_ID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, recaudador.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbComand);
            return r;
        }

        public int Activar(string owner, decimal idDiv, decimal idAgent, string user)
        {
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASD_ACTIVAR_SOCIO_RECAUDADOR");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@DAD_ID", DbType.Decimal, idDiv);
            oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, idAgent);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, user);
            int r = oDataBase.ExecuteNonQuery(oDbComand);
            return r;
        }

        public List<SocioNegocio> ListarAgenteRecaudo(string owner, decimal codigo)
        {
            SocioNegocio item = null;
            List<SocioNegocio> lista = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_SOCIO_DIV"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DAD_ID", DbType.Decimal, codigo);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    lista = new List<SocioNegocio>();

                    while (dr.Read())
                    {
                        item = new SocioNegocio();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        //item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        {
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }

                        item.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                        {
                            item.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        {
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        {
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                        {
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                        }
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public SocioNegocio ObtenerAgenteRecaudo(string owner, decimal codigo, decimal idAgente)
        {
            SocioNegocio item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SOCIO_DIV"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DAD_ID", DbType.Decimal, codigo);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idAgente);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new SocioNegocio();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        {
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }

                        item.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                        {
                            item.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }
            return item;
        }

        public SocioNegocio ValidarAgenteRecaudo(string owner, decimal Id)
        {
            SocioNegocio item = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_VALIDACION_RECAUDADOR"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, Id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new SocioNegocio();
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    }
                }
            }
            return item;
        }

        public BEDivisionRecaudador ValidarDivision(string owner, decimal Id)
        {
            BEDivisionRecaudador item = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_VALIDACION_DIVISION"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DAD_ID", DbType.Decimal, Id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEDivisionRecaudador();
                        item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    }
                }
            }
            return item;
        }

        //OFICINA COMERCIAL - DIVISION
 

    }
}
