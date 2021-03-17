using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DACampaniaCallCenter
    {
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BECampaniaCallCenter> ListaDropCampaniaContacto(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_CAMPANIA_ESTADO");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            BECampaniaCallCenter item = null;
            List<BECampaniaCallCenter> lista = new List<BECampaniaCallCenter>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BECampaniaCallCenter();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CSTATUS")))
                        item.CONC_CSTATUS = dr.GetString(dr.GetOrdinal("CONC_CSTATUS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CSTATUS_DESC")))
                        item.CONC_CSTATUS_DESC = dr.GetString(dr.GetOrdinal("CONC_CSTATUS_DESC"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public BECampaniaCallCenter ObtenerDatos(string owner, decimal Id)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_CONTAC_CAMPAINGS_GET");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, Id);
            BECampaniaCallCenter item = new BECampaniaCallCenter();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CID")))
                        item.CONC_CID = dr.GetDecimal(dr.GetOrdinal("CONC_CID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CNAME")))
                        item.CONC_CNAME = dr.GetString(dr.GetOrdinal("CONC_CNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CTNAME")))
                        item.CONC_CTNAME = dr.GetString(dr.GetOrdinal("CONC_CTNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENT_DESC")))
                        item.ENT_DESC = dr.GetString(dr.GetOrdinal("ENT_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CDESC")))
                        item.CONC_CDESC = dr.GetString(dr.GetOrdinal("CONC_CDESC"));

                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CDINI")))
                    {
                        item.CONC_CDINI = dr.GetDateTime(dr.GetOrdinal("CONC_CDINI"));
                        item.FechaIni = (dr.GetDateTime(dr.GetOrdinal("CONC_CDINI"))).ToShortDateString();
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CDEND")))
                    {
                        item.CONC_CDEND = dr.GetDateTime(dr.GetOrdinal("CONC_CDEND"));
                        item.FechaFin = (dr.GetDateTime(dr.GetOrdinal("CONC_CDEND"))).ToShortDateString();
                    }

                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_ID")))
                        item.CONC_ID = dr.GetDecimal(dr.GetOrdinal("CONC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CTID")))
                        item.CONC_CTID = dr.GetDecimal(dr.GetOrdinal("CONC_CTID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CSTATUS")))
                        item.CONC_CSTATUS = dr.GetString(dr.GetOrdinal("CONC_CSTATUS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENT_ID")))
                        item.ENT_ID = dr.GetDecimal(dr.GetOrdinal("ENT_ID"));
                }
            }
            return item;
        }

        public int Insertar(BECampaniaCallCenter en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAI_CONTAC_CAMPAINGS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddOutParameter(oDbCommand, "@CONC_CID", DbType.Decimal, Convert.ToInt32(en.CONC_CID));
            oDataBase.AddInParameter(oDbCommand, "@CONC_CNAME", DbType.String, en.CONC_CNAME == null ? string.Empty : en.CONC_CNAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CONC_ID", DbType.Decimal, en.CONC_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CDESC", DbType.String, en.CONC_CDESC == null ? string.Empty : en.CONC_CDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CONC_CTID", DbType.Decimal, en.CONC_CTID);
            oDataBase.AddInParameter(oDbCommand, "@ENT_ID", DbType.Decimal, en.ENT_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CSTATUS", DbType.String, en.CONC_CSTATUS == null ? string.Empty : en.CONC_CSTATUS.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CONC_CDINI", DbType.DateTime, en.CONC_CDINI);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CDEND", DbType.DateTime, en.CONC_CDEND);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(oDataBase.GetParameterValue(oDbCommand, "@CONC_CID"));
            return id;
        }

        public int Actualizar(BECampaniaCallCenter en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAU_CONTAC_CAMPAINGS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, en.CONC_CID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CNAME", DbType.String, en.CONC_CNAME == null ? string.Empty : en.CONC_CNAME.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CONC_ID", DbType.Decimal, en.CONC_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CDESC", DbType.String, en.CONC_CDESC == null ? string.Empty : en.CONC_CDESC.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CONC_CTID", DbType.Decimal, en.CONC_CTID);
            oDataBase.AddInParameter(oDbCommand, "@ENT_ID", DbType.Decimal, en.ENT_ID);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CSTATUS", DbType.String, en.CONC_CSTATUS == null ? string.Empty : en.CONC_CSTATUS.ToUpper());
            oDataBase.AddInParameter(oDbCommand, "@CONC_CDINI", DbType.DateTime, en.CONC_CDINI);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CDEND", DbType.DateTime, en.CONC_CDEND);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            int r = oDataBase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int Eliminar(BECampaniaCallCenter en)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAD_CONTAC_CAMPAINGS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, en.OWNER);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, en.CONC_CID);
            oDataBase.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE);
            return Convert.ToInt32(oDataBase.ExecuteNonQuery(oDbCommand));
        }

        public List<BECampaniaCallCenter> ListarCampaniaCallCenter(string owner, decimal contacto, decimal tipoCamp, string estadoCamp, string nombre, decimal perfilCliente, DateTime fechaIni, DateTime fechaFin, int st, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_CONTAC_CAMPAINGS");
            oDataBase.AddInParameter(oDbCommand, "@owner", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@cconctacto", DbType.Decimal, contacto);
            oDataBase.AddInParameter(oDbCommand, "@tipocampania", DbType.Decimal, tipoCamp);
            oDataBase.AddInParameter(oDbCommand, "@estadoCampania", DbType.String, estadoCamp);
            oDataBase.AddInParameter(oDbCommand, "@nombre", DbType.String, nombre);
            oDataBase.AddInParameter(oDbCommand, "@perfilcliente", DbType.Decimal, perfilCliente);
            oDataBase.AddInParameter(oDbCommand, "@fechaIni", DbType.DateTime, fechaIni);
            oDataBase.AddInParameter(oDbCommand, "@fechaFin", DbType.DateTime, fechaFin);
            oDataBase.AddInParameter(oDbCommand, "@estado", DbType.Int32, st);
            oDataBase.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            oDataBase.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            oDataBase.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 50);
            oDataBase.ExecuteNonQuery(oDbCommand);
            string results = Convert.ToString(oDataBase.GetParameterValue(oDbCommand, "@RecordCount"));

            BECampaniaCallCenter item = null;
            List<BECampaniaCallCenter> lista = new List<BECampaniaCallCenter>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BECampaniaCallCenter();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CID")))
                        item.CONC_CID = dr.GetDecimal(dr.GetOrdinal("CONC_CID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CNAME")))
                        item.CONC_CNAME = dr.GetString(dr.GetOrdinal("CONC_CNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CTNAME")))
                        item.CONC_CTNAME = dr.GetString(dr.GetOrdinal("CONC_CTNAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ENT_DESC")))
                        item.ENT_DESC = dr.GetString(dr.GetOrdinal("ENT_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CDINI")))
                        item.CONC_CDINI = dr.GetDateTime(dr.GetOrdinal("CONC_CDINI"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CDEND")))
                        item.CONC_CDEND = dr.GetDateTime(dr.GetOrdinal("CONC_CDEND"));
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

        public List<BECampaniaConsultaAsignarSocio> ListaSociosAsignar(string owner, decimal idTipoLic, decimal idMod, string idGrupoMod, decimal idGrupoFac, decimal idTemp, decimal idSerie, decimal idEst,
            decimal idSubtipoEst, decimal idTipoEst, string idTipoPersona, decimal idTipoDoc, string numeroDoc, string socio, decimal idUbigeo, string usuario, string recaudador, string asociado, string grupo, string empleado, string proveedor, int Estado)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_SOCIO_ASIGNAR_CAMPANIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@LIC_TYPE", DbType.Decimal, idTipoLic);
            oDataBase.AddInParameter(oDbCommand, "@MOD_ID", DbType.Decimal, idMod);
            oDataBase.AddInParameter(oDbCommand, "@MOG_ID", DbType.String, idGrupoMod);
            oDataBase.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, idGrupoFac);
            oDataBase.AddInParameter(oDbCommand, "@RATE_FID", DbType.Decimal, idTemp);
            oDataBase.AddInParameter(oDbCommand, "@INV_NMR", DbType.Decimal, idSerie);
            //-------------------------------------------------------------------------------------
            oDataBase.AddInParameter(oDbCommand, "@EST_ID", DbType.Decimal, idEst);
            oDataBase.AddInParameter(oDbCommand, "@SUBE_ID", DbType.Decimal, idSubtipoEst);
            oDataBase.AddInParameter(oDbCommand, "@ESTT_ID", DbType.Decimal, idTipoEst);
            //------------------------------------------------------------------------------------
            oDataBase.AddInParameter(oDbCommand, "@ENT_TYPE", DbType.String, idTipoPersona);
            oDataBase.AddInParameter(oDbCommand, "@TAXT_ID", DbType.Decimal, idTipoDoc);
            oDataBase.AddInParameter(oDbCommand, "@TAX_ID", DbType.String, numeroDoc);
            oDataBase.AddInParameter(oDbCommand, "@BPS_NAME", DbType.String, socio);
            oDataBase.AddInParameter(oDbCommand, "@ID_UBIGEO", DbType.Decimal, idUbigeo);
            oDataBase.AddInParameter(oDbCommand, "@BPS_USER", DbType.String, usuario);
            oDataBase.AddInParameter(oDbCommand, "@BPS_COLLECTOR", DbType.String, recaudador);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ASSOCIATION", DbType.String, asociado);
            oDataBase.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.String, grupo);
            oDataBase.AddInParameter(oDbCommand, "@BPS_EMPLOYEE", DbType.String, empleado);
            oDataBase.AddInParameter(oDbCommand, "@BPS_SUPPLIER", DbType.String, proveedor);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ENDS", DbType.Int32, Estado);

            List<BECampaniaConsultaAsignarSocio> lista = new List<BECampaniaConsultaAsignarSocio>();
            BECampaniaConsultaAsignarSocio item = null;

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BECampaniaConsultaAsignarSocio();
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TAXN_NAME")))
                        item.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                        item.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                        item.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTN")))
                        item.INV_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTN"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                        item.INV_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));

                    //if (!dr.IsDBNull(dr.GetOrdinal("BPS_USER")))
                    //{
                    //    item.BPS_USER = dr.GetString(dr.GetOrdinal("BPS_USER"));
                    //    if (item.BPS_USER == "1")
                    //        item.PERFIL = "USUARIO";
                    //}
                    //if (!dr.IsDBNull(dr.GetOrdinal("BPS_COLLECTOR")))
                    //{
                    //    item.BPS_COLLECTOR = dr.GetString(dr.GetOrdinal("BPS_COLLECTOR"));
                    //    if (item.BPS_COLLECTOR == "1")
                    //        item.PERFIL = "RECAUDADOR";
                    //}
                    //if (!dr.IsDBNull(dr.GetOrdinal("BPS_ASSOCIATION")))
                    //{
                    //    item.BPS_ASSOCIATION = dr.GetString(dr.GetOrdinal("BPS_ASSOCIATION"));
                    //    if (item.BPS_ASSOCIATION == "1")
                    //        item.PERFIL = "ASOCIADO";
                    //}
                    //if (!dr.IsDBNull(dr.GetOrdinal("BPS_GROUP")))
                    //{
                    //    item.BPS_GROUP = dr.GetString(dr.GetOrdinal("BPS_GROUP"));
                    //    if (item.BPS_GROUP == "1")
                    //        item.PERFIL = "GRUPO EMP";
                    //}
                    //if (!dr.IsDBNull(dr.GetOrdinal("BPS_EMPLOYEE")))
                    //{
                    //    item.BPS_EMPLOYEE = dr.GetString(dr.GetOrdinal("BPS_EMPLOYEE"));
                    //    if (item.BPS_EMPLOYEE == "1")
                    //        item.PERFIL = "EMPLEADO";
                    //}
                    //if (!dr.IsDBNull(dr.GetOrdinal("BPS_SUPPLIER")))
                    //{
                    //    item.BPS_SUPPLIER = dr.GetString(dr.GetOrdinal("BPS_SUPPLIER"));
                    //    if (item.BPS_SUPPLIER == "1")
                    //        item.PERFIL = "PROVEEDOR";
                    //}
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<BECampaniaConsultaAsignarSocio> ListaSociosAsignarDetalle(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_SOCIO_ASIGNAR_CAMPANIA_DET");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            List<BECampaniaConsultaAsignarSocio> lista = new List<BECampaniaConsultaAsignarSocio>();
            BECampaniaConsultaAsignarSocio item = null;

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BECampaniaConsultaAsignarSocio();
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_NAME")))
                        item.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EST_NAME")))
                        item.EST_NAME = dr.GetString(dr.GetOrdinal("EST_NAME"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                        item.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTN")))
                        item.INV_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTN"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                        item.INV_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                        item.CUR_DESC = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<BECampaniaConsultaAsignarSocio> ListaSociosAsignarSubDetalle(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_SOCIO_ASIGNAR_CAMPANIA_SUB_DET");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);

            List<BECampaniaConsultaAsignarSocio> lista = new List<BECampaniaConsultaAsignarSocio>();
            BECampaniaConsultaAsignarSocio item = null;

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BECampaniaConsultaAsignarSocio();
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_PL_ID")))
                        item.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_ID")))
                        item.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LIC_DATE_CAD")))
                        item.LIC_DATE_CAD = dr.GetString(dr.GetOrdinal("LIC_DATE_CAD"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                        item.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTN")))
                        item.INV_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTN"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                        item.INV_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public bool InsertarCampaniaAsignarXML(string xml)
        {
            bool exito = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDAI_ASIGNAR_CAMP_XML"))
            {
                oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                exito = oDataBase.ExecuteNonQuery(cm) > 0;
            }
            return exito;
        }

        public BEContactoAsignarCampania ObtenerSociosAsignados(string owner, decimal idCampania, decimal idSocio)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_OBTENER_CONTAC_CUSTOMER");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, idCampania);
            oDataBase.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idSocio);
            BEContactoAsignarCampania item = null;

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BEContactoAsignarCampania();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CID")))
                        item.CONC_CID = dr.GetDecimal(dr.GetOrdinal("CONC_CID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                        item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                }
            }
            return item;
        }

        public BECampaniaCallCenter obtenerNombreCampania(string owner, decimal idCampania)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_OBTENER_CAMPANIA_DES");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, idCampania);
            BECampaniaCallCenter item = null;

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BECampaniaCallCenter();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CNAME")))
                        item.CONC_CNAME = dr.GetString(dr.GetOrdinal("CONC_CNAME"));
                }
            }
            return item;
        }

        public List<BECampaniaCallCenter> ListarCentroContacto(string owner)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_CENTRO_CONTACTOS");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            BECampaniaCallCenter item = null;
            List<BECampaniaCallCenter> lista = new List<BECampaniaCallCenter>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    item = new BECampaniaCallCenter();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_ID")))
                        item.CONC_ID = dr.GetDecimal(dr.GetOrdinal("CONC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_NAME")))
                        item.CONC_NAME = dr.GetString(dr.GetOrdinal("CONC_NAME"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<BECampaniaCallCenter> ListarCampaniaPorTipo(string owner, decimal idtipo)
        {
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_LISTAR_CAMPANIA");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@CONC_CTID", DbType.String, idtipo);
            BECampaniaCallCenter item = null;
            List<BECampaniaCallCenter> lista = new List<BECampaniaCallCenter>();

            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    item = new BECampaniaCallCenter();
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CID")))
                        item.CONC_CID = dr.GetDecimal(dr.GetOrdinal("CONC_CID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CONC_CNAME")))
                        item.CONC_CNAME = dr.GetString(dr.GetOrdinal("CONC_CNAME"));
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<BECampaniaCallCenter> ListarClientesAsignadosCampania(string owner, decimal Id)
        {
            List<BECampaniaCallCenter> lista = new List<BECampaniaCallCenter>();
            using (DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_VER_ASIGNADOS"))
            {
                oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(oDbCommand, "@CONC_CID", DbType.Decimal, Id);
                BECampaniaCallCenter item = null;

                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        item = new BECampaniaCallCenter();
                        if (!dr.IsDBNull(dr.GetOrdinal("CONC_CID")))
                            item.CONC_CID = dr.GetDecimal(dr.GetOrdinal("CONC_CID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_ID")))
                            item.BPS_ID = dr.GetDecimal(dr.GetOrdinal("BPS_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                            item.BPS_NAME = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_CREAT")))
                            item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        lista.Add(item);
                    }
                }
                return lista;
            }
        }
    }
}
