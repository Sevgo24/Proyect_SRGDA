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
   public class DA_BecsEspeciales
    {
        private Database oDataBase = DatabaseFactory.CreateDatabase("conexion");
        public List<Be_BecsEspeciales> ListarDatosBesEspeciales(int cant, int mes,int anio ,int oficina_id)
        {
            List<Be_BecsEspeciales> lista = new List<Be_BecsEspeciales>();
            Be_BecsEspeciales item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("Listar_BecsEspeciales"))
            {
                oDataBase.AddInParameter(cm, "@CantMesesCierre", DbType.Int32, cant);
                oDataBase.AddInParameter(cm, "@MesCierre", DbType.Int32, mes);
                oDataBase.AddInParameter(cm, "@AñoCierre", DbType.Int32, anio);
                oDataBase.AddInParameter(cm, "@Off_id", DbType.Int32, oficina_id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new Be_BecsEspeciales();
                        if (!dr.IsDBNull(dr.GetOrdinal("Mes")))
                            item.Mes = dr.GetString(dr.GetOrdinal("Mes"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BEC")))
                            item.BEC = dr.GetDecimal(dr.GetOrdinal("BEC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("RazonSocial")))
                            item.RazonSocial = dr.GetString(dr.GetOrdinal("RazonSocial"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Territorio")))
                            item.Territorio = dr.GetString(dr.GetOrdinal("Territorio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TD")))
                            item.TD = dr.GetString(dr.GetOrdinal("TD"));
                        if (!dr.IsDBNull(dr.GetOrdinal("SERIE")))
                            item.SERIE = dr.GetString(dr.GetOrdinal("SERIE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Correlativo")))
                            item.Correlativo = dr.GetDecimal(dr.GetOrdinal("Correlativo"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaEmision")))
                            item.FechaEmision = dr.GetString(dr.GetOrdinal("FechaEmision"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaDeposito")))
                            item.FechaDeposito = dr.GetString(dr.GetOrdinal("FechaDeposito"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaCreacionBec")))
                            item.FechaCreacionBec = dr.GetString(dr.GetOrdinal("FechaCreacionBec"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaConfirmacion")))
                            item.FechaConfirmacion = dr.GetString(dr.GetOrdinal("FechaConfirmacion"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BANCO")))
                            item.BANCO = dr.GetString(dr.GetOrdinal("BANCO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("CUENTA")))
                            item.CUENTA = dr.GetString(dr.GetOrdinal("CUENTA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Voucher")))
                            item.Voucher = dr.GetString(dr.GetOrdinal("Voucher"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Deposito")))
                            item.Deposito = dr.GetDecimal(dr.GetOrdinal("Deposito"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Afecto")))
                            item.Afecto = dr.GetDecimal(dr.GetOrdinal("Afecto"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Rubro")))
                            item.Rubro = dr.GetString(dr.GetOrdinal("Rubro"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaInicio")))
                            item.FechaInicio = dr.GetString(dr.GetOrdinal("FechaInicio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaFin")))
                            item.FechaFin = dr.GetString(dr.GetOrdinal("FechaFin"));
                        lista.Add(item);
                    }
                }
            }         
            return lista;
        }
        public List<Be_BecsEspeciales> ListarAniosCierre()
        {           
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDA_COMBO_ANIOS_CIERRE");
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
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDA_COMBO_MESES_X_ANIO_CIERRE");
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

        public List<Be_BecsEspeciales> ListarDatosBesEspecialesResumen(int cant, int mes, int anio,int oficina_id)
        {
            List<Be_BecsEspeciales> lista = new List<Be_BecsEspeciales>();
            Be_BecsEspeciales item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("Lista_Resumen_BecsEspeciales"))
            {
                oDataBase.AddInParameter(cm, "@CantMesesCierre", DbType.Int32, cant);
                oDataBase.AddInParameter(cm, "@MesCierre", DbType.Int32, mes);
                oDataBase.AddInParameter(cm, "@AñoCierre", DbType.Int32, anio);
                oDataBase.AddInParameter(cm, "@Off_id", DbType.Int32, oficina_id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new Be_BecsEspeciales();                       
                        if (!dr.IsDBNull(dr.GetOrdinal("Territorio")))
                            item.Territorio = dr.GetString(dr.GetOrdinal("Territorio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Rubro")))
                            item.Rubro = dr.GetString(dr.GetOrdinal("Rubro"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Deposito")))
                            item.Deposito = dr.GetDecimal(dr.GetOrdinal("Deposito"));
                        if (!dr.IsDBNull(dr.GetOrdinal("Afecto")))
                            item.Afecto = dr.GetDecimal(dr.GetOrdinal("Afecto"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaInicio")))
                            item.FechaInicio = dr.GetString(dr.GetOrdinal("FechaInicio"));
                        if (!dr.IsDBNull(dr.GetOrdinal("FechaFin")))
                            item.FechaFin = dr.GetString(dr.GetOrdinal("FechaFin"));

                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

    }
}
