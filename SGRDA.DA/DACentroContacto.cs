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
    public class DACentroContacto
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BECentroContacto> ListarCentroContactoPage(string owner, decimal Idoficina, string dato, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_CONTACCENTER_PAGE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, Idoficina);
            oDataBase.AddInParameter(oDbCommand, "@CONC_NAME", DbType.String, dato);
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BECentroContacto>();
            var item = new BECentroContacto();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BECentroContacto();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_ID")))
                        item.CONC_ID = dr.GetDecimal(dr.GetOrdinal("CONC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_NAME")))
                        item.CONC_NAME = dr.GetString(dr.GetOrdinal("CONC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_DESC")))
                        item.CONC_DESC = dr.GetString(dr.GetOrdinal("CONC_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OFF_NAME")))
                        item.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
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

        public int Eliminar(BECentroContacto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAU_INACTIVAR_CENTROCONTACTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONC_ID", DbType.Decimal, en.CONC_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BECentroContacto Obtener(string owner, decimal Id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_DATOS_CENTROCONTACTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CONC_ID", DbType.Decimal, Id);

            var item = new BECentroContacto();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BECentroContacto();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_ID")))
                        item.CONC_ID = dr.GetDecimal(dr.GetOrdinal("CONC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_NAME")))
                        item.CONC_NAME = dr.GetString(dr.GetOrdinal("CONC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OFF_ID")))
                        item.OFF_ID = dr.GetDecimal(dr.GetOrdinal("OFF_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_DESC")))
                        item.CONC_DESC = dr.GetString(dr.GetOrdinal("CONC_DESC"));
                }
            }
            return item;
        }

        public int Insertar(BECentroContacto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAI_CENTROCONTACTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CONC_NAME", DbType.String, en.CONC_NAME == null ? string.Empty : en.CONC_NAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_DESC", DbType.String, en.CONC_DESC == null ? string.Empty : en.CONC_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BECentroContacto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAU_CENTROCONTACTOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CONC_ID", DbType.Decimal, en.CONC_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_NAME", DbType.String, en.CONC_NAME == null ? string.Empty : en.CONC_NAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, en.OFF_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_DESC", DbType.String, en.CONC_DESC == null ? string.Empty : en.CONC_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int ObtenerXDescripcion(BECentroContacto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_CENTROCONTACTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONC_NAME", DbType.String, en.CONC_NAME);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.String, en.OFF_ID);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BECentroContacto> ListaCentroContactos(string owner, decimal Idoficina, string Descripcion, int Estado)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_CENTROCONTACTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, Idoficina);
            oDataBase.AddInParameter(oDbCommand, "@CONC_NAME", DbType.String, Descripcion);
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, Estado);

            BECentroContacto item = null;
            List<BECentroContacto> lista = new List<BECentroContacto>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BECentroContacto();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_ID")))
                        item.CONC_ID = dr.GetDecimal(dr.GetOrdinal("CONC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_NAME")))
                        item.CONC_NAME = dr.GetString(dr.GetOrdinal("CONC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_DESC")))
                        item.CONC_DESC = dr.GetString(dr.GetOrdinal("CONC_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OFF_NAME")))
                        item.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));
                    item.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<BECentroContacto> ListarDropCentroContacto(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_CENTRO_CONTACTOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            BECentroContacto item = null;
            List<BECentroContacto> lista = new List<BECentroContacto>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BECentroContacto();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_ID")))
                        item.CONC_ID = dr.GetDecimal(dr.GetOrdinal("CONC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_NAME")))
                        item.CONC_NAME = dr.GetString(dr.GetOrdinal("CONC_NAME"));
                    lista.Add(item);
                }
            }
            return lista;
        }
    }
}
