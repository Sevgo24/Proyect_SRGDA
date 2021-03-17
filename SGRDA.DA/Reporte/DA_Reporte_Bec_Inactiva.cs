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
    public class DA_Reporte_Bec_Inactiva
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");
        public List<Be_Bec_Inactivas> Listar_Becs_Inactivas(decimal LIC_ID, decimal BPS_ID, decimal INV_ID,string Serie,decimal nro,
            decimal Bec_id ,string Fini_Rechazo,string Ffin_Rechazo,decimal oficina_id)
        {
            List<Be_Bec_Inactivas> lista = new List<Be_Bec_Inactivas>();
            Be_Bec_Inactivas item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("BEC_INACTIVAS"))
            {
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, LIC_ID);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, BPS_ID);
                oDataBase.AddInParameter(cm, "@INV_ID", DbType.Decimal, INV_ID);
                oDataBase.AddInParameter(cm, "@SERIE", DbType.String, Serie);
                oDataBase.AddInParameter(cm, "@NRO", DbType.Decimal, nro);
                oDataBase.AddInParameter(cm, "@BEC_ID", DbType.Decimal, Bec_id);
                oDataBase.AddInParameter(cm, "@FECHA_RECHAZO_INI", DbType.String, Fini_Rechazo);
                oDataBase.AddInParameter(cm, "@FECHA_RECHAZO_FIN", DbType.String, Ffin_Rechazo);
                oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, oficina_id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {

                      
                        item = new Be_Bec_Inactivas();
                        if (!dr.IsDBNull(dr.GetOrdinal("BEC_ID")))
                            item.BEC_ID = dr.GetDecimal(dr.GetOrdinal("BEC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            item.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NRO")))
                            item.NRO = dr.GetDecimal(dr.GetOrdinal("NRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_EMISION")))
                            item.FECHA_EMISION = dr.GetDateTime(dr.GetOrdinal("FECHA_EMISION")).ToString("dd'/'MM'/'yyyy");
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_FACT")))
                            item.ESTADO_FACT = dr.GetString(dr.GetOrdinal("ESTADO_FACT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_RECHAZO")))
                            item.FECHA_RECHAZO = dr.GetString(dr.GetOrdinal("FECHA_RECHAZO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOTIVO")))
                            item.MOTIVO = dr.GetString(dr.GetOrdinal("MOTIVO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_INACTIVACION")))
                            item.FECHA_INACTIVACION = dr.GetDateTime(dr.GetOrdinal("FECHA_INACTIVACION")).ToString("dd'/'MM'/'yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("SITUACION")))
                            item.SITUACION = dr.GetString(dr.GetOrdinal("SITUACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BANCO_ORIGEN")))
                            item.BANCO_ORIGEN = dr.GetString(dr.GetOrdinal("BANCO_ORIGEN"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_DEPOSITO")))
                            item.FECHA_DEPOSITO = dr.GetDateTime(dr.GetOrdinal("FECHA_DEPOSITO")).ToString("dd'/'MM'/'yyyy");

                        if (!dr.IsDBNull(dr.GetOrdinal("NRO_OPERACION")))
                            item.NRO_OPERACION = dr.GetString(dr.GetOrdinal("NRO_OPERACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_DEPOSITO")))
                            item.MONTO_DEPOSITO = dr.GetDecimal(dr.GetOrdinal("MONTO_DEPOSITO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BANCO_DESTINO")))
                            item.BANCO_DESTINO = dr.GetString(dr.GetOrdinal("BANCO_DESTINO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CUENTA_BANCARIA")))
                            item.CUENTA_BANCARIA = dr.GetString(dr.GetOrdinal("CUENTA_BANCARIA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFICINA")))
                            item.OFICINA = dr.GetString(dr.GetOrdinal("OFICINA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESC_SOLICITUD")))
                            item.DESC_SOLICITUD = dr.GetString(dr.GetOrdinal("DESC_SOLICITUD"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

    }
}
