using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;

using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.Reflection;
using SGRDA.Entities.Reporte;
using SGRDA.BL.Reporte;
using SGRDA.Entities;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using SGRDA.BL;
using SGRDA.Entities.WorkFlow;
using SGRDA.Utility;




using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;



namespace SGRDA.Documento
{
    public class WordDocumentReport
    {
        const string NomAplicacion = "SGRDA";
        const string UsuarioActual = "ADMIN";
        const string ReplaceVacio = "__";

        public void CrearReporteBaileEspectaculos(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            //string sourceFile = GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculos.docx";
            //string destinationFile = GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.docx";
            //System.IO.File.Copy(sourceFile, destinationFile, true);string sourceFile = nombreFile;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("@Autorizado", "INVERSIONES CYE SAC");
            keyValues.Add("NumDocAut", "20553343586");
            keyValues.Add("@Representante", "RONALD GARCIA CHERRES");
            keyValues.Add("NumDocRep", "45232048");
            keyValues.Add("DirRep", "JR.LAS PERAS 218 URB. NARANJAL - INDEPENDENCIA");
            keyValues.Add("DistritoRep", "INDEPENDENCIA");
            keyValues.Add("@Evento", "FIESTA ROCK");
            keyValues.Add("@Artistas", "6 VOLTIOS, AMEN, BANDA NI VOZ NI VOTO, CHABELOS, DANIEL F, DIFONIA, EL TRI DE MEXICO, GRUPO PANDA, GRUPO RIO, INYECTORES, JORGE GONZALEZ, LEUSEMIA, LIBIDO, LOS MOJARRAS, MAR DE COPAS, PSICOS");
            keyValues.Add("@Fechas", "25/02/2014");
            keyValues.Add("@Local", "PARQUE DE LA EXPOSICION");
            keyValues.Add("DireccionEvento", "AV.28 DE JULIO S/N");
            keyValues.Add("DistritoEvento", "LIMA");
            keyValues.Add("ImporteLong", "20,720.70 (VEINTE MIL SETECIENTOS VEINTE SOLES 70/100 N.S.)");
            keyValues.Add("ImporteShort", "(S/. 20,720.70)");
            keyValues.Add("@AUMP", "20141227E1277L221176");
            keyValues.Add("@", "");
            SearchAndReplace(destinationFile, keyValues);
            string RutaWord = destinationFile; // GlobalVars.Global.DocumentoBailesEspectaculos + "CartaInfVerificaciónUsoObrasMusicalescopia.docx";
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            //FindandReplace(destinationFile, keyValues);
            //Convert(GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.docx", GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.pdf", WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteCartaInformativaVerificacionObrasMusicales(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            var datosLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);

            string IdLic = ReplaceVacio;
            string RazonSocial = ReplaceVacio;
            string NombreLocal = ReplaceVacio;
            string DireccionLocal = ReplaceVacio;

            string FechaCorta = new BLReporte().FechaActualShort();
            string Fecha = string.Empty;
            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (datosLicencia != null)
            {
                IdLic = datosLicencia.IdLicencia == null ? ReplaceVacio : datosLicencia.IdLicencia.ToString();
                RazonSocial = datosLicencia.RazonSocial == string.Empty ? ReplaceVacio : datosLicencia.RazonSocial.Trim();
            }

            if (establecimiento != null)
            {
                NombreLocal = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                DireccionLocal = establecimiento.ADDRESS == string.Empty ? ReplaceVacio : establecimiento.ADDRESS.Trim();
            }

            keyValues.Add("@Fecha", Fecha == string.Empty ? ReplaceVacio : Fecha);
            keyValues.Add("Numero", IdLic);
            keyValues.Add("Repest", RazonSocial);
            keyValues.Add("@Local", NombreLocal);
            keyValues.Add("Direccion", DireccionLocal);
            keyValues.Add("@Oficinas", GlobalVars.Global.DireccionApdayc);
            //keyValues.Add("@Atentamente", "David balvis");
            keyValues.Add("@", string.Empty);
            FindandReplace(destinationFile, keyValues);
            string RutaWord = destinationFile;
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteCartaReiterativaAutorizacionObrasMusicales(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            var datosLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            string fechaCartaInformativa = new BLReporte().FechaCartaInformativa(GlobalVars.Global.OWNER, IdLicencia);

            string IdLic = ReplaceVacio;
            string RazonSocial = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string Fecha = string.Empty;
            string FechaCorta = new BLReporte().FechaActualShort();
            string FechaInf = ReplaceVacio;

            if (!string.IsNullOrEmpty(fechaCartaInformativa))
            {
                FechaInf = fechaCartaInformativa;
            }

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (datosLicencia != null)
            {
                IdLic = datosLicencia.IdLicencia == null ? ReplaceVacio : datosLicencia.IdLicencia.ToString();
                RazonSocial = datosLicencia.RazonSocial == string.Empty ? ReplaceVacio : datosLicencia.RazonSocial.Trim();
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
            }

            keyValues.Add("Date", Fecha);
            keyValues.Add("@Fecha", FechaInf);
            keyValues.Add("Numero", IdLic);
            keyValues.Add("Repre", RazonSocial);
            keyValues.Add("@Local", LocalName);
            keyValues.Add("@Oficinas", GlobalVars.Global.DireccionApdayc);
            //keyValues.Add("@Atentamente", "David balvis");
            keyValues.Add("@", string.Empty);
            FindandReplace(destinationFile, keyValues);
            string RutaPDF = rutaPDF;
            string RutaWord = nombreFileCopy;
            Convert(RutaWord, RutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteFichaLevantamientoInformacion(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            double ValorMusica = 0;
            double horasDias = 0;
            double DiasMes = 0;
            double HorasMusicaMes = 0;
            double CategoriaLocal = 0;
            double MedioEjecucion = 0;
            double Tarifa = 0;
            double AforoLocal = 0;
            double IncidenciaObra = 0;

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            List<BEModalidadIncidencia> modalidadIncidencia = new List<BEModalidadIncidencia>();
            var representantelegal = new BLReporte().ObtenerRepresentanteLegal(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var datosLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia);
            var valorunidadMusical = new BLReporte().ValorUnidadMusical(GlobalVars.Global.OWNER);
            var direccionCobranza = new BLReporte().ObtenerDatosDireccionCobranza(GlobalVars.Global.OWNER, IdLicencia);
            modalidadIncidencia = new BLReporte().ListarTipo(GlobalVars.Global.OWNER);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdRum);

            string distritoRep = ReplaceVacio;
            string provinciaRep = ReplaceVacio;
            string departamentoRep = ReplaceVacio;
            string distritoLoc = ReplaceVacio;
            string provinciaLoc = ReplaceVacio;
            string departamentoLoc = ReplaceVacio;
            string distritoCob = ReplaceVacio;
            string provinciaCob = ReplaceVacio;
            string departamentoCob = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalGiro = ReplaceVacio;
            string Mes = ReplaceVacio;
            string Anio = ReplaceVacio;
            string Valor = ReplaceVacio;

            string Version = ReplaceVacio;
            string Rum = ReplaceVacio;
            string Razonsocial = ReplaceVacio;
            string NumRazonSocial = ReplaceVacio;
            string Ruc = ReplaceVacio;
            string Phone = ReplaceVacio;
            string Fx = ReplaceVacio;
            string Representante = ReplaceVacio;
            string RepresentanteDoc = ReplaceVacio;
            string Indispensable = ReplaceVacio;
            string Necesaria = ReplaceVacio;
            string Secundaria = ReplaceVacio;
            string IndispensableVal = ReplaceVacio;
            string NecesariaVal = ReplaceVacio;
            string SecundariaVal = ReplaceVacio;

            decimal valIsp = 0;
            decimal valCs = 0;
            decimal valSc = 0;
            decimal vSuma = 0;

            string Fecha = ReplaceVacio;
            string Time = ReplaceVacio;

            var varFecha = new BLReporte().FechaActualShort();
            var varTime = DateTime.Now.ToShortTimeString();

            Version = "03";

            if (!string.IsNullOrEmpty(varFecha))
            {
                Fecha = varFecha;
            }
            else
            {
                Fecha = DateTime.Now.ToShortDateString();
            }

            if (!string.IsNullOrEmpty(varTime))
            {
                Time = varTime;
            }

            if (representantelegal != null)
            {
                Representante = representantelegal.BPS_NAME == null ? ReplaceVacio : representantelegal.BPS_NAME.Trim();
                RepresentanteDoc = representantelegal.TAX_ID == null ? ReplaceVacio : representantelegal.TAX_ID.Trim();

                if (representantelegal.TIS_N != null && representantelegal.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, representantelegal.TIS_N.ToString(), representantelegal.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoRep = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito;
                        provinciaRep = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia;
                        departamentoRep = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento;
                    }
                }
            }

            if (datosLicencia != null)
            {
                //Rum = "080101-0716";
                Razonsocial = datosLicencia.RazonSocial == string.Empty ? ReplaceVacio : datosLicencia.RazonSocial;
                Ruc = datosLicencia.NumRazonSocial == null ? ReplaceVacio : datosLicencia.NumRazonSocial;
                Phone = "";
                Fx = "";
            }

            if (ParametroValue != null)
            {
                Rum = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalGiro = establecimiento.TIPO == null ? ReplaceVacio : establecimiento.TIPO.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito;
                        provinciaLoc = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia;
                        departamentoLoc = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento;
                    }
                }
            }

            if (direccionCobranza != null)
            {
                if (direccionCobranza.Tis_n_DirCobranza != null && direccionCobranza.Geo_id_DirCobranza != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, direccionCobranza.Tis_n_DirCobranza.ToString(), direccionCobranza.Geo_id_DirCobranza.ToString());

                    if (ubigeo != null)
                    {
                        departamentoCob = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento;
                        provinciaCob = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia;
                        distritoCob = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito;
                    }
                }
            }

            if (valorunidadMusical != null)
            {
                if (valorunidadMusical.Mes != null)
                    Mes = valorunidadMusical.Mes.Length == 1 ? ('0' + valorunidadMusical.Mes) : valorunidadMusical.Mes;
                if (valorunidadMusical.Anio != null)
                    Anio = valorunidadMusical.Anio;
                if (valorunidadMusical.Valor != 0)
                {
                    Valor = valorunidadMusical.Valor.ToString();
                    ValorMusica = valorunidadMusical.Valor;
                }
            }

            if (modalidadIncidencia != null && modalidadIncidencia.Count > 0 && modalidadIncidencia.Count > 0)
            {
                Indispensable = modalidadIncidencia[0].MOD_IDESC == null ? ReplaceVacio : modalidadIncidencia[0].MOD_IDESC;
                Necesaria = modalidadIncidencia[1].MOD_IDESC == null ? ReplaceVacio : modalidadIncidencia[1].MOD_IDESC;
                Secundaria = modalidadIncidencia[2].MOD_IDESC == null ? ReplaceVacio : modalidadIncidencia[2].MOD_IDESC;
                IndispensableVal = modalidadIncidencia[0].MOD_PRC.ToString();
                NecesariaVal = modalidadIncidencia[1].MOD_PRC.ToString();
                SecundariaVal = modalidadIncidencia[2].MOD_PRC.ToString();

                if (!string.IsNullOrEmpty(IndispensableVal))
                    valIsp = decimal.Parse(IndispensableVal);
                if (!string.IsNullOrEmpty(NecesariaVal))
                    valCs = decimal.Parse(NecesariaVal);
                if (!string.IsNullOrEmpty(SecundariaVal))
                    valSc = decimal.Parse(SecundariaVal);
                vSuma = (valIsp + valCs + valSc);
            }

            keyValues.Add("Version", Version);
            keyValues.Add("Date", Fecha);
            keyValues.Add("Hora", Time);
            keyValues.Add("Rum", Rum);
            keyValues.Add("Razonsocial", Razonsocial);
            keyValues.Add("Ruc", NumRazonSocial);
            keyValues.Add("Phone", Phone);
            keyValues.Add("Fx", Fx);
            keyValues.Add("Rpre", Representante);
            keyValues.Add("Dxnix", RepresentanteDoc);

            keyValues.Add("GiroLocal", LocalGiro);
            keyValues.Add("NombreLocal", LocalName);
            keyValues.Add("DepartamentoLocal", departamentoLoc);
            keyValues.Add("ProvinciaLocal", provinciaLoc);
            keyValues.Add("DistritoLocal", distritoLoc);
            keyValues.Add("DepartamentoRep", departamentoRep);
            keyValues.Add("ProvinciaRep", provinciaRep);
            keyValues.Add("DistritoRep", distritoRep);
            keyValues.Add("DepartamentoCob", departamentoCob);
            keyValues.Add("ProvinciaCob", provinciaCob);
            keyValues.Add("DistritoCob", distritoCob);

            keyValues.Add("IE", Indispensable);
            keyValues.Add("NRA", Necesaria);
            keyValues.Add("SD", Secundaria);
            keyValues.Add("IPE", IndispensableVal);
            keyValues.Add("NRV", NecesariaVal);
            keyValues.Add("SC", SecundariaVal);
            keyValues.Add("NIM", vSuma.ToString());

            //Valor Unidad Musical
            keyValues.Add("Xew", Mes);
            keyValues.Add("Anio", Anio);
            keyValues.Add("UIM", Valor.ToString());

            //aforo del local            
            keyValues.Add("XAX", "40");
            keyValues.Add("XMX", "00");
            keyValues.Add("XHX", "00");
            keyValues.Add("XTX", "40");
            keyValues.Add("RRR", "60");
            keyValues.Add("YY", "240");
            AforoLocal = 24;
            //horas música al mes
            horasDias = 6;
            DiasMes = 25;
            HorasMusicaMes = horasDias * DiasMes;
            keyValues.Add("HD", horasDias.ToString());
            keyValues.Add("DM", DiasMes.ToString());
            keyValues.Add("@XX", HorasMusicaMes.ToString());
            ////categoria del local
            CategoriaLocal = 0.24;
            keyValues.Add("@CL", CategoriaLocal.ToString());
            ////medio de ejecucion
            MedioEjecucion = 0.12;
            keyValues.Add("@QQ", MedioEjecucion.ToString());
            ////Tarifa mensual a pagar
            Tarifa = ValorMusica + IncidenciaObra + AforoLocal + HorasMusicaMes + CategoriaLocal + MedioEjecucion;
            keyValues.Add("@WW", Tarifa.ToString());

            SearchAndReplace(destinationFile, keyValues);
            //FindandReplace(destinationFile, keyValues);
            string RutaPDF = rutaPDF;
            string RutaWord = nombreFileCopy;
            Convert(RutaWord, RutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteAvisoPrejudicial(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            var datosLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);

            string IdLic = ReplaceVacio;
            string RazonSocial = ReplaceVacio;
            string NombreLocal = ReplaceVacio;
            string FechasInf = ReplaceVacio;
            string FechaRei = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Fecha = string.Empty;
            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            string fechaCartaInformativa = new BLReporte().FechaCartaInformativa(GlobalVars.Global.OWNER, IdLicencia);
            string fechaCartaReiterativa = new BLReporte().FechaCartaReiterativa(GlobalVars.Global.OWNER, IdLicencia);

            if (!string.IsNullOrEmpty(fechaCartaInformativa))
            {
                FechasInf = fechaCartaInformativa;
            }

            if (!string.IsNullOrEmpty(fechaCartaReiterativa))
            {
                FechaRei = fechaCartaReiterativa;
            }

            string Fechas = FechasInf + " - " + FechaRei;


            if (datosLicencia != null)
            {
                IdLic = datosLicencia.IdLicencia == null ? ReplaceVacio : datosLicencia.IdLicencia.ToString();
                RazonSocial = datosLicencia.RazonSocial == string.Empty ? ReplaceVacio : datosLicencia.RazonSocial.Trim();
            }

            if (establecimiento != null)
            {
                NombreLocal = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
            }

            keyValues.Add("@Fecha", Fecha);
            keyValues.Add("Numero", IdLic);
            keyValues.Add("Repre", RazonSocial);
            keyValues.Add("Date", Fechas);
            keyValues.Add("@Local", NombreLocal);
            keyValues.Add("@", string.Empty);
            FindandReplace(destinationFile, keyValues);
            string RutaWord = destinationFile;
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteCartillaInformativaLeyDerechoAutor(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = new BLReporte().FechaActualShort();

            string Fecha = string.Empty;
            if (!string.IsNullOrEmpty(FechaCorta))
            {
                Fecha = FechaCorta;
            }
            else
            {
                Fecha = DateTime.Now.ToShortDateString();
            }

            keyValues.Add("Date", Fecha);
            FindandReplace(destinationFile, keyValues);
            string RutaWord = destinationFile;
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteDeclaracionJurada(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            double ValorMusica = 0;
            double horasDias = 0;
            double DiasMes = 0;
            double HorasMusicaMes = 0;
            double CategoriaLocal = 0;
            double MedioEjecucion = 0;
            double Tarifa = 0;
            double AforoLocal = 0;
            double IncidenciaObra = 0;
            string UsuName = ReplaceVacio;
            string UsuRuc = ReplaceVacio;
            string AngenteRecaudoName = ReplaceVacio;
            string distritoCob = ReplaceVacio;
            string provinciaCob = ReplaceVacio;
            string departamentoCob = ReplaceVacio;
            string direccionCob = ReplaceVacio;
            string distritoRep = ReplaceVacio;
            string provinciaRep = ReplaceVacio;
            string departamentoRep = ReplaceVacio;
            string distritoLoc = ReplaceVacio;
            string provinciaLoc = ReplaceVacio;
            string departamentoLoc = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalGiro = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string codigoRum = ReplaceVacio;
            string Propietario = ReplaceVacio;
            string Ruc = ReplaceVacio;
            string Telefono = ReplaceVacio;
            string Fax = ReplaceVacio;
            string Representante = ReplaceVacio;
            string RepresentanteDir = ReplaceVacio;
            string Mes = ReplaceVacio;
            string Anio = ReplaceVacio;
            string Valor = ReplaceVacio;

            string Indispensable = ReplaceVacio;
            string Necesaria = ReplaceVacio;
            string Secundaria = ReplaceVacio;
            string IndispensableVal = ReplaceVacio;
            string NecesariaVal = ReplaceVacio;
            string SecundariaVal = ReplaceVacio;

            decimal valIsp = 0;
            decimal valCs = 0;
            decimal valSc = 0;
            decimal vSuma = 0;


            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            List<BEModalidadIncidencia> modalidadIncidencia = new List<BEModalidadIncidencia>();

            var datosLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia);
            var valorunidadMusical = new BLReporte().ValorUnidadMusical(GlobalVars.Global.OWNER);
            modalidadIncidencia = new BLReporte().ListarTipo(GlobalVars.Global.OWNER);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var agenterecaudo = new BLReporte().ObtenerAgenteRecaudo(GlobalVars.Global.OWNER, IdLicencia);
            var direccionCobranza = new BLReporte().ObtenerDatosDireccionCobranza(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var representantelegal = new BLReporte().ObtenerRepresentanteLegal(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdRum);

            if (datosLicencia != null)
            {
                //codigoRum = "080101-0716";
                Propietario = datosLicencia.RazonSocial == string.Empty ? ReplaceVacio : datosLicencia.RazonSocial.Trim();
                Ruc = datosLicencia.NumRazonSocial == null ? ReplaceVacio : datosLicencia.NumRazonSocial.Trim();
                Telefono = "2460205";
                Fax = "234-002";
            }

            if (ParametroValue != null)
            {
                codigoRum = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalGiro = establecimiento.TIPO == null ? ReplaceVacio : establecimiento.TIPO.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if ((establecimiento.TIS_N != null && establecimiento.TIS_N != 0) && (establecimiento.GEO_ID != null && establecimiento.GEO_ID != 0))
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLoc = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLoc = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (representantelegal != null)
            {
                Representante = representantelegal.BPS_NAME == null ? ReplaceVacio : representantelegal.BPS_NAME.Trim();
                RepresentanteDir = representantelegal.ADDRESS == null ? ReplaceVacio : representantelegal.ADDRESS.Trim();

                if ((representantelegal.TIS_N != null && establecimiento.TIS_N != 0) && (representantelegal.GEO_ID != null && establecimiento.GEO_ID != 0))
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, representantelegal.TIS_N.ToString(), representantelegal.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoRep = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaRep = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoRep = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (direccionCobranza != null)
            {
                direccionCob = direccionCobranza.DireccionCobranza == null ? ReplaceVacio : direccionCobranza.DireccionCobranza.Trim();

                if (direccionCobranza.Tis_n_DirCobranza != null && direccionCobranza.Geo_id_DirCobranza != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, direccionCobranza.Tis_n_DirCobranza.ToString(), direccionCobranza.Geo_id_DirCobranza.ToString());

                    if (ubigeo != null)
                    {
                        departamentoCob = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                        provinciaCob = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        distritoCob = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (valorunidadMusical != null)
            {
                if (valorunidadMusical.Mes != null)
                    Mes = valorunidadMusical.Mes.Length == 1 ? ('0' + valorunidadMusical.Mes) : valorunidadMusical.Mes;
                if (valorunidadMusical.Anio != null)
                    Anio = valorunidadMusical.Anio;
                if (valorunidadMusical.Valor != 0)
                {
                    Valor = valorunidadMusical.Valor.ToString();
                    ValorMusica = valorunidadMusical.Valor;
                }
            }

            if (usuarioderecho != null)
            {
                if (usuarioderecho.BPS_NAME != null)
                    UsuName = usuarioderecho.BPS_NAME;
                if (usuarioderecho.TAX_ID != null)
                    UsuRuc = usuarioderecho.TAX_ID;
            }

            if (agenterecaudo != null)
            {
                if (agenterecaudo.BPS_NAME != null)
                    AngenteRecaudoName = agenterecaudo.BPS_NAME;
            }

            if (modalidadIncidencia != null && modalidadIncidencia.Count > 0 && modalidadIncidencia.Count > 0)
            {
                Indispensable = modalidadIncidencia[0].MOD_IDESC == null ? ReplaceVacio : modalidadIncidencia[0].MOD_IDESC;
                Necesaria = modalidadIncidencia[1].MOD_IDESC == null ? ReplaceVacio : modalidadIncidencia[1].MOD_IDESC;
                Secundaria = modalidadIncidencia[2].MOD_IDESC == null ? ReplaceVacio : modalidadIncidencia[2].MOD_IDESC;
                IndispensableVal = modalidadIncidencia[0].MOD_PRC.ToString();
                NecesariaVal = modalidadIncidencia[1].MOD_PRC.ToString();
                SecundariaVal = modalidadIncidencia[2].MOD_PRC.ToString();

                if (!string.IsNullOrEmpty(IndispensableVal))
                    valIsp = decimal.Parse(IndispensableVal);
                if (!string.IsNullOrEmpty(NecesariaVal))
                    valCs = decimal.Parse(NecesariaVal);
                if (!string.IsNullOrEmpty(SecundariaVal))
                    valSc = decimal.Parse(SecundariaVal);
                vSuma = (valIsp + valCs + valSc);
            }


            keyValues.Add("@IE", Indispensable);
            keyValues.Add("@NRA", Necesaria);
            keyValues.Add("@SD", Secundaria);
            keyValues.Add("@IPE", IndispensableVal);
            keyValues.Add("@NRV", NecesariaVal);
            keyValues.Add("@SC", SecundariaVal);
            keyValues.Add("@NIM", vSuma.ToString());


            //aforo del local            
            keyValues.Add("@A", "40");
            keyValues.Add("@M", "00");
            keyValues.Add("@H", "00");
            keyValues.Add("@T", "40");
            keyValues.Add("@YY", "240");
            AforoLocal = 240;

            //horas música al mes
            horasDias = 6;
            DiasMes = 25;
            HorasMusicaMes = horasDias * DiasMes;
            keyValues.Add("@HD", horasDias.ToString());
            keyValues.Add("@DM", DiasMes.ToString());
            keyValues.Add("@XX", HorasMusicaMes.ToString());

            //categoria del local
            CategoriaLocal = 0.24;
            keyValues.Add("@CL", CategoriaLocal.ToString());

            //medio de ejecucion
            MedioEjecucion = 0.12;
            keyValues.Add("@QQ", MedioEjecucion.ToString());

            //Tarifa mensual a pagar
            Tarifa = ValorMusica + IncidenciaObra + AforoLocal + HorasMusicaMes + CategoriaLocal + MedioEjecucion;
            keyValues.Add("@WW", Tarifa.ToString());

            keyValues.Add("@CodigoRum", codigoRum);
            keyValues.Add("@Propietario", Propietario);
            keyValues.Add("@Ruc", Ruc);
            keyValues.Add("@Telefono", Telefono);
            keyValues.Add("@Fax", Fax);
            keyValues.Add("@Representante", Representante);
            keyValues.Add("@DireccionRep", RepresentanteDir);

            keyValues.Add("@DepartamentoCob", departamentoCob);
            keyValues.Add("@ProvinciaCob", provinciaCob);
            keyValues.Add("@DistritoCob", distritoCob);
            keyValues.Add("@DepartamentoLocal", departamentoLoc);
            keyValues.Add("@ProvinciaLocal", provinciaLoc);
            keyValues.Add("@DistritoLocal", distritoLoc);
            keyValues.Add("@DepartamentoRep", departamentoRep);
            keyValues.Add("@ProvinciaRep", provinciaRep);
            keyValues.Add("@DistritoRep", distritoRep);
            keyValues.Add("@NombreUsuario", UsuName);
            keyValues.Add("@DocumentoUsuario", UsuRuc);
            keyValues.Add("@GiroLocal", LocalGiro);
            keyValues.Add("@NombreLocal", LocalName);
            keyValues.Add("@DireccionLocal", LocalDir);
            keyValues.Add("@DireccionCob", direccionCob);
            keyValues.Add("@RA", AngenteRecaudoName);
            keyValues.Add("@Cargo", ReplaceVacio);

            keyValues.Add("@Mes", Mes);
            keyValues.Add("@Anio", Anio);
            keyValues.Add("@UM", Valor);

            FindandReplace(destinationFile, keyValues);
            string RutaWord = destinationFile;
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteCartaSolidarioResponsableLocal(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string DistritoOficina = ReplaceVacio;
            string FechaActual = ReplaceVacio;
            string ResponsableSolidario = ReplaceVacio;
            string DirecResponsableSolidario = ReplaceVacio;
            string DistritoResponsableSolidario = ReplaceVacio;
            string Evento = ReplaceVacio;
            string FechaEvento = ReplaceVacio;
            string Local = ReplaceVacio;
            string Artistas = ReplaceVacio;
            string DireccionOficina = ReplaceVacio;
            string TelefonoOficina = ReplaceVacio;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = new BLReporte().FechaActualShort();

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                FechaActual = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                FechaActual = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var datosLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia);
            var artistas = new BLReporte().ObtenerArtistas(GlobalVars.Global.OWNER, IdLicencia);
            var representantelegal = new BLReporte().ObtenerRepresentanteLegal(GlobalVars.Global.OWNER, IdLicencia);

            if (oficina != null)
            {
                DireccionOficina = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();
                TelefonoOficina = ReplaceVacio;

                if ((oficina.TIS_N != null && oficina.TIS_N != 0) && (oficina.GEO_ID != null && oficina.GEO_ID != 0))
                {
                    var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

                    if (ubigeo != null)
                    {
                        DistritoOficina = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                ResponsableSolidario = establecimiento.BPS_NAME == null ? ReplaceVacio : establecimiento.BPS_NAME.Trim();
                DirecResponsableSolidario = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();
                Local = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

                if ((establecimiento.TIS_N != null && establecimiento.TIS_N != 0) && (establecimiento.GEO_ID != null && establecimiento.GEO_ID != 0))
                {
                    var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        DistritoResponsableSolidario = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (datosLicencia != null)
            {
                Evento = datosLicencia.NombreLicencia == null ? ReplaceVacio : datosLicencia.NombreLicencia.Trim();
            }

            if (artistas != null && artistas.Count > 0)
            {
                Artistas = String.Join(", ", artistas);
            }

            keyValues.Add("@DistritoOficina", DistritoOficina);
            keyValues.Add("@FechaActual", FechaActual);
            keyValues.Add("@ResponsableSolidario", ResponsableSolidario);
            keyValues.Add("@DireccionResponsableSolidario", DirecResponsableSolidario);
            keyValues.Add("@DistritoResponsableSolidario", DistritoResponsableSolidario);
            keyValues.Add("@Evento", Evento);
            keyValues.Add("@FechaEvento", FechaEvento);
            keyValues.Add("@Local", Local);
            keyValues.Add("@Artistas", Artistas);
            keyValues.Add("@DireccionOficina", DireccionOficina);
            keyValues.Add("@TelefonoOficina", TelefonoOficina);

            FindandReplace(destinationFile, keyValues);
            string RutaWord = destinationFile;
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteCartaOrganizacionRequemientoAutorizacion(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string FechaActual = ReplaceVacio;
            string Organizador = ReplaceVacio;
            string DireccionOrganizador = ReplaceVacio;
            string DistritoOrganizador = ReplaceVacio;
            string Evento = ReplaceVacio;
            string FechaEvento = ReplaceVacio;
            string Local = ReplaceVacio;
            string Artistas = ReplaceVacio;
            string DireccionOficina = ReplaceVacio;
            string TelefonoOficina = ReplaceVacio;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = new BLReporte().FechaActualShort();
            DateTime datex = DateTime.Parse(FechaCorta);
            var FechaLong = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            FechaActual = FechaLong;

            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var datosLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia);
            var artistas = new BLReporte().ObtenerArtistas(GlobalVars.Global.OWNER, IdLicencia);
            var usarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);

            if (usarioderecho != null)
            {
                Organizador = usarioderecho.BPS_NAME == null ? ReplaceVacio : usarioderecho.BPS_NAME.Trim();
                DireccionOrganizador = usarioderecho.ADDRESS == null ? ReplaceVacio : usarioderecho.ADDRESS.Trim();

                if ((usarioderecho.TIS_N != null && usarioderecho.TIS_N != 0) && (usarioderecho.GEO_ID != null && usarioderecho.GEO_ID != 0))
                {
                    var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usarioderecho.TIS_N.ToString(), usarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        DistritoOrganizador = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (datosLicencia != null)
            {
                Evento = datosLicencia.NombreLicencia == null ? ReplaceVacio : datosLicencia.NombreLicencia.Trim();
            }

            if (establecimiento != null)
            {
                Local = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Replace("/", " ").Trim();
            }

            if (artistas != null && artistas.Count > 0)
            {
                Artistas = String.Join(", ", artistas);
            }

            if (oficina != null)
            {
                DireccionOficina = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();
                TelefonoOficina = ReplaceVacio;
            }

            keyValues.Add("@FechaActual", FechaActual);
            keyValues.Add("@Organizador", Organizador);
            keyValues.Add("@DireccionOrganizador", DireccionOrganizador);
            keyValues.Add("@DistritoOrganizador", DistritoOrganizador);
            keyValues.Add("@Evento", Evento);
            keyValues.Add("@FechaEvento", FechaEvento);
            keyValues.Add("@Local", Local);
            keyValues.Add("@Artistas", Artistas);
            keyValues.Add("@DireccionOficina", DireccionOficina);
            keyValues.Add("@TelefonoOficina", TelefonoOficina);

            FindandReplace(destinationFile, keyValues);
            string RutaWord = destinationFile;
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteCartaVerficacionUsoInautorizadoObrasMusicales(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string FechaActual = ReplaceVacio;
            string NroCarta = ReplaceVacio;
            string Usuario = ReplaceVacio;
            string FechaEvento = ReplaceVacio;
            string DireccionLocal = ReplaceVacio;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = new BLReporte().FechaActualShort();
            DateTime datex = DateTime.Parse(FechaCorta);
            var FechaLong = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            FechaActual = FechaLong;

            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var datosLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia);
            var usarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);

            if (usarioderecho != null)
            {
                Usuario = usarioderecho.BPS_NAME == null ? ReplaceVacio : usarioderecho.BPS_NAME.Trim();
            }

            if (establecimiento != null)
            {
                DireccionLocal = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();
            }

            if (datosLicencia != null)
            {
                NroCarta = datosLicencia.IdLicencia == null ? ReplaceVacio : datosLicencia.IdLicencia.ToString().Trim();
            }

            keyValues.Add("@FechaActual", FechaActual);
            keyValues.Add("@NroCarta", NroCarta);
            keyValues.Add("@Usuario", Usuario);
            keyValues.Add("@FechaEvento", FechaEvento);
            keyValues.Add("@DireccionLocal", DireccionLocal);

            FindandReplace(destinationFile, keyValues);
            string RutaWord = destinationFile;
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteCartaResponsableSolidarioRegularizarAutorizacion(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string DistritoOficina = ReplaceVacio;
            string FechaActual = ReplaceVacio;
            string ResponsableSolidario = ReplaceVacio;
            string DireccionResponsableSolidario = ReplaceVacio;
            string DistritoResponsableSolidario = ReplaceVacio;
            string RepresentanteLegal = ReplaceVacio;
            string FechaCartaResponsableSolidario = ReplaceVacio;
            string FechaCartaVerificaciónUso = ReplaceVacio;
            string Evento = ReplaceVacio;
            string FechaEvento = ReplaceVacio;
            string Artistas = ReplaceVacio;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = new BLReporte().FechaActualShort();
            DateTime datex = DateTime.Parse(FechaCorta);
            var FechaLong = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            FechaActual = FechaLong;

            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var datosLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia);
            var artistas = new BLReporte().ObtenerArtistas(GlobalVars.Global.OWNER, IdLicencia);
            var representantelegal = new BLReporte().ObtenerRepresentanteLegal(GlobalVars.Global.OWNER, IdLicencia);
            var fechaRequerimientoAutorizacion = new BLReporte().FechaCartaRequerimientoAutorizacion(GlobalVars.Global.OWNER, IdLicencia);
            var fechaVerificacionUsoNoAutorizado = new BLReporte().FechaCartaVerificaionUsoAutorizacionObrasMusicales(GlobalVars.Global.OWNER, IdLicencia);

            if (representantelegal != null)
            {
                RepresentanteLegal = representantelegal.BPS_NAME == null ? ReplaceVacio : representantelegal.BPS_NAME.Trim();
            }

            if (oficina != null)
            {
                if ((oficina.TIS_N != null && oficina.TIS_N != 0) && (oficina.GEO_ID != null && oficina.GEO_ID != 0))
                {
                    var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

                    if (ubigeo != null)
                    {
                        DistritoOficina = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                ResponsableSolidario = establecimiento.BPS_NAME == null ? ReplaceVacio : establecimiento.BPS_NAME.Trim();
                DireccionResponsableSolidario = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if ((establecimiento.TIS_N != null && establecimiento.TIS_N != 0) && (establecimiento.GEO_ID != null && establecimiento.GEO_ID != 0))
                {
                    var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        DistritoResponsableSolidario = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (datosLicencia != null)
            {
                Evento = datosLicencia.NombreLicencia == string.Empty ? ReplaceVacio : datosLicencia.NombreLicencia.Trim();
            }

            if (artistas != null && artistas.Count > 0)
            {
                Artistas = String.Join(", ", artistas);
            }

            if (!string.IsNullOrEmpty(fechaRequerimientoAutorizacion))
            {
                FechaCartaResponsableSolidario = fechaRequerimientoAutorizacion == string.Empty ? ReplaceVacio : fechaRequerimientoAutorizacion;
            }

            if (!string.IsNullOrEmpty(fechaVerificacionUsoNoAutorizado))
            {
                FechaCartaVerificaciónUso = fechaVerificacionUsoNoAutorizado == string.Empty ? ReplaceVacio : fechaVerificacionUsoNoAutorizado;
            }

            keyValues.Add("@DistritoOficina", DistritoOficina);
            keyValues.Add("@FechaActual", FechaActual);
            keyValues.Add("@ResponsableSolidario", ResponsableSolidario);
            keyValues.Add("@DireccionResponsableSolidario", DireccionResponsableSolidario);
            keyValues.Add("@DistritoResponsableSolidario", DistritoResponsableSolidario);
            keyValues.Add("@RepresentanteLegal", RepresentanteLegal);
            keyValues.Add("@FechaCartaResponsableSolidario", FechaCartaResponsableSolidario);
            keyValues.Add("@FechaCartaVerificacionUso", FechaCartaVerificaciónUso);
            keyValues.Add("@Evento", Evento);
            keyValues.Add("@FechaEvento", FechaEvento);
            keyValues.Add("@Artistas", Artistas);

            FindandReplace(destinationFile, keyValues);
            string RutaWord = destinationFile;
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteActualizacionTarifaModelo(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = new BLReporte().FechaActualShort();
            string Fecha = string.Empty;
            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            keyValues.Add("@Fecha", Fecha);
            FindandReplace(destinationFile, keyValues);
            string RutaWord = destinationFile;
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public void CrearReporteDeclaracionJuradaCalculoRemuneracionDerechosAutor(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Razonsocial = ReplaceVacio;
            //string Representante = string.Empty;
            string ContactoNombre = ReplaceVacio;
            string Ruc = ReplaceVacio;
            string NombreEmisora = ReplaceVacio;
            string DireccionEmisora = ReplaceVacio;
            string DistritoEmisora = ReplaceVacio;

            var datosLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia);
            //var representantelegal = new BLReporte().ObtenerRepresentanteLegal(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (datosLicencia != null)
            {
                Razonsocial = datosLicencia.RazonSocial == string.Empty ? ReplaceVacio : datosLicencia.RazonSocial.Trim();
                Ruc = datosLicencia.NumRazonSocial == string.Empty ? ReplaceVacio : datosLicencia.NumRazonSocial.Trim();
            }

            //if (representantelegal != null)
            //{
            //    Representante = representantelegal.BPS_NAME == null ? ReplaceVacio : representantelegal.BPS_NAME.Trim();
            //}

            if (establecimiento != null)
            {
                NombreEmisora = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                DireccionEmisora = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        DistritoEmisora = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
            }

            keyValues.Add("@RazonSocial", Razonsocial);
            keyValues.Add("@NombreRepresentante", ContactoNombre);
            keyValues.Add("@Ruc", Ruc);
            keyValues.Add("@NombreEmisora", NombreEmisora);
            keyValues.Add("@DireccionEmisora", DireccionEmisora);
            keyValues.Add("@DistritoEmisora", DistritoEmisora);
            FindandReplace(destinationFile, keyValues);
            string RutaWord = destinationFile;
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
        }

        public bool CrearReportePlanillaEjecicion(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idSerie, decimal idReport)
        {
            int[] val = new int[9];
            bool resultado = true;

            string DistritoOficina = ReplaceVacio;
            string DireccionOficina = ReplaceVacio;
            string DepartamentoOficina = ReplaceVacio;
            string ProvinciaOficina = ReplaceVacio;
            string Local = ReplaceVacio;
            string Rum = ReplaceVacio;
            string NumeroBoleta = ReplaceVacio;
            string Frecuencia = ReplaceVacio;
            string AnioPeriodoFactura = ReplaceVacio;
            string MesPeriodoFactura = ReplaceVacio;
            string FechaPeriodo = ReplaceVacio;
            string FechaFormato = ReplaceVacio;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = new BLReporte().FechaActualShort();

            // var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdRum);
            var ParametroValueHz = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var planilla = new BLReporte().ObtenerDatosPlanilla(GlobalVars.Global.OWNER, IdLicencia, idReport);

            //comprobando correlativo
            var correlativo = new BLREC_NUMBERING().ObtenerCorrelativoXtipo(GlobalVars.Global.OWNER, "PL").NMR_NOW;

            if (ParametroValue != null)
            {
                Rum = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            if (planilla != null)
            {
                //Rum = planilla.RUM == null ? ReplaceVacio : planilla.RUM;
                NumeroBoleta = planilla.REPORT_NUMBER == null ? ReplaceVacio : planilla.REPORT_NUMBER.ToString();//correlativo.ToString() == null ? ReplaceVacio : correlativo.ToString();

                //var dato = establecimiento.SUBTIPO;

                //if (dato != null)
                //{
                //    var AM = dato.Contains("AM");
                //    var FM = dato.Contains("FM");
                //    if (AM)
                //        Frecuencia = "AM";
                //    else if (FM)
                //        Frecuencia = "FM";
                //    else
                //        Frecuencia = ReplaceVacio;
                //}
                //else
                //    Frecuencia = ReplaceVacio;
                if (ParametroValueHz != null && ParametroValueHz.PAR_VALUE != null) Frecuencia = ParametroValueHz.PAR_VALUE;


                MesPeriodoFactura = planilla.LIC_MONTH == null ? ReplaceVacio : planilla.LIC_MONTH.ToString();
                AnioPeriodoFactura = planilla.LIC_YEAR == null ? ReplaceVacio : planilla.LIC_YEAR.ToString();

                switch (MesPeriodoFactura)
                {
                    case "1":
                        FechaPeriodo = "Enero";
                        break;
                    case "2":
                        FechaPeriodo = "Febrero";
                        break;
                    case "3":
                        FechaPeriodo = "Marzo";
                        break;
                    case "4":
                        FechaPeriodo = "Abril";
                        break;
                    case "5":
                        FechaPeriodo = "Mayo";
                        break;
                    case "6":
                        FechaPeriodo = "Junio";
                        break;
                    case "7":
                        FechaPeriodo = "Julio";
                        break;
                    case "8":
                        FechaPeriodo = "Agosto";
                        break;
                    case "9":
                        FechaPeriodo = "Setiembre";
                        break;
                    case "10":
                        FechaPeriodo = "Octubre";
                        break;
                    case "11":
                        FechaPeriodo = "Noviembre";
                        break;
                    case "12":
                        FechaPeriodo = "Diciembre";
                        break;
                    default:
                        break;
                }

                FechaFormato = FechaPeriodo + " " + AnioPeriodoFactura;
            }

            string Fecha = string.Empty;
            if (!string.IsNullOrEmpty(FechaCorta))
                Fecha = FechaCorta;
            else
                Fecha = DateTime.Now.ToShortDateString();



            //if (oficina != null)
            //{
            //    //DireccionOficina = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();



            //    if ((oficina.TIS_N != null && oficina.TIS_N != 0) && (oficina.GEO_ID != null && oficina.GEO_ID != 0))
            //    {
            //        var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

            //        if (ubigeo != null)
            //        {
            //            ProvinciaOficina = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
            //            DistritoOficina = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
            //            DepartamentoOficina = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
            //        }
            //    }
            //}

            if (establecimiento != null)
            {
                DireccionOficina = establecimiento.ADDRESS; //nuevo
                Local = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

                var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    ProvinciaOficina = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    DistritoOficina = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    DepartamentoOficina = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = NumeroBoleta == ReplaceVacio ? 0 : 1;
            val[1] = Local == ReplaceVacio ? 0 : 1;
            val[2] = Rum == ReplaceVacio ? 0 : 1;
            val[3] = DireccionOficina == ReplaceVacio ? 0 : 1;
            val[4] = DepartamentoOficina == ReplaceVacio ? 0 : 1;
            val[5] = ProvinciaOficina == ReplaceVacio ? 0 : 1;
            val[6] = DistritoOficina == ReplaceVacio ? 0 : 1;
            //val[7] = FechaPeriodo == ReplaceVacio ? 0 : 1;
            val[7] = 1;
            // val[8] = Rum == ReplaceVacio ? 1 : 1;
            val[8] = Frecuencia == ReplaceVacio ? 1 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Número de boleta");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del local");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Rum");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección de la oficina");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Departamento de la oficina");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Provincia de la oficina");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Distrito de la oficina");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha Periodo Factura");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@NumeroBoleta", NumeroBoleta);
                keyValues.Add("@FechaGiro", Fecha);
                keyValues.Add("@Establecimiento", Local);
                keyValues.Add("@Frecuencia", Frecuencia);//falta
                keyValues.Add("@Rum", Rum);
                keyValues.Add("@Direccion", DireccionOficina);
                keyValues.Add("@Departamento", DepartamentoOficina);
                keyValues.Add("@Provincia", ProvinciaOficina);
                keyValues.Add("@Localidad", DistritoOficina);
                keyValues.Add("@Mes", FechaFormato);
                keyValues.Add("@FactPre", " ");
                keyValues.Add("@FecFactPre", " ");
                keyValues.Add("@FactLiq", " ");
                keyValues.Add("@FecFactLiq", " ");
                keyValues.Add("@Importe", " ");
                keyValues.Add("@Artistas", " ");
                FindandReplace(destinationFile, keyValues);

                #region PruebaGenerarTablaDinamica
                //int rows = 3;
                //int cols = 4;
                //object oMissing = System.Reflection.Missing.Value;          
                //object oEndOfDoc = "\\endofdoc";
                //Microsoft.Office.Interop.Word._Application objWord;
                //Microsoft.Office.Interop.Word._Document objDoc;
                //objWord = new Microsoft.Office.Interop.Word.Application();
                //objWord.Visible = true;
                //objDoc = objWord.Documents.Add(ref oMissing, ref oMissing,
                //    ref oMissing, ref oMissing);

                //int i = 0;
                //int j = 0;
                //Microsoft.Office.Interop.Word.Table objTable;
                //Microsoft.Office.Interop.Word.Range wrdRng = objDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                //objTable = objDoc.Tables.Add(wrdRng, rows, cols, ref oMissing, ref oMissing);
                //objTable.Range.ParagraphFormat.SpaceAfter = 7;



                //string strText;
                //for (i = 1; i <= rows; i++)
                //    for (j = 1; j <= cols; j++)
                //    {
                //        strText = "Row" + i + " Coulmn" + j;
                //        objTable.Cell(i, j).Range.Text = strText;
                //    }
                //objTable.Rows[1].Range.Font.Bold = 2;
                //objTable.Rows[1].Range.Font.Italic = 1;
                //objTable.Borders.Shadow = true;            
                #endregion

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);

                if (idSerie != null)
                {
                    //actualizando el correlativo de la planilla
                    var resultado1 = new BLREC_NUMBERING().ActualizarCorrelativoPlanilla(GlobalVars.Global.OWNER, correlativo, idReport, UsuarioActual);
                    //actualizar correlativo de planilla tabla REC_NUMBERING
                    var resultado2 = new BLRecibo().ActualizarSerie(GlobalVars.Global.OWNER, idSerie, "PL", UsuarioActual);
                    //actualizar estado de impresión tabla REC_LIC_AUT_ARTIST_REPORT
                    var resultado3 = new BLLicenciaReporte().ActualizarEstadoImpresion(GlobalVars.Global.OWNER, idReport);
                    //actualizar el numero de impresión 
                    var resultado4 = new BLLicenciaReporte().ActualizarNroImpresion(GlobalVars.Global.OWNER, idReport, UsuarioActual);

                }
            }

            return resultado;
        }

        //OMISOS RADIO WEB
        public bool CrearReporteCartaInicioRecuerdoOmiso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[9];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;

            string LocalName = ReplaceVacio;
            string DireccionEstablecimiento = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;

            string DireccionOficina = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string Parametro = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                DireccionEstablecimiento = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (oficina != null)
            {
                DireccionOficina = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = DireccionEstablecimiento == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = DireccionOficina == ReplaceVacio ? 0 : 1;
            val[8] = Parametro == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Dirección de local");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", DireccionEstablecimiento);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@DireccionOficina", DireccionOficina);
                keyValues.Add("@Parametro", Parametro);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearReporteNotificacion72HorasOmiso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[11];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;

            string LocalName = ReplaceVacio;
            string DireccionEstablecimiento = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;

            string FechaCorta = new BLReporte().FechaActualShort();
            string fechaCartaInicioRecuerdoOmiso = ReplaceVacio;

            string NombreAutoridadPrincipal = ReplaceVacio;
            string Parametro = ReplaceVacio;
            string TelefonoAutoridadPrincipal = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                DireccionEstablecimiento = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (AutoridadPrincipal != null)
            {
                NombreAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                TelefonoAutoridadPrincipal = AutoridadPrincipal.PHONE_NUMBER == null ? ReplaceVacio : AutoridadPrincipal.PHONE_NUMBER;
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }


            //fechas cartas 1 
            List<DateTime?> listaFechasCarta = new List<DateTime?>();
            var WRKF_SID = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, idTrace);
            if (WRKF_SID != 0)
            {
                var prerequisitoId = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, idTrace);

                if (prerequisitoId != 0 || prerequisitoId != null)
                {
                    WORKF_TRACES en = new WORKF_TRACES();
                    var item = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId, WRKF_SID);
                    en.WRKF_TID = item.WRKF_TID;
                    en.WRKF_ADATE = item.WRKF_ADATE;
                    listaFechasCarta.Add(en.WRKF_ADATE);
                }
            }

            if (listaFechasCarta.Count > 0)
            {
                List<string> lista = new List<string>();

                foreach (DateTime item in listaFechasCarta)
                {
                    lista.Add(item.ToString("dd 'de' MMMMMM 'de' yyyy"));
                }

                fechaCartaInicioRecuerdoOmiso = String.Join(" ,", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = DireccionEstablecimiento == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = fechaCartaInicioRecuerdoOmiso == ReplaceVacio ? 0 : 1;
            val[8] = NombreAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[9] = Parametro == ReplaceVacio ? 0 : 1;
            val[10] = TelefonoAutoridadPrincipal == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha primera carta recordatorio a omiso");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Nombre autoridad principal");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("Telefono autoridad principal");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", DireccionEstablecimiento);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@FCartaNumeroUno", fechaCartaInicioRecuerdoOmiso);
                keyValues.Add("@AutoridadPrincipal", NombreAutoridadPrincipal);
                keyValues.Add("@Parametros", Parametro);
                keyValues.Add("@Telefono", TelefonoAutoridadPrincipal);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearReporteNotificacion48HorasOmiso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;

            string LocalName = ReplaceVacio;
            string DireccionEstablecimiento = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;

            string FechaCorta = new BLReporte().FechaActualShort();
            string fechaCarta72HorasOmiso = ReplaceVacio;
            string Parametro = ReplaceVacio;
            string PeriodoDeudaFormato = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                DireccionEstablecimiento = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }

            //fechas cartas 72
            List<DateTime?> listaFechasCarta = new List<DateTime?>();
            var WRKF_SID = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, idTrace);
            if (WRKF_SID != 0)
            {
                var prerequisitoId = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, idTrace);

                if (prerequisitoId != 0 || prerequisitoId != null)
                {
                    WORKF_TRACES en = new WORKF_TRACES();
                    var item = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId, WRKF_SID);
                    en.WRKF_TID = item.WRKF_TID;
                    en.WRKF_ADATE = item.WRKF_ADATE;
                    listaFechasCarta.Add(en.WRKF_ADATE);
                }
            }

            if (listaFechasCarta.Count > 0)
            {
                List<string> lista = new List<string>();

                foreach (DateTime item in listaFechasCarta)
                {
                    lista.Add(item.ToString("dd 'de' MMMMMM 'de' yyyy"));
                }

                fechaCarta72HorasOmiso = String.Join(" ,", lista);
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = DireccionEstablecimiento == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = fechaCarta72HorasOmiso == ReplaceVacio ? 0 : 1;
            val[8] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[9] = Parametro == ReplaceVacio ? 0 : 1;
            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha primera carta 72 horas omiso");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Periodo de deuda");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", DireccionEstablecimiento);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@FCarta72horas", fechaCarta72HorasOmiso);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@Parametros", Parametro);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearReporteNotificacion24HorasOmiso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[9];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;

            string LocalName = ReplaceVacio;
            string DireccionEstablecimiento = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Parametro = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string PeriodoDeudaFormato = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                DireccionEstablecimiento = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = DireccionEstablecimiento == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = Parametro == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo de deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", DireccionEstablecimiento);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@Parametros", Parametro);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaNotarialOmiso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[9];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeo ubigeo = new BEUbigeo();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string DireccionEstablecimiento = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string FechasCarta = ReplaceVacio;
            string Parametro = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;


            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                DireccionEstablecimiento = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLUbigeo().ObtenerDescripcion(establecimiento.TIS_N, establecimiento.GEO_ID);
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.NOMBRE_UBIGEO;
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            //fechas cartas 72,48,24
            List<DateTime?> listaFechasCarta = new List<DateTime?>();
            var WRKF_SID = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, idTrace);
            if (WRKF_SID != 0)
            {
                var prerequisitoId24 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, idTrace);

                if (prerequisitoId24 != 0 || prerequisitoId24 != null)
                {
                    WORKF_TRACES en = new WORKF_TRACES();
                    var item = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId24, WRKF_SID);
                    en.WRKF_TID = item.WRKF_TID;
                    en.WRKF_ADATE = item.WRKF_ADATE;
                    listaFechasCarta.Add(en.WRKF_ADATE);

                    var WRKF_SID2 = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, en.WRKF_TID);

                    if (WRKF_SID2 != 0)
                    {
                        var prerequisitoId48 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, en.WRKF_TID);

                        if (prerequisitoId48 != 0 || prerequisitoId48 != null)
                        {
                            WORKF_TRACES en2 = new WORKF_TRACES();
                            var item2 = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId48, WRKF_SID2);
                            en2.WRKF_TID = item2.WRKF_TID;
                            en2.WRKF_ADATE = item2.WRKF_ADATE;
                            listaFechasCarta.Add(en2.WRKF_ADATE);

                            var WRKF_SID3 = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, en2.WRKF_TID);

                            if (WRKF_SID3 != 0)
                            {
                                var prerequisitoId72 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, en2.WRKF_TID);

                                if (prerequisitoId72 != 0 || prerequisitoId72 != null)
                                {
                                    WORKF_TRACES en3 = new WORKF_TRACES();
                                    var item3 = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId72, WRKF_SID2);
                                    en3.WRKF_TID = item3.WRKF_TID;
                                    en3.WRKF_ADATE = item3.WRKF_ADATE;
                                    listaFechasCarta.Add(en3.WRKF_ADATE);
                                }
                            }
                        }
                    }
                }
            }

            if (listaFechasCarta.Count > 0)
            {
                List<string> lista = new List<string>();

                foreach (DateTime item in listaFechasCarta)
                {
                    lista.Add(item.ToString("dd 'de' MMMMMM 'de' yyyy"));
                }

                FechasCarta = String.Join(" ,", lista);
            }

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = DireccionEstablecimiento == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = FechasCarta == ReplaceVacio ? 0 : 1;
            val[8] = Parametro == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fechas carta 72, 48 y 24");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", DireccionEstablecimiento);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@FechCart", FechasCarta);
                keyValues.Add("@Parametros", Parametro);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearContratoOmiso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[16];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string DocUsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string DireccionEstablecimiento = ReplaceVacio;
            string distritoLoc = ReplaceVacio;
            string provinciaLoc = ReplaceVacio;
            string departamentoLoc = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Parametro = ReplaceVacio;
            string NombreAutoridadPrincipal = ReplaceVacio;
            string TelefonoAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string DirUsuarioDer = ReplaceVacio;
            string DisUsuarioDer = ReplaceVacio;
            string ProvUsuarioDer = ReplaceVacio;
            string DepUsuarioDer = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string DocContancto = ReplaceVacio;
            DateTime FechaLicencia;
            string FechaLicenciaFormanto = ReplaceVacio;
            string DireccionOficina = ReplaceVacio;
            string RucApdayc = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            FechaLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia).FechaCreacionLicencia;
            DireccionOficina = GlobalVars.Global.DireccionApdayc;
            RucApdayc = GlobalVars.Global.RucApdayc;

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
                DocContancto = contacto.TAX_ID == null ? ReplaceVacio : contacto.TAX_ID;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                DocUsuarioDerecho = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                DirUsuarioDer = usuarioderecho.ADDRESS == null ? ReplaceVacio : usuarioderecho.ADDRESS.Trim();

                if ((usuarioderecho.TIS_N != null && usuarioderecho.TIS_N != 0) && (usuarioderecho.GEO_ID != null && usuarioderecho.GEO_ID != 0))
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        DisUsuarioDer = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        ProvUsuarioDer = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        DepUsuarioDer = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLoc = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLoc = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }

                DireccionEstablecimiento = LocalDir + " - " + distritoLoc + " " + departamentoLoc;
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            if (AutoridadPrincipal != null)
            {
                NombreAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                TelefonoAutoridadPrincipal = AutoridadPrincipal.PHONE_NUMBER == null ? ReplaceVacio : AutoridadPrincipal.PHONE_NUMBER;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            if (FechaLicencia != null)
            {
                FechaLicenciaFormanto = FechaLicencia.ToString("dd 'del mes de' MMMMMM 'de' yyyy");
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = RucApdayc == ReplaceVacio ? 0 : 1;
            val[1] = DireccionOficina == ReplaceVacio ? 0 : 1;
            val[2] = NombreAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[3] = DocAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[4] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[5] = DocUsuarioDerecho == ReplaceVacio ? 0 : 1;

            val[6] = DirUsuarioDer == ReplaceVacio ? 0 : 1;
            val[7] = DisUsuarioDer == ReplaceVacio ? 0 : 1;
            val[8] = ProvUsuarioDer == ReplaceVacio ? 0 : 1;
            val[9] = DepUsuarioDer == ReplaceVacio ? 0 : 1;

            val[10] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[11] = DocContancto == ReplaceVacio ? 0 : 1;
            val[12] = CargoCT == ReplaceVacio ? 0 : 1;
            val[13] = LocalName == ReplaceVacio ? 0 : 1;
            val[14] = Parametro == ReplaceVacio ? 0 : 1;
            val[15] = FechaLicenciaFormanto == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Ruc oficina");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Dirección oficina");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Autoridad principal");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Documento autoridad principal");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Documento usuario de derecho");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Dirección usuario de derecho");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Distrito usuario de derecho");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Provincia usuario de derecho");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Departamento usuario de derecho");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("Nombre contacto");
                                break;
                            case 11:
                                GlobalVars.Global.ListMessageReport.Add("Documento de contacto");
                                break;
                            case 12:
                                GlobalVars.Global.ListMessageReport.Add("Cargo de contacto");
                                break;
                            case 13:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 14:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro URL web");
                                break;
                            case 15:
                                GlobalVars.Global.ListMessageReport.Add("Fecha de conformidad de contrato");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@RucApdayc", RucApdayc);
                keyValues.Add("@DirOficina", DireccionOficina);
                keyValues.Add("@AutoriPrincipal", NombreAutoridadPrincipal);
                keyValues.Add("@DocAutoriPrincipal", DocAutoridadPrincipal);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@DocUsuarioDerecho", DocUsuarioDerecho);

                keyValues.Add("@DirUsuDerecho", DirUsuarioDer);
                keyValues.Add("@DisUsuDerecho ", DisUsuarioDer);
                keyValues.Add("@ProvUsuDerecho", ProvUsuarioDer);
                keyValues.Add("@DepUsuDerecho", DepUsuarioDer);



                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@DocCont", DocContancto);
                keyValues.Add("@CARGOCT", CargoCT);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@Parametros", Parametro);
                keyValues.Add("@FechLicencia", FechaLicenciaFormanto);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        //MOROSOS RADIO WEB
        public bool CrearReporteNotificacion72HorasMoroso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[11];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Parametro = ReplaceVacio;
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = Parametro == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;
            val[10] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre de contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo de contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total Deuda");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("Total Deuda");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@Parametros", Parametro);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearReporteNotificacion48HorasMoroso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[9];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Parametro = ReplaceVacio;
            string fechaCarta72HorasMoroso = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            //fechas cartas 72
            List<DateTime?> listaFechasCarta = new List<DateTime?>();
            var WRKF_SID = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, idTrace);
            if (WRKF_SID != 0)
            {
                var prerequisitoId = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, idTrace);

                if (prerequisitoId != 0 || prerequisitoId != null)
                {
                    WORKF_TRACES en = new WORKF_TRACES();
                    var item = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId, WRKF_SID);
                    en.WRKF_TID = item.WRKF_TID;
                    en.WRKF_ADATE = item.WRKF_ADATE;
                    listaFechasCarta.Add(en.WRKF_ADATE);
                }
            }

            if (listaFechasCarta.Count > 0)
            {
                List<string> lista = new List<string>();

                foreach (DateTime item in listaFechasCarta)
                {
                    lista.Add(item.ToString("dd 'de' MMMMMM 'de' yyyy"));
                }

                fechaCarta72HorasMoroso = String.Join(" ,", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = fechaCarta72HorasMoroso == ReplaceVacio ? 0 : 1;
            val[8] = Parametro == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha primera carta 72 horas radio web");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@FechCart72", fechaCarta72HorasMoroso);
                keyValues.Add("@Parametros", Parametro);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearReporteNotificacion24HorasMoroso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[9];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Parametro = ReplaceVacio;
            string FechasCarta = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            //fechas cartas 72,48
            List<DateTime?> listaFechasCarta = new List<DateTime?>();
            var WRKF_SID2 = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, idTrace);
            if (WRKF_SID2 != 0)
            {
                var prerequisitoId48 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, idTrace);

                if (prerequisitoId48 != 0 || prerequisitoId48 != null)
                {
                    WORKF_TRACES en2 = new WORKF_TRACES();
                    var item2 = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId48, WRKF_SID2);
                    en2.WRKF_TID = item2.WRKF_TID;
                    en2.WRKF_ADATE = item2.WRKF_ADATE;
                    listaFechasCarta.Add(en2.WRKF_ADATE);

                    var WRKF_SID3 = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, en2.WRKF_TID);

                    if (WRKF_SID3 != 0)
                    {
                        var prerequisitoId72 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, en2.WRKF_TID);

                        if (prerequisitoId72 != 0 || prerequisitoId72 != null)
                        {
                            WORKF_TRACES en3 = new WORKF_TRACES();
                            var item3 = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId72, WRKF_SID2);
                            en3.WRKF_TID = item3.WRKF_TID;
                            en3.WRKF_ADATE = item3.WRKF_ADATE;
                            listaFechasCarta.Add(en3.WRKF_ADATE);
                        }
                    }
                }
            }

            if (listaFechasCarta.Count > 0)
            {
                List<string> lista = new List<string>();

                foreach (DateTime item in listaFechasCarta)
                {
                    lista.Add(item.ToString("dd 'de' MMMMMM 'de' yyyy"));
                }

                FechasCarta = String.Join(" ,", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = FechasCarta == ReplaceVacio ? 0 : 1;
            val[8] = Parametro == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha primera carta 72,48 horas radio web");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@FechCarta7248 ", FechasCarta);
                keyValues.Add("@Parametros", Parametro);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaNotarialMoroso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[8];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Parametro = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;


            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = Parametro == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechCN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@Parametros", Parametro);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }


        //OMISOS RADIO
        public bool CrearCartaOmisoRadio72Horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[8];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Parametro = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string DireccionOficina = ReplaceVacio;


            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            if (oficina != null)
            {
                DireccionOficina = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = DireccionOficina == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Dirección oficina");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@DirOfice", DireccionOficina);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaOmisoRadio48Horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[9];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Parametro = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string DireccionOficina = ReplaceVacio;
            string fechaCarta72HorasOmiso = ReplaceVacio;


            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            if (oficina != null)
            {
                DireccionOficina = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();
            }

            //fechas cartas 72
            List<DateTime?> listaFechasCarta = new List<DateTime?>();
            var WRKF_SID = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, idTrace);
            if (WRKF_SID != 0)
            {
                var prerequisitoId = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, idTrace);

                if (prerequisitoId != 0 || prerequisitoId != null)
                {
                    WORKF_TRACES en = new WORKF_TRACES();
                    var item = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId, WRKF_SID);
                    en.WRKF_TID = item.WRKF_TID;
                    en.WRKF_ADATE = item.WRKF_ADATE;
                    listaFechasCarta.Add(en.WRKF_ADATE);
                }
            }

            if (listaFechasCarta.Count > 0)
            {
                List<string> lista = new List<string>();

                foreach (DateTime item in listaFechasCarta)
                {
                    lista.Add(item.ToString("dd 'de' MMMMMM 'de' yyyy"));
                }

                fechaCarta72HorasOmiso = String.Join(" ,", lista);
            }



            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = fechaCarta72HorasOmiso == ReplaceVacio ? 0 : 1;
            val[8] = DireccionOficina == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha carta 72 horas radio");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Dirección oficina");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@FCarta72horas", fechaCarta72HorasOmiso);
                keyValues.Add("@DirOfice", DireccionOficina);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaOmisoRadio24Horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[8];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Parametro = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string fechaCarta7248HorasOmiso = ReplaceVacio;


            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    //ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            //fechas cartas 72,48
            List<DateTime?> listaFechasCarta = new List<DateTime?>();
            var WRKF_SID2 = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, idTrace);
            if (WRKF_SID2 != 0)
            {
                var prerequisitoId48 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, idTrace);

                if (prerequisitoId48 != 0 || prerequisitoId48 != null)
                {
                    WORKF_TRACES en2 = new WORKF_TRACES();
                    var item2 = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId48, WRKF_SID2);
                    en2.WRKF_TID = item2.WRKF_TID;
                    en2.WRKF_ADATE = item2.WRKF_ADATE;
                    listaFechasCarta.Add(en2.WRKF_ADATE);

                    var WRKF_SID3 = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, en2.WRKF_TID);

                    if (WRKF_SID3 != 0)
                    {
                        var prerequisitoId72 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, en2.WRKF_TID);

                        if (prerequisitoId72 != 0 || prerequisitoId72 != null)
                        {
                            WORKF_TRACES en3 = new WORKF_TRACES();
                            var item3 = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId72, WRKF_SID2);
                            en3.WRKF_TID = item3.WRKF_TID;
                            en3.WRKF_ADATE = item3.WRKF_ADATE;
                            listaFechasCarta.Add(en3.WRKF_ADATE);
                        }
                    }
                }
            }

            if (listaFechasCarta.Count > 0)
            {
                List<string> lista = new List<string>();

                foreach (DateTime item in listaFechasCarta)
                {
                    lista.Add(item.ToString("dd 'de' MMMMMM 'de' yyyy"));
                }

                fechaCarta7248HorasOmiso = String.Join(" ,", lista);
            }



            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = fechaCarta7248HorasOmiso == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha carta 72, 48 horas radio");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@FCarta7248horas,", fechaCarta7248HorasOmiso);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaNotarialRadioOmiso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[8];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Parametro = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string FechasCarta = ReplaceVacio;


            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            //fechas cartas 72,48,24
            List<DateTime?> listaFechasCarta = new List<DateTime?>();
            var WRKF_SID = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, idTrace);
            if (WRKF_SID != 0)
            {
                var prerequisitoId24 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, idTrace);

                if (prerequisitoId24 != 0 || prerequisitoId24 != null)
                {
                    WORKF_TRACES en = new WORKF_TRACES();
                    var item = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId24, WRKF_SID);
                    en.WRKF_TID = item.WRKF_TID;
                    en.WRKF_ADATE = item.WRKF_ADATE;
                    listaFechasCarta.Add(en.WRKF_ADATE);

                    var WRKF_SID2 = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, en.WRKF_TID);

                    if (WRKF_SID2 != 0)
                    {
                        var prerequisitoId48 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, en.WRKF_TID);

                        if (prerequisitoId48 != 0 || prerequisitoId48 != null)
                        {
                            WORKF_TRACES en2 = new WORKF_TRACES();
                            var item2 = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId48, WRKF_SID2);
                            en2.WRKF_TID = item2.WRKF_TID;
                            en2.WRKF_ADATE = item2.WRKF_ADATE;
                            listaFechasCarta.Add(en2.WRKF_ADATE);

                            var WRKF_SID3 = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, en2.WRKF_TID);

                            if (WRKF_SID3 != 0)
                            {
                                var prerequisitoId72 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, en2.WRKF_TID);

                                if (prerequisitoId72 != 0 || prerequisitoId72 != null)
                                {
                                    WORKF_TRACES en3 = new WORKF_TRACES();
                                    var item3 = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId72, WRKF_SID2);
                                    en3.WRKF_TID = item3.WRKF_TID;
                                    en3.WRKF_ADATE = item3.WRKF_ADATE;
                                    listaFechasCarta.Add(en3.WRKF_ADATE);
                                }
                            }
                        }
                    }
                }
            }

            if (listaFechasCarta.Count > 0)
            {
                List<string> lista = new List<string>();

                foreach (DateTime item in listaFechasCarta)
                {
                    lista.Add(item.ToString("dd 'de' MMMMMM 'de' yyyy"));
                }

                FechasCarta = String.Join(" ,", lista);
            }



            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = FechasCarta == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha carta 72, 48 y 24 horas radio");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechCN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@FechCart", FechasCarta);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }


        // MOROSOS RADIO
        public bool CrearReporteNotificacion72Horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearReporteNotificacion48Horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[9];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string fechaCarta72Horas = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }

            //fechas cartas 72
            List<DateTime?> listaFechasCarta = new List<DateTime?>();
            var WRKF_SID = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, idTrace);
            if (WRKF_SID != 0)
            {
                var prerequisitoId = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, idTrace);

                if (prerequisitoId != 0 || prerequisitoId != null)
                {
                    WORKF_TRACES en = new WORKF_TRACES();
                    var item = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId, WRKF_SID);
                    en.WRKF_TID = item.WRKF_TID;
                    en.WRKF_ADATE = item.WRKF_ADATE;
                    listaFechasCarta.Add(en.WRKF_ADATE);
                }
            }

            if (listaFechasCarta.Count > 0)
            {
                List<string> lista = new List<string>();

                foreach (DateTime item in listaFechasCarta)
                {
                    lista.Add(item.ToString("dd 'de' MMMMMM 'de' yyyy"));
                }

                fechaCarta72Horas = String.Join(" ,", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = fechaCarta72Horas == ReplaceVacio ? 0 : 1;
            val[8] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha carta de 72 horas");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda formato cadena");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@FCarta72horas", fechaCarta72Horas);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearReporteNotificacion24Horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace)
        {
            int[] val = new int[8];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string fechaCarta7248Horas = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }

            //fechas cartas 72,48
            List<DateTime?> listaFechasCarta = new List<DateTime?>();
            var WRKF_SID2 = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, idTrace);
            if (WRKF_SID2 != 0)
            {
                var prerequisitoId48 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, idTrace);

                if (prerequisitoId48 != 0 || prerequisitoId48 != null)
                {
                    WORKF_TRACES en2 = new WORKF_TRACES();
                    var item2 = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId48, WRKF_SID2);
                    en2.WRKF_TID = item2.WRKF_TID;
                    en2.WRKF_ADATE = item2.WRKF_ADATE;
                    listaFechasCarta.Add(en2.WRKF_ADATE);

                    var WRKF_SID3 = new BLReporte().ObtenerEstadoTrace(GlobalVars.Global.OWNER, en2.WRKF_TID);

                    if (WRKF_SID3 != 0)
                    {
                        var prerequisitoId72 = new BLReporte().ObtenerPrerequisito(GlobalVars.Global.OWNER, IdLicencia, en2.WRKF_TID);

                        if (prerequisitoId72 != 0 || prerequisitoId72 != null)
                        {
                            WORKF_TRACES en3 = new WORKF_TRACES();
                            var item3 = new BLReporte().ObtenerFechaCarta(GlobalVars.Global.OWNER, prerequisitoId72, WRKF_SID2);
                            en3.WRKF_TID = item3.WRKF_TID;
                            en3.WRKF_ADATE = item3.WRKF_ADATE;
                            listaFechasCarta.Add(en3.WRKF_ADATE);
                        }
                    }
                }
            }

            if (listaFechasCarta.Count > 0)
            {
                List<string> lista = new List<string>();

                foreach (DateTime item in listaFechasCarta)
                {
                    lista.Add(item.ToString("dd 'de' MMMMMM 'de' yyyy"));
                }

                fechaCarta7248Horas = String.Join(" ,", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = fechaCarta7248Horas == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha carta de 72 y 48 horas");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@FCarta7248horas", fechaCarta7248Horas);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearReporteCartaNotarilalMoroso(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[8];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string Parametro = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (ParametroValue != null)
            {
                Parametro = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = Parametro == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechCN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@Parametros", Parametro);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }


        //CONTRATO TIPO RADIO DIFUSION
        public bool CrearContratoTipoRadioDifusion(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[20];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            BEUbigeoRpt ubigeoOficina = new BEUbigeoRpt();
            string RuApdayc = ReplaceVacio;
            string DirOficna = ReplaceVacio;
            string UbigeoOficina = ReplaceVacio;
            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string DocContacto = ReplaceVacio;
            string LocalName = ReplaceVacio;
            DateTime FechaLicencia;
            string FechaLicenciaFormanto = ReplaceVacio;
            string FRadio = ReplaceVacio;
            string DirUsuarioDerecho = ReplaceVacio;
            string UbigeoUsuarioDerecho = ReplaceVacio;
            string PartidaUsuarioDerecho = ReplaceVacio;

            string distritoLocUsu = ReplaceVacio;
            string provinciaLocUsu = ReplaceVacio;
            string departamentoLocUsu = ReplaceVacio;

            string distritoLocOfi = ReplaceVacio;
            string provinciaLocOfi = ReplaceVacio;
            string departamentoLocOfi = ReplaceVacio;

            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            FechaLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia).FechaCreacionLicencia;
            var FrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);

            RuApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (oficina != null)
            {
                DirOficna = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();

                ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    distritoLocOfi = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    provinciaLocOfi = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    departamentoLocOfi = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                DirUsuarioDerecho = usuarioderecho.ADDRESS == null ? ReplaceVacio : usuarioderecho.ADDRESS;
                PartidaUsuarioDerecho = usuarioderecho.BPS_PARTIDA == null ? ReplaceVacio : usuarioderecho.BPS_PARTIDA;

                if (usuarioderecho.TIS_N != null && usuarioderecho.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLocUsu = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLocUsu = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLocUsu = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
                DocContacto = contacto.TAX_ID == null ? ReplaceVacio : contacto.TAX_ID;
            }


            if (FechaLicencia != null)
            {
                FechaLicenciaFormanto = FechaLicencia.ToString("dd 'del mes de' MMMMMM 'de' yyyy");
            }

            if (FrecuenciaRadio != null)
            {
                FRadio = FrecuenciaRadio.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = RuApdayc == ReplaceVacio ? 0 : 1;
            val[1] = DirOficna == ReplaceVacio ? 0 : 1;
            val[2] = NomAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[3] = DocAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[4] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[5] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[8] = DocContacto == ReplaceVacio ? 0 : 1;
            val[9] = LocalName == ReplaceVacio ? 0 : 1;
            val[10] = FRadio == ReplaceVacio ? 0 : 1;
            val[11] = FechaLicenciaFormanto == ReplaceVacio ? 0 : 1;
            val[12] = DirUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[13] = PartidaUsuarioDerecho == ReplaceVacio ? 0 : 1;

            val[14] = distritoLocUsu == ReplaceVacio ? 0 : 1;
            val[15] = provinciaLocUsu == ReplaceVacio ? 0 : 1;
            val[16] = departamentoLocUsu == ReplaceVacio ? 0 : 1;

            val[17] = distritoLocOfi == ReplaceVacio ? 0 : 1;
            val[18] = provinciaLocOfi == ReplaceVacio ? 0 : 1;
            val[19] = departamentoLocOfi == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Ruc apdayc");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo Oficina");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre autoridad principal (ir a oficina de recaudo -> pestaña agentes)");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Documento autoridad principal (ir a oficina de recaudo -> pestaña agentes)");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Nombre usuario de derecho (ir a socio de negocio  - crear perfil usuario de derecho) ");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Ruc usuario de derecho (ir a socio de negocio  - crear perfil usuario de derecho) ");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Nombre contacto");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Número documento contacto");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                break;
                            case 11:
                                GlobalVars.Global.ListMessageReport.Add("Fecha de conformidad de contrato");
                                break;
                            case 12:
                                GlobalVars.Global.ListMessageReport.Add("Dirección del usuario de derecho");
                                break;
                            case 13:
                                GlobalVars.Global.ListMessageReport.Add("Número de partida del usuario de derecho");
                                break;


                            case 14:
                                GlobalVars.Global.ListMessageReport.Add("Distrito del usuario de derecho");
                                break;
                            case 15:
                                GlobalVars.Global.ListMessageReport.Add("Provincia del usuario de derecho");
                                break;
                            case 16:
                                GlobalVars.Global.ListMessageReport.Add("Departamento del usuario de derecho");
                                break;



                            case 17:
                                GlobalVars.Global.ListMessageReport.Add("Distrito de la oficina de recaudo");
                                break;
                            case 18:
                                GlobalVars.Global.ListMessageReport.Add("Provincia de la oficina de recaudo");
                                break;
                            case 19:
                                GlobalVars.Global.ListMessageReport.Add("Departamento de la oficina de recaudo");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@RApdayc", RuApdayc);
                keyValues.Add("@DirOficina", DirOficna);
                keyValues.Add("@AutoriPrinciOf", NomAutoridadPrincipal);
                keyValues.Add("@DocAutori", DocAutoridadPrincipal);
                keyValues.Add("@UsuDer", UsuarioDerecho);
                keyValues.Add("@RUsuDer", RucUsuarioDerecho);
                keyValues.Add("@DirUsuDer", DirUsuarioDerecho);

                keyValues.Add("@DisUsuDerecho", distritoLocUsu);
                keyValues.Add("@ProvUsuDerecho", provinciaLocUsu);
                keyValues.Add("@DepUsuDerecho", departamentoLocUsu);

                keyValues.Add("@DisOfi", distritoLocOfi);
                keyValues.Add("@ProvOfi", provinciaLocOfi);
                keyValues.Add("@DepOfi", departamentoLocOfi);

                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@DocContacto", DocContacto);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@FRadio", FRadio);
                keyValues.Add("@FechLicencia", FechaLicenciaFormanto);
                keyValues.Add("@NumPart", PartidaUsuarioDerecho);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public static void Convert(string input, string output, WdSaveFormat format)
        {
            try
            {
                Word._Application oWord = new Word.Application();
                oWord.Visible = false;
                object oMissing = System.Reflection.Missing.Value;
                object isVisible = true;
                object readOnly = false;
                object oInput = input;
                object oOutput = output;
                object oFormat = format;
                Word._Document oDoc = oWord.Documents.Open(ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                oDoc.Activate();
                oDoc.SaveAs(ref oOutput, ref oFormat, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Convert", ex);
            }
        }

        public void SearchAndReplace(string document, Dictionary<string, string> dict)
        {
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(document, true))
                {
                    string docText = null;
                    using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }

                    foreach (KeyValuePair<string, string> item in dict)
                    {
                        Regex regexText = new Regex(item.Key);
                        docText = regexText.Replace(docText, item.Value);
                    }

                    using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }
                    //XmlDocument xdoc = new XmlDocument();
                    //xdoc.LoadXml(docText);
                    //xdoc.Save(GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.xml"); 

                }
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "SearchAndReplace", ex);
            }
        }

        public void FindandReplace(string fileDoc, Dictionary<string, string> param)
        {
            Word._Application oWord = new Word.Application();
            oWord.Visible = false;
            object oMissing = System.Reflection.Missing.Value;
            object isVisible = false;
            object readOnly = false;
            object oInput = fileDoc;
            object oOutput = oMissing;
            object oFormat = oMissing;
            Word._Document doc = oWord.Documents.Open(ref oInput, ref oMissing, ref readOnly,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            doc.Activate();

            foreach (KeyValuePair<string, string> item in param)
            {
                Word.Range myStoryRange = doc.Range();

                //First search the main document using the Selection
                Word.Find myFind = myStoryRange.Find;
                myFind.Text = item.Key;
                myFind.Replacement.Text = item.Value;
                myFind.Forward = true;
                myFind.Wrap = Word.WdFindWrap.wdFindContinue;
                myFind.Format = false;
                myFind.MatchCase = false;
                myFind.MatchWholeWord = false;
                myFind.MatchWildcards = false;
                myFind.MatchSoundsLike = false;
                myFind.MatchAllWordForms = false;
                myFind.Execute(Replace: Word.WdReplace.wdReplaceAll);

                ////'Now search all other stories using Ranges
                //foreach (Word.Range otherStoryRange in doc.StoryRanges)
                //{
                //    if (otherStoryRange.StoryType != Word.WdStoryType.wdMainTextStory)
                //    {
                //        Word.Find myOtherFind = otherStoryRange.Find;
                //        myOtherFind.Text = item.Key;
                //        myOtherFind.Replacement.Text = item.Value;
                //        myOtherFind.Wrap = Word.WdFindWrap.wdFindContinue;
                //        myOtherFind.Execute(Replace: Word.WdReplace.wdReplaceAll);
                //    }

                //    // 'Now search all next stories of other stories (doc.storyRanges dont seem to cascades in sub story)
                //    Word.Range nextStoryRange = otherStoryRange.NextStoryRange;
                //    while (nextStoryRange != null)
                //    {
                //        Word.Find myNextStoryFind = nextStoryRange.Find;
                //        myNextStoryFind.Text = item.Key;
                //        myNextStoryFind.Replacement.Text = item.Value; ;
                //        myNextStoryFind.Wrap = Word.WdFindWrap.wdFindContinue;
                //        myNextStoryFind.Execute(Replace: Word.WdReplace.wdReplaceAll);

                //        nextStoryRange = nextStoryRange.NextStoryRange;
                //    }

                // }
            }
            doc.Save();
            doc.Close();
            oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
        }

        public string LeerWord(string ruta)
        {
            string contenido = "";
            try
            {
                using (StreamReader sr = new StreamReader(ruta, false))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        contenido = contenido + line;
                    }
                }
            }
            catch (Exception)
            {
                contenido = "El archivo no se puede leer";
            }
            return contenido;
        }

        #region TV_CARTA
        //CrearCartaProcesoBaseTvPersonaNaturalCarta1
        public bool CrearCartaInformativa(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[7];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            val[3] = LocalName == ReplaceVacio ? 0 : 1;
            val[4] = Web == ReplaceVacio ? 0 : 1;
            val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[6] = Horario == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Localidad");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                break;
                            case 5:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");

                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Horario en parametros del establecimiento");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Frecuencia", FrecuenciaRadio);
                keyValues.Add("@Localidad", LocalName);
                keyValues.Add("@Web", Web);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Horario", Horario);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaTv72horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            val[3] = LocalName == ReplaceVacio ? 0 : 1;
            val[4] = Web == ReplaceVacio ? 0 : 1;
            val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Localidad");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                break;
                            case 5:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Socio", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Frecuencia", FrecuenciaRadio);
                keyValues.Add("@Web", Web);
                keyValues.Add("@Localidad", LocalName);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaTv48horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();


            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            val[3] = LocalName == ReplaceVacio ? 0 : 1;
            val[4] = Web == ReplaceVacio ? 0 : 1;
            val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Localidad");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                break;
                            case 5:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Socio", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Frecuencia", FrecuenciaRadio);
                keyValues.Add("@Web", Web);
                keyValues.Add("@Localidad", LocalName);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaTv24horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();


            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            val[3] = LocalName == ReplaceVacio ? 0 : 1;
            val[4] = Web == ReplaceVacio ? 0 : 1;
            val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Localidad");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                break;
                            case 5:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Socio", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Frecuencia", FrecuenciaRadio);
                keyValues.Add("@Web", Web);
                keyValues.Add("@Localidad", LocalName);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        //Carta Moroso TV 72
        public bool CrearCartaMorosoTv72H(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    //ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                    else
                    {
                        ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                        if (ubigeo != null) UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        //Carta Moroso TV 48
        public bool CrearCartaMorosoTv48H(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                //if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                //{
                //    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                //    if (ubigeo != null)
                //    {
                //        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                //    }
                //}
                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    //ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                    else
                    {
                        ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                        if (ubigeo != null) UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }


            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        //Carta Moroso TV 24
        public bool CrearCartaMorosoTv24H(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    //ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                    else
                    {
                        ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                        if (ubigeo != null) UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        //Carta Notarial de Prohibicion de Uso del Repertorio
        public bool CrearCartaNotarialProhibicionTv(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearContratoLicenciaTv(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[20];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            BEUbigeoRpt ubigeoOficina = new BEUbigeoRpt();
            string RuApdayc = ReplaceVacio;
            string DirOficna = ReplaceVacio;
            string UbigeoOficina = ReplaceVacio;
            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string DocContacto = ReplaceVacio;
            string LocalName = ReplaceVacio;
            DateTime FechaLicencia;
            string FechaLicenciaFormanto = ReplaceVacio;
            string FRadio = ReplaceVacio;
            string DirUsuarioDerecho = ReplaceVacio;
            string UbigeoUsuarioDerecho = ReplaceVacio;
            string PartidaUsuarioDerecho = ReplaceVacio;

            string distritoLocUsu = ReplaceVacio;
            string provinciaLocUsu = ReplaceVacio;
            string departamentoLocUsu = ReplaceVacio;

            string distritoLocOfi = ReplaceVacio;
            string provinciaLocOfi = ReplaceVacio;
            string departamentoLocOfi = ReplaceVacio;

            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            FechaLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia).FechaCreacionLicencia;
            var FrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);

            RuApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (oficina != null)
            {
                DirOficna = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();

                ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    distritoLocOfi = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    provinciaLocOfi = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    departamentoLocOfi = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                DirUsuarioDerecho = usuarioderecho.ADDRESS == null ? ReplaceVacio : usuarioderecho.ADDRESS;
                PartidaUsuarioDerecho = usuarioderecho.BPS_PARTIDA == null ? ReplaceVacio : usuarioderecho.BPS_PARTIDA;

                if (usuarioderecho.TIS_N != null && usuarioderecho.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLocUsu = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLocUsu = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLocUsu = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
                DocContacto = contacto.TAX_ID == null ? ReplaceVacio : contacto.TAX_ID;
            }


            if (FechaLicencia != null)
            {
                FechaLicenciaFormanto = FechaLicencia.ToString("dd 'del mes de' MMMMMM 'de' yyyy");
            }

            if (FrecuenciaRadio != null)
            {
                FRadio = FrecuenciaRadio.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = RuApdayc == ReplaceVacio ? 0 : 1;
            val[1] = DirOficna == ReplaceVacio ? 0 : 1;
            val[2] = NomAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[3] = DocAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[4] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[5] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[8] = DocContacto == ReplaceVacio ? 0 : 1;
            val[9] = LocalName == ReplaceVacio ? 0 : 1;
            val[10] = FRadio == ReplaceVacio ? 0 : 1;
            val[11] = FechaLicenciaFormanto == ReplaceVacio ? 0 : 1;
            val[12] = DirUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[13] = PartidaUsuarioDerecho == ReplaceVacio ? 0 : 1;

            val[14] = distritoLocUsu == ReplaceVacio ? 0 : 1;
            val[15] = provinciaLocUsu == ReplaceVacio ? 0 : 1;
            val[16] = departamentoLocUsu == ReplaceVacio ? 0 : 1;

            val[17] = distritoLocOfi == ReplaceVacio ? 0 : 1;
            val[18] = provinciaLocOfi == ReplaceVacio ? 0 : 1;
            val[19] = departamentoLocOfi == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Ruc apdayc");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo Oficina");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre autoridad principal (ir a oficina de recaudo -> pestaña agentes)");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Documento autoridad principal (ir a oficina de recaudo -> pestaña agentes)");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Nombre usuario de derecho (ir a socio de negocio  - crear perfil usuario de derecho) ");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Ruc usuario de derecho (ir a socio de negocio  - crear perfil usuario de derecho) ");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Nombre contacto");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Número documento contacto");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("Agregar una Frecuencia en la pestaña parametros del establecimiento");
                                break;
                            case 11:
                                GlobalVars.Global.ListMessageReport.Add("Fecha de conformidad de contrato");
                                break;
                            case 12:
                                GlobalVars.Global.ListMessageReport.Add("Dirección del usuario de derecho");
                                break;
                            case 13:
                                GlobalVars.Global.ListMessageReport.Add("Número de partida del usuario de derecho");
                                break;


                            case 14:
                                GlobalVars.Global.ListMessageReport.Add("Distrito del usuario de derecho");
                                break;
                            case 15:
                                GlobalVars.Global.ListMessageReport.Add("Provincia del usuario de derecho");
                                break;
                            case 16:
                                GlobalVars.Global.ListMessageReport.Add("Departamento del usuario de derecho");
                                break;



                            case 17:
                                GlobalVars.Global.ListMessageReport.Add("Distrito de la oficina de recaudo");
                                break;
                            case 18:
                                GlobalVars.Global.ListMessageReport.Add("Provincia de la oficina de recaudo");
                                break;
                            case 19:
                                GlobalVars.Global.ListMessageReport.Add("Departamento de la oficina de recaudo");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@RApdayc", RuApdayc);
                keyValues.Add("@DirOficina", DirOficna);
                keyValues.Add("@AutoriPrinciOf", NomAutoridadPrincipal);
                keyValues.Add("@DocAutori", DocAutoridadPrincipal);
                keyValues.Add("@UsuDer", UsuarioDerecho);
                keyValues.Add("@RUsuDer", RucUsuarioDerecho);
                keyValues.Add("@DirUsuDer", DirUsuarioDerecho);

                keyValues.Add("@DisUsuDerecho", distritoLocUsu);
                keyValues.Add("@ProvUsuDerecho", provinciaLocUsu);
                keyValues.Add("@DepUsuDerecho", departamentoLocUsu);

                keyValues.Add("@DisOfi", distritoLocOfi);
                keyValues.Add("@ProvOfi", provinciaLocOfi);
                keyValues.Add("@DepOfi", departamentoLocOfi);

                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@DocContacto", DocContacto);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@FRadio", FRadio);
                keyValues.Add("@FechLicencia", FechaLicenciaFormanto);
                keyValues.Add("@NumPart", PartidaUsuarioDerecho);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        #endregion

        #region CABLE CARTA

        public bool CrearCartaInformativaCable(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[7];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            val[3] = LocalName == ReplaceVacio ? 0 : 1;
            val[4] = Web == ReplaceVacio ? 0 : 1;
            val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[6] = Horario == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Agregar una Frecuencia en la pestaña parametros del establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Localidad");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                break;
                            case 5:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Horario en parametros del establecimiento");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Frecuencia", FrecuenciaRadio);
                keyValues.Add("@Localidad", LocalName);
                keyValues.Add("@Web", Web);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Horario", Horario);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaCable72horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            val[3] = LocalName == ReplaceVacio ? 0 : 1;
            val[4] = Web == ReplaceVacio ? 0 : 1;
            val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Agregar la Frecuencia en la pestaña parametros del establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Localidad");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                break;
                            case 5:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Socio", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Frecuencia", FrecuenciaRadio);
                keyValues.Add("@Web", Web);
                keyValues.Add("@Localidad", LocalName);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaCable48horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            val[3] = LocalName == ReplaceVacio ? 0 : 1;
            val[4] = Web == ReplaceVacio ? 0 : 1;
            val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Localidad");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                break;
                            case 5:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Socio", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Frecuencia", FrecuenciaRadio);
                keyValues.Add("@Web", Web);
                keyValues.Add("@Localidad", LocalName);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaCable24horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            val[3] = LocalName == ReplaceVacio ? 0 : 1;
            val[4] = Web == ReplaceVacio ? 0 : 1;
            val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Localidad");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                break;
                            case 5:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Socio", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Frecuencia", FrecuenciaRadio);
                keyValues.Add("@Web", Web);
                keyValues.Add("@Localidad", LocalName);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearContratoLicenciaCable(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[20];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            BEUbigeoRpt ubigeoOficina = new BEUbigeoRpt();
            string RuApdayc = ReplaceVacio;
            string DirOficna = ReplaceVacio;
            string UbigeoOficina = ReplaceVacio;
            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string DocContacto = ReplaceVacio;
            string LocalName = ReplaceVacio;
            DateTime FechaLicencia;
            string FechaLicenciaFormanto = ReplaceVacio;
            string FRadio = ReplaceVacio;
            string DirUsuarioDerecho = ReplaceVacio;
            string UbigeoUsuarioDerecho = ReplaceVacio;
            string PartidaUsuarioDerecho = ReplaceVacio;

            string distritoLocUsu = ReplaceVacio;
            string provinciaLocUsu = ReplaceVacio;
            string departamentoLocUsu = ReplaceVacio;

            string distritoLocOfi = ReplaceVacio;
            string provinciaLocOfi = ReplaceVacio;
            string departamentoLocOfi = ReplaceVacio;

            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            FechaLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia).FechaCreacionLicencia;
            var FrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);

            RuApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (oficina != null)
            {
                DirOficna = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();

                ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    distritoLocOfi = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    provinciaLocOfi = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    departamentoLocOfi = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                DirUsuarioDerecho = usuarioderecho.ADDRESS == null ? ReplaceVacio : usuarioderecho.ADDRESS;
                PartidaUsuarioDerecho = usuarioderecho.BPS_PARTIDA == null ? ReplaceVacio : usuarioderecho.BPS_PARTIDA;

                if (usuarioderecho.TIS_N != null && usuarioderecho.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLocUsu = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLocUsu = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLocUsu = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
                DocContacto = contacto.TAX_ID == null ? ReplaceVacio : contacto.TAX_ID;
            }


            if (FechaLicencia != null)
            {
                FechaLicenciaFormanto = FechaLicencia.ToString("dd 'del mes de' MMMMMM 'de' yyyy");
            }

            if (FrecuenciaRadio != null)
            {
                FRadio = FrecuenciaRadio.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = RuApdayc == ReplaceVacio ? 0 : 1;
            val[1] = DirOficna == ReplaceVacio ? 0 : 1;
            val[2] = NomAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[3] = DocAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[4] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[5] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[8] = DocContacto == ReplaceVacio ? 0 : 1;
            val[9] = LocalName == ReplaceVacio ? 0 : 1;
            val[10] = FRadio == ReplaceVacio ? 0 : 1;
            val[11] = FechaLicenciaFormanto == ReplaceVacio ? 0 : 1;
            val[12] = DirUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[13] = PartidaUsuarioDerecho == ReplaceVacio ? 0 : 1;

            val[14] = distritoLocUsu == ReplaceVacio ? 0 : 1;
            val[15] = provinciaLocUsu == ReplaceVacio ? 0 : 1;
            val[16] = departamentoLocUsu == ReplaceVacio ? 0 : 1;

            val[17] = distritoLocOfi == ReplaceVacio ? 0 : 1;
            val[18] = provinciaLocOfi == ReplaceVacio ? 0 : 1;
            val[19] = departamentoLocOfi == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Ruc apdayc");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo Oficina");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre autoridad principal (ir a oficina de recaudo -> pestaña agentes)");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Documento autoridad principal (ir a oficina de recaudo -> pestaña agentes)");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Nombre usuario de derecho (ir a socio de negocio  - crear perfil usuario de derecho) ");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Ruc usuario de derecho (ir a socio de negocio  - crear perfil usuario de derecho) ");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Nombre contacto");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Número documento contacto");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                break;
                            case 11:
                                GlobalVars.Global.ListMessageReport.Add("Fecha de conformidad de contrato");
                                break;
                            case 12:
                                GlobalVars.Global.ListMessageReport.Add("Dirección del usuario de derecho");
                                break;
                            case 13:
                                GlobalVars.Global.ListMessageReport.Add("Número de partida del usuario de derecho");
                                break;
                            case 14:
                                GlobalVars.Global.ListMessageReport.Add("Distrito del usuario de derecho");
                                break;
                            case 15:
                                GlobalVars.Global.ListMessageReport.Add("Provincia del usuario de derecho");
                                break;
                            case 16:
                                GlobalVars.Global.ListMessageReport.Add("Departamento del usuario de derecho");
                                break;
                            case 17:
                                GlobalVars.Global.ListMessageReport.Add("Distrito de la oficina de recaudo");
                                break;
                            case 18:
                                GlobalVars.Global.ListMessageReport.Add("Provincia de la oficina de recaudo");
                                break;
                            case 19:
                                GlobalVars.Global.ListMessageReport.Add("Departamento de la oficina de recaudo");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@RApdayc", RuApdayc);
                keyValues.Add("@DirOficina", DirOficna);
                keyValues.Add("@AutoriPrinciOf", NomAutoridadPrincipal);
                keyValues.Add("@DocAutori", DocAutoridadPrincipal);
                keyValues.Add("@UsuDer", UsuarioDerecho);
                keyValues.Add("@RUsuDer", RucUsuarioDerecho);
                keyValues.Add("@DirUsuDer", DirUsuarioDerecho);

                keyValues.Add("@DisUsuDerecho", distritoLocUsu);
                keyValues.Add("@ProvUsuDerecho", provinciaLocUsu);
                keyValues.Add("@DepUsuDerecho", departamentoLocUsu);

                keyValues.Add("@DisOfi", distritoLocOfi);
                keyValues.Add("@ProvOfi", provinciaLocOfi);
                keyValues.Add("@DepOfi", departamentoLocOfi);

                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@DocContacto", DocContacto);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@FRadio", FRadio);
                keyValues.Add("@FechLicencia", FechaLicenciaFormanto);
                keyValues.Add("@NumPart", PartidaUsuarioDerecho);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        //CARTA MOROSO CABLE
        public bool CrearCartaMorosoCable72H(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaMorosoCable48H(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de Derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Falta Ingresar la Fecha del documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Falta el Nombre de establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Agregar la Dirección de establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Ingrese el Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Ingrese el Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaMorosoCable24H(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        //Carta Notarial de Prohibicion de Uso del Repertorio cable
        public bool CrearCartaNotarialProhibicionCable(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        #endregion

        #region TV POR INTERNET

        public bool CrearCartaInformativaTVxInternet(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[4];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = Web == ReplaceVacio ? 0 : 1;
            val[3] = UsuarioDerecho == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                break;
                            case 3:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Web", Web);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaTVxInternet72horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[2];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Socio", UsuarioDerecho);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaTVxInternet48horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            val[3] = LocalName == ReplaceVacio ? 0 : 1;
            val[4] = Web == ReplaceVacio ? 0 : 1;
            val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Localidad");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                break;
                            case 5:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Socio", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Frecuencia", FrecuenciaRadio);
                keyValues.Add("@Web", Web);
                keyValues.Add("@Localidad", LocalName);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaTVxInternet24horas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            val[3] = LocalName == ReplaceVacio ? 0 : 1;
            val[4] = Web == ReplaceVacio ? 0 : 1;
            val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Localidad");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                break;
                            case 5:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Socio", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Frecuencia", FrecuenciaRadio);
                keyValues.Add("@Web", Web);
                keyValues.Add("@Localidad", LocalName);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        //CARTA MOROSO TV POR INTERNET
        public bool CrearCartaMorosoTVxInternet72H(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaMorosoTVxInternet48H(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaMorosoTVxInternet24H(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        //Carta Notarial de Prohibicion de Uso del Repertorio Tv Internet
        public bool CrearCartaNotarialProhibicionTVxInternet(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;
            val[2] = LocalName == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = UbigeoEstablecimiento == ReplaceVacio ? 0 : 1;
            val[5] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = PeriodoDeudaFormato == ReplaceVacio ? 0 : 1;
            val[8] = TotalDeudaCadena == ReplaceVacio ? 0 : 1;
            val[9] = TotalDeuda == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección establecimiento");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del contacto");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo del contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Periodo deuda");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato cadena");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Total deuda formato numérico");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Nom_Comercial", UsuarioDerecho);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@UbigeoL", UbigeoEstablecimiento);
                keyValues.Add("@CONTACTO", ContactoNombre);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@PeriodDeuda", PeriodoDeudaFormato);
                keyValues.Add("@TotalCad", TotalDeudaCadena);
                keyValues.Add("@TotalDeuda", TotalDeuda);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearContratoLicenciaTvInternet(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[20];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            BEUbigeoRpt ubigeoOficina = new BEUbigeoRpt();
            string RuApdayc = ReplaceVacio;
            string DirOficna = ReplaceVacio;
            string UbigeoOficina = ReplaceVacio;
            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string DocContacto = ReplaceVacio;
            string LocalName = ReplaceVacio;
            DateTime FechaLicencia;
            string FechaLicenciaFormanto = ReplaceVacio;
            string FRadio = ReplaceVacio;
            string DirUsuarioDerecho = ReplaceVacio;
            string UbigeoUsuarioDerecho = ReplaceVacio;
            string PartidaUsuarioDerecho = ReplaceVacio;

            string distritoLocUsu = ReplaceVacio;
            string provinciaLocUsu = ReplaceVacio;
            string departamentoLocUsu = ReplaceVacio;

            string distritoLocOfi = ReplaceVacio;
            string provinciaLocOfi = ReplaceVacio;
            string departamentoLocOfi = ReplaceVacio;

            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            FechaLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia).FechaCreacionLicencia;
            var FrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);

            RuApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (oficina != null)
            {
                DirOficna = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();

                ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    distritoLocOfi = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    provinciaLocOfi = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    departamentoLocOfi = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                DirUsuarioDerecho = usuarioderecho.ADDRESS == null ? ReplaceVacio : usuarioderecho.ADDRESS;
                PartidaUsuarioDerecho = usuarioderecho.BPS_PARTIDA == null ? ReplaceVacio : usuarioderecho.BPS_PARTIDA;

                if (usuarioderecho.TIS_N != null && usuarioderecho.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLocUsu = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLocUsu = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLocUsu = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
                DocContacto = contacto.TAX_ID == null ? ReplaceVacio : contacto.TAX_ID;
            }


            if (FechaLicencia != null)
            {
                FechaLicenciaFormanto = FechaLicencia.ToString("dd 'del mes de' MMMMMM 'de' yyyy");
            }

            if (FrecuenciaRadio != null)
            {
                FRadio = FrecuenciaRadio.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = RuApdayc == ReplaceVacio ? 0 : 1;
            val[1] = DirOficna == ReplaceVacio ? 0 : 1;
            val[2] = NomAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[3] = DocAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[4] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[5] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[6] = CargoCT == ReplaceVacio ? 0 : 1;
            val[7] = ContactoNombre == ReplaceVacio ? 0 : 1;
            val[8] = DocContacto == ReplaceVacio ? 0 : 1;
            val[9] = LocalName == ReplaceVacio ? 0 : 1;
            val[10] = FRadio == ReplaceVacio ? 0 : 1;
            val[11] = FechaLicenciaFormanto == ReplaceVacio ? 0 : 1;
            val[12] = DirUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[13] = PartidaUsuarioDerecho == ReplaceVacio ? 0 : 1;

            val[14] = distritoLocUsu == ReplaceVacio ? 0 : 1;
            val[15] = provinciaLocUsu == ReplaceVacio ? 0 : 1;
            val[16] = departamentoLocUsu == ReplaceVacio ? 0 : 1;

            val[17] = distritoLocOfi == ReplaceVacio ? 0 : 1;
            val[18] = provinciaLocOfi == ReplaceVacio ? 0 : 1;
            val[19] = departamentoLocOfi == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Ruc apdayc");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Ubigeo Oficina");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre autoridad principal (ir a oficina de recaudo -> pestaña agentes)");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Documento autoridad principal (ir a oficina de recaudo -> pestaña agentes)");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Nombre usuario de derecho (ir a socio de negocio  - crear perfil usuario de derecho) ");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Ruc usuario de derecho (ir a socio de negocio  - crear perfil usuario de derecho) ");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Cargo contacto");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Nombre contacto");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Número documento contacto");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Nombre establecimiento");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                break;
                            case 11:
                                GlobalVars.Global.ListMessageReport.Add("Fecha de conformidad de contrato");
                                break;
                            case 12:
                                GlobalVars.Global.ListMessageReport.Add("Dirección del usuario de derecho");
                                break;
                            case 13:
                                GlobalVars.Global.ListMessageReport.Add("Número de partida del usuario de derecho");
                                break;


                            case 14:
                                GlobalVars.Global.ListMessageReport.Add("Distrito del usuario de derecho");
                                break;
                            case 15:
                                GlobalVars.Global.ListMessageReport.Add("Provincia del usuario de derecho");
                                break;
                            case 16:
                                GlobalVars.Global.ListMessageReport.Add("Departamento del usuario de derecho");
                                break;



                            case 17:
                                GlobalVars.Global.ListMessageReport.Add("Distrito de la oficina de recaudo");
                                break;
                            case 18:
                                GlobalVars.Global.ListMessageReport.Add("Provincia de la oficina de recaudo");
                                break;
                            case 19:
                                GlobalVars.Global.ListMessageReport.Add("Departamento de la oficina de recaudo");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@RApdayc", RuApdayc);
                keyValues.Add("@DirOficina", DirOficna);
                keyValues.Add("@AutoriPrinciOf", NomAutoridadPrincipal);
                keyValues.Add("@DocAutori", DocAutoridadPrincipal);
                keyValues.Add("@UsuDer", UsuarioDerecho);
                keyValues.Add("@RUsuDer", RucUsuarioDerecho);
                keyValues.Add("@DirUsuDer", DirUsuarioDerecho);

                keyValues.Add("@DisUsuDerecho", distritoLocUsu);
                keyValues.Add("@ProvUsuDerecho", provinciaLocUsu);
                keyValues.Add("@DepUsuDerecho", departamentoLocUsu);

                keyValues.Add("@DisOfi", distritoLocOfi);
                keyValues.Add("@ProvOfi", provinciaLocOfi);
                keyValues.Add("@DepOfi", departamentoLocOfi);

                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@Contacto", ContactoNombre);
                keyValues.Add("@DocContacto", DocContacto);
                keyValues.Add("@Local", LocalName);
                keyValues.Add("@FRadio", FRadio);
                keyValues.Add("@FechLicencia", FechaLicenciaFormanto);
                keyValues.Add("@NumPart", PartidaUsuarioDerecho);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        #endregion

        #region FONO_CARTA

        public bool CrearPrimeraCartaReqFono(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[2];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            //val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            //val[3] = LocalName == ReplaceVacio ? 0 : 1;
            //val[4] = Web == ReplaceVacio ? 0 : 1;
            //val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            //val[6] = Horario == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El Documento debe tener asignada una Fecha.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                                //case 2:
                                //    GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                                //    break;
                                //case 3:
                                //    GlobalVars.Global.ListMessageReport.Add("Localidad");
                                //    break;
                                //case 4:
                                //    GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                                //    break;
                                //case 5:
                                //    GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                                //    break;
                                //case 6:
                                //    GlobalVars.Global.ListMessageReport.Add("Horario en parametros del establecimiento");
                                //    break;
                                //default:
                                //    break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Socio", UsuarioDerecho);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearSegundaCartaReqFono(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idTrace, string tipoPersona)
        {
            int[] val = new int[2];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string Web = ReplaceVacio;
            string FrecuenciaRadio = ReplaceVacio;
            string Horario = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroWeb = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var ParametroFrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var ParametroHorario = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdHorario);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }


            if (usuarioderecho != null)
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;

            if (establecimiento != null)
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

            if (ParametroWeb != null)
                Web = ParametroWeb.PAR_VALUE == null ? ReplaceVacio : ParametroWeb.PAR_VALUE;

            if (ParametroFrecuenciaRadio != null)
                FrecuenciaRadio = ParametroFrecuenciaRadio.PAR_VALUE.ToString();

            if (ParametroHorario != null)
                Horario = ParametroHorario.PAR_VALUE.ToString();

            GlobalVars.Global.ListMessageReport = new List<string>();
            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            //val[2] = FrecuenciaRadio == ReplaceVacio ? 0 : 1;
            //val[3] = LocalName == ReplaceVacio ? 0 : 1;
            //val[4] = Web == ReplaceVacio ? 0 : 1;
            //val[5] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            //val[6] = Horario == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);
            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El Documento debe tener asignada una Fecha.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            //case 2:
                            //    GlobalVars.Global.ListMessageReport.Add("Frecuencia radio en parametros del establecimiento");
                            //    break;
                            //case 3:
                            //    GlobalVars.Global.ListMessageReport.Add("Localidad");
                            //    break;
                            //case 4:
                            //    GlobalVars.Global.ListMessageReport.Add("Parámetro Web url en parametros del establecimiento");
                            //    break;
                            //case 5:
                            //    GlobalVars.Global.ListMessageReport.Add("Nombre Comercial.");
                            //    break;
                            //case 6:
                            //    GlobalVars.Global.ListMessageReport.Add("Horario en parametros del establecimiento");
                            //    break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Socio", UsuarioDerecho);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaTransExtrajudicialFono(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();

            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = NomAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[5] = DocAutoridadPrincipal == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El documento debe tener asignada una fecha.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario derecho debe tener registrado su Ruc.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El Establecimiento debe tener asignada una Dirección.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Nombre autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo. ");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Documento autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo.");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Ruc", RucUsuarioDerecho);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@NomAut", NomAutoridadPrincipal);
                keyValues.Add("@DocAuto", DocAutoridadPrincipal);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;

        }

        public bool CrearCartaAcuerdoExtrajudicialFono(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();

            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;


            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = NomAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[5] = DocAutoridadPrincipal == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El documento debe tener asignado una fecha.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario Derecho debe tener registrador un número de Ruc.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El Establecimiento debe tener registrado una Dirección del Local.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Nombre autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo.");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Documento autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo.");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Ruc", RucUsuarioDerecho);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@NomAut", NomAutoridadPrincipal);
                keyValues.Add("@DocAuto", DocAutoridadPrincipal);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;

        }

        public bool CrearCartaConvenioExtrajudicialFono(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();

            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;


            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = NomAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[5] = DocAutoridadPrincipal == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El Documento debe tener asignada una fecha");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario Derecho debe de tener registrado un número de Ruc.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El Establecimiento debe tener registrado una Dirección del local.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Nombre autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo.");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Documento autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo.");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Ruc", RucUsuarioDerecho);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@NomAut", NomAutoridadPrincipal);
                keyValues.Add("@DocAuto", DocAutoridadPrincipal);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;

        }

        public bool CrearCartaLicenciaFono(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[2];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            BEUbigeoRpt ubigeoOficina = new BEUbigeoRpt();
            string RuApdayc = ReplaceVacio;
            string Fecha = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string DirOficna = ReplaceVacio;
            string UbigeoOficina = ReplaceVacio;
            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string DocContacto = ReplaceVacio;
            string LocalName = ReplaceVacio;
            DateTime FechaLicencia;
            string FechaLicenciaFormanto = ReplaceVacio;
            string FRadio = ReplaceVacio;
            string DirUsuarioDerecho = ReplaceVacio;
            string UbigeoUsuarioDerecho = ReplaceVacio;
            string PartidaUsuarioDerecho = ReplaceVacio;

            string distritoLocUsu = ReplaceVacio;
            string provinciaLocUsu = ReplaceVacio;
            string departamentoLocUsu = ReplaceVacio;

            string distritoLocOfi = ReplaceVacio;
            string provinciaLocOfi = ReplaceVacio;
            string departamentoLocOfi = ReplaceVacio;

            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            FechaLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia).FechaCreacionLicencia;
            var FrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            RuApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (oficina != null)
            {
                DirOficna = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();

                ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    distritoLocOfi = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    provinciaLocOfi = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    departamentoLocOfi = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                DirUsuarioDerecho = usuarioderecho.ADDRESS == null ? ReplaceVacio : usuarioderecho.ADDRESS;
                PartidaUsuarioDerecho = usuarioderecho.BPS_PARTIDA == null ? ReplaceVacio : usuarioderecho.BPS_PARTIDA;

                if (usuarioderecho.TIS_N != null && usuarioderecho.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLocUsu = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLocUsu = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLocUsu = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
                DocContacto = contacto.TAX_ID == null ? ReplaceVacio : contacto.TAX_ID;
            }


            if (FechaLicencia != null)
            {
                FechaLicenciaFormanto = FechaLicencia.ToString("dd 'del mes de' MMMMMM 'de' yyyy");
            }

            if (FrecuenciaRadio != null)
            {
                FRadio = FrecuenciaRadio.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = Fecha == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El documento debe tener asignado una fecha.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@UsuDer", UsuarioDerecho);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        #endregion

        #region REDES_DIGITALES_CARTA

        public bool CrearPrimeraCartaReqRedes(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string Web = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string DistritoLoc = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (ParametroValue != null)
                Web = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    //ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        DistritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                    else
                    {
                        ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                        if (ubigeo != null) DistritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = LocalDir == ReplaceVacio ? 0 : 1;
            val[3] = DistritoLoc == ReplaceVacio ? 0 : 1;
            val[4] = CargoCT == ReplaceVacio ? 0 : 1;
            val[5] = Web == ReplaceVacio ? 0 : 1;


            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El documento debe tener asignado una fecha.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Establecimiento debe de tener una registrado una Dirección");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El Establecimiento debe tener asignado un Distrito.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El Contacto debe tener asignado un Cargo.");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario de Derecho debe tener asignado una página web.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@DistritoLoc", DistritoLoc);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@WebUsuDerecho", Web);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearSegundaCartaReqRedes(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string DistritoLoc = ReplaceVacio;
            string Web = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (ParametroValue != null)
                Web = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    //ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        DistritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                    else
                    {
                        ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                        if (ubigeo != null) DistritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = LocalDir == ReplaceVacio ? 0 : 1;
            val[3] = DistritoLoc == ReplaceVacio ? 0 : 1;
            val[4] = CargoCT == ReplaceVacio ? 0 : 1;
            val[5] = Web == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El documento debe tener asignada una fecha.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El establecimiento debe tener asignado una dirección del Local.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El establecimiento debe tener asignado un distrito.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El contacto debe tener asignado un cargo.");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El contacto debe tener asignado una página web.");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@DistritoLoc", DistritoLoc);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@Web", Web);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaNotarialRedes(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            BEUbigeoRpt ubigeoOficina = new BEUbigeoRpt();
            string RucApdayc = ReplaceVacio;
            string Fecha = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string DirOficna = ReplaceVacio;
            string UbigeoOficina = ReplaceVacio;
            string WebContacto = ReplaceVacio;
            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string DocContacto = ReplaceVacio;
            string LocalName = ReplaceVacio;
            DateTime FechaLicencia;
            string FechaLicenciaFormanto = ReplaceVacio;
            string FRadio = ReplaceVacio;
            string DirUsuarioDerecho = ReplaceVacio;
            string UbigeoUsuarioDerecho = ReplaceVacio;
            string PartidaUsuarioDerecho = ReplaceVacio;

            string distritoLocUsu = ReplaceVacio;
            string provinciaLocUsu = ReplaceVacio;
            string departamentoLocUsu = ReplaceVacio;

            string distritoLocOfi = ReplaceVacio;
            string provinciaLocOfi = ReplaceVacio;
            string departamentoLocOfi = ReplaceVacio;

            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            FechaLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia).FechaCreacionLicencia;
            var FrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            RucApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (oficina != null)
            {
                DirOficna = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();

                ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    distritoLocOfi = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    provinciaLocOfi = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    departamentoLocOfi = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                DirUsuarioDerecho = usuarioderecho.ADDRESS == null ? ReplaceVacio : usuarioderecho.ADDRESS;
                PartidaUsuarioDerecho = usuarioderecho.BPS_PARTIDA == null ? ReplaceVacio : usuarioderecho.BPS_PARTIDA;

                if (usuarioderecho.TIS_N != null && usuarioderecho.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLocUsu = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLocUsu = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLocUsu = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (ParametroValue != null)
                WebContacto = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
                DocContacto = contacto.TAX_ID == null ? ReplaceVacio : contacto.TAX_ID;
            }


            if (FechaLicencia != null)
            {
                FechaLicenciaFormanto = FechaLicencia.ToString("dd 'del mes de' MMMMMM 'de' yyyy");
            }

            if (FrecuenciaRadio != null)
            {
                FRadio = FrecuenciaRadio.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = DirUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[3] = distritoLocUsu == ReplaceVacio ? 0 : 1;
            val[4] = CargoCT == ReplaceVacio ? 0 : 1;
            val[5] = WebContacto == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Fecha documento");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe de tener registrado una Direccion.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El usuario derecho debe tener asignado un distrito.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El contacto debe tener asignado un Cargo");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El contacto debe tener asignado una página web.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@DirUsuarioDerecho", DirUsuarioDerecho);
                keyValues.Add("@DisUsuarioDerecho", distritoLocUsu);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@WebContacto", WebContacto);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearContratoLicenciaRedes(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[7];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            BEUbigeoRpt ubigeoOficina = new BEUbigeoRpt();
            string RucApdayc = ReplaceVacio;
            string Fecha = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string DirOficna = ReplaceVacio;
            string UbigeoOficina = ReplaceVacio;
            string WebContacto = ReplaceVacio;
            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string DocContacto = ReplaceVacio;
            string LocalName = ReplaceVacio;
            DateTime FechaLicencia;
            string FechaLicenciaFormanto = ReplaceVacio;
            string FRadio = ReplaceVacio;
            string DirUsuarioDerecho = ReplaceVacio;
            string UbigeoUsuarioDerecho = ReplaceVacio;
            string PartidaUsuarioDerecho = ReplaceVacio;

            string distritoLocUsu = ReplaceVacio;
            string provinciaLocUsu = ReplaceVacio;
            string departamentoLocUsu = ReplaceVacio;

            string distritoLocOfi = ReplaceVacio;
            string provinciaLocOfi = ReplaceVacio;
            string departamentoLocOfi = ReplaceVacio;

            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            FechaLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia).FechaCreacionLicencia;
            var FrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            RucApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (oficina != null)
            {
                DirOficna = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();

                ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    distritoLocOfi = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    provinciaLocOfi = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    departamentoLocOfi = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                DirUsuarioDerecho = usuarioderecho.ADDRESS == null ? ReplaceVacio : usuarioderecho.ADDRESS;
                PartidaUsuarioDerecho = usuarioderecho.BPS_PARTIDA == null ? ReplaceVacio : usuarioderecho.BPS_PARTIDA;

                if (usuarioderecho.TIS_N != null && usuarioderecho.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLocUsu = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLocUsu = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLocUsu = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
                DocContacto = contacto.TAX_ID == null ? ReplaceVacio : contacto.TAX_ID;
            }


            if (FechaLicencia != null)
            {
                FechaLicenciaFormanto = FechaLicencia.ToString("dd 'del mes de' MMMMMM 'de' yyyy");
            }

            if (FrecuenciaRadio != null)
            {
                FRadio = FrecuenciaRadio.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = RucApdayc == ReplaceVacio ? 0 : 1;
            val[2] = NomAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[3] = DocAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[4] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[5] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[6] = DirUsuarioDerecho == ReplaceVacio ? 0 : 1;


            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El documento debe tener asignada una fecha.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("No se encuentra asociado el Ruc de apdayc");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Documento autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado su Ruc.");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@RucApdayc", RucApdayc);

                keyValues.Add("@NomAutoridadPrincipal", NomAutoridadPrincipal);
                keyValues.Add("@DocAutoridadPrincipal", DocAutoridadPrincipal);

                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@RucUsuarioDerecho", RucUsuarioDerecho);
                keyValues.Add("@DirUsuarioDerecho", DirUsuarioDerecho);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaTransExtrajudicialRedes(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string RucApdayc = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string Web = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();

            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            RucApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID;
            }

            if (ParametroValue != null)
                Web = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = RucApdayc == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = Web == ReplaceVacio ? 0 : 1;
            val[5] = Fecha == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado su Ruc.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una número de ruc.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El establecimiento debe tener asignado una Direccion de local");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una Página web.");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El documento debe tener registrado una fecha.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@RucApdayc", RucApdayc);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@RucUsuarioDerecho", RucUsuarioDerecho);
                keyValues.Add("@LocalDir", LocalDir);
                keyValues.Add("@WebEst", Web);
                keyValues.Add("@Fecha", Fecha);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;

        }

        #endregion

        #region SINCRONIZACION_CARTA

        public bool CrearPrimeraCartaReqSinco(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string Web = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string DistritoLoc = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string TelfAutoridadPrincipal = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (ParametroValue != null)
                Web = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    //ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        DistritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                    else
                    {
                        ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                        if (ubigeo != null) DistritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
                TelfAutoridadPrincipal = AutoridadPrincipal.PHONE_NUMBER == null ? ReplaceVacio : AutoridadPrincipal.PHONE_NUMBER;
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = LocalDir == ReplaceVacio ? 0 : 1;
            val[3] = DistritoLoc == ReplaceVacio ? 0 : 1;
            val[4] = CargoCT == ReplaceVacio ? 0 : 1;
            //val[5] = Web == ReplaceVacio ? 0 : 1;
            val[5] = TelfAutoridadPrincipal == ReplaceVacio ? 0 : 1;


            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El documento debe tener asignado una fecha.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El establecimiento debe tener registrado una dirección.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El establecimiento debe tener registrado un distrito.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El contacto debe tener un Cargo");
                                break;
                            //case 5:
                            //    GlobalVars.Global.ListMessageReport.Add("El Usuario Derecho debe tener registrada una página web.");
                            //    break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Esta autoridad principal debe tener un telefono de trabajo.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@DistritoLoc", DistritoLoc);
                keyValues.Add("@CargoCT", CargoCT);
                //keyValues.Add("@WebUsuDerecho", Web);
                keyValues.Add("@TelfAutoridad", TelfAutoridadPrincipal);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearSegundaCartaReqSincro(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string DistritoLoc = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string PeriodoDeudaFormato = ReplaceVacio;
            string TotalDeudaCadena = ReplaceVacio;
            string TotalDeuda = ReplaceVacio;
            List<BEPeriodoDeuda> PeriodoDeuda = new List<BEPeriodoDeuda>();
            string ContactoNombre = ReplaceVacio;
            string CargoCT = ReplaceVacio;

            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string TelfAutoridadPrincipal = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            PeriodoDeuda = new BLReporte().ListaPeriodoDeuda(GlobalVars.Global.OWNER, IdLicencia);
            var DeudaFactura = new BLReporte().TotalDeudaFactura(GlobalVars.Global.OWNER, IdLicencia);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);


            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
            }

            if (DeudaFactura != 0)
            {
                TotalDeuda = String.Format("S/.{0}", DeudaFactura.ToString("# ### ###.00"));
                TotalDeudaCadena = Utility.Util.NumeroALetras(DeudaFactura.ToString());
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    //ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        DistritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                    else
                    {
                        ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                        if (ubigeo != null) DistritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (PeriodoDeuda != null)
            {
                var FechaPeriodo = string.Empty;
                List<string> lista = new List<string>();

                foreach (var item in PeriodoDeuda)
                {
                    lista.Add(item.LIC_MONTH_NAME + " " + item.LIC_YEAR);
                }

                PeriodoDeudaFormato = String.Join("  -  ", lista);
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
                TelfAutoridadPrincipal = AutoridadPrincipal.PHONE_NUMBER == null ? ReplaceVacio : AutoridadPrincipal.PHONE_NUMBER;
            }


            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = LocalDir == ReplaceVacio ? 0 : 1;
            val[3] = DistritoLoc == ReplaceVacio ? 0 : 1;
            val[4] = CargoCT == ReplaceVacio ? 0 : 1;
            val[5] = TelfAutoridadPrincipal == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El Documento debe de tener una fecha asignada.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Establecimiento debe tener asignado un Local.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El Establecimiento debe tener asignado un distrito.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El contacto debe de tener un cargo.");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Esta autoridad principal debe tener un telefono de trabajo.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@DistritoLoc", DistritoLoc);
                keyValues.Add("@CargoCT", CargoCT);
                keyValues.Add("@TelfAutoridad", TelfAutoridadPrincipal);

                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaNotarialSincro(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[5];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            BEUbigeoRpt ubigeoOficina = new BEUbigeoRpt();
            string RucApdayc = ReplaceVacio;
            string Fecha = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string DirOficna = ReplaceVacio;
            string UbigeoOficina = ReplaceVacio;
            string WebContacto = ReplaceVacio;
            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string DocContacto = ReplaceVacio;
            string LocalName = ReplaceVacio;
            DateTime FechaLicencia;
            string FechaLicenciaFormanto = ReplaceVacio;
            string FRadio = ReplaceVacio;
            string DirUsuarioDerecho = ReplaceVacio;
            string UbigeoUsuarioDerecho = ReplaceVacio;
            string PartidaUsuarioDerecho = ReplaceVacio;

            string distritoLocUsu = ReplaceVacio;
            string provinciaLocUsu = ReplaceVacio;
            string departamentoLocUsu = ReplaceVacio;

            string distritoLocOfi = ReplaceVacio;
            string provinciaLocOfi = ReplaceVacio;
            string departamentoLocOfi = ReplaceVacio;

            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            FechaLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia).FechaCreacionLicencia;
            var FrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            RucApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (oficina != null)
            {
                DirOficna = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();

                ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    distritoLocOfi = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    provinciaLocOfi = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    departamentoLocOfi = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                DirUsuarioDerecho = usuarioderecho.ADDRESS == null ? ReplaceVacio : usuarioderecho.ADDRESS;
                PartidaUsuarioDerecho = usuarioderecho.BPS_PARTIDA == null ? ReplaceVacio : usuarioderecho.BPS_PARTIDA;

                if (usuarioderecho.TIS_N != null && usuarioderecho.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLocUsu = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLocUsu = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLocUsu = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (ParametroValue != null)
                WebContacto = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
                DocContacto = contacto.TAX_ID == null ? ReplaceVacio : contacto.TAX_ID;
            }


            if (FechaLicencia != null)
            {
                FechaLicenciaFormanto = FechaLicencia.ToString("dd 'del mes de' MMMMMM 'de' yyyy");
            }

            if (FrecuenciaRadio != null)
            {
                FRadio = FrecuenciaRadio.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = DirUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[3] = distritoLocUsu == ReplaceVacio ? 0 : 1;
            val[4] = CargoCT == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El Documento debe tener asignado una fecha.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado un distrito.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El contacto debe tener asignado un cargo.");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@DirUsuarioDerecho", DirUsuarioDerecho);
                keyValues.Add("@DisUsuarioDerecho", distritoLocOfi);
                keyValues.Add("@CargoCT", CargoCT);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearContratoLicenciaSincro(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[7];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            BEUbigeoRpt ubigeoOficina = new BEUbigeoRpt();
            string RucApdayc = ReplaceVacio;
            string Fecha = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();

            string DirOficna = ReplaceVacio;
            string UbigeoOficina = ReplaceVacio;
            string WebContacto = ReplaceVacio;
            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;

            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string CargoCT = ReplaceVacio;
            string ContactoNombre = ReplaceVacio;
            string DocContacto = ReplaceVacio;
            string LocalName = ReplaceVacio;
            DateTime FechaLicencia;
            string FechaLicenciaFormanto = ReplaceVacio;
            string FRadio = ReplaceVacio;
            string DirUsuarioDerecho = ReplaceVacio;
            string UbigeoUsuarioDerecho = ReplaceVacio;
            string PartidaUsuarioDerecho = ReplaceVacio;

            string distritoLocUsu = ReplaceVacio;
            string provinciaLocUsu = ReplaceVacio;
            string departamentoLocUsu = ReplaceVacio;

            string distritoLocOfi = ReplaceVacio;
            string provinciaLocOfi = ReplaceVacio;
            string departamentoLocOfi = ReplaceVacio;

            var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            FechaLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, IdLicencia).FechaCreacionLicencia;
            var FrecuenciaRadio = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            RucApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (oficina != null)
            {
                DirOficna = oficina.ADDRESS == null ? ReplaceVacio : oficina.ADDRESS.Trim();

                ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, oficina.TIS_N.ToString(), oficina.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    distritoLocOfi = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    provinciaLocOfi = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    departamentoLocOfi = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                DirUsuarioDerecho = usuarioderecho.ADDRESS == null ? ReplaceVacio : usuarioderecho.ADDRESS;
                PartidaUsuarioDerecho = usuarioderecho.BPS_PARTIDA == null ? ReplaceVacio : usuarioderecho.BPS_PARTIDA;

                if (usuarioderecho.TIS_N != null && usuarioderecho.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLocUsu = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLocUsu = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLocUsu = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            if (contacto != null)
            {
                ContactoNombre = contacto.BPS_NAME == null ? ReplaceVacio : contacto.BPS_NAME;
                CargoCT = contacto.ROL_DESC == null ? ReplaceVacio : contacto.ROL_DESC;
                DocContacto = contacto.TAX_ID == null ? ReplaceVacio : contacto.TAX_ID;
            }


            if (FechaLicencia != null)
            {
                FechaLicenciaFormanto = FechaLicencia.ToString("dd 'del mes de' MMMMMM 'de' yyyy");
            }

            if (FrecuenciaRadio != null)
            {
                FRadio = FrecuenciaRadio.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = RucApdayc == ReplaceVacio ? 0 : 1;
            val[2] = NomAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[3] = DocAutoridadPrincipal == ReplaceVacio ? 0 : 1;
            val[4] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[5] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[6] = DirUsuarioDerecho == ReplaceVacio ? 0 : 1;


            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El Documento debe tener una fecha.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El documento debe tener el ruc de apdayc asociado");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Nombre autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Documento autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado su ruc");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado su dirección.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@RucApdayc", RucApdayc);
                keyValues.Add("@NomAutoridadPrincipal", NomAutoridadPrincipal);
                keyValues.Add("@DocAutoridadPrincipal", DocAutoridadPrincipal);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@RucUsuarioDerecho", RucUsuarioDerecho);
                keyValues.Add("@DirUsuarioDerecho", DirUsuarioDerecho);

                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;
        }

        public bool CrearCartaTransExtrajudicialSincro(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[5];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string RucApdayc = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string Web = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();

            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            RucApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID;
            }

            if (ParametroValue != null)
                Web = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = RucApdayc == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = Fecha == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Ruc Apdayc");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado el ruc.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El establecimiento debe tener registrado la dirección");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El Documento debe tener registrado la fecha.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@RucApdayc", RucApdayc);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@RucUsuarioDerecho", RucUsuarioDerecho);
                keyValues.Add("@LocalDir", LocalDir);
                keyValues.Add("@Fecha", Fecha);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }
            return resultado;
        }

        #endregion

        #region Cadenas
        public bool CrearCartaContratoCadenas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string RucApdayc = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string UsuDir = ReplaceVacio;

            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string Web = ReplaceVacio;
            string Ubigeorazon = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();

            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            RucApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                UsuDir = usuarioderecho.ADDRESS == null ? ReplaceVacio : usuarioderecho.ADDRESS;
            }

            if (ParametroValue != null)
                Web = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null && usuarioderecho != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        Ubigeorazon = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = RucApdayc == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[3] = Ubigeorazon == ReplaceVacio ? 0 : 1;
            val[4] = Fecha == ReplaceVacio ? 0 : 1;
            val[5] = UsuDir == ReplaceVacio ? 0 : 1;
            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Ruc Apdayc");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado el ruc.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("No se encuentra el Ubigeo");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El Documento debe tener registrado la fecha.");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario no posea direccion");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@RucApdayc", RucApdayc);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@RucUsuarioDerecho", RucUsuarioDerecho);
                keyValues.Add("@UsuDir", UsuDir);
                keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@ubigeo", Ubigeorazon);

                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }
            return resultado;
        }
        public bool CrearConstanciaAutorizacionCadenas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[11];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string RucApdayc = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string Nombrecomercial = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string Web = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();
            string DireccionUsuario = ReplaceVacio;
            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;
            string FechaMinima = ReplaceVacio;
            string FechaMaxima = ReplaceVacio;
            string Responsable = ReplaceVacio;
            string Responsabledni = ReplaceVacio;
            int daysinmonth = 0;
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var sociocab = new BLSocioNegocio().ObtenerDatos(usuarioderecho.BPS_ID, GlobalVars.Global.OWNER);
            var sociodir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, sociocab.BPS_ID);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);
            //XXXXXXXXXXXX RESPONSABLE XXXXXXXX
            var respon = new BLReporte().AsociadoXSocio(sociocab.BPS_ID);
            decimal responsableID = 0;
            if (respon != null && respon.Count > 0)
            {
                var responsableROll = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().ROL_DESC;
                responsableID = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().BPSA_ID;

            }
            var responsable = new BLSocioNegocio().ObtenerDatos(responsableID, GlobalVars.Global.OWNER);//datos del responsable
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            var periodominimo = new BLReporte().ObtenerPeriodoMinimo(IdLicencia);
            var periodomaximo = new BLReporte().ObtenerPeriodoMaximo(IdLicencia);


            if (responsable != null)
            {
                Responsable = responsable.BPS_FIRST_NAME + ' ' + responsable.BPS_FATH_SURNAME + ' ' + responsable.BPS_MOTH_SURNAME;
                Responsabledni = responsable.TAX_ID;

            }

            #region fecha minima
            if (periodominimo != null)
            {
                string mes = periodominimo.LIC_MONTH.ToString();
                switch (mes)
                {
                    case "1":
                        mes = "Enero";
                        break;
                    case "2":
                        mes = "Febrero";
                        break;
                    case "3":
                        mes = "Marzo";
                        break;
                    case "4":
                        mes = "Abril";
                        break;
                    case "5":
                        mes = "Mayo";
                        break;
                    case "6":
                        mes = "Junio";
                        break;
                    case "7":
                        mes = "Julio";
                        break;
                    case "8":
                        mes = "Agosto";
                        break;
                    case "9":
                        mes = "Setiembre";
                        break;
                    case "10":
                        mes = "Octubre";
                        break;
                    case "11":
                        mes = "Noviembre";
                        break;
                    case "12":
                        mes = "Diciembre";
                        break;
                    default:
                        break;
                }
                FechaMinima = "01 de " + mes + " " + periodominimo.LIC_YEAR.ToString();

            }
            #endregion
            #region fecha maxima
            if (periodomaximo != null)
            {
                string mes = periodomaximo.LIC_MONTH.ToString();
                switch (mes)
                {
                    case "1":
                        mes = "Enero";
                        break;
                    case "2":
                        mes = "Febrero";
                        break;
                    case "3":
                        mes = "Marzo";
                        break;
                    case "4":
                        mes = "Abril";
                        break;
                    case "5":
                        mes = "Mayo";
                        break;
                    case "6":
                        mes = "Junio";
                        break;
                    case "7":
                        mes = "Julio";
                        break;
                    case "8":
                        mes = "Agosto";
                        break;
                    case "9":
                        mes = "Setiembre";
                        break;
                    case "10":
                        mes = "Octubre";
                        break;
                    case "11":
                        mes = "Noviembre";
                        break;
                    case "12":
                        mes = "Diciembre";
                        break;
                    default:
                        break;
                }
                if (mes != "0")
                {
                    daysinmonth = System.DateTime.DaysInMonth(decimal.ToInt32(periodomaximo.LIC_YEAR), decimal.ToInt32(periodomaximo.LIC_MONTH));

                    FechaMaxima = daysinmonth + " de " + mes + " " + periodomaximo.LIC_YEAR.ToString();
                }
            }
            #endregion
            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            RucApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID;
                Nombrecomercial = sociocab.BPS_TRADE_NAME == null ? ReplaceVacio : sociocab.BPS_TRADE_NAME;
                if (sociodir.Count > 0 && sociodir != null)
                    DireccionUsuario = sociodir.First().ADDRESS;
            }

            if (ParametroValue != null)
                Web = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = RucApdayc == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = Fecha == ReplaceVacio ? 0 : 1;
            val[5] = Nombrecomercial == ReplaceVacio ? 0 : 1;
            val[6] = DireccionUsuario == ReplaceVacio ? 0 : 1;
            val[7] = FechaMinima == ReplaceVacio ? 0 : 1;
            val[8] = FechaMaxima == ReplaceVacio ? 0 : 1;
            val[9] = Responsable == ReplaceVacio ? 0 : 1;
            val[10] = Responsabledni == ReplaceVacio ? 0 : 1;


            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Ruc Apdayc");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado el ruc.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El establecimiento debe tener registrado la dirección");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El Documento debe tener registrado la fecha.");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("No posee Nombre comercial");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("No SE AGREGO DIRECCION AL SOCIO");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("No SE AGREGO PLANEAMIENTO ");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("No SE AGREGO PLANEAMIENTO ");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("No SE AGREGO RESPONSABLE ");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("No SE AGREGO RESPONSABLE ");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@RucApdayc", RucApdayc);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@RucUsuarioDerecho", RucUsuarioDerecho);
                keyValues.Add("@LocalDir", LocalDir);
                //keyValues.Add("@Fecha", Fecha);
                keyValues.Add("@Nombrecomercial", Nombrecomercial);
                keyValues.Add("@FechaMinima", FechaMinima);
                keyValues.Add("@FechaMaxima", FechaMaxima);
                keyValues.Add("@Responsable", Responsable);
                keyValues.Add("@Respdni", Responsabledni);

                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }
            return resultado;
        }
        public bool CrearCartillaInformativaCadenas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[5];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string RucApdayc = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string Web = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();

            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            RucApdayc = GlobalVars.Global.RucApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.RucApdayc;

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID;
            }

            if (ParametroValue != null)
                Web = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null && usuarioderecho != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = RucApdayc == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = Fecha == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Ruc Apdayc");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado el ruc.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El establecimiento debe tener registrado la dirección");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("El Documento debe tener registrado la fecha.");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@RucApdayc", RucApdayc);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@RucUsuarioDerecho", RucUsuarioDerecho);
                keyValues.Add("@LocalDir", LocalDir);
                keyValues.Add("@Fecha", Fecha);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }
            return resultado;
        }
        public bool ContratoMensualLocalPermanente(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {

            int[] val = new int[11];
            bool resultado = true;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            var apoderado = new BLReporte().ObtenerDatosApoderadoLegal(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var UsuParZonSed = new BLReporte().ObtenerPartidaZonaSedeUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var representantelegal = new BLReporte().ObtenerRepresentanteLegal(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var Mont = new BLReporte().ObtieneMontoLicencias(IdLicencia);

            SocioNegocio sociocab = new SocioNegocio();
            if (usuarioderecho != null)
                sociocab = new BLSocioNegocio().ObtenerDatos(usuarioderecho.BPS_ID, GlobalVars.Global.OWNER);
            //XXXXXXXXXXXX RESPONSABLE XXXXXXXX
            var respon = new BLReporte().AsociadoXSocio(sociocab.BPS_ID);
            string responsableROll = null;
            decimal responsableID = 0;
            if (respon != null && respon.Count > 0)
            {
                responsableROll = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().ROL_DESC;
                responsableID = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().BPSA_ID;

            }
            var responsable = new BLSocioNegocio().ObtenerDatos(responsableID, GlobalVars.Global.OWNER);//datos del responsable
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            var sociodir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, sociocab.BPS_ID);
            int cantloca = 0;
            var licencia = new BLLicencias().ObtenerLicenciaXCodigo(IdLicencia, GlobalVars.Global.OWNER);
            if (licencia.LIC_MASTER != 0 && licencia != null)
                cantloca = new BLLicencias().ListarLicHijasxPadre(IdLicencia).Count();

            var Fecha = DateTime.Now.ToShortDateString();
            var periodominimo = new BLReporte().ObtenerPeriodoMinimo(IdLicencia);

            string UsuName = ReplaceVacio;
            string UsuRuc = ReplaceVacio;
            string UsuDir = ReplaceVacio;
            string usunombrecomercial = ReplaceVacio;
            string UsuPartida = ReplaceVacio;
            string UsuZona = ReplaceVacio;
            string UsuSede = ReplaceVacio;
            string ApoName = ReplaceVacio;
            string ApoDni = ReplaceVacio;
            string ApoCargo = ReplaceVacio;
            string ApoDirOf = ReplaceVacio;
            string Representante = ReplaceVacio;
            string RepreRoll = ReplaceVacio;
            string RepDni = ReplaceVacio;
            string RepDir = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string distritoRep = ReplaceVacio;
            string provinciaRep = ReplaceVacio;
            string departamentoRep = ReplaceVacio;
            string distritoLoc = ReplaceVacio;
            string provinciaLoc = ReplaceVacio;
            string departamentoLoc = ReplaceVacio;
            string DireccionUsu = ReplaceVacio;
            string cantidadlocales = ReplaceVacio;
            string Responsable = ReplaceVacio;
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString();
            string anio = DateTime.Now.Year.ToString();
            string monto = Mont > 0 ? Mont.ToString("#.##") : ReplaceVacio;
            string montoLetras = Mont > 0 ? Util.NumeroALetras(Mont.ToString()) : ReplaceVacio;
            #region mes
            switch (mes)
            {
                case "1":
                    mes = "Enero";
                    break;
                case "2":
                    mes = "Febrero";
                    break;
                case "3":
                    mes = "Marzo";
                    break;
                case "4":
                    mes = "Abril";
                    break;
                case "5":
                    mes = "Mayo";
                    break;
                case "6":
                    mes = "Junio";
                    break;
                case "7":
                    mes = "Julio";
                    break;
                case "8":
                    mes = "Agosto";
                    break;
                case "9":
                    mes = "Setiembre";
                    break;
                case "10":
                    mes = "Octubre";
                    break;
                case "11":
                    mes = "Noviembre";
                    break;
                case "12":
                    mes = "Diciembre";
                    break;
                default:
                    break;
            }
            #endregion


            if (usuarioderecho != null)
            {
                UsuName = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME.Trim();
                UsuRuc = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID.Trim();
                usunombrecomercial = sociocab.BPS_TRADE_NAME == null ? ReplaceVacio : sociocab.BPS_TRADE_NAME;

                if (sociodir != null && sociodir.Count > 0)
                    DireccionUsu = sociodir[0].ADDRESS;
            }

            //if (UsuParZonSed != null)
            //{
            //    UsuPartida = UsuParZonSed.BPS_PARTIDA == null ? ReplaceVacio : UsuParZonSed.BPS_PARTIDA.Trim();
            //    UsuZona = UsuParZonSed.BPS_ZONA == null ? ReplaceVacio : UsuParZonSed.BPS_ZONA.Trim();
            //    UsuSede = UsuParZonSed.BPS_SEDE == null ? ReplaceVacio : UsuParZonSed.BPS_SEDE.Trim();
            //}

            if (responsable != null)
            {
                Responsable = responsable.BPS_FIRST_NAME + ' ' + responsable.BPS_FATH_SURNAME + ' ' + responsable.BPS_MOTH_SURNAME;
                RepDni = responsable.TAX_ID;
                RepreRoll = responsableROll;
                //ApoDirOf = socioresponsable.ADDRESS == null ? ReplaceVacio : apoderado.ADDRESS.Trim();
            }

            if (representantelegal != null)
            {
                RepDni = representantelegal.TAX_ID == null ? ReplaceVacio : representantelegal.TAX_ID.Trim();
                RepDir = representantelegal.ADDRESS == null ? ReplaceVacio : representantelegal.ADDRESS.Trim();

                if (representantelegal.TIS_N != null && representantelegal.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, representantelegal.TIS_N.ToString(), representantelegal.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoRep = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaRep = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoRep = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLoc = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLoc = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (cantloca > 0)
            {
                cantidadlocales = cantloca.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuName == ReplaceVacio ? 0 : 1;
            val[1] = UsuRuc == ReplaceVacio ? 0 : 1;
            val[2] = DireccionUsu == ReplaceVacio ? 0 : 1;
            val[3] = Responsable == ReplaceVacio ? 0 : 1;
            val[4] = RepDni == ReplaceVacio ? 0 : 1;
            val[5] = RepreRoll == ReplaceVacio ? 0 : 1;
            val[6] = LocalDir == ReplaceVacio ? 0 : 1;
            //val[7] = provinciaRep == ReplaceVacio ? 0 : 1;
            //val[8] = distritoRep == ReplaceVacio ? 0 : 1;
            val[7] = 1;
            //val[7] = cantidadlocales == ReplaceVacio ? 0 : 1;
            val[8] = usunombrecomercial == ReplaceVacio ? 0 : 1;
            val[9] = monto == ReplaceVacio ? 0 : 1;
            val[10] = montoLetras == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("NO SE ENCUENTRA AL SOCIO DE NEGOCIO");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener RUC");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario derecho debe tener DIRECICON PRINCIPAL");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("No se asigno una entidad Responsable al Usuario de derecho");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro el dni de la entidad Responsable al Usuario de derecho");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El responsable no tiene asignado un Roll");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro Direccion en Establecimiento");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Cantidad de locales");
                                break;
                            case 8:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre comercial");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("NO SE SE ENCONTRO EL VALOR DE LA TARIFA | AGREGAR CARACTERISTICAS");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("NO SE SE ENCONTRO EL VALOR DE LA TARIFA EN LETRAS | AGREGAR CARACTERISTICAS");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {

                keyValues.Add("@DireccionApdayc", GlobalVars.Global.DireccionApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.DireccionApdayc);
                //keyValues.Add("@AUTORIDAD", ApoName);
                // keyValues.Add("@DniAutoridad", ApoDni);
                //keyValues.Add("@CARGO", ApoCargo);
                // keyValues.Add("@DireccionOf", ApoDirOf);
                keyValues.Add("@Usuario", UsuName);
                keyValues.Add("@Usuruc", UsuRuc);
                keyValues.Add("@UsuDir", UsuDir);

                keyValues.Add("@Representante", Responsable);
                keyValues.Add("@Repredni", RepDni);
                //keyValues.Add("@RepreDirec", RepDir + " " + departamentoRep + " " + provinciaRep + " " + distritoRep);
                keyValues.Add("@LOCAL", LocalName);
                //keyValues.Add("@LOCDIRECC", LocalDir + " " + departamentoRep + " " + provinciaRep + " " + distritoRep);
                keyValues.Add("@LOCDIRECC", LocalDir);
                // keyValues.Add("@Partreg", UsuPartida);
                // keyValues.Add("@Zonreg", UsuZona);
                keyValues.Add("@DireccionUsu", DireccionUsu);
                keyValues.Add("@cantloc", cantidadlocales);
                keyValues.Add("@dia", dia);
                keyValues.Add("@mes", mes);
                keyValues.Add("@anio", anio);
                keyValues.Add("@usunombrecomercial", usunombrecomercial);
                keyValues.Add("@RepreRoll", RepreRoll);
                keyValues.Add("@Monto", monto);
                keyValues.Add("@LetrasMonto", montoLetras);

                //keyValues.Add("@TARIFA", "");
                var imagen = GenerarQR(IdLicencia);
                FindandReplaceWithImage(destinationFile, keyValues, imagen,5);
                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }
            return resultado;
        }

        public bool ContratoMensualLocalesPermanente(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {

            int[] val = new int[11];
            bool resultado = true;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            var apoderado = new BLReporte().ObtenerDatosApoderadoLegal(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var UsuParZonSed = new BLReporte().ObtenerPartidaZonaSedeUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var representantelegal = new BLReporte().ObtenerRepresentanteLegal(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);

            //var sociocab = new BLSocioNegocio().ObtenerDatos(usuarioderecho.BPS_ID, GlobalVars.Global.OWNER);
            SocioNegocio sociocab = new SocioNegocio();
            if (usuarioderecho != null)
                sociocab = new BLSocioNegocio().ObtenerDatos(usuarioderecho.BPS_ID, GlobalVars.Global.OWNER);


            //XXXXXXXXXXXX RESPONSABLE XXXXXXXX
            var respon = new BLReporte().AsociadoXSocio(sociocab.BPS_ID);
            string responsableROll = null;
            decimal responsableID = 0;
            if (respon != null && respon.Count > 0)
            {
                responsableROll = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().ROL_DESC;
                responsableID = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().BPSA_ID;

            }
            var responsable = new BLSocioNegocio().ObtenerDatos(responsableID, GlobalVars.Global.OWNER);//datos del responsable
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            var sociodir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, sociocab.BPS_ID);
            int cantloca = 0;
            var licencia = new BLLicencias().ObtenerLicenciaXCodigo(IdLicencia, GlobalVars.Global.OWNER);
            if (licencia.LIC_MASTER != 0 && licencia != null)
                cantloca = new BLLicencias().ListarLicHijasxPadre(IdLicencia).Count();
            var Fecha = DateTime.Now.ToShortDateString();
            var periodominimo = new BLReporte().ObtenerPeriodoMinimo(IdLicencia);
            var Mont = new BLReporte().ObtieneMontoLicencias(IdLicencia);
            var AgenciasLima = new BLReporte().ObtienAgenciasLicencia(IdLicencia,1);    //Lima
            var AgenciasProvincia = new BLReporte().ObtienAgenciasLicencia(IdLicencia, 2); // provincia

            string UsuName = ReplaceVacio;
            string UsuRuc = ReplaceVacio;
            string UsuDir = ReplaceVacio;
            string usunombrecomercial = ReplaceVacio;
            string UsuPartida = ReplaceVacio;
            string UsuZona = ReplaceVacio;
            string UsuSede = ReplaceVacio;
            string ApoName = ReplaceVacio;
            string ApoDni = ReplaceVacio;
            string ApoCargo = ReplaceVacio;
            string ApoDirOf = ReplaceVacio;
            string Representante = ReplaceVacio;
            string RepreRoll = ReplaceVacio;
            string RepDni = ReplaceVacio;
            string RepDir = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string distritoRep = ReplaceVacio;
            string provinciaRep = ReplaceVacio;
            string departamentoRep = ReplaceVacio;
            string distritoLoc = ReplaceVacio;
            string provinciaLoc = ReplaceVacio;
            string departamentoLoc = ReplaceVacio;
            string DireccionUsu = ReplaceVacio;
            string cantidadlocales = ReplaceVacio;
            string Responsable = ReplaceVacio;
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString();
            string anio = DateTime.Now.Year.ToString();
            string monto =  Mont > 0 ? (Mont).ToString("#.##") :  ReplaceVacio;
            string montoLetras = Mont > 0 ? Util.NumeroALetras((Mont).ToString()) : ReplaceVacio;

            #region mes
            switch (mes)
            {
                case "1":
                    mes = "Enero";
                    break;
                case "2":
                    mes = "Febrero";
                    break;
                case "3":
                    mes = "Marzo";
                    break;
                case "4":
                    mes = "Abril";
                    break;
                case "5":
                    mes = "Mayo";
                    break;
                case "6":
                    mes = "Junio";
                    break;
                case "7":
                    mes = "Julio";
                    break;
                case "8":
                    mes = "Agosto";
                    break;
                case "9":
                    mes = "Setiembre";
                    break;
                case "10":
                    mes = "Octubre";
                    break;
                case "11":
                    mes = "Noviembre";
                    break;
                case "12":
                    mes = "Diciembre";
                    break;
                default:
                    break;
            }
            #endregion


            if (usuarioderecho != null)
            {
                UsuName = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME.Trim();
                UsuRuc = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID.Trim();
                usunombrecomercial = sociocab.BPS_TRADE_NAME == null ? ReplaceVacio : sociocab.BPS_TRADE_NAME;

                if (sociodir != null && sociodir.Count > 0)
                    DireccionUsu = sociodir[0].ADDRESS;
            }

            //if (UsuParZonSed != null)
            //{
            //    UsuPartida = UsuParZonSed.BPS_PARTIDA == null ? ReplaceVacio : UsuParZonSed.BPS_PARTIDA.Trim();
            //    UsuZona = UsuParZonSed.BPS_ZONA == null ? ReplaceVacio : UsuParZonSed.BPS_ZONA.Trim();
            //    UsuSede = UsuParZonSed.BPS_SEDE == null ? ReplaceVacio : UsuParZonSed.BPS_SEDE.Trim();
            //}

            if (responsable != null)
            {
                Responsable = responsable.BPS_FIRST_NAME + ' ' + responsable.BPS_FATH_SURNAME + ' ' + responsable.BPS_MOTH_SURNAME;
                RepDni = responsable.TAX_ID;
                RepreRoll = responsableROll;
                //ApoDirOf = socioresponsable.ADDRESS == null ? ReplaceVacio : apoderado.ADDRESS.Trim();
            }

            if (representantelegal != null)
            {
                RepDni = representantelegal.TAX_ID == null ? ReplaceVacio : representantelegal.TAX_ID.Trim();
                RepDir = representantelegal.ADDRESS == null ? ReplaceVacio : representantelegal.ADDRESS.Trim();

                if (representantelegal.TIS_N != null && representantelegal.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, representantelegal.TIS_N.ToString(), representantelegal.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoRep = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaRep = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoRep = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLoc = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLoc = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (cantloca > 0)
            {
                cantidadlocales = cantloca.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuName == ReplaceVacio ? 0 : 1;
            val[1] = UsuRuc == ReplaceVacio ? 0 : 1;
            val[2] = DireccionUsu == ReplaceVacio ? 0 : 1;
            val[3] = Responsable == ReplaceVacio ? 0 : 1;
            val[4] = RepDni == ReplaceVacio ? 0 : 1;
            val[5] = RepreRoll == ReplaceVacio ? 0 : 1;
            val[6] = LocalDir == ReplaceVacio ? 0 : 1;
            //val[7] = provinciaRep == ReplaceVacio ? 0 : 1;
            //val[8] = distritoRep == ReplaceVacio ? 0 : 1;
            val[7] = cantidadlocales == ReplaceVacio ? 0 : 1;
            val[8] = usunombrecomercial == ReplaceVacio ? 0 : 1;
            val[9] = monto == ReplaceVacio ? 0 : 1;
            val[10] = montoLetras == ReplaceVacio ? 0 : 1;
            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("NO SE ENCUENTRA AL SOCIO DE NEGOCIO");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener RUC");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario derecho debe tener DIRECICON PRINCIPAL");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("No se asigno una entidad Responsable al Usuario de derecho");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro el dni de la entidad Responsable al Usuario de derecho");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El responsable no tiene asignado un Roll");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro Direccion en Establecimiento");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Cantidad de locales");
                                break;
                            case 8:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre comercial");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;

                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("NO SE SE ENCONTRO EL VALOR DE LA TARIFA | AGREGAR CARACTERISTICAS");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("NO SE SE ENCONTRO EL VALOR DE LA TARIFA EN LETRAS | AGREGAR CARACTERISTICAS");
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {

                keyValues.Add("@DireccionApdayc", GlobalVars.Global.DireccionApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.DireccionApdayc);
                //keyValues.Add("@AUTORIDAD", ApoName);
                // keyValues.Add("@DniAutoridad", ApoDni);
                //keyValues.Add("@CARGO", ApoCargo);
                // keyValues.Add("@DireccionOf", ApoDirOf);
                keyValues.Add("@Usuario", UsuName);
                keyValues.Add("@Usuruc", UsuRuc);
                keyValues.Add("@UsuDir", UsuDir);

                keyValues.Add("@Representante", Responsable);
                keyValues.Add("@Repredni", RepDni);
                //keyValues.Add("@RepreDirec", RepDir + " " + departamentoRep + " " + provinciaRep + " " + distritoRep);
                keyValues.Add("@LOCAL", LocalName);
                //keyValues.Add("@LOCDIRECC", LocalDir + " " + departamentoRep + " " + provinciaRep + " " + distritoRep);
                keyValues.Add("@LOCDIRECC", LocalDir);
                // keyValues.Add("@Partreg", UsuPartida);
                // keyValues.Add("@Zonreg", UsuZona);
                keyValues.Add("@DireccionUsu", DireccionUsu);
                keyValues.Add("@cantloc", cantidadlocales);
                keyValues.Add("@dia", dia);
                keyValues.Add("@mes", mes);
                keyValues.Add("@anio", anio);
                keyValues.Add("@usunombrecomercial", usunombrecomercial);
                keyValues.Add("@RepreRoll", RepreRoll);
                keyValues.Add("@Monto", monto);
                keyValues.Add("@LetrasMonto", montoLetras);
                keyValues.Add("@LimaAgencias", AgenciasLima);
                keyValues.Add("@ProvinciasAgencia", AgenciasProvincia);

                //keyValues.Add("@TARIFA", "");
                var imagen = GenerarQR(IdLicencia);
                FindandReplaceWithImage(destinationFile, keyValues, imagen, 5);
                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }
            return resultado;
        }
        public bool ContratoSemestralLocalPermanente(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {

            int[] val = new int[11];
            bool resultado = true;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            var apoderado = new BLReporte().ObtenerDatosApoderadoLegal(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var UsuParZonSed = new BLReporte().ObtenerPartidaZonaSedeUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var representantelegal = new BLReporte().ObtenerRepresentanteLegal(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var Mont = new BLReporte().ObtieneMontoLicencias(IdLicencia);
            //var sociocab = new BLSocioNegocio().ObtenerDatos(usuarioderecho.BPS_ID, GlobalVars.Global.OWNER);

            SocioNegocio sociocab = new SocioNegocio();
            if (usuarioderecho != null)
                sociocab = new BLSocioNegocio().ObtenerDatos(usuarioderecho.BPS_ID, GlobalVars.Global.OWNER);


            //XXXXXXXXXXXX RESPONSABLE XXXXXXXX
            var respon = new BLReporte().AsociadoXSocio(sociocab.BPS_ID);
            string responsableROll = null;
            decimal responsableID = 0;
            if (respon != null && respon.Count > 0)
            {
                responsableROll = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().ROL_DESC;
                responsableID = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().BPSA_ID;

            }
            var responsable = new BLSocioNegocio().ObtenerDatos(responsableID, GlobalVars.Global.OWNER);//datos del responsable
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            var sociodir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, sociocab.BPS_ID);
            int cantloca = 0;
            var licencia = new BLLicencias().ObtenerLicenciaXCodigo(IdLicencia, GlobalVars.Global.OWNER);
            if (licencia.LIC_MASTER != 0 && licencia != null)
                cantloca = new BLLicencias().ListarLicHijasxPadre(IdLicencia).Count();
            var Fecha = DateTime.Now.ToShortDateString();
            var periodominimo = new BLReporte().ObtenerPeriodoMinimo(IdLicencia);

            string UsuName = ReplaceVacio;
            string UsuRuc = ReplaceVacio;
            string UsuDir = ReplaceVacio;
            string usunombrecomercial = ReplaceVacio;
            string UsuPartida = ReplaceVacio;
            string UsuZona = ReplaceVacio;
            string UsuSede = ReplaceVacio;
            string ApoName = ReplaceVacio;
            string ApoDni = ReplaceVacio;
            string ApoCargo = ReplaceVacio;
            string ApoDirOf = ReplaceVacio;
            string Representante = ReplaceVacio;
            string RepreRoll = ReplaceVacio;
            string RepDni = ReplaceVacio;
            string RepDir = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string distritoRep = ReplaceVacio;
            string provinciaRep = ReplaceVacio;
            string departamentoRep = ReplaceVacio;
            string distritoLoc = ReplaceVacio;
            string provinciaLoc = ReplaceVacio;
            string departamentoLoc = ReplaceVacio;
            string DireccionUsu = ReplaceVacio;
            string cantidadlocales = ReplaceVacio;
            string Responsable = ReplaceVacio;
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString();
            string anio = DateTime.Now.Year.ToString();
            string monto = Mont > 0 ? (Mont * 6).ToString("#.##") : ReplaceVacio;
            string montoLetras = Mont > 0 ? Util.NumeroALetras((Mont * 6).ToString()) : ReplaceVacio;
            #region mes
            switch (mes)
            {
                case "1":
                    mes = "Enero";
                    break;
                case "2":
                    mes = "Febrero";
                    break;
                case "3":
                    mes = "Marzo";
                    break;
                case "4":
                    mes = "Abril";
                    break;
                case "5":
                    mes = "Mayo";
                    break;
                case "6":
                    mes = "Junio";
                    break;
                case "7":
                    mes = "Julio";
                    break;
                case "8":
                    mes = "Agosto";
                    break;
                case "9":
                    mes = "Setiembre";
                    break;
                case "10":
                    mes = "Octubre";
                    break;
                case "11":
                    mes = "Noviembre";
                    break;
                case "12":
                    mes = "Diciembre";
                    break;
                default:
                    break;
            }
            #endregion


            if (usuarioderecho != null)
            {
                UsuName = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME.Trim();
                UsuRuc = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID.Trim();
                usunombrecomercial = sociocab.BPS_TRADE_NAME == null ? ReplaceVacio : sociocab.BPS_TRADE_NAME;

                if (sociodir != null && sociodir.Count > 0)
                    DireccionUsu = sociodir[0].ADDRESS;
            }

            //if (UsuParZonSed != null)
            //{
            //    UsuPartida = UsuParZonSed.BPS_PARTIDA == null ? ReplaceVacio : UsuParZonSed.BPS_PARTIDA.Trim();
            //    UsuZona = UsuParZonSed.BPS_ZONA == null ? ReplaceVacio : UsuParZonSed.BPS_ZONA.Trim();
            //    UsuSede = UsuParZonSed.BPS_SEDE == null ? ReplaceVacio : UsuParZonSed.BPS_SEDE.Trim();
            //}

            if (responsable != null)
            {
                Responsable = responsable.BPS_FIRST_NAME + ' ' + responsable.BPS_FATH_SURNAME + ' ' + responsable.BPS_MOTH_SURNAME;
                RepDni = responsable.TAX_ID;
                RepreRoll = responsableROll;
                //ApoDirOf = socioresponsable.ADDRESS == null ? ReplaceVacio : apoderado.ADDRESS.Trim();
            }

            if (representantelegal != null)
            {
                RepDni = representantelegal.TAX_ID == null ? ReplaceVacio : representantelegal.TAX_ID.Trim();
                RepDir = representantelegal.ADDRESS == null ? ReplaceVacio : representantelegal.ADDRESS.Trim();

                if (representantelegal.TIS_N != null && representantelegal.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, representantelegal.TIS_N.ToString(), representantelegal.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoRep = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaRep = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoRep = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLoc = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLoc = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (cantloca > 0)
            {
                cantidadlocales = cantloca.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuName == ReplaceVacio ? 0 : 1;
            val[1] = UsuRuc == ReplaceVacio ? 0 : 1;
            val[2] = DireccionUsu == ReplaceVacio ? 0 : 1;
            val[3] = Responsable == ReplaceVacio ? 0 : 1;
            val[4] = RepDni == ReplaceVacio ? 0 : 1;
            val[5] = RepreRoll == ReplaceVacio ? 0 : 1;
            val[6] = LocalDir == ReplaceVacio ? 0 : 1;
            //val[7] = provinciaRep == ReplaceVacio ? 0 : 1;
            //val[8] = distritoRep == ReplaceVacio ? 0 : 1;
            val[7] = 1;
            val[8] = usunombrecomercial == ReplaceVacio ? 0 : 1;
            val[9] = monto == ReplaceVacio ? 0 : 1;
            val[10] = montoLetras == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("NO SE ENCUENTRA AL SOCIO DE NEGOCIO");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener RUC");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario derecho debe tener DIRECICON PRINCIPAL");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("No se asigno una entidad Responsable al Usuario de derecho");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro el dni de la entidad Responsable al Usuario de derecho");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El responsable no tiene asignado un Roll");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro Direccion en Establecimiento");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Cantidad de locales");
                                break;
                            case 8:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre comercial");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("NO SE SE ENCONTRO EL VALOR DE LA TARIFA | AGREGAR CARACTERISTICAS");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("NO SE SE ENCONTRO EL VALOR DE LA TARIFA EN LETRAS | AGREGAR CARACTERISTICAS");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {

                keyValues.Add("@DireccionApdayc", GlobalVars.Global.DireccionApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.DireccionApdayc);
                //keyValues.Add("@AUTORIDAD", ApoName);
                // keyValues.Add("@DniAutoridad", ApoDni);
                //keyValues.Add("@CARGO", ApoCargo);
                // keyValues.Add("@DireccionOf", ApoDirOf);
                keyValues.Add("@Usuario", UsuName);
                keyValues.Add("@Usuruc", UsuRuc);
                keyValues.Add("@UsuDir", UsuDir);

                keyValues.Add("@Representante", Responsable);
                keyValues.Add("@Repredni", RepDni);
                //keyValues.Add("@RepreDirec", RepDir + " " + departamentoRep + " " + provinciaRep + " " + distritoRep);
                keyValues.Add("@LOCAL", LocalName);
                //keyValues.Add("@LOCDIRECC", LocalDir + " " + departamentoRep + " " + provinciaRep + " " + distritoRep);
                keyValues.Add("@LOCDIRECC", LocalDir);
                // keyValues.Add("@Partreg", UsuPartida);
                // keyValues.Add("@Zonreg", UsuZona);
                keyValues.Add("@DireccionUsu", DireccionUsu);
                keyValues.Add("@cantloc", cantidadlocales);
                keyValues.Add("@dia", dia);
                keyValues.Add("@mes", mes);
                keyValues.Add("@anio", anio);
                keyValues.Add("@usunombrecomercial", usunombrecomercial);
                keyValues.Add("@RepreRoll", RepreRoll);

                //keyValues.Add("@TARIFA", "");
                keyValues.Add("@Monto", monto);
                keyValues.Add("@LetrasMonto", montoLetras);

                //keyValues.Add("@TARIFA", "");
                var imagen = GenerarQR(IdLicencia);
                FindandReplaceWithImage(destinationFile, keyValues, imagen, 5);
                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }
            return resultado;
        }
        public bool ContratoSemestralLocalesPermanente(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {

            int[] val = new int[11];
            bool resultado = true;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            var apoderado = new BLReporte().ObtenerDatosApoderadoLegal(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var UsuParZonSed = new BLReporte().ObtenerPartidaZonaSedeUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var representantelegal = new BLReporte().ObtenerRepresentanteLegal(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var Mont = new BLReporte().ObtieneMontoLicencias(IdLicencia);

            //var sociocab = new BLSocioNegocio().ObtenerDatos(usuarioderecho.BPS_ID, GlobalVars.Global.OWNER);
            SocioNegocio sociocab = new SocioNegocio();
            if (usuarioderecho != null)
                sociocab = new BLSocioNegocio().ObtenerDatos(usuarioderecho.BPS_ID, GlobalVars.Global.OWNER);


            //XXXXXXXXXXXX RESPONSABLE XXXXXXXX
            var respon = new BLReporte().AsociadoXSocio(sociocab.BPS_ID);
            string responsableROll = null;
            decimal responsableID = 0;
            if (respon != null && respon.Count > 0)
            {
                responsableROll = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().ROL_DESC;
                responsableID = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().BPSA_ID;

            }
            var responsable = new BLSocioNegocio().ObtenerDatos(responsableID, GlobalVars.Global.OWNER);//datos del responsable
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            var sociodir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, sociocab.BPS_ID);
            int cantloca = 0;
            var licencia = new BLLicencias().ObtenerLicenciaXCodigo(IdLicencia, GlobalVars.Global.OWNER);
            if (licencia.LIC_MASTER != 0 && licencia != null)
                cantloca = new BLLicencias().ListarLicHijasxPadre(IdLicencia).Count();
            var Fecha = DateTime.Now.ToShortDateString();
            var periodominimo = new BLReporte().ObtenerPeriodoMinimo(IdLicencia);

            string UsuName = ReplaceVacio;
            string UsuRuc = ReplaceVacio;
            string UsuDir = ReplaceVacio;
            string usunombrecomercial = ReplaceVacio;
            string UsuPartida = ReplaceVacio;
            string UsuZona = ReplaceVacio;
            string UsuSede = ReplaceVacio;
            string ApoName = ReplaceVacio;
            string ApoDni = ReplaceVacio;
            string ApoCargo = ReplaceVacio;
            string ApoDirOf = ReplaceVacio;
            string Representante = ReplaceVacio;
            string RepreRoll = ReplaceVacio;
            string RepDni = ReplaceVacio;
            string RepDir = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string distritoRep = ReplaceVacio;
            string provinciaRep = ReplaceVacio;
            string departamentoRep = ReplaceVacio;
            string distritoLoc = ReplaceVacio;
            string provinciaLoc = ReplaceVacio;
            string departamentoLoc = ReplaceVacio;
            string DireccionUsu = ReplaceVacio;
            string cantidadlocales = ReplaceVacio;
            string Responsable = ReplaceVacio;
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString();
            string anio = DateTime.Now.Year.ToString();
            string monto = Mont > 0 ? (Mont *6).ToString("#.##") : ReplaceVacio;
            string montoLetras = Mont > 0 ? Util.NumeroALetras((Mont *6).ToString()) : ReplaceVacio;
            #region mes
            switch (mes)
            {
                case "1":
                    mes = "Enero";
                    break;
                case "2":
                    mes = "Febrero";
                    break;
                case "3":
                    mes = "Marzo";
                    break;
                case "4":
                    mes = "Abril";
                    break;
                case "5":
                    mes = "Mayo";
                    break;
                case "6":
                    mes = "Junio";
                    break;
                case "7":
                    mes = "Julio";
                    break;
                case "8":
                    mes = "Agosto";
                    break;
                case "9":
                    mes = "Setiembre";
                    break;
                case "10":
                    mes = "Octubre";
                    break;
                case "11":
                    mes = "Noviembre";
                    break;
                case "12":
                    mes = "Diciembre";
                    break;
                default:
                    break;
            }
            #endregion


            if (usuarioderecho != null)
            {
                UsuName = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME.Trim();
                UsuRuc = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID.Trim();
                usunombrecomercial = sociocab.BPS_TRADE_NAME == null ? ReplaceVacio : sociocab.BPS_TRADE_NAME;

                if (sociodir != null && sociodir.Count > 0)
                    DireccionUsu = sociodir[0].ADDRESS;
            }

            //if (UsuParZonSed != null)
            //{
            //    UsuPartida = UsuParZonSed.BPS_PARTIDA == null ? ReplaceVacio : UsuParZonSed.BPS_PARTIDA.Trim();
            //    UsuZona = UsuParZonSed.BPS_ZONA == null ? ReplaceVacio : UsuParZonSed.BPS_ZONA.Trim();
            //    UsuSede = UsuParZonSed.BPS_SEDE == null ? ReplaceVacio : UsuParZonSed.BPS_SEDE.Trim();
            //}

            if (responsable != null)
            {
                Responsable = responsable.BPS_FIRST_NAME + ' ' + responsable.BPS_FATH_SURNAME + ' ' + responsable.BPS_MOTH_SURNAME;
                RepDni = responsable.TAX_ID;
                RepreRoll = responsableROll;
                //ApoDirOf = socioresponsable.ADDRESS == null ? ReplaceVacio : apoderado.ADDRESS.Trim();
            }

            if (representantelegal != null)
            {
                RepDni = representantelegal.TAX_ID == null ? ReplaceVacio : representantelegal.TAX_ID.Trim();
                RepDir = representantelegal.ADDRESS == null ? ReplaceVacio : representantelegal.ADDRESS.Trim();

                if (representantelegal.TIS_N != null && representantelegal.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, representantelegal.TIS_N.ToString(), representantelegal.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoRep = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaRep = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoRep = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLoc = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLoc = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (cantloca > 0)
            {
                cantidadlocales = cantloca.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuName == ReplaceVacio ? 0 : 1;
            val[1] = UsuRuc == ReplaceVacio ? 0 : 1;
            val[2] = DireccionUsu == ReplaceVacio ? 0 : 1;
            val[3] = Responsable == ReplaceVacio ? 0 : 1;
            val[4] = RepDni == ReplaceVacio ? 0 : 1;
            val[5] = RepreRoll == ReplaceVacio ? 0 : 1;
            val[6] = LocalDir == ReplaceVacio ? 0 : 1;
            //val[7] = provinciaRep == ReplaceVacio ? 0 : 1;
            //val[8] = distritoRep == ReplaceVacio ? 0 : 1;
            val[7] = cantidadlocales == ReplaceVacio ? 0 : 1;
            val[8] = usunombrecomercial == ReplaceVacio ? 0 : 1;
            val[9] = monto == ReplaceVacio ? 0 : 1;
            val[10] = montoLetras == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("NO SE ENCUENTRA AL SOCIO DE NEGOCIO");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener RUC");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario derecho debe tener DIRECICON PRINCIPAL");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("No se asigno una entidad Responsable al Usuario de derecho");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro el dni de la entidad Responsable al Usuario de derecho");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El responsable no tiene asignado un Roll");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro Direccion en Establecimiento");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Cantidad de locales");
                                break;
                            case 8:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre comercial");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("NO SE SE ENCONTRO EL VALOR DE LA TARIFA | AGREGAR CARACTERISTICAS");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("NO SE SE ENCONTRO EL VALOR DE LA TARIFA EN LETRAS | AGREGAR CARACTERISTICAS");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {

                keyValues.Add("@DireccionApdayc", GlobalVars.Global.DireccionApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.DireccionApdayc);
                //keyValues.Add("@AUTORIDAD", ApoName);
                // keyValues.Add("@DniAutoridad", ApoDni);
                //keyValues.Add("@CARGO", ApoCargo);
                // keyValues.Add("@DireccionOf", ApoDirOf);
                keyValues.Add("@Usuario", UsuName);
                keyValues.Add("@Usuruc", UsuRuc);
                keyValues.Add("@UsuDir", UsuDir);

                keyValues.Add("@Representante", Responsable);
                keyValues.Add("@Repredni", RepDni);
                //keyValues.Add("@RepreDirec", RepDir + " " + departamentoRep + " " + provinciaRep + " " + distritoRep);
                keyValues.Add("@LOCAL", LocalName);
                //keyValues.Add("@LOCDIRECC", LocalDir + " " + departamentoRep + " " + provinciaRep + " " + distritoRep);
                keyValues.Add("@LOCDIRECC", LocalDir);
                // keyValues.Add("@Partreg", UsuPartida);
                // keyValues.Add("@Zonreg", UsuZona);
                keyValues.Add("@DireccionUsu", DireccionUsu);
                keyValues.Add("@cantloc", cantidadlocales);
                keyValues.Add("@dia", dia);
                keyValues.Add("@mes", mes);
                keyValues.Add("@anio", anio);
                keyValues.Add("@usunombrecomercial", usunombrecomercial);
                keyValues.Add("@RepreRoll", RepreRoll);
                keyValues.Add("@Monto", monto);
                keyValues.Add("@LetrasMonto", montoLetras);

                //keyValues.Add("@TARIFA", "");
                //keyValues.Add("@TARIFA", "");
                var imagen = GenerarQR(IdLicencia);
                FindandReplaceWithImage(destinationFile, keyValues, imagen, 5);
                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }
            return resultado;
        }
        public bool ContratoAnualLocalesPermanente(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[11];
            bool resultado = true;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            var apoderado = new BLReporte().ObtenerDatosApoderadoLegal(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var UsuParZonSed = new BLReporte().ObtenerPartidaZonaSedeUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var representantelegal = new BLReporte().ObtenerRepresentanteLegal(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);

            //var sociocab = new BLSocioNegocio().ObtenerDatos(usuarioderecho.BPS_ID, GlobalVars.Global.OWNER);
            SocioNegocio sociocab = new SocioNegocio();
            if (usuarioderecho != null)
                sociocab = new BLSocioNegocio().ObtenerDatos(usuarioderecho.BPS_ID, GlobalVars.Global.OWNER);


            //XXXXXXXXXXXX RESPONSABLE XXXXXXXX
            var respon = new BLReporte().AsociadoXSocio(sociocab.BPS_ID);
            string responsableROll = null;
            decimal responsableID = 0;
            if (respon != null && respon.Count > 0)
            {
                responsableROll = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().ROL_DESC;
                responsableID = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().BPSA_ID;

            }
            var responsable = new BLSocioNegocio().ObtenerDatos(responsableID, GlobalVars.Global.OWNER);//datos del responsable
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            var sociodir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, sociocab.BPS_ID);
            int cantloca = 0;
            var licencia = new BLLicencias().ObtenerLicenciaXCodigo(IdLicencia, GlobalVars.Global.OWNER);
            if (licencia.LIC_MASTER != 0 && licencia != null)
                cantloca = new BLLicencias().ListarLicHijasxPadre(IdLicencia).Count();
            var Fecha = DateTime.Now.ToShortDateString();
            var periodominimo = new BLReporte().ObtenerPeriodoMinimo(IdLicencia);
            var Mont = new BLReporte().ObtieneMontoLicencias(IdLicencia);
            var AgenciasLima = new BLReporte().ObtienAgenciasLicencia(IdLicencia, 1);    //Lima
            var AgenciasProvincia = new BLReporte().ObtienAgenciasLicencia(IdLicencia, 2); // provincia

            string UsuName = ReplaceVacio;
            string UsuRuc = ReplaceVacio;
            string UsuDir = ReplaceVacio;
            string usunombrecomercial = ReplaceVacio;
            string UsuPartida = ReplaceVacio;
            string UsuZona = ReplaceVacio;
            string UsuSede = ReplaceVacio;
            string ApoName = ReplaceVacio;
            string ApoDni = ReplaceVacio;
            string ApoCargo = ReplaceVacio;
            string ApoDirOf = ReplaceVacio;
            string Representante = ReplaceVacio;
            string RepreRoll = ReplaceVacio;
            string RepDni = ReplaceVacio;
            string RepDir = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string distritoRep = ReplaceVacio;
            string provinciaRep = ReplaceVacio;
            string departamentoRep = ReplaceVacio;
            string distritoLoc = ReplaceVacio;
            string provinciaLoc = ReplaceVacio;
            string departamentoLoc = ReplaceVacio;
            string DireccionUsu = ReplaceVacio;
            string cantidadlocales = ReplaceVacio;
            string Responsable = ReplaceVacio;
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString();
            string anio = DateTime.Now.Year.ToString();
            string monto = Mont > 0 ? (Mont *12).ToString("#.##") : ReplaceVacio;
            string montoLetras = Mont > 0 ? Util.NumeroALetras((Mont*12).ToString()) : ReplaceVacio;

            #region mes
            switch (mes)
            {
                case "1":
                    mes = "Enero";
                    break;
                case "2":
                    mes = "Febrero";
                    break;
                case "3":
                    mes = "Marzo";
                    break;
                case "4":
                    mes = "Abril";
                    break;
                case "5":
                    mes = "Mayo";
                    break;
                case "6":
                    mes = "Junio";
                    break;
                case "7":
                    mes = "Julio";
                    break;
                case "8":
                    mes = "Agosto";
                    break;
                case "9":
                    mes = "Setiembre";
                    break;
                case "10":
                    mes = "Octubre";
                    break;
                case "11":
                    mes = "Noviembre";
                    break;
                case "12":
                    mes = "Diciembre";
                    break;
                default:
                    break;
            }
            #endregion


            if (usuarioderecho != null)
            {
                UsuName = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME.Trim();
                UsuRuc = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID.Trim();
                usunombrecomercial = sociocab.BPS_TRADE_NAME == null ? ReplaceVacio : sociocab.BPS_TRADE_NAME;

                if (sociodir != null && sociodir.Count > 0)
                    DireccionUsu = sociodir[0].ADDRESS;
            }

            //if (UsuParZonSed != null)
            //{
            //    UsuPartida = UsuParZonSed.BPS_PARTIDA == null ? ReplaceVacio : UsuParZonSed.BPS_PARTIDA.Trim();
            //    UsuZona = UsuParZonSed.BPS_ZONA == null ? ReplaceVacio : UsuParZonSed.BPS_ZONA.Trim();
            //    UsuSede = UsuParZonSed.BPS_SEDE == null ? ReplaceVacio : UsuParZonSed.BPS_SEDE.Trim();
            //}

            if (responsable != null)
            {
                Responsable = responsable.BPS_FIRST_NAME + ' ' + responsable.BPS_FATH_SURNAME + ' ' + responsable.BPS_MOTH_SURNAME;
                RepDni = responsable.TAX_ID;
                RepreRoll = responsableROll;
                //ApoDirOf = socioresponsable.ADDRESS == null ? ReplaceVacio : apoderado.ADDRESS.Trim();
            }

            if (representantelegal != null)
            {
                RepDni = representantelegal.TAX_ID == null ? ReplaceVacio : representantelegal.TAX_ID.Trim();
                RepDir = representantelegal.ADDRESS == null ? ReplaceVacio : representantelegal.ADDRESS.Trim();

                if (representantelegal.TIS_N != null && representantelegal.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, representantelegal.TIS_N.ToString(), representantelegal.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoRep = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaRep = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoRep = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLoc = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLoc = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (cantloca > 0)
            {
                cantidadlocales = cantloca.ToString();
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuName == ReplaceVacio ? 0 : 1;
            val[1] = UsuRuc == ReplaceVacio ? 0 : 1;
            val[2] = DireccionUsu == ReplaceVacio ? 0 : 1;
            val[3] = Responsable == ReplaceVacio ? 0 : 1;
            val[4] = RepDni == ReplaceVacio ? 0 : 1;
            val[5] = RepreRoll == ReplaceVacio ? 0 : 1;
            val[6] = LocalDir == ReplaceVacio ? 0 : 1;
            //val[7] = provinciaRep == ReplaceVacio ? 0 : 1;
            //val[8] = distritoRep == ReplaceVacio ? 0 : 1;
            val[7] = cantidadlocales == ReplaceVacio ? 0 : 1;
            val[8] = usunombrecomercial == ReplaceVacio ? 0 : 1;
            val[9] = monto == ReplaceVacio ? 0 : 1;
            val[10] = montoLetras == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("NO SE ENCUENTRA AL SOCIO DE NEGOCIO");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener RUC");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario derecho debe tener DIRECICON PRINCIPAL");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("No se asigno una entidad Responsable al Usuario de derecho");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro el dni de la entidad Responsable al Usuario de derecho");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("El responsable no tiene asignado un Roll");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro Direccion en Establecimiento");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Cantidad de locales");
                                break;
                            case 8:
                                //GlobalVars.Global.ListMessageReport.Add("Nombre comercial");
                                GlobalVars.Global.ListMessageReport.Add("Completar datos en el socio o Marcar una dirección como principal.");
                                break;

                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("NO SE SE ENCONTRO EL VALOR DE LA TARIFA | AGREGAR CARACTERISTICAS");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("NO SE SE ENCONTRO EL VALOR DE LA TARIFA EN LETRAS | AGREGAR CARACTERISTICAS");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {

                keyValues.Add("@DireccionApdayc", GlobalVars.Global.DireccionApdayc == string.Empty ? ReplaceVacio : GlobalVars.Global.DireccionApdayc);
                //keyValues.Add("@AUTORIDAD", ApoName);
                // keyValues.Add("@DniAutoridad", ApoDni);
                //keyValues.Add("@CARGO", ApoCargo);
                // keyValues.Add("@DireccionOf", ApoDirOf);
                keyValues.Add("@Usuario", UsuName);
                keyValues.Add("@Usuruc", UsuRuc);
                keyValues.Add("@UsuDir", UsuDir);

                keyValues.Add("@Representante", Responsable);
                keyValues.Add("@Repredni", RepDni);
                //keyValues.Add("@RepreDirec", RepDir + " " + departamentoRep + " " + provinciaRep + " " + distritoRep);
                keyValues.Add("@LOCAL", LocalName);
                //keyValues.Add("@LOCDIRECC", LocalDir + " " + departamentoRep + " " + provinciaRep + " " + distritoRep);
                keyValues.Add("@LOCDIRECC", LocalDir);
                // keyValues.Add("@Partreg", UsuPartida);
                // keyValues.Add("@Zonreg", UsuZona);
                keyValues.Add("@DireccionUsu", DireccionUsu);
                keyValues.Add("@cantloc", cantidadlocales);
                keyValues.Add("@dia", dia);
                keyValues.Add("@mes", mes);
                keyValues.Add("@anio", anio);
                keyValues.Add("@usunombrecomercial", usunombrecomercial);
                keyValues.Add("@RepreRoll", RepreRoll);

                //keyValues.Add("@TARIFA", "");
                var imagen = GenerarQR(IdLicencia);
                FindandReplaceWithImage(destinationFile, keyValues, imagen, 5);
                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }
            return resultado;
        }


        public bool CrearCartaTransExtrajudicialCadena(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[6];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            var apoderado = new BLReporte().ObtenerDatosApoderadoLegal(GlobalVars.Global.OWNER, IdLicencia);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var UsuParZonSed = new BLReporte().ObtenerPartidaZonaSedeUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var representantelegal = new BLReporte().ObtenerRepresentanteLegal(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var sociocab = new BLSocioNegocio().ObtenerDatos(usuarioderecho.BPS_ID, GlobalVars.Global.OWNER);
            //XXXXXXXXXXXX RESPONSABLE XXXXXXXX
            var respon = new BLReporte().AsociadoXSocio(sociocab.BPS_ID);
            string responsableROll = null;
            decimal responsableID = 0;
            if (respon != null && respon.Count > 0)
            {
                responsableROll = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().ROL_DESC;
                responsableID = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().BPSA_ID;

            }
            var responsable = new BLSocioNegocio().ObtenerDatos(responsableID, GlobalVars.Global.OWNER);//datos del responsable
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            var sociodir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, sociocab.BPS_ID);
            var cantloca = new BLLicencias().ListarLicHijasxPadre(IdLicencia).Count();
            var Fecha = DateTime.Now.ToShortDateString();
            var periodominimo = new BLReporte().ObtenerPeriodoMinimo(IdLicencia);

            string UsuName = ReplaceVacio;
            string UsuRuc = ReplaceVacio;
            string UsuDir = ReplaceVacio;
            string usunombrecomercial = ReplaceVacio;
            string UsuPartida = ReplaceVacio;
            string UsuZona = ReplaceVacio;
            string UsuSede = ReplaceVacio;
            string ApoName = ReplaceVacio;
            string ApoDni = ReplaceVacio;
            string ApoCargo = ReplaceVacio;
            string ApoDirOf = ReplaceVacio;
            string Representante = ReplaceVacio;
            string RepreRoll = ReplaceVacio;
            string RepDni = ReplaceVacio;
            string RepDir = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string distritoRep = ReplaceVacio;
            string provinciaRep = ReplaceVacio;
            string departamentoRep = ReplaceVacio;
            string distritoLoc = ReplaceVacio;
            string provinciaLoc = ReplaceVacio;
            string departamentoLoc = ReplaceVacio;
            string DireccionUsu = ReplaceVacio;
            string cantidadlocales = ReplaceVacio;
            string Responsable = ReplaceVacio;
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString();
            string anio = DateTime.Now.Year.ToString();

            if (usuarioderecho != null)
            {
                UsuName = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME.Trim();
                UsuRuc = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID.Trim();
                usunombrecomercial = sociocab.BPS_TRADE_NAME == null ? ReplaceVacio : sociocab.BPS_TRADE_NAME;

                if (sociodir != null && sociodir.Count > 0)
                    DireccionUsu = usuarioderecho.ADDRESS;
            }

            if (UsuParZonSed != null)
            {
                UsuPartida = UsuParZonSed.BPS_PARTIDA == null ? ReplaceVacio : UsuParZonSed.BPS_PARTIDA.Trim();
                UsuZona = UsuParZonSed.BPS_ZONA == null ? ReplaceVacio : UsuParZonSed.BPS_ZONA.Trim();
                UsuSede = UsuParZonSed.BPS_SEDE == null ? ReplaceVacio : UsuParZonSed.BPS_SEDE.Trim();
            }

            if (responsable != null)
            {
                Responsable = responsable.BPS_FIRST_NAME + ' ' + responsable.BPS_FATH_SURNAME + ' ' + responsable.BPS_MOTH_SURNAME;
                RepDni = responsable.TAX_ID;
                RepreRoll = responsableROll;

                //ApoDirOf = socioresponsable.ADDRESS == null ? ReplaceVacio : apoderado.ADDRESS.Trim();
            }

            if (representantelegal != null)
            {
                RepDni = representantelegal.TAX_ID == null ? ReplaceVacio : representantelegal.TAX_ID.Trim();
                RepDir = representantelegal.ADDRESS == null ? ReplaceVacio : representantelegal.ADDRESS.Trim();

                if (representantelegal.TIS_N != null && representantelegal.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, representantelegal.TIS_N.ToString(), representantelegal.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoRep = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaRep = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoRep = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        distritoLoc = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                        provinciaLoc = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                        departamentoLoc = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                    }
                }
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = UsuName == ReplaceVacio ? 0 : 1;
            val[1] = UsuRuc == ReplaceVacio ? 0 : 1;
            val[2] = DireccionUsu == ReplaceVacio ? 0 : 1;
            val[3] = Responsable == ReplaceVacio ? 0 : 1;
            val[4] = RepDni == ReplaceVacio ? 0 : 1;
            val[5] = RepreRoll == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("NO SE ENCUENTRA AL SOCIO DE NEGOCIO");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener RUC");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario derecho debe tener DIRECICON PRINCIPAL");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("No se asigno una entidad Responsable al Usuario de derecho");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro el dni de la entidad Responsable al Usuario de derecho");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro el roll de la entidad Responsable al Usuario de derecho");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@UsuName", UsuName);
                keyValues.Add("@UsuRuc", UsuRuc);
                keyValues.Add("@DireccionUsu", DireccionUsu);
                keyValues.Add("@Responsable", Responsable);
                keyValues.Add("@RepDni", RepDni);
                keyValues.Add("@RepreRoll", RepreRoll);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;

        }
        public bool CrearCartaCnciliacionCadena(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            string Fecha = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RucUsuarioDerecho = ReplaceVacio;
            string LocalName = ReplaceVacio;
            string LocalDir = ReplaceVacio;
            string UbigeoEstablecimiento = ReplaceVacio;
            string FechaCorta = new BLReporte().FechaActualShort();

            string NomAutoridadPrincipal = ReplaceVacio;
            string DocAutoridadPrincipal = ReplaceVacio;

            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdParametroURL);
            var contacto = new BLReporte().ObtenerContacto(GlobalVars.Global.OWNER, IdLicencia);
            var AutoridadPrincipal = new BLReporte().ObtenerAutoridadPrincipal(GlobalVars.Global.OWNER, IdLicencia);

            if (!string.IsNullOrEmpty(FechaCorta))
            {
                DateTime datex = DateTime.Parse(FechaCorta);
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }
            else
            {
                DateTime datex = DateTime.Now;
                Fecha = datex.ToString("dd 'de' MMMMMM 'de' yyyy");
            }

            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME == null ? ReplaceVacio : usuarioderecho.BPS_NAME;
                RucUsuarioDerecho = usuarioderecho.TAX_ID == null ? ReplaceVacio : usuarioderecho.TAX_ID;
            }

            if (establecimiento != null)
            {
                LocalName = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();
                LocalDir = establecimiento.ADDRESS == null ? ReplaceVacio : establecimiento.ADDRESS.Trim();

                if (establecimiento.TIS_N != null && establecimiento.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                    {
                        UbigeoEstablecimiento = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    }
                }
            }

            if (AutoridadPrincipal != null)
            {
                NomAutoridadPrincipal = AutoridadPrincipal.BPS_NAME == null ? ReplaceVacio : AutoridadPrincipal.BPS_NAME;
                DocAutoridadPrincipal = AutoridadPrincipal.TAX_ID == null ? ReplaceVacio : AutoridadPrincipal.TAX_ID;
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = Fecha == ReplaceVacio ? 0 : 1;
            val[1] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[2] = RucUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[3] = LocalDir == ReplaceVacio ? 0 : 1;
            val[4] = 1;//NomAutoridadPrincipal = 
            val[5] = 1; // DocAutoridadPrincipal =

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("El documento debe tener asignada una fecha.");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("El usuario de derecho debe tener registrado una direccion principal.");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("El Usuario derecho debe tener registrado su Ruc.");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("El Establecimiento debe tener asignada una Dirección.");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Nombre autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo. ");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Documento autoridad principal (ir a oficina de recaudo -> pestaña agentes). Esta autoridad principal debe tener un telefono de trabajo.");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;


            if (resultado)
            {
                keyValues.Add("@FechN", Fecha);
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Ruc", RucUsuarioDerecho);
                keyValues.Add("@DireccionL", LocalDir);
                keyValues.Add("@NomAut", NomAutoridadPrincipal);
                keyValues.Add("@DocAuto", DocAutoridadPrincipal);
                FindandReplace(destinationFile, keyValues);

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            }

            return resultado;

        }
        #endregion

        #region Megaconciertos
        public bool CrearAutorizacionMegaconcierto(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[15];
            bool resultado = true;
            List<BEShow> show = new List<BEShow>();
            List<BEShowArtista> Artistas = new List<BEShowArtista>();
            List<BEDireccion> ResponsDir = null;
            string DistritoOficina = ReplaceVacio;
            string DescripcionAutorizacion = ReplaceVacio;
            string DireccionOficina = ReplaceVacio;
            string DepartamentoOficina = ReplaceVacio;
            string ProvinciaOficina = ReplaceVacio;
            string Local = ReplaceVacio;
            string FechaInicio = ReplaceVacio;
            string FechaFin = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string DireccionUsu = ReplaceVacio;
            string UbigeoUsu = ReplaceVacio;
            string RUCUsuarioDerecho = ReplaceVacio;
            string MontoPrimeraFactura = ReplaceVacio;
            string MontoFormato = ReplaceVacio;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = new BLReporte().FechaActualShort();
            string ArtistasxShow = ReplaceVacio;
            string Responsable = ReplaceVacio;
            string RepDni = ReplaceVacio;
            string RepreRoll = ReplaceVacio;
            string RepreDir = ReplaceVacio;
            string SeriNumFactura = ReplaceVacio;
            string LIC_ID = IdLicencia.ToString();
            string ESTADO_LICENCIA = ReplaceVacio;

            // var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdRum);
            var licencia = new BLLicencias().ObtenerLicenciaXCodigo(IdLicencia, GlobalVars.Global.OWNER);
            var sociocab = new BLSocioNegocio().ObtenerDatos(licencia.BPS_ID, GlobalVars.Global.OWNER);
            var sociodir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, sociocab.BPS_ID);
            var ubisocio = new BLDirecciones().ObtenerUbigeoSocio(GlobalVars.Global.OWNER, IdLicencia);
            ESTADO_LICENCIA = new BLReporte().ObtieneDescripcionEstadoLicencia(IdLicencia);
            var autorizacion = new BLAutorizacion().AutorizacionXLicencia(licencia.LIC_ID, GlobalVars.Global.OWNER);
            var monto = new BLLicencias().ObtienePrimeraFacturaAutorizacion(licencia.LIC_ID);
            var montoletras = ReplaceVacio;
            SeriNumFactura = new BLLicencias().ObtieneSerieNumFacturaEspectLic(licencia.LIC_ID);
            //XXXXXXXXXXXX RESPONSABLE XXXXXXXX
            List<BEAsociado> respon = new List<BEAsociado>();
            if (sociocab != null) //s ies diferente de null
            {
                if (sociocab.TAXT_ID == 1) // si es JURIDICO DEBE TENER UN RESPONSABLE
                { 

                respon = new BLReporte().AsociadoXSocio(sociocab.BPS_ID);
                }
                else // SI NO LO ES SALTAR VALIDACION
                {
                    Responsable = "";
                    RepDni = "";
                }
            }
            string responsableROll = null;
            decimal responsableID = 0;
            if (respon != null && respon.Count > 0 && sociocab != null) //
            {
                responsableROll = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().ROL_DESC;
                responsableID = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().BPSA_ID;
                ResponsDir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, responsableID);

            }


            var responsable = new BLSocioNegocio().ObtenerDatos(responsableID, GlobalVars.Global.OWNER);//datos del responsable

            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            string Fecha = string.Empty;
            if (!string.IsNullOrEmpty(FechaCorta))
                Fecha = FechaCorta;
            else
                Fecha = DateTime.Now.ToShortDateString();



            if (sociocab != null)
            {
                if(sociocab.TAXT_ID==1) //1 juridio
                UsuarioDerecho = sociocab.BPS_NAME;
                else   // diferente natural o extranjeria 
                UsuarioDerecho = sociocab.BPS_FIRST_NAME+ ' '+ sociocab.BPS_FATH_SURNAME + ' ' + sociocab.BPS_MOTH_SURNAME;

                RUCUsuarioDerecho = sociocab.TAX_ID;
                if (sociodir != null && sociodir.Count > 0)
                    DireccionUsu = sociodir[0].ADDRESS;

                UbigeoUsu = ubisocio.DESCRIPTION;


            }
            if (responsable != null)
            {
                Responsable = responsable.BPS_FIRST_NAME + ' ' + responsable.BPS_FATH_SURNAME + ' ' + responsable.BPS_MOTH_SURNAME;
                RepDni = responsable.TAX_ID;
                RepreRoll = responsableROll;
                if (ResponsDir.Count > 0 && ResponsDir != null)
                    RepreDir = ResponsDir.FirstOrDefault().ADDRESS;
                //ApoDirOf = socioresponsable.ADDRESS == null ? ReplaceVacio : apoderado.ADDRESS.Trim();
            }

            if (establecimiento != null)
            {
                if (establecimiento.ADDRESS.Length > 1)
                    DireccionOficina = establecimiento.ADDRESS; //nuevo

                Local = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

                var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    ProvinciaOficina = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    DistritoOficina = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    DepartamentoOficina = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }


            if (autorizacion != null && autorizacion.Count > 0)
            {
                var fechaini = autorizacion.FirstOrDefault().LIC_AUT_START;
                FechaInicio = fechaini.ToString("dd/MM/yyyy");

                var fechafin = autorizacion.FirstOrDefault().LIC_AUT_END;
                FechaFin = string.Format("{0:dd/MM/yyyy}", fechafin);
                //FechaFin = fechafin.ToString();
                DescripcionAutorizacion = autorizacion.FirstOrDefault().LIC_AUT_OBS.ToString();
                //SHOW
                show = new BLShow().ShowsXAutorizaciones(autorizacion.FirstOrDefault().LIC_AUT_ID, GlobalVars.Global.OWNER);

            }
            if (show.Count > 0 && show != null)
            {

                var cont = 0;
                foreach (var x in show)
                {
                    Artistas = new BLShowArtista().ShowsXArtistas(x.SHOW_ID, GlobalVars.Global.OWNER).Where(z => z.ARTIST_PPAL == "1").ToList();
                    ArtistasxShow = "";
                    if (Artistas.Count > 0 && Artistas != null)
                    {
                        cont++;

                        ArtistasxShow = ArtistasxShow + Artistas.Where(z => z.ARTIST_PPAL == "1").FirstOrDefault().NAME;

                        if (show.Count > cont)
                            ArtistasxShow = ArtistasxShow + " , ";
                    }
                }
            }

            if (monto > 0 && monto != null)
            {

                MontoPrimeraFactura = monto.ToString();
                montoletras = NumeroALetras(monto);
                MontoFormato = "S/. " + MontoPrimeraFactura;
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            //   val[0] = NumeroBoleta == ReplaceVacio ? 0 : 1;
            val[0] = Local == ReplaceVacio ? 0 : 1;
            val[1] = DistritoOficina == ReplaceVacio ? 0 : 1;
            val[2] = Responsable == ReplaceVacio ? 0 : 1;
            val[3] = RepDni == ReplaceVacio ? 0 : 1;
            val[4] = RUCUsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[5] = DireccionUsu == ReplaceVacio ? 0 : 1;
            val[6] = FechaInicio == ReplaceVacio ? 0 : 1;
            val[7] = DescripcionAutorizacion == ReplaceVacio ? 0 : 1;
            val[8] = MontoPrimeraFactura == ReplaceVacio ? 0 : 1;
            val[9] = montoletras == ReplaceVacio ? 0 : 1;
            val[10] = ArtistasxShow == ReplaceVacio || ArtistasxShow=="" ? 0 : 1;
            val[11] = DireccionOficina == ReplaceVacio ? 1 : 1;
            val[12] = SeriNumFactura == ReplaceVacio ? 1 : 1;
            val[13] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[14] = UbigeoUsu == ReplaceVacio ? 0 : 1;
            //val[7] = Frecuencia == ReplaceVacio ? 1 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del local");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("FALTA UBIGEO DE ESTABLECIMIENTO ");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Falta Asignar un Responsable de la Razon social");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Falta Dni del Responsable");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("RUC Usuario de derecho Erronea");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Falta Direccion Principal de Usuario de Derecho");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add(" Falta fECHA DE Autorizacion");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("FALTA DESCRIPCION DE LA AUTORIZACION");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("FALTA  MONTO DE FACTURA");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add(" FALTA MONTO DE FACTURA LETRAS");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add(" FALTAN ARTISTAS  POR SHOW O NO SE MARCARON COMO PRINCIPAL");
                                break;
                            case 11:
                                GlobalVars.Global.ListMessageReport.Add("Direccion Establecimiento");
                                break;
                            case 12:
                                GlobalVars.Global.ListMessageReport.Add(" FALTA SERIE - NUM FACTURA ");
                                break;
                            case 13:
                                GlobalVars.Global.ListMessageReport.Add("Error en datos de Usuario de Derecho");

                                break;

                            case 14:
                                GlobalVars.Global.ListMessageReport.Add("No se Encontro Ubigeo en la Direccion del Usuario");

                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado) //descomentar
            {
                keyValues.Add("@RUCUsuarioDerecho", RUCUsuarioDerecho);
                keyValues.Add("@FechaInicio", FechaInicio);
                keyValues.Add("@Establecimiento", Local);
                keyValues.Add("@FechaFin", FechaFin);//falta
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Responsable", Responsable);
                keyValues.Add("@DirecUsu", DireccionUsu);
                keyValues.Add("@DetalleAutorizacion",ESTADO_LICENCIA);
                keyValues.Add("@UbigeoSocio", UbigeoUsu);
                keyValues.Add("@RepDni", RepDni);
                keyValues.Add("@Provincia", ProvinciaOficina);
                keyValues.Add("@Localidad", DistritoOficina);
                keyValues.Add("@MontoPrimeraFactura", MontoPrimeraFactura);
                keyValues.Add("@montoletras", montoletras);
                keyValues.Add("@MontoFormato", MontoFormato);
                keyValues.Add("@Artista", ArtistasxShow);
                keyValues.Add("@DescripcionAutorizacion", DescripcionAutorizacion);
                keyValues.Add("@Direccion", DireccionOficina);
                keyValues.Add("@Factura", SeriNumFactura);
                keyValues.Add("@LicID", LIC_ID);

                //   keyValues.Add("@FecFactPre", " ");
                //   keyValues.Add("@FactLiq", " ");
                //   keyValues.Add("@FecFactLiq", " ");
                //   keyValues.Add("@Importe", " ");
                //   keyValues.Add("@Artistas", " ");

                var imagen = GenerarQR(IdLicencia);
                FindandReplaceWithImage(destinationFile, keyValues, imagen,6);

                #region PruebaGenerarTablaDinamica

                #endregion

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);

                //resultado = true;//comentar
                }

            return resultado;
        }
        public bool CrearCartaSolidarioResponsable(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[10];
            bool resultado = true;
            BEUbigeoRpt ubigeo = new BEUbigeoRpt();
            List<BEShow> show = new List<BEShow>();
            List<BEShowArtista> Artistas = new List<BEShowArtista>();
            String ArtistasxShow = ReplaceVacio;
            string DistritoUsu = ReplaceVacio;
            string DireccionOficina = ReplaceVacio;
            string DepartamentoOficina = ReplaceVacio;
            string ProvinciaOficina = ReplaceVacio;
            string Local = ReplaceVacio;
            string FechaInicio = ReplaceVacio;
            string FechaFin = ReplaceVacio;
            string DescripcionAutorizacion = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string DireccionUsu = ReplaceVacio;
            string RUCUsuarioDerecho = ReplaceVacio;
            string MontoPrimeraFactura = ReplaceVacio;
            string MontoFormato = ReplaceVacio;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = DateTime.Now.ToLongDateString();

            // var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdRum);
            var licencia = new BLLicencias().ObtenerLicenciaXCodigo(IdLicencia, GlobalVars.Global.OWNER);
            var sociocab = new BLSocioNegocio().ObtenerDatos(licencia.BPS_ID, GlobalVars.Global.OWNER);
            var sociodir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, sociocab.BPS_ID);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            var autorizacion = new BLAutorizacion().AutorizacionXLicencia(licencia.LIC_ID, GlobalVars.Global.OWNER);
            // var monto = new BLLicencias().ObtienePrimeraFacturaAutorizacion(licencia.LIC_ID);
            // var montoletras = ReplaceVacio;

            string Fecha = string.Empty;
            if (!string.IsNullOrEmpty(FechaCorta))
                Fecha = FechaCorta;
            else
                Fecha = DateTime.Now.ToShortDateString();



            if (sociocab != null)
            {
                UsuarioDerecho = sociocab.BPS_NAME;
                RUCUsuarioDerecho = sociocab.TAX_ID;
                if (sociodir != null && sociodir.Count > 0)
                    DireccionUsu = sociodir.First().ADDRESS;


            }
            if (usuarioderecho != null)
            {
                UsuarioDerecho = usuarioderecho.BPS_NAME;
                RUCUsuarioDerecho = usuarioderecho.TAX_ID;
                DireccionUsu = usuarioderecho.ADDRESS;
                //  if (sociodir != null && sociodir.Count > 0)
                //      DireccionUsu = sociodir.First().ADDRESS;
                if (usuarioderecho.TIS_N.ToString() != null && usuarioderecho.GEO_ID != null)
                {
                    ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString());
                    if (ubigeo != null)
                        DistritoUsu = ubigeo.Distrito;
                }
            }
            if (establecimiento != null)
            {
                Local = establecimiento.EST_NAME;

            }

            if (autorizacion != null && autorizacion.Count > 0)
            {
                var fechaini = autorizacion.FirstOrDefault().LIC_AUT_START;
                FechaInicio = fechaini.ToString("dd/MM/yyyy");

                var fechafin = autorizacion.FirstOrDefault().LIC_AUT_END;
                FechaFin = fechafin.ToString();

                DescripcionAutorizacion = autorizacion.FirstOrDefault().LIC_AUT_OBS.ToString();
                //SHOW
                show = new BLShow().ShowsXAutorizaciones(autorizacion.FirstOrDefault().LIC_AUT_ID, GlobalVars.Global.OWNER);

            }
            if (show.Count > 0 && show != null)
            {
                var cont = 0;
                foreach (var x in show)
                {
                    Artistas = new BLShowArtista().ShowsXArtistas(x.SHOW_ID, GlobalVars.Global.OWNER);
                    if (Artistas.Count > 0 && Artistas != null)
                    {
                        cont++;
                        ArtistasxShow = ArtistasxShow + Artistas.Where(z => z.ARTIST_PPAL == "1").FirstOrDefault().NAME;

                        if (show.Count > cont)
                            ArtistasxShow = ArtistasxShow + " , ";
                    }
                }
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            //   val[0] = NumeroBoleta == ReplaceVacio ? 0 : 1;
            val[0] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[1] = DistritoUsu == ReplaceVacio ? 0 : 1;
            val[2] = DireccionUsu == ReplaceVacio ? 0 : 1;
            val[3] = DistritoUsu == ReplaceVacio ? 0 : 1;
            val[4] = DescripcionAutorizacion == ReplaceVacio ? 0 : 1;
            val[5] = FechaInicio == ReplaceVacio ? 0 : 1;
            val[6] = FechaFin == ReplaceVacio ? 0 : 1;
            val[7] = FechaCorta == ReplaceVacio ? 0 : 1;
            val[8] = Local == ReplaceVacio ? 0 : 1;
            val[9] = ArtistasxShow == ReplaceVacio ? 0 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de Derecho");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Distrito de Usuario de Derecho");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Dirección de Usuario de Derecho");
                                break;
                            case 3:

                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("La Autorizacion no tiene una Descripcion");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("La Autorizacion no tiene una Fecha de Inicio");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("La Autorizacion no tiene una Fecha de Fin");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha del Sistema");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del Establecimiento");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Show de Autorizaciones Sin Artistas Principales");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@FechaInicio", FechaInicio);
                keyValues.Add("@Establecimiento", Local);
                keyValues.Add("@FechaFin", FechaFin);//falta
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@Direccion", DireccionOficina);
                keyValues.Add("@DireccionUsu", DireccionUsu);
                keyValues.Add("@Localidad", DistritoUsu);
                keyValues.Add("@fecha", FechaCorta);
                keyValues.Add("@DescripcionAutorizacion", DescripcionAutorizacion);
                keyValues.Add("@ArtistaPrincipal", ArtistasxShow);
                keyValues.Add("@Licencia", IdLicencia.ToString());
                //   keyValues.Add("@Artistas", " ");
                FindandReplace(destinationFile, keyValues);

                #region PruebaGenerarTablaDinamica

                #endregion

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);


            }

            return resultado;
        }

        public void CrearFormatodeBailesEspectaculos(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            //string sourceFile = GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculos.docx";
            //string destinationFile = GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.docx";
            //System.IO.File.Copy(sourceFile, destinationFile, true);string sourceFile = nombreFile;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            SearchAndReplace(destinationFile, keyValues);
            string RutaWord = destinationFile; // GlobalVars.Global.DocumentoBailesEspectaculos + "CartaInfVerificaciónUsoObrasMusicalescopia.docx";
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            //FindandReplace(destinationFile, keyValues);
            //Convert(GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.docx", GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.pdf", WdSaveFormat.wdFormatPDF);
        }
        public void CrearDeclaracionJuradadeBoletaje(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            //string sourceFile = GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculos.docx";
            //string destinationFile = GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.docx";
            //System.IO.File.Copy(sourceFile, destinationFile, true);string sourceFile = nombreFile;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            SearchAndReplace(destinationFile, keyValues);
            string RutaWord = destinationFile; // GlobalVars.Global.DocumentoBailesEspectaculos + "CartaInfVerificaciónUsoObrasMusicalescopia.docx";
            Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);
            //FindandReplace(destinationFile, keyValues);
            //Convert(GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.docx", GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.pdf", WdSaveFormat.wdFormatPDF);
        }


        public bool CrearDocumentoRetencionSimple(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[8];
            bool resultado = true;
            List<BEShow> show = new List<BEShow>();
            List<BEShowArtista> Artistas = new List<BEShowArtista>();
            List<BEDireccion> ResponsDir = null;
            string DistritoOficina = ReplaceVacio;
            string DescripcionAutorizacion = ReplaceVacio;
            string DireccionOficina = ReplaceVacio;
            string DepartamentoOficina = ReplaceVacio;
            string ProvinciaOficina = ReplaceVacio;
            string Local = ReplaceVacio;
            string FechaInicio = ReplaceVacio;
            string FechaFin = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string DireccionUsu = ReplaceVacio;
            string RUCUsuarioDerecho = ReplaceVacio;
            string MontoPrimeraFactura = ReplaceVacio;
            string MontoFormato = ReplaceVacio;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = new BLReporte().FechaActualShort();
            string ArtistasxShow = ReplaceVacio;
            string Responsable = ReplaceVacio;
            string RepDni = ReplaceVacio;
            string RepreRoll = ReplaceVacio;
            string RepreDir = ReplaceVacio;
            // var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdRum);
            var licencia = new BLLicencias().ObtenerLicenciaXCodigo(IdLicencia, GlobalVars.Global.OWNER);
            var sociocab = new BLSocioNegocio().ObtenerDatos(licencia.BPS_ID, GlobalVars.Global.OWNER);
            var sociodir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, sociocab.BPS_ID);
            var autorizacion = new BLAutorizacion().AutorizacionXLicencia(licencia.LIC_ID, GlobalVars.Global.OWNER);
            //var monto = new BLLicencias().ObtienePrimeraFacturaAutorizacion(licencia.LIC_ID);
            //XXXXXXXXXXXX RESPONSABLE XXXXXXXX
            var respon = new BLReporte().AsociadoXSocio(sociocab.BPS_ID);
            string responsableROll = null;
            decimal responsableID = 0;
            if (respon != null && respon.Count > 0)
            {
                responsableROll = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().ROL_DESC;
                responsableID = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().BPSA_ID;
                ResponsDir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, responsableID);

            }
            var responsable = new BLSocioNegocio().ObtenerDatos(responsableID, GlobalVars.Global.OWNER);//datos del responsable

            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            var montoletras = ReplaceVacio;

            string Fecha = string.Empty;
            if (!string.IsNullOrEmpty(FechaCorta))
                Fecha = FechaCorta;
            else
                Fecha = DateTime.Now.ToShortDateString();



            if (sociocab != null)
            {
                UsuarioDerecho = sociocab.BPS_NAME;
                RUCUsuarioDerecho = sociocab.TAX_ID;
                if (sociodir != null && sociodir.Count > 0)
                    DireccionUsu = sociodir[0].ADDRESS;


            }

            if (establecimiento != null)
            {
                DireccionOficina = establecimiento.ADDRESS; //nuevo
                Local = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

                var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    ProvinciaOficina = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    DistritoOficina = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    DepartamentoOficina = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }


            if (responsable != null)
            {
                Responsable = responsable.BPS_FIRST_NAME + ' ' + responsable.BPS_FATH_SURNAME + ' ' + responsable.BPS_MOTH_SURNAME;
                RepDni = responsable.TAX_ID;
                RepreRoll = responsableROll;
                if (ResponsDir != null && ResponsDir.Count > 0)
                    RepreDir = ResponsDir.FirstOrDefault().ADDRESS;
                //ApoDirOf = socioresponsable.ADDRESS == null ? ReplaceVacio : apoderado.ADDRESS.Trim();
            }

            if (autorizacion != null && autorizacion.Count > 0)
            {
                var fechaini = autorizacion.FirstOrDefault().LIC_AUT_START;
                FechaInicio = fechaini.ToString("dd/MM/yyyy");

                var fechafin = autorizacion.FirstOrDefault().LIC_AUT_END;
                FechaFin = fechafin.ToString();

                DescripcionAutorizacion = autorizacion.FirstOrDefault().LIC_AUT_OBS.ToString();
                //SHOW
                show = new BLShow().ShowsXAutorizaciones(autorizacion.FirstOrDefault().LIC_AUT_ID, GlobalVars.Global.OWNER);

            }
            if (show.Count > 0 && show != null)
            {

                var cont = 0;
                foreach (var x in show)
                {
                    Artistas = new BLShowArtista().ShowsXArtistas(x.SHOW_ID, GlobalVars.Global.OWNER);
                    ArtistasxShow = "";
                    if (Artistas.Count > 0 && Artistas != null)
                    {
                        cont++;

                        ArtistasxShow = ArtistasxShow + Artistas.Where(z => z.ARTIST_PPAL == "1").FirstOrDefault().NAME;

                        if (show.Count > cont)
                            ArtistasxShow = ArtistasxShow + " , ";
                    }
                }
            }

            //if (monto > 0 && monto != null)
            //{

            //    MontoPrimeraFactura = monto.ToString();
            //    montoletras = NumeroALetras(monto);
            //    MontoFormato = "S/. " + MontoPrimeraFactura;
            //}

            GlobalVars.Global.ListMessageReport = new List<string>();

            //   val[0] = NumeroBoleta == ReplaceVacio ? 0 : 1;
            val[0] = Responsable == ReplaceVacio ? 0 : 1;
            val[1] = RepDni == ReplaceVacio ? 0 : 1;
            val[2] = RepreDir == ReplaceVacio ? 0 : 1;
            val[3] = DescripcionAutorizacion == ReplaceVacio ? 0 : 1;
            val[4] = Local == ReplaceVacio ? 0 : 1;
            val[5] = FechaInicio == ReplaceVacio ? 0 : 1;
            val[6] = ArtistasxShow == ReplaceVacio ? 0 : 1;
            val[7] = DireccionUsu == ReplaceVacio ? 0 : 1;

            //val[8] = FechaInicio == ReplaceVacio ? 0 : 1;
            //val[9] = FechaFin == ReplaceVacio ? 0 : 1;
            //val[10] = MontoPrimeraFactura == ReplaceVacio ? 0 : 1;
            //val[11] = montoletras == ReplaceVacio ? 0 : 1;
            //val[12] = ArtistasxShow == ReplaceVacio ? 0 : 1;
            // val[8] = Rum == ReplaceVacio ? 1 : 1;
            //val[7] = Frecuencia == ReplaceVacio ? 1 : 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Responsable");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Responsable DNI");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Dirección del Responsable");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Descripcion de Autorizacion");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Fecha de Inicio");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Artista Principal");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("DIRECCION DE USUARIO DE DERECHO");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@RUCUsuarioDerecho", RUCUsuarioDerecho);
                keyValues.Add("@FechaInicio", FechaInicio);
                keyValues.Add("@Establecimiento", Local);
                keyValues.Add("@Responsable", Responsable);//falta
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                //     keyValues.Add("@Direccion", DireccionOficina);
                keyValues.Add("@DireccionUsu", DireccionUsu);
                //   keyValues.Add("@Departamento", DepartamentoOficina);
                //  keyValues.Add("@Provincia", ProvinciaOficina);
                keyValues.Add("@DescripcionAutorizacion", DescripcionAutorizacion);
                //   keyValues.Add("@MontoPrimeraFactura", MontoPrimeraFactura);
                //  keyValues.Add("@montoletras", montoletras);
                //   keyValues.Add("@MontoFormato", MontoFormato);
                keyValues.Add("@ArtistaPrincipal", ArtistasxShow);
                keyValues.Add("@fecha", FechaCorta);
                //   keyValues.Add("@FactPre", " ");
                //   keyValues.Add("@FecFactPre", " ");
                //   keyValues.Add("@FactLiq", " ");
                //   keyValues.Add("@FecFactLiq", " ");
                //   keyValues.Add("@Importe", " ");
                //   keyValues.Add("@Artistas", " ");
                FindandReplace(destinationFile, keyValues);

                #region PruebaGenerarTablaDinamica

                #endregion

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);


            }

            return resultado;
        }

        public bool CrearDocumentoRetencionProntoPago(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF)
        {
            int[] val = new int[12];
            bool resultado = true;
            List<BEShow> show = new List<BEShow>();
            List<BEShowArtista> Artistas = new List<BEShowArtista>();
            List<BEDireccion> ResponsDir = null;
            string DistritoUsu = ReplaceVacio;
            string DistritoEst = ReplaceVacio;
            string ProvinciaEst = ReplaceVacio;
            string DescripcionAutorizacion = ReplaceVacio;
            string DireccionOficina = ReplaceVacio;
            string DepartamentoEst = ReplaceVacio;
            string ProvinciaUsu = ReplaceVacio;
            string Local = ReplaceVacio;
            string FechaInicio = ReplaceVacio;
            string FechaFin = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string DireccionUsu = ReplaceVacio;
            string RUCUsuarioDerecho = ReplaceVacio;
            string MontoPrimeraFactura = ReplaceVacio;
            string MontoFormato = ReplaceVacio;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = DateTime.Now.ToLongDateString();
            string ArtistasxShow = ReplaceVacio;
            string Responsable = ReplaceVacio;
            string RepDni = ReplaceVacio;
            string RepreRoll = ReplaceVacio;
            string RepreDir = ReplaceVacio;
            // var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdRum);
            var licencia = new BLLicencias().ObtenerLicenciaXCodigo(IdLicencia, GlobalVars.Global.OWNER);
            var sociocab = new BLSocioNegocio().ObtenerDatos(licencia.BPS_ID, GlobalVars.Global.OWNER);
            var sociodir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, sociocab.BPS_ID);
            var autorizacion = new BLAutorizacion().AutorizacionXLicencia(licencia.LIC_ID, GlobalVars.Global.OWNER);
            var usuarioderecho = new BLReporte().ObtenerUsuarioDerecho(GlobalVars.Global.OWNER, IdLicencia);
            //var monto = new BLLicencias().ObtienePrimeraFacturaAutorizacion(licencia.LIC_ID);
            //XXXXXXXXXXXX RESPONSABLE XXXXXXXX
            var respon = new BLReporte().AsociadoXSocio(sociocab.BPS_ID);
            string responsableROll = null;
            decimal responsableID = 0;
            if (respon != null && respon.Count > 0)
            {
                responsableROll = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().ROL_DESC;
                responsableID = new BLReporte().AsociadoXSocio(sociocab.BPS_ID).FirstOrDefault().BPSA_ID;
                ResponsDir = new BLDirecciones().Obtener(GlobalVars.Global.OWNER, responsableID);

            }
            var responsable = new BLSocioNegocio().ObtenerDatos(responsableID, GlobalVars.Global.OWNER);//datos del responsable

            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            var montoletras = ReplaceVacio;

            string Fecha = string.Empty;
            if (!string.IsNullOrEmpty(FechaCorta))
                Fecha = FechaCorta;
            else
                Fecha = DateTime.Now.ToShortDateString();



            if (sociocab != null)
            {
                UsuarioDerecho = sociocab.BPS_NAME;
                RUCUsuarioDerecho = sociocab.TAX_ID;
                if (sociodir != null && sociodir.Count > 0)
                    DireccionUsu = sociodir[0].ADDRESS;
                if (UsuarioDerecho != null)
                {

                    if (usuarioderecho.TIS_N != null && usuarioderecho.GEO_ID != null)
                        DistritoUsu = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString()).Distrito;
                    ProvinciaUsu = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, usuarioderecho.TIS_N.ToString(), usuarioderecho.GEO_ID.ToString()).Provincia;
                }

            }

            if (establecimiento != null)
            {
                DireccionOficina = establecimiento.ADDRESS; //nuevo
                Local = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

                var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    ProvinciaEst = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    DistritoEst = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    DepartamentoEst = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }


            if (responsable != null)
            {
                Responsable = responsable.BPS_FIRST_NAME + ' ' + responsable.BPS_FATH_SURNAME + ' ' + responsable.BPS_MOTH_SURNAME;
                RepDni = responsable.TAX_ID;
                RepreRoll = responsableROll;
                if (ResponsDir != null && ResponsDir.Count > 0)
                    RepreDir = ResponsDir.FirstOrDefault().ADDRESS;
                //ApoDirOf = socioresponsable.ADDRESS == null ? ReplaceVacio : apoderado.ADDRESS.Trim();
            }

            if (autorizacion != null && autorizacion.Count > 0)
            {
                var fechaini = autorizacion.FirstOrDefault().LIC_AUT_START;
                FechaInicio = fechaini.ToString("dd/MM/yyyy");

                var fechafin = autorizacion.FirstOrDefault().LIC_AUT_END;
                FechaFin = fechafin.ToString();

                DescripcionAutorizacion = autorizacion.FirstOrDefault().LIC_AUT_OBS.ToString();
                //SHOW
                show = new BLShow().ShowsXAutorizaciones(autorizacion.FirstOrDefault().LIC_AUT_ID, GlobalVars.Global.OWNER);

            }
            if (show.Count > 0 && show != null)
            {

                var cont = 0;
                foreach (var x in show)
                {
                    Artistas = new BLShowArtista().ShowsXArtistas(x.SHOW_ID, GlobalVars.Global.OWNER);
                    ArtistasxShow = "";
                    if (Artistas.Count > 0 && Artistas != null)
                    {
                        cont++;

                        ArtistasxShow = ArtistasxShow + Artistas.Where(z => z.ARTIST_PPAL == "1").FirstOrDefault().NAME;

                        if (show.Count > cont)
                            ArtistasxShow = ArtistasxShow + " , ";
                    }
                }
            }

            //if (monto > 0 && monto != null)
            //{

            //    MontoPrimeraFactura = monto.ToString();
            //    montoletras = NumeroALetras(monto);
            //    MontoFormato = "S/. " + MontoPrimeraFactura;
            //}

            GlobalVars.Global.ListMessageReport = new List<string>();

            //   val[0] = NumeroBoleta == ReplaceVacio ? 0 : 1;
            val[0] = Responsable == ReplaceVacio ? 0 : 1;
            val[1] = RepDni == ReplaceVacio ? 0 : 1;
            val[2] = RepreDir == ReplaceVacio ? 0 : 1;
            val[3] = DescripcionAutorizacion == ReplaceVacio ? 0 : 1;
            val[4] = Local == ReplaceVacio ? 0 : 1;
            val[5] = FechaInicio == ReplaceVacio ? 0 : 1;
            val[6] = ArtistasxShow == ReplaceVacio ? 0 : 1;
            val[7] = DireccionUsu == ReplaceVacio ? 0 : 1;
            val[8] = DistritoUsu == ReplaceVacio ? 0 : 1;
            val[9] = ProvinciaUsu == ReplaceVacio ? 0 : 1;
            val[10] = ProvinciaEst == ReplaceVacio ? 0 : 1;
            val[11] = DistritoEst == ReplaceVacio ? 0 : 1;


            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Responsable");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Responsable DNI");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Dirección del Responsable");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Descripcion de Autorizacion");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Establecimiento");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Fecha de Inicio");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Artista Principal");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("DIRECCION DE USUARIO DE DERECHO");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("DISTRITO  DE USUARIO DE DERECHO");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("PROVINCIA DE USUARIO DE DERECHO");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("DISTRITO DE ESTABLECIMIENTO");
                                break;
                            case 11:
                                GlobalVars.Global.ListMessageReport.Add("PROVINCIA DE ESTABLECIMIENTO");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@RUCUsuarioDerecho", RUCUsuarioDerecho);
                keyValues.Add("@FechaInicio", FechaInicio);
                keyValues.Add("@Establecimiento", Local);
                keyValues.Add("@Responsable", Responsable);//falta
                keyValues.Add("@UsuarioDerecho", UsuarioDerecho);
                keyValues.Add("@DireccionUsu", DireccionUsu);
                keyValues.Add("@RepDni", RepDni);
                keyValues.Add("@DistritoUsu", DistritoUsu);
                keyValues.Add("@ProvinciaUsu", ProvinciaUsu);
                keyValues.Add("@DescripcionAutorizacion", DescripcionAutorizacion);
                keyValues.Add("@DistritoEst", DistritoEst);
                keyValues.Add("@ProvinciaEst", ProvinciaEst);
                keyValues.Add("@ArtistaPrincipal", ArtistasxShow);
                keyValues.Add("@fecha", FechaCorta);
                //   keyValues.Add("@FactPre", " ");
                //   keyValues.Add("@FecFactPre", " ");
                //   keyValues.Add("@FactLiq", " ");
                //   keyValues.Add("@FecFactLiq", " ");
                //   keyValues.Add("@Importe", " ");
                //   keyValues.Add("@Artistas", " ");
                FindandReplace(destinationFile, keyValues);

                #region PruebaGenerarTablaDinamica

                #endregion

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);


            }

            return resultado;
        }
        public bool CrearReportePlanillaEjecicionStandard(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idSerie, decimal idReport,string nombrOfi)
        {
            int[] val = new int[12];
            bool resultado = true;

            string DistritoOficina = ReplaceVacio;
            string DireccionOficina = ReplaceVacio;
            string DepartamentoOficina = ReplaceVacio;
            string ProvinciaOficina = ReplaceVacio;
            string Local = ReplaceVacio;
            string Rum = ReplaceVacio;
            string NumeroBoleta = ReplaceVacio;
            string Frecuencia = ReplaceVacio;
            string AnioPeriodoFactura = ReplaceVacio;
            string MesPeriodoFactura = ReplaceVacio;
            string FechaPeriodo = ReplaceVacio;
            string FechaFormato = ReplaceVacio;
            string CodigoLicencia = IdLicencia.ToString();
            string CodigoSHow = ReplaceVacio;
            string FechaIni = ReplaceVacio;
            string FechaFin = ReplaceVacio;
            string FechaEmisionPlanilla = ReplaceVacio;
            string Modalidad = ReplaceVacio;
            string GrupoModalidad = ReplaceVacio;
            string DescripcionArtistas="";
            string Artistas="";
            string DescripcionFila1 = "";
            string DescripcionFila2 = "";
            string DescripcionFila1A = "";
            string DescripcionFila2B = "";
            string RazonSocial = "";
            string DireccionRazonSocial = "Direccion :";
            string Documento1 = "";
            string IMporte1 = "";
            string Documento2 = "";
            string IMporte2 = "";
            string TotalImporte = "";
            string correo = "Correo Electronico :";
            string FechaCancel1 = "";
            string FechaCancel2 = "";
            string NombreLicencia = "";

            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = new BLReporte().FechaActualShort();

            // var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var licencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER,IdLicencia);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdRum);
            var ParametroValueHz = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var planilla = new BLReporte().ObtenerDatosPlanilla(GlobalVars.Global.OWNER, IdLicencia, idReport);
            List<BEAutorizacion> Autorizacion = new BLAutorizacion().AutorizacionXLicencia(IdLicencia, GlobalVars.Global.OWNER).Where(x=>x.ENDS ==null).ToList();
            var MOntoFactura = new BLReporte().ObtieneFacturasxIdReporte(idReport);
            //comprobando correlativo
            var correlativo = new BLREC_NUMBERING().ObtenerCorrelativoXtipo(GlobalVars.Global.OWNER, "PL").NMR_NOW;

            if (licencia != null)
            {
                RazonSocial = licencia.RazonSocial;
                DireccionRazonSocial = licencia.DireccionRazonSocial;
                NombreLicencia = licencia.NombreLicencia;
            }

            if (MOntoFactura != null)
            {
                if (MOntoFactura.Count > 0)
                {
                    Documento1 = MOntoFactura.FirstOrDefault().DOCUMENTO;
                    IMporte1 =MOntoFactura.FirstOrDefault().MONTO.ToString();
                    FechaCancel1 = MOntoFactura.FirstOrDefault().FECHA_CANCEL;
                }
                if (MOntoFactura.Count > 1)
                {
                    Documento2 = MOntoFactura.LastOrDefault().DOCUMENTO;
                    IMporte2 = MOntoFactura.LastOrDefault().MONTO.ToString();
                    FechaCancel2 = MOntoFactura.LastOrDefault().FECHA_CANCEL;
                }
                TotalImporte = MOntoFactura.Sum(x => x.MONTO).ToString();
            }


            if (ParametroValue != null)
            {
                Rum = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }

            if (Autorizacion.Count>0 )
            {
                FechaIni = Autorizacion.FirstOrDefault().LIC_AUT_START.ToShortDateString();
                FechaFin = Autorizacion.FirstOrDefault().LIC_AUT_END.ToShortDateString();
            }

            if (planilla != null)
            {
                FechaEmisionPlanilla = planilla.LOG_DATE_CREAT.ToString();

                NumeroBoleta = planilla.REPORT_NUMBER == null ? ReplaceVacio : planilla.REPORT_NUMBER.ToString();//correlativo.ToString() == null ? ReplaceVacio : correlativo.ToString();

                CodigoSHow = planilla.SHOW.ToString();

                Modalidad = planilla.MODALIDAD;
                GrupoModalidad = planilla.GRUPO_MODALIDAD;

                if (ParametroValueHz != null && ParametroValueHz.PAR_VALUE != null) Frecuencia = ParametroValueHz.PAR_VALUE;


                MesPeriodoFactura = planilla.LIC_MONTH == null ? ReplaceVacio : planilla.LIC_MONTH.ToString();

                MesPeriodoFactura = MesPeriodoFactura.Equals('0') ? planilla.MES_EVENTO : planilla.MES_EVENTO;
                AnioPeriodoFactura = planilla.LIC_YEAR == null ? ReplaceVacio : planilla.LIC_YEAR.ToString();


               var  descripplanilla = new BLReporte().ObtieneDescripcionxModalidad(planilla.MOG_ID);
               var descripcionFila1 = new BLReporte().ObtieneDescripcionxModalidad2(planilla.MOG_ID,planilla.MOD_ID);
               var descripcionFila2 = new BLReporte().ObtieneDescripcionxModalidad3(planilla.MOG_ID, planilla.MOD_ID);

                if (descripplanilla != null)
                {
                    DescripcionArtistas = descripplanilla.MODALIDAD; //descripcion de la planilla
                    if(descripplanilla.ARTISTA_DESC.Equals("1")) // si tiene que mostrar artistas 
                        Artistas = planilla.ARTISTA_DESC;
                    else
                        Artistas = ""; // no muestra
                }
                else
                {
                    DescripcionArtistas = "";
                    Artistas = "";
                }

                if (descripcionFila1 != null)
                {
                    DescripcionFila1 = descripcionFila1.MODALIDAD; //descripcion de la planilla
                    DescripcionFila1A = descripcionFila1.ARTISTA_DESC;

                }

                if (descripcionFila2 != null)
                {
                    DescripcionFila2 = descripcionFila2.MODALIDAD; //descripcion de la planilla
                    DescripcionFila2B = descripcionFila2.ARTISTA_DESC;

                }


                switch (MesPeriodoFactura)
                {

                    case "1":
                        FechaPeriodo = "Enero";
                        break;
                    case "2":
                        FechaPeriodo = "Febrero";
                        break;
                    case "3":
                        FechaPeriodo = "Marzo";
                        break;
                    case "4":
                        FechaPeriodo = "Abril";
                        break;
                    case "5":
                        FechaPeriodo = "Mayo";
                        break;
                    case "6":
                        FechaPeriodo = "Junio";
                        break;
                    case "7":
                        FechaPeriodo = "Julio";
                        break;
                    case "8":
                        FechaPeriodo = "Agosto";
                        break;
                    case "9":
                        FechaPeriodo = "Setiembre";
                        break;
                    case "10":
                        FechaPeriodo = "Octubre";
                        break;
                    case "11":
                        FechaPeriodo = "Noviembre";
                        break;
                    case "12":
                        FechaPeriodo = "Diciembre";
                        break;
                    default:
                        break;
                }

                FechaFormato = FechaPeriodo + " " + AnioPeriodoFactura;

                if (Autorizacion.Count==0)
                {
                    FechaIni = FechaFormato;
                    FechaFin = FechaFormato;
                }
            }

            string Fecha = string.Empty;
            if (!string.IsNullOrEmpty(FechaCorta))
                Fecha = FechaCorta;
            else
                Fecha = DateTime.Now.ToShortDateString();


            if (establecimiento != null)
            {
                DireccionOficina = establecimiento.ADDRESS; //nuevo
                Local = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

                var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    ProvinciaOficina = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    DistritoOficina = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    DepartamentoOficina = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = NumeroBoleta == ReplaceVacio ? 0 : 1;
            val[1] = Local == ReplaceVacio ? 0 : 1;
            val[2] = FechaIni == ReplaceVacio ? 1 : 1 ;
            val[3] = DireccionOficina == ReplaceVacio ? 0 : 1;
            val[4] = DepartamentoOficina == ReplaceVacio ? 0 : 1;
            val[5] = ProvinciaOficina == ReplaceVacio ? 0 : 1;
            val[6] = DistritoOficina == ReplaceVacio ? 0 : 1;
            //val[7] = FechaPeriodo == ReplaceVacio ? 0 : 1;
            val[7] = FechaFin==ReplaceVacio? 1 : 1;
            // val[8] = Rum == ReplaceVacio ? 1 : 1;
            val[8] = CodigoSHow == ReplaceVacio ?  1: 1;
            val[9] = FechaEmisionPlanilla == ReplaceVacio ? 0 : 1;
            val[10] = Modalidad == ReplaceVacio ? 0 : 1;
            val[11] = GrupoModalidad == ReplaceVacio ? 0 : 1;
            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Número de boleta");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del local");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Falta Fecha Inicio de Autorizacion");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección de la oficina");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Departamento de la oficina");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Provincia de la oficina");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Distrito de la oficina");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Fecha Fecha Fin de Autorizacion");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Falta Asignar Show A la Licencia ");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro Fecha de Emision de la Planilla ");
                                break;
                            case 10:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro Modalidad Activa asociada a la Licencia");
                                break;
                            case 11:
                                GlobalVars.Global.ListMessageReport.Add("No se encontro Grupo de Modalidad asociada a la licencia");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@NumeroBoleta", NumeroBoleta);
                keyValues.Add("@FechaGiro", Fecha);
                keyValues.Add("@Establecimiento", Local);
                //keyValues.Add("@Frecuencia", Frecuencia);//falta
               // keyValues.Add("@Rum", Rum);
                keyValues.Add("@Direccion", DireccionOficina);
                keyValues.Add("@ Departamento", DepartamentoOficina);
                keyValues.Add("@Provincia", ProvinciaOficina);
                keyValues.Add("@Distrito", DistritoOficina);
                keyValues.Add("@Mes", FechaFormato);
               // keyValues.Add("@FactPre", " ");
              //  keyValues.Add("@FecFactPre", " ");
               // keyValues.Add("@FactLiq", " ");
              //  keyValues.Add("@FecFactLiq", " ");
              //  keyValues.Add("@Importe", " ");
              //  keyValues.Add("@Artistas", " ");
                keyValues.Add("@IdLic", CodigoLicencia);
                keyValues.Add("@Año", AnioPeriodoFactura);
                keyValues.Add("@Show", CodigoSHow);
                keyValues.Add("@Seq", NumeroBoleta);
                keyValues.Add("@IniAut", FechaIni);
                keyValues.Add("@FinAut", FechaFin);
                keyValues.Add("@GrupoModalidad", GrupoModalidad);
                keyValues.Add("@Modalidad", Modalidad);
                keyValues.Add("@Oficina", nombrOfi);
                keyValues.Add("@FecEmisionPla", FechaEmisionPlanilla);
                keyValues.Add("@Periodo", MesPeriodoFactura);
                keyValues.Add("@Anio", AnioPeriodoFactura);
                keyValues.Add("@DescArtista", DescripcionArtistas);
                keyValues.Add("@Interpretes ", Artistas);
                keyValues.Add("@Usuario", RazonSocial);
                keyValues.Add("@DirecciónRazSocial", DireccionRazonSocial);
                keyValues.Add("@Correo", correo);
                

                keyValues.Add("@NumeroBolet1", Documento1);
                keyValues.Add("@Importe1", IMporte1);
                keyValues.Add("@NumeroBolet2", Documento2);
                keyValues.Add("@Importe2", IMporte2);
                keyValues.Add("@Importe", TotalImporte);
                keyValues.Add("@FechaCancelación1", FechaCancel1);
                keyValues.Add("@FechaCancelación", FechaCancel2);
                keyValues.Add("@NombreLicencia", NombreLicencia);

                keyValues.Add("@DescripcionFila1", DescripcionFila1);
                keyValues.Add("@DescripcionAfila", DescripcionFila1A);
                keyValues.Add("@DescripcionFila2", DescripcionFila2);
                keyValues.Add("@DescripcionBFila2", DescripcionFila2B);



                FindandReplace(destinationFile, keyValues);

                #region PruebaGenerarTablaDinamica
                //int rows = 3;
                //int cols = 4;
                //object oMissing = System.Reflection.Missing.Value;          
                //object oEndOfDoc = "\\endofdoc";
                //Microsoft.Office.Interop.Word._Application objWord;
                //Microsoft.Office.Interop.Word._Document objDoc;
                //objWord = new Microsoft.Office.Interop.Word.Application();
                //objWord.Visible = true;
                //objDoc = objWord.Documents.Add(ref oMissing, ref oMissing,
                //    ref oMissing, ref oMissing);

                //int i = 0;
                //int j = 0;
                //Microsoft.Office.Interop.Word.Table objTable;
                //Microsoft.Office.Interop.Word.Range wrdRng = objDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                //objTable = objDoc.Tables.Add(wrdRng, rows, cols, ref oMissing, ref oMissing);
                //objTable.Range.ParagraphFormat.SpaceAfter = 7;



                //string strText;
                //for (i = 1; i <= rows; i++)
                //    for (j = 1; j <= cols; j++)
                //    {
                //        strText = "Row" + i + " Coulmn" + j;
                //        objTable.Cell(i, j).Range.Text = strText;
                //    }
                //objTable.Rows[1].Range.Font.Bold = 2;
                //objTable.Rows[1].Range.Font.Italic = 1;
                //objTable.Borders.Shadow = true;            
                #endregion

                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);

                if (idSerie != null)
                {
                    //actualizando el correlativo de la planilla
                    var resultado1 = new BLREC_NUMBERING().ActualizarCorrelativoPlanilla(GlobalVars.Global.OWNER, correlativo, idReport, UsuarioActual);
                    //actualizar correlativo de planilla tabla REC_NUMBERING
                    var resultado2 = new BLRecibo().ActualizarSerie(GlobalVars.Global.OWNER, idSerie, "PL", UsuarioActual);
                    //actualizar estado de impresión tabla REC_LIC_AUT_ARTIST_REPORT
                    var resultado3 = new BLLicenciaReporte().ActualizarEstadoImpresion(GlobalVars.Global.OWNER, idReport);
                    //actualizar el numero de impresión 
                    var resultado4 = new BLLicenciaReporte().ActualizarNroImpresion(GlobalVars.Global.OWNER, idReport, UsuarioActual);

                }
            }

            return resultado;
        }

        public bool CrearReportePlanillaEjecicionCadenas(decimal IdLicencia, string nombreFile, string nombreFileCopy, string rutaPDF, decimal idSerie, decimal idReport)
        {
            int[] val = new int[10];
            bool resultado = true;

            string DistritoOficina = ReplaceVacio;
            string DireccionEstablecimiento = ReplaceVacio;
            string DepartamentoOficina = ReplaceVacio;
            string ProvinciaOficina = ReplaceVacio;
            string Local = ReplaceVacio;
            string Rum = ReplaceVacio;
            string NumeroBoleta = ReplaceVacio;
            string Frecuencia = ReplaceVacio;
            string AnioPeriodoFactura = ReplaceVacio;
            string MesPeriodoFactura = ReplaceVacio;
            string FechaPeriodo = ReplaceVacio;
            string FechaFormato = ReplaceVacio;
            string UsuarioDerecho = ReplaceVacio;
            string RUCUsuarioDerecho = ReplaceVacio;
            string CodLicencia = ReplaceVacio;
            string NumSerieFactura = ReplaceVacio;
            // string Periodo = ReplaceVacio;
            string Importe = ReplaceVacio;
            decimal lic_id = 0;
            decimal mes = 0;
            decimal anio = 0;
            string sourceFile = nombreFile;
            string destinationFile = nombreFileCopy;
            System.IO.File.Copy(sourceFile, destinationFile, true);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string FechaCorta = new BLReporte().FechaActualShort();

            // var oficina = new BLReporte().ObtenerDatosOficina(GlobalVars.Global.OWNER, IdLicencia);
            var licencia = new BLLicencias().ObtenerLicenciaXCodigo(IdLicencia, GlobalVars.Global.OWNER);
            var sociocab = new BLSocioNegocio().ObtenerDatos(licencia.BPS_ID, GlobalVars.Global.OWNER);
            var establecimiento = new BLReporte().ObtenerDatosEstablecimiento(GlobalVars.Global.OWNER, IdLicencia);
            var ParametroValue = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdRum);
            var ParametroValueHz = new BLReporte().ObtenerParametro(GlobalVars.Global.OWNER, IdLicencia, GlobalVars.Global.VarIdFrecuenciaRadios);
            var planilla = new BLReporte().ObtenerDatosPlanilla(GlobalVars.Global.OWNER, IdLicencia, idReport);


            //comprobando correlativo
            var correlativo = new BLREC_NUMBERING().ObtenerCorrelativoXtipo(GlobalVars.Global.OWNER, "PL").NMR_NOW;

            if (ParametroValue != null)
            {
                Rum = ParametroValue.PAR_VALUE == null ? ReplaceVacio : ParametroValue.PAR_VALUE;
            }
            if
                (sociocab != null)
            {
                UsuarioDerecho = sociocab.BPS_NAME;
                RUCUsuarioDerecho = sociocab.TAX_ID;
            }
            if (licencia != null)
            {
                CodLicencia = licencia.LIC_ID.ToString();
                lic_id = licencia.LIC_ID;
            }



            if (planilla != null)
            {
                //Rum = planilla.RUM == null ? ReplaceVacio : planilla.RUM;
                NumeroBoleta = planilla.REPORT_NUMBER == null ? ReplaceVacio : planilla.REPORT_NUMBER.ToString();//correlativo.ToString() == null ? ReplaceVacio : correlativo.ToString();

                if (ParametroValueHz != null && ParametroValueHz.PAR_VALUE != null) Frecuencia = ParametroValueHz.PAR_VALUE;


                MesPeriodoFactura = planilla.LIC_MONTH == null ? ReplaceVacio : planilla.LIC_MONTH.ToString();
                AnioPeriodoFactura = planilla.LIC_YEAR == null ? ReplaceVacio : planilla.LIC_YEAR.ToString();

                mes = planilla.LIC_MONTH;
                anio = planilla.LIC_YEAR;
                switch (MesPeriodoFactura)
                {
                    case "1":
                        FechaPeriodo = "Enero";
                        break;
                    case "2":
                        FechaPeriodo = "Febrero";
                        break;
                    case "3":
                        FechaPeriodo = "Marzo";
                        break;
                    case "4":
                        FechaPeriodo = "Abril";
                        break;
                    case "5":
                        FechaPeriodo = "Mayo";
                        break;
                    case "6":
                        FechaPeriodo = "Junio";
                        break;
                    case "7":
                        FechaPeriodo = "Julio";
                        break;
                    case "8":
                        FechaPeriodo = "Agosto";
                        break;
                    case "9":
                        FechaPeriodo = "Setiembre";
                        break;
                    case "10":
                        FechaPeriodo = "Octubre";
                        break;
                    case "11":
                        FechaPeriodo = "Noviembre";
                        break;
                    case "12":
                        FechaPeriodo = "Diciembre";
                        break;
                    default:
                        break;
                }

                FechaFormato = FechaPeriodo + " " + AnioPeriodoFactura;
            }

            var monto = new BLLicencias().ObtienePrimeraFacturaCandesPlanilla(lic_id, mes, anio);
            var numfact = new BLLicencias().ObtieneSerieNumFacturaCandenaPlanilla(lic_id, mes, anio);

            if (monto != null)
            {
                if (monto == 0)
                    Importe = "";
                else
                    Importe = monto.ToString();



            }
            if (numfact != null)
                NumSerieFactura = numfact;
            else
                NumSerieFactura = "";

            string Fecha = string.Empty;
            if (!string.IsNullOrEmpty(FechaCorta))
                Fecha = FechaCorta;
            else
                Fecha = DateTime.Now.ToShortDateString();

            if (establecimiento != null)
            {
                DireccionEstablecimiento = establecimiento.ADDRESS; //nuevo
                Local = establecimiento.EST_NAME == null ? ReplaceVacio : establecimiento.EST_NAME.Trim();

                var ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, establecimiento.TIS_N.ToString(), establecimiento.GEO_ID.ToString());

                if (ubigeo != null)
                {
                    //   ProvinciaOficina = ubigeo.Provincia == null ? ReplaceVacio : ubigeo.Provincia.Trim();
                    //   DistritoOficina = ubigeo.Distrito == null ? ReplaceVacio : ubigeo.Distrito.Trim();
                    //   DepartamentoOficina = ubigeo.Departamento == null ? ReplaceVacio : ubigeo.Departamento.Trim();
                }
            }

            GlobalVars.Global.ListMessageReport = new List<string>();

            val[0] = NumeroBoleta == ReplaceVacio ? 0 : 1;
            val[1] = Local == ReplaceVacio ? 0 : 1;
            val[2] = 1;
            val[3] = DireccionEstablecimiento == ReplaceVacio ? 0 : 1;
            val[4] = 1;
            val[5] = 1;
            val[6] = 1;
            val[7] = UsuarioDerecho == ReplaceVacio ? 0 : 1;
            val[8] = CodLicencia == ReplaceVacio ? 0 : 1;
            // val[8] = Rum == ReplaceVacio ? 1 : 1;
            val[9] = 1;

            var validacion = val.Contains(0);

            if (validacion)
            {
                resultado = false;

                int i = -1;
                foreach (var item in val.ToList())
                {
                    i++;
                    if (item == 0)
                    {
                        switch (i)
                        {
                            case 0:
                                GlobalVars.Global.ListMessageReport.Add("Número de boleta");
                                break;
                            case 1:
                                GlobalVars.Global.ListMessageReport.Add("Nombre del local");
                                break;
                            case 2:
                                GlobalVars.Global.ListMessageReport.Add("Rum");
                                break;
                            case 3:
                                GlobalVars.Global.ListMessageReport.Add("Dirección del Establecimiento(UBIGEO)");
                                break;
                            case 4:
                                GlobalVars.Global.ListMessageReport.Add("Departamento del Establecimiento(UBIGEO)");
                                break;
                            case 5:
                                GlobalVars.Global.ListMessageReport.Add("Provincia del  Establecimiento(UBIGEO)");
                                break;
                            case 6:
                                GlobalVars.Global.ListMessageReport.Add("Distrito de la oficina");
                                break;
                            case 7:
                                GlobalVars.Global.ListMessageReport.Add("Usuario de Derecho");
                                break;
                            case 8:
                                GlobalVars.Global.ListMessageReport.Add("Codigo de Licencia no Encontrado");
                                break;
                            case 9:
                                GlobalVars.Global.ListMessageReport.Add("Frecuencia");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
                resultado = true;

            if (resultado)
            {
                keyValues.Add("@NumeroBoleta", NumeroBoleta);
                keyValues.Add("@FechaGiro", Fecha);
                keyValues.Add("@Establecimiento", Local);
                keyValues.Add("@Frecuencia", Frecuencia);//falta
                keyValues.Add("@Rum", Rum);
                keyValues.Add("@Direccion", DireccionEstablecimiento);
                keyValues.Add("@Departamento", DepartamentoOficina);
                keyValues.Add("@Provincia", ProvinciaOficina);
                keyValues.Add("@Localidad", DistritoOficina);
                keyValues.Add("@Periodo", FechaFormato);
                keyValues.Add("@FactPre", NumSerieFactura);
                keyValues.Add("@FecFactPre", " ");
                keyValues.Add("@FactLiq", " ");
                keyValues.Add("@FecFactLiq", " ");
                keyValues.Add("@Artistas", "");
                keyValues.Add("@Usuario", UsuarioDerecho);
                keyValues.Add("@codigoLicencia", CodLicencia);
                keyValues.Add("@Importe", Importe);


                FindandReplace(destinationFile, keyValues);


                string RutaWord = destinationFile;
                Convert(RutaWord, rutaPDF, WdSaveFormat.wdFormatPDF);

                if (idSerie != null)
                {
                    //actualizando el correlativo de la planilla
                    var resultado1 = new BLREC_NUMBERING().ActualizarCorrelativoPlanilla(GlobalVars.Global.OWNER, correlativo, idReport, UsuarioActual);
                    //actualizar correlativo de planilla tabla REC_NUMBERING
                    var resultado2 = new BLRecibo().ActualizarSerie(GlobalVars.Global.OWNER, idSerie, "PL", UsuarioActual);
                    //actualizar estado de impresión tabla REC_LIC_AUT_ARTIST_REPORT
                    var resultado3 = new BLLicenciaReporte().ActualizarEstadoImpresion(GlobalVars.Global.OWNER, idReport);
                    //actualizar el numero de impresión 
                    var resultado4 = new BLLicenciaReporte().ActualizarNroImpresion(GlobalVars.Global.OWNER, idReport, UsuarioActual);

                }
            }

            return resultado;
        }


        //public static string NumeroALetrasDOC(string num)
        //{
        //    string res, dec = "";
        //    Int64 entero;
        //    int decimales;
        //    double nro;

        //    try
        //    {
        //        nro = Convert.ToDouble(num);
        //    }
        //    catch
        //    {
        //        return "";
        //    }

        //    entero = Convert.ToInt64(Math.Truncate(nro));
        //    decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

        //    if (decimales > 0)
        //        dec = " CON " + decimales.ToString() + "/100";
        //    else
        //        dec = " CON 00/100";
        //    string TipoMoneda = " SOLES.";

        //    res = NumeroALetras(Convert.ToDouble(entero)) + dec + TipoMoneda;
        //    return res;
        //}
        private static string NumeroALetras(decimal value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + NumeroALetras(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                Num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return Num2Text;
        }



        #endregion




        public System.Drawing.Image GenerarQR(decimal IdLicencia)
        {
            #region QR GENERATE

            var texto = new BLReporte().ObtieneInfoxLicencia(IdLicencia);

            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(texto, out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemporal = new Bitmap(ms);
            var img = new Bitmap(imageTemporal, new Size(new System.Drawing.Point(50, 50)));
            //var img = new Bitmap(imageTemporal, new Size(new System.Drawing.Point(200, 200)));

            System.Drawing.Image imagen = img;

            #endregion

            return imagen;
        }


        public void FindandReplaceWithImage(string fileDoc, Dictionary<string, string> param, System.Drawing.Image imagen,int Range)
        {
            string temPath = GlobalVars.Global.RutaPlantillaLicencia +"imagen.png";
            Word._Application oWord = new Word.Application();
            oWord.Visible = false;
            object oMissing = System.Reflection.Missing.Value;
            object isVisible = false;
            object readOnly = false;
            object oInput = fileDoc;
            object oOutput = oMissing;
            object oFormat = oMissing;
            Word._Document doc = oWord.Documents.Open(ref oInput, ref oMissing, ref readOnly,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            doc.Activate();

            foreach (KeyValuePair<string, string> item in param)
            {
                Word.Range myStoryRange = doc.Range();

                //First search the main document using the Selection
                Word.Find myFind = myStoryRange.Find;
                myFind.Text = item.Key;
                myFind.Replacement.Text = item.Value;
                myFind.Forward = true;
                myFind.Wrap = Word.WdFindWrap.wdFindContinue;
                myFind.Format = false;
                myFind.MatchCase = false;
                myFind.MatchWholeWord = false;
                myFind.MatchWildcards = false;
                myFind.MatchSoundsLike = false;
                myFind.MatchAllWordForms = false;
                myFind.Execute(Replace: Word.WdReplace.wdReplaceAll);

            }

            //Image img = resizeImage();
            imagen.Save(temPath);
            object oMissed = doc.Paragraphs[Range].Range; // 6 autorizacion 5 locales
            object oLinkTofile = false;
            Object oSaveWithDocument = true;
            doc.InlineShapes.AddPicture(temPath,ref oLinkTofile,ref oSaveWithDocument,ref oMissed);


            doc.Save();
            doc.Close();
            oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
        }
    }
}
