using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities.Empadronamiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.DA.Empadronamiento
{
    public class DADetalleEmpadronamiento
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");
        public List<BEDetalleEmpadronamiento> ObtenerLista_Matriz_Detalle_EMPADRONAMIENTO(decimal LIC_ID)
        {
            BEDetalleEmpadronamiento item = null;
            List<BEDetalleEmpadronamiento> lista = new List<BEDetalleEmpadronamiento>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("Matriz_EMP_Detalle"))
            {
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Int32, Convert.ToInt32(LIC_ID));
                cm.CommandTimeout = 3600;
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {


                        item = new BEDetalleEmpadronamiento();
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            item.INV_ID = dr.GetInt32(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TE")))
                            item.TE = dr.GetString(dr.GetOrdinal("TE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO")))
                            item.TIPO = dr.GetString(dr.GetOrdinal("TIPO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NRO")))
                            item.NRO = dr.GetInt32(dr.GetOrdinal("NRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_EMISION")))
                            item.FECHA_EMISION = dr.GetString(dr.GetOrdinal("FECHA_EMISION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CANCELACION")))
                            item.FECHA_CANCELACION = dr.GetString(dr.GetOrdinal("FECHA_CANCELACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IDENT")))
                            item.IDENT = dr.GetString(dr.GetOrdinal("IDENT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NRO_IDENT")))
                            item.NRO_IDENT = dr.GetString(dr.GetOrdinal("NRO_IDENT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            item.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONEDA")))
                            item.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FACTURADO")))
                            item.FACTURADO = dr.GetDecimal(dr.GetOrdinal("FACTURADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("COBRADO")))
                            item.COBRADO = dr.GetDecimal(dr.GetOrdinal("COBRADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SALDO")))
                            item.SALDO = dr.GetDecimal(dr.GetOrdinal("SALDO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            item.ESTADO = dr.GetInt32(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NC")))
                            item.NC = dr.GetInt32(dr.GetOrdinal("NC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SUNAT")))
                            item.ESTADO_SUNAT = dr.GetString(dr.GetOrdinal("ESTADO_SUNAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                            item.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
                        
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
        public string Nombre_x_Licencia(int LIC_ID)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("NOMBRE_X_LICENCIA");
            oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Int32, LIC_ID);
            string Nombre = "";
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {

                while (reader.Read())
                {
                    Nombre = Convert.ToString(reader["LIC_NAME"]);
                }
            }
            return Nombre;
        }
    }
}
