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
    public class DARedSocialType
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");

        public List<BERedSocialType> Listar_Page_TipRedSocial(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_TIPO_REDSOCIAL");
            oDataBase.AddInParameter(oDbCommand, "@CONT_TDESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BERedSocialType>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BERedSocialType(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BERedSocialType> ListarTipoRedes(string Owner)
        {
            List<BERedSocialType> lst = new List<BERedSocialType>();
            BERedSocialType item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_TIPOREDES"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BERedSocialType();
                            item.CONT_TYPE = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                            item.CONT_TDESC = dr.GetString(dr.GetOrdinal("TEXT"));
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

        public int Insertar(BERedSocialType doc)
        {

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_TIPO_REDES_SOCIALES");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, doc.OWNER);
            oDataBase.AddInParameter(oDbComand, "@CONT_TDESC", DbType.String, doc.CONT_TDESC.ToUpper());
            oDataBase.AddInParameter(oDbComand, "@CONT_OBSERV", DbType.String, doc.CONT_OBSERV != null ? doc.CONT_OBSERV.ToUpper() : string.Empty);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, doc.LOG_USER_CREAT.ToUpper());
            int n = oDataBase.ExecuteNonQuery(oDbComand);
            return n;

        }

        public int Actualizar(BERedSocialType par)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_TIPO_REDES_SOCIALES");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, par.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONT_TYPE", DbType.Int32, par.CONT_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@CONT_TDESC", DbType.String, par.CONT_TDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CONT_OBSERV", DbType.String, par.CONT_OBSERV != null ? par.CONT_OBSERV.ToUpper() : string.Empty);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, par.LOG_USER_UPDATE.ToUpper());
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        //public BERedSocialType Obtener(string owner, decimal idtipo)
        //{

        //    BERedSocialType Obj = null;

        //    using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_REDSOCIAL"))
        //    {
        //        oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
        //        oDataBase.AddInParameter(cm, "@CONT_TYPE", DbType.Decimal, idtipo);

        //        using (IDataReader dr = oDataBase.ExecuteReader(cm))
        //        {
        //            while (dr.Read())
        //            {
        //                Obj = new BERedSocialType();
        //                Obj.CONT_TYPE = dr.GetDecimal(dr.GetOrdinal("CONT_TYPE"));
        //                Obj.CONT_TDESC = dr.GetString(dr.GetOrdinal("CONT_TDESC"));
        //                Obj.CONT_OBSERV = dr.GetString(dr.GetOrdinal("CONT_OBSERV"));

        //                if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
        //                {
        //                    Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
        //                }
        //            }
        //        }
        //    }

        //    return Obj;
        //}

        public List<BERedSocialType> Obtener(string owner, decimal idtipo)
        {
            List<BERedSocialType> lst = new List<BERedSocialType>();
            BERedSocialType Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_REDSOCIAL"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@CONT_TYPE", DbType.Decimal, idtipo);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BERedSocialType();
                        Obj.CONT_TYPE = dr.GetDecimal(dr.GetOrdinal("CONT_TYPE"));
                        Obj.CONT_TDESC = dr.GetString(dr.GetOrdinal("CONT_TDESC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("CONT_OBSERV")))
                        {
                            Obj.CONT_OBSERV = dr.GetString(dr.GetOrdinal("CONT_OBSERV"));
                        }
                        lst.Add(Obj);
                    }
                }
            }

            return lst;
        }

        public int Eliminar(BERedSocialType del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_TIPO_REDSOCIAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONT_TYPE", DbType.String, del.CONT_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
