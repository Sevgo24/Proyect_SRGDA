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
    public class DA_ReporteEstablecimiento
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");
        public List<BE_Reporte_Establecimiento> ListarDatosEstablecimiento(string MOG_ID,int id_socio, int id_departamento, int id_provincia
            , int id_distrito, int id_est)
        {
            List<BE_Reporte_Establecimiento> lista = new List<BE_Reporte_Establecimiento>();
            BE_Reporte_Establecimiento item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("TQR_ESTABLECIMIENTOS"))
            {
                oDataBase.AddInParameter(cm, "@MOG_ID", DbType.String, MOG_ID);
                oDataBase.AddInParameter(cm, "@ID_SOCIO", DbType.Int32, id_socio);
                oDataBase.AddInParameter(cm, "@ID_DEPARTAMENTO", DbType.Int32, id_departamento);
                oDataBase.AddInParameter(cm, "@ID_PROVINCIA", DbType.Int32, id_provincia);
                oDataBase.AddInParameter(cm, "@ID_DISTRITO", DbType.Int32, id_distrito);
                oDataBase.AddInParameter(cm, "@ID_EST", DbType.Int32, id_est);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BE_Reporte_Establecimiento();
                        if (!dr.IsDBNull(dr.GetOrdinal("ID_SOCIO")))
                            item.ID_SOCIO = dr.GetDecimal(dr.GetOrdinal("ID_SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            item.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO_DOCUMENTO")))
                            item.TIPO_DOCUMENTO = dr.GetString(dr.GetOrdinal("TIPO_DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NRO_DOCUMENTO")))
                            item.NRO_DOCUMENTO = dr.GetString(dr.GetOrdinal("NRO_DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GRUPO")))
                            item.GRUPO = dr.GetString(dr.GetOrdinal("GRUPO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
                            item.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LICENCIA")))
                            item.LICENCIA = dr.GetString(dr.GetOrdinal("LICENCIA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CREACION_LICENCIA")))
                            item.FECHA_CREACION_LICENCIA = dr.GetString(dr.GetOrdinal("FECHA_CREACION_LICENCIA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTABLECIMIENTO")))
                            item.ESTABLECIMIENTO = dr.GetString(dr.GetOrdinal("ESTABLECIMIENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO")))
                            item.TIPO = dr.GetString(dr.GetOrdinal("TIPO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SUB_TIPO")))
                            item.SUB_TIPO = dr.GetString(dr.GetOrdinal("SUB_TIPO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                            item.DIRECCION = dr.GetString(dr.GetOrdinal("DIRECCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                            item.UBIGEO = dr.GetString(dr.GetOrdinal("UBIGEO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DEPARTAMENTO")))
                            item.DEPARTAMENTO = dr.GetString(dr.GetOrdinal("DEPARTAMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PROVINCIA")))
                            item.PROVINCIA = dr.GetString(dr.GetOrdinal("PROVINCIA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO")))
                            item.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TD")))
                            item.TD = dr.GetString(dr.GetOrdinal("TD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FEC_EMI_FACTURA")))
                            item.FEC_EMI_FACTURA = dr.GetString(dr.GetOrdinal("FEC_EMI_FACTURA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONEDA")))
                            item.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO")))
                            item.NUMERO = dr.GetDecimal(dr.GetOrdinal("NUMERO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IMPORTE")))
                            item.IMPORTE = dr.GetDecimal(dr.GetOrdinal("IMPORTE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RECAUDADO")))
                            item.RECAUDADO = dr.GetDecimal(dr.GetOrdinal("RECAUDADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FEC_CANCELACION")))
                            item.FEC_CANCELACION = dr.GetString(dr.GetOrdinal("FEC_CANCELACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ANIO_ULTIMA_FACTURA")))
                            item.ANIO_ULTIMA_FACTURA = dr.GetInt32(dr.GetOrdinal("ANIO_ULTIMA_FACTURA"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

    }
}
