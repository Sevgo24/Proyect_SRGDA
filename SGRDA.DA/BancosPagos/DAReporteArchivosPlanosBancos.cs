using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities.BancosPagos;
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
    public class DAReporteArchivosPlanosBancos
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");
        public List<BEArchivosPlanosBancos> ObtenerDatosArchivosPlanosBancos(string finicio, string ffin)
        {
            BEArchivosPlanosBancos item = null;
            List<BEArchivosPlanosBancos> lista = new List<BEArchivosPlanosBancos>();

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDA_REPORTE_DE_ARCHIVO_PLANO_BANCO"))
            {
                oDataBase.AddInParameter(cm, "@FECHAINI", DbType.String, finicio);
                oDataBase.AddInParameter(cm, "@FECHAFIN", DbType.String, ffin);



                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEArchivosPlanosBancos();
                        if (!dr.IsDBNull(dr.GetOrdinal("TR")))
                            item.TR = dr.GetString(dr.GetOrdinal("TR"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
        public List<BEArchivosPlanosBancos> ObtenerImporteTotal(string finicio, string ffin)
        {
            BEArchivosPlanosBancos item = null;
            List<BEArchivosPlanosBancos> lista = new List<BEArchivosPlanosBancos>();

            using (DbCommand cm = oDataBase.GetStoredProcCommand("OBTENER_IMPORTE_ARCHIVO_PLANO"))
            {
                oDataBase.AddInParameter(cm, "@FECHAINI", DbType.String, finicio);
                oDataBase.AddInParameter(cm, "@FECHAFIN", DbType.String, ffin);



                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEArchivosPlanosBancos();
                        if (!dr.IsDBNull(dr.GetOrdinal("IMPORTE_TOTAL")))
                            item.IMPORTE_TOTAL = dr.GetString(dr.GetOrdinal("IMPORTE_TOTAL"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }   
        public List<BEArchivosPlanosBancos> CargarArchivoPlano(string xml, int pagina, int cantRegxPag)
        {
            BEArchivosPlanosBancos item = null;            
            List<BEArchivosPlanosBancos> lista = new List<BEArchivosPlanosBancos>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SEPARAR_CAMPOS_ARCHIVOPLANO_CONTINENTAL"))
            {
                oDataBase.AddInParameter(cm, "@XML", DbType.Xml, xml);
                oDataBase.AddInParameter(cm, "@PageIndex", DbType.Int32, pagina);
                oDataBase.AddInParameter(cm, "@PageSize", DbType.Int32, cantRegxPag);
                oDataBase.AddOutParameter(cm, "@RecordCount", DbType.Int32, 50);
                #region Lista
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEArchivosPlanosBancos();
                        if (!dr.IsDBNull(dr.GetOrdinal("TR")))
                            item.TR = dr.GetString(dr.GetOrdinal("TR"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NOMBRE_CLIENTE")))
                            item.NOMBRE_CLIENTE = dr.GetString(dr.GetOrdinal("NOMBRE_CLIENTE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("IMPORTE_ORIGEN")))
                            item.IMPORTE_ORIGEN = dr.GetString(dr.GetOrdinal("IMPORTE_ORIGEN")).TrimStart('0');
                        if (!dr.IsDBNull(dr.GetOrdinal("IMPORTE_DEPOSITADO")))
                            item.IMPORTE_DEPOSITADO = dr.GetString(dr.GetOrdinal("IMPORTE_DEPOSITADO")).TrimStart('0');
                        if (!dr.IsDBNull(dr.GetOrdinal("IMPORTE_MORA")))
                            item.IMPORTE_MORA = dr.GetString(dr.GetOrdinal("IMPORTE_MORA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NORO_ID")))
                            item.NORO_ID = dr.GetString(dr.GetOrdinal("NORO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NRO_MOVIMIENTO")))
                            item.NRO_MOVIMIENTO = dr.GetString(dr.GetOrdinal("NRO_MOVIMIENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_PAGO")))
                            item.FECHA_PAGO = dr.GetString(dr.GetOrdinal("FECHA_PAGO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO_VALOR")))
                            item.TIPO_VALOR = dr.GetString(dr.GetOrdinal("TIPO_VALOR"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RUC")))
                            item.RUC = dr.GetString(dr.GetOrdinal("RUC")).TrimStart('0');
                        if (!dr.IsDBNull(dr.GetOrdinal("TD")))
                            item.TD = dr.GetString(dr.GetOrdinal("TD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NRO")))
                            item.NRO = dr.GetString(dr.GetOrdinal("NRO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CANAL_ENTRADA")))
                            item.CANAL_ENTRADA = dr.GetString(dr.GetOrdinal("CANAL_ENTRADA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CUENTA_DESTINO")))
                            item.CUENTA_DESTINO = dr.GetString(dr.GetOrdinal("CUENTA_DESTINO"));
                        item.TotalVirtual = dr.GetInt32(dr.GetOrdinal("CANTIDAD"));
                        lista.Add(item);
                    }
                }
            }
            #endregion

            return lista;

        }
        public List<BE_CargaArchivoPlano> ListaBancosPagos()
        {
            BE_CargaArchivoPlano item = null;
            List<BE_CargaArchivoPlano> lista = new List<BE_CargaArchivoPlano>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("BANCOS_PAGOS"))
            {
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BE_CargaArchivoPlano();
                        if (!dr.IsDBNull(dr.GetOrdinal("ID_VALUE")))
                            item.ID_VALUE = dr.GetDecimal(dr.GetOrdinal("ID_VALUE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("VTYPE")))
                            item.VTYPE = dr.GetString(dr.GetOrdinal("VTYPE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("VSUB_TYPE")))
                            item.VSUB_TYPE = dr.GetString(dr.GetOrdinal("VSUB_TYPE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("VDESC")))
                            item.VDESC = dr.GetString(dr.GetOrdinal("VDESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("VALUE")))
                            item.VALUE = dr.GetString(dr.GetOrdinal("VALUE"));

                        lista.Add(item);
                    }
                }
            }
            return lista;

        } 
        public List<BE_CargaArchivoPlano> ListaBancosxCuenta(string nroCuenta)
        {
            BE_CargaArchivoPlano item = null;
            List<BE_CargaArchivoPlano> lista = new List<BE_CargaArchivoPlano>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("RecuperarBancoXCuenta"))

            {
                oDataBase.AddInParameter(cm, "@nroCuenta", DbType.String, nroCuenta);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BE_CargaArchivoPlano();
                        if (!dr.IsDBNull(dr.GetOrdinal("BNK_ID")))
                            item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BNK_NAME")))
                            item.BNK_NAME = dr.GetString(dr.GetOrdinal("BNK_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ACC_ID")))
                            item.BPS_ACC_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ACC_ID"));
                        lista.Add(item);
                    }
                }
            }
            return lista;

        }
        public string CargarPagos(string xml, int idBank, int idCuenta,string User,int Id_FileCobro)
        {
            //BEArchivosPlanosBancos item = null;
            //List<BEArchivosPlanosBancos> lista = new List<BEArchivosPlanosBancos>();
             using (DbCommand cm = oDataBase.GetStoredProcCommand("INSERTAR_CABECERA_CONTINENTAL"))
            {
                //@Id_FileCobro
                try
                {
                    oDataBase.AddInParameter(cm, "@XMLDETALLE", DbType.Xml, xml);
                    oDataBase.AddInParameter(cm, "@IdBnk", DbType.Int32, idBank);
                    oDataBase.AddInParameter(cm, "@IdCuenta", DbType.Int32, idCuenta);
                    oDataBase.AddInParameter(cm, "@User", DbType.String, User);
                    oDataBase.AddInParameter(cm, "@Id_FileCobro", DbType.Int32, Id_FileCobro);
                    cm.CommandTimeout = 3600;
                    oDataBase.ExecuteNonQuery(cm);
                   
                    return "r";
                }
                catch(Exception)
                {
                    return "n";
                }
              
               
               

            }

        }
        public List<BEFileBanco> LISTAR_ARCHIVOS_CARGADOS(int bnk_id)
        {
            BEFileBanco item = null;
            List<BEFileBanco> lista = new List<BEFileBanco>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("USP_LISTAR_NAME_FILE"))
            {
                oDataBase.AddInParameter(cm, "@bnk_id", DbType.Int32, bnk_id);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEFileBanco();
                        if (!dr.IsDBNull(dr.GetOrdinal("FILE_COBRO_ID")))
                            item.FILE_COBRO_ID = dr.GetInt32(dr.GetOrdinal("FILE_COBRO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESC_FILE")))
                            item.DESC_FILE = dr.GetString(dr.GetOrdinal("DESC_FILE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BNK_ID")))
                            item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetString(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_DATE_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));

                        lista.Add(item);
                    }
                }
            }
            return lista;

        }
        public int InsertarArchivosCargados(string name_file, int idBank, int cant_cabeceras, decimal montocabecera ,string User, int Id_FileCobro )
        {
            //BEArchivosPlanosBancos item = null;
            //List<BEArchivosPlanosBancos> lista = new List<BEArchivosPlanosBancos>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("USP_INSERT_NAME_FILE"))
            {
                try
                {
                    oDataBase.AddInParameter(cm, "@NAME_FILE", DbType.String, name_file);
                    oDataBase.AddInParameter(cm, "@BNK_ID", DbType.Int32, idBank);
                    oDataBase.AddInParameter(cm, "@TOTAL_CABECERAS", DbType.Int32, cant_cabeceras);
                    oDataBase.AddInParameter(cm, "@TOTAL", DbType.Decimal, montocabecera);
                    oDataBase.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, User);
                    oDataBase.AddOutParameter(cm, "@Id_FileCobro", DbType.Int32, Id_FileCobro);                    
                    int n = oDataBase.ExecuteNonQuery(cm);
                    int idFileCobro = Convert.ToInt32(oDataBase.GetParameterValue(cm, "@Id_FileCobro"));
                    return idFileCobro;
                }
                catch(Exception)
                {
                    return 0;
                }
             

              

            }

        }
        public string InsertarArchivoGenerado(string DESC_ARC, int idBank, string FECHA_INI, string FECHA_FIN,int CANT_ARC, decimal monto, string User)
        {
            string resultado = "Ok";
            //BEArchivosPlanosBancos item = null;
            //List<BEArchivosPlanosBancos> lista = new List<BEArchivosPlanosBancos>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("USP_INSERT_GENERAR_ARCHIVOS"))
            {
                oDataBase.AddInParameter(cm, "@DESC_ARC", DbType.String, DESC_ARC);
                oDataBase.AddInParameter(cm, "@BNK_ID", DbType.Int32, idBank);
                oDataBase.AddInParameter(cm, "@FECHA_INI", DbType.String, FECHA_INI);
                oDataBase.AddInParameter(cm, "@FECHA_FIN", DbType.String, FECHA_FIN);
                oDataBase.AddInParameter(cm, "@CANT_ARC", DbType.Int32, CANT_ARC);
                oDataBase.AddInParameter(cm, "@MONTO", DbType.Decimal, monto);
                oDataBase.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, User);
                oDataBase.ExecuteReader(cm);
                return resultado;

            }

        } 
        public List<BEGenerarArchivo> LISTAR_ArchivosGenerados(int bnk_id, int pagina, int cantRegxPag)
        {
            BEGenerarArchivo item = null;
            List<BEGenerarArchivo> lista = new List<BEGenerarArchivo>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("USP_LISTAR_ARCHIVOS_GENERADOS"))
            {
                oDataBase.AddInParameter(cm, "@bnk_id", DbType.Int32, bnk_id);
                oDataBase.AddInParameter(cm, "@PageIndex", DbType.Int32, pagina);
                oDataBase.AddInParameter(cm, "@PageSize", DbType.Int32, cantRegxPag);
                oDataBase.AddOutParameter(cm, "@RecordCount", DbType.Int32, 50);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEGenerarArchivo();
                        if (!dr.IsDBNull(dr.GetOrdinal("DESC_ARC")))
                            item.DESC_ARC = dr.GetString(dr.GetOrdinal("DESC_ARC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BNK_ID")))
                            item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_INI")))
                            item.FECHA_INI = dr.GetString(dr.GetOrdinal("FECHA_INI"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_FIN")))
                            item.FECHA_FIN = dr.GetString(dr.GetOrdinal("FECHA_FIN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CANT_ARC")))
                            item.CANT_ARC = dr.GetDecimal(dr.GetOrdinal("CANT_ARC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_TOTAL")))
                            item.MONTO_TOTAL = dr.GetDecimal(dr.GetOrdinal("MONTO_TOTAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetString(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_CREAT = dr.GetString(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        item.TotalVirtual = dr.GetInt32(dr.GetOrdinal("CANTIDAD"));
                        lista.Add(item);
                    }
                }
            }
            return lista;

        }
        public List<BEFileBanco> LISTAR_ArchivosGenerados_Page(int bnk_id, int pagina, int cantRegxPag)
        {
            BEFileBanco item = null;
            List<BEFileBanco> lista = new List<BEFileBanco>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("USP_LISTAR_ARCHIVOS_CARGADOS_Page"))
            {
                oDataBase.AddInParameter(cm, "@bnk_id", DbType.Int32, bnk_id);
                oDataBase.AddInParameter(cm, "@PageIndex", DbType.Int32, pagina);
                oDataBase.AddInParameter(cm, "@PageSize", DbType.Int32, cantRegxPag);
                oDataBase.AddOutParameter(cm, "@RecordCount", DbType.Int32, 50);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEFileBanco();
                        if (!dr.IsDBNull(dr.GetOrdinal("FILE_COBRO_ID")))
                            item.FILE_COBRO_ID = dr.GetInt32(dr.GetOrdinal("FILE_COBRO_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESC_FILE")))
                            item.DESC_FILE = dr.GetString(dr.GetOrdinal("DESC_FILE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BNK_ID")))
                            item.BNK_ID = dr.GetDecimal(dr.GetOrdinal("BNK_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TOTAL_CABECERAS")))
                            item.TOTAL_CABECERAS = dr.GetDecimal(dr.GetOrdinal("TOTAL_CABECERAS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TOTAL")))
                            item.TOTAL = dr.GetString(dr.GetOrdinal("TOTAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetString(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                            item.LOG_DATE_CREAT = dr.GetString(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TOTAL_CARGADO")))
                            item.TOTAL_CARGADO = dr.GetInt32(dr.GetOrdinal("TOTAL_CARGADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MONTO_CARGADO")))
                            item.MONTO_CARGADO = dr.GetString(dr.GetOrdinal("MONTO_CARGADO"));
                        item.TotalVirtual = dr.GetInt32(dr.GetOrdinal("CANTIDAD"));
                        lista.Add(item);
                    }
                }
            }
            return lista;

        }
        public List<BEGenerarArchivo> CABECERA_ARCHIVO_PLANO_CONTINENTAL(int bnk_id)
        {
            BEGenerarArchivo item = null;
            List<BEGenerarArchivo> lista = new List<BEGenerarArchivo>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("CABECERA_ARCHIVO_PLANO_CONTINENTAL"))
            {

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEGenerarArchivo();
                        if (!dr.IsDBNull(dr.GetOrdinal("TR")))
                            item.DESC_ARC = dr.GetString(dr.GetOrdinal("TR"));   
                        lista.Add(item);
                    }
                }
            }
            return lista;

        }
        public List<BEREPORTE_DEPOSITO_BANCO> ListarReporteDepositoBancario_Cobro(int id)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("REPORTE_DEPOSITO_BANCOS_X_ID_FILE_COBRO");           
            db.AddInParameter(oDbCommand, "@FILE_COBRO_ID", DbType.Int32, id);            
            var lista = new List<BEREPORTE_DEPOSITO_BANCO>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEREPORTE_DEPOSITO_BANCO reporte = null;
                while (dr.Read())
                {
                    reporte = new BEREPORTE_DEPOSITO_BANCO();
                    reporte.ANIO_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("ANIO_CANCELACION_DETALLE"));
                    reporte.MES_CANCELACION_DETALLE = dr.GetInt32(dr.GetOrdinal("MES_CANCELACION_DETALLE"));
                    reporte.FEC_CANCELACION_DETALLE_DATE = dr.GetDateTime(dr.GetOrdinal("FEC_CANCELACION_DETALLE_DATE"));
                    reporte.FEC_CANCELACION_DETALLE = dr.GetString(dr.GetOrdinal("FEC_CANCELACION_DETALLE"));

                    reporte.nro_operacion = dr.GetString(dr.GetOrdinal("NRO_OPERACION"));
                    reporte.ID_COBRO = dr.GetDecimal(dr.GetOrdinal("ID_COBRO"));
                    reporte.FEC_DEPOSITO = dr.GetString(dr.GetOrdinal("FEC_DEPOSITO"));
                    reporte.FEC_DEPOSITO_CONFIRMACION = dr.GetString(dr.GetOrdinal("FEC_DEPOSITO_CONFIRMACION"));
                    reporte.CUENTA = dr.GetString(dr.GetOrdinal("CUENTA"));
                    reporte.DEPOSITO_MONTO = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_MONTO"));
                    reporte.DEPOSITO_SALDO = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_SALDO"));
                    reporte.DEPOSITO_MONTO_SOLES = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_MONTO_SOLES"));
                    reporte.DEPOSITO_SALDO_SOLES = dr.GetDecimal(dr.GetOrdinal("DEPOSITO_SALDO_SOLES"));

                    reporte.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));
                    reporte.RUBRO_NOMBRE = dr.GetString(dr.GetOrdinal("RUBRO_NOMBRE"));
                    reporte.DOCUMENTO = dr.GetString(dr.GetOrdinal("DOCUMENTO"));

                    reporte.IMPORTE_DETALLE_DEPOSITO = dr.GetDecimal(dr.GetOrdinal("IMPORTE_DETALLE_DEPOSITO"));
                    reporte.NODO_ID = dr.GetDecimal(dr.GetOrdinal("NODO_ID"));
                    reporte.NODO = dr.GetString(dr.GetOrdinal("NODO"));
                    reporte.TERRITORIO = dr.GetInt32(dr.GetOrdinal("TERRITORIO"));

                    lista.Add(reporte);

                }
            }
            return lista;
        }
    }
}


