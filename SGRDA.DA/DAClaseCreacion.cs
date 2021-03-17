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
    public class DAClaseCreacion
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEClaseCreacion> Listar_Page_Clase_Creacion(string owner, string clas, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_CLASE_CREACION");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CLASS_DESC", DbType.String, clas);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEClaseCreacion>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEClaseCreacion(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public BEClaseCreacion Obtener(string owner, string nombre)
        {
            BEClaseCreacion Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CLASE_CREACION"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CLASS_COD", DbType.String, nombre);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEClaseCreacion();
                        Obj.CLASS_COD = dr.GetString(dr.GetOrdinal("CLASS_COD"));
                        Obj.CLASS_DESC = dr.GetString(dr.GetOrdinal("CLASS_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                }
            }

            return Obj;
        }

        public BEClaseCreacion ObtenerDetalle(string owner, string Id, string IdDerecho)
        {
            BEClaseCreacion Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DETALLE_DERECHO_CLASE"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CLASS_COD", DbType.String, Id);
                oDataBase.AddInParameter(cm, "@RIGHT_COD", DbType.String, IdDerecho);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEClaseCreacion();
                        Obj.SEQUENCE = dr.GetDecimal(dr.GetOrdinal("SEQUENCE"));
                        Obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        Obj.CLASS_COD = dr.GetString(dr.GetOrdinal("CLASS_COD"));
                        Obj.CLASS_DESC = dr.GetString(dr.GetOrdinal("CLASS_DESC"));
                        Obj.RIGHT_COD = dr.GetString(dr.GetOrdinal("RIGHT_COD"));
                        Obj.auxRIGHT_COD = dr.GetString(dr.GetOrdinal("auxRIGHT_COD"));
                        Obj.RIGHT_DESC = dr.GetString(dr.GetOrdinal("RIGHT_DESC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            Obj.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            Obj.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            Obj.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            Obj.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                }
            }

            return Obj;
        }

        public int Insertar(BEClaseCreacion en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_CREACION_CLASE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, en.CLASS_COD.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CLASS_DESC", DbType.String, en.CLASS_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@CLASS_COD"));
            return n;
        }

        public int InsertarDetalle(BEClaseCreacion det)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_DERECHO_CLASE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, det.RIGHT_COD);
            oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, det.CLASS_COD);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, det.LOG_USER_CREAT);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Actualizar(BEClaseCreacion en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_CLASE_CREACION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, en.CLASS_COD.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CLASS_DESC", DbType.String, en.CLASS_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int ActualizarDetalle(BEClaseCreacion en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_DERECHO_CLASE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@auxRIGHT_COD", DbType.String, en.auxRIGHT_COD);
            oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, en.CLASS_COD);
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, en.RIGHT_COD);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BEClaseCreacion del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_CLASE_CREACION");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, del.CLASS_COD);
            //oDataBase.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, del.SEQUENCE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(BEClaseCreacion del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_DERECHO_CLASE");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, del.CLASS_COD);
            oDataBase.AddInParameter(oDbCommand, "@SEQUENCE", DbType.Decimal, del.SEQUENCE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
