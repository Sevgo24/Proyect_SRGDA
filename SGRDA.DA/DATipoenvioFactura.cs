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
    public class DATipoenvioFactura
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETipoenvioFactura> ListarPaginacion(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPOENVIOFACTURA_PAGE");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_TIPOENVIOFACTURA_PAGE", owner, param, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BETipoenvioFactura>();
            //var Tipoenviofactura = new BETipoenvioFactura();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                        lista.Add(new BETipoenvioFactura(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public int Eliminar(BETipoenvioFactura TipoenvioFactura)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_INACTIVAR_TIPOENVIOFACTURA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, TipoenvioFactura.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@LIC_SEND", DbType.String, TipoenvioFactura.LIC_SEND);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, TipoenvioFactura.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Insertar(BETipoenvioFactura TipoenvioFactura)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPOENVIOFACTURA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, TipoenvioFactura.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@LIC_FSEND", DbType.String, TipoenvioFactura.LIC_FSEND.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, TipoenvioFactura.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public BETipoenvioFactura Obtener(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPOENVIOFACTURA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_SEND", DbType.String, id);

            BETipoenvioFactura ent = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    ent = new BETipoenvioFactura();
                    ent.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    ent.LIC_SEND = dr.GetDecimal(dr.GetOrdinal("LIC_SEND"));
                    ent.LIC_FSEND = dr.GetString(dr.GetOrdinal("LIC_FSEND"));
                }
            }
            return ent;
        }

        public int Actualizar(BETipoenvioFactura TipoenvioFactura)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPOENVIOFACTURA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, TipoenvioFactura.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@LIC_SEND", DbType.String, TipoenvioFactura.LIC_SEND);
            oDataBase.AddInParameter(oDbCommand, "@LIC_FSEND", DbType.String, TipoenvioFactura.LIC_FSEND.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, TipoenvioFactura.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }



        public int ObtenerXDescripcion(BETipoenvioFactura TipoenvioFactura)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPOENVIOFACTURA_DESC");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, TipoenvioFactura.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@LIC_FSEND", DbType.String, TipoenvioFactura.LIC_FSEND);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BETipoenvioFactura> Listar(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_TIPO_ENVIO_FAC");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
   
            var lista = new List<BETipoenvioFactura>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                BETipoenvioFactura ent = new BETipoenvioFactura();
                while (dr.Read())
                {
                    ent = new BETipoenvioFactura();
                    ent.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    ent.LIC_SEND = dr.GetDecimal(dr.GetOrdinal("LIC_SEND"));
                    ent.LIC_FSEND = dr.GetString(dr.GetOrdinal("LIC_FSEND"));
                    lista.Add(ent);
                }
            }
            return lista;
        }
    }
}
