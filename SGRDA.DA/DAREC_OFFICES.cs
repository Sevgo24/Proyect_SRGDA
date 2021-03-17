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
    public class DAREC_OFFICES
    {

        public List<BEOffices> Usp_Get_Rec_Offices_Get_By_Off_Name(string owner,decimal OFF_ID, string param, int estado, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_Get_REC_OFFICES_GET_BY_OFF_NAME");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFICINA", DbType.Decimal, OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@OFF_NAME", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_Get_REC_OFFICES_GET_BY_OFF_NAME", owner, OFF_ID, param, estado, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEOffices>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEOffices(reader, Convert.ToInt32(results)));
            }
            return lista;

        }

        public int Usps_Del_Rec_Offices(BEOffices office)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("Usps_Del_Rec_Offices");
                oDatabase.AddInParameter(oDbCommand, "@owner", DbType.String, office.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@Log_User_Updat", DbType.String, office.LOG_USER_UPDAT);
                oDatabase.AddInParameter(oDbCommand, "@off_id", DbType.Int32, office.OFF_ID);
                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {

                return 0;
            }
        }


        public List<BETreeview> Usp_Get_Rec_Offices_Get_By_Off_Name_By_Hq_Ind(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_Get_REC_OFFICES_GET_BY_OFF_NAME_by_HQ_IND");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.ExecuteNonQuery(oDbCommand);

            var lista = new List<BETreeview>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                BETreeview ent;
                while (reader.Read())
                {
                    ent = new BETreeview();
                    ent.cod = Convert.ToInt32(reader["OFF_ID"]);
                    ent.text = Convert.ToString(reader["OFF_NAME"]).ToUpper();
                    if (!reader.IsDBNull(reader.GetOrdinal("SOFF_ID")))
                        ent.ManagerID = Convert.ToInt32(reader["SOFF_ID"]);
                    else
                        ent.ManagerID = 0;
                    lista.Add(ent);
                }
            }
            return lista;
        }

        public int ups_Ins_Rec_Offices(BEOffices oficina)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("usps_Ins_Rec_Offices");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, oficina.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@OFF_NAME", DbType.String, oficina.OFF_NAME.ToUpper());
                oDatabase.AddInParameter(oDbCommand, "@HQ_IND", DbType.String, oficina.HQ_IND);
                oDatabase.AddInParameter(oDbCommand, "@SOFF_ID", DbType.Int32, oficina.SOFF_ID);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, oficina.LOG_USER_CREAT);
                oDatabase.AddInParameter(oDbCommand, "@ADD_ID", DbType.Int32, oficina.ADD_ID);
                oDatabase.AddInParameter(oDbCommand, "@OFF_TYPE", DbType.Decimal, oficina.OFF_TYPE);
                oDatabase.AddInParameter(oDbCommand, "@OFF_CC", DbType.String, oficina.OFF_CC.ToUpper());
                oDatabase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, oficina.BPS_ID);
                oDatabase.AddOutParameter(oDbCommand, "@OFF_ID", DbType.Decimal, oficina.OFF_ID);
                oDatabase.AddInParameter(oDbCommand, "@OFF_ID_PRE", DbType.Decimal, oficina.OFF_ID_PRE);

                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                int id = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@OFF_ID"));
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public BEOffices Obtener(string owner, decimal off_id)
        {
            BEOffices oficina = null;
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("Usp_Get_Office_by_OFF_ID");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Int32, off_id);


                using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
                {
                    if (reader.Read())
                    {
                        oficina = new BEOffices();
                        oficina.OFF_ID = Convert.ToInt32(reader["OFF_ID"]);
                        oficina.OFF_NAME = Convert.ToString(reader["OFF_NAME"]);
                        oficina.HQ_IND = Convert.ToString(reader["HQ_IND"]);
                        oficina.SOFF_ID = Convert.ToInt32(reader["SOFF_ID"]);
                        oficina.ADD_ID = Convert.ToInt32(reader["ADD_ID"]);
                        oficina.OFF_TYPE = Convert.ToInt32(reader["OFF_TYPE"]);
                        oficina.OFF_CC = Convert.ToString(reader["OFF_CC"]);
                        oficina.BPS_ID = Convert.ToDecimal(reader["BPS_ID"]);
                        oficina.SOCIO = Convert.ToString(reader["SOCIO"]);
                    }


                }

            }
            catch (Exception)
            {
                throw;
            }
            return oficina;
        }

        public int Actualizar(BEOffices office)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("Usp_Upd_Rec_Offices");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, office.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Int32, office.OFF_ID);
                oDatabase.AddInParameter(oDbCommand, "@OFF_NAME", DbType.String, office.OFF_NAME.ToUpper());
                oDatabase.AddInParameter(oDbCommand, "@HQ_IND", DbType.String, office.HQ_IND);
                oDatabase.AddInParameter(oDbCommand, "@SOFF_ID", DbType.Int32, office.SOFF_ID);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, office.LOG_USER_UPDAT);
                oDatabase.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, office.ADD_ID);
                oDatabase.AddInParameter(oDbCommand, "@OFF_TYPE", DbType.Decimal, office.OFF_TYPE);
                oDatabase.AddInParameter(oDbCommand, "@OFF_CC", DbType.String, office.OFF_CC.ToUpper());
                oDatabase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, office.BPS_ID);
                oDatabase.AddInParameter(oDbCommand, "@OFF_ID_PRE", DbType.Decimal, office.OFF_ID_PRE);

                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<BEOffices> Usp_Get_Rec_Offices_By_Offname_Dep(string owner, string param, decimal off_id, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("Usp_Get_Rec_Offices_By_Offname_Dep");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_NAME", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, off_id);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("Usp_Get_Rec_Offices_By_Offname_Dep", owner, param, off_id, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEOffices>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEOffices(reader, Convert.ToInt32(results)));
            }
            return lista;
        }


        public int Usp_Upd_RecOffices_by_OffId_and_SoffId(BEOffices office)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("Usp_Upd_RecOffices_by_OffId_and_SoffId");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, office.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Int32, office.OFF_ID);
                oDatabase.AddInParameter(oDbCommand, "@SOFF_ID", DbType.Int32, office.SOFF_ID);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, office.LOG_USER_UPDAT);

                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Inserta Observaciones relacionada a la oficina
        /// </summary>
        /// <param name="obs"></param>
        /// <returns></returns>
        public int InsertarObsOff(BEObservationOff obs)
        {
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_INSERTAR_OBS_OFF");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, obs.OWNER);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, obs.LOG_USER_CREAT);
                oDataBase.AddInParameter(oDbComand, "@OBS_ID", DbType.Int32, obs.OBS_ID);
                oDataBase.AddInParameter(oDbComand, "@OFF_ID", DbType.Int32, obs.OFF_ID);

                int n = oDataBase.ExecuteNonQuery(oDbComand);

                return n;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public BEOffices OficinaXID(int OFF_ID)
        {
            BEOffices obj = null;
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_OFF_ACTIVAS_X_ID"))
                {
                    db.AddInParameter(cm, "@OFF_ID", DbType.Decimal, OFF_ID);
                    db.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            obj = new BEOffices();
                            obj.OFF_ID = Convert.ToInt32(dr["OFF_ID"]);
                            obj.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return obj;
        }

        public List<BEOffices> ListarOffActivasLYRICS(string owner, decimal OFICINA)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_OFF_ACTIVAS_SERVICE");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@OFICINA", DbType.Decimal, OFICINA);


            var lista = new List<BEOffices>();
            BEOffices obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    obs = new BEOffices();
                    obs.OFF_ID = Convert.ToInt32(reader["OFF_ID"]);
                    obs.OFF_NAME = Convert.ToString(reader["OFF_NAME"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }
        public List<BEOffices> ListarOffActivas(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_OFF_ACTIVAS");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            //oDataBase.AddInParameter(oDbComand, "@OFICINA", DbType.Decimal, OFICINA);


            var lista = new List<BEOffices>();
            BEOffices obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    obs = new BEOffices();
                    obs.OFF_ID = Convert.ToInt32(reader["OFF_ID"]);
                    obs.OFF_NAME = Convert.ToString(reader["OFF_NAME"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }
        public List<BEObservationGral> ObtenerObsOficina(string owner, int offID, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_OBSOFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Int16, offID);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_OBTENER_OBSOFICINA", owner, offID, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEObservationGral>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEObservationGral(reader, Convert.ToInt32(results)));
            }
            return lista;

        }


        public int ObtenerPrincipales(string owner)
        {
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PRINCIPALES");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                int n = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
                return n;
            }
            catch (Exception)
            {
                return 0;
            }

        }




        public List<BEParametroOff> ListarParametroOFF(string owner, decimal offid, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_PARAMETROS_OFF");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, offid);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_PARAMETROS_OFF", owner, offid, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEParametroOff>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEParametroOff
                    {
                        PAR_ID = Convert.ToDecimal(reader["PAR_ID"]),
                        PAR_DESC = Convert.ToString(reader["PAR_DESC"]),
                        PAR_VALUE = Convert.ToString(reader["PAR_VALUE"]),
                        TotalVirtual = Convert.ToInt32(results)
                    });
            }
            return lista;
        }

        public List<BEDocumentoOfi> ListarDocumentoOfi(string owner, decimal offid, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_DOCUMENTO_OFF");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, offid);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_DOCUMENTO_OFF", owner, offid, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEDocumentoOfi>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEDocumentoOfi
                    {
                        DOC_ID = Convert.ToDecimal(reader["DOC_ID"]),
                        DOC_DESC = Convert.ToString(reader["DOC_DESC"]),
                        DOC_PATH = Convert.ToString(reader["DOC_PATH"]),
                        DOC_DATE = Convert.ToDateTime(reader["DOC_DATE"]),
                        TotalVirtual = Convert.ToInt32(results)
                    });
            }
            return lista;
        }


        public int InsertarDireccionOfi(BEDireccionOfi dir)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_DIRECCION_OFF");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, dir.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, dir.OFF_ID);
                oDatabase.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, dir.ADD_ID);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, dir.LOG_USER_CREAT);

                int n = oDatabase.ExecuteNonQuery(oDbCommand);
                return n;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public List<BEOffices> DependenciaXOficina(decimal offID, string owner)
        {
            List<BEOffices> oficinas = null;
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DEPENDENCIA_OFF"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, offID);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BEOffices ObjOfi = null;
                        oficinas = new List<BEOffices>();
                        while (dr.Read())
                        {
                            ObjOfi = new BEOffices();

                            ObjOfi.OFF_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OFF_ID")));
                            ObjOfi.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                            ObjOfi.SOFF_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("SOFF_ID")));

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                ObjOfi.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }

                            oficinas.Add(ObjOfi);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return oficinas;
        }
        
        public List<BERecaudadorBps> ListarAgente(decimal offid, string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_BPSCOLLECTOR_OFF");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, offid);
            oDataBase.ExecuteNonQuery(oDbCommand);

            var lista = new List<BERecaudadorBps>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BERecaudadorBps
                    {
                        BPS_ID = Convert.ToDecimal(reader["BPS_ID"]),
                        TAXN_NAME = Convert.ToString(reader["TAXN_NAME"]),
                        TAX_ID = Convert.ToString(reader["TAX_ID"]),
                        BPS_NAME = Convert.ToString(reader["BPS_NAME"]),
                        LEVEL = Convert.ToString(reader["LEVEL"]),
                        COLL_MAIN = Convert.ToBoolean(reader["COLL_MAIN"])
                    });
            }
            return lista;
        }


        public List<BEOffices> ObtenerXDescripcion(BEOffices oficna)
        {
            try
            {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_OFICINA_NOMBRE");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, oficna.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@OFF_NAME", DbType.String, oficna.OFF_NAME);

                List<BEOffices> lista;
                using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
                {
                    BEOffices ent;
                    lista = new List<BEOffices>();
                    while (dr.Read())
                    {
                        ent = new BEOffices();

                        ent.OFF_ID = Convert.ToInt32(dr.GetDecimal(dr.GetOrdinal("OFF_ID")));
                        ent.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME")).ToUpper();
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            oficna.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        lista.Add(ent);
                    }
                }
                return lista;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public SocioNegocio ObtenerSocioDocumento(string owner, decimal tipo, string nro_tipo)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            SocioNegocio socio = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SOCIO_X_DOCUMENTO_OFICINA"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@TAXT_ID", DbType.Decimal, tipo);
                    oDataBase.AddInParameter(cm, "@TAX_ID", DbType.String, nro_tipo);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            socio = new SocioNegocio();
                            socio.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            {
                                socio.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                            {
                                socio.BPS_FIRST_NAME = dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                            {
                                socio.BPS_FATH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                            {
                                socio.BPS_MOTH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("TAXT_ID")))
                            {
                                socio.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("TAXN_NAME")))
                            {
                                socio.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            {
                                socio.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return socio;
        }


        public int EliminarOficinaDir(string owner, decimal offId, decimal addId, string userUpd)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASD_DIRECCION_OFF");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, offId);
            oDatabase.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, addId);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, userUpd);

            int n = oDatabase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BEOffices ObtenerNombre(string owner, decimal off_id)
        {
            BEOffices oficina = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("Usp_Get_Office_by_OFF_ID");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Int32, off_id);
            
            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                if (reader.Read())
                {
                    oficina = new BEOffices();
                    oficina.OFF_NAME = Convert.ToString(reader["OFF_NAME"]);
                }
            }
            return oficina;
        }

        public int ActualizarAgenteprincipal(string owner,decimal idOficina,decimal idAgente)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_AGENTE_PRINCIPAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idAgente);            
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        #region VALIDA_OFI
        public int ValidaOficina(string owner, int oficina)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_VALIDA_OFI");
            int ofi=0;
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Int16, oficina);
            oDatabase.AddOutParameter(oDbCommand, "@OFF_ID_RETURN", DbType.Int16, ofi);

            int r = oDatabase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@OFF_ID_RETURN"));
            return id;
        }

        public int ValidaEmisionMensualOficina(int oficina)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_VALIDA_EMISION_MENSUAL");
            oDatabase.AddInParameter(oDbCommand, "@oficina", DbType.Int16, oficina);
            int r = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return r;
        }

        #endregion
    }
}
