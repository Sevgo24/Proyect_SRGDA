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
    public class DATipoDescuento
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETipoDescuento> Listar_Page_TipoDescuento(string owner, string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPO_DESCUENTO");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@DISC_TYPE_NAME", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BETipoDescuento>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BETipoDescuento(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BETipoDescuento> Obtener(string owner, decimal id)
        {
            List<BETipoDescuento> lst = new List<BETipoDescuento>();
            BETipoDescuento Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_DESCUENTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DISC_TYPE", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETipoDescuento();
                        Obj.DISC_TYPE = dr.GetDecimal(dr.GetOrdinal("DISC_TYPE"));
                        Obj.DISC_TYPE_NAME = dr.GetString(dr.GetOrdinal("DISC_TYPE_NAME")).ToUpper();
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        public List<BETipoDescuento> ListarTipoDescuento(string Owner)
        {
            List<BETipoDescuento> lst = new List<BETipoDescuento>();
            BETipoDescuento item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPODESCUENTO"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BETipoDescuento();
                            item.DISC_TYPE = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                            item.DISC_TYPE_NAME = dr.GetString(dr.GetOrdinal("TEXT"));
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public int ValidacionTipoDescuento(BETipoDescuento en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_VALIDACION_TIPO_DESCUENTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DISC_TYPE_NAME", DbType.String, en.DISC_TYPE_NAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@DISC_TYPE", DbType.Decimal, en.DISC_TYPE);
            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }

        public int Insertar(BETipoDescuento en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPO_DESCUENTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DISC_TYPE_NAME", DbType.String, en.DISC_TYPE_NAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BETipoDescuento en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPO_DESCUENTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DISC_TYPE", DbType.Decimal, en.DISC_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@DISC_TYPE_NAME", DbType.String, en.DISC_TYPE_NAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Eliminar(BETipoDescuento del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TIPO_DESCUENTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DISC_TYPE", DbType.Decimal, del.DISC_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
