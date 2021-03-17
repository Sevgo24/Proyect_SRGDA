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
    public class DAREF_DIVISIONES_VALUES
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEREF_DIVISIONES_VALUES> USP_BUSCAR_UBIGEO(decimal id, string name)
        {
            List<BEREF_DIVISIONES_VALUES> lst = new List<BEREF_DIVISIONES_VALUES>();
            BEREF_DIVISIONES_VALUES item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("USP_BUSCAR_UBIGEO"))
                {
                    db.AddInParameter(cm, "@TIS_N", DbType.Decimal, id);
                    db.AddInParameter(cm, "@DAD_VNAME", DbType.String, name);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEREF_DIVISIONES_VALUES();
                            item.TIS_N = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                            item.DAD_VNAME = dr.GetString(dr.GetOrdinal("TEXT"));
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

        public List<BEREF_DIVISIONES_VALUES> Listar(string owner, decimal id,decimal subId,decimal depId,string nombre ,int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_VALORES");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, id);
            db.AddInParameter(oDbCommand, "@SUBDIVISION", DbType.Decimal, subId);
            db.AddInParameter(oDbCommand, "@DEPENDENCIA", DbType.Decimal, depId);
            db.AddInParameter(oDbCommand, "@NOMBRE", DbType.String, nombre);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEREF_DIVISIONES_VALUES>();
            BEREF_DIVISIONES_VALUES div;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    div = new BEREF_DIVISIONES_VALUES();
                    div.DADV_ID = dr.GetDecimal(dr.GetOrdinal("DADV_ID"));
                    div.DAD_BELONGS = dr.GetDecimal(dr.GetOrdinal("DAD_BELONGS"));
                    div.NOMBRE = dr.GetString(dr.GetOrdinal("NOMBRE")).ToUpper();
                    div.SUBDIVISION = dr.GetString(dr.GetOrdinal("SUBDIVISION")).ToUpper();
                    div.DEPENDENCIA = dr.GetString(dr.GetOrdinal("DEPENDENCIA")).ToUpper();
                    div.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(div);
                }
            }
            return lista;
        }

        public int insertar(BEREF_DIVISIONES_VALUES values)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_DIVISION_VALORES");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, values.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, values.DAD_ID);
            db.AddInParameter(oDbCommand, "@DAD_STYPE", DbType.String, values.DAD_STYPE);
            db.AddInParameter(oDbCommand, "@DAD_VCODE", DbType.String, values.DAD_VCODE.ToUpper());
            db.AddInParameter(oDbCommand, "@DAD_VNAME", DbType.String, values.DAD_VNAME.ToUpper());
            db.AddInParameter(oDbCommand, "@DAD_BELONGS", DbType.Decimal, values.DAD_BELONGS);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, values.LOG_USER_CREAT);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEREF_DIVISIONES_VALUES> ListarValoresDep(string  owner,decimal id,decimal subdivision)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_DIVISIONES_VALORES_DEP");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@DAD_ID", DbType.Decimal, id);
            oDataBase.AddInParameter(oDbComand, "@DAD_STYPE", DbType.Decimal, subdivision);

            var lista = new List<BEREF_DIVISIONES_VALUES>();
            BEREF_DIVISIONES_VALUES obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEREF_DIVISIONES_VALUES();
                    obs.DADV_ID = dr.GetDecimal(dr.GetOrdinal("DADV_ID"));
                    obs.DAD_VNAME = dr.GetString(dr.GetOrdinal("DAD_VNAME"));
                    lista.Add(obs);
                }
            }
            return lista;
        }

        public int Eliminar(BEREF_DIVISIONES_VALUES valores)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_DIVISIONES_VALORES");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, valores.OWNER);
            db.AddInParameter(oDbCommand, "@DADV_ID", DbType.Decimal, valores. DADV_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, valores.LOG_USER_UPDATE);
            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int ObtenerXAbrev(BEREF_DIVISIONES_VALUES valores)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_DIVISION_VALOR_ABREV");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, valores.OWNER);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, valores.DAD_ID);
            db.AddInParameter(oDbCommand, "@DAD_VCODE", DbType.String, valores.DAD_VCODE);
            BESociedad obj = new BESociedad();
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        public List<BETreeview> ArbolValoresReporte(string owner, decimal id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_VALORES_DEP");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@DAD_ID", DbType.Decimal, id);

            var lista = new List<BETreeview>();
            BETreeview obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BETreeview();
                    obs.cod = Convert.ToInt32(dr["DADV_ID"]);
                    obs.text = Convert.ToString(dr["NOMBRE"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("DAD_BELONGS")))
                        obs.ManagerID = Convert.ToInt32(dr["DAD_BELONGS"]);
                    else
                        obs.ManagerID = 0;
                    lista.Add(obs);
                }
            }
            return lista;
        }
        
        public List<BEREF_DIVISIONES_VALUES> ListarValoresXSubdivision(string owner, decimal id, decimal subdivision)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_VALORES_X_SUBDIVISION");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@DAD_ID", DbType.Decimal, id);
            oDataBase.AddInParameter(oDbComand, "@DAD_STYPE", DbType.Decimal, subdivision);

            var lista = new List<BEREF_DIVISIONES_VALUES>();
            BEREF_DIVISIONES_VALUES obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEREF_DIVISIONES_VALUES();
                    obs.DADV_ID = dr.GetDecimal(dr.GetOrdinal("DADV_ID"));
                    obs.DAD_VNAME = dr.GetString(dr.GetOrdinal("DAD_VNAME"));
                    lista.Add(obs);
                }
            }
            return lista;
        }

        public List<BEREF_DIVISIONES_VALUES> ListarDivisionesValor(decimal id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_DIVISIONES_VALOR");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbComand, "@DAD_STYPE", DbType.Decimal, id);

            var lista = new List<BEREF_DIVISIONES_VALUES>();
            BEREF_DIVISIONES_VALUES obs;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    obs = new BEREF_DIVISIONES_VALUES();
                    obs.DADV_ID = dr.GetDecimal(dr.GetOrdinal("DADV_ID"));
                    obs.DAD_VNAME = dr.GetString(dr.GetOrdinal("DAD_VNAME"));
                    lista.Add(obs);
                }
            }
            return lista;
        }

        public BEREF_DIVISIONES_VALUES ObtenerValor(string owner, decimal id)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_VALOR_DIV");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbComand, "@DADV_ID", DbType.Decimal, id);

            BEREF_DIVISIONES_VALUES valor=new BEREF_DIVISIONES_VALUES ();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbComand))
            {
                while (dr.Read())
                {
                    valor = new BEREF_DIVISIONES_VALUES();
                    valor.DADV_ID = dr.GetDecimal(dr.GetOrdinal("DADV_ID"));
                    valor.DAD_VNAME = dr.GetString(dr.GetOrdinal("DAD_VNAME"));
                }
            }
            return valor;
        }

    }
}
