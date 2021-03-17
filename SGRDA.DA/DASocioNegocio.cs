using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DASocioNegocio
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public bool ValidarTieneDocumento(string owner, decimal bps)
        {
            bool existe = false;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_TIENE_NUMDOC"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Int32, bps);
                    oDataBase.AddOutParameter(cm, "@retorno", DbType.Boolean, 4);
                    int n = oDataBase.ExecuteNonQuery(cm);
                    existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@retorno"));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return existe;
        }

        public List<SocioNegocio> USP_SOCIOS_LISTARPAGE(decimal tipo, string nro_tipo, string nombre, int pagina, int cantRegxPag, string BPS_USER, string BPS_ASSOCIATION, string BPS_GROUP, string BPS_COLLECTOR, string BPS_SUPPLIER, string BPS_EMPLOYEE, int estado)
        {
            String BPS_KEY = String.Format("{0}{1}{2}{3}{4}{5}", BPS_USER, BPS_COLLECTOR, BPS_EMPLOYEE, BPS_ASSOCIATION, BPS_GROUP, BPS_SUPPLIER);
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand = oDataBase.GetStoredProcCommand("USP_SOCIOS_LISTARPAGE");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("USP_SOCIOS_LISTARPAGE_FILTRO");
            oDataBase.AddInParameter(oDbCommand, "@TAXT_ID", DbType.Decimal, tipo);
            oDataBase.AddInParameter(oDbCommand, "@TAX_ID", DbType.String, nro_tipo);
            oDataBase.AddInParameter(oDbCommand, "@BPS_NAME", DbType.String, nombre);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@BPS_USER", DbType.String, BPS_USER);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ASSOCIATION", DbType.String, BPS_ASSOCIATION);
            oDataBase.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.String, BPS_GROUP);
            oDataBase.AddInParameter(oDbCommand, "@BPS_COLLECTOR", DbType.String, BPS_COLLECTOR);
            oDataBase.AddInParameter(oDbCommand, "@BPS_SUPPLIER", DbType.String, BPS_SUPPLIER);
            oDataBase.AddInParameter(oDbCommand, "@BPS_EMPLOYEE", DbType.String, BPS_EMPLOYEE);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ENDS", DbType.Int32, estado);
            oDataBase.AddInParameter(oDbCommand, "@BPS_KEY", DbType.String, BPS_KEY);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("USP_SOCIOS_LISTARPAGE_FILTRO", tipo, nro_tipo, nombre, pagina, cantRegxPag, BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE, estado, BPS_KEY, ParameterDirection.Output);

            var lista = new List<SocioNegocio>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new SocioNegocio(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public SocioNegocio ObtenerSocioDocumento(string owner, decimal tipo, string nro_tipo)
        {
            SocioNegocio socio = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SOCIO_X_DOCUMENTO"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@TAXT_ID", DbType.Decimal, tipo);
                    oDataBase.AddInParameter(cm, "@TAX_ID", DbType.String, nro_tipo);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            socio = new SocioNegocio();
                            socio.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            {
                                socio.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                            {
                                socio.BPS_FIRST_NAME = dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                            {
                                socio.BPS_FATH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                            {
                                socio.BPS_MOTH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("TAXT_ID")))
                            {
                                socio.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("TAXN_NAME")))
                            {
                                socio.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            {
                                socio.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return socio;
        }

        public List<SocioNegocio> UPS_BUSCAR_SOCIOS_X_RAZONSOCIAL(string Owner, string datos)
        {
            List<SocioNegocio> lst = new List<SocioNegocio>();
            SocioNegocio item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("UPS_BUSCAR_SOCIOS_X_RAZONSOCIAL"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                    oDataBase.AddInParameter(cm, "@DATOS", DbType.String, datos);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new SocioNegocio();
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            {
                                item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                            }
                            //if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                            //{
                            //    item.BPS_FIRST_NAME = dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME"));
                            //}
                            //if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                            //{
                            //    item.BPS_FATH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME"));
                            //}
                            //if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                            //{
                            //    item.BPS_MOTH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME"));
                            //}
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        public int Insertar(SocioNegocio bps)
        {

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_BPS_GRAL");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, bps.OWNER);
            oDataBase.AddOutParameter(oDbComand, "@BPS_ID", DbType.Decimal, Convert.ToInt32(bps.BPS_ID));
            oDataBase.AddInParameter(oDbComand, "@ENT_TYPE", DbType.String, bps.ENT_TYPE);
            oDataBase.AddInParameter(oDbComand, "@BPS_NAME", DbType.String, bps.BPS_NAME != null ? bps.BPS_NAME.ToString().ToUpper() : bps.BPS_NAME);
            oDataBase.AddInParameter(oDbComand, "@BPS_FIRST_NAME", DbType.String, bps.BPS_FIRST_NAME != null ? bps.BPS_FIRST_NAME.ToString().ToUpper() : bps.BPS_FIRST_NAME);
            oDataBase.AddInParameter(oDbComand, "@BPS_FATH_SURNAME", DbType.String, bps.BPS_FATH_SURNAME != null ? bps.BPS_FATH_SURNAME.ToString().ToUpper() : bps.BPS_FATH_SURNAME);
            oDataBase.AddInParameter(oDbComand, "@BPS_MOTH_SURNAME", DbType.String, bps.BPS_MOTH_SURNAME != null ? bps.BPS_MOTH_SURNAME.ToString().ToUpper() : bps.BPS_MOTH_SURNAME);
            oDataBase.AddInParameter(oDbComand, "@TAXT_ID", DbType.Decimal, bps.TAXT_ID);
            oDataBase.AddInParameter(oDbComand, "@TAX_ID", DbType.String, bps.TAX_ID);
            oDataBase.AddInParameter(oDbComand, "@BPS_GROUP", DbType.String, bps.BPS_GROUP);
            oDataBase.AddInParameter(oDbComand, "@BPS_INT", DbType.DateTime, bps.BPS_INT);
            oDataBase.AddInParameter(oDbComand, "@BPS_INT_N", DbType.String, bps.BPS_INT_N != null ? bps.BPS_INT_N.ToString().ToUpper() : bps.BPS_INT_N);
            oDataBase.AddInParameter(oDbComand, "@BPS_USER", DbType.String, bps.BPS_USER);
            oDataBase.AddInParameter(oDbComand, "@BPS_COLLECTOR", DbType.String, bps.BPS_COLLECTOR);
            oDataBase.AddInParameter(oDbComand, "@BPS_ASSOCIATION", DbType.String, bps.BPS_ASSOCIATION);
            oDataBase.AddInParameter(oDbComand, "@BPS_EMPLOYEE", DbType.String, bps.BPS_EMPLOYEE);
            oDataBase.AddInParameter(oDbComand, "@BPS_SUPPLIER", DbType.String, bps.BPS_SUPPLIER);
            oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, bps.LOG_USER_CREAT.ToUpper());
            oDataBase.AddInParameter(oDbComand, "@BPS_TRADE_NAME", DbType.String, bps.BPS_TRADE_NAME);

            int n = oDataBase.ExecuteNonQuery(oDbComand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbComand, "@BPS_ID"));

            return id;

        }

        public int Actualizar(SocioNegocio bps)
        {
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASU_BPS_GRAL");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, bps.OWNER);
                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Int32, bps.BPS_ID);
                oDataBase.AddInParameter(oDbComand, "@ENT_TYPE", DbType.String, bps.ENT_TYPE);
                oDataBase.AddInParameter(oDbComand, "@BPS_NAME", DbType.String, bps.BPS_NAME);
                oDataBase.AddInParameter(oDbComand, "@BPS_FIRST_NAME", DbType.String, bps.BPS_FIRST_NAME);
                oDataBase.AddInParameter(oDbComand, "@BPS_FATH_SURNAME", DbType.String, bps.BPS_FATH_SURNAME);
                oDataBase.AddInParameter(oDbComand, "@BPS_MOTH_SURNAME", DbType.String, bps.BPS_MOTH_SURNAME);
                oDataBase.AddInParameter(oDbComand, "@TAXT_ID", DbType.Decimal, bps.TAXT_ID);
                oDataBase.AddInParameter(oDbComand, "@TAX_ID", DbType.String, bps.TAX_ID);
                oDataBase.AddInParameter(oDbComand, "@BPS_GROUP", DbType.String, bps.BPS_GROUP);
                oDataBase.AddInParameter(oDbComand, "@BPS_INT", DbType.DateTime, bps.BPS_INT);
                oDataBase.AddInParameter(oDbComand, "@BPS_INT_N", DbType.String, bps.BPS_INT_N);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDAT", DbType.String, bps.LOG_USER_UPDATE);
                oDataBase.AddInParameter(oDbComand, "@BPS_TRADE_NAME", DbType.String, bps.BPS_TRADE_NAME);

                int n = oDataBase.ExecuteNonQuery(oDbComand);

                return n;
            }
            catch (Exception)
            {
                return 0; ;
            }
        }
        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="bps"></param>
        /// <returns></returns>
        public int Eliminar(SocioNegocio bps)
        {
            try
            {
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASD_BPS_GRAL");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, bps.OWNER);
                oDataBase.AddInParameter(oDbComand, "@BPS_ID", DbType.Int32, bps.BPS_ID);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_UPDATE", DbType.String, bps.LOG_USER_UPDATE);
                int r = oDataBase.ExecuteNonQuery(oDbComand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public List<SocioNegocio> ObtenerGrupoEmp(string Owner)
        {
            List<SocioNegocio> Obj = null;
            try
            {

                Database oDataBase = new DatabaseProviderFactory().Create("conexion");

                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_GRUPO_EMP"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        Obj = new List<SocioNegocio>();
                        while (dr.Read())
                        {
                            var socio = new SocioNegocio();
                            socio.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            socio.ENT_TYPE = Convert.ToChar(dr.GetValue(dr.GetOrdinal("ENT_TYPE")));
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            {
                                socio.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                            {
                                socio.BPS_FIRST_NAME = dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                            {
                                socio.BPS_FATH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                            {
                                socio.BPS_MOTH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME"));
                            }
                            Obj.Add(socio);
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return Obj;
        }
        public SocioNegocio Obtener(decimal codigoBps, string owner)
        {
            SocioNegocio socio = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SOCIO"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, codigoBps);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            socio = new SocioNegocio();
                            socio.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            socio.ENT_TYPE = Convert.ToChar(dr.GetValue(dr.GetOrdinal("ENT_TYPE")));

                            socio.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));

                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            {
                                //  socio.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                                socio.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                            {
                                socio.BPS_FIRST_NAME = dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                            {
                                socio.BPS_FIRST_NAME = dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                            {
                                socio.BPS_FATH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                            {
                                socio.BPS_MOTH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME"));
                            }

                            socio.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            {
                                socio.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                            }


                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                socio.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_INT")))
                            {
                                socio.BPS_INT = dr.GetDateTime(dr.GetOrdinal("BPS_INT"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_INT_N")))
                            {
                                socio.BPS_INT_N = dr.GetString(dr.GetOrdinal("BPS_INT_N"));
                            }

                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_TRADE_NAME")))
                                socio.BPS_TRADE_NAME = dr.GetString(dr.GetOrdinal("BPS_TRADE_NAME"));


                            socio.BPS_GROUP = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BPS_GROUP")));
                            socio.BPS_GROUP = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BPS_GROUP")));

                            socio.BPS_EMPLOYEE = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BPS_EMPLOYEE")));
                            socio.BPS_USER = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BPS_USER")));
                            socio.BPS_COLLECTOR = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BPS_COLLECTOR")));
                            socio.BPS_ASSOCIATION = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BPS_ASSOCIATION")));
                            socio.BPS_EMPLOYEE = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BPS_EMPLOYEE")));
                            socio.BPS_SUPPLIER = Convert.ToChar(dr.GetValue(dr.GetOrdinal("BPS_SUPPLIER")));
                            socio.VERIFICADO = dr.GetString(dr.GetOrdinal("VERIFICADO"));


                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return socio;
        }

        public SocioNegocio BuscarXtipodocumento(decimal idTipoDocumento, string nroDocumento)
        {
            SocioNegocio socio = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SOCIO_TIPODOC"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                    oDataBase.AddInParameter(cm, "@TAXT_ID", DbType.String, idTipoDocumento);
                    oDataBase.AddInParameter(cm, "@TAX_ID", DbType.String, nroDocumento);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            socio = new SocioNegocio();
                            socio.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            socio.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return socio;
        }


        public List<SocioNegocio> AutoCompletarRecaudadores(string Owner, string datos)
        {
            List<SocioNegocio> lst = new List<SocioNegocio>();
            SocioNegocio item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_RECAUDADORES_AC"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                oDataBase.AddInParameter(cm, "@DATOS", DbType.String, datos);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new SocioNegocio();
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                            item.BPS_FIRST_NAME = dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                            item.BPS_FATH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                            item.BPS_MOTH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME"));
                        lst.Add(item);
                    }
                }
            }
            return lst;
        }



        public SocioNegocio BuscarXtipodocumentoRecaudador(decimal idTipoDocumento, string nroDocumento)
        {
            SocioNegocio socio = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_SOCIO_TIPODOC_RECAUDADOR"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@TAXT_ID", DbType.String, idTipoDocumento);
                oDataBase.AddInParameter(cm, "@TAX_ID", DbType.String, nroDocumento);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        socio = new SocioNegocio();

                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        {
                            socio.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        {
                            socio.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("DAD_ID")))
                        {
                            socio.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("DAD_NAME")))
                        {
                            socio.DAD_NAME = dr.GetString(dr.GetOrdinal("DAD_NAME"));
                        }
                    }
                }
            }
            return socio;
        }

        public List<SocioNegocio> BuscarSocio(string tipoPersona,
                                                decimal? tipoId,
                                                string nro_tipo,
                                                string nombre,
                                                decimal ubigeo,
                                                int estado,
                                                int pagina,
                                                int cantRegxPag, string BPS_USER, string BPS_ASSOCIATION, string BPS_GROUP, string BPS_COLLECTOR, string BPS_SUPPLIER, string BPS_EMPLOYEE)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            //DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_SOCIOS_LISTARPAGE");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("USP_SOCIOS_LISTARPAGE_CONSULTA_FILTRO");
            oDataBase.AddInParameter(oDbCommand, "ENT_TYPE", DbType.String, tipoPersona);
            oDataBase.AddInParameter(oDbCommand, "@TAXT_ID", DbType.Decimal, tipoId);
            oDataBase.AddInParameter(oDbCommand, "@TAX_ID", DbType.String, nro_tipo);
            oDataBase.AddInParameter(oDbCommand, "@BPS_NAME", DbType.String, nombre);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddInParameter(oDbCommand, "@BPS_USER", DbType.String, BPS_USER);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ASSOCIATION", DbType.String, BPS_ASSOCIATION);
            oDataBase.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.String, BPS_GROUP);
            oDataBase.AddInParameter(oDbCommand, "@BPS_COLLECTOR", DbType.String, BPS_COLLECTOR);
            oDataBase.AddInParameter(oDbCommand, "@BPS_SUPPLIER", DbType.String, BPS_SUPPLIER);
            oDataBase.AddInParameter(oDbCommand, "@BPS_EMPLOYEE", DbType.String, BPS_EMPLOYEE);
            oDataBase.AddInParameter(oDbCommand, "@ID_UBIGEO", DbType.Decimal, ubigeo);
            //oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado); @BPS_ENDS
            oDataBase.AddInParameter(oDbCommand, "@BPS_ENDS", DbType.Int32, estado);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("USP_SOCIOS_LISTARPAGE_CONSULTA_FILTRO", tipoPersona,
                                                                                                tipoId,
                                                                                                nro_tipo,
                                                                                                nombre,
                                                                                                pagina,
                                                                                                cantRegxPag,
                                                                                                BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE,
                                                                                                ubigeo,
                                                                                                estado,
                                                                                                ParameterDirection.Output
                                                                                               );

            var lista = new List<SocioNegocio>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new SocioNegocio(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<SocioNegocio> GET_REC_BUSINESS_PARTNER_GRAL()
        {
            List<SocioNegocio> lst = new List<SocioNegocio>();
            SocioNegocio item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_REC_BUSINESS_PARTNER_GRAL"))
                {
                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new SocioNegocio();
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            item.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            {
                                item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                            {
                                item.BPS_FIRST_NAME = dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                            {
                                item.BPS_FATH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME"));
                            }
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                            {
                                item.BPS_MOTH_SURNAME = dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME"));
                            }
                            lst.Add(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }
        public bool existeSocioXDocumento(string Owner, decimal idTipoDocumento, string nroDocumento)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_EXISTE_DOCUMENTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@TAXT_ID", DbType.Decimal, idTipoDocumento);
                oDataBase.AddInParameter(cm, "@TAX_ID", DbType.String, nroDocumento);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));


            }
            return existe;
        }
        public bool existeSocioXDocumento(string Owner, decimal idTipoDocumento, string nroDocumento, decimal bpsId)
        {
            bool existe = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_EXISTE_DOC_X_SOCIO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(cm, "@TAXT_ID", DbType.Decimal, idTipoDocumento);
                oDataBase.AddInParameter(cm, "@TAX_ID", DbType.String, nroDocumento);
                oDataBase.AddInParameter(cm, "@BPS_ID", DbType.Decimal, bpsId);
                oDataBase.AddOutParameter(cm, "@RETORNO", DbType.Boolean, 4);
                int n = oDataBase.ExecuteNonQuery(cm);
                existe = Convert.ToBoolean(oDataBase.GetParameterValue(cm, "@RETORNO"));


            }
            return existe;
        }
        public SocioNegocio ObtenerTipoPersonaXCodigoLic(decimal idLicencia, string owner)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            SocioNegocio item = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_TIPO_PERSONA"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLicencia);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            item = new SocioNegocio();
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            item.ENT_TYPE = Convert.ToChar(dr.GetString(dr.GetOrdinal("ENT_TYPE")));
                        }
                    }
                }
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region GRUPO EMPRESARIAL
        public List<SocioNegocio> BuscarSocioEmpresarial(string tipoPersona,
                                                decimal? tipoId,
                                                string nro_tipo,
                                                string nombre,
                                                decimal ubigeo,
                                                int estado,
                                                int pagina,
                                                int cantRegxPag, string BPS_USER, string BPS_ASSOCIATION, string BPS_GROUP, string BPS_COLLECTOR, string BPS_SUPPLIER, string BPS_EMPLOYEE, string nombreComercial)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            //DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_SOCIOS_LISTARPAGE");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("USP_SOCIOS_EMPRESARIAL_LISTARPAGE_CONSULTA_FILTRO");
            oDataBase.AddInParameter(oDbCommand, "ENT_TYPE", DbType.String, tipoPersona);
            oDataBase.AddInParameter(oDbCommand, "@TAXT_ID", DbType.Decimal, tipoId);
            oDataBase.AddInParameter(oDbCommand, "@TAX_ID", DbType.String, nro_tipo);
            oDataBase.AddInParameter(oDbCommand, "@BPS_NAME", DbType.String, nombre);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddInParameter(oDbCommand, "@BPS_USER", DbType.String, BPS_USER);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ASSOCIATION", DbType.String, BPS_ASSOCIATION);
            oDataBase.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.String, BPS_GROUP);
            oDataBase.AddInParameter(oDbCommand, "@BPS_COLLECTOR", DbType.String, BPS_COLLECTOR);
            oDataBase.AddInParameter(oDbCommand, "@BPS_SUPPLIER", DbType.String, BPS_SUPPLIER);
            oDataBase.AddInParameter(oDbCommand, "@BPS_EMPLOYEE", DbType.String, BPS_EMPLOYEE);
            oDataBase.AddInParameter(oDbCommand, "@ID_UBIGEO", DbType.Decimal, ubigeo);
            oDataBase.AddInParameter(oDbCommand, "@BPS_TRADE_NAME", DbType.String, nombreComercial);
            //oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado); @BPS_ENDS
            oDataBase.AddInParameter(oDbCommand, "@BPS_ENDS", DbType.Int32, estado);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("USP_SOCIOS_EMPRESARIAL_LISTARPAGE_CONSULTA_FILTRO", tipoPersona,
                                                                                                tipoId,
                                                                                                nro_tipo,
                                                                                                nombre,
                                                                                                pagina,
                                                                                                cantRegxPag,
                                                                                                BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE,
                                                                                                ubigeo,
                                                                                                estado,
                                                                                                nombreComercial,
                                                                                                ParameterDirection.Output
                                                                                               );

            var lista = new List<SocioNegocio>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new SocioNegocio(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        #endregion



        #region DescuentosSocio
        //Lista BPSID por Grupo Empresarial

        public List<SocioNegocio> ListaBPSIDxGrupoEmpresarial(string owner, decimal bpsid)
        {
            List<SocioNegocio> lista = new List<SocioNegocio>();
            SocioNegocio entidad = null;
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_SOCIOS_X_GRUPO_EMPRESARIAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@BPSID", DbType.Decimal, bpsid);
            using (IDataReader dr = (oDataBase.ExecuteReader(oDbCommand)))
            {
                while (dr.Read())
                {
                    entidad = new SocioNegocio();

                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        entidad.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                    lista.Add(entidad);
                }
                return lista;
            }
        }

        //Valida Si el Socio Tiene Grupo Empresarial
        public int ValidaSocioEmpresarial(string owner, decimal bpsid)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDA_GRUPO_EMPRESARIAL");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, bpsid);

            int r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));

            return r;
        }


        #region UsuarioDerecho
        //LIstar Licencias Por socio derecho pestaña usuario.derecho 
        public List<BELicencias> ListarLicenciasxBPSID(string owner, decimal? BPSID, int pag, int cantxregpag)
        {
            List<BELicencias> LISTA = null;
            BELicencias entidad = null;
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_REC_LICENCES_GRAL_SOCIO_DERECHO");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPSID);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);
            String Results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            //**** Este bloque se crea por que cuando ejecuto el nonQuery ya no puedo ejecutar el Execute reader... 
            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_REC_LICENCES_GRAL_SOCIO_DERECHO");
            oDataBase1.AddInParameter(oDbCommand1, "@owner", DbType.String, owner);
            oDataBase1.AddInParameter(oDbCommand1, "@BPS_ID", DbType.Decimal, BPSID);
            oDataBase1.AddInParameter(oDbCommand1, "@PageIndex", DbType.Int32, pag);
            oDataBase1.AddInParameter(oDbCommand1, "@PageSize", DbType.Int32, cantxregpag);
            oDataBase1.AddOutParameter(oDbCommand1, "@RecordCount", DbType.Int32, 50);
            //*****************************************************************************************************
            using (IDataReader dr = oDataBase1.ExecuteReader(oDbCommand1)) //aqui pongo el nuevo OdbCommand1 que obtuves
            {
                LISTA = new List<BELicencias>();
                while (dr.Read())
                {
                    entidad = new BELicencias();

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                        entidad.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                        entidad.MONEDA = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TIPOPAGO")))
                        entidad.TIPOPAGO = dr.GetString(dr.GetOrdinal("TIPOPAGO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVG_DESC")))
                        entidad.INVG_DESC = dr.GetString(dr.GetOrdinal("INVG_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        entidad.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                        entidad.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        entidad.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        entidad.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                    entidad.TotalVirtual = Convert.ToInt32(Results);

                    LISTA.Add(entidad);

                }
            }

            return LISTA;
        }

        //establecimientos
        public List<BEEstablecimiento> ListarEstablecimientosxBPSID(string owner, decimal? BPSID, int pag, int cantxregpag)
        {
            List<BEEstablecimiento> LISTA = null;
            BEEstablecimiento entidad = null;
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_REC_ESTABLISHMENT_GRAL_SOCIO_DERECHO");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPSID);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);
            String Results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            //**** Este bloque se crea por que cuando ejecuto el nonQuery ya no puedo ejecutar el Execute reader... 
            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_REC_ESTABLISHMENT_GRAL_SOCIO_DERECHO");
            oDataBase1.AddInParameter(oDbCommand1, "@owner", DbType.String, owner);
            oDataBase1.AddInParameter(oDbCommand1, "@BPS_ID", DbType.Decimal, BPSID);
            oDataBase1.AddInParameter(oDbCommand1, "@PageIndex", DbType.Int32, pag);
            oDataBase1.AddInParameter(oDbCommand1, "@PageSize", DbType.Int32, cantxregpag);
            oDataBase1.AddOutParameter(oDbCommand1, "@RecordCount", DbType.Int32, 50);
            //*****************************************************************************************************
            using (IDataReader dr = oDataBase1.ExecuteReader(oDbCommand1)) //aqui pongo el nuevo OdbCommand1 que obtuves
            {
                LISTA = new List<BEEstablecimiento>();
                while (dr.Read())
                {
                    entidad = new BEEstablecimiento();

                    if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                        entidad.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                        entidad.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        entidad.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDAT")))
                        entidad.LOG_USER_UPDAT = dr.GetString(dr.GetOrdinal("LOG_USER_UPDAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        entidad.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        entidad.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                    entidad.TotalVirtual = Convert.ToInt32(Results);

                    LISTA.Add(entidad);

                }
            }

            return LISTA;
        }
        #endregion

        #endregion

        public List<SocioNegocio> ObtenerSocioxRuc(string ruc, string razsocial)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_SOCIO_DUPLICADO");
            db.AddInParameter(oDbCommand, "@TAX_ID", DbType.String, ruc);
            db.AddInParameter(oDbCommand, "@BPS_NAME", DbType.String, razsocial);
            var lista = new List<SocioNegocio>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                SocioNegocio valor = null;
                while (dr.Read())
                {
                    valor = new SocioNegocio();
                    valor.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    valor.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    lista.Add(valor);
                }
            }
            return lista;
        }


        public bool AgruparSocio(string owner, decimal bpsorigen, decimal bpsid)
        {
            bool exito = false;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_ASOCIAR_SOCIO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPS_ORIGEN", DbType.Decimal, bpsorigen);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, bpsid);
            exito = db.ExecuteNonQuery(oDbCommand) > 0;
            return exito;
        }

        public bool VerificarSocio(string owner, decimal bpsid, Boolean Bps_Verificado, string UsuarioVerificado)
        {
            bool exito = false;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_VERIFICAR_SOCIO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, bpsid);
            db.AddInParameter(oDbCommand, "@BPS_VERIFICADO", DbType.Boolean, Bps_Verificado);
            db.AddInParameter(oDbCommand, "@USUARIO_VERIFICADO", DbType.String, UsuarioVerificado);
            exito = db.ExecuteNonQuery(oDbCommand) > 0;
            return exito;
        }


        public int ValidaPermisoModificacionSocion()
        {
            int res = 0;

            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTIENE_APROBACION_MODIF_SOCIO");
            res = Convert.ToInt32( db.ExecuteScalar(cm));
            return res;
        }
    }
}
