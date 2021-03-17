using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DAModalidad
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEModalidad> Listar(BEModalidad modalidad, int pagina, int cantRegxPag, decimal idOficina, string UsuarioActual)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_MODALIDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, modalidad.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_DEC", DbType.String, modalidad.MOD_DEC);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, modalidad.MOD_ORIG);
            oDataBase.AddInParameter(oDbCommand, "@MOD_SOC", DbType.String, modalidad.MOD_SOC);
            oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, modalidad.CLASS_COD);
            oDataBase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, modalidad.MOG_ID);
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, modalidad.RIGHT_COD);
            oDataBase.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, modalidad.MOD_INCID);
            oDataBase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, modalidad.MOD_USAGE);
            oDataBase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, modalidad.MOD_REPER);
            oDataBase.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);

            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_MODALIDAD",
                modalidad.OWNER, modalidad.MOD_DEC, modalidad.MOD_ORIG, modalidad.MOD_SOC, modalidad.CLASS_COD, modalidad.MOG_ID,
                modalidad.RIGHT_COD, modalidad.MOD_INCID, modalidad.MOD_USAGE, modalidad.MOD_REPER, idOficina,
                pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEModalidad>();
            var obj = new BEModalidad();
            using (IDataReader dr = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (dr.Read())
                {
                    obj = new BEModalidad();
                    obj.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                    obj.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_ORIG")))
                        obj.MOD_ORIG = dr.GetString(dr.GetOrdinal("MOD_ORIG"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_ODESC")))
                        obj.ORIGEN = dr.GetString(dr.GetOrdinal("MOD_ODESC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("MOG_SOC")))
                        obj.MOG_SOC = dr.GetString(dr.GetOrdinal("MOG_SOC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOG_SDESC")))
                        obj.TIPO_SOCIEDAD = dr.GetString(dr.GetOrdinal("MOG_SDESC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CLASS_COD")))
                        obj.CLASS_COD = dr.GetString(dr.GetOrdinal("CLASS_COD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CLASS_DESC")))
                        obj.TIPO_CREACION = dr.GetString(dr.GetOrdinal("CLASS_DESC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("MOG_ID")))
                        obj.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOG_DESC")))
                        obj.MODALIDAD = dr.GetString(dr.GetOrdinal("MOG_DESC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("RIGHT_COD")))
                        obj.RIGHT_COD = dr.GetString(dr.GetOrdinal("RIGHT_COD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RIGHT_DESC")))
                        obj.TIPO_DERECHO = dr.GetString(dr.GetOrdinal("RIGHT_DESC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_INCID")))
                        obj.MOD_INCID = dr.GetString(dr.GetOrdinal("MOD_INCID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_IDESC")))
                        obj.INCIDENCIA = dr.GetString(dr.GetOrdinal("MOD_IDESC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_USAGE")))
                        obj.MOD_USAGE = dr.GetString(dr.GetOrdinal("MOD_USAGE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DUSAGE")))
                        obj.TIPO_OBRA = dr.GetString(dr.GetOrdinal("MOD_DUSAGE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_REPER")))
                        obj.MOD_REPER = dr.GetString(dr.GetOrdinal("MOD_REPER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DREPER")))
                        obj.USO_REPERTORIO = dr.GetString(dr.GetOrdinal("MOD_DREPER"));

                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_COM")))
                        obj.MOD_COM = dr.GetDecimal(dr.GetOrdinal("MOD_COM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DISCA")))
                        obj.MOD_DISCA = dr.GetDecimal(dr.GetOrdinal("MOD_DISCA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DISCS")))
                        obj.MOD_DISCS = dr.GetDecimal(dr.GetOrdinal("MOD_DISCS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DISCC")))
                        obj.MOD_DISCC = dr.GetDecimal(dr.GetOrdinal("MOD_DISCC"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        obj.ESTADO = "ACTIVO";
                    else
                        obj.ESTADO = "INACTIVO";

                    if (!dr.IsDBNull(dr.GetOrdinal("SAP_CODIGO")))
                        obj.SAP_CODIGO = dr.GetString(dr.GetOrdinal("SAP_CODIGO"));

                    obj.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(obj);
                }
            }
            return lista;
        }

        public int Insertar(BEModalidad modalidad)
        {
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_MODALIDAD");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, modalidad.OWNER);
                oDataBase.AddInParameter(oDbCommand, "@MOD_DEC", DbType.String, modalidad.MOD_DEC.ToUpper());
                oDataBase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, modalidad.MOD_ORIG.ToUpper());
                oDataBase.AddInParameter(oDbCommand, "@MOD_SOC", DbType.String, modalidad.MOD_SOC.ToUpper());

                oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, modalidad.CLASS_COD.ToUpper());
                oDataBase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, modalidad.MOG_ID.ToUpper());
                oDataBase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, modalidad.RIGHT_COD.ToUpper());
                oDataBase.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, modalidad.MOD_INCID.ToUpper());
                oDataBase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, modalidad.MOD_USAGE.ToUpper());

                oDataBase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, modalidad.MOD_REPER.ToUpper());
                oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, modalidad.LOG_USER_CREAT.ToUpper());
                if (modalidad.MOD_COM != -1)
                    oDataBase.AddInParameter(oDbCommand, "@MOD_COM", DbType.Decimal, modalidad.MOD_COM);
                if (modalidad.MOD_DISCA != -1)
                    oDataBase.AddInParameter(oDbCommand, "@MOD_DISCA", DbType.Decimal, modalidad.MOD_DISCA);

                if (modalidad.MOD_DISCS != -1)
                    oDataBase.AddInParameter(oDbCommand, "@MOD_DISCS", DbType.Decimal, modalidad.MOD_DISCS);
                if (modalidad.MOD_DISCC != -1)
                    oDataBase.AddInParameter(oDbCommand, "@MOD_DISCC", DbType.Decimal, modalidad.MOD_DISCC);

                oDataBase.AddInParameter(oDbCommand, "@WRFK_ID", DbType.Decimal, modalidad.WRFK_ID);
                oDataBase.AddInParameter(oDbCommand, "@MOD_OBS", DbType.String, modalidad.MOD_OBS);
                int r = oDataBase.ExecuteNonQuery(oDbCommand);
                return r;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Eliminar(BEModalidad modalidad)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_MODALIDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, modalidad.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, modalidad.MOD_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, modalidad.LOG_USER_UPDAT);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public BEModalidad Obtener(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_MODALIDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, id);

            BEModalidad modalidad = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    modalidad = new BEModalidad();
                    modalidad.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                    modalidad.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    modalidad.MOD_ORIG = dr.GetString(dr.GetOrdinal("MOD_ORIG"));
                    modalidad.MOD_SOC = dr.GetString(dr.GetOrdinal("MOD_SOC"));
                    modalidad.CLASS_COD = dr.GetString(dr.GetOrdinal("CLASS_COD"));
                    modalidad.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                    modalidad.RIGHT_COD = dr.GetString(dr.GetOrdinal("RIGHT_COD"));
                    modalidad.MOD_INCID = dr.GetString(dr.GetOrdinal("MOD_INCID"));
                    modalidad.MOD_USAGE = dr.GetString(dr.GetOrdinal("MOD_USAGE"));
                    modalidad.MOD_REPER = dr.GetString(dr.GetOrdinal("MOD_REPER"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_COM")))
                        modalidad.MOD_COM = dr.GetDecimal(dr.GetOrdinal("MOD_COM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DISCA")))
                        modalidad.MOD_DISCA = dr.GetDecimal(dr.GetOrdinal("MOD_DISCA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DISCS")))
                        modalidad.MOD_DISCS = dr.GetDecimal(dr.GetOrdinal("MOD_DISCS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DISCC")))
                        modalidad.MOD_DISCC = dr.GetDecimal(dr.GetOrdinal("MOD_DISCC"));


                    if (!dr.IsDBNull(dr.GetOrdinal("WRFK_ID")))
                        modalidad.WRFK_ID = dr.GetDecimal(dr.GetOrdinal("WRFK_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_OBS")))
                        modalidad.MOD_OBS = dr.GetString(dr.GetOrdinal("MOD_OBS"));
                }
            }
            return modalidad;
        }

        public int Update(BEModalidad modalidad)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_MODALIDAD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, modalidad.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, modalidad.MOD_ID);
            oDataBase.AddInParameter(oDbCommand, "@MOD_DEC", DbType.String, modalidad.MOD_DEC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, modalidad.MOD_ORIG.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_SOC", DbType.String, modalidad.MOD_SOC.ToUpper());

            oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, modalidad.CLASS_COD.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, modalidad.MOG_ID.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, modalidad.RIGHT_COD.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, modalidad.MOD_INCID.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, modalidad.MOD_USAGE.ToUpper());

            oDataBase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, modalidad.MOD_REPER.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, modalidad.LOG_USER_UPDAT.ToUpper());
            if (modalidad.MOD_COM != -1)
                oDataBase.AddInParameter(oDbCommand, "@MOD_COM", DbType.Decimal, modalidad.MOD_COM);
            if (modalidad.MOD_DISCA != -1)
                oDataBase.AddInParameter(oDbCommand, "@MOD_DISCA", DbType.Decimal, modalidad.MOD_DISCA);

            if (modalidad.MOD_DISCS != -1)
                oDataBase.AddInParameter(oDbCommand, "@MOD_DISCS", DbType.Decimal, modalidad.MOD_DISCS);
            if (modalidad.MOD_DISCC != -1)
                oDataBase.AddInParameter(oDbCommand, "@MOD_DISCC", DbType.Decimal, modalidad.MOD_DISCC);
            oDataBase.AddInParameter(oDbCommand, "@WRFK_ID", DbType.String, modalidad.WRFK_ID);
            oDataBase.AddInParameter(oDbCommand, "@MOD_OBS", DbType.String, modalidad.MOD_OBS);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEModalidad> ListarReporte(BEModalidad modalidad)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_MODALIDAD_REP");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, modalidad.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ORIG", DbType.String, modalidad.MOD_ORIG);
            oDataBase.AddInParameter(oDbCommand, "@MOD_SOC", DbType.String, modalidad.MOD_SOC);
            oDataBase.AddInParameter(oDbCommand, "@CLASS_COD", DbType.String, modalidad.CLASS_COD);
            oDataBase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, modalidad.MOG_ID);
            oDataBase.AddInParameter(oDbCommand, "@RIGHT_COD", DbType.String, modalidad.RIGHT_COD);
            oDataBase.AddInParameter(oDbCommand, "@MOD_INCID", DbType.String, modalidad.MOD_INCID);
            oDataBase.AddInParameter(oDbCommand, "@MOD_USAGE", DbType.String, modalidad.MOD_USAGE);
            oDataBase.AddInParameter(oDbCommand, "@MOD_REPER", DbType.String, modalidad.MOD_REPER);

            oDataBase.ExecuteNonQuery(oDbCommand);

            var lista = new List<BEModalidad>();
            var obj = new BEModalidad();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    obj = new BEModalidad();
                    obj.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                    obj.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));

                    obj.MOD_ORIG = dr.GetString(dr.GetOrdinal("MOD_ODESC"));
                    obj.MOD_SOC = dr.GetString(dr.GetOrdinal("MOG_SDESC"));
                    obj.TIPO_CREACION = dr.GetString(dr.GetOrdinal("CLASS_DESC"));
                    obj.MODALIDAD = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                    obj.TIPO_DERECHO = dr.GetString(dr.GetOrdinal("RIGHT_DESC"));
                    obj.INCIDENCIA = dr.GetString(dr.GetOrdinal("MOD_IDESC"));
                    obj.TIPO_OBRA = dr.GetString(dr.GetOrdinal("MOD_DUSAGE"));
                    obj.USO_REPERTORIO = dr.GetString(dr.GetOrdinal("MOD_DREPER"));

                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_COM")))
                        obj.MOD_COM = dr.GetDecimal(dr.GetOrdinal("MOD_COM"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DISCA")))
                        obj.MOD_DISCA = dr.GetDecimal(dr.GetOrdinal("MOD_DISCA"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DISCS")))
                        obj.MOD_DISCS = dr.GetDecimal(dr.GetOrdinal("MOD_DISCS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("MOD_DISCC")))
                        obj.MOD_DISCC = dr.GetDecimal(dr.GetOrdinal("MOD_DISCC"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        obj.ESTADO = "ACTIVO";
                    else
                        obj.ESTADO = "INACTIVO";

                    lista.Add(obj);
                }
            }
            return lista;
        }

        public BEModalidad ObtenerCodigosDatosModalidad(string owner, decimal id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DESC_ID_MOD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, id);

            BEModalidad modalidad = null;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    modalidad = new BEModalidad();
                    modalidad.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                    modalidad.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    modalidad.MOD_ORIG = dr.GetString(dr.GetOrdinal("MOD_ORIG"));
                    modalidad.MOD_SOC = dr.GetString(dr.GetOrdinal("MOD_SOC"));
                    modalidad.CLASS_COD = dr.GetString(dr.GetOrdinal("CLASS_COD"));
                    modalidad.MOG_ID = dr.GetString(dr.GetOrdinal("MOG_ID"));
                    modalidad.RIGHT_COD = dr.GetString(dr.GetOrdinal("RIGHT_COD"));
                    modalidad.MOD_INCID = dr.GetString(dr.GetOrdinal("MOD_INCID"));
                    modalidad.MOD_USAGE = dr.GetString(dr.GetOrdinal("MOD_USAGE"));
                    modalidad.MOD_REPER = dr.GetString(dr.GetOrdinal("MOD_REPER"));
                    modalidad.MOD_ODESC = dr.GetString(dr.GetOrdinal("MOD_ODESC"));
                    modalidad.MOG_SDESC = dr.GetString(dr.GetOrdinal("MOG_SDESC"));
                    modalidad.CLASS_DESC = dr.GetString(dr.GetOrdinal("CLASS_DESC"));
                    modalidad.MOG_DESC = dr.GetString(dr.GetOrdinal("MOG_DESC"));
                    modalidad.RIGHT_DESC = dr.GetString(dr.GetOrdinal("RIGHT_DESC"));
                    modalidad.MOD_IDESC = dr.GetString(dr.GetOrdinal("MOD_IDESC"));
                    modalidad.MOD_DUSAGE = dr.GetString(dr.GetOrdinal("MOD_DUSAGE"));
                    modalidad.MOD_DREPER = dr.GetString(dr.GetOrdinal("MOD_DREPER"));
                }
            }
            return modalidad;
        }

        public List<BEModalidad> ListarGrupoModXOficina(int? ID_OFF)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDA_LISTAR_GRUPOMODALIDAD_X_OFICINA");
            oDataBase.AddInParameter(oDbComand, "@ID_OFF", DbType.Int32, ID_OFF);

            var lista = new List<BEModalidad>();
            BEModalidad obs;
            try
            {
                using (IDataReader reader = oDataBase.ExecuteReader(oDbComand))
                {
                    while (reader.Read())
                    {
                        obs = new BEModalidad();
                        if (!reader.IsDBNull(reader.GetOrdinal("MOG_ID")))
                            obs.MOG_ID = Convert.ToString(reader["MOG_ID"]);
                        if (!reader.IsDBNull(reader.GetOrdinal("MOG_DESC")))
                            obs.MOG_DESC = Convert.ToString(reader["MOG_DESC"]);
                        lista.Add(obs);
                    }
                }
                return lista;
            }
            catch (Exception ex) {
                return null;
            }
        }
        public List<BEModalidad> ListaModalidadPorGrupoDropDownList(string Artista)
        {
            List<BEModalidad> lista = new List<BEModalidad>();
            BEModalidad item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("Lista_ModalidadXGrupoModalidad"))
            {
                oDataBase.AddInParameter(cm, "@PARAMETROS", DbType.String, Artista);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEModalidad();
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_ID")))
                            item.MOD_ID = dr.GetDecimal(dr.GetOrdinal("MOD_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("MOD_DEC")))
                            item.MOD_DEC = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }


    }
}
