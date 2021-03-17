using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities.Reporte;
using SGRDA.Entities;

namespace SGRDA.DA
{
    public class DAAgenteRecaudo
    {
        public decimal Insertar(BEAgenteRecaudo entidad)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_AGENTE_RECAUDO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, entidad.OWNER);
            oDataBase.AddInParameter(oDbComand, "@OFF_ID", DbType.Decimal, entidad.OFF_ID);
            oDataBase.AddInParameter(oDbComand, "@DAD_ID", DbType.Decimal, entidad.DAD_ID);
            oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, entidad.BPS_ID);
            oDataBase.AddInParameter(oDbComand, "@ROL_ID", DbType.Decimal, entidad.ROL_ID);
            oDataBase.AddInParameter(oDbComand, "@START", DbType.DateTime, entidad.START);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT);
            decimal n = Convert.ToDecimal(oDataBase.ExecuteScalar(oDbComand));
            return n;
        }

        public decimal Actualizar(BEAgenteRecaudo entidad)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_AGENTE_RECAUDO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, entidad.OWNER);
            oDataBase.AddInParameter(oDbComand, "@COLL_OFF_ID", DbType.Decimal, entidad.COLL_OFF_ID);
            oDataBase.AddInParameter(oDbComand, "@OFF_ID", DbType.Decimal, entidad.OFF_ID);
            oDataBase.AddInParameter(oDbComand, "@DAD_ID", DbType.Decimal, entidad.DAD_ID);
            oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Decimal, entidad.BPS_ID);
            oDataBase.AddInParameter(oDbComand, "@ROL_ID", DbType.Decimal, entidad.ROL_ID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, entidad.LOG_USER_UPDAT);
            oDataBase.AddInParameter(oDbComand, "@START", DbType.DateTime, entidad.START);
            oDataBase.AddInParameter(oDbComand, "@ENDS", DbType.DateTime, entidad.ENDS);
            decimal n = Convert.ToDecimal(oDataBase.ExecuteNonQuery(oDbComand));
            return n;
        }

        public List<BEAgenteRecaudo> ListarAgenteRecaudoXDivision(string owner,decimal idOficina,decimal idDivision,string agenteRecaudador, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_AGENTES_RECAUDO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
            oDataBase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, idDivision);
            oDataBase.AddInParameter(oDbCommand, "@RECAUDADOR", DbType.String, agenteRecaudador);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEAgenteRecaudo>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                BEAgenteRecaudo recaudador;
                while (dr.Read())
                {
                    recaudador = new BEAgenteRecaudo();
                    recaudador.COLL_OFF_ID = dr.GetDecimal(dr.GetOrdinal("COLL_OFF_ID"));
                    recaudador.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                    recaudador.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                    recaudador.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    recaudador.RECAUDADOR = dr.GetString(dr.GetOrdinal("RECAUDADOR"));
                    recaudador.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    recaudador.DIVISION = dr.GetString(dr.GetOrdinal("DIVISION"));
                    recaudador.ROL_ID = dr.GetDecimal(dr.GetOrdinal("ROL_ID"));
                    recaudador.ROL = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("F_INICIAL")))
                        recaudador.F_INICIAL = dr.GetString(dr.GetOrdinal("F_INICIAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("F_FINAL")))
                        recaudador.F_FINAL = dr.GetString(dr.GetOrdinal("F_FINAL"));
                    recaudador.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    recaudador.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    recaudador.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(recaudador);
                }
            }
            return lista;
        }

        public BEAgenteRecaudo Obtener(BEAgenteRecaudo agente)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_AGENTES_RECAUDO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, agente.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@COLL_OFF_ID", DbType.Decimal, agente.COLL_OFF_ID);
            BEAgenteRecaudo ent = new BEAgenteRecaudo();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                dr.Read();
                ent.COLL_OFF_ID = dr.GetDecimal(dr.GetOrdinal("COLL_OFF_ID"));
                ent.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                ent.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                ent.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                ent.RECAUDADOR = dr.GetString(dr.GetOrdinal("RECAUDADOR"));
                ent.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                ent.DIVISION = dr.GetString(dr.GetOrdinal("DIVISION"));
                ent.ROL_ID = dr.GetDecimal(dr.GetOrdinal("ROL_ID"));
                ent.ROL = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                ent.F_INICIAL = dr.GetString(dr.GetOrdinal("F_INICIAL"));
                ent.F_FINAL = dr.GetString(dr.GetOrdinal("F_FINAL"));
            }
            return ent;
        }

        //Licencia - Validación ubigeo
        public List<BEAgenteRecaudo> Obtener_Division_Modalidad_Agente(string usuario)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            List<BEAgenteRecaudo> ListaDato = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_AGENTE_DIVISION_MODALIDAD"))
                {
                    oDataBase.AddInParameter(cm, "@USUARIO", DbType.String, usuario);
                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        BEAgenteRecaudo dato = null;
                        ListaDato = new List<BEAgenteRecaudo>();
                        while (dr.Read())
                        {
                            dato = new BEAgenteRecaudo();
                            dato.DIV_RiGHTS_ID = dr.GetDecimal(dr.GetOrdinal("DIV_RiGHTS_ID"));
                            dato.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                            dato.ID_COLL_DIV = dr.GetDecimal(dr.GetOrdinal("ID_COLL_DIV"));
                            dato.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                            dato.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                            dato.ROL_ID = dr.GetDecimal(dr.GetOrdinal("ROL_ID"));
                            dato.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                            ListaDato.Add(dato);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ListaDato;
        }


        public List<BEAgenteRecaudo> ListarAgenteRecaudoXDivisionObligatorio(string owner, decimal idOficina, decimal idDivision, string agenteRecaudador)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_AGENTES_RECAUDO_OBLIGATORIO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
            oDataBase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, idDivision);
            oDataBase.AddInParameter(oDbCommand, "@RECAUDADOR", DbType.String, agenteRecaudador);
            oDataBase.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEAgenteRecaudo>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                BEAgenteRecaudo recaudador;
                while (dr.Read())
                {
                    recaudador = new BEAgenteRecaudo();
                    recaudador.COLL_OFF_ID = dr.GetDecimal(dr.GetOrdinal("COLL_OFF_ID"));
                    recaudador.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                    recaudador.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                    recaudador.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    recaudador.RECAUDADOR = dr.GetString(dr.GetOrdinal("RECAUDADOR"));
                    recaudador.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    recaudador.DIVISION = dr.GetString(dr.GetOrdinal("DIVISION"));
                    recaudador.ROL_ID = dr.GetDecimal(dr.GetOrdinal("ROL_ID"));
                    recaudador.ROL = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("F_INICIAL")))
                        recaudador.F_INICIAL = dr.GetString(dr.GetOrdinal("F_INICIAL"));
                    if (!dr.IsDBNull(dr.GetOrdinal("F_FINAL")))
                        recaudador.F_FINAL = dr.GetString(dr.GetOrdinal("F_FINAL"));
                    recaudador.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    recaudador.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    lista.Add(recaudador);
                }
            }
            return lista;
        }


    }
}
