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
using System.Data;
using System.Xml;


namespace SGRDA.DA.Consulta
{
    public class DAConsultaDocumento
    {

        #region CONSULTA_GENERAL

        public List<BEFactura> ListarConsultaDocumento(string owner, decimal idSerial, decimal numFact, decimal idFactura,
                                                                    decimal idSocio, decimal idGrupoFacturacion, decimal idGrupoEmpresarial,
                                                                    int conFecha, DateTime Fini, DateTime Ffin, decimal idLicencia,
                                                                    decimal idDivision, decimal idOficina, decimal idAgente,
                                                                    string idMoneda, decimal tipoDoc, decimal estado,
                                                                    decimal idDepartamento, decimal idProvincia, decimal idDistrito,int estadoSun,int ORDEN)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CONSULTA_DOCUMENTO");
                //db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.Decimal, idSerial);
                db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, numFact);
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);

                db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idSocio);
                db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, idGrupoFacturacion);
                db.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.Decimal, idGrupoEmpresarial);

                db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
                db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, Fini);
                db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, Ffin);
                db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLicencia);

                db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, idDivision);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
                db.AddInParameter(oDbCommand, "@AGENTE_ID", DbType.Decimal, idAgente);

                db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, idMoneda);
                db.AddInParameter(oDbCommand, "@INV_TYPE", DbType.Decimal, tipoDoc);
                db.AddInParameter(oDbCommand, "@ESTADO", DbType.Decimal, estado);
                db.AddInParameter(oDbCommand, "@ESTADO_SUNAT",DbType.Int32, estadoSun);
                db.AddInParameter(oDbCommand, "@ORDEN", DbType.Int32, ORDEN);

                //db.AddInParameter(oDbCommand, "@DEPARTAMENTO", DbType.Decimal, idDepartamento);
                //db.AddInParameter(oDbCommand, "@PROVINCIA", DbType.Decimal, idProvincia);
                //db.AddInParameter(oDbCommand, "@DISTRITO", DbType.Decimal, idDistrito);

                var lista = new List<BEFactura>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFactura factura = null;
                    while (dr.Read())
                    {
                        factura = new BEFactura();
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_TYPE")))
                            factura.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("INV_TYPE"));
                        else
                            factura.INV_TYPE = 0;
                        if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                            factura.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                            factura.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                            factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_DATE")))
                            factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NULL")))
                            factura.INV_NULL = dr.GetDateTime(dr.GetOrdinal("INV_NULL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAXN_NAME")))
                            factura.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            factura.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));


                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        {
                            factura.SOCIO = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }
                        else
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                                factura.SOCIO += dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                                factura.SOCIO += " " + dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                                factura.SOCIO += " " + dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME")).ToString();
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_F1_NC_F2")))
                            factura.INV_F1_NC_F2 = dr.GetDecimal(dr.GetOrdinal("INV_F1_NC_F2"));
                        
                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                            factura.MONEDA = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                            factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTN")))
                            factura.INV_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                            factura.INV_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));

                        if (factura.INV_TYPE == 1 || factura.INV_TYPE == 2)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_CN_REF")))
                                factura.INV_CN_REF = dr.GetDecimal(dr.GetOrdinal("INV_CN_REF"));
                        }
                        else
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_KEY")))
                                factura.INV_CN_REF = Convert.ToDecimal(dr.GetString(dr.GetOrdinal("INV_KEY")));
                        }

                        //ESTADO QUE DEVUELVE SUNAT PARA LA CONSULTA DE FACTURAS
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SUNAT")))
                        {
                            string estadoSunat = dr.GetString(dr.GetOrdinal("ESTADO_SUNAT"));
                            switch (estadoSunat)
                            {
                                case TipoSunat.DOK: factura.ESTADO_SUNAT = TipoSunatDes.DOK; break;
                                case TipoSunat.FIR: factura.ESTADO_SUNAT = TipoSunatDes.FIR; break;
                                case TipoSunat.RCH: factura.ESTADO_SUNAT = TipoSunatDes.RCH; break;

                                case TipoSunat.ERROR: factura.ESTADO_SUNAT = TipoSunatDes.ERROR; break;
                                case TipoSunat.ERDTE: factura.ESTADO_SUNAT = TipoSunatDes.ERDTE; break;
                                case TipoSunat.EL: factura.ESTADO_SUNAT = TipoSunatDes.EL; break;
                                default: factura.ESTADO_SUNAT = ""; break;
                            }
                        }
                        else
                        {
                            factura.ESTADO_SUNAT = "";
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_MANUAL")))
                        {
                            factura.INV_MANUAL = dr.GetBoolean(dr.GetOrdinal("INV_MANUAL"));
                            factura.TIPO_EMI_DOC = factura.INV_MANUAL ? "M" : "A";
                        }
                        else
                        {
                            factura.INV_MANUAL = true;
                            factura.TIPO_EMI_DOC= "M";
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))

                            factura.EST_FACT = dr.GetInt32(dr.GetOrdinal("ESTADO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CANCELACION")))
                            factura.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("FECHA_CANCELACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_QUIEBRA")))
                            factura.INV_QUIEBRA = dr.GetInt32(dr.GetOrdinal("INV_QUIEBRA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NOTA_CREDITO")))
                            factura.INV_NOTA_CREDITO = dr.GetInt32(dr.GetOrdinal("INV_NOTA_CREDITO"));
                        //factura.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_IND_NC_TOTAL")))
                            factura.INV_IND_NC_TOTAL = dr.GetDecimal(dr.GetOrdinal("INV_IND_NC_TOTAL"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_STATUS_NC")))
                            factura.INV_STATUS_NC = dr.GetDecimal(dr.GetOrdinal("INV_STATUS_NC"));


                        lista.Add(factura);
                    }
                    return lista;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<BEFactura> ListarConsultaDocumento2(string owner, string idSerial, decimal numFact, decimal idFactura, decimal idOficina,
                                                                  int conFecha, DateTime Fini, DateTime Ffin
                                                                  )
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CONSULTA_DOCUMENTO2");
                //db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@NMR_SERIAL", DbType.String, idSerial);
                db.AddInParameter(oDbCommand, "@INV_NUMBER", DbType.Decimal, numFact);
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);

                //db.AddInParameter(oDbCommand, "@BPS_ID", DbType.Decimal, idSocio);
                //db.AddInParameter(oDbCommand, "@INVG_ID", DbType.Decimal, idGrupoFacturacion);
                //db.AddInParameter(oDbCommand, "@BPS_GROUP", DbType.Decimal, idGrupoEmpresarial);

                db.AddInParameter(oDbCommand, "@CON_FECHA", DbType.Int32, conFecha);
                db.AddInParameter(oDbCommand, "@FINI", DbType.DateTime, Fini);
                db.AddInParameter(oDbCommand, "@FFIN", DbType.DateTime, Ffin);
                //db.AddInParameter(oDbCommand, "@LIC_ID", DbType.Decimal, idLicencia);

                //db.AddInParameter(oDbCommand, "@DAD_ID", DbType.Decimal, idDivision);
                db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, idOficina);
                //db.AddInParameter(oDbCommand, "@AGENTE_ID", DbType.Decimal, idAgente);

                //db.AddInParameter(oDbCommand, "@CUR_ALPHA", DbType.String, idMoneda);
                //db.AddInParameter(oDbCommand, "@INV_TYPE", DbType.Decimal, tipoDoc);
                //db.AddInParameter(oDbCommand, "@ESTADO", DbType.Decimal, estado);
                //db.AddInParameter(oDbCommand, "@ESTADO_SUNAT", DbType.Int32, estadoSun);
                //db.AddInParameter(oDbCommand, "@ORDEN", DbType.Int32, ORDEN);

                //db.AddInParameter(oDbCommand, "@DEPARTAMENTO", DbType.Decimal, idDepartamento);
                //db.AddInParameter(oDbCommand, "@PROVINCIA", DbType.Decimal, idProvincia);
                //db.AddInParameter(oDbCommand, "@DISTRITO", DbType.Decimal, idDistrito);

                var lista = new List<BEFactura>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFactura factura = null;
                    while (dr.Read())
                    {
                        factura = new BEFactura();
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_TYPE")))
                            factura.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("INV_TYPE"));
                        else
                            factura.INV_TYPE = 0;
                        if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                            factura.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                            factura.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                            factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_DATE")))
                            factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NULL")))
                            factura.INV_NULL = dr.GetDateTime(dr.GetOrdinal("INV_NULL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAXN_NAME")))
                            factura.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            factura.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));


                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        {
                            factura.SOCIO = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }
                        else
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                                factura.SOCIO += dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                                factura.SOCIO += " " + dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                                factura.SOCIO += " " + dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME")).ToString();
                        }



                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                            factura.MONEDA = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                            factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTN")))
                            factura.INV_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                            factura.INV_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));

                        if (factura.INV_TYPE == 1 || factura.INV_TYPE == 2)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_CN_REF")))
                                factura.INV_CN_REF = dr.GetDecimal(dr.GetOrdinal("INV_CN_REF"));
                        }
                        else
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_KEY")))
                                factura.INV_CN_REF = Convert.ToDecimal(dr.GetString(dr.GetOrdinal("INV_KEY")));
                        }

                        //ESTADO QUE DEVUELVE SUNAT PARA LA CONSULTA DE FACTURAS
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SUNAT")))
                        {
                            string estadoSunat = dr.GetString(dr.GetOrdinal("ESTADO_SUNAT"));
                            switch (estadoSunat)
                            {
                                case TipoSunat.DOK: factura.ESTADO_SUNAT = TipoSunatDes.DOK; break;
                                case TipoSunat.FIR: factura.ESTADO_SUNAT = TipoSunatDes.FIR; break;
                                case TipoSunat.RCH: factura.ESTADO_SUNAT = TipoSunatDes.RCH; break;

                                case TipoSunat.ERROR: factura.ESTADO_SUNAT = TipoSunatDes.ERROR; break;
                                case TipoSunat.ERDTE: factura.ESTADO_SUNAT = TipoSunatDes.ERDTE; break;
                                case TipoSunat.EL: factura.ESTADO_SUNAT = TipoSunatDes.EL; break;
                                default: factura.ESTADO_SUNAT = ""; break;
                            }
                        }
                        else
                        {
                            factura.ESTADO_SUNAT = "";
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_MANUAL")))
                        {
                            factura.INV_MANUAL = dr.GetBoolean(dr.GetOrdinal("INV_MANUAL"));
                            factura.TIPO_EMI_DOC = factura.INV_MANUAL ? "M" : "A";
                        }
                        else
                        {
                            factura.INV_MANUAL = true;
                            factura.TIPO_EMI_DOC = "M";
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))

                            factura.EST_FACT = dr.GetInt32(dr.GetOrdinal("ESTADO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CANCELACION")))
                            factura.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("FECHA_CANCELACION"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_QUIEBRA")))
                            factura.INV_QUIEBRA = dr.GetInt32(dr.GetOrdinal("INV_QUIEBRA"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NOTA_CREDITO")))
                            factura.INV_NOTA_CREDITO = dr.GetInt32(dr.GetOrdinal("INV_NOTA_CREDITO"));
                        //factura.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_IND_NC_TOTAL")))
                            factura.INV_IND_NC_TOTAL = dr.GetDecimal(dr.GetOrdinal("INV_IND_NC_TOTAL"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_STATUS_NC")))
                            factura.INV_STATUS_NC = dr.GetDecimal(dr.GetOrdinal("INV_STATUS_NC"));


                        lista.Add(factura);
                    }
                    return lista;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region TIPO_SUNAT

        public class TipoSunat
        {
            public const string DOK = "DOK";
            public const string FIR = "FIR";
            public const string RCH = "RCH";
            public const string ERROR = "ERROR";
            public const string ERDTE = "ERDTE";
            public const string EL = "EL";
        }

        public class TipoSunatDes
        {
            public const string DOK = "ACEPTADO";
            public const string FIR = "PROCESO DE ENVIO";
            public const string RCH = "RECHAZADO";
            public const string ERROR = "ERROR EN LA SUITE";
            public const string ERDTE = "EL COMPROBANTE YA EXISTE";
            public const string EL = "ERROR DE ENVIO - ANULE LA FACTURA";
        }

        #endregion



        #region CONSULTA_DETALLE
        public List<BEFactura> CD_Cabecera(decimal idFactura)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CONSULTA_DOCUMENTO_CABECERA");
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);
                var lista = new List<BEFactura>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFactura factura = null;
                    while (dr.Read())
                    {
                        factura = new BEFactura();
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_TYPE")))
                            factura.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("INV_TYPE"));
                        else
                            factura.INV_TYPE = 0;
                        if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                            factura.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                            factura.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                            factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_DATE")))
                            factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NULL")))
                            factura.INV_NULL = dr.GetDateTime(dr.GetOrdinal("INV_NULL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAXN_NAME")))
                            factura.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            factura.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));


                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        {
                            factura.SOCIO = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }
                        else
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                                factura.SOCIO += dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                                factura.SOCIO += " " + dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                                factura.SOCIO += " " + dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME")).ToString();
                        }



                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                            factura.MONEDA = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                            factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTN")))
                            factura.INV_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                            factura.INV_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));

                        if (factura.INV_TYPE == 1 || factura.INV_TYPE == 2)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_CN_REF")))
                                factura.INV_CN_REF = dr.GetDecimal(dr.GetOrdinal("INV_CN_REF"));
                        }
                        else
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_KEY")))
                                factura.INV_CN_REF = Convert.ToDecimal(dr.GetString(dr.GetOrdinal("INV_KEY")));
                        }

                        //ESTADO QUE DEVUELVE SUNAT PARA LA CONSULTA DE FACTURAS
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SUNAT")))
                        {
                            string estadoSunat = dr.GetString(dr.GetOrdinal("ESTADO_SUNAT"));
                            switch (estadoSunat)
                            {
                                case TipoSunat.DOK: factura.ESTADO_SUNAT = TipoSunatDes.DOK; break;
                                case TipoSunat.FIR: factura.ESTADO_SUNAT = TipoSunatDes.FIR; break;
                                case TipoSunat.RCH: factura.ESTADO_SUNAT = TipoSunatDes.RCH; break;

                                case TipoSunat.ERROR: factura.ESTADO_SUNAT = TipoSunatDes.ERROR; break;
                                case TipoSunat.ERDTE: factura.ESTADO_SUNAT = TipoSunatDes.ERDTE; break;
                                case TipoSunat.EL: factura.ESTADO_SUNAT = TipoSunatDes.EL; break;
                                default: factura.ESTADO_SUNAT = ""; break;
                            }
                        }
                        else
                        {
                            factura.ESTADO_SUNAT = "";
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_MANUAL")))
                        {
                            factura.INV_MANUAL = dr.GetBoolean(dr.GetOrdinal("INV_MANUAL"));
                            factura.TIPO_EMI_DOC = factura.INV_MANUAL ? "M" : "A";
                        }
                        else
                        {
                            factura.INV_MANUAL = true;
                            factura.TIPO_EMI_DOC = "M";
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))

                            factura.EST_FACT = dr.GetInt32(dr.GetOrdinal("ESTADO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CANCELACION")))
                            factura.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("FECHA_CANCELACION"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_IND_NC_TOTAL")))
                            factura.INV_IND_NC_TOTAL = dr.GetDecimal(dr.GetOrdinal("INV_IND_NC_TOTAL"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_STATUS_NC")))
                            factura.INV_STATUS_NC = dr.GetDecimal(dr.GetOrdinal("INV_STATUS_NC"));


                        //factura.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        lista.Add(factura);
                    }
                    return lista;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<BELicencias> CD_Licencia(decimal idFactura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CONSULTA_DOCUMENTO_LICENCIA");
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);
            var lista = new List<BELicencias>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BELicencias factura = null;
                while (dr.Read())
                {
                    factura = new BELicencias();
                    factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                    factura.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    factura.LIC_NAME = dr.GetString(dr.GetOrdinal("LIC_NAME"));
                    factura.Modalidad = dr.GetString(dr.GetOrdinal("MOD_DEC"));
                    factura.Establecimiento = dr.GetString(dr.GetOrdinal("EST_NAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_GROSS")))
                        factura.INVL_GROSS = dr.GetDecimal(dr.GetOrdinal("INVL_GROSS"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_DISC")))
                        factura.INVL_DISC = dr.GetDecimal(dr.GetOrdinal("INVL_DISC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_BASE")))
                        factura.INVL_BASE = dr.GetDecimal(dr.GetOrdinal("INVL_BASE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_TAXES")))
                        factura.INVL_TAXES = dr.GetDecimal(dr.GetOrdinal("INVL_TAXES"));
                    if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                        factura.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));
                    lista.Add(factura);
                }
            }
            return lista;
        }


        public List<BEFacturaDetalle> CD_Periodos(decimal idFactura)
        { 
            decimal id = 0;
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CONSULTA_DOCUMENTO_PERIODOS");
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);
                var lista = new List<BEFacturaDetalle>();

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFacturaDetalle licPlaneamiento = null;
                    while (dr.Read())
                    {
                        licPlaneamiento = new BEFacturaDetalle();
                        licPlaneamiento.INVL_ID = dr.GetDecimal(dr.GetOrdinal("INVL_ID"));
                        id = licPlaneamiento.INVL_ID;
                        licPlaneamiento.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        licPlaneamiento.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                        licPlaneamiento.LIC_DATE = dr.GetDateTime(dr.GetOrdinal("LIC_DATE"));
                        licPlaneamiento.LIC_YEAR = dr.GetDecimal(dr.GetOrdinal("LIC_YEAR"));
                        licPlaneamiento.LIC_MONTH = dr.GetDecimal(dr.GetOrdinal("LIC_MONTH"));

                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_GROSS")))
                            licPlaneamiento.INVL_GROSS = dr.GetDecimal(dr.GetOrdinal("INVL_GROSS"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_DISC")))
                            licPlaneamiento.INVL_DISC = dr.GetDecimal(dr.GetOrdinal("INVL_DISC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_BASE")))
                            licPlaneamiento.INVL_BASE = dr.GetDecimal(dr.GetOrdinal("INVL_BASE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_TAXES")))
                            licPlaneamiento.INVL_TAXES = dr.GetDecimal(dr.GetOrdinal("INVL_TAXES"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INVL_NET")))
                            licPlaneamiento.INVL_NET = dr.GetDecimal(dr.GetOrdinal("INVL_NET"));

                        licPlaneamiento.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                        licPlaneamiento.EST_ID = dr.GetDecimal(dr.GetOrdinal("EST_ID"));
                        lista.Add(licPlaneamiento);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                decimal dec = id;
                return null;
            }
        }



        #endregion


        public List<BEFactura> LICENCIA_DETALLE_FACTURA_X_PERIODO(decimal idPeriodo)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CONSULTA_DOCUMENTO_CABECERA_X_PERIODO");
                db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, idPeriodo);
                var lista = new List<BEFactura>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFactura factura = null;
                    while (dr.Read())
                    {
                        factura = new BEFactura();
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_TYPE")))
                            factura.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("INV_TYPE"));
                        else
                            factura.INV_TYPE = 0;
                        if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                            factura.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                            factura.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                            factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_DATE")))
                            factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NULL")))
                            factura.INV_NULL = dr.GetDateTime(dr.GetOrdinal("INV_NULL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAXN_NAME")))
                            factura.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            factura.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));


                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        {
                            factura.SOCIO = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }
                        else
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                                factura.SOCIO += dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                                factura.SOCIO += " " + dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                                factura.SOCIO += " " + dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME")).ToString();
                        }



                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                            factura.MONEDA = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                            factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTN")))
                            factura.INV_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                            factura.INV_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));

                        if (factura.INV_TYPE == 1 || factura.INV_TYPE == 2)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_CN_REF")))
                                factura.INV_CN_REF = dr.GetDecimal(dr.GetOrdinal("INV_CN_REF"));
                        }
                        else
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_KEY")))
                                factura.INV_CN_REF = Convert.ToDecimal(dr.GetString(dr.GetOrdinal("INV_KEY")));
                        }

                        //ESTADO QUE DEVUELVE SUNAT PARA LA CONSULTA DE FACTURAS
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SUNAT")))
                        {
                            string estadoSunat = dr.GetString(dr.GetOrdinal("ESTADO_SUNAT"));
                            switch (estadoSunat)
                            {
                                case TipoSunat.DOK: factura.ESTADO_SUNAT = TipoSunatDes.DOK; break;
                                case TipoSunat.FIR: factura.ESTADO_SUNAT = TipoSunatDes.FIR; break;
                                case TipoSunat.RCH: factura.ESTADO_SUNAT = TipoSunatDes.RCH; break;

                                case TipoSunat.ERROR: factura.ESTADO_SUNAT = TipoSunatDes.ERROR; break;
                                case TipoSunat.ERDTE: factura.ESTADO_SUNAT = TipoSunatDes.ERDTE; break;
                                case TipoSunat.EL: factura.ESTADO_SUNAT = TipoSunatDes.EL; break;
                                default: factura.ESTADO_SUNAT = ""; break;
                            }
                        }
                        else
                        {
                            factura.ESTADO_SUNAT = "";
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_MANUAL")))
                        {
                            factura.INV_MANUAL = dr.GetBoolean(dr.GetOrdinal("INV_MANUAL"));
                            factura.TIPO_EMI_DOC = factura.INV_MANUAL ? "M" : "A";
                        }
                        else
                        {
                            factura.INV_MANUAL = true;
                            factura.TIPO_EMI_DOC = "M";
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))

                            factura.EST_FACT = dr.GetInt32(dr.GetOrdinal("ESTADO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CANCELACION")))
                            factura.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("FECHA_CANCELACION"));
                        //factura.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        lista.Add(factura);
                    }
                    return lista;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int ValidaDocumentoCobro(int INV_ID)
        {

            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("VALIDA_DOCUMENTO_COBRO");
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Int32, INV_ID);
                db.AddOutParameter(oDbCommand, "@RETORNO", DbType.Int32, 0);
            int n = db.ExecuteNonQuery(oDbCommand);
            int exito = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@RETORNO"));

            return exito;
        }
        public int Valida_Fecha_Factura_Para_NC(int INV_ID)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("VALIDA_FECHA_PARA_NC");
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Int32, INV_ID);
            db.AddOutParameter(oDbCommand, "@RETORNO", DbType.Int32, 0);
            int n = db.ExecuteNonQuery(oDbCommand);
            int exito = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@RETORNO"));
            return exito;
        }

        public List<BEFactura> CD_Referencia(decimal idFactura)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_CONSULTA_DOCUMENTO_CABECERA_REFERENCIA");
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, idFactura);
                var lista = new List<BEFactura>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    BEFactura factura = null;
                    while (dr.Read())
                    {
                        factura = new BEFactura();
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_ID")))
                            factura.INV_ID = dr.GetDecimal(dr.GetOrdinal("INV_ID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_TYPE")))
                            factura.INV_TYPE = dr.GetDecimal(dr.GetOrdinal("INV_TYPE"));
                        else
                            factura.INV_TYPE = 0;
                        if (!dr.IsDBNull(dr.GetOrdinal("INVT_DESC")))
                            factura.INVT_DESC = dr.GetString(dr.GetOrdinal("INVT_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("NMR_SERIAL")))
                            factura.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NUMBER")))
                            factura.INV_NUMBER = dr.GetDecimal(dr.GetOrdinal("INV_NUMBER"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_DATE")))
                            factura.INV_DATE = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NULL")))
                            factura.INV_NULL = dr.GetDateTime(dr.GetOrdinal("INV_NULL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAXN_NAME")))
                            factura.TAXN_NAME = dr.GetString(dr.GetOrdinal("TAXN_NAME"));
                        if (!dr.IsDBNull(dr.GetOrdinal("TAX_ID")))
                            factura.TAX_ID = dr.GetString(dr.GetOrdinal("TAX_ID"));


                        if (!dr.IsDBNull(dr.GetOrdinal("BPS_NAME")))
                        {
                            factura.SOCIO = dr.GetString(dr.GetOrdinal("BPS_NAME"));
                        }
                        else
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FIRST_NAME")))
                                factura.SOCIO += dr.GetString(dr.GetOrdinal("BPS_FIRST_NAME")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_FATH_SURNAME")))
                                factura.SOCIO += " " + dr.GetString(dr.GetOrdinal("BPS_FATH_SURNAME")).ToString();
                            if (!dr.IsDBNull(dr.GetOrdinal("BPS_MOTH_SURNAME")))
                                factura.SOCIO += " " + dr.GetString(dr.GetOrdinal("BPS_MOTH_SURNAME")).ToString();
                        }



                        if (!dr.IsDBNull(dr.GetOrdinal("CUR_DESC")))
                            factura.MONEDA = dr.GetString(dr.GetOrdinal("CUR_DESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_NET")))
                            factura.INV_NET = dr.GetDecimal(dr.GetOrdinal("INV_NET"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_COLLECTN")))
                            factura.INV_COLLECTN = dr.GetDecimal(dr.GetOrdinal("INV_COLLECTN"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_BALANCE")))
                            factura.INV_BALANCE = dr.GetDecimal(dr.GetOrdinal("INV_BALANCE"));

                        if (factura.INV_TYPE == 1 || factura.INV_TYPE == 2)
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_CN_REF")))
                                factura.INV_CN_REF = dr.GetDecimal(dr.GetOrdinal("INV_CN_REF"));
                        }
                        else
                        {
                            if (!dr.IsDBNull(dr.GetOrdinal("INV_KEY")))
                                factura.INV_CN_REF = Convert.ToDecimal(dr.GetString(dr.GetOrdinal("INV_KEY")));
                        }

                        //ESTADO QUE DEVUELVE SUNAT PARA LA CONSULTA DE FACTURAS
                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO_SUNAT")))
                        {
                            string estadoSunat = dr.GetString(dr.GetOrdinal("ESTADO_SUNAT"));
                            switch (estadoSunat)
                            {
                                case TipoSunat.DOK: factura.ESTADO_SUNAT = TipoSunatDes.DOK; break;
                                case TipoSunat.FIR: factura.ESTADO_SUNAT = TipoSunatDes.FIR; break;
                                case TipoSunat.RCH: factura.ESTADO_SUNAT = TipoSunatDes.RCH; break;

                                case TipoSunat.ERROR: factura.ESTADO_SUNAT = TipoSunatDes.ERROR; break;
                                case TipoSunat.ERDTE: factura.ESTADO_SUNAT = TipoSunatDes.ERDTE; break;
                                case TipoSunat.EL: factura.ESTADO_SUNAT = TipoSunatDes.EL; break;
                                default: factura.ESTADO_SUNAT = ""; break;
                            }
                        }
                        else
                        {
                            factura.ESTADO_SUNAT = "";
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("INV_MANUAL")))
                        {
                            factura.INV_MANUAL = dr.GetBoolean(dr.GetOrdinal("INV_MANUAL"));
                            factura.TIPO_EMI_DOC = factura.INV_MANUAL ? "M" : "A";
                        }
                        else
                        {
                            factura.INV_MANUAL = true;
                            factura.TIPO_EMI_DOC = "M";
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("ESTADO")))
                            factura.EST_FACT = dr.GetInt32(dr.GetOrdinal("ESTADO"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_CANCELACION")))
                            factura.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("FECHA_CANCELACION"));
                        //factura.FECHA_CANCELACION = dr.GetDateTime(dr.GetOrdinal("INV_DATE"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_IND_NC_TOTAL")))
                            factura.INV_IND_NC_TOTAL = dr.GetDecimal(dr.GetOrdinal("INV_IND_NC_TOTAL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("INV_STATUS_NC")))
                            factura.INV_STATUS_NC = dr.GetDecimal(dr.GetOrdinal("INV_STATUS_NC"));

                        lista.Add(factura);
                    }
                    return lista;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<int> UsuariosAprobadosParaAnular()
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("UsuariosAprobadosAnular");
                var lista = new List<int>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        int codigo = 0;                       
                        if (!dr.IsDBNull(dr.GetOrdinal("Codigo")))
                            codigo = dr.GetInt32(dr.GetOrdinal("Codigo"));
                        lista.Add(codigo);
                    }
                    return lista;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<BEMultiRecibo> CobrosxFactura(decimal CodigoFactura)
        {
            try
            {
                Database db = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_LISTAR_COBROS_X_FACT");
                db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, CodigoFactura);

                var lista = new List<BEMultiRecibo>();
                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        BEMultiRecibo entidad = new BEMultiRecibo();
                        if (!dr.IsDBNull(dr.GetOrdinal("MREC_ID")))
                            entidad.MREC_ID = dr.GetDecimal(dr.GetOrdinal("MREC_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REC_ID")))
                            entidad.REC_ID = dr.GetDecimal(dr.GetOrdinal("REC_ID"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REC_DATEDEPOSITE")))
                            entidad.FECH_DEPO = dr.GetDateTime(dr.GetOrdinal("REC_DATEDEPOSITE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REC_DATECONFIRMED")))
                            entidad.FECHA_CONFIR = dr.GetDateTime(dr.GetOrdinal("REC_DATECONFIRMED"));


                        if (!dr.IsDBNull(dr.GetOrdinal("REC_PVALUE")))
                            entidad.MONTO = dr.GetDecimal(dr.GetOrdinal("REC_PVALUE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("REC_REFERENCE")))
                            entidad.VOUCHER = dr.GetString(dr.GetOrdinal("REC_REFERENCE"));

                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_CREAT")))
                            entidad.MREC_DATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));

                        if (!dr.IsDBNull(dr.GetOrdinal("OFF_NAME")))
                            entidad.OFICINA = dr.GetString(dr.GetOrdinal("OFF_NAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("VDESC")))
                            entidad.ESTADO_COBRO = dr.GetString(dr.GetOrdinal("VDESC"));

                        if (!dr.IsDBNull(dr.GetOrdinal("VERSION")))
                            entidad.VERSION = dr.GetString(dr.GetOrdinal("VERSION"));

                        lista.Add(entidad);
                    }
                    return lista;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int VALIDAR_ANULACION_X_MODALIDAD(decimal INV_ID,decimal OFF_ID)
        {

            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("VALIDAR_ANULACION_X_MODALIDAD");
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, INV_ID);
            db.AddInParameter(oDbCommand, "@OFF_ID", DbType.Decimal, OFF_ID);
            db.AddOutParameter(oDbCommand, "@RESULT", DbType.Int32, 0);
            int n = db.ExecuteNonQuery(oDbCommand);
            int result = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@RESULT"));

            return result;
        }

    }
}
