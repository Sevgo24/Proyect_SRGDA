using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities;
using System.Data.Common;
using System.Data;

namespace SGRDA.DA
{
    public class DAGrupoModalidadOficina
    {

        public List<BEGrupoModalidadOficina> ListarGrupoModalidad(decimal idOficina, string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_GRUPO_MOD_OFICINA");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@OFF_ID", DbType.Decimal, idOficina);

            var lista = new List<BEGrupoModalidadOficina>();
            BEGrupoModalidadOficina modalidad;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    modalidad = new BEGrupoModalidadOficina();
                    modalidad.MODG_ID = dr.GetDecimal(dr.GetOrdinal("MODG_ID"));
                    modalidad.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                    modalidad.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                    modalidad.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    modalidad.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        modalidad.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        modalidad.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        modalidad.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(modalidad);
                }
            }
            return lista;
        }

        public int Insertar(BEGrupoModalidadOficina en)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_GRUPO_MOD_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, en.MOG_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BEGrupoModalidadOficina en)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_GRUPO_MOD_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@MODG_ID", DbType.String, en.MODG_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Activar(BEGrupoModalidadOficina en)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_GRUPO_MOD_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@MODG_ID", DbType.String, en.MODG_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEGrupoModalidadOficina en)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_GRUPO_MOD_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@MODG_ID", DbType.Decimal, en.MODG_ID);
            oDataBase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, en.MOG_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BEGrupoModalidadOficina Obtener(string owner, decimal idOficina, decimal idGMOfi)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_GRUPO_MOD_OFICINA");
            oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, idOficina);
            oDataBase.AddInParameter(cm, "@MODG_ID", DbType.Decimal, idGMOfi);

            BEGrupoModalidadOficina obj = null;
            using (IDataReader dr = oDataBase.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    obj = new BEGrupoModalidadOficina();
                    obj.MODG_ID = dr.GetDecimal(dr.GetOrdinal("MODG_ID"));
                    obj.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                    obj.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return obj;
        }



    }
}
