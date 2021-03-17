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
    public class DALicenciaDescuento
    {
        private Database oDatabase = DatabaseFactory.CreateDatabase("conexion");

        public List<BEDescuentos> ListaDescuentos(string Owner, decimal idLic     )
        {
            List<BEDescuentos> lista = new List<BEDescuentos>();
            BEDescuentos item = null;

            using (DbCommand cm = oDatabase.GetStoredProcCommand("SGRDASS_LISTAR_DESC"))
            {
                
                oDatabase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                oDatabase.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);


                using (IDataReader dr = oDatabase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BEDescuentos();
                        if (!dr.IsDBNull(dr.GetOrdinal("ORDEN")))
                        item.ORDEN =Convert.ToInt32( dr.GetValue(dr.GetOrdinal("ORDEN")));
                        item.ORIGEN = dr.GetString(dr.GetOrdinal("ORIGEN"));// este campo te indica el origen del descuento 1 es por usuario de derecho asociado a una asociacion, 2 descuento por usuario de derecho , 3 descuento por tarifa
                        item.LIC_DISC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_DISC_ID"));
                        item.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));
                        item.TIPO = dr.GetString(dr.GetOrdinal("DISC_TYPE_NAME"));
                        item.DISC_NAME = dr.GetString(dr.GetOrdinal("DISC_NAME"));
                        item.FORMATO = dr.GetString(dr.GetOrdinal("FORMATO"));//muestra los simbolos $,%
                        item.DISC_VALUE = dr.GetDecimal(dr.GetOrdinal("DISC_VALUE"));
                        item.BASE = dr.GetInt32(dr.GetOrdinal("BASE"));
                        item.DISC_SIGN = dr.GetString(dr.GetOrdinal("DISC_SIGN"));

                        if (!dr.IsDBNull(dr.GetOrdinal("DISC_ORG")))
                        item.DISC_ORG = dr.GetString(dr.GetOrdinal("DISC_ORG"));

                        //item.DES_ORIGEN = dr.GetString(dr.GetOrdinal("DesOrigen"));
                        item.LIC_DISC_ID =Convert.ToDecimal( dr.GetValue(dr.GetOrdinal("Id")));
                        item.IS_AUTO =  dr.GetBoolean(dr.GetOrdinal("IS_AUTO"));

                        if(!dr.IsDBNull(dr.GetOrdinal("DISC_ORG_DESC")))
                            item.DISC_ORG_DESC = dr.GetString(dr.GetOrdinal("DISC_ORG_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("OBSERVACION")))
                            item.OBSERVACION = dr.GetString(dr.GetOrdinal("OBSERVACION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("DISC_ESTADO")))
                            item.DISC_ESTADO = dr.GetInt32(dr.GetOrdinal("DISC_ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("DISC_RESP_OBSERVACION")))
                            item.DISC_RESP_OBSERVACION = dr.GetString(dr.GetOrdinal("DISC_RESP_OBSERVACION"));

                        lista.Add(item);
                    }
                }
                return lista;
            }
        }

        public int Insertar(BEDescuentos entidad)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_DESCUENTO_LIC");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, entidad.LIC_ID);
            oDatabase.AddInParameter(oDbCommand, "@DISC_ID", DbType.Decimal, entidad.DISC_ID);
           // oDatabase.AddInParameter(oDbCommand, "@DISC_VALUE", DbType.String, entidad.DISC_VALUE);
            oDatabase.AddInParameter(oDbCommand, "@DISC_ORDER", DbType.Int32, entidad.ORDEN);
            oDatabase.AddInParameter(oDbCommand, "@DISC_ORG", DbType.String, entidad.DISC_ORG);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@IS_AUTO", DbType.Boolean, entidad.IS_AUTO);
            oDatabase.AddInParameter(oDbCommand, "@OBSERVACION", DbType.String, entidad.OBSERVACION);
            return  oDatabase.ExecuteNonQuery(oDbCommand);

        }



        public int ActualizarAplicable(decimal idLicDes, string owner, string valor, string usuModi)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_DESCUENTO_LIC_APLI");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@LIC_DISC_ID", DbType.Decimal, idLicDes);
            oDatabase.AddInParameter(oDbCommand, "@DISC_ORG", DbType.String, valor);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuModi.ToUpper());
 
            return oDatabase.ExecuteNonQuery(oDbCommand);

        }

        public int Eliminar(decimal idLicDes, string owner, string usuModi)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASD_DESCUENTO_LIC");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@LIC_DISC_ID", DbType.Decimal, idLicDes);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, usuModi.ToUpper());

            return oDatabase.ExecuteNonQuery(oDbCommand);

        }
        #region Descuentos Socio

        public int InsertarDescAuto(BEDescuentos entidad) //--
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_DESCUENTO_LIC_AUTO");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, entidad.OWNER);
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, entidad.LIC_ID);
            oDatabase.AddInParameter(oDbCommand, "@DISC_ID", DbType.Decimal, entidad.DISC_ID);
            // oDatabase.AddInParameter(oDbCommand, "@DISC_VALUE", DbType.String, entidad.DISC_VALUE);
            oDatabase.AddInParameter(oDbCommand, "@DISC_ORDER", DbType.Int32, entidad.ORDEN);
            oDatabase.AddInParameter(oDbCommand, "@DISC_ORG", DbType.String, entidad.DISC_ORG);
            oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, entidad.LOG_USER_CREAT.ToUpper());
            oDatabase.AddInParameter(oDbCommand, "@IS_AUTO", DbType.Boolean, entidad.IS_AUTO);
            oDatabase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, entidad.BPS_ID);
            return oDatabase.ExecuteNonQuery(oDbCommand);

        }
        //Inserta Descuentos en las licencias Luego de Agregar o inactivar Desc De los Grpos empresariales
        public List<BEDescuentos> InsertaDescuentosGRUPOXML(string xml)
        {
            List<BEDescuentos> lista=null;
            BEDescuentos entidad=null;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRADSI_DESCUENTOS_GRUPO_XML");
            oDatabase.AddInParameter(oDbCommand, "@xmlList", DbType.String, xml);

            using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
            {
                lista=new List<BEDescuentos>();
                while (dr.Read())
                {
                    entidad=new BEDescuentos();
                    if (!dr.IsDBNull(dr.GetOrdinal("nLIC_DISC_ID")))
                        entidad.DISC_ID = dr.GetDecimal(dr.GetOrdinal("nLIC_DISC_ID"));
                    lista.Add(entidad);
                }
            }

            return lista;
        }
        //Inserta Descuentos Automaticamente XMl luego de Agregar a lICENCIA PADRE
        public int InsertaDescuentosLicenciaXML(string xml, string owner)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRADSI_DESCUENTOS_LICENCIA_XML");
            oDatabase.AddInParameter(oDbCommand, "@xml", DbType.Xml, xml);
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            int r = oDatabase.ExecuteNonQuery(oDbCommand);

            return r;
        }

        #endregion

        #region Descuentos_Validacion

        public BEDescuentos ObtieneDescuento_DiscLic(string owner, decimal LIC_DISC_ID)
        {
            BEDescuentos entidad = new BEDescuentos();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_DESCUENTO_LIC");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@LIC_DISC_ID", DbType.Decimal, LIC_DISC_ID);

            using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {

                    entidad.LIC_DISC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_DISC_ID"));
                    entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    entidad.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));
                    entidad.DISC_ORG = dr.GetString(dr.GetOrdinal("DISC_ORG"));
                    if(!dr.IsDBNull(dr.GetOrdinal("DISC_ESTADO")))
                    entidad.DISC_ESTADO = dr.GetInt32(dr.GetOrdinal("DISC_ESTADO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_RESP_OBSERVACION")))
                    entidad.DISC_RESP_OBSERVACION = dr.GetString(dr.GetOrdinal("DISC_RESP_OBSERVACION"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OBSERVACION")))
                        entidad.OBSERVACION = dr.GetString(dr.GetOrdinal("OBSERVACION"));

                    entidad.FORMATO = dr.GetString(dr.GetOrdinal("FORMATO"));

                    entidad.DISC_NAME = dr.GetString(dr.GetOrdinal("DISC_NAME"));
                    entidad.DISC_VALUE = dr.GetDecimal(dr.GetOrdinal("DISC_VALUE"));
                    entidad.DISC_TYPE = dr.GetDecimal(dr.GetOrdinal("DISC_TYPE"));
                }

            }
                return entidad;
        }


        

        public BEDescuentos ObtieneDescuento_Panel(string owner, decimal DISC_ID,decimal LIC_ID)
        {
            BEDescuentos entidad = new BEDescuentos();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_OBTENER_DESCUENTO_PANEL");
            oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@DISC_ID", DbType.Decimal, DISC_ID);
            oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
            using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {

                    entidad.LIC_DISC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_DISC_ID"));
                    entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    entidad.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));
                    entidad.DISC_ORG = dr.GetString(dr.GetOrdinal("DISC_ORG"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_ESTADO")))
                        entidad.DISC_ESTADO = dr.GetInt32(dr.GetOrdinal("DISC_ESTADO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_RESP_OBSERVACION")))
                        entidad.DISC_RESP_OBSERVACION = dr.GetString(dr.GetOrdinal("DISC_RESP_OBSERVACION"));
                    if (!dr.IsDBNull(dr.GetOrdinal("OBSERVACION")))
                        entidad.OBSERVACION = dr.GetString(dr.GetOrdinal("OBSERVACION"));

                    entidad.FORMATO = dr.GetString(dr.GetOrdinal("FORMATO"));
                    entidad.DISC_NAME = dr.GetString(dr.GetOrdinal("DISC_NAME"));
                    entidad.DISC_VALUE = dr.GetDecimal(dr.GetOrdinal("DISC_VALUE"));

                }

            }
            return entidad;
        }


        public void ActualizaDescuentoLicencia(string owner, decimal LIC_DISC_ID, string NombDescuento, decimal MontoDescuento,string usuarioActual)
        {

            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_DESCUENTO_LIC");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDatabase.AddInParameter(oDbCommand, "@DISC_LIC_ID", DbType.Decimal, LIC_DISC_ID);
                oDatabase.AddInParameter(oDbCommand, "@DISC_MONTO", DbType.Decimal, MontoDescuento);
                oDatabase.AddInParameter(oDbCommand, "@DISC_NAME", DbType.String, NombDescuento);
                oDatabase.AddInParameter(oDbCommand, "@Usuario", DbType.String, usuarioActual);
                


                oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
            }


        }

        public void ActualizaDescuentoLicencia_Panel(string owner,decimal DISC_ID, int estado, string observ_respuesta,decimal LIC_ID)
        {

            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_DESCUENTO_LIC_PANEL");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDatabase.AddInParameter(oDbCommand, "@DISC_ID", DbType.Decimal, DISC_ID);
                oDatabase.AddInParameter(oDbCommand, "@ESTADO_DISC", DbType.Decimal, estado);
                oDatabase.AddInParameter(oDbCommand, "@RES_OBSER", DbType.String, observ_respuesta);
                oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, LIC_ID);
                oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
            }


        }

        public List<BEDescuentos> ListaDescuentosxAprobar(int pagina, int cantRegxPag ,string owner, decimal LIC_ID, string NOM_LIC, int OFI_ID, int EST_DESC, string NOM_DESC, int CON_FECHA, DateTime FECHA_INICIO, DateTime FECHA_FIN)
        {
            List<BEDescuentos> lista = new List<BEDescuentos>();
            BEDescuentos entidad = null;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASS_LICENCIAS_DESCUENTOS_APROB");
            oDatabase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDatabase.AddInParameter(oDbCommand, "@lic_id", DbType.Decimal, LIC_ID);
            oDatabase.AddInParameter(oDbCommand, "@lic_name", DbType.String, NOM_LIC);
            oDatabase.AddInParameter(oDbCommand, "@oficina", DbType.Int32, OFI_ID);
            oDatabase.AddInParameter(oDbCommand, "@estado_desc", DbType.Int32, EST_DESC);
            oDatabase.AddInParameter(oDbCommand, "@nombre_descuento", DbType.String, NOM_DESC);
            oDatabase.AddInParameter(oDbCommand, "@confecha", DbType.Int32, CON_FECHA);
            oDatabase.AddInParameter(oDbCommand, "@fechainicial", DbType.DateTime, FECHA_INICIO);
            oDatabase.AddInParameter(oDbCommand, "@fechafinal", DbType.DateTime, FECHA_FIN);
            oDatabase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDatabase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDatabase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);

            using (IDataReader dr = oDatabase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    entidad = new BEDescuentos();

                    //if (!dr.IsDBNull(dr.GetOrdinal("LIC_DISC_ID")))
                    //    entidad.LIC_DISC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_DISC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        entidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_ID")))
                        entidad.DISC_ID = dr.GetDecimal(dr.GetOrdinal("DISC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("DISC_NAME")))
                        entidad.DISC_NAME = dr.GetString(dr.GetOrdinal("DISC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("VALOR")))
                        entidad.VALOR = dr.GetDecimal(dr.GetOrdinal("VALOR"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                        entidad.LOG_DATE_CREACION = dr.GetString(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                        entidad.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("FORMATO")))
                        entidad.FORMATO = dr.GetString(dr.GetOrdinal("FORMATO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CANTIDAD")))
                        entidad.CANTIDAD = dr.GetInt32(dr.GetOrdinal("CANTIDAD"));
                    if(!dr.IsDBNull(dr.GetOrdinal("DISC_ORG")))
                        entidad.DISC_ORG = dr.GetString(dr.GetOrdinal("DISC_ORG"));
                    lista.Add(entidad);

                }


            }

                return lista;
        }


        public void ActualizaDescuentoLicencia_Panel(string owner, decimal LIC_DISC_ID, string NombDescuento, decimal MontoDescuento, string usuarioActual)
        {

            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_DESCUENTO_LIC");
                oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDatabase.AddInParameter(oDbCommand, "@DISC_LIC_ID", DbType.Decimal, LIC_DISC_ID);
                oDatabase.AddInParameter(oDbCommand, "@DISC_MONTO", DbType.Decimal, MontoDescuento);
                oDatabase.AddInParameter(oDbCommand, "@DISC_NAME", DbType.String, NombDescuento);
                oDatabase.AddInParameter(oDbCommand, "@Usuario", DbType.String, usuarioActual);



                oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
            }


        }

        public int ActualizaDescuentoLicenciaCalc(decimal CodigoLicencia)
        {
            int r = 0;
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_LICENCIA_DESCUENTOS");
                oDatabase.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, CodigoLicencia);

                r =oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                return 0;
            }

            return r;
        }
        #endregion
    }
}
