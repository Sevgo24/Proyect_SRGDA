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
    public class DADescuentos
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BEDescuentos> Listar_Page_Descuentos(decimal tipo, string parametro, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PAGE_DESCUENTOS");
            oDataBase.AddInParameter(oDbCommand, "@DISC_TYPE", DbType.Decimal, tipo);
            oDataBase.AddInParameter(oDbCommand, "@DISC_NAME", DbType.String, parametro);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            var lista = new List<BEDescuentos>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                    lista.Add(new BEDescuentos(dr, Convert.ToInt32(results)));
            }
            return lista;
        }

        public List<BEDescuentos> Obtener(string owner, decimal id)
        {
            List<BEDescuentos> lst = new List<BEDescuentos>();
            BEDescuentos Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DESCUENTOS"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DISC_ID", DbType.Decimal, id);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDescuentos();
                        Obj.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
                        Obj.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));
                        Obj.DISC_TYPE = dr.GetDecimal(dr.GetOrdinal("DISC_TYPE"));
                        Obj.DISC_NAME = dr.GetString(dr.GetOrdinal("DISC_NAME"));
                        Obj.DISC_SIGN = dr.GetString(dr.GetOrdinal("DISC_SIGN"));
                        Obj.DISC_PERC = dr.GetDecimal(dr.GetOrdinal("DISC_PERC"));
                        Obj.DISC_VALUE = dr.GetDecimal(dr.GetOrdinal("DISC_VALUE"));
                        Obj.DISC_ACC = dr.GetDecimal(dr.GetOrdinal("DISC_ACC"));
                        Obj.DISC_AUT = Convert.ToChar(dr.GetValue(dr.GetOrdinal("DISC_AUT")));
                        if (!dr.IsDBNull(dr.GetOrdinal("TEMP_ID_DSC")))
                        Obj.TEMP_ID_DSC = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC"));
                        lst.Add(Obj);
                    }
                }
            }

            return lst;
        }

        public int Insertar(BEDescuentos en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_DESCUENTOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DISC_TYPE", DbType.Decimal, en.DISC_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@DISC_NAME", DbType.String, en.DISC_NAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@DISC_SIGN", DbType.String, en.DISC_SIGN.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@DISC_PERC", DbType.Decimal, en.DISC_PERC);
            oDataBase.AddInParameter(oDbCommand, "@DISC_VALUE", DbType.Decimal, en.DISC_VALUE);
            oDataBase.AddInParameter(oDbCommand, "@DISC_ACC", DbType.Decimal, en.DISC_ACC);
            oDataBase.AddInParameter(oDbCommand, "@DISC_AUT", DbType.String, en.DISC_AUT);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, en.TEMP_ID_DSC);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@OBSERVACION", DbType.String, en.OBSERVACION);
            oDataBase.AddOutParameter(oDbCommand, "@DISC_ID", DbType.Int32, 0);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@DISC_ID"));
            return id;
        }

        public int Actualizar(BEDescuentos en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_DESCUENTOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DISC_ID", DbType.Decimal, en.DISC_ID);
            oDataBase.AddInParameter(oDbCommand, "@DISC_TYPE", DbType.Decimal, en.DISC_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@DISC_NAME", DbType.String, en.DISC_NAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@DISC_SIGN", DbType.String, en.DISC_SIGN.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@DISC_PERC", DbType.Decimal, en.DISC_PERC);
            oDataBase.AddInParameter(oDbCommand, "@DISC_VALUE", DbType.Decimal, en.DISC_VALUE);
            oDataBase.AddInParameter(oDbCommand, "@DISC_ACC", DbType.Decimal, en.DISC_ACC);
            oDataBase.AddInParameter(oDbCommand, "@DISC_AUT", DbType.String, en.DISC_AUT);
            oDataBase.AddInParameter(oDbCommand, "@TEMP_ID_DSC", DbType.Decimal, en.TEMP_ID_DSC);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());

            int n = oDataBase.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int Eliminar(BEDescuentos del)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASD_DESCUENTOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, del.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DISC_ID", DbType.Decimal, del.DISC_ID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, del.LOG_USER_UPDATE);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public List<BEDescuentos> ListarCombo(string owner, decimal idTipo)
        {
            List<BEDescuentos> lst = new List<BEDescuentos>();
            BEDescuentos Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_LISTA_DESCUENTO"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DISC_TYPE", DbType.Decimal, idTipo);

                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDescuentos();
                        Obj.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));
                        Obj.DISC_NAME = dr.GetString(dr.GetOrdinal("DISC_NAME"));
                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        public List<BEDescuentos> ObtenerDescuentoLicXML(string owner, string xml)
        {
            List<BEDescuentos> Lista = new List<BEDescuentos>();
            try
            {
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_DESCUENTOS_LICENCIA_MASIVA");
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "xmlLst", DbType.Xml, xml);

                oDbCommand.CommandTimeout = 1800;
                BEDescuentos descuento = null;

                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        descuento = new BEDescuentos();
                        descuento.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));
                        descuento.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        descuento.ORDEN = Convert.ToInt32( dr.GetDecimal(dr.GetOrdinal("DISC_ORDER")));
                        descuento.DISC_ORG = dr.GetString(dr.GetOrdinal("DISC_ORG"));
                        descuento.DISC_PERC = dr.GetDecimal(dr.GetOrdinal("DISC_PERC"));
                        descuento.DISC_VALUE = dr.GetDecimal(dr.GetOrdinal("DISC_VALUE"));
                        descuento.DISC_SIGN = dr.GetString(dr.GetOrdinal("DISC_SIGN"));
                        descuento.DISC_ACC = dr.GetDecimal(dr.GetOrdinal("DISC_ACC"));
                        Lista.Add(descuento);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Lista;
        }

        public List<BEDescuentos> DescuentoPorTarifa(string owner, decimal idTipo,decimal idTarifa)
        {
            List<BEDescuentos> lst = new List<BEDescuentos>();
            BEDescuentos Obj = null;

            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_DESCUENTOS_X_TARIFA"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@DISC_TYPE", DbType.Decimal, idTipo);
                oDataBase.AddInParameter(cm, "@RATE_ID", DbType.Decimal, idTarifa);


                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        Obj = new BEDescuentos();
                        Obj.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));
                        Obj.DISC_NAME = dr.GetString(dr.GetOrdinal("DISC_NAME"));
                        lst.Add(Obj);
                    }
                }
            }
            return lst;
        }

        #region Descuentos
        public int InsertarDescuentoSocioBPS(decimal BPSID, decimal DISCID, string UsuarioActual, string observacion)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_DESCUENTOS_BPS");
            oDataBase.AddInParameter(oDbCommand, "OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "BPS_ID", DbType.Decimal, BPSID);
            oDataBase.AddInParameter(oDbCommand, "DISC_ID", DbType.Decimal, DISCID);
            oDataBase.AddInParameter(oDbCommand, "LOG_USER_CREATE", DbType.String, UsuarioActual);
            oDataBase.AddInParameter(oDbCommand, "OBSERVACION", DbType.String, observacion);
           // Convert.ToString(Session[Constantes.Sesiones.Usuario]).ToUpper();
            
            
            int r= oDataBase.ExecuteNonQuery(oDbCommand);


            return r;
        }
        public int InsertaDescuentoSoc(BEDescuentos Descuento)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASI_REC_DISCOUNTS");

            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, Descuento.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@DISC_TYPE", DbType.Int32,Descuento.DISC_TYPE);
            oDataBase.AddInParameter(oDbCommand, "@DISC_NAME", DbType.String,Descuento.DISC_NAME);
            oDataBase.AddInParameter(oDbCommand, "@DISC_SIGN", DbType.String, Descuento.DISC_SIGN);
            oDataBase.AddInParameter(oDbCommand, "@DISC_PERC", DbType.Decimal, Descuento.DISC_PERC);
            oDataBase.AddInParameter(oDbCommand, "@DISC_VALUE", DbType.Decimal, Descuento.DISC_VALUE);
            oDataBase.AddInParameter(oDbCommand, "@DISC_ACC", DbType.Int32, Descuento.DISC_ACC);
            oDataBase.AddInParameter(oDbCommand, "@DISC_AUT", DbType.String, Descuento.DISC_AUT);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String,Descuento.LOG_USER_CREAT);
            oDataBase.AddInParameter(oDbCommand, "@OBSERVACION", DbType.String, Descuento.OBSERVACION);
            oDataBase.AddOutParameter(oDbCommand, "@DISC_ID", DbType.Int32, 0);

            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@DISC_ID"));
            return id;
        }
        
        //Lista Descuentos Por Socio

        public List<BEDescuentos> Listar_Descuentos_Socio(decimal bpsi)
        {

            List<BEDescuentos> lst = new List<BEDescuentos>();
            BEDescuentos Obj = null;

            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_DESCUENTOS_BPS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@BPSID", DbType.Decimal, bpsi);

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    Obj = new BEDescuentos();

                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_ID")))
                        Obj.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_NAME")))
                        Obj.DISC_NAME = dr.GetString(dr.GetOrdinal("DISC_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_VALUE")))
                        Obj.DISC_VALUE = dr.GetDecimal(dr.GetOrdinal("DISC_VALUE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_SIGN")))
                        Obj.DISC_SIGN = dr.GetString(dr.GetOrdinal("DISC_SIGN"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_TYPE")))
                        Obj.DISC_TYPE = dr.GetDecimal(dr.GetOrdinal("DISC_TYPE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_PERC")))
                        Obj.DISC_PERC = dr.GetDecimal(dr.GetOrdinal("DISC_PERC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_ACC")))
                        Obj.DISC_ACC = dr.GetDecimal(dr.GetOrdinal("DISC_ACC"));
                   // if (!dr.IsDBNull(dr.GetOrdinal("DISC_AUT")))
                     //   Obj.DISC_AUT = dr.GetChar(dr.GetOrdinal("DISC_AUT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OBSERVACION")))
                        Obj.OBSERVACION = dr.GetString(dr.GetOrdinal("OBSERVACION"));
                            
                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    lst.Add(Obj);
                }
            }


            return lst;
        }

        //Inactiva Descuentos Del Socio
        public int InactivaDescuentosSocio(decimal discid, decimal bpsid, decimal bpsgroup, string usuario)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_INACTIVA_DESCUENTOS_SOCIO");
            oDataBase.AddInParameter(oDbCommand, "@DISCID", DbType.Decimal, discid);
            oDataBase.AddInParameter(oDbCommand, "@BPSID", DbType.Decimal, bpsid);
            oDataBase.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.Decimal, bpsgroup);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuario);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);

            return r;
        
        }
        //Activa Descuentos de Socios

        public int ActivaDescuentosSocio(decimal discid,decimal bpsid ,decimal bpsgroup,string usuario)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASU_ACTIVA_DESCUENTOS_SOCIO");
            oDataBase.AddInParameter(oDbCommand, "@DISCID", DbType.Decimal, discid);
            oDataBase.AddInParameter(oDbCommand, "@BPSID", DbType.Decimal, bpsid);
            oDataBase.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.Decimal, bpsgroup);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuario);
            int r = oDataBase.ExecuteNonQuery(oDbCommand);

            return r;

        }

        public List<BEDescuentos> ObtieneTotalDescuentoSoc(decimal licid)
        {
            List<BEDescuentos> lista = new List<BEDescuentos>();
            BEDescuentos descuento = null;

            DbCommand oDbcommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_TOTAL_DESCUENTO_SOCIO");
            oDataBase.AddInParameter(oDbcommand, "@LICID", DbType.Decimal, licid);
            using (IDataReader dr = oDataBase.ExecuteReader(oDbcommand))
            {
                while (dr.Read())
                {
                    descuento = new BEDescuentos();
                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_PERC")))
                        descuento.DISC_PERC = dr.GetDecimal(dr.GetOrdinal("DISC_PERC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_VALUE")))
                        descuento.DISC_VALUE = dr.GetDecimal(dr.GetOrdinal("DISC_VALUE"));

                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_SIGN")))
                        descuento.DISC_SIGN = dr.GetString(dr.GetOrdinal("DISC_SIGN"));

                    lista.Add(descuento);
                }


                return lista;
            }



        }

        public List<BEDescuentos> ListarDescuentosxTipoDesc(decimal disctype)
        {
            List<BEDescuentos> lista = new List<BEDescuentos>();
            BEDescuentos descuento=null;

            DbCommand oDbcommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTA_DESCUENTOSX_TIPODESC");
            oDataBase.AddInParameter(oDbcommand, "@DISCTYPE", DbType.Decimal, disctype);

            using (IDataReader dr = oDataBase.ExecuteReader(oDbcommand))
            {
                while (dr.Read())
                {
                    descuento = new BEDescuentos();
                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_ID")))
                        descuento.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_NAME")))
                        descuento.DISC_NAME = dr.GetString(dr.GetOrdinal("DISC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_VALUE")))
                        descuento.DISC_VALUE = dr.GetDecimal(dr.GetOrdinal("DISC_VALUE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_SIGN")))
                        descuento.DISC_SIGN = dr.GetString(dr.GetOrdinal("DISC_SIGN"));

                    lista.Add(descuento);
                }
                return lista;
            }
            
        }

        //Recupera COdigo de SOcio mediante su codigo de licencia

        public decimal RecuperaCodigodeSOcio(decimal licid)
        {
            DbCommand oDbcommand = oDataBase.GetStoredProcCommand("SGRDASS_RECUPERA_CODCLIENTE");
            oDataBase.AddInParameter(oDbcommand, "@LICID", DbType.Decimal, licid);
            decimal r = 0;
            using (IDataReader dr = oDataBase.ExecuteReader(oDbcommand))
            {
                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                      r = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));

                }
            }
            return r;

        }

        //Recuperar LICID por socio de Negocio
        public List<BEDescuentos> ObtenerLicIdxSocio(string owner,decimal BPSID)
        {
            List<BEDescuentos> lista = new List<BEDescuentos>();
            BEDescuentos descuento = null;

            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_LICIDXBPSID");
            oDataBase.AddInParameter(oDbCommand,"@OWNER",DbType.String,owner);
            oDataBase.AddInParameter(oDbCommand, "@BPSID", DbType.Decimal, BPSID);

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    descuento = new BEDescuentos();

                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        descuento.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                    lista.Add(descuento);
                }
            }

            return lista;

        }

        //Valida Periodo Por descuentos

        public int ValidaPeridoDescuento(string owner, decimal licplid)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDARPERIODODESCUENTO");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@licplid", DbType.Decimal, licplid);

            int r =Convert.ToInt32(  oDataBase.ExecuteScalar(oDbCommand));

            return r;
        }
        //Inactivar Descuentos del Grupo Emp cuando se remueve una entidad(socio) del mismo
        public int InactivaDesSocioxGrupoEmpresarial(string owner,decimal bpsid, decimal bpsidgroup)
        {
            DbCommand oDbcommand = oDataBase.GetStoredProcCommand("SGRDASU_INACTIVA_DESC_GRUPO_EMP");
            oDataBase.AddInParameter(oDbcommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbcommand, "@BPSID", DbType.Decimal, bpsid);
            oDataBase.AddInParameter(oDbcommand, "@BPSIDGRUP", DbType.Decimal, bpsidgroup);

            int r = Convert.ToInt32( oDataBase.ExecuteScalar(oDbcommand));

            return r;
        }

        //lista plantilla descuentos
        public List<BEDescuentosPlantilla> listaDescuentoPlantilla(int page,int pagesize,string owner,string descripcion,DateTime? fecha)
        {
            List<BEDescuentosPlantilla> lista = null;
            BEDescuentosPlantilla entidad = null;
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_LISTAR_PLANTILLA_DESC");
            oDataBase.AddInParameter(oDbCommand,"@owner",DbType.String,owner);
            oDataBase.AddInParameter(oDbCommand, "@descripcion", DbType.String, descripcion);
            oDataBase.AddInParameter(oDbCommand, "@fecha", DbType.DateTime, fecha);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex",DbType.Int32,50);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, 50);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);

            oDataBase.ExecuteNonQuery(oDbCommand);

            String Results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "RecordCount"));


            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PLANTILLA_DESC");
            oDataBase1.AddInParameter(oDbCommand1, "@owner", DbType.String, owner);
            oDataBase1.AddInParameter(oDbCommand1, "@descripcion", DbType.String, descripcion);
            oDataBase1.AddInParameter(oDbCommand1, "@fecha", DbType.DateTime, fecha);
            oDataBase1.AddInParameter(oDbCommand1, "@PageIndex", DbType.Int32, page);
            oDataBase1.AddInParameter(oDbCommand1, "@PageSize", DbType.Int32, pagesize);
            oDataBase1.AddOutParameter(oDbCommand1, "@RecordCount", DbType.Int32, 50);

            using (IDataReader dr = (oDataBase1.ExecuteReader(oDbCommand1)))
            {
                lista = new List<BEDescuentosPlantilla>();
                while (dr.Read())
                {
                    entidad = new BEDescuentosPlantilla();

                    if (!dr.IsDBNull(dr.GetOrdinal("TEMP_ID_DSC")))
                        entidad.TEMP_ID_DSC = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TEMP_DESC")))
                        entidad.TEMP_DESC = dr.GetString(dr.GetOrdinal("TEMP_DESC"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("STARTS")))
                      //  entidad.STARTS = dr.GetDateTime(dr.GetOrdinal("STARTS"));
                    entidad.TotalVirtual = Convert.ToInt32(Results);
                    lista.Add(entidad);
                }
            }
            return lista;
        }

        //lista plantilla descuentos SIN LISTADO DE PAGINA
        public List<BEDescuentosPlantilla> listaDescuentoPlantillasinPaginado(string owner, decimal DescID)
        {
            List<BEDescuentosPlantilla> lista = null;
            BEDescuentosPlantilla entidad = null;

            Database oDataBase1 = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand1 = oDataBase1.GetStoredProcCommand("SGRDASS_LISTAR_PLANTILLA_DESCUENTO");
            oDataBase1.AddInParameter(oDbCommand1, "@owner", DbType.String, owner);
            oDataBase1.AddInParameter(oDbCommand1, "@DISC_TYPE_ID", DbType.Int32, DescID);

            using (IDataReader dr = (oDataBase1.ExecuteReader(oDbCommand1)))
            {
                lista = new List<BEDescuentosPlantilla>();
                while (dr.Read())
                {
                    entidad = new BEDescuentosPlantilla();

                    if (!dr.IsDBNull(dr.GetOrdinal("TEMP_ID_DSC")))
                        entidad.TEMP_ID_DSC = dr.GetDecimal(dr.GetOrdinal("TEMP_ID_DSC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TEMP_DESC")))
                        entidad.TEMP_DESC = dr.GetString(dr.GetOrdinal("TEMP_DESC"));
                    //if (!dr.IsDBNull(dr.GetOrdinal("STARTS")))
                    //  entidad.STARTS = dr.GetDateTime(dr.GetOrdinal("STARTS"));
                    lista.Add(entidad);
                }
            }
            return lista;
        }

        public int ActivaDescuentosGrupoEmpresarialXML(decimal BPS_ID_GROUP, string xml)
        {

            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRADSU_ACTIVA_DESCUENTOS_GRUPO_XML");
            oDataBase.AddInParameter(oDbCommand, "@xmlList", DbType.String, xml);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID_GROUP);


            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;

        }
        public int InactivaDescuentosGrupoEmpresarialXML(decimal BPS_ID_GROUP, string xml)
        {

            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRADSU_INACTIVA_DESCUENTOS_GRUPO_XML");
            oDataBase.AddInParameter(oDbCommand, "@xmlList", DbType.String, xml);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, BPS_ID_GROUP);


            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;

        }

        public List<BEDescuentos> ListaLicenciasDescuentos(decimal DISC_ID, decimal BPS_ID)
        {
            List<BEDescuentos> lista = null;
            BEDescuentos entidad = null;
            DbCommand oDbcommand = oDataBase.GetStoredProcCommand("SGRDASS_LIC_X_INSERTAR_AUTO");
            oDataBase.AddInParameter(oDbcommand, "@DISC_ID", DbType.Decimal, DISC_ID);
            oDataBase.AddInParameter(oDbcommand, "@BPS_ID", DbType.Decimal, BPS_ID);

            using (IDataReader dr = oDataBase.ExecuteReader(oDbcommand))
            {
                lista = new List<BEDescuentos>();
                while (dr.Read())
                {
                    entidad = new BEDescuentos();
                    if(!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        entidad.LIC_ID=dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                    if(!dr.IsDBNull(dr.GetOrdinal("DISC_ID")))
                        entidad.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));

                    if(!dr.IsDBNull(dr.GetOrdinal("ORDEN")))
                        entidad.ORDEN = dr.GetInt32(dr.GetOrdinal("ORDEN"));

                    //if(!dr.IsDBNull(dr.GetOrdinal("IS_AUTO")))
                    //    entidad.IS_AUTO = dr.GetBoolean(dr.GetOrdinal("IS_AUTO"));

                    if(!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        entidad.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    lista.Add(entidad);

                }
            }

            return lista;
        }
        #endregion
        #region  Descuentos Plantilla
        /// <summary>
        /// DEVUELE EL VALOR DE LA FORMULA DE DESCUENTO PLANTILLA SEGUN LOS PARAMETROS INGRESADOS
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="DISC_ID"></param>
        /// <param name="valor1"></param>
        /// <param name="CHAR_ID1"></param>
        /// <param name="valor2"></param>
        /// <param name="CHAR_ID2"></param>
        /// <param name="valor3"></param>
        /// <param name="CHAR_ID3"></param>
        /// <returns></returns>
        public decimal ObtieneDescuentoPlantillaCadena(string owner, decimal DISC_ID, decimal? valor1, decimal? CHAR_ID1, decimal? valor2, decimal? CHAR_ID2, decimal? valor3, decimal? CHAR_ID3)
        {
            decimal Total = 0;
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_PLANTILLA_DESC_MONTO_TOTAL");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@DISC_ID", DbType.Decimal, DISC_ID);
            oDataBase.AddInParameter(oDbCommand, "@param1", DbType.Decimal, valor1);
            oDataBase.AddInParameter(oDbCommand, "@caract1", DbType.Decimal, CHAR_ID1);
            oDataBase.AddInParameter(oDbCommand, "@param2", DbType.Decimal, valor2);
            oDataBase.AddInParameter(oDbCommand, "@caract2", DbType.Decimal, CHAR_ID2);
            oDataBase.AddInParameter(oDbCommand, "@param3", DbType.Decimal, valor3);
            oDataBase.AddInParameter(oDbCommand, "@caract3", DbType.Decimal, CHAR_ID3);

            Total = Convert.ToDecimal(oDataBase.ExecuteScalar(oDbCommand));

            return Total;
        }
        /// <summary>
        /// DEVUELVE 1 SI ES UN DESCUENTO QUE TIENE PLANTILLA O 0 CASO CONTRARIO.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="DISC_ID"></param>
        /// <returns></returns>
        public int ValidaDescuentoPlantilla(string owner, decimal DISC_ID)
        {
            int r = 0;
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDA_DESC_PLANTILLA");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@DISC_ID", DbType.Decimal, DISC_ID);

            r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }
        /// <summary>
        /// LISTA VALORES DE LA PLANTILLA SEGUN EL ID DESCUENTO
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="DISC_ID"></param>
        /// <returns></returns>
        public List<BEDescuentosPlantilla> listaPlantillaxDISCID(string owner, decimal DISC_ID)
        {
            List<BEDescuentosPlantilla> lista = null;
            BEDescuentosPlantilla entidad = null;

            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_CARACTERISTICA_PLANTILLA_DESC");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@DISC_ID", DbType.Decimal, DISC_ID);

            using (IDataReader dr = (oDataBase.ExecuteReader(oDbCommand)))
            {
                lista = new List<BEDescuentosPlantilla>();
                while (dr.Read())
                {
                    entidad = new BEDescuentosPlantilla();
                    if (!dr.IsDBNull(dr.GetOrdinal("CHAR_ID")))
                        entidad.CHAR_ID = dr.GetDecimal(dr.GetOrdinal("CHAR_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SECC_CHARSEQ")))
                        entidad.SECC_CHARSEQ = dr.GetDecimal(dr.GetOrdinal("SECC_CHARSEQ"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IND_TR")))
                        entidad.IND_TR = dr.GetDecimal(dr.GetOrdinal("IND_TR"));

                    lista.Add(entidad);
                }

            }

            return lista;
        }

        #endregion

        #region Descuentos Megaconcierto
        public int Obtiene_PlanificacionUnica_Megaconcierto(string owner, decimal LIC_ID)
        {
            int r = 0;
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PLANIFICACION_UNICA_MEGACONCIERTO");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);

            r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }
        public int ValidaLicenciaMegaconcierto(string owner, decimal LIC_ID)
        {
            int r = 0;
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_VALIDA_MOD_MEGAOCNCIERTO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);

            r = Convert.ToInt32(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }
        #endregion
    }
}
