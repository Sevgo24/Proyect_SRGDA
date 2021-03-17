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
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Configuration;

namespace SGRDA.DA
{
    public class DAArtista
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public List<BEArtista> ListaArtistaPaginada(string owner, int flag, string nombre, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_ARTISTA");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@flag", DbType.Int32, flag);
            db.AddInParameter(oDbCommand, "@nombre", DbType.String, nombre);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEArtista>();
            var item = new BEArtista();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEArtista();
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    item.COD_ARTIST_SQ = dr.GetDecimal(dr.GetOrdinal("COD_ARTIST_SQ"));
                    item.NAME = dr.GetString(dr.GetOrdinal("NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("IP_NAME")))
                        item.IP_NAME = dr.GetString(dr.GetOrdinal("IP_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FIRST_NAME")))
                        item.FIRST_NAME = dr.GetString(dr.GetOrdinal("FIRST_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ART_COMPLETE")))
                        item.ART_COMPLETE = dr.GetString(dr.GetOrdinal("ART_COMPLETE"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";

                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public int Eliminar(string id, string owner, string usuDel,decimal SHOW_ID)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASD_ARTIST");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@IP_NAME", DbType.String, id);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuDel);
            db.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, SHOW_ID);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        

        public int Solicitud_Eliminar_Activar(string id, string Observacion, string usuDel, decimal SHOW_ID,int tipo,decimal Artist_ID)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SOLICITUD_ELMINAR_Y_ACTIVAR_ARTISTA");
            db.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, SHOW_ID);
            db.AddInParameter(oDbCommand, "@IP_NAME", DbType.String, id);
            db.AddInParameter(oDbCommand, "@Artist_ID", DbType.Decimal, Artist_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREATE", DbType.String, usuDel);
            db.AddInParameter(oDbCommand, "@OBSERVACION", DbType.String, Observacion);
            db.AddInParameter(oDbCommand, "@Tipo", DbType.Int32, tipo);

            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Activar(string id, string owner, string usuDel,decimal SHOW_ID)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACTIVAR_ARTIST");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@IP_NAME", DbType.String, id);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuDel);
            db.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, SHOW_ID);

            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public int Prioridad(string id, string owner)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_PRIORIDAD_ARTIST");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@IP_NAME", DbType.String, id);

            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public decimal Insertar(decimal idShow, string nameIp, string ppalArtist, string owner, string usuario, string name)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_ARTIST");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, idShow);
            db.AddInParameter(oDbCommand, "@IP_NAME", DbType.String, nameIp);
            db.AddInParameter(oDbCommand, "@ARTIST_PPAL", DbType.String, ppalArtist);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, usuario);
            db.AddInParameter(oDbCommand, "@NAME", DbType.String, name); ;

            decimal r = Convert.ToDecimal( db.ExecuteScalar(oDbCommand));
            return r;
        }

        public decimal InsertarSolicitud(decimal idShow, string nameIp, string ppalArtist, string owner, string usuario, string name,string observacion)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_SOLICITUD_ARTISTA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, idShow);
            db.AddInParameter(oDbCommand, "@IP_NAME", DbType.String, nameIp);
            db.AddInParameter(oDbCommand, "@ARTIST_PPAL", DbType.String, ppalArtist);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, usuario);
            db.AddInParameter(oDbCommand, "@NAME", DbType.String, name); 
            db.AddInParameter(oDbCommand, "@OBSERVACION", DbType.String, observacion); 

            decimal r = Convert.ToDecimal(db.ExecuteScalar(oDbCommand));
            return r;
        }


        public BEArtista Obtener(decimal id, string owner)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_ARTISTA");
            db.AddInParameter(oDbCommand, "@COD_ARTIST_SQ", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            BEArtista item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEArtista();
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    item.COD_ARTIST_SQ = dr.GetDecimal(dr.GetOrdinal("COD_ARTIST_SQ"));

                    if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                        item.NAME = dr.GetString(dr.GetOrdinal("NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("IP_NAME")))
                        item.IP_NAME = dr.GetString(dr.GetOrdinal("IP_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FIRST_NAME")))
                        item.FIRST_NAME = dr.GetString(dr.GetOrdinal("FIRST_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ART_COMPLETE")))
                        item.ART_COMPLETE = dr.GetString(dr.GetOrdinal("ART_COMPLETE"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";
                }
            }
            return item;
        }

        public List<BEArtista> ListaArtistaSQL(string owner, int flag, string nombre, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_ARTISTA");
            db.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@flag", DbType.Int32, flag);
            db.AddInParameter(oDbCommand, "@nombre", DbType.String, nombre);
            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEArtista>();
            var item = new BEArtista();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEArtista();
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    item.COD_ARTIST_SQ = dr.GetDecimal(dr.GetOrdinal("COD_ARTIST_SQ"));
                    item.NAME = dr.GetString(dr.GetOrdinal("NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("IP_NAME")))
                        item.IP_NAME = dr.GetString(dr.GetOrdinal("IP_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FIRST_NAME")))
                        item.FIRST_NAME = dr.GetString(dr.GetOrdinal("FIRST_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ART_COMPLETE")))
                        item.ART_COMPLETE = dr.GetString(dr.GetOrdinal("ART_COMPLETE"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";

                    item.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(item);
                }
            }
            return lista;
        }

        private OracleConnection setearConexion()
        {
            try
            {
                OracleConnection oraConexion;
                oraConexion = new OracleConnection(ConfigurationManager.ConnectionStrings["ConexionDbOracle"].ToString());
                return oraConexion;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public List<BEArtista> ListaArtistaOracle(string owner, int flag, string nombre, int pagina, int cantRegxPag)
        {
            List<BEArtista> ListaArtista = new List<BEArtista>();
            OracleConnection oraCon = new OracleConnection();
            oraCon = setearConexion();


            OracleCommand oracmd = oraCon.CreateCommand();
            oracmd.CommandText = "SRGDA_ARTIST.ListarArtistas";
            oracmd.CommandType = CommandType.StoredProcedure;

            OracleParameter oraparFlag = new OracleParameter("p_Flag", OracleDbType.Varchar2);
            oraparFlag.Value = flag;
            oraparFlag.Direction = System.Data.ParameterDirection.Input;
            oracmd.Parameters.Add(oraparFlag);

            OracleParameter oraparNombre = new OracleParameter("p_Nombre", OracleDbType.Varchar2);
            oraparNombre.Value = nombre;
            oraparNombre.Direction = System.Data.ParameterDirection.Input;
            oracmd.Parameters.Add(oraparNombre);

            OracleParameter oraparPagina = new OracleParameter("p_Pagina", OracleDbType.Varchar2);
            oraparPagina.Value = pagina;
            oraparPagina.Direction = System.Data.ParameterDirection.Input;
            oracmd.Parameters.Add(oraparPagina);

            OracleParameter oraparCantRegistros = new OracleParameter("p_Registros", OracleDbType.Varchar2);
            oraparCantRegistros.Value = cantRegxPag;
            oraparCantRegistros.Direction = System.Data.ParameterDirection.Input;
            oracmd.Parameters.Add(oraparCantRegistros);

            OracleParameter oraparHC = new OracleParameter("p_tcArtista", OracleDbType.RefCursor);
            oraparHC.Direction = System.Data.ParameterDirection.Output;
            oracmd.Parameters.Add(oraparHC);

            try
            {
                oraCon.Open();
                OracleDataReader dr = oracmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        BEArtista item = new BEArtista();
                        item.COD_ARTIST_SQ = dr.GetDecimal(dr.GetOrdinal("COD_ARTIST_SQ"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                            item.NAME = dr.GetString(dr.GetOrdinal("NAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("IP_NAME")))
                            item.IP_NAME = dr.GetString(dr.GetOrdinal("IP_NAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FIRST_NAME")))
                            item.FIRST_NAME = dr.GetString(dr.GetOrdinal("FIRST_NAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ART_COMPLETE")))
                            item.ART_COMPLETE = dr.GetString(dr.GetOrdinal("ART_COMPLETE"));

                        item.ESTADO = "ACTIVO";

                        var cantidadreg = dr.GetDecimal(dr.GetOrdinal("CANTIDAD"));
                        item.TotalVirtual = Convert.ToInt32(cantidadreg);
                        ListaArtista.Add(item);
                    }
                }
                return ListaArtista;
            }
            catch (Exception e)
            {
                oraCon.Close();
                return null;
            }
        }


        public int InsertarGeneral(string owner, string name, string ipname, string firstname, string artcomplete, string user)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_ARTIST");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@NAME", DbType.String, name);
            db.AddInParameter(oDbCommand, "@IP_NAME", DbType.String, ipname);
            db.AddInParameter(oDbCommand, "@FIRST_NAME", DbType.String, firstname);
            db.AddInParameter(oDbCommand, "@ART_COMPLETE", DbType.String, artcomplete);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, user);

            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
        public BEArtista ObtenerXNombreCompleto(string nombreCompleto, string owner)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAS_ARTISTA_X_NOMBRE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@ART_COMPLETE", DbType.String, nombreCompleto);


            BEArtista item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEArtista();
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    item.COD_ARTIST_SQ = dr.GetDecimal(dr.GetOrdinal("COD_ARTIST_SQ"));

                    if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                        item.NAME = dr.GetString(dr.GetOrdinal("NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("IP_NAME")))
                        item.IP_NAME = dr.GetString(dr.GetOrdinal("IP_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FIRST_NAME")))
                        item.FIRST_NAME = dr.GetString(dr.GetOrdinal("FIRST_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ART_COMPLETE")))
                        item.ART_COMPLETE = dr.GetString(dr.GetOrdinal("ART_COMPLETE"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";
                }
            }
            return item;
        }


        public BEArtista ObtenerArtistaOracle(string owner, decimal codigoArt)
        {
            BEArtista entidad = null;
            OracleConnection oraCon = new OracleConnection();
            oraCon = setearConexion();


            OracleCommand oracmd = oraCon.CreateCommand();
            oracmd.CommandText = "SRGDA_ARTIST_OBTENER.ObtenerArtistas";
            oracmd.CommandType = CommandType.StoredProcedure;

            OracleParameter oracodigoArt = new OracleParameter("p_idArtista", OracleDbType.Decimal);
            oracodigoArt.Value = codigoArt;
            oracodigoArt.Direction = System.Data.ParameterDirection.Input;
            oracmd.Parameters.Add(oracodigoArt);

            OracleParameter oraparHC = new OracleParameter("p_tcArtista", OracleDbType.RefCursor);
            oraparHC.Direction = System.Data.ParameterDirection.Output;
            oracmd.Parameters.Add(oraparHC);

            try
            {
                oraCon.Open();
                OracleDataReader dr = oracmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        entidad = new BEArtista();

                        entidad.COD_ARTIST_SQ = dr.GetDecimal(dr.GetOrdinal("COD_ARTIST_SQ"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                            entidad.NAME = dr.GetString(dr.GetOrdinal("NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("ART_COMPLETE")))
                            entidad.ART_COMPLETE = dr.GetString(dr.GetOrdinal("ART_COMPLETE"));


                    }
                }
                return entidad;
            }
            catch (Exception ex)
            {
                return null;
            }
           

        }

        /// <summary>
        /// LISTA ARTISTAS POR CODIGO DE SHOW  DE LA MISMA LICENCIA
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="codshow"></param>
        /// <returns></returns>
        public List<BEArtista> Listar_Artista_x_Show(string owner,decimal codshow)
        {
            List<BEArtista> lista = null;
            BEArtista entidad = null;

            DbCommand cmd = db.GetStoredProcCommand("SGRDASS_LISTAR_ARTISTAS_X_SHOW");

            db.AddInParameter(cmd, "@OWNER", DbType.String, owner);
            db.AddInParameter(cmd, "@SHOW_ID", DbType.Decimal, codshow);

            try
            {
                using(IDataReader dr=db.ExecuteReader(cmd))
                {
                    lista = new List<BEArtista>();
                    while (dr.Read())
                    {

                        entidad = new BEArtista();

                        if (!dr.IsDBNull(dr.GetOrdinal("ARTIST_ID")))
                            entidad.COD_ARTIST_SQ = dr.GetDecimal(dr.GetOrdinal("ARTIST_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                            entidad.NAME = dr.GetString(dr.GetOrdinal("NAME"));

                        lista.Add(entidad);
                    }

                }
                
            }
            catch (Exception ex)
            {
                return null;
            }
            return lista;
        }

        public List<BEArtista> Listar_Artista_NO_CODSGS_PAGEJSON(int pagina, int cantRegxPag, string owner,string nombre,decimal LIC_ID,string SHOW_NAME)
        {
            List<BEArtista> lista = null;
            BEArtista entidad = null;
            DbCommand cmd = db.GetStoredProcCommand("SGRDASS_LISTAR_ARTISTAS_SIN_CODSGS_PAGEJSON");

            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddInParameter(cmd, "@OWNER", DbType.String,owner);
            db.AddInParameter(cmd, "@NOMBRE", DbType.String,nombre);
            db.AddInParameter(cmd, "@LIC_ID", DbType.Decimal, LIC_ID);
            db.AddInParameter(cmd, "@SHOW_NAME", DbType.String, SHOW_NAME);
            db.AddOutParameter(cmd, "@RecordCount", DbType.Int32, 50);
            
            //db.ExecuteNonQuery(cmd);

            //string results = Convert.ToString(db.GetParameterValue(cmd, "@RecordCount"));

            //DbCommand cmd1 = db.GetStoredProcCommand("SGRDASS_LISTAR_ARTISTAS_SIN_CODSGS_PAGEJSON", owner,nombre,pagina, cantRegxPag, ParameterDirection.Output);

            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                lista = new List<BEArtista>();

                while (dr.Read())
                {
                    entidad = new BEArtista();
                    if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                        entidad.NAME = dr.GetString(dr.GetOrdinal("NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        entidad.LOG_DATE_CREAT = dr.GetString(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        entidad.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SHOW_NAME")))
                        entidad.SHOW_NAME = dr.GetString(dr.GetOrdinal("SHOW_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ARTIST_ID")))
                        entidad.ARTIST_ID = dr.GetDecimal(dr.GetOrdinal("ARTIST_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CANTIDAD")))
                        entidad.TotalVirtual = dr.GetInt32(dr.GetOrdinal("CANTIDAD"));

                    lista.Add(entidad);
                }


            }

            return lista;
        }

        public int ActualizarArtistaSGS(string owner, decimal COD_SGS, decimal codArtist)
        {

            try
            {
                DbCommand cmd = db.GetStoredProcCommand("SGRDASU_REC_LIC_AUT_ARTIST");
                db.AddInParameter(cmd, "@OWNER", DbType.String, owner);
                db.AddInParameter(cmd, "@ARTIST_ID", DbType.Decimal, codArtist);
                db.AddInParameter(cmd, "@COD_SGS", DbType.Decimal, COD_SGS);


                int results = db.ExecuteNonQuery(cmd);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public BEArtista ObtenerArtista(decimal id, string owner)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_REC_LIC_AUT_ARTIST");
            db.AddInParameter(oDbCommand, "@COD_ARTIST_SQ", DbType.Decimal, id);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            BEArtista item = null;
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BEArtista();
                    item.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                    item.COD_ARTIST_SQ = dr.GetDecimal(dr.GetOrdinal("ARTIST_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                        item.NAME = dr.GetString(dr.GetOrdinal("NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("IP_NAME")))
                        item.IP_NAME = dr.GetString(dr.GetOrdinal("IP_NAME"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        item.ESTADO = "ACTIVO";
                    else
                        item.ESTADO = "INACTIVO";
                }
            }
            return item;
        }



        public int InsertaPlanillaAutomatica(decimal LIC_ID, decimal ARTIST_ID,string LOG_USER_CREAT,string owner)
        {
            int resp = 0;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_PLANILLA_X_ARTISTA");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            db.AddInParameter(oDbCommand, "@ARTIST_ID", DbType.Decimal, ARTIST_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, LOG_USER_CREAT);
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            resp = db.ExecuteNonQuery(oDbCommand);

            return resp;
        }

        public int ValidaPlanillaAutomatica(decimal LIC_ID, decimal ARTIST_ID)
        {
            int resp = 0;

            DbCommand oDbCommand = db.GetStoredProcCommand("VALIDA_TIENE_PLANILLA");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            db.AddInParameter(oDbCommand, "@ARTIST_ID", DbType.Decimal, ARTIST_ID);

            resp = db.ExecuteNonQuery(oDbCommand);

            return resp;
        }

        public int ValidamodEspectBaile(string owner, decimal LIC_ID)
        {
            int r = 0;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_MOD_ESPECT_BAILE");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);

            r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        #region VALIDAR MODALIDAD
        public int ValidaModalidadGrabadaenVIVO(decimal LIC_ID,decimal SHOW_ID)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_VALIDA_MOD_GRABADA");
            db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            db.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, SHOW_ID);
            int r = Convert.ToInt32(db.ExecuteScalar(oDbCommand));
            return r;
        }

        #endregion

        public List<BEArtista> Listar_Solicitud_Artista(decimal Lic_Id)
        {
            List<BEArtista> lista = null;
            BEArtista entidad = null;

            DbCommand cmd = db.GetStoredProcCommand("SGRDASS_LISTA_SOLICITUD_ARTISTA");

            db.AddInParameter(cmd, "@LIC_ID", DbType.Decimal, Lic_Id);

            try
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    lista = new List<BEArtista>();
                    while (dr.Read())
                    {

                        entidad = new BEArtista();

                        if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                            entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("SHOW_NAME")))
                            entidad.SHOW_NAME = dr.GetString(dr.GetOrdinal("SHOW_NAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("NAME")))
                            entidad.NAME = dr.GetString(dr.GetOrdinal("NAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SOLICITUD")))
                            entidad.ESTADO = dr.GetString(dr.GetOrdinal("ESTADO_SOLICITUD"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_ID")))
                            entidad.ESTADO_ID = dr.GetDecimal(dr.GetOrdinal("ESTADO_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("OBSERVACION")))
                            entidad.OBSERVACION = dr.GetString(dr.GetOrdinal("OBSERVACION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("ARTIST_ID")))
                            entidad.ARTIST_ID = dr.GetDecimal(dr.GetOrdinal("ARTIST_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("SHOW_ID")))
                            entidad.SHOW_ID = dr.GetDecimal(dr.GetOrdinal("SHOW_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("OFF_NAME")))
                            entidad.OFF_NAME = dr.GetString(dr.GetOrdinal("OFF_NAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("MODALIDAD")))
                            entidad.MODALIDAD = dr.GetString(dr.GetOrdinal("MODALIDAD"));

                        if (!dr.IsDBNull(dr.GetOrdinal("RUBRO")))
                            entidad.RUBRO = dr.GetString(dr.GetOrdinal("RUBRO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FEC_CANCELED")))
                            entidad.FEC_CANCELED = dr.GetString(dr.GetOrdinal("FEC_CANCELED"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FACTURA")))
                            entidad.FACTURA = dr.GetString(dr.GetOrdinal("FACTURA"));

                        lista.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lista;
        }

        public int Aprobar_Solicitud_Artista(decimal Show_Id, decimal Artist_Id)
        {
            int resp = 0;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDA_APROBAR_SOLICITUD_ARTISTA");
            db.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, Show_Id);
            db.AddInParameter(oDbCommand, "@ARTIST_ID", DbType.Decimal, Artist_Id);
            resp = db.ExecuteNonQuery(oDbCommand);

            return resp;
        }
        public int Rechazar_Solicitud_Artista(decimal Show_Id, decimal Artist_Id)
        {
            int resp = 0;
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDA_RECHAZAR_SOLICITUD_ARTISTA");
            db.AddInParameter(oDbCommand, "@SHOW_ID", DbType.Decimal, Show_Id);
            db.AddInParameter(oDbCommand, "@ARTIST_ID", DbType.Decimal, Artist_Id);
            resp = db.ExecuteNonQuery(oDbCommand);

            return resp;
        }


    }
}




