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
    public class DAOficinaDivisionModalidad
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public decimal InsertarDivMod(BEOficinaDivisionModalidad DivModalidad)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_OFICINA_DIV_ADM_MODALIDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, DivModalidad.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@ID_COLL_DIV", DbType.Decimal, DivModalidad.ID_COLL_DIV);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, DivModalidad.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, DivModalidad.DAD_ID);
            oDataBase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, DivModalidad.MOG_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, DivModalidad.LOG_USER_CREAT);
            decimal IdOficinaDivAdm = oDataBase.ExecuteNonQuery(oDbCommand);
            return IdOficinaDivAdm;
        }
        public List<BEOficinaDivisionModalidad> ObtenerListaDivMod(decimal IdOficina, string owner)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_OFICINA_DIV_GRUPO_MOD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, IdOficina);
            var lista = new List<BEOficinaDivisionModalidad>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEOficinaDivisionModalidad item = null;
                while (dr.Read())
                {
                    item = new BEOficinaDivisionModalidad();
                    item.DIV_RiGHTS_ID = dr.GetDecimal(dr.GetOrdinal("DIV_RiGHTS_ID"));
                    item.ID_COLL_DIV = dr.GetDecimal(dr.GetOrdinal("ID_COLL_DIV"));
                    item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    item.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                    item.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public BEOficinaDivisionModalidad Obtener(string owner, decimal IdOficina, decimal idDivModalidad)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_GRUPO_MOD_OFI_X_CODIGO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, IdOficina);
            oDataBase.AddInParameter(oDbCommand, "@DIV_RiGHTS_ID", DbType.Decimal, idDivModalidad);
            BEOficinaDivisionModalidad item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEOficinaDivisionModalidad();
                    item.DIV_RiGHTS_ID = dr.GetDecimal(dr.GetOrdinal("DIV_RiGHTS_ID"));
                    item.ID_COLL_DIV = dr.GetDecimal(dr.GetOrdinal("ID_COLL_DIV"));
                    item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    item.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                    item.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return item;
        }

        public int Activar(string owner, decimal idOficina, decimal idDivModalidad, string usuario)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_GRUPO_MOD_DIV_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
            oDataBase.AddInParameter(oDbCommand, "@DIV_RiGHTS_ID", DbType.Decimal, idDivModalidad);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuario);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(string owner, decimal idOficina, decimal idDivModalidad, string usuario)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_GRUPO_MOD_DIV_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
            oDataBase.AddInParameter(oDbCommand, "@DIV_RiGHTS_ID", DbType.Decimal, idDivModalidad);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuario);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

    }
}
