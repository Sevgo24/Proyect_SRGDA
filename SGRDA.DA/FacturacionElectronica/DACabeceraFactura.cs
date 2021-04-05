using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGRDA.Entities.FacturaElectronica;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace SGRDA.DA.FacturacionElectronica
{
    public class DACabeceraFactura
    {
        Database db = new DatabaseProviderFactory().Create("conexion");

        public List<BECabeceraFactura> ListarCabeceraFactura(string owner, decimal IdFactura)
        {
            //DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_INTERFAZ_FACTURACION_CAB");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_INTERFAZ_FACTURACION_CAB_PRUEBASUNAT");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, IdFactura);
            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BECabeceraFactura>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BECabeceraFactura factura = null;
                while (dr.Read())
                {
                    factura = new BECabeceraFactura();
                    factura.TipoDTE = dr.GetString(dr.GetOrdinal("TipoDTE"));
                    factura.Serie = dr.GetString(dr.GetOrdinal("Serie"));
                    factura.Correlativo = dr.GetString(dr.GetOrdinal("Correlativo"));
                    factura.FChEmis = dr.GetString(dr.GetOrdinal("FChEmis")).Substring(0, 10);
                    factura.FChVen = dr.GetString(dr.GetOrdinal("FChVen"));
                    factura.TipoMoneda = dr.GetString(dr.GetOrdinal("TipoMoneda"));
                    factura.RUTEmisor = dr.GetString(dr.GetOrdinal("RUTEmisor"));
                    factura.TipoRucEmis = dr.GetString(dr.GetOrdinal("TipoRucEmis"));
                    factura.RznSocEmis = dr.GetString(dr.GetOrdinal("RznSocEmis"));
                    factura.NomComer = dr.GetString(dr.GetOrdinal("NomComer"));
                    factura.DirEmis = dr.GetString(dr.GetOrdinal("DirEmis"));
                    factura.CodiComu = dr.GetString(dr.GetOrdinal("CodiComu"));
                    factura.TipoRUTRecep = dr.GetString(dr.GetOrdinal("TipoRUTRecep"));
                    factura.CodiUsuario = dr.GetString(dr.GetOrdinal("CodiUsuario"));
                    factura.RUTRecep = dr.GetString(dr.GetOrdinal("RUTRecep"));
                    factura.RznSocRecep = dr.GetString(dr.GetOrdinal("RznSocRecep"));
                    factura.DirRecep = dr.GetString(dr.GetOrdinal("DirRecep"));
                    factura.Grupo = dr.GetString(dr.GetOrdinal("Grupo"));
                    factura.CorreoUsuario = dr.GetString(dr.GetOrdinal("CorreoUsuario"));
                    factura.MntNeto = dr.GetDecimal(dr.GetOrdinal("MntNeto"));
                    factura.MntExe = dr.GetDecimal(dr.GetOrdinal("MntExe"));
                    factura.MntExo = dr.GetDecimal(dr.GetOrdinal("MntExo"));
                    factura.MntTotal = dr.GetDecimal(dr.GetOrdinal("MntTotal"));
                    factura.TipoOper = dr.GetString(dr.GetOrdinal("TipoOper"));
                    factura.OficinaRecaudo = dr.GetString(dr.GetOrdinal("OficinaRecaudo"));
                    // Agregar Campos Facturacion UBL 2.1
                    factura.HoraEmision = dr.GetString(dr.GetOrdinal("FChEmis")).Substring(11, 8);
                    factura.CodigoLocal = "0000";
                    factura.FormaPago = dr.GetString(dr.GetOrdinal("FormaPago"));
                    factura.MontoNetoPendPago = dr.GetDecimal(dr.GetOrdinal("MontoNetoPendPago"));


                    lista.Add(factura);
                }
            }
            return lista;
        }

        public List<BECabeceraFactura> ListarCabeceraFacturaEmision(decimal IdFactura, decimal serie, decimal dir)
        {
            //DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_INTERFAZ_EMISION_CAB");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_INTERFAZ_EMISION_CAB_PRUEBASUNAT");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, IdFactura);
            db.AddInParameter(oDbCommand, "@NMR_ID", DbType.Decimal, serie);
            db.AddInParameter(oDbCommand, "@ADD_ID", DbType.Decimal, dir);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BECabeceraFactura>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BECabeceraFactura factura = null;
                while (dr.Read())
                {
                    factura = new BECabeceraFactura();
                    factura.TipoDTE = dr.GetString(dr.GetOrdinal("TipoDTE"));
                    factura.Serie = dr.GetString(dr.GetOrdinal("Serie"));
                    factura.Correlativo = dr.GetString(dr.GetOrdinal("Correlativo"));
                    factura.FChEmis = dr.GetString(dr.GetOrdinal("FChEmis"));
                    factura.FChVen = dr.GetString(dr.GetOrdinal("FChVen"));
                    factura.TipoMoneda = dr.GetString(dr.GetOrdinal("TipoMoneda"));
                    factura.RUTEmisor = dr.GetString(dr.GetOrdinal("RUTEmisor"));
                    factura.TipoRucEmis = dr.GetString(dr.GetOrdinal("TipoRucEmis"));
                    factura.RznSocEmis = dr.GetString(dr.GetOrdinal("RznSocEmis"));
                    factura.NomComer = dr.GetString(dr.GetOrdinal("NomComer"));
                    factura.DirEmis = dr.GetString(dr.GetOrdinal("DirEmis"));
                    factura.CodiComu = dr.GetString(dr.GetOrdinal("CodiComu"));
                    factura.TipoRUTRecep = dr.GetString(dr.GetOrdinal("TipoRUTRecep"));
                    factura.CodiUsuario = dr.GetString(dr.GetOrdinal("CodiUsuario"));
                    factura.RUTRecep = dr.GetString(dr.GetOrdinal("RUTRecep"));
                    factura.RznSocRecep = dr.GetString(dr.GetOrdinal("RznSocRecep"));
                    factura.DirRecep = dr.GetString(dr.GetOrdinal("DirRecep"));
                    factura.Grupo = dr.GetString(dr.GetOrdinal("Grupo"));
                    factura.CorreoUsuario = dr.GetString(dr.GetOrdinal("CorreoUsuario"));
                    factura.MntNeto = dr.GetDecimal(dr.GetOrdinal("MntNeto"));
                    factura.MntExe = dr.GetDecimal(dr.GetOrdinal("MntExe"));
                    factura.MntExo = dr.GetDecimal(dr.GetOrdinal("MntExo"));
                    factura.MntTotal = dr.GetDecimal(dr.GetOrdinal("MntTotal"));
                    factura.TipoOper = dr.GetString(dr.GetOrdinal("TipoOper"));
                    factura.OficinaRecaudo = dr.GetString(dr.GetOrdinal("OficinaRecaudo"));
                    factura.FormaPago = dr.GetString(dr.GetOrdinal("FormaPago"));
                    factura.MontoNetoPendPago = dr.GetDecimal(dr.GetOrdinal("MontoNetoPendPago"));
                    lista.Add(factura);
                }
            }
            return lista;
        }

        public List<BECabeceraFactura> ListarCabeceraPreview(string owner, decimal IdFactura)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_FACTURACION_PREVIEW_CAB");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, IdFactura);
            //db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BECabeceraFactura>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BECabeceraFactura factura = null;
                while (dr.Read())
                {
                    factura = new BECabeceraFactura();
                    factura.TipoDTE = dr.GetString(dr.GetOrdinal("TipoDTE"));
                    factura.Serie = dr.GetString(dr.GetOrdinal("Serie"));
                    factura.Correlativo = dr.GetString(dr.GetOrdinal("Correlativo"));
                    factura.FChEmis = dr.GetString(dr.GetOrdinal("FChEmis"));
                    factura.FChVen = dr.GetString(dr.GetOrdinal("FChVen"));
                    factura.TipoMoneda = dr.GetString(dr.GetOrdinal("TipoMoneda"));
                    factura.RUTEmisor = dr.GetString(dr.GetOrdinal("RUTEmisor"));
                    factura.TipoRucEmis = dr.GetString(dr.GetOrdinal("TipoRucEmis"));
                    factura.RznSocEmis = dr.GetString(dr.GetOrdinal("RznSocEmis"));
                    factura.NomComer = dr.GetString(dr.GetOrdinal("NomComer"));
                    factura.DirEmis = dr.GetString(dr.GetOrdinal("DirEmis"));
                    factura.CodiComu = dr.GetString(dr.GetOrdinal("CodiComu"));
                    factura.TipoRUTRecep = dr.GetString(dr.GetOrdinal("TipoRUTRecep"));
                    factura.CodiUsuario = dr.GetString(dr.GetOrdinal("CodiUsuario"));
                    factura.RUTRecep = dr.GetString(dr.GetOrdinal("RUTRecep"));
                    factura.RznSocRecep = dr.GetString(dr.GetOrdinal("RznSocRecep"));
                    factura.DirRecep = dr.GetString(dr.GetOrdinal("DirRecep"));
                    factura.Grupo = dr.GetString(dr.GetOrdinal("Grupo"));
                    factura.CorreoUsuario = dr.GetString(dr.GetOrdinal("CorreoUsuario"));
                    factura.MntNeto = dr.GetDecimal(dr.GetOrdinal("MntNeto"));
                    factura.MntExe = dr.GetDecimal(dr.GetOrdinal("MntExe"));
                    factura.MntExo = dr.GetDecimal(dr.GetOrdinal("MntExo"));
                    factura.MntTotal = dr.GetDecimal(dr.GetOrdinal("MntTotal"));
                    factura.TipoOper = dr.GetString(dr.GetOrdinal("TipoOper"));
                    factura.OficinaRecaudo = dr.GetString(dr.GetOrdinal("OficinaRecaudo"));
                    lista.Add(factura);
                }
            }
            return lista;
        }

        public List<BECabeceraFactura> ListarCabeceraFacturaNc(string owner, decimal IdFactura)
        {
            //DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_INTERFAZ_FACTURACION_CAB_NC");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_INTERFAZ_FACTURACION_CAB_NC_PRUEBASUNAT");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, IdFactura);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BECabeceraFactura>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BECabeceraFactura factura = null;
                while (dr.Read())
                {
                    factura = new BECabeceraFactura();
                    factura.TipoDTE = dr.GetString(dr.GetOrdinal("TipoDTE"));
                    factura.Serie = dr.GetString(dr.GetOrdinal("Serie"));
                    factura.Correlativo = dr.GetString(dr.GetOrdinal("Correlativo"));
                    factura.FChEmis = dr.GetString(dr.GetOrdinal("FChEmis")).Substring(0, 10);
                    factura.FChVen = dr.GetString(dr.GetOrdinal("FChVen"));
                    factura.TipoMoneda = dr.GetString(dr.GetOrdinal("TipoMoneda"));
                    factura.RUTEmisor = dr.GetString(dr.GetOrdinal("RUTEmisor"));
                    factura.TipoRucEmis = dr.GetString(dr.GetOrdinal("TipoRucEmis"));
                    factura.RznSocEmis = dr.GetString(dr.GetOrdinal("RznSocEmis"));
                    factura.NomComer = dr.GetString(dr.GetOrdinal("NomComer"));
                    factura.DirEmis = dr.GetString(dr.GetOrdinal("DirEmis"));
                    factura.CodiComu = dr.GetString(dr.GetOrdinal("CodiComu"));
                    factura.TipoRUTRecep = dr.GetString(dr.GetOrdinal("TipoRUTRecep"));
                    factura.CodiUsuario = dr.GetString(dr.GetOrdinal("CodiUsuario"));
                    factura.RUTRecep = dr.GetString(dr.GetOrdinal("RUTRecep"));
                    factura.RznSocRecep = dr.GetString(dr.GetOrdinal("RznSocRecep"));
                    factura.DirRecep = dr.GetString(dr.GetOrdinal("DirRecep"));
                    factura.Grupo = dr.GetString(dr.GetOrdinal("Grupo"));
                    factura.CorreoUsuario = dr.GetString(dr.GetOrdinal("CorreoUsuario"));
                    factura.Sustento = dr.GetString(dr.GetOrdinal("Sustento"));
                    factura.TipoNotaCredito = dr.GetString(dr.GetOrdinal("TipoNotaCredito"));
                    factura.MntNeto = dr.GetDecimal(dr.GetOrdinal("MntNeto"));
                    factura.MntExe = dr.GetDecimal(dr.GetOrdinal("MntExe"));
                    factura.MntExo = dr.GetDecimal(dr.GetOrdinal("MntExo"));
                    factura.MntTotal = dr.GetDecimal(dr.GetOrdinal("MntTotal"));
                    factura.TipoOper = dr.GetString(dr.GetOrdinal("TipoOper"));
                    factura.OficinaRecaudo = dr.GetString(dr.GetOrdinal("OficinaRecaudo"));
                    factura.Id_Ref = dr.GetDecimal(dr.GetOrdinal("Id_Ref"));
                    factura.CodigoLocal = "0000";
                    factura.HoraEmision = dr.GetString(dr.GetOrdinal("FChEmis")).Substring(11, 8);
                    factura.FormaPago = dr.GetString(dr.GetOrdinal("FormaPago"));
                    factura.MontoNetoPendPago = dr.GetDecimal(dr.GetOrdinal("MontoNetoPendPago"));
                    lista.Add(factura);
                }
            }
            return lista;
        }

        public List<BECabeceraFactura> ObtenerCorrelativo(string owner, string serie)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("OBTENER_CORRELATIVO");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@SERIE", DbType.String, serie);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BECabeceraFactura>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BECabeceraFactura factura = null;
                while (dr.Read())
                {
                    factura = new BECabeceraFactura();
                    factura.Correlativo = dr.GetString(dr.GetOrdinal("Correlativo"));
                    lista.Add(factura);
                }
            }
            return lista;
        }
    }
}
