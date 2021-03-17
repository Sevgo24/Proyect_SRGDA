using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DAOrigenComision
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEOrigenComision> ListarPage(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_COMISION_ORIG");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@COM_DESC", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEOrigenComision>();
            var item = new BEOrigenComision();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEOrigenComision();
                    item.COM_ORG = dr.GetDecimal(dr.GetOrdinal("COM_ORG"));
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

        public List<BEOrigenComision> Listar(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_ORIGEN_COMISION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            List<BEOrigenComision> lista = null;

            BEOrigenComision item = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                lista = new List<BEOrigenComision>();

                while (dr.Read())
                {
                    item = new BEOrigenComision();
                    item.COM_ORG = dr.GetDecimal(dr.GetOrdinal("COM_ORG"));
                    item.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Eliminar(BEOrigenComision en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_COMISION_ORIG");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@COM_ORG", DbType.String, en.COM_ORG);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ValidacionOrigenComision(BEOrigenComision en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_VALIDACION_ORG_COMISION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@COM_DESC", DbType.String, en.COM_DESC.ToUpper().Trim());
            oDataBase.AddInParameter(oDbCommand, "@COM_ORG", DbType.String, en.COM_ORG);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public int Insertar(BEOrigenComision en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_COMISION_ORIG");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@COM_DESC", DbType.String, en.COM_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEOrigenComision en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_COMISION_ORIG");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@COM_ORG", DbType.Decimal, en.COM_ORG);
            oDataBase.AddInParameter(oDbCommand, "@COM_DESC", DbType.String, en.COM_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEOrigenComision Obtener(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_COMISION_ORIG");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@COM_ORG", DbType.Decimal, id);

            BEOrigenComision item = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEOrigenComision();

                    item.COM_ORG = dr.GetDecimal(dr.GetOrdinal("COM_ORG"));
                    item.COM_DESC = dr.GetString(dr.GetOrdinal("COM_DESC")).ToUpper();
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                }
            }
            return item;
        }
    }
}
