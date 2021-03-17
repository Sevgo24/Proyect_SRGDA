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
    public class DAGrupoFacturacion
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEGrupoFacturacion> Listar_Page_Grupo_Facturacion(decimal UserDer, string GrupoMod, string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_GRUPO_FACTURACION");
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, UserDer);
            db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, GrupoMod);
            db.AddInParameter(oDbCommand, "@param", DbType.String, parametro);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEGrupoFacturacion>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEGrupoFacturacion(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEGrupoFacturacion> GET_REC_LIC_FACT_GROUP_X_USU_MOD(decimal idSocio, decimal idGrupoFac, string owner)
        {
            List<BEGrupoFacturacion> lst = new List<BEGrupoFacturacion>();
            BEGrupoFacturacion item = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_GRUPOFACTURACION"))
            {
                db.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idSocio);
                db.AddInParameter(cm, "@INVG_ID", DbType.Decimal, idGrupoFac);
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEGrupoFacturacion();
                        item.INVG_ID = dr.GetDecimal(dr.GetOrdinal("INVG_ID"));
                        item.INVG_DESC = dr.GetString(dr.GetOrdinal("INVG_DESC"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public int InsertarXModId(string OWNER, string INVG_DESC, decimal BPS_ID, decimal MOD_ID, string USER)
        {
            
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASI_GRUPOFACTURACION_X_MODALIDAD"))
            {
                db.AddInParameter(cm, "@BPS_ID", DbType.Decimal, BPS_ID);
                db.AddInParameter(cm, "@INVG_DESC", DbType.String, INVG_DESC);
                db.AddInParameter(cm, "@OWNER", DbType.String, OWNER);
                db.AddInParameter(cm, "@MOD_ID", DbType.Decimal, MOD_ID);
                db.AddInParameter(cm, "@USER", DbType.String, USER);

                int r = db.ExecuteNonQuery(cm);
                return r;
            }
        }

        public List<BEGrupoFacturacion> Obtener(string owner, decimal id)
        {
            List<BEGrupoFacturacion> lst = new List<BEGrupoFacturacion>();
            BEGrupoFacturacion Obj = null;

            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_GRUPO_FACTURACION"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@INVG_ID", DbType.Decimal, id);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEGrupoFacturacion();
                        Obj.INVG_ID = dr.GetDecimal(dr.GetOrdinal("INVG_ID"));
                        Obj.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        Obj.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME")).ToUpper();
                        Obj.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID")).ToUpper();
                        Obj.INVG_DESC = dr.GetString(dr.GetOrdinal("INVG_DESC")).ToUpper();
                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        public int Insertar(BEGrupoFacturacion en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_GRUPO_FACTURACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, en.MOG_ID.ToUpper());
            db.AddInParameter(oDbCommand, "@INVG_DESC", DbType.String, en.INVG_DESC.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);

            int n = db.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));
            return n;
        }

        public int Actualizar(BEGrupoFacturacion en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_GRUPO_FACTURACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, en.INVG_ID);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
            db.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, en.MOG_ID.ToUpper());
            db.AddInParameter(oDbCommand, "@INVG_DESC", DbType.String, en.INVG_DESC.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT.ToUpper());

            int n = db.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Eliminar(BEGrupoFacturacion del)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_GRUPO_FACTURACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, del.INVG_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, del.LOG_USER_UPDAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
