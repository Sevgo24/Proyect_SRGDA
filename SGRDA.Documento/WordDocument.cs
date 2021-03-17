using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Word;
using SGRDA.BL.Reporte;
using SGRDA.Entities.Reporte;
using System.Linq;
using System.Text;

namespace SGRDA.Documento
{
    public class WordDocument
    {
        private Word.Application wordApp;
        private Word.Document aDoc;
        private object missing = Missing.Value;
        private object filename;
        const string NomAplicacion = "SGRDA";
        const string UsuarioActual = "ADMIN";

        public void reporteBaileEspectaculos()
        {
            try
            {
                //archivo original
                //string docPath = System.AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Formatos\\Documentos\\BailesEspectaculos.docx";
                string docPath = GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculos.docx";
      
                //archivo copia
                //string ruta = System.AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Formatos\\Documentos\\";
                string ruta = GlobalVars.Global.DocumentoBailesEspectaculos;
                string documento = "BailesEspectaculoscopia.docx";

                System.IO.File.Copy(docPath, Path.Combine(ruta, documento), true);

                object fileName = Path.Combine(ruta, documento);

                Word.Application wordApp = new Microsoft.Office.Interop.Word.Application { Visible = true };
                Word.Document aDoc = wordApp.Documents.Open(fileName, ReadOnly: false, Visible: true);
                wordApp.Visible = false;
                aDoc.Activate();

                Replace(wordApp, "@Autorizado", "INVERSIONES CYE SAC");
                Replace(wordApp, "@NumDocAut", "20553343586");
                Replace(wordApp, "@Representante", "LUCERO STHEPAHIE MENESES HERNANDEZ");
                Replace(wordApp, "@NumDocRep", "45232048");
                Replace(wordApp, "@DirRep", "JR.LAS PERAS 218 URB. NARANJAL - INDEPENDENCIA");
                Replace(wordApp, "@DistritoRep", "INDEPENDENCIA");
                Replace(wordApp, "@Evento", "FIESTA ROCK");
                Replace(wordApp, "@Artistas", "6 VOLTIOS, AMEN, BANDA NI VOZ NI VOTO, CHABELOS, DANIEL F, DIFONIA, EL TRI DE MEXICO, GRUPO PANDA, GRUPO RIO, INYECTORES, JORGE GONZALEZ, LEUSEMIA, LIBIDO, LOS MOJARRAS, MAR DE COPAS, PSICOS");
                Replace(wordApp, "@Fechas", "25/02/2014");
                Replace(wordApp, "@Local", "PARQUE DE LA EXPOSICION");
                Replace(wordApp, "@DireccionEvento", "AV.28 DE JULIO S/N");
                Replace(wordApp, "@DistritoEvento", "LIMA");
                Replace(wordApp, "@ImporteLong", "20,720.70 (VEINTE MIL SETECIENTOS VEINTE SOLES 70/100 N.S.)");
                Replace(wordApp, "@ImporteShort", "(S/. 20,720.70)");
                Replace(wordApp, "@AUMP", "20141227E1277L221176");
                aDoc.Save();
                object saveChanges = Word.WdSaveOptions.wdSaveChanges;
                wordApp.Quit(ref saveChanges, ref missing, ref missing);

                //Convert(System.AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Formatos\\Documentos\\BailesEspectaculoscopia.docx", System.AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Formatos\\Documentos\\BailesEspectaculoscopia.pdf", WdSaveFormat.wdFormatPDF);
                Convert(GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.docx", GlobalVars.Global.DocumentoBailesEspectaculos + "BailesEspectaculoscopia.pdf", WdSaveFormat.wdFormatPDF);

                //eliminando archivo word copia
                //string wordcopia = System.AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Formatos\\Documentos\\BailesEspectaculoscopia.docx";
                //string pdf = System.AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Formatos\\Documentos\\BailesEspectaculoscopia.pdf";

                //if (System.IO.File.Exists(wordcopia))
                //{
                //    System.IO.File.Delete(wordcopia);
                //}
                //if (System.IO.File.Exists(pdf))
                //{
                //    System.IO.File.Delete(pdf);
                //}
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "BailesEspectaculoscopia", ex);
            }
        }

        public void reporteFichaInscripcion()
        {
            try
            {
                //archivo original
                //string docPath = System.AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Formatos\\Documentos\\FichaInscripcion.docx";
                string docPath = GlobalVars.Global.DocumentoFichaInscripcion + "FichaInscripcion.docx";

                //archivo copia
                //string ruta = System.AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Formatos\\Documentos\\";
                string ruta = GlobalVars.Global.DocumentoFichaInscripcion;
                string documento = "FichaInscripcioncopia.docx";

                System.IO.File.Copy(docPath, Path.Combine(ruta, documento), true);
                object fileName = Path.Combine(ruta, documento);

                Word.Application wordApp = new Microsoft.Office.Interop.Word.Application { Visible = true };
                Word.Document aDoc = wordApp.Documents.Open(fileName, ReadOnly: false, Visible: true);
                wordApp.Visible = false;
                aDoc.Activate();

                //prueba format texto--------------------------------------------

                //---------------------------------------------------------------

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
                BELicencia datosLicencia = new BELicencia();
                BELicencia valorunidadMusical = new BELicencia();
                List<BELicencia> nivelIncidenciaMusical = new List<BELicencia>();
                datosLicencia = new BLReporte().ObtenerDatosLicencia(GlobalVars.Global.OWNER, 70);
                valorunidadMusical = new BLReporte().ValorUnidadMusical(GlobalVars.Global.OWNER);
                nivelIncidenciaMusical = new BLReporte().NivelIncidenciaMusical(GlobalVars.Global.OWNER);

                if (datosLicencia != null)
                {
                    Replace(wordApp, "@CodigoRum", "080101-0716");
                    Replace(wordApp, "@Propietario", datosLicencia.RazonSocial);
                    Replace(wordApp, "@Ruc", datosLicencia.NumRazonSocial);
                    Replace(wordApp, "@Telefono", "");
                    Replace(wordApp, "@Fax", "");
                    Replace(wordApp, "@GiroLocal", datosLicencia.GiroLocal);
                    Replace(wordApp, "@NombreLocal", datosLicencia.NombreLocal);
                    Replace(wordApp, "@DireccionLocal", datosLicencia.DireccionLocal);
                    Replace(wordApp, "@Representante", datosLicencia.Representante);
                    Replace(wordApp, "@DireccionRep", datosLicencia.DireccionRepresentante);
                    Replace(wordApp, "@DepartamentoCob", "");
                    Replace(wordApp, "@ProvinciaCob", "");
                    Replace(wordApp, "@DistritoCob", "");
                    Replace(wordApp, "@DireccionCob", "");

                    if (!string.IsNullOrEmpty(datosLicencia.Tis_n_Establecimiento) && !string.IsNullOrEmpty(datosLicencia.Geo_id_Establecimiento))
                    {
                        ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, datosLicencia.Tis_n_Establecimiento, datosLicencia.Geo_id_Establecimiento);
                        Replace(wordApp, "@DepartamentoLocal", ubigeo.Departamento);
                        Replace(wordApp, "@ProvinciaLocal", ubigeo.Provincia);
                        Replace(wordApp, "@DistritoLocal", ubigeo.Distrito);
                    }

                    if (!string.IsNullOrEmpty(datosLicencia.Tis_n_Representante) && !string.IsNullOrEmpty(datosLicencia.Geo_id_Representante))
                    {
                        ubigeo = new BLReporte().ObtenerUbigeo(GlobalVars.Global.OWNER, datosLicencia.Tis_n_Representante, datosLicencia.Geo_id_Representante);
                        Replace(wordApp, "@DepartamentoRep", ubigeo.Departamento);
                        Replace(wordApp, "@ProvinciaRep", ubigeo.Provincia);
                        Replace(wordApp, "@DistritoRep", ubigeo.Distrito);
                    }
                }

                if (valorunidadMusical != null)
                {
                    var Mes = valorunidadMusical.Mes.Length == 1 ? ('0' + valorunidadMusical.Mes) : valorunidadMusical.Mes;
                    Replace(wordApp, "@Mes", Mes);
                    Replace(wordApp, "@Anio", valorunidadMusical.Anio);
                    Replace(wordApp, "@UM", valorunidadMusical.Valor.ToString());
                    ValorMusica = valorunidadMusical.Valor;
                }

                if (nivelIncidenciaMusical != null)
                {
                    Replace(wordApp, "@Indispensable", nivelIncidenciaMusical[0].NivelIncidencia);
                    Replace(wordApp, "@Necesaria", nivelIncidenciaMusical[1].NivelIncidencia);
                    Replace(wordApp, "@Secundaria", nivelIncidenciaMusical[2].NivelIncidencia);
                    Replace(wordApp, "@NIM", "1.20");
                    IncidenciaObra = 1.20;
                }

                //aforo del local            
                Replace(wordApp, "@A", "40");
                Replace(wordApp, "@M", "00");
                Replace(wordApp, "@H", "00");
                Replace(wordApp, "@T", "40");
                Replace(wordApp, "@YY", "24");
                AforoLocal = 24;

                //horas música al mes
                horasDias = 6;
                DiasMes = 25;
                HorasMusicaMes = horasDias * DiasMes;
                Replace(wordApp, "@HD", horasDias);
                Replace(wordApp, "@DM", DiasMes);
                Replace(wordApp, "@XX", HorasMusicaMes.ToString());

                //categoria del local
                CategoriaLocal = 0.24;
                Replace(wordApp, "@CL", CategoriaLocal.ToString());

                //medio de ejecucion
                MedioEjecucion = 0.12;
                Replace(wordApp, "@QQ", MedioEjecucion.ToString());

                //Tarifa mensual a pagar
                Tarifa = ValorMusica + IncidenciaObra + AforoLocal + HorasMusicaMes + CategoriaLocal + MedioEjecucion;
                Replace(wordApp, "@WW", Tarifa.ToString());
                //Replace(wordApp, "@TM", String.Format("{0:#,###.##}", Tarifa));
                //Replace(wordApp, "@TM", nivelIncidenciaMusical[2].NivelIncidencia); 
                //Replace(wordApp, "@Cargo", String.Format("{0:#,###.##}", Tarifa));

                aDoc.Save();
                object saveChanges = Word.WdSaveOptions.wdSaveChanges;
                wordApp.Quit(ref saveChanges, ref missing, ref missing);
                //Convert(System.AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Formatos\\Documentos\\FichaInscripcioncopia.docx", System.AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Formatos\\Documentos\\FichaInscripcioncopia.pdf", WdSaveFormat.wdFormatPDF);
                Convert(GlobalVars.Global.DocumentoFichaInscripcion + "FichaInscripcioncopia.docx", GlobalVars.Global.DocumentoFichaInscripcion + "FichaInscripcioncopia.pdf", WdSaveFormat.wdFormatPDF);
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "reporteFichaInscripcion", ex);
            }
        }

        public static void Convert(string input, string output, WdSaveFormat format)
        {
            try
            {
                // Create an instance of Word.exe
                Word._Application oWord = new Word.Application();
                // Make this instance of word invisible (Can still see it in the taskmgr).
                oWord.Visible = false;
                // Interop requires objects.
                object oMissing = System.Reflection.Missing.Value;
                object isVisible = true;
                object readOnly = false;
                object oInput = input;
                object oOutput = output;
                object oFormat = format;

                // Load a document into our instance of word.exe
                Word._Document oDoc = oWord.Documents.Open(ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                // Make this document the active document.
                oDoc.Activate();
                // Save this document in Word 2003 format.
                oDoc.SaveAs(ref oOutput, ref oFormat, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                // Always close Word.exe.
                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Convert", ex);
            }
        }

        private void Replace(Microsoft.Office.Interop.Word.Application doc, object findText, object replaceWithText)
        {
            try
            {
                object matchCase = false;
                object matchWholeWord = true;
                object matchWildCards = false;
                object matchSoundsLike = false;
                object matchAllWordForms = false;
                object forward = true;
                object format = false;
                object matchKashida = false;
                object matchDiacritics = false;
                object matchAlefHamza = false;
                object matchControl = false;
                object read_only = false;
                object visible = true;
                object replace = 2;
                object wrap = 1;
                doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                    ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                    ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
            }
            catch (Exception ex)
            {
                ucLogApp.ucLog.GrabarLogError(NomAplicacion, UsuarioActual, "Replace", ex);
            }
        }
    }
}
