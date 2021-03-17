using System;
using System.Collections.Generic;
using SGRDA.Entities.Reporte;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace SGRDA.DA.Reporte
{
    public class DAREPORTE_DE_EMPRADRONAMIENTO
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");
        public List<BEREPORTE_DE_EMPRADRONAMIENTO> ObtenerDatosREPORTE_DE_EMPRADRONAMIENTO(int MES, int ANIO,int ID_OFICINA,int oficina_id)
        {
             BEREPORTE_DE_EMPRADRONAMIENTO item = null;
            List<BEREPORTE_DE_EMPRADRONAMIENTO> lista = new List<BEREPORTE_DE_EMPRADRONAMIENTO>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDA_REPORTE_DE_EMPRADRONAMIENTO2"))
            {
                //oDataBase.AddInParameter(cm, "@FECHAINI", DbType.String, finicio);
                //oDataBase.AddInParameter(cm, "@FECHAFIN", DbType.String, ffin);oficina_id

                oDataBase.AddInParameter(cm, "@Oficina_Logueo", DbType.String, oficina_id);
                oDataBase.AddInParameter(cm, "@MES", DbType.String, MES);
                oDataBase.AddInParameter(cm, "@ANIO", DbType.Int32, ANIO);
                oDataBase.AddInParameter(cm, "@ID_OFICINA", DbType.Int32, ID_OFICINA);
                cm.CommandTimeout = 3600;


                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {


                        item = new BEREPORTE_DE_EMPRADRONAMIENTO();
                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));                       
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTABLECIMIENTO")))
                            item.ESTABLECIMIENTO = dr.GetString(dr.GetOrdinal("ESTABLECIMIENTO"));
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
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CONFIRMACION")))
                            item.FECHA_CONFIRMACION = dr.GetString(dr.GetOrdinal("FECHA_CONFIRMACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                            item.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_COLLECTN")))
                            item.INVL_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INVL_COLLECTN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("COMISION")))
                            item.COMISION = dr.GetDecimal(dr.GetOrdinal("COMISION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOG_DESC")))
                            item.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                            item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NORO")))
                            item.NORO = dr.GetString(dr.GetOrdinal("NORO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OFICINA_ADMINISTRATIVA")))
                            item.OFICINA_ADMINISTRATIVA = dr.GetString(dr.GetOrdinal("OFICINA_ADMINISTRATIVA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO")))
                            item.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
        public List<Be_BecsEspeciales> ListarAniosCierre()
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDA_COMBO_ANIOS_CIERRE2");
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
        public List<Be_BecsEspeciales> ListarMesesCierre(int ANIO)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDA_COMBO_MESES_X_ANIO_CIERRE2");
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
        public List<BEREPORTE_DE_EMPRADRONAMIENTO> ObtenerDatosREPORTE_DE_GESTION_EMPADRONAMIENTO(string finicio, string ffin, int ID_OFICINA, int oficina_id
     , string TIPO_PAGO
     )
        {
            BEREPORTE_DE_EMPRADRONAMIENTO item = null;
            List<BEREPORTE_DE_EMPRADRONAMIENTO> lista = new List<BEREPORTE_DE_EMPRADRONAMIENTO>();
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDA_REPORTE_DE_GESTION_EMPADRONAMIENTO"))
            {
                oDataBase.AddInParameter(cm, "@Fini", DbType.String, finicio);
                oDataBase.AddInParameter(cm, "@Ffin", DbType.String, ffin);
                oDataBase.AddInParameter(cm, "@Oficina_Logueo", DbType.String, oficina_id);
                oDataBase.AddInParameter(cm, "@ID_OFICINA", DbType.Int32, ID_OFICINA);
                oDataBase.AddInParameter(cm, "@FLAG", DbType.String, TIPO_PAGO);
                cm.CommandTimeout = 3600;
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {


                        item = new BEREPORTE_DE_EMPRADRONAMIENTO();
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
                        if (!dr.IsDBNull(dr.GetOrdinal("TD")))
                            item.TD = dr.GetString(dr.GetOrdinal("TD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_EMISION")))
                            item.FECHA_EMISION = dr.GetString(dr.GetOrdinal("FECHA_EMISION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NRO")))
                            item.NRO = dr.GetDecimal(dr.GetOrdinal("NRO"));
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
                        if (!dr.IsDBNull(dr.GetOrdinal("DISTRITO")))
                            item.DISTRITO = dr.GetString(dr.GetOrdinal("DISTRITO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PROVINCIA")))
                            item.PROVINCIA = dr.GetString(dr.GetOrdinal("PROVINCIA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DEPARTAMENTO")))
                            item.DEPARTAMENTO = dr.GetString(dr.GetOrdinal("DEPARTAMENTO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                            item.DIRECCION = dr.GetString(dr.GetOrdinal("DIRECCION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TIPO_EST")))
                            item.TIPO_EST = dr.GetString(dr.GetOrdinal("TIPO_EST"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SUBTIPO_EST")))
                            item.SUBTIPO_EST = dr.GetString(dr.GetOrdinal("SUBTIPO_EST"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FLAG_PAG_ADEL")))
                            item.FLAG_PAG_ADEL = dr.GetString(dr.GetOrdinal("FLAG_PAG_ADEL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PAGOS")))
                            item.PAGOS = dr.GetInt32(dr.GetOrdinal("PAGOS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DESCRIPCION")))
                            item.DESCRIPCION = dr.GetString(dr.GetOrdinal("DESCRIPCION"));









                        lista.Add(item);
                    }
                }
            }
            return lista;
        }
    }

}
 