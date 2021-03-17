using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.DA.Reporte;
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
    public class DAReporteArtistaDetallado
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");
        public List<BEReporteArtistaDetallado> ListarArtistaDetallado(string femi_ini, string femi_fin, string feve_ini, string feve_fin,string fcan_ini,string fcan_fin,string fcon_ini,string fcon_fin,string artista)
        {
            List<BEReporteArtistaDetallado> lista = new List<BEReporteArtistaDetallado>();
            BEReporteArtistaDetallado item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDA_LISTAR_REPORTE_ARTISTAS_DETALLADO"))
            {
                oDataBase.AddInParameter(cm, "@FECHA_EMISION_INI", DbType.String, femi_ini);
                oDataBase.AddInParameter(cm, "@FECHA_EMISION_FIN", DbType.String, femi_fin);
                oDataBase.AddInParameter(cm, "@FECHA_EVENTO_INI", DbType.String, feve_ini);
                oDataBase.AddInParameter(cm, "@FECHA_EVENTO_FIN", DbType.String, feve_fin);
                oDataBase.AddInParameter(cm, "@FECHA_CANCELACION_INI", DbType.String, fcan_ini);
                oDataBase.AddInParameter(cm, "@FECHA_CANCELACION_FIN", DbType.String, fcan_fin);
                oDataBase.AddInParameter(cm, "@FECHA_CONTABLE_INI", DbType.String, fcon_ini);
                oDataBase.AddInParameter(cm, "@FECHA_CONTABLE_FIN", DbType.String, fcon_fin);
                oDataBase.AddInParameter(cm, "@ARTISTA", DbType.String, artista);
                cm.CommandTimeout = 120;
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEReporteArtistaDetallado();
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EVENTO")))
                            item.EVENTO = dr.GetString(dr.GetOrdinal("EVENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                            item.SOCIO = dr.GetString(dr.GetOrdinal("SOCIO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NRO_DOCUMENTO")))
                            item.NRO_DOCUMENTO = dr.GetString(dr.GetOrdinal("NRO_DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_AUTORIZACION_INI")))
                            item.FECHA_AUTORIZACION_INI = dr.GetString(dr.GetOrdinal("FECHA_AUTORIZACION_INI"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_AUTORIZACION_FIN")))
                            item.FECHA_AUTORIZACION_FIN = dr.GetString(dr.GetOrdinal("FECHA_AUTORIZACION_FIN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE_ESTABLECIMIENTO")))
                            item.NOMBRE_ESTABLECIMIENTO = dr.GetString(dr.GetOrdinal("NOMBRE_ESTABLECIMIENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("UBIGEO")))
                            item.UBIGEO = dr.GetString(dr.GetOrdinal("UBIGEO"));

                        //Detalle Show
                        if (!dr.IsDBNull(dr.GetOrdinal("CANTIDAD_ARTISTAS")))
                            item.CANTIDAD_ARTISTAS = dr.GetInt32(dr.GetOrdinal("CANTIDAD_ARTISTAS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ID_GRUPO")))
                            item.ID_GRUPO = dr.GetString(dr.GetOrdinal("ID_GRUPO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("GRUPO")))
                            item.GRUPO = dr.GetString(dr.GetOrdinal("GRUPO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ID_MOD")))
                            item.ID_MOD = dr.GetDecimal(dr.GetOrdinal("ID_MOD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
                            item.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CODIGO_SAP")))
                            item.CODIGO_SAP = dr.GetString(dr.GetOrdinal("CODIGO_SAP"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CANTIDAD_ARTISTA_SHOW")))
                            item.CANTIDAD_ARTISTA_SHOW = dr.GetInt32(dr.GetOrdinal("CANTIDAD_ARTISTA_SHOW"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ID_SHOW")))
                            item.ID_SHOW = dr.GetDecimal(dr.GetOrdinal("ID_SHOW"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SHOW_NAME")))
                            item.SHOW_NAME = dr.GetString(dr.GetOrdinal("SHOW_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SHOW_START")))
                            item.SHOW_START = dr.GetString(dr.GetOrdinal("SHOW_START"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SHOW_ENDS")))
                            item.SHOW_ENDS = dr.GetString(dr.GetOrdinal("SHOW_ENDS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ARTISTA_ID")))
                            item.ARTISTA_ID = dr.GetDecimal(dr.GetOrdinal("ARTISTA_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ARTISTA")))
                            item.ARTISTA = dr.GetString(dr.GetOrdinal("ARTISTA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ARTISTA_ID_SGS")))
                            item.ARTISTA_ID_SGS = dr.GetDecimal(dr.GetOrdinal("ARTISTA_ID_SGS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_ARTISTA")))
                            item.ESTADO_ARTISTA = dr.GetString(dr.GetOrdinal("ESTADO_ARTISTA"));

                        //DetalleFactura
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            item.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FACTURA")))
                            item.FACTURA = dr.GetString(dr.GetOrdinal("FACTURA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO_DOCUMENTO")))
                            item.TIPO_DOCUMENTO = dr.GetString(dr.GetOrdinal("TIPO_DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NUMERO")))
                            item.NUMERO = dr.GetString(dr.GetOrdinal("NUMERO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FACTURA")))
                            item.FACTURA = dr.GetString(dr.GetOrdinal("FACTURA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_EMISION")))
                            item.FECHA_EMISION = dr.GetString(dr.GetOrdinal("FECHA_EMISION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CANCELACION")))
                            item.FECHA_CANCELACION = dr.GetString(dr.GetOrdinal("FECHA_CANCELACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CONTABLE")))
                            item.FECHA_CONTABLE = dr.GetString(dr.GetOrdinal("FECHA_CONTABLE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_ANULLADO")))
                            item.FECHA_ANULLADO = dr.GetString(dr.GetOrdinal("FECHA_ANULLADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONEDA")))
                            item.MONEDA = dr.GetString(dr.GetOrdinal("MONEDA"));   
                        if (!dr.IsDBNull(dr.GetOrdinal("FACTURADO")))
                            item.FACTURADO = dr.GetDecimal(dr.GetOrdinal("FACTURADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RECAUDADO")))
                            item.RECAUDADO = dr.GetDecimal(dr.GetOrdinal("RECAUDADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PENDIENTE")))
                            item.PENDIENTE = dr.GetDecimal(dr.GetOrdinal("PENDIENTE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            item.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO"));

                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public List<BEArtistas> ListaArtista(string Artista)
        {
            List<BEArtistas> lista = new List<BEArtistas>();
            BEArtistas item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("LISTA_ARTISTAS"))
            {
                oDataBase.AddInParameter(cm, "@ARTISTA", DbType.String, Artista);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEArtistas();
                        if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                            item.Artista = dr.GetString(dr.GetOrdinal("NAME"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
    }
}
