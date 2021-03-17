using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.DA.Reporte
{
    public class DAREPORTE_DE_LICENCIAS_NUEVAS
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");
        public List<BE_REPORTE_DE_LICENCIAS_NUEVAS> ObtenerDatosREPORTE_DE_LICENCIAS_NUEVAS(string finicio, string ffin, int ID_SOCIO, string ID_MODALIDAD, int ID_OFICINA, int Estado)
        {
            BE_REPORTE_DE_LICENCIAS_NUEVAS item = null;
            List<BE_REPORTE_DE_LICENCIAS_NUEVAS> lista = new List<BE_REPORTE_DE_LICENCIAS_NUEVAS>();

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDA_REPORTE_DE_LICENCIAS_NUEVAS"))
            {
                oDataBase.AddInParameter(cm, "@FECHAINI", DbType.String, finicio);
                oDataBase.AddInParameter(cm, "@FECHAFIN", DbType.String, ffin);
                oDataBase.AddInParameter(cm, "@ID_SOCIO", DbType.Int32, ID_SOCIO);
                oDataBase.AddInParameter(cm, "@ID_MODALIDAD", DbType.String, ID_MODALIDAD);
                oDataBase.AddInParameter(cm, "@ID_OFICINA", DbType.Int32, ID_OFICINA);
                oDataBase.AddInParameter(cm, "@ESTADO", DbType.Int32, Estado);



                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {


                        item = new BE_REPORTE_DE_LICENCIAS_NUEVAS();
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("RepresentanteLegal")))
                            item.RepresentanteLegal = dr.GetString(dr.GetOrdinal("RepresentanteLegal"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TELEFONO")))
                            item.TELEFONO = dr.GetString(dr.GetOrdinal("TELEFONO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CORREO")))
                            item.CORREO = dr.GetString(dr.GetOrdinal("CORREO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            item.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CREACION_LICENCIA")))
                            item.FECHA_CREACION_LICENCIA = dr.GetString(dr.GetOrdinal("FECHA_CREACION_LICENCIA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTABLECIMIENTO")))
                            item.ESTABLECIMIENTO = dr.GetString(dr.GetOrdinal("ESTABLECIMIENTO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                            item.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TD")))
                            item.TD = dr.GetString(dr.GetOrdinal("TD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NRO")))
                            item.NRO = dr.GetDecimal(dr.GetOrdinal("NRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                            item.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_EMISION")))
                            item.FECHA_EMISION = dr.GetString(dr.GetOrdinal("FECHA_EMISION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                            item.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                            item.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTN")))
                            item.INVL_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            item.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOG_DESC")))
                            item.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NORO")))
                            item.NORO = dr.GetString(dr.GetOrdinal("NORO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                            item.UBIGEO = dr.GetString(dr.GetOrdinal("UBIGEO"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

    }

}
