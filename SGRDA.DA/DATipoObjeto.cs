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
    public class DATipoObjeto
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BETipoObjeto> Listar_Page_TipoObjeto(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPO_OBJETO");
            oDataBase.AddInParameter(oDbCommand, "@WRKF_OTDESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_BLOQUEOS", parametro, st, pagina, cantRegxPag, ParameterDirection.Output);
            var lista = new List<BETipoObjeto>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BETipoObjeto(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BETipoObjeto> Obtener(string owner, decimal? id)
        {
            List<BETipoObjeto> lst = new List<BETipoObjeto>();
            BETipoObjeto Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_OBJETO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@WRKF_OTID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BETipoObjeto();
                        Obj.WRKF_OTID = dr.GetDecimal(dr.GetOrdinal("WRKF_OTID"));
                        Obj.WRKF_OTDESC = dr.GetString(dr.GetOrdinal("WRKF_OTDESC")).ToUpper();
                        Obj.WRKF_OPREF = dr.GetString(dr.GetOrdinal("WRKF_OPREF")).ToUpper();
                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        public int Insertar(BETipoObjeto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_TIPO_OBJETO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_OTDESC", DbType.String, en.WRKF_OTDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@WRKF_OPREF", DbType.String, en.WRKF_OPREF.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Actualizar(BETipoObjeto en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPO_OBJETO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_OTID", DbType.Decimal, en.WRKF_OTID);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_OTDESC", DbType.String, en.WRKF_OTDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@WRKF_OPREF", DbType.String, en.WRKF_OPREF.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Eliminar(BETipoObjeto del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TIPO_OBJETO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_OTID", DbType.Decimal, del.WRKF_OTID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public bool existeTipoObjeto(string Owner, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_EXISTE_TIPO_OBJETO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@WRKF_OTDESC", DbType.String, nombre);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool existePrefijo(string Owner, string prefijo)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_EXISTE_PREFIJO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@WRKF_OPREF", DbType.String, prefijo);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool existeTipoObjeto(string Owner, decimal id, string nombre)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_UPDATE_EXISTE_TIPO_OBJETO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@WRKF_OTDESC", DbType.String, nombre);
                oDataBase.AddInParameter(cm, "@WRKF_OTID", DbType.Decimal, id);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }

        public bool existePrefijo(string Owner, decimal id, string prefijo)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_UPDATE_EXISTE_PREFIJO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@WRKF_OPREF", DbType.String, prefijo);
                oDataBase.AddInParameter(cm, "@WRKF_OTID", DbType.Decimal, id);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));
            }
            return existe;
        }
    }
}
