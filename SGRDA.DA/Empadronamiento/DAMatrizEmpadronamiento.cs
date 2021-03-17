using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities.Empadronamiento;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.DA.Empadronamiento
{
    public class DAMatrizEmpadronamiento
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");
        public List<BEMatrizEmpadronamiento> ObtenerLista_Matriz_EMPADRONAMIENTO(string anio, string mes, int ID_OFICINA
            , int oficina_id, string TIPO_PAGO, int LIC_ID, int pagina, int cantRegxPag
     )
        {
            BEMatrizEmpadronamiento item = null;
            List<BEMatrizEmpadronamiento> lista = new List<BEMatrizEmpadronamiento>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("MATRIZ_EMPADRONAMIENTO"))
            {
                oDataBase.AddInParameter(cm, "@anio", DbType.String, anio);
                oDataBase.AddInParameter(cm, "@mes", DbType.String, mes);
                oDataBase.AddInParameter(cm, "@Oficina_Logueo", DbType.String, oficina_id);
                oDataBase.AddInParameter(cm, "@ID_OFICINA", DbType.Int32, ID_OFICINA);
                oDataBase.AddInParameter(cm, "@FLAG", DbType.String, TIPO_PAGO);
                oDataBase.AddInParameter(cm, "@LIC_ID", DbType.String, LIC_ID);

                oDataBase.AddInParameter(cm, "@PageIndex", DbType.Int32, pagina);
                oDataBase.AddInParameter(cm, "@PageSize", DbType.Int32, cantRegxPag);
                oDataBase.AddOutParameter(cm, "@RecordCount", DbType.Int32, 50);

                cm.CommandTimeout = 3600;
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {


                        item = new BEMatrizEmpadronamiento();
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CREACION")))
                            item.FECHA_CREACION = dr.GetString(dr.GetOrdinal("FECHA_CREACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                            item.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTABLECIMIENTO")))
                            item.ESTABLECIMIENTO = dr.GetString(dr.GetOrdinal("ESTABLECIMIENTO"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("MOG_DESC")))
                        //    item.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("TD")))
                        //    item.TD = dr.GetString(dr.GetOrdinal("TD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_EMISION")))
                            item.FECHA_EMISION = dr.GetString(dr.GetOrdinal("FECHA_EMISION"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                        //    item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTO")))
                            item.DOCUMENTO = dr.GetString(dr.GetOrdinal("DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                            item.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CONFIRMACION")))
                            item.FECHA_CONFIRMACION = dr.GetString(dr.GetOrdinal("FECHA_CONFIRMACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                            item.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTN")))
                            item.INVL_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTN"));


                        if (!dr.IsDBNull(dr.GetOrdinal("NODO")))
                            item.NODO = dr.GetString(dr.GetOrdinal("NODO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFICINA_ADMINISTRATIVA")))
                            item.OFICINA_ADMINISTRATIVA = dr.GetString(dr.GetOrdinal("OFICINA_ADMINISTRATIVA"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO")))
                        //    item.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("PROVINCIA")))
                        //    item.PROVINCIA = dr.GetString(dr.GetOrdinal("PROVINCIA"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("DEPARTAMENTO")))
                        //    item.DEPARTAMENTO = dr.GetString(dr.GetOrdinal("DEPARTAMENTO"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                        //    item.DIRECCION = dr.GetString(dr.GetOrdinal("DIRECCION"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("TIPO_EST")))
                        //    item.TIPO_EST = dr.GetString(dr.GetOrdinal("TIPO_EST"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("SUBTIPO_EST")))
                        //    item.SUBTIPO_EST = dr.GetString(dr.GetOrdinal("SUBTIPO_EST"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FLAG_PAG_ADEL")))
                            item.FLAG_PAG_ADEL = dr.GetString(dr.GetOrdinal("FLAG_PAG_ADEL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PAGOS")))
                            item.PAGOS = dr.GetInt32(dr.GetOrdinal("PAGOS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPCION")))
                            item.DESCRIPCION = dr.GetString(dr.GetOrdinal("DESCRIPCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CANTIDAD")))
                            item.TotalVirtual = dr.GetInt32(dr.GetOrdinal("CANTIDAD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PAGO_EMPADRONAMIENTO")))
                            item.PAGO_EMPADRONAMIENTO = dr.GetDecimal(dr.GetOrdinal("PAGO_EMPADRONAMIENTO"));








                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        //
        public List<Be_BecsEspeciales> ListarAnios()
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("MATRIZ_EMPADRONAMIENTO_COMBO_ANIOS_CIERRE2_");
            var lista = new List<Be_BecsEspeciales>();
            Be_BecsEspeciales obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    obs = new Be_BecsEspeciales();
                    obs.ACCOUNTANT_YEAR = Convert.ToInt32(reader["ACCOUNTANT_YEAR"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }

        public List<Be_BecsEspeciales> ListarMeses(int ANIO)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("MATRIZ_EMPADRONAMIENTO_COMBO_MESES_X_ANIO_CIERRE2");
            oDataBase.AddInParameter(oDbComand, "@ANIO", DbType.Int32, ANIO);
            var lista = new List<Be_BecsEspeciales>();
            Be_BecsEspeciales obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {

                while (reader.Read())
                {
                    obs = new Be_BecsEspeciales();
                    obs.NOMBRE_MES = Convert.ToString(reader["NOMBRE_MES"]);
                    obs.ACCOUNTANT_MONTH = Convert.ToInt32(reader["ACCOUNTANT_MONTH"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }



        public List<BETabla_Comision> Lista_Tabla_Comision()
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDA_LISTA_TABLA_COMISION");
            var lista = new List<BETabla_Comision>();
            BETabla_Comision obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    obs = new BETabla_Comision();
                    obs.ID_COMISION = Convert.ToInt32(reader["ID_COMISION"]);
                    obs.ID_RANGO = Convert.ToInt32(reader["ID_RANGO"]);
                    obs.MONTO_DESDE = Convert.ToDecimal(reader["MONTO_DESDE"]);
                    obs.MONTO_HASTA = Convert.ToDecimal(reader["MONTO_HASTA"]);
                    obs.PORCENTAJE = Convert.ToDecimal(reader["PORCENTAJE"]);
                    obs.LOG_USER_CREATE = Convert.ToString(reader["LOG_USER_CREATE"]);
                    obs.Fecha_Creacion = Convert.ToString(reader["Fecha_Creacion"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }

        public List<BETabla_Comision> Listar_Combo_RangoComision()
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDA_COMBO_RANGO_COMISION");
            var lista = new List<BETabla_Comision>();
            BETabla_Comision obs;
            using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
            {
                while (reader.Read())
                {
                    obs = new BETabla_Comision();
                    obs.ID_RANGO = Convert.ToInt32(reader["RANGO"]);
                    obs.Desc_Rango = Convert.ToString(reader["Desc_Rango"]);
                    lista.Add(obs);
                }
            }
            return lista;
        }

        public int Insertar_RangoComision(int ID_RANGO, decimal MONTO_DESDE, decimal MONTO_HASTA, decimal PORCENTAJE, string User)
        {
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAI_INSERTAR_RANGO_COMISION"))
            {
                try
                {
                    oDataBase.AddInParameter(cm, "@ID_RANGO", DbType.Int32, ID_RANGO);
                    oDataBase.AddInParameter(cm, "@MONTO_DESDE", DbType.Decimal, MONTO_DESDE);
                    oDataBase.AddInParameter(cm, "@MONTO_HASTA", DbType.Decimal, MONTO_HASTA);
                    oDataBase.AddInParameter(cm, "@PORCENTAJE", DbType.Decimal, PORCENTAJE);
                    oDataBase.AddInParameter(cm, "@LOG_USER_CREATE", DbType.String, User);
                    oDataBase.AddOutParameter(cm, "@ID_COMISION", DbType.Int32, 0);

                    int n = oDataBase.ExecuteNonQuery(cm);
                    int idComision = Convert.ToInt32(oDataBase.GetParameterValue(cm, "@ID_COMISION"));
                    return idComision;
                }
                catch (Exception)
                {
                    return 0;
                }
            }

        }

        //SGRDAD_DELETE_RANGO_COMISION
        public int Delete_RangoComision(int ID_COMISION, string User)
        {
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAD_DELETE_RANGO_COMISION"))
            {
                try
                {
                    oDataBase.AddInParameter(cm, "@ID_COMISION", DbType.Int32, ID_COMISION);
                    oDataBase.AddInParameter(cm, "@LOG_USER_UPDATE", DbType.String, User);
                    oDataBase.ExecuteNonQuery(cm);
                    return 1;
                }
                catch (Exception e)
                {
                    var message = e.Message;
                    return 0;
                }




            }

        }

        // AGREGAR TIPO_RANGO
        public int Insertar_TipoRango(string user)
        {
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASI_Agregar_Rango"))
            {
                try
                {
                    oDataBase.AddInParameter(cm, "@User", DbType.String, user);
                    oDataBase.AddOutParameter(cm, "@Retorno", DbType.Int32, 0);

                    int n = oDataBase.ExecuteNonQuery(cm);
                    int id = Convert.ToInt32(oDataBase.GetParameterValue(cm, "@Retorno"));
                    return id;
                }
                catch (Exception)
                {
                    return 0;
                }
            }

        }

        public int Desactivar_TipoRango(string user)
        {
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASU_Desactivar_Rango"))
            {
                try
                {
                    oDataBase.AddInParameter(cm, "@User", DbType.String, user);
                    oDataBase.AddOutParameter(cm, "@Retorno", DbType.Int32, 0);

                    int n = oDataBase.ExecuteNonQuery(cm);
                    int id = Convert.ToInt32(oDataBase.GetParameterValue(cm, "@Retorno"));
                    return id;
                }
                catch (Exception)
                {
                    return 0;
                }
            }

        }

        //NUEVO MODULO
        public List<BEMatrizEmpadronamiento> ObtenerLista_Modulo_EMPADRONAMIENTO(int anio, int mes, int ID_OFICINA
    , int oficina_id, string TIPO_PAGO, int LIC_ID, int pagina, int cantRegxPag
)
        {
            BEMatrizEmpadronamiento item = null;
            List<BEMatrizEmpadronamiento> lista = new List<BEMatrizEmpadronamiento>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("MODULO_EMPADRONAMIENTO"))
            {
                oDataBase.AddInParameter(cm, "@ANIO", DbType.Int32, anio);
                oDataBase.AddInParameter(cm, "@MES", DbType.Int32, mes);
                oDataBase.AddInParameter(cm, "@OFICINA_LOGUEO", DbType.String, oficina_id);
                oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Int32, ID_OFICINA);
                //oDataBase.AddInParameter(cm, "@FLAG", DbType.String, TIPO_PAGO);
                //oDataBase.AddInParameter(cm, "@LIC_ID", DbType.String, LIC_ID);

                oDataBase.AddInParameter(cm, "@PageIndex", DbType.Int32, pagina);
                oDataBase.AddInParameter(cm, "@PageSize", DbType.Int32, cantRegxPag);
                oDataBase.AddOutParameter(cm, "@RecordCount", DbType.Int32, 100);

                cm.CommandTimeout = 3600;
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {


                        item = new BEMatrizEmpadronamiento();
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CREACION")))
                            item.FECHA_CREACION = dr.GetString(dr.GetOrdinal("FECHA_CREACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                            item.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTABLECIMIENTO")))
                            item.ESTABLECIMIENTO = dr.GetString(dr.GetOrdinal("ESTABLECIMIENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOG_DESC")))
                            item.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("TD")))
                        //    item.TD = dr.GetString(dr.GetOrdinal("TD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_EMISION")))
                            item.FECHA_EMISION = dr.GetString(dr.GetOrdinal("FECHA_EMISION"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                        //    item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DOCUMENTO")))
                            item.DOCUMENTO = dr.GetString(dr.GetOrdinal("DOCUMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PERIODO")))
                            item.PERIODO = dr.GetString(dr.GetOrdinal("PERIODO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CONFIRMACION")))
                            item.FECHA_CONFIRMACION = dr.GetString(dr.GetOrdinal("FECHA_CONFIRMACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                            item.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTN")))
                            item.INVL_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTN"));


                        if (!dr.IsDBNull(dr.GetOrdinal("NODO")))
                            item.NODO = dr.GetString(dr.GetOrdinal("NODO"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("OFICINA_ADMINISTRATIVA")))
                        //    item.OFICINA_ADMINISTRATIVA = dr.GetString(dr.GetOrdinal("OFICINA_ADMINISTRATIVA"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO")))
                        //    item.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("PROVINCIA")))
                        //    item.PROVINCIA = dr.GetString(dr.GetOrdinal("PROVINCIA"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("DEPARTAMENTO")))
                        //    item.DEPARTAMENTO = dr.GetString(dr.GetOrdinal("DEPARTAMENTO"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                        //    item.DIRECCION = dr.GetString(dr.GetOrdinal("DIRECCION"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("TIPO_EST")))
                        //    item.TIPO_EST = dr.GetString(dr.GetOrdinal("TIPO_EST"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("SUBTIPO_EST")))
                        //    item.SUBTIPO_EST = dr.GetString(dr.GetOrdinal("SUBTIPO_EST"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("FLAG_PAG_ADEL")))
                        //    item.FLAG_PAG_ADEL = dr.GetString(dr.GetOrdinal("FLAG_PAG_ADEL"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("PAGOS")))
                        //    item.PAGOS = dr.GetInt32(dr.GetOrdinal("PAGOS"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPCION")))
                        //    item.DESCRIPCION = dr.GetString(dr.GetOrdinal("DESCRIPCION"));
                        //if (!dr.IsDBNull(dr.GetOrdinal("CANTIDAD")))
                        //    item.TotalVirtual = dr.GetInt32(dr.GetOrdinal("CANTIDAD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PAGO_EMPADRONAMIENTO")))
                            item.PAGO_EMPADRONAMIENTO = dr.GetDecimal(dr.GetOrdinal("PAGO_EMPADRONAMIENTO"));

                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
    }
}
