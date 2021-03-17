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
    public class DAAdministracionEmisionMensual
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");


        public List<BEAdministracionEmisionMensual> ListarOficinasEmisionMensual(string NOMBRE_OFICINA,int DIA,string FECHA_INI,string FECHA_FIN,int ESTADO)
        {
            List<BEAdministracionEmisionMensual> lista = new List<BEAdministracionEmisionMensual>();
            BEAdministracionEmisionMensual entidad = null;


            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_EMISION_MENSUAL");

                db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, NOMBRE_OFICINA);
                db.AddInParameter(oDbCommand, "@DIA", DbType.Int32, DIA);
                db.AddInParameter(oDbCommand, "@RANGO_INI", DbType.String, FECHA_INI);
                db.AddInParameter(oDbCommand, "@RANGO_FIN", DbType.String, FECHA_FIN);
                db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, ESTADO);
                //oDbCommand.CommandTimeout = 90;


                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        entidad = new BEAdministracionEmisionMensual();

                        if (!dr.IsDBNull(dr.GetOrdinal("ID_VALUE")))
                            entidad.ID_VALUE = dr.GetDecimal(dr.GetOrdinal("ID_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESC_VALUE")))
                            entidad.DESC_VALUE = dr.GetString(dr.GetOrdinal("DESC_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DIA")))
                            entidad.DIA = dr.GetInt32(dr.GetOrdinal("DIA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RANGO_INICIAL")))
                            entidad.RANGO_INICIAL = dr.GetString(dr.GetOrdinal("RANGO_INICIAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RANGO_FINAL")))
                            entidad.RANGO_FINAL = dr.GetString(dr.GetOrdinal("RANGO_FINAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFICINA")))
                            entidad.OFICINA = dr.GetDecimal(dr.GetOrdinal("OFICINA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_DE_BAJA")))
                            entidad.FECHA_DE_BAJA = dr.GetString(dr.GetOrdinal("FECHA_DE_BAJA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            entidad.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));

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


        public BEAdministracionEmisionMensual Obtiene(decimal ID)
        {
            BEAdministracionEmisionMensual entidad = null;

            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTIENE_EMISION_X_ID_VALUE");

                db.AddInParameter(oDbCommand, "@ID_VALUE", DbType.Decimal, ID);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        entidad = new BEAdministracionEmisionMensual();

                        //if (!dr.IsDBNull(dr.GetOrdinal("ID_VALUE")))
                        //    entidad.ID_VALUE = dr.GetDecimal(dr.GetOrdinal("ID_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESC_VALUE")))
                            entidad.DESC_VALUE = dr.GetString(dr.GetOrdinal("DESC_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DIA")))
                            entidad.DIA = dr.GetInt32(dr.GetOrdinal("DIA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RANGO_INICIAL")))
                            entidad.RANGO_INICIAL = dr.GetString(dr.GetOrdinal("RANGO_INICIAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RANGO_FINAL")))
                            entidad.RANGO_FINAL = dr.GetString(dr.GetOrdinal("RANGO_FINAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFF_ID")))
                            entidad.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));

                    }


                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return entidad;
        }


        public int InactivaEmisionMensual(decimal ID)
        {
            int res = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVA_EMISION_X_VALUE");

                db.AddInParameter(oDbCommand, "@ID_VALUE", DbType.Decimal, ID);

                res =db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                return res;
            }

            return res;
        }

        public int ModificarEmisionMensual(decimal ID, string NOMBRE_OFICINA, int DIA, string FECHA_INI, string FECHA_FIN, decimal OFF_ID)
        {
            int res = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_EMISION_MENSUAL");

                db.AddInParameter(oDbCommand, "@ID_VALUE", DbType.Decimal, ID);
                db.AddInParameter(oDbCommand, "@OFICINA", DbType.String, NOMBRE_OFICINA);
                db.AddInParameter(oDbCommand, "@DIA", DbType.Int32, DIA);
                db.AddInParameter(oDbCommand, "@RANGO_INI", DbType.String, FECHA_INI);
                db.AddInParameter(oDbCommand, "@RANGO_FIN", DbType.String, FECHA_FIN);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, OFF_ID);
                res =Convert.ToInt32( db.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                return 3; //error
            }

            return res;
        }


        public int ValidaEmisionMensual(decimal ID, int DIA, string FECHA_INI, string FECHA_FIN)
        {
            int res = 0;
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_OFICINA_EMISION_MENSUAL");

                db.AddInParameter(oDbCommand, "@ID_VALUE", DbType.Decimal, ID);
                db.AddInParameter(oDbCommand, "@DIA", DbType.Int32, DIA);
                db.AddInParameter(oDbCommand, "@RANGO_INI", DbType.String, FECHA_INI);
                db.AddInParameter(oDbCommand, "@RANGO_FIN", DbType.String, FECHA_FIN);

                res = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            }
            catch (Exception ex)
            {
                return 3; //error
            }

            return res;
        }
    }
}
