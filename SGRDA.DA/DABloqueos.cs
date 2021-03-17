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
    public class DABloqueos
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEBloqueos> Listar(string owner)
        {
            List<BEBloqueos> lst = new List<BEBloqueos>();
            BEBloqueos Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_BLOQUEOS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEBloqueos();
                        Obj.BLOCK_ID = dr.GetDecimal(dr.GetOrdinal("BLOCK_ID"));
                        Obj.BLOCK_DESC = dr.GetString(dr.GetOrdinal("BLOCK_DESC")).ToUpper();
                        Obj.BLOCK_PULL = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BLOCK_PULL")));
                        Obj.BLOCK_AUT = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BLOCK_AUT")));

                        lst.Add(Obj);
                    }
                }
                return lst;
            }
        }

        public List<BEBloqueos> Listar_Page_Bloqueos(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_BLOQUEOS");
            oDataBase.AddInParameter(oDbCommand, "@BLOCK_DESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEBloqueos>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEBloqueos(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEBloqueos> Obtener(string owner, decimal id)
        {
            List<BEBloqueos> lst = new List<BEBloqueos>();
            BEBloqueos Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_BLOQUEOS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@BLOCK_ID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEBloqueos();
                        Obj.BLOCK_ID = dr.GetDecimal(dr.GetOrdinal("BLOCK_ID"));
                        Obj.BLOCK_DESC = dr.GetString(dr.GetOrdinal("BLOCK_DESC")).ToUpper();
                        Obj.BLOCK_PULL = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BLOCK_PULL")));
                        Obj.BLOCK_AUT = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BLOCK_AUT")));

                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        public int Insertar(BEBloqueos en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_BLOQUEOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BLOCK_DESC", DbType.String, en.BLOCK_DESC.ToString().ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@BLOCK_PULL", DbType.String, en.BLOCK_PULL.ToString().ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@BLOCK_AUT", DbType.String, en.BLOCK_AUT.ToString().ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEBloqueos en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_BLOQUEOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BLOCK_ID", DbType.Decimal, en.BLOCK_ID);
            oDataBase.AddInParameter(oDbCommand, "@BLOCK_DESC", DbType.String, en.BLOCK_DESC.ToString().ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@BLOCK_PULL", DbType.String, en.BLOCK_PULL.ToString().ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@BLOCK_AUT", DbType.String, en.BLOCK_AUT.ToString().ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BEBloqueos del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_BLOQUEOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BLOCK_ID", DbType.Decimal, del.BLOCK_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public bool existeTipoBloqueo(string Owner, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_EXISTE_TIPO_BLOQUEO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@BLOCK_DESC", DbType.String, nombre);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }
    }
}
