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

    public class DALicenciaDivisionAgente
    {
        public int Insertar(BELicenciaDivisionAgente entidad)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_DIV_LICENCIA_AGENTE");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, entidad.OWNER);
            oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Decimal, entidad.LIC_ID);
            oDataBase.AddInParameter(oDbComand, "@DAD_ID", DbType.Decimal, entidad.DAD_ID);
            oDataBase.AddInParameter(oDbComand, "@COLL_OFF_ID", DbType.Decimal, entidad.COLL_OFF_ID);
            oDataBase.AddInParameter(oDbComand, "@OFF_ID", DbType.Decimal, entidad.OFF_ID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbComand);
            return n;
        }

        //public int Actualizar(BELicenciaDivisionAgente entidad)
        //{
        //    Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        //    DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASD_DIV_LICENCIA_AGENTE");
        //    oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, entidad.OWNER);
        //    oDataBase.AddInParameter(oDbComand, "@ID", DbType.Decimal, entidad.ID);
        //    oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Decimal, entidad.LIC_ID);
        //    oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, entidad.LOG_USER_UPDAT);
        //    int n = oDataBase.ExecuteNonQuery(oDbComand);
        //    return n;
        //}

        //public int Eliminar(BELicenciaDivisionAgente entidad)
        //{
        //    Database oDataBase = new DatabaseProviderFactory().Create("conexion");
        //    DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_DIV_LICENCIA_AGENTE");
        //    oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, entidad.OWNER);
        //    oDataBase.AddInParameter(oDbComand, "@ID", DbType.Decimal, entidad.ID);
        //    oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Decimal, entidad.LIC_ID);
        //    oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, entidad.LOG_USER_UPDAT);
        //    int n = oDataBase.ExecuteNonQuery(oDbComand);
        //    return n;
        //}

        public List<BELicenciaDivisionAgente> Listar(string owner, decimal idLicencia)
        {
            List<BELicenciaDivisionAgente> lista = new List<BELicenciaDivisionAgente>();
            BELicenciaDivisionAgente item = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DIV_LICENCIA_AGENTE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLicencia);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BELicenciaDivisionAgente();
                        item.ID = dr.GetDecimal(dr.GetOrdinal("ID"));
                        item.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                        item.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                        item.ROL_ID = dr.GetDecimal(dr.GetOrdinal("ROL_ID"));
                        item.ROL_DESC = dr.GetString(dr.GetOrdinal("ROL_DESC"));
                        item.AGENTE = dr.GetString(dr.GetOrdinal("AGENTE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        lista.Add(item);
                    }
                }
                return lista;
            }
        }
                
        public List<BELicenciaDivisionAgente> Obtener_Agente_X_Division(BELicenciaDivisionAgente DivisionAgente)
        {
            List<BELicenciaDivisionAgente> Lista = new List<BELicenciaDivisionAgente>();
            BELicenciaDivisionAgente item = null;
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_AGENTE_X_DIV_LICENCIA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, DivisionAgente.OWNER);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, DivisionAgente.LIC_ID);
                oDataBase.AddInParameter(cm, "@DAD_ID", DbType.Decimal, DivisionAgente.DAD_ID);
                oDataBase.AddInParameter(cm, "@COLL_OFF_ID", DbType.Decimal, DivisionAgente.COLL_OFF_ID);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BELicenciaDivisionAgente();
                        item.ID = dr.GetDecimal(dr.GetOrdinal("ID"));
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                        item.COLL_OFF_ID = dr.GetDecimal(dr.GetOrdinal("COLL_OFF_ID"));
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        Lista.Add(item);
                    }
                }
                return Lista;
            }
        }

        public int ActualizarEstado(BELicenciaDivisionAgente entidad)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_DIV_LICENCIA_AGENTE_ESTADO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, entidad.OWNER);
            oDataBase.AddInParameter(oDbComand, "@ID", DbType.Decimal, entidad.ID);
            oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Decimal, entidad.LIC_ID);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, entidad.LOG_USER_UPDAT);
            oDataBase.AddInParameter(oDbComand, "@INDICADOR", DbType.Decimal, entidad.INDICADOR);
            int resultado = Convert.ToInt32(oDataBase.ExecuteNonQuery(oDbComand));
            return resultado;
        }

    }
}
