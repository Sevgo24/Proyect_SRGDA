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
    public class DACaracteristicaPredefinida
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BECaracteristicaPredefinida> Listar_Page_CaracteristicaPredefinida(decimal tipo, decimal? subtipo, int st, int pagina, int cantRegxPag)
        {
            if (subtipo == null) { subtipo = 0; }
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_CARACTERISTICA_PREDEFINIDA");
            oDataBase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, tipo);
            oDataBase.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, subtipo);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BECaracteristicaPredefinida>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BECaracteristicaPredefinida(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public BECaracteristicaPredefinida Obtener(string owner, decimal id, decimal idTipoEsta, decimal idSubTipoEst)
        {
            BECaracteristicaPredefinida Obj = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CARACTERISTICA_PREDEFINIDA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CHAR_TYPES_ID", DbType.Decimal, id);
                oDataBase.AddInParameter(cm, "@EST_ID", DbType.Decimal, idTipoEsta);
                oDataBase.AddInParameter(cm, "@SUBE_ID", DbType.Decimal, idSubTipoEst);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BECaracteristicaPredefinida();
                        Obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        Obj.CHAR_TYPES_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_TYPES_ID"));
                        Obj.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                        Obj.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        Obj.SUBE_ID = dr.GetDecimal(dr.GetOrdinal("SUBE_ID"));
                    }
                }
            }
            return Obj;
        }

        public List<BECaracteristicaPredefinida> ObtenerCaracteristica(string owner, decimal id)
        {
            List<BECaracteristicaPredefinida> lst = new List<BECaracteristicaPredefinida>();
            BECaracteristicaPredefinida Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_CARACTERISTICA_PREDEFINIDA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CHAR_TYPES_ID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BECaracteristicaPredefinida();
                        Obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        Obj.CHAR_TYPES_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_TYPES_ID"));
                        Obj.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                        Obj.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        Obj.SUBE_ID = dr.GetDecimal(dr.GetOrdinal("SUBE_ID"));
                        lst.Add(Obj);
                    }
                }
            }

            return lst;
        }

        public List<BECaracteristicaPredefinida> Listar(string owner, decimal tipo, decimal sub)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_CARACTERISTICAS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, tipo);
            oDataBase.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, sub);
            oDataBase.ExecuteNonQuery(oDbCommand);

            List<BECaracteristicaPredefinida> lista = new List<BECaracteristicaPredefinida>();
            BECaracteristicaPredefinida detalle = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    detalle = new BECaracteristicaPredefinida();
                    detalle.CHAR_TYPES_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_TYPES_ID"));
                    detalle.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                    detalle.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                    detalle.SUBE_ID = dr.GetDecimal(dr.GetOrdinal("SUBE_ID"));
                    detalle.TIPO = dr.GetString(dr.GetOrdinal("TIPO"));
                    detalle.SUBTIPO = dr.GetString(dr.GetOrdinal("SUBTIPO"));
                    detalle.CHAR_SHORT = dr.GetString(dr.GetOrdinal("CHAR_SHORT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        detalle.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));

                    detalle.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    detalle.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        detalle.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        detalle.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));

                    lista.Add(detalle);
                }

            }
            return lista;
        }

        public int Insertar(BECaracteristicaPredefinida en)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_CARACTERISTICA_PREDEFINIDA");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(oDbCommand, "@CHAR_ID", DbType.Decimal, en.CHAR_ID);
                oDataBase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
                oDataBase.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, en.SUBE_ID);
                oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
                int n = oDataBase.ExecuteNonQuery(oDbCommand);
                return n;
            }catch(Exception ex){
                return 0;
            }
        }

        public int Actualizar(BECaracteristicaPredefinida en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_CARACTERISTICA_PREDEFINIDA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CHAR_TYPES_ID", DbType.Decimal, en.CHAR_TYPES_ID);
            oDataBase.AddInParameter(oDbCommand, "@CHAR_ID", DbType.Decimal, en.CHAR_ID);
            oDataBase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
            oDataBase.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, en.SUBE_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BECaracteristicaPredefinida del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_CARACTERISTICA_PREDEFINIDA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CHAR_TYPES_ID", DbType.Decimal, del.CHAR_TYPES_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(BECaracteristicaPredefinida del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVAR_CARACTERISTICA_PREDEFINIDA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CHAR_TYPES_ID", DbType.Decimal, del.CHAR_TYPES_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
