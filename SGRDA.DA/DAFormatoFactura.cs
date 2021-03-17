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
    public class DAFormatoFactura
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEFormatoFactura> Listar_Page_Formato_Factura(string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_FORMATO_FACTURA");
            oDataBase.AddInParameter(oDbCommand, "@INVF_DESC", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEFormatoFactura>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEFormatoFactura(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEFormatoFactura> Obtener(string owner, decimal id)
        {
            List<BEFormatoFactura> lst = new List<BEFormatoFactura>();
            BEFormatoFactura Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_FORMATO_FACTURA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@INVF_ID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEFormatoFactura();
                        Obj.INVF_ID = dr.GetDecimal(dr.GetOrdinal("INVF_ID"));
                        Obj.INVF_DESC = dr.GetString(dr.GetOrdinal("INVF_DESC")).ToUpper();

                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        public List<BEFormatoFactura> ListarFormatoFactura(string Owner)
        {
            List<BEFormatoFactura> lst = new List<BEFormatoFactura>();
            BEFormatoFactura item = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_FORMATO_FACTURA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEFormatoFactura();
                        item.INVF_ID = dr.GetDecimal(dr.GetOrdinal("VALUE"));
                        item.INVF_DESC = dr.GetString(dr.GetOrdinal("TEXT"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

        public int Insertar(BEFormatoFactura en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_FORMATO_FACTURA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@INVF_DESC", DbType.String, en.INVF_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Actualizar(BEFormatoFactura en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_FORMATO_FACTURA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@INVF_ID", DbType.Decimal, en.INVF_ID);
            oDataBase.AddInParameter(oDbCommand, "@INVF_DESC", DbType.String, en.INVF_DESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            //int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@ADD_ID"));

            return n;
        }

        public int Eliminar(BEFormatoFactura del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_FORMATO_FACTURA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@INVF_ID", DbType.Decimal, del.INVF_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEFormatoFactura> FormatoFacturacion(string Owner)
        {
            List<BEFormatoFactura> lst = new List<BEFormatoFactura>();
            BEFormatoFactura item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_FORMATO_IMPRESION"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEFormatoFactura();
                        item.INVF_ID = dr.GetDecimal(dr.GetOrdinal("INVF_ID"));
                        item.INVF_DESC = dr.GetString(dr.GetOrdinal("INVF_DESC"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }

    }
}
