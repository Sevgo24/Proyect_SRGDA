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
    public class DAAgente
    {

        public List<BEAgente> ListarAgentes(string owner, string param, int st, int pagina, int cantRegxPag)
        {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_AGENTES");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@LEVEL", DbType.String, param);
                oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
                oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
                oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
                oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
                oDataBase.ExecuteNonQuery(oDbCommand);

                string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

                //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
                //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_AGENTES", owner, param, st, pagina, cantRegxPag, ParameterDirection.Output);

                var lista = new List<BEAgente>();
                var agente = new BEAgente();
                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        agente = new BEAgente();
                        agente.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));

                        agente.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION")).ToUpper();

                        if (dr.IsDBNull(dr.GetOrdinal("LEVEL_DEP")))
                            agente.LEVEL_DEP = 0;
                        else
                            agente.LEVEL_DEP = dr.GetDecimal(dr.GetOrdinal("LEVEL_DEP"));


                        if (dr.IsDBNull(dr.GetOrdinal("DEPENDENCIA")))
                            agente.DEPENDENCIA = "";
                        else
                            agente.DEPENDENCIA = dr.GetString(dr.GetOrdinal("DEPENDENCIA")).ToUpper();


                        if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            agente.ESTADO = "A";
                        else
                            agente.ESTADO = "I";
                        agente.TotalVirtual = Convert.ToInt32(results);
                        lista.Add(agente);
                    }
                }
                return lista;

            
        }


        public List<BETreeview> ListarArbol(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_AGENTES_ARBOL");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.ExecuteNonQuery(oDbCommand);

            var lista = new List<BETreeview>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                BETreeview ent;
                while (reader.Read())
                {
                    ent = new BETreeview();
                    ent.cod = Convert.ToInt32(reader["LEVEL_ID"]);
                    ent.text = Convert.ToString(reader["DESCRIPTION"]).ToUpper();
                    if (!reader.IsDBNull(reader.GetOrdinal("LEVEL_DEP")))
                        ent.ManagerID = Convert.ToInt32(reader["LEVEL_DEP"]);
                    else
                        ent.ManagerID = 0;
                    lista.Add(ent);
                }
            }
            return lista;
        }

        public int Eliminar(BEAgente agente)
        {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_AGENTE_ESTADO");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, agente.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Int32, agente.LEVEL_ID);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, agente.LOG_USER_UPDAT);

                int r = oDatabase.ExecuteNonQuery(oDbCommand);
                return r;
           
        }

        public List<BEAgente> ListarAgenteDependencia(string owner)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_LISTAR_AGENTE_DEP");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            List<BEAgente> lista;
            using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
            {
                BEAgente ent;
                lista = new List<BEAgente>();
                while (dr.Read())
                {
                    ent = new BEAgente();

                    ent.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                    ent.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION")).ToUpper();
                    lista.Add(ent);
                }
            }
            return lista;
        }

        public int Insertar(BEAgente agente)
        {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_AGENTE");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, agente.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, agente.DESCRIPTION.ToUpper());
                oDatabase.AddInParameter(oDbCommand, "@LEVEL_DEP", DbType.Decimal, agente.LEVEL_DEP);
                oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, agente.LOG_USER_CREAT.ToUpper());

                int n = oDatabase.ExecuteNonQuery(oDbCommand);

                return n;          

        }


        public List<BEAgente> ObtenerXDescripcion(BEAgente agente)
        {
                Database oDatabase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_AGENTE_DEP");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, agente.OWNER);
                oDatabase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, agente.DESCRIPTION.ToUpper());

                List<BEAgente> lista;
                using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
                {
                    BEAgente ent;
                    lista = new List<BEAgente>();
                    while (dr.Read())
                    {
                        ent = new BEAgente();

                        ent.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                        ent.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION")).ToUpper();
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            agente.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        lista.Add(ent);
                    }
                }
                return lista;

            
        }


        public BEAgente Obtener(string owner, decimal id)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_AGENTE");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, id);

            BEAgente ent = null;
            using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BEAgente();

                    ent.LEVEL_ID = dr.GetDecimal(dr.GetOrdinal("LEVEL_ID"));
                    ent.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION")).ToUpper();

                    if (dr.IsDBNull(dr.GetOrdinal("LEVEL_DEP")))
                        ent.LEVEL_DEP = 0;
                    else
                        ent.LEVEL_DEP = dr.GetDecimal(dr.GetOrdinal("LEVEL_DEP"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        ent.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return ent;
        }

        public int Actualizar(BEAgente agente)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_AGENTE");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, agente.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Decimal, agente.LEVEL_ID);
            oDatabase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, agente.DESCRIPTION.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@LEVEL_DEP", DbType.String, agente.LEVEL_DEP);

            int r = oDatabase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEAgente> ListarCombo(string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_LISTA_AGENTES");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.ExecuteNonQuery(oDbCommand);

            var lista = new List<BEAgente>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                BEAgente ent;
                while (reader.Read())
                {
                    ent = new BEAgente();
                    ent.LEVEL_ID = Convert.ToInt32(reader["LEVEL_ID"]);
                    ent.DESCRIPTION = Convert.ToString(reader["DESCRIPTION"]).ToUpper();
                    lista.Add(ent);
                }
            }
            return lista;
        }

    }
}


