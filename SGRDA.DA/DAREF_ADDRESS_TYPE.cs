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
    public class DAREF_ADDRESS_TYPE
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREF_ADDRESS_TYPE> ListarDirecciones()
        {
            List<BEREF_ADDRESS_TYPE> lst = new List<BEREF_ADDRESS_TYPE>();
            BEREF_ADDRESS_TYPE item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("usp_Get_REF_ADDRESS_TYPE"))
                {
                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_ADDRESS_TYPE();
                            item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            item.ADDT_ID = dr.GetDecimal(dr.GetOrdinal("ADDT_ID"));
                            item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
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

        public BEREF_ADDRESS_TYPE Obtener(string owner, decimal ADDT_ID)
        {
            BEREF_ADDRESS_TYPE item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("usp_REF_ADDRESS_TYPE_GET_by_ADDT_ID"))
            {
                oDataBase.AddInParameter(cm, "@ADDT_ID", DbType.Decimal, ADDT_ID);
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEREF_ADDRESS_TYPE();
                        item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        item.ADDT_ID = dr.GetDecimal(dr.GetOrdinal("ADDT_ID"));
                        item.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));
                        item.ADDT_OBSERV = dr.GetString(dr.GetOrdinal("ADDT_OBSERV"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }
            return item;
        }

        public List<BEREF_ADDRESS_TYPE> ListarPage(string param, int st, int pagina, int cantRegxPag)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_ADDRESS_TYPE_GET_Page");
            oDataBase.AddInParameter(oDbCommand, "@param", DbType.String, param);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREF_ADDRESS_TYPE>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEREF_ADDRESS_TYPE(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public int Insertar(BEREF_ADDRESS_TYPE en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_ADDRESS_TYPE_Ins");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@ADDT_OBSERV", DbType.String, en.ADDT_OBSERV != null ? en.ADDT_OBSERV.ToString().ToUpper() : en.ADDT_OBSERV);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEREF_ADDRESS_TYPE en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_ADDRESS_TYPE_Upd");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@ADDT_ID", DbType.Decimal, en.ADDT_ID);
            oDataBase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, en.DESCRIPTION.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@ADDT_OBSERV", DbType.String, en.ADDT_OBSERV != null ? en.ADDT_OBSERV.ToString().ToUpper() : en.ADDT_OBSERV);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, en.ESTADO);
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BEREF_ADDRESS_TYPE en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("usp_REF_ADDRESS_TYPE_Del");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@ADDT_ID", DbType.Decimal, en.ADDT_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="idDireccion"></param>
        /// <returns></returns>
        public BEREF_ADDRESS_TYPE Obtiene(string owner, decimal idTipoDireccion)
        {

            BEREF_ADDRESS_TYPE tipoDireccion = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("usp_REF_ADDRESS_TYPE_GET_by_ADDT_ID"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@ADDT_ID", DbType.Decimal, idTipoDireccion);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            tipoDireccion = new BEREF_ADDRESS_TYPE();
                            tipoDireccion.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                            tipoDireccion.ADDT_ID = dr.GetDecimal(dr.GetOrdinal("ADDT_ID"));
                            tipoDireccion.DESCRIPTION = dr.GetString(dr.GetOrdinal("DESCRIPTION"));

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tipoDireccion;
        }

        public bool existeTipoDirecciones(string Owner, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_EXISTE_TIPO_DIRECCIONES"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@DESCRIPTION", DbType.String, nombre);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool existeTipoDirecciones(string Owner, decimal id, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_UPDATE_EXISTE_TIPO_DIRECCIONES"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@DESCRIPTION", DbType.String, nombre);
                oDataBase.AddInParameter(cm, "@ADDT_ID", DbType.Decimal, id);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }
    }
}
