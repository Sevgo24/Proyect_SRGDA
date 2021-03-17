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
    public class DATarifaPlantilla
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public int Insertar(BETarifaPlantilla plantilla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_CARAC_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, plantilla.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_DESC", DbType.String, plantilla.TEMP_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@TEMP_NVAR", DbType.Decimal, plantilla.TEMP_NVAR);
            oDataBase.AddInParameter(oDbCommand, "@STARTS", DbType.DateTime, plantilla.STARTS);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, plantilla.LOG_USER_CREAT);
            oDataBase.AddOutParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, Convert.ToInt32(plantilla.TEMP_ID));

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@TEMP_ID"));
            return id;
        }

        public BETarifaPlantilla Obtener(string owner, decimal id)
        {
            BETarifaPlantilla Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBETENER_TARIFA_PLANTILLA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@TEMP_ID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETarifaPlantilla();
                        Obj.TEMP_ID = dr.GetDecimal(dr.GetOrdinal("TEMP_ID"));
                        Obj.TEMP_DESC = dr.GetString(dr.GetOrdinal("TEMP_DESC"));
                        Obj.TEMP_NVAR = dr.GetDecimal(dr.GetOrdinal("TEMP_NVAR"));
                        Obj.STARTS = dr.GetDateTime(dr.GetOrdinal("STARTS"));
                    }
                }
            }
            return Obj;
        }


        public int Actualizar(BETarifaPlantilla plantilla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_CARAC_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, plantilla.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, plantilla.TEMP_ID);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_DESC", DbType.String, plantilla.TEMP_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@TEMP_NVAR", DbType.Decimal, plantilla.TEMP_NVAR);
            oDataBase.AddInParameter(oDbCommand, "@STARTS", DbType.DateTime, plantilla.STARTS);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, plantilla.LOG_USER_UPDATE);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }


        public List<BETarifaPlantilla> Listar(string owner, string desc, decimal nro, DateTime fini, DateTime ffin,  int estado,int confecha, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_PLANTILLA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_DESC", DbType.String, desc);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_NVAR", DbType.Decimal, nro);
            oDataBase.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, fini);
            oDataBase.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, ffin);
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);
            oDataBase.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, confecha);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BETarifaPlantilla>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                BETarifaPlantilla plantilla=null;
                while (dr.Read())
                {
                    plantilla=new BETarifaPlantilla ();
                    plantilla.TEMP_ID=   dr.GetDecimal ( dr.GetOrdinal("TEMP_ID")) ;
                    plantilla.TEMP_DESC=   dr.GetString ( dr.GetOrdinal("TEMP_DESC")) ;
                    plantilla.TEMP_NVAR=   dr.GetDecimal ( dr.GetOrdinal("TEMP_NVAR")) ;
                    plantilla.STARTS=   dr.GetDateTime ( dr.GetOrdinal("STARTS")) ;
                      if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        plantilla.ESTADO = "ACTIVO";
                    else
                        plantilla.ESTADO = "INACTIVO";
                    plantilla.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(plantilla);
                }                    
            }
            return lista;
        }


        public int Eliminar(BETarifaPlantilla plantilla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_ELIMINAR_PLANTILLA_TARIFA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, plantilla.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.Decimal, plantilla.TEMP_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, plantilla.LOG_USER_UPDATE);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ObtenerXDescripcion(BETarifaPlantilla plantilla)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TARIFA_PLANTILLA_DESC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, plantilla.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID", DbType.String, plantilla.TEMP_ID);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_DESC", DbType.String, plantilla.TEMP_DESC);
            BESociedad obj = new BESociedad();
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }


        public List<BETarifaPlantilla> ListarPlantilla(string owner)
        {
            List<BETarifaPlantilla> lista = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PLANTILLA_TARIFA_LISTAR"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    BETarifaPlantilla item = null;
                    lista = new List<BETarifaPlantilla>();
                    while (dr.Read())
                    {
                        item = new BETarifaPlantilla();
                        item.TEMP_ID = dr.GetDecimal(dr.GetOrdinal("TEMP_ID"));
                        item.TEMP_DESC = dr.GetString(dr.GetOrdinal("TEMP_DESC"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
    }
}
