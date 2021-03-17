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
    public class DAOficinaDivision
    {
        //OFICINA COMERCIAL - DIVISION
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public decimal InsertarDivAdm(BEDivisionRecaudador recaudador)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_OFICINA_DIV_ADM");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, recaudador.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, recaudador.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, recaudador.DAD_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, recaudador.LOG_USER_CREAT);
            decimal IdOficinaDivAdm = Convert.ToDecimal(oDataBase.ExecuteScalar(oDbCommand));
            return IdOficinaDivAdm;
        }
        public List<BEDivisionRecaudador> ObtenerDivAdm(decimal IdOficina, string owner)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_OFICINA_DIV_ADM");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, IdOficina);
            var lista = new List<BEDivisionRecaudador>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEDivisionRecaudador item = null;
                while (dr.Read())
                {
                    item = new BEDivisionRecaudador();
                    item.ID_COLL_DIV = dr.GetDecimal(dr.GetOrdinal("ID_COLL_DIV"));
                    item.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                    item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                    item.DIV_DESCRIPTION = dr.GetString(dr.GetOrdinal("DIV_DESCRIPTION"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lista.Add(item);
                }
            }
            return lista;
        }
        public BEDivisionRecaudador Obtener(string owner, decimal IdOficina, decimal idDivAdmOfi)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_OFICINA_DIV_ADM_ID");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, IdOficina);
            oDataBase.AddInParameter(oDbCommand, "@ID_COLL_DIV", DbType.Decimal, idDivAdmOfi);
            BEDivisionRecaudador item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEDivisionRecaudador();
                    item.ID_COLL_DIV = dr.GetDecimal(dr.GetOrdinal("ID_COLL_DIV"));
                    item.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                    item.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                    item.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                    item.DIV_DESCRIPTION = dr.GetString(dr.GetOrdinal("DIV_DESCRIPTION"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return item;
        }
        public int Activar(string owner, decimal idOficina, decimal idDivAdmOfi, string usuario)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_DIV_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
            oDataBase.AddInParameter(oDbCommand, "@ID_COLL_DIV", DbType.Decimal, idDivAdmOfi);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuario);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public int Eliminar(string owner, decimal idOficina, decimal idDivAdmOfi, string usuario)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_DIV_OFICINA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
            oDataBase.AddInParameter(oDbCommand, "@ID_COLL_DIV", DbType.Decimal, idDivAdmOfi);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuario);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public List<BEDivisionRecaudador> ObtenerDivXOficina_Deplegable(decimal IdOficina, string owner)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_DIVISIONES_OFICINA_DESP");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, IdOficina);
            var lista = new List<BEDivisionRecaudador>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEDivisionRecaudador item = null;
                while (dr.Read())
                {
                    item = new BEDivisionRecaudador();
                    item.ID_COLL_DIV = dr.GetDecimal(dr.GetOrdinal("ID"));
                    item.DIV_DESCRIPTION = dr.GetString(dr.GetOrdinal("DIVISION"));
                    lista.Add(item);
                }
            }
            return lista;
        }


    }
}
