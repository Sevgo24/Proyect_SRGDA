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
    public class DACaracteristica
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BECaracteristica> ListarCartacteristica()
        {
            BECaracteristica be = null;
            List<BECaracteristica> lista = new List<BECaracteristica>();

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CARACT");
            db.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new BECaracteristica();
                    be.CHAR_ID = reader.GetDecimal(reader.GetOrdinal("CHAR_ID"));
                    be.CHAR_LONG = reader.GetString(reader.GetOrdinal("CHAR_LONG"));
                    lista.Add(be);
                }
            }
            return lista;
        }

        public List<BECaracteristica> ListarTarifaCartacteristica(string owner)
        {
            BECaracteristica be = null;
            List<BECaracteristica> lista = new List<BECaracteristica>();

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CARAC_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new BECaracteristica();
                    be.CHAR_ID = reader.GetDecimal(reader.GetOrdinal("CHAR_ID"));
                    be.CHAR_LONG = reader.GetString(reader.GetOrdinal("CHAR_LONG"));
                    lista.Add(be);
                }
            }
            return lista;
        }

        public List<BECaracteristica> ObtenerReglaCartacteristica(string owner,decimal idRegla)
        {
            BECaracteristica be = null;
            List<BECaracteristica> lista = new List<BECaracteristica>();

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_MANT_CAR_REGLA_TARIFA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@CALR_ID", DbType.Decimal, idRegla);
            db.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new BECaracteristica();
                    be.CHAR_ID = reader.GetDecimal(reader.GetOrdinal("CHAR_ID"));
                    be.CHAR_LONG = reader.GetString(reader.GetOrdinal("CHAR_LONG"));
                    be.CHAR_SHORT = reader.GetString(reader.GetOrdinal("CHAR_SHORT"));
                    lista.Add(be);
                }
            }
            return lista;
        }
        public List<BECaracteristicaLic> ListarCaractLicencia(string owner, decimal idLic, string fecha,decimal idLicPlan)
        {
            BECaracteristicaLic be = null;
            List<BECaracteristicaLic> lista = new List<BECaracteristicaLic>();

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_CARACTERISTICAS_X_LIC");
            db.AddInParameter(oDbCommand, "@ID_LIC", DbType.Decimal, idLic);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@FECHA", DbType.String,  fecha);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, idLicPlan);
            
            db.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new BECaracteristicaLic();
                    be.CHAR_ID = reader.GetDecimal(reader.GetOrdinal("CHAR_ID"));

                    if (!reader.IsDBNull(reader.GetOrdinal("CHAR_LONG")))
                    be.CHAR_LONG = reader.GetString(reader.GetOrdinal("CHAR_LONG"));

                    be.CHAR_SHORT = reader.GetString(reader.GetOrdinal("CHAR_SHORT"));

                    be.LIC_VAL_ORIGEN = reader.GetString(reader.GetOrdinal("TIPO"));



                    if (!(reader.IsDBNull(reader.GetOrdinal("VALOR"))))
                    {
                        be.LIC_CHAR_VAL = reader.GetDecimal(reader.GetOrdinal("VALOR"));
                    }
                    if (!(reader.IsDBNull(reader.GetOrdinal("START"))))
                    {
                        be.START = reader.GetDateTime(reader.GetOrdinal("START"));
                    }

                    if (!(reader.IsDBNull(reader.GetOrdinal("FLG_MANUAL"))))
                    {
                        be.FLG_MANUAL = reader.GetBoolean(reader.GetOrdinal("FLG_MANUAL"));
                    }
                    if (!(reader.IsDBNull(reader.GetOrdinal("COMMENT_FLG"))))
                    {
                        be.COMMENT_FLG = reader.GetString(reader.GetOrdinal("COMMENT_FLG"));
                    }

                    
                    lista.Add(be);
                }
            }
            return lista;
        }

        /// <summary>
        /// REGISTRA LAS CARACTERISTICAS DE UNA LICENCIA
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public int InsertarCaractLicencia(BECaracteristicaLic entidad)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_CARACTERISTICA_LICENCIA");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, entidad.LIC_ID);
                db.AddInParameter(oDbCommand, "@CHAR_ID", DbType.String, entidad.CHAR_ID);
                db.AddInParameter(oDbCommand, "@LIC_CHAR_VAL", DbType.String, entidad.LIC_CHAR_VAL);
                db.AddInParameter(oDbCommand, "@LIC_VAL_ORIGEN", DbType.String, entidad.LIC_VAL_ORIGEN);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT);
                db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, entidad.LIC_PL_ID);
                db.AddInParameter(oDbCommand, "@FLG_MANUAL", DbType.Boolean, entidad.FLG_MANUAL);
                db.AddInParameter(oDbCommand, "@COMMENT_FLG", DbType.String, entidad.COMMENT_FLG);

                return db.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }

        /// <summary>
        /// lista las fechas que existen registros de caracteristicas de una licencia para un dropdownlist
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idLic"></param>
        /// <param name="idLicPlan"></param>
        /// <returns></returns>
        public List<BESelectListItem> ListarFechaCaractLicencia(string owner, decimal idLic,decimal idLicPlan)
        {
            BESelectListItem be = null;
            List<BESelectListItem> lista = new List<BESelectListItem>();

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_FECHA_LIC_CAR");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, idLicPlan);
            db.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new BESelectListItem();
                    be.Text = reader.GetString(reader.GetOrdinal("TEXT"));
                    be.Value = reader.GetString(reader.GetOrdinal("VALUE"));
                    lista.Add(be);
                }
            }
            return lista;
        }
        
        /// <summary>
        /// INACTIVA LAS CARACTERISTICAS DE UNA LICENCIA
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="licId"></param>
        /// <returns></returns>
        public int InactivaCaractLicencia(string owner, decimal licId,decimal  licPlanId)
        {

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVAR_LIC_CAR");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, licId);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, licPlanId);
        

             
         return  db.ExecuteNonQuery(oDbCommand);
            
        }
        public int ActualizarCaractLicencia(string owner, decimal licCarId,decimal valor)
        {

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_CARACTERISTICA_LICENCIA");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, licCarId);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_CHAR_VAL", DbType.Decimal, valor);


            return db.ExecuteNonQuery(oDbCommand);

        }

        /// <summary>
        /// OBTIENE LAS CARACTERISITICAS REGISTRADAS PARA UNA LICENCIA Y PERIODO
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idLic"></param>
        /// <param name="idLicPlan"></param>
        /// <returns></returns>
        public List<BECaracteristicaLic> ListarCaracteristicasXPeriodo(string owner, decimal idLic, decimal idLicPlan)
        {
            BECaracteristicaLic be = null;
            List<BECaracteristicaLic> lista = new List<BECaracteristicaLic>();

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_CARACTERISTICAS_PERIODO");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLic);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, idLicPlan);

            db.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new BECaracteristicaLic();
                    be.CHAR_ID = reader.GetDecimal(reader.GetOrdinal("CHAR_ID"));
                    be.LIC_VAL_ORIGEN = reader.GetString(reader.GetOrdinal("LIC_VAL_ORIGEN"));
                    if (!(reader.IsDBNull(reader.GetOrdinal("CHAR_LONG")))) be.CHAR_LONG = reader.GetString(reader.GetOrdinal("CHAR_LONG"));
                    if (!(reader.IsDBNull(reader.GetOrdinal("CHAR_SHORT")))) be.CHAR_SHORT = reader.GetString(reader.GetOrdinal("CHAR_SHORT"));
                    if (!(reader.IsDBNull(reader.GetOrdinal("LIC_CHAR_VAL")))) be.LIC_CHAR_VAL = reader.GetDecimal(reader.GetOrdinal("LIC_CHAR_VAL"));
                    if (!(reader.IsDBNull(reader.GetOrdinal("START")))) be.START = reader.GetDateTime(reader.GetOrdinal("START"));
                    if (!(reader.IsDBNull(reader.GetOrdinal("FLG_MANUAL")))) be.FLG_MANUAL = reader.GetBoolean(reader.GetOrdinal("FLG_MANUAL"));
                    if (!(reader.IsDBNull(reader.GetOrdinal("COMMENT_FLG")))) be.COMMENT_FLG = reader.GetString(reader.GetOrdinal("COMMENT_FLG"));
                    if (!(reader.IsDBNull(reader.GetOrdinal("ENDS")))) be.ENDS = reader.GetDateTime(reader.GetOrdinal("ENDS"));
                    lista.Add(be);
                }
            }
            return lista;
        }
        #region Cadena
        public int InactivaLicenciaMultCaract(string owner, decimal licid)
        {


            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVAR_LICMULT_CAR");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, licid);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);           
         return  db.ExecuteNonQuery(oDbCommand);
        }


        //
        //Utilizacion :EstablecimientoController
        //Descripcion:obtener La licencia mediante el COdigo de establecimiento
        public List<BECaracteristicaLic> LicxCodEst(decimal CodEst)
        {
            List<BECaracteristicaLic> lista = new List<BECaracteristicaLic>();
            BECaracteristicaLic entidad = null;

            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_LisLicxEst");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@ESTID", DbType.Decimal, CodEst);

            using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    entidad = new BECaracteristicaLic();

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_VAL_ORIGEN")))
                        entidad.LIC_VAL_ORIGEN = dr.GetString(dr.GetOrdinal("LIC_VAL_ORIGEN"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FLG_MANUAL")))
                        entidad.FLG_MANUAL = dr.GetBoolean(dr.GetOrdinal("FLG_MANUAL"));

                    if (!dr.IsDBNull(dr.GetOrdinal("COMMENT_FLG")))
                        entidad.COMMENT_FLG = dr.GetString(dr.GetOrdinal("COMMENT_FLG"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CHAR_ID")))
                        entidad.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));

                        //PONER LOS DEMAS ATRIBUTOS SI SE NECESITA PARA EL LISTADO POSTERIOR
                        lista.Add(entidad);
                }

            }

            return lista;

        }


        //Inserta Caracteristicas de Licencia XML
        public List<BECaracteristicaLic> InsertaCaractersiticasLicHijaXML(string owner,string xml)
        {
            List<BECaracteristicaLic> lista = null;
            BECaracteristicaLic entidad = null;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_CARACTERSITICAS_LICENCIA_XML");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand,"@xmlLst",DbType.Xml,xml);

            using (IDataReader dr = (db.ExecuteReader(oDbCommand)))
            {
                lista = new List<BECaracteristicaLic>();
                while (dr.Read())
                {
                    entidad = new BECaracteristicaLic();

                    if (!dr.IsDBNull(dr.GetOrdinal("new_LIC_CAR_ID")))
                        entidad.LIC_CAR_ID = dr.GetDecimal(dr.GetOrdinal("new_LIC_CAR_ID"));

                    lista.Add(entidad);
                }
            }
            return lista;
        }
        /// <summary>
        /// Inactiva las caracteristicas de la licencia por planeamiento y inserta nuevas XML
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="xml"></param>
        /// <returns></returns>

        public List<BECaracteristicaLic> ActualizaCaracteristicasXML(string owner, string xml)
        {
            List<BECaracteristicaLic> lista = null;
            BECaracteristicaLic entidad = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_CARACTERISTICAS_LICENCIA_XML");
            oDataBase.AddInParameter(oDbCommand, "@xml", DbType.Xml, xml);

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                lista = new List<BECaracteristicaLic>();
                while (dr.Read())
                {
                    entidad = new BECaracteristicaLic();

                    if (!dr.IsDBNull(dr.GetOrdinal("nLIC_CAR_VALUE_ID")))
                        entidad.LIC_CAR_ID = dr.GetDecimal(dr.GetOrdinal("nLIC_CAR_VALUE_ID"));


                    lista.Add(entidad);

                }
            }

            return lista;

        }


        #endregion


        #region Megaconcierto
        /// <summary>
        /// LISTA LAS CARACTERISTICAS QUE SE UTILIZAN EN LA TARIFA
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="lic_id"></param>
        /// <returns></returns>
        public List<BECaracteristicaLic> ListarCaractDescPlantillaxTarifa(string owner, decimal lic_id)
        {
            BECaracteristicaLic be = null;
            List<BECaracteristicaLic> lista = new List<BECaracteristicaLic>();

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CHAR_DISC_PLANT_X_RAT");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, lic_id);

            db.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new BECaracteristicaLic();
                    be.CHAR_ID = reader.GetDecimal(reader.GetOrdinal("CHAR_ID"));

                    if (!reader.IsDBNull(reader.GetOrdinal("CHAR_LONG")))
                        be.CHAR_LONG = reader.GetString(reader.GetOrdinal("CHAR_LONG"));
                    lista.Add(be);
                }
            }
            return lista;
        }
        /// <summary>
        /// lista las caracteristicas  que utilizan los descuentos de plantilla de la licencia 
        /// SE EMPLEA PARA LA LISTARLOS EN EL TAB DESCUENTOS
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="lic_id"></param>
        /// <returns></returns>
        public List<BECaracteristicaLic> ListarCaractLicDscPlantilla(string owner, decimal lic_id)
        {
            BECaracteristicaLic be = null;
            List<BECaracteristicaLic> lista = new List<BECaracteristicaLic>();

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_CARACT_ACT_LIC_MEG");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, lic_id);

            db.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new BECaracteristicaLic();
                    be.CHAR_ID = reader.GetDecimal(reader.GetOrdinal("CHAR_ID"));

                    if (!reader.IsDBNull(reader.GetOrdinal("CHAR_LONG")))
                        be.CHAR_LONG = reader.GetString(reader.GetOrdinal("CHAR_LONG"));
                    if (!(reader.IsDBNull(reader.GetOrdinal("LIC_CHAR_VAL"))))
                        be.LIC_CHAR_VAL = reader.GetDecimal(reader.GetOrdinal("LIC_CHAR_VAL"));
                    if (!(reader.IsDBNull(reader.GetOrdinal("LIC_ID"))))
                        be.LIC_ID = reader.GetDecimal(reader.GetOrdinal("LIC_ID"));
                    lista.Add(be);
                }
            }
            return lista;
        }

        #endregion

    }
}

