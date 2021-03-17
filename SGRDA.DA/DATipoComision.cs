using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using SGRDA.Entities;
using System.Data.Common;
using System.Data;

namespace SGRDA.DA
{
    public class DATipoComision
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETipoComision> ListarPaginacion(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPOCOMISION");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_TIPOCOMISION", owner, param, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BETipoComision>();
            var item = new BETipoComision();

            using (IDataReader dr = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (dr.Read())
                {
                    item = new BETipoComision();
                    item.COMT_ID = dr.GetDecimal(dr.GetOrdinal("COMT_ID"));
                    item.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";
                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Eliminar(BETipoComision en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPOCOMISION_INACTIVAR");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, en.COMT_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Insertar(BETipoComision en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPOCOMISION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@COM_DESC", DbType.String, en.COM_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BETipoComision Obtener(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPOCOMISION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, id);

            BETipoComision ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                dr.Read();
                ent = new BETipoComision();
                ent.COMT_ID = dr.GetDecimal(dr.GetOrdinal("COMT_ID"));
                ent.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC"));
            }
            return ent;
        }

        public BETipoComision ObtenerDescripcion(string owner, string desc)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_TIPOCOMISION_DESC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@COM_DESC", DbType.String, desc);

            BETipoComision ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                dr.Read();
                ent = new BETipoComision();
                ent.COMT_ID = dr.GetDecimal(dr.GetOrdinal("COMT_ID"));
                ent.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC"));
            }
            return ent;
        }

        public List<BETipoComision> Listar(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_TIPOCOMISION_REPORT");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            List<BETipoComision> lista = null;

            BETipoComision item = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                lista = new List<BETipoComision>();

                while (dr.Read())
                {
                    item = new BETipoComision();
                    item.COMT_ID = dr.GetDecimal(dr.GetOrdinal("COMT_ID"));
                    item.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Actualizar(BETipoComision en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPOCOMISION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@COMT_ID", DbType.Decimal, en.COMT_ID);
            oDataBase.AddInParameter(oDbCommand, "@COM_DESC", DbType.String, en.COM_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
