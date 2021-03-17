using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities;

namespace SGRDA.DA
{
    public class DAEstablecimiento
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEEstablecimiento> Listar_Establecimiento_Principal(string Owner, decimal tipoEst, decimal? subTipoEst, decimal? IdEstablecimiento, string nombre, int st,decimal bpsId,
            decimal division, decimal subtipo1, decimal subtipo2, decimal subtipo3,int pagina, int cantRegxPag)
        {
            if (IdEstablecimiento == null) IdEstablecimiento = 0;
            if (subTipoEst == null) subTipoEst = 0;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_ESTABLECIMIENTO_PRINCIPAL");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, tipoEst);
            db.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, subTipoEst);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, IdEstablecimiento);
            db.AddInParameter(oDbCommand, "@EST_NAME", DbType.String, nombre);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, bpsId);

            db.AddInParameter(oDbCommand, "@DIVISION", DbType.Decimal, division);
            db.AddInParameter(oDbCommand, "@SUBTIPO1", DbType.Decimal, subtipo1);
            db.AddInParameter(oDbCommand, "@SUBTIPO2", DbType.Decimal, subtipo2);
            db.AddInParameter(oDbCommand, "@SUBTIPO3", DbType.Decimal, subtipo3);

            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);

            var lista = new List<BEEstablecimiento>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEEstablecimiento(reader, reader.GetInt32(reader.GetOrdinal("CANTIDAD"))));
            }
            return lista;
        }

        public List<BEEstablecimiento> usp_Get_PorSocioNegocioPage(string Owner, decimal? IdSocio, int st, int pagina, int cantRegxPag)
        {
            if (IdSocio == null) IdSocio = 0;
            DbCommand oDbCommand = db.GetStoredProcCommand("usp_REC_ESTABLISHMENT_GRAL_X_Socio_Page");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, IdSocio);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            //db.ExecuteNonQuery(oDbCommand);

            //string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("usp_REC_ESTABLISHMENT_GRAL_X_Socio_Page", Owner, IdSocio, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEEstablecimiento>();

            //using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEEstablecimiento(reader, reader.GetInt32(reader.GetOrdinal("CANTIDAD"))));
                //lista.Add(new BEEstablecimiento(reader, Convert.ToInt32(results)));

            }
            return lista;


        }

        public List<BEEstablecimiento> usp_Get_PorEstablecimientoPage(string Owner, decimal tipoEst, decimal? subTipoEst, decimal? IdEstablecimiento, string nombre, int st, int pagina, int cantRegxPag)
        {
            if (IdEstablecimiento == null) IdEstablecimiento = 0;
            if (subTipoEst == null) subTipoEst = 0;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_POR_ESTABLECIMIENTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, tipoEst);
            db.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, subTipoEst);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, IdEstablecimiento);
            db.AddInParameter(oDbCommand, "@EST_NAME", DbType.String, nombre);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            //db.ExecuteNonQuery(oDbCommand);

            //string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            //Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            //DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_POR_ESTABLECIMIENTO", Owner, tipoEst, subTipoEst, IdEstablecimiento, nombre, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEEstablecimiento>();

            //using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEEstablecimiento(reader, reader.GetInt32(reader.GetOrdinal("CANTIDAD"))));
                //lista.Add(new BEEstablecimiento(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEEstablecimiento> usp_Get_DivisionAdministrativaPage(string owner, string divTipo, decimal? divAdmin, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_POR_DIVADMINISTRATIVA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, divAdmin == null ? 0 : divAdmin);
            db.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, divTipo == "0" ? string.Empty : divTipo);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEEstablecimiento>();

            using (IDataReader reader = db.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                    lista.Add(new BEEstablecimiento(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEEstablecimiento> usp_Get_PorDireccionesPage(string Owner, string NombreViaDir, decimal tipodireccion, decimal TipoUrbanizacion, string NombreUrbanizacion, string Manzana, decimal? Numero, string Lote, string TipoInterior, string NumeroInterior, string CodigoViaDir, decimal TipoEtapa, string NombreEtapa, decimal TerritorioDir, string ReferenciaDir, decimal? ubigeo, int st, int pagina, int cantRegxPag)
        {
            if (ubigeo == null) ubigeo = 0;
            if (TipoInterior == "0") TipoInterior = "";
            if (Numero == null) Numero = 0;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_POR_DRECCION_PAGE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, Owner);
            db.AddInParameter(oDbCommand, "@ROU_NAME", DbType.String, NombreViaDir);
            db.AddInParameter(oDbCommand, "@ADD_TYPE", DbType.Decimal, tipodireccion);
            db.AddInParameter(oDbCommand, "@HOU_TURZN", DbType.Decimal, TipoUrbanizacion);
            db.AddInParameter(oDbCommand, "@HOU_URZN", DbType.String, NombreUrbanizacion);
            db.AddInParameter(oDbCommand, "@HOU_MZ", DbType.String, Manzana);
            db.AddInParameter(oDbCommand, "@HOU_NRO", DbType.Decimal, Numero);
            db.AddInParameter(oDbCommand, "@HOU_LOT", DbType.String, Lote);
            db.AddInParameter(oDbCommand, "@ADD_TINT", DbType.String, TipoInterior);
            db.AddInParameter(oDbCommand, "@ADD_INT", DbType.String, NumeroInterior);
            db.AddInParameter(oDbCommand, "@ROU_ID", DbType.Decimal, CodigoViaDir);
            db.AddInParameter(oDbCommand, "@HOU_TETP", DbType.Decimal, TipoEtapa);
            db.AddInParameter(oDbCommand, "@HOU_NETP", DbType.String, NombreEtapa);
            db.AddInParameter(oDbCommand, "@TIS_N", DbType.Decimal, TerritorioDir);
            db.AddInParameter(oDbCommand, "@ADD_REFER", DbType.String, ReferenciaDir);
            db.AddInParameter(oDbCommand, "@GEO_ID", DbType.Decimal, ubigeo);
            db.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            //--falta codigo postal investigar CPO_ID
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, 50);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_POR_DRECCION_PAGE", Owner, NombreViaDir, tipodireccion, TipoUrbanizacion, NombreUrbanizacion, Manzana, Numero, Lote, TipoInterior, NumeroInterior, CodigoViaDir, TipoEtapa, NombreEtapa, TerritorioDir, ReferenciaDir, ubigeo, st, pagina, cantRegxPag, ParameterDirection.Output);

            var lista = new List<BEEstablecimiento>();

            using (IDataReader reader = oDataBase1.ExecuteReader(oDbCommand1))
            {
                while (reader.Read())
                    lista.Add(new BEEstablecimiento(reader, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEEstablecimiento> UPS_BUSCAR_ESTABLECIMIENTO_X_NOMBRE(string Owner, string datos)
        {
            List<BEEstablecimiento> lst = new List<BEEstablecimiento>();
            BEEstablecimiento item = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("UPS_BUSCAR_ESTABLECIMIENTO_X_NOMBRE"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                    db.AddInParameter(cm, "@DATOS", DbType.String, datos);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            item = new BEEstablecimiento();
                            item.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                            item.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
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

        public int Insertar(BEEstablecimiento en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_ESTABLECIMIENTO_GRAL");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddOutParameter(oDbCommand, "@EST_ID", DbType.Decimal, Convert.ToInt32(en.EST_ID));
                db.AddInParameter(oDbCommand, "@EST_NAME", DbType.String, en.EST_NAME);
                db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.String, en.ESTT_ID);
                db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, en.DAD_ID);
                db.AddInParameter(oDbCommand, "@SUBE_ID", DbType.String, en.SUBE_ID);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
                db.AddInParameter(oDbCommand, "@LATI", DbType.String, en.LATITUD);
                db.AddInParameter(oDbCommand, "@LONG", DbType.String, en.LONGITUD);
                db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
                int n = db.ExecuteNonQuery(oDbCommand);
                int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@EST_ID"));

                return id;
            }
            catch (Exception ex )
            {
                return 0;
            }
        }

        public int Update(BEEstablecimiento en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ESTABLECIMIENTO_GRAL");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.String, en.EST_ID);
                db.AddInParameter(oDbCommand, "@EST_NAME", DbType.String, en.EST_NAME);
                db.AddInParameter(oDbCommand, "@ESTT_ID", DbType.String, en.ESTT_ID);
                db.AddInParameter(oDbCommand, "@DAD_ID", DbType.String, en.DAD_ID);
                db.AddInParameter(oDbCommand, "@SUBE_ID", DbType.String, en.SUBE_ID);
                db.AddInParameter(oDbCommand, "@DIF_ID", DbType.String, en.DIF_ID);
                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.String, en.BPS_ID);
                db.AddInParameter(oDbCommand, "@LATI", DbType.String, en.LATITUD);
                db.AddInParameter(oDbCommand, "@LONG", DbType.String, en.LONGITUD);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception ex )
            {
                return 0;
            }
        }

        //public int InsertarEstablecimientoSocio(BEEstablecimiento en)
        //{
        //    try
        //    {
        //        DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_ESTABLECIMIENTO_SOCIO");
        //        db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
        //        db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, en.BPS_ID);
        //        db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, en.EST_ID);
        //        db.AddInParameter(oDbCommand, "@ROL_ID", DbType.String, en.ROL_ID);
        //        db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT);
        //        int r = db.ExecuteNonQuery(oDbCommand);
        //        return r;
        //    }
        //    catch (Exception)
        //    {
        //        return 0; 
        //    }
        //}

        public BEEstablecimiento ObetnerNombre(string owner, decimal idEstablecimiento)
        {
            BEEstablecimiento establecimiento = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_NOMBRE_EST"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    db.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEstablecimiento);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            establecimiento = new BEEstablecimiento();
                            establecimiento.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return establecimiento;
        }

        public BEEstablecimiento cabeceraEstablecimiento(decimal idEstablecimiento, string owner)
        {
            BEEstablecimiento establecimiento = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_ESTABLECIMIENTO_CAB"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    db.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEstablecimiento);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            establecimiento = new BEEstablecimiento();
                            if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                                establecimiento.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                                establecimiento.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                                establecimiento.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                            if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                                establecimiento.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                            if (!dr.IsDBNull(dr.GetOrdinal("TAXT_ID")))
                                establecimiento.TAXT_ID = dr.GetDecimal(dr.GetOrdinal("TAXT_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ESTT_ID")))
                                establecimiento.ESTT_ID = dr.GetDecimal(dr.GetOrdinal("ESTT_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SUBE_ID")))
                                establecimiento.SUBE_ID = dr.GetDecimal(dr.GetOrdinal("SUBE_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DAD_ID")))
                                establecimiento.DAD_ID = dr.GetDecimal(dr.GetOrdinal("DAD_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                                establecimiento.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("LATITUD")))
                                establecimiento.LATITUD = dr.GetString(dr.GetOrdinal("LATITUD"));
                            if (!dr.IsDBNull(dr.GetOrdinal("LONGITUD")))
                                establecimiento.LONGITUD = dr.GetString(dr.GetOrdinal("LONGITUD"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return establecimiento;
        }

        public int InactivarEstablecimiento(BEEstablecimiento en)
        {
            try
            {
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_INACTIVAR_EST");
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
                db.AddInParameter(oDbCommand, "@EST_ID", DbType.String, en.EST_ID);
                db.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, en.LOG_USER_UPDAT);
                int r = db.ExecuteNonQuery(oDbCommand);
                return r;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<BEEstablecimiento> ConsultaGeneralEstablecimiento(
                                        decimal Tipoestablecimiento,
                                        decimal? SubTipoestableimiento,
                                        string Nombreestablecimiento,
                                        decimal Socio,
                                        string Tipodivision,
                                        decimal? Division,
                                        int estado,
                                        decimal oficina,
                                        int pagina,
                                        int cantRegxPag)
        {
            try
            {

                if (SubTipoestableimiento == null) SubTipoestableimiento = 0;
                if (Division == null) Division = 0;
                if (Tipodivision == "0") Tipodivision = string.Empty;
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_ESTABLECIMIENTO_CONGENERAL_LISTARPAGE");
                //DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_ESTABLECIMIENTO_CONGENERAL_LISTARPAGE2");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
                oDataBase.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, Tipoestablecimiento);
                oDataBase.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, SubTipoestableimiento);
                oDataBase.AddInParameter(oDbCommand, "@EST_NAME", DbType.String, Nombreestablecimiento);
                oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, Socio);
                oDataBase.AddInParameter(oDbCommand, "@DAD_TYPE", DbType.String, Tipodivision);
                oDataBase.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, Division);
                oDataBase.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);
                oDataBase.AddInParameter(oDbCommand, "@OFICINA", DbType.Decimal, oficina);
                oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
                oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
                oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
                //oDataBase.ExecuteNonQuery(oDbCommand);

                //string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
                var lista = new List<BEEstablecimiento>();
                //string results = "100";
                using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (reader.Read())
                        lista.Add(new BEEstablecimiento(reader, reader.GetInt32(reader.GetOrdinal("CANTIDAD")) ));
                    //lista.Add(new BEEstablecimiento(reader, Convert.ToInt32(results)));
                }
                return lista;
            } catch (Exception ex) {
                return null;
            }
        }
        //NOTA DAVID
        public List<BEEstablecimiento> ConsultaEstablecimientoSocioEmpr(
                                        decimal Socio,
                                        int pagina,
                                        int cantRegxPag)
        {

            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_ESTABLECIMIENTO_X_SOCIO");
            oDataBase.AddInParameter(oDbCommand, "@IdSocio", DbType.Decimal, Socio);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<BEEstablecimiento>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                BEEstablecimiento establecimiento = null;
                while (reader.Read())
                {
                    establecimiento.OWNER = reader.GetString(reader.GetOrdinal("OWNER"));
                    establecimiento.EST_NAME = reader.GetString(reader.GetOrdinal("EST_NAME"));
                    establecimiento.EST_ID = reader.GetDecimal(reader.GetOrdinal("EST_ID"));
                    establecimiento.DAD_ID = reader.GetInt32(reader.GetOrdinal("DAD_ID"));
                    establecimiento.ESTT_ID = reader.GetInt32(reader.GetOrdinal("ESTT_ID"));
                    establecimiento.SUBE_ID = reader.GetInt32(reader.GetOrdinal("SUBE_ID"));
                    lista.Add(establecimiento);
                }
            }
            return lista;
        }



        //DAVID
        #region EstablecimientoSocioTempora
        public List<BEEstablecimiento> ConsultaEstablecimientoSocioEmpresarial(
                                decimal Socio,decimal?licmas)
        {

            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_ESTABLECIMIENTO_X_SOCIO_EMP");
            oDataBase.AddInParameter(oDbCommand, "@IdSocio", DbType.Decimal, Socio);
            oDataBase.AddInParameter(oDbCommand, "@licmaster", DbType.Decimal, licmas);
            oDataBase.ExecuteNonQuery(oDbCommand);

            var lista = new List<BEEstablecimiento>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                BEEstablecimiento establecimiento = null;
                while (reader.Read())
                {
                    establecimiento = new BEEstablecimiento();
                    //establecimiento.OWNER = reader.GetString(reader.GetOrdinal("OWNER"));
                    if (!reader.IsDBNull(reader.GetOrdinal("EST_NAME")))
                        establecimiento.EST_NAME = reader.GetString(reader.GetOrdinal("EST_NAME"));
                    if (!reader.IsDBNull(reader.GetOrdinal("EST_ID")))
                        establecimiento.EST_ID = reader.GetDecimal(reader.GetOrdinal("EST_ID"));


                    //establecimiento.DAD_ID = reader.GetInt32(reader.GetOrdinal("DAD_ID"));
                    //establecimiento.ESTT_ID = reader.GetInt32(reader.GetOrdinal("ESTT_ID"));
                    //establecimiento.SUBE_ID = reader.GetInt32(reader.GetOrdinal("SUBE_ID"));
                    lista.Add(establecimiento);
                }
            }
            return lista;
        }
        #endregion

        public List<BEEstablecimiento> ConsultaEstablecimiento(string owner, decimal idEst, string nombre, decimal idSoc,
                                                         decimal tipo, decimal subTipo, decimal idDivision,
                                                          decimal ubigeo, decimal pagina, decimal cantRegistro)
        {
            BEEstablecimiento establecimiento = null;
            List<BEEstablecimiento> lista = new List<BEEstablecimiento>();
            using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_CONSULTA_ESTABLECIMIENTO"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEst);
                db.AddInParameter(cm, "@EST_NAME", DbType.String, nombre);
                db.AddInParameter(cm, "@BPS_ID", DbType.Decimal, idSoc);
                db.AddInParameter(cm, "@ESTT_ID", DbType.Decimal, tipo);
                db.AddInParameter(cm, "@SUBE_ID", DbType.Decimal, subTipo);
                db.AddInParameter(cm, "@DAD_ID", DbType.Decimal, idDivision);
                db.AddInParameter(cm, "@UBIGEO", DbType.Decimal, ubigeo);
                db.AddInParameter(cm, "@PageIndex", DbType.Int32, pagina);
                db.AddInParameter(cm, "@PageSize", DbType.Int32, cantRegistro);
                db.AddOutParameter(cm, "@RecordCount", DbType.Int32, 0);
                db.ExecuteNonQuery(cm);
                string results = Convert.ToString(db.GetParameterValue(cm, "@RecordCount"));

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        establecimiento = new BEEstablecimiento();
                        establecimiento.EST_ID = dr.GetDecimal(dr.GetOrdinal("ID"));
                        establecimiento.EST_NAME = dr.GetString(dr.GetOrdinal("ESTABLECIMIENTO"));
                        establecimiento.EST_TYPE = dr.GetString(dr.GetOrdinal("TIPO"));
                        establecimiento.EST_SUBTYPE = dr.GetString(dr.GetOrdinal("SUBTIPO"));
                        establecimiento.BPS_NAME = dr.GetString(dr.GetOrdinal("SOCIO"));

                        establecimiento.DIVISION = dr.GetString(dr.GetOrdinal("DIVISION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DIRECCION")))
                            establecimiento.ADDRESS = dr.GetString(dr.GetOrdinal("DIRECCION"));
                        establecimiento.TotalVirtual = Convert.ToInt32(results);
                        lista.Add(establecimiento);


                    }
                }
            }
            return lista;
        }

        public BEEstablecimiento CabeceraConsultaEst(decimal idEstablecimiento, string owner)
        {
            BEEstablecimiento establecimiento = null;
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_CAB_CONSULTA_EST"))
                {
                    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    db.AddInParameter(cm, "@EST_ID", DbType.Decimal, idEstablecimiento);

                    using (IDataReader dr = db.ExecuteReader(cm))
                    {
                        if (dr.Read())
                        {
                            establecimiento = new BEEstablecimiento();
                            if (!dr.IsDBNull(dr.GetOrdinal("EST_ID")))
                                establecimiento.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                                establecimiento.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SOCIO")))
                                establecimiento.BPS_NAME = dr.GetString(dr.GetOrdinal("SOCIO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("TIPO")))
                                establecimiento.EST_TYPE = dr.GetString(dr.GetOrdinal("TIPO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("SUBTIPO")))
                                establecimiento.EST_SUBTYPE = dr.GetString(dr.GetOrdinal("SUBTIPO"));
                            if (!dr.IsDBNull(dr.GetOrdinal("DIVISION")))
                                establecimiento.DIVISION = dr.GetString(dr.GetOrdinal("DIVISION"));
                            if (!dr.IsDBNull(dr.GetOrdinal("ADDRESS")))
                                establecimiento.ADDRESS = dr.GetString(dr.GetOrdinal("ADDRESS"));
                            if (!dr.IsDBNull(dr.GetOrdinal("GEO_ID")))
                                establecimiento.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                            if (!dr.IsDBNull(dr.GetOrdinal("TIS_N")))
                                establecimiento.TIS_N = dr.GetDecimal(dr.GetOrdinal("TIS_N"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return establecimiento;
        }

        #region CADENAS

        //
        public List<BEEstablecimiento> ConsultaEstablecimientoSocioEmpresarialGrabados(
                                decimal Socio, decimal licmas)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_EST_MULT_GRAB");
            oDataBase.AddInParameter(oDbCommand, "@BPSID", DbType.Decimal, Socio);
            oDataBase.AddInParameter(oDbCommand, "@LICMAS", DbType.Decimal, licmas);
            oDataBase.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEEstablecimiento>();
            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                BEEstablecimiento establecimiento = null;
                while (reader.Read())
                {
                    establecimiento = new BEEstablecimiento();
                    //establecimiento.OWNER = reader.GetString(reader.GetOrdinal("OWNER"));
                    if (!reader.IsDBNull(reader.GetOrdinal("EST_NAME")))
                        establecimiento.EST_NAME = reader.GetString(reader.GetOrdinal("EST_NAME"));
                    if (!reader.IsDBNull(reader.GetOrdinal("EST_ID")))
                        establecimiento.EST_ID = reader.GetDecimal(reader.GetOrdinal("EST_ID"));


                    //establecimiento.DAD_ID = reader.GetInt32(reader.GetOrdinal("DAD_ID"));
                    //establecimiento.ESTT_ID = reader.GetInt32(reader.GetOrdinal("ESTT_ID"));
                    //establecimiento.SUBE_ID = reader.GetInt32(reader.GetOrdinal("SUBE_ID"));
                    lista.Add(establecimiento);
                }
            }
            return lista;
        }
        //Validar Establecimiento Si Posee Caracteristicas
        public int ValidarEstablecimientoCaracteristicas(int IdEst)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SP_VALIDAR_ESTABLECIMIENTO_CARACTERISTICAS");
            db.AddInParameter(oDbCommand, "@IDEST", DbType.Int32, IdEst);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        //Listar Caracteristicas Predefinidas por Establecimiento
        public List<BECaracteristicaEst> Listar_Caracteristicas_PredefinidasxEst(decimal idTipEst, decimal idSubTipEst)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_CARACTERISTICA_PREDEFINIDA");
            oDataBase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, idTipEst);
            oDataBase.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, idSubTipEst);

            oDataBase.ExecuteNonQuery(oDbCommand);

            var lista = new List<BECaracteristicaEst>();

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                BECaracteristicaEst caracteristica = null;
                while (reader.Read())
                {
                    caracteristica = new BECaracteristicaEst();
                    //establecimiento.OWNER = reader.GetString(reader.GetOrdinal("OWNER"));
                    if (!reader.IsDBNull(reader.GetOrdinal("CHAR_ID")))
                        caracteristica.CHAR_ID = reader.GetDecimal(reader.GetOrdinal("CHAR_ID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("CHAR_SHORT")))
                        caracteristica.CHAR_LONG = reader.GetString(reader.GetOrdinal("CHAR_SHORT"));
                    if (!reader.IsDBNull(reader.GetOrdinal("EST_ID")))
                        caracteristica.EST_ID = reader.GetDecimal(reader.GetOrdinal("EST_ID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("ESTT_ID")))
                        caracteristica.ESTT_ID = reader.GetDecimal(reader.GetOrdinal("ESTT_ID"));
                    if (!reader.IsDBNull(reader.GetOrdinal("LOG_DATE_CREAT")))
                        caracteristica.LOG_DATE_CREAT = reader.GetDateTime(reader.GetOrdinal("LOG_DATE_CREAT"));
                    if (!reader.IsDBNull(reader.GetOrdinal("LOG_DATE_UPDATE")))
                        caracteristica.LOG_DATE_UPDATE = reader.GetDateTime(reader.GetOrdinal("LOG_DATE_UPDATE"));
                    //if (!reader.IsDBNull(reader.GetOrdinal("LOG_USER_CREAT")))
                    //   caracteristica.LOG_USER_CREAT = reader.GetDateTime(reader.GetOrdinal("LOG_USER_CREAT"));
                    //if (!reader.IsDBNull(reader.GetOrdinal("LOG_USER_UPDATE")))
                    //   caracteristica.LOG_USER_UPDATE = reader.GetDateTime(reader.GetOrdinal("LOG_USER_UPDATE"));
                    lista.Add(caracteristica);
                }

            }
            return lista;
        }

        //Obtener Caracteristicas Registradas de Establecimientos Para Alamacenar en la licencia
        public List<BECaracteristicaLic> ListaDecarcatxLic(decimal IdLic) {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_CARACT_PREDEF_X_LIC_EST");
            oDataBase.AddInParameter(oDbCommand, "@LICID", DbType.Decimal, IdLic);
            oDataBase.ExecuteNonQuery(oDbCommand);
            var lista = new List<BECaracteristicaLic>();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                BECaracteristicaLic caracteristica = null;
                while (dr.Read())
                {

                    caracteristica = new BECaracteristicaLic();
                    if (!dr.IsDBNull(dr.GetOrdinal("CHAR_ID")))
                        caracteristica.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_VAL_ORIGEN")))
                        caracteristica.LIC_VAL_ORIGEN = dr.GetString(dr.GetOrdinal("LIC_VAL_ORIGEN"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LICID")))
                        caracteristica.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LICID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("VALUE")))
                        caracteristica.LIC_CHAR_VAL = dr.GetDecimal(dr.GetOrdinal("VALUE"));

                    lista.Add(caracteristica);
                }

            }
            return lista;
        
        }


        #endregion


        #region Descuentos Socio

        public int ValidaEstablecimientoModif(decimal ESTID)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_VALIDA_ESTABLECIMIENTO_LICENCIA");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@ESTID", DbType.Decimal, ESTID);
            int r = 0;

            r =Convert.ToInt32( oDatabase.ExecuteScalar(oDbCommand));

            return r;
        }
        #endregion

        public List<BEEstablecimiento> ObtenerEstablecimientoxRuc(string ruc)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_ESTABLECIMIENTO_DUPLICADOS");
            db.AddInParameter(oDbCommand, "@TAX_ID", DbType.String, ruc);
            var lista = new List<BEEstablecimiento>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEEstablecimiento valor = null;
                while (dr.Read())
                {
                    valor = new BEEstablecimiento();
                    valor.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    valor.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    valor.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                    valor.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                    valor.GEO_ID = dr.GetDecimal(dr.GetOrdinal("GEO_ID"));
                    valor.UBIGEO = dr.GetString(dr.GetOrdinal("UBIGEO"));
                    lista.Add(valor);
                }
            }
            return lista;
        }

        public bool AgruparEstablecimiento(string owner, decimal estorigen, decimal estid)
        {
            bool exito = false;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_ASOCIAR_ESTABLECIMIENTO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@EST_ORIGEN", DbType.Decimal, estorigen);
            db.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, estid);
            exito = db.ExecuteNonQuery(oDbCommand) > 0;
            return exito;
        }

        
    }
}