using SGRD.Componente.Integracion;
using SGRDA.Servicios.Proxy.Entidad;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using ucLogApp;

namespace SGRDA.Servicio.PrintLocal
{
    public partial class Service1 : ServiceBase
    {

        //string K_OWNER="APD";
        string K_PREF1 = "";
        string K_PREF2 = "";
        string FormatoNameDoc =""; 
        string NombImpresora = "";
        string LeerNombImpresoraConfig = "";
        private Font fuente = new Font("Arial", 9);
        
        int tmpContador = 0;
        string fileSentPrint = "";

        string localhost = string.Empty; 
        List<SGRDA.Servicios.Proxy.Entidad.PreImpresion> docsImprimir;
        System.Timers.Timer myTimer =null;
        System.Timers.Timer myTimer2 = null;

        public bool flgInitTimer1 = false;
        public bool flgInitTimer2 = false;

        public bool validarVariables()
        {
            bool flgVal = false;
            string msj = string.Empty;

            if (System.Configuration.ConfigurationManager.AppSettings["PrefijoFact1"] != null)
            {
                K_PREF1 = System.Configuration.ConfigurationManager.AppSettings["PrefijoFact1"];
                flgVal = true;
            }
            else
            {
                msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable PrefijoFact1, no está inicializada en el App.Config \n";
            }
            if (System.Configuration.ConfigurationManager.AppSettings["PrefijoFact2"] != null )
            {
                K_PREF2 = System.Configuration.ConfigurationManager.AppSettings["PrefijoFact2"];
            }
            else
            {
                msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable PrefijoFact2, no está inicializada en el App.Config \n";
                flgVal = false;
            }

            if (System.Configuration.ConfigurationManager.AppSettings["PrintName"] != null )
            {
                NombImpresora = System.Configuration.ConfigurationManager.AppSettings["PrintName"];
            }
            else
            {
                msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable PrintName, no está inicializada en el App.Config.  Se requiere inicializar esta variable al no encontrar una impresora predetermianda en la pc local. \n";
                flgVal = false;
            }

            if (System.Configuration.ConfigurationManager.AppSettings["ReadPrintName"] != null )
            {
                LeerNombImpresoraConfig = System.Configuration.ConfigurationManager.AppSettings["ReadPrintName"];
            }
            else
            {
                msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable LeerNombImpresoraConfig, no está inicializada en el App.Config.  \n ";
                flgVal = false;
            }

            if (!flgVal) {
                ucLog.GrabarLogTexto(msj);
            }
            return flgVal;
        }

        Impresion impresion; 
        public Service1()
        {
            
            InitializeComponent();

            bool flg = validarVariables();
         
            if (flg)
            {
                impresion = new Impresion();
                FormatoNameDoc = K_PREF1 + "-{0}-" + K_PREF2 + "-{1}";
                localhost = impresion.getIpv4(Dns.GetHostName());
                if (LeerNombImpresoraConfig.ToUpper() == "N") {
                    var printname = impresion.GetDefaultPrinterName();
                    if (!string.IsNullOrEmpty(printname)) NombImpresora = printname;// caso contrario busca Print configuarada en app.config
                }

                string msj = string.Empty;
                if (System.Configuration.ConfigurationManager.AppSettings["EjecutarImpresion1"] != null)
                {
                    int time1 = 0;
                    bool flg1 = Int32.TryParse(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EjecutarImpresion1"]), out time1);

                    if (flg1)
                    {
                        if (time1 >= 1000)
                        {
                            myTimer = new System.Timers.Timer(time1);
                            myTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
                            flgInitTimer1 = true;
                        }
                        else
                        {
                            msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " El valor de la variable EjecutarImpresion1 debe ser mayor a 1000 en el App.Config";
                            ucLog.GrabarLogTexto(msj);
                        }
                    }
                    else
                    {
                        msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable EjecutarImpresion1, no contiene el valor esperado en el App.Config";
                        ucLog.GrabarLogTexto(msj);
                    }
                }
                else
                {
                    msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " No existe la variable EjecutarImpresion1 en el App.Config";
                    ucLog.GrabarLogTexto(msj);
                }

                if (System.Configuration.ConfigurationManager.AppSettings["EjecutarImpresion2"] != null)
                {
                    int time2 = 0;
                    bool flg2 = Int32.TryParse(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EjecutarImpresion2"]), out time2);

                    if (flg2)
                    {
                        if (time2 >= 1000)
                        {
                            myTimer2 = new System.Timers.Timer(time2);
                            myTimer2.Elapsed += new ElapsedEventHandler(DisplayTimeEvent2);
                            flgInitTimer2 = true;
                        }
                        else
                        {
                            msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " El valor de la variable EjecutarImpresion2 debe ser mayor a 1000 en el App.Config";
                            ucLog.GrabarLogTexto(msj);
                        }
                    }
                    else
                    {
                        msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable EjecutarImpresion2, no contiene el valor esperado en el App.Config";
                        ucLog.GrabarLogTexto(msj);
                    }
                }
                else
                {
                    msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " No existe la variable EjecutarImpresion2 en el App.Config";
                    ucLog.GrabarLogTexto(msj);
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            if (flgInitTimer1 && flgInitTimer2)
            {
                string msj = string.Empty;
                if (System.Configuration.ConfigurationManager.AppSettings["TimeToLaunchEvento2"] != null)
                {
                    int time = 0;
                    bool flg = Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["TimeToLaunchEvento2"], out time);
                    if (flg)
                    {
                        if (time >= 1000)
                        {
                            myTimer.Start();
                            Thread.Sleep(time);
                            myTimer2.Start();
                        }
                        else
                        {
                            msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " El valor de la variable TimeToLaunchEvento2 debe ser mayor a 1000 en el App.Config";
                            ucLog.GrabarLogTexto(msj);
                        }
                    }
                    else
                    {
                        msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable TimeToLaunchEvento2, no contiene el valor esperado en el App.Config";
                        ucLog.GrabarLogTexto(msj);
                    }
                }
                else
                {
                    msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable TimeToLaunchEvento2, existe en el App.Config";
                    ucLog.GrabarLogTexto(msj);
                }
            }
        }

        protected override void OnStop()
        {
            myTimer.Stop();
            myTimer2.Stop();
        }

        public   void DisplayTimeEvent2(object source, ElapsedEventArgs e)
        {
            try {
                impresion.checkPrintSuccessful(docsImprimir, tmpContador, NombImpresora, fileSentPrint, FormatoNameDoc, K_PREF1, K_PREF2, localhost);
            }
            catch (Exception ex) {
                ucLog.GrabarLogError("SRGRDA-SERVICIO WINDOWS", "LOCAL PRINT", "DisplayTimeEvent2", ex);
            }
            
        }
        public   void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                ImprimirTmp();
            }
            catch (Exception ex)
            {
                ucLog.GrabarLogError("SRGRDA-SERVICIO WINDOWS", "LOCAL PRINT", "DisplayTimeEvent", ex);
            }
            
        }

        void doc_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {

            impresion.checkPrintSuccessful(docsImprimir,tmpContador,NombImpresora,fileSentPrint,FormatoNameDoc,K_PREF1,K_PREF2,localhost);
        }
        void doc_PrintPage(object sender, PrintPageEventArgs ev)
        {

            impresion.PrintPage(ev, docsImprimir[tmpContador].ID_DOCUMENTO);
            //using (SGRDA.Servicios.Proxy.SEFacturaClient servFact = new Servicios.Proxy.SEFacturaClient())
            //{
            //    Factura factura = servFact.ObtenerFactura(docsImprimir[tmpContador].ID_DOCUMENTO);
            //    if (factura != null && factura.Detalle != null)
            //    {
            //        float pos_x = 100;
            //        float pos_y = 10;
            //        var socio = factura.RazonSocial;
            //        var direccion = factura.Direccion;
            //        var RUM = factura.RUM;
            //        var RUC = factura.RUC;
            //        var local = "-" + docsImprimir[tmpContador].ID_DOCUMENTO;
            //        string numero = Convert.ToString(factura.NumFact);
            //        var factor = 17;

            //        pos_x = 110;
            //        pos_y = 160;

            //        ev.Graphics.DrawString(numero, fuente, Brushes.Black, pos_x + 350, (pos_y - 20), new StringFormat());
            //        ev.Graphics.DrawString(DateTime.Now.ToString(), fuente, Brushes.Black, pos_x + 370, (pos_y), new StringFormat());
            //        ev.Graphics.DrawString(RUM, fuente, Brushes.Black, pos_x, (pos_y), new StringFormat());
            //        ev.Graphics.DrawString(socio, fuente, Brushes.Black, pos_x, (pos_y + (factor * 1)), new StringFormat());
            //        ev.Graphics.DrawString(RUC, fuente, Brushes.Black, pos_x, (pos_y + (factor * 2)), new StringFormat());
            //        ev.Graphics.DrawString(local, fuente, Brushes.Black, pos_x, (pos_y + (factor * 3)), new StringFormat());
            //        ev.Graphics.DrawString(direccion, fuente, Brushes.Black, pos_x, (pos_y + (factor * 4)), new StringFormat());

            //        factor = 20;
            //        var pos_x_deta_ini = 1;
            //        var pos_y_deta_ini = (pos_y + (factor * 5));

            //        int i = 1;
            //        foreach (var item in factura.Detalle)
            //        {
            //            var valor = 100 * i;
            //            var fila = (pos_y_deta_ini + (factor * i));
            //            ev.Graphics.DrawString(Convert.ToString(item.Item), fuente, Brushes.Black, pos_x_deta_ini, fila, new StringFormat());
            //            ev.Graphics.DrawString(item.Descripcion, fuente, Brushes.Black, pos_x_deta_ini + 70, fila, new StringFormat());
            //            ev.Graphics.DrawString(String.Format("{0:0.##}", item.SubTotal), fuente, Brushes.Black, pos_x_deta_ini + 650, fila, new StringFormat());

            //            i++;
            //        }
            //        ev.Graphics.DrawString(String.Format("{0:0.##}", factura.Total), fuente, Brushes.Black, pos_x_deta_ini + 650, 440, new StringFormat());

            //    }

            //}

        }

          private void ImprimirTmp()
          {

              tmpContador = 0;
              using (SGRDA.Servicios.Proxy.SEPreImpresionClient preImp = new SGRDA.Servicios.Proxy.SEPreImpresionClient())
              {
                  docsImprimir =preImp.ListarPendientes(localhost).ToList();
                  if (docsImprimir != null && docsImprimir.Count > 0)
                  {
                      foreach (var item in docsImprimir)
                      {
                          bool existeFilPrinting = false;
                          fileSentPrint = string.Format(FormatoNameDoc, item.ID_DOCUMENTO, item.ID);
                          bool idPrinterOnline;
                          List<KeyValuePair<string, string>> jobs = impresion.GetPrintJobsCollection(NombImpresora, fileSentPrint, out idPrinterOnline, false);
                          /*Verifica si cola de impresion */
                          if (jobs != null && jobs.Count > 0) existeFilPrinting = true;

                          /*Verifica si ya existe un documento en cola con el mismo nombre. De ser asi no imprime */
                          if (!existeFilPrinting)
                          {
                              PrintDocument doc = new PrintDocument();

                              doc.PrintPage += doc_PrintPage;
                              doc.QueryPageSettings += doc_QueryPageSettings;
                              doc.DocumentName = fileSentPrint;
                              doc.PrinterSettings.PrinterName = NombImpresora;
                              Margins margins = new Margins(20, 10, 20, 20);
                              doc.DefaultPageSettings.Margins = margins;
                              doc.OriginAtMargins = true;

                              if (doc.PrinterSettings.IsValid)
                              {
                                  doc.Print();
                              }
                             
                              tmpContador++;
                          }
                      }
                  }
              }

          }

          //public static class Constante
          //{
          //    public class EstadoImpresion
          //    {
          //        public static string FINALIZADO = "FIN";
          //        public static string ERROR = "ERR";
          //        public static string CANCELADO = "CAN";
          //        public static string ANULADO = "ANU";
          //        public static string PENDIENTE = "PEN";
          //        public static string BLOQUEADO = "BLO";
          //        public static string OFFLINE_PRINT = "OFF";
          //        public static string NO_JOB = "NOJ";
          //        public static string OFFLINE_PRINT2 = "OF2";
          //    }
          //}

          //private  void checkPrintSuccessful()
          //{

          //    if (docsImprimir != null && docsImprimir.Count > 0 && tmpContador < docsImprimir.Count)
          //    {
          //        bool idPrinterOnline;
          //        List<KeyValuePair<string, string>> jobs = GetPrintJobsCollection(NombImpresora, fileSentPrint, out idPrinterOnline);

          //        var idActualizar = docsImprimir[tmpContador].ID;
          //        var idDoc = docsImprimir[tmpContador].ID_DOCUMENTO;
                
          //        if (idPrinterOnline)
          //        {
          //            if (jobs.Count > 0)
          //            {
          //                for (int i = 0; i < jobs.Count; i++)
          //                {
          //                    var nombre = jobs[i];

          //                    string[] param = nombre.Key.Split('-');

          //                    if (param.Length == 4 && param[0] == K_PREF1 && param[2] == K_PREF2)
          //                    {
          //                        if (docsImprimir[tmpContador] != null &&
          //                            string.Format(FormatoNameDoc, idDoc, idActualizar) == string.Format(FormatoNameDoc, param[1], param[3]))
          //                        {

          //                            bool existeQueAndPend = false;
          //                            using (SGRDA.Servicios.Proxy.SEPreImpresionClient servPrePrint = new Servicios.Proxy.SEPreImpresionClient())
          //                            {
          //                                List<SGRDA.Servicios.Proxy.Entidad.PreImpresion> docsImprimirVal = servPrePrint.ListarPendientes(localhost).ToList();
          //                                existeQueAndPend = docsImprimirVal.Exists(x => x.ID_DOCUMENTO == docsImprimir[tmpContador].ID_DOCUMENTO);
          //                            }

          //                            string[] EstadoJob = nombre.Value.Split('*');
          //                            string estadoPrint = EstadoJob[0];
          //                            int idJob = Convert.ToInt32(EstadoJob[1]);

          //                            if ((estadoPrint == "5" || estadoPrint == "OK") && existeQueAndPend)
          //                            {
          //                                /*VALIDAR SI LA IMPRESORA ESTA ACTIVA LUEGO DE PASAR LAS VALIDACIONES DE IMPRESION COMO  OFFLINE.*/
          //                                bool isPrinterOnline = false;
          //                                var result = ImpresoraActiva(NombImpresora, isPrinterOnline);
          //                                if (result)
          //                                {   /*SI ENTRÓ A ESTA VALIDACION SE INTERPRETA QUE LA IMPRESORA EESTUVO ACTIVA Y SE IMPRIMIÓ CORRECTAMENTE*/
          //                                    ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.FINALIZADO);
          //                                }
          //                                else
          //                                {
          //                                    ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.OFFLINE_PRINT2);
          //                                }
          //                            }
          //                            else
          //                            {
          //                                ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.ERROR);
          //                            }
          //                        }
          //                        else
          //                        {
          //                            ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.ANULADO);
          //                        }
          //                    }
          //                    else
          //                    {
          //                        ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.CANCELADO);
          //                    }
          //                }
          //            }
          //            else
          //            {
          //                ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.NO_JOB);
          //            }
          //        }
          //        else
          //        {
          //            ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.OFFLINE_PRINT);

          //        }
          //    }
          //}

          //private  void ActualizarTMPImpresion( decimal idActualizar, decimal idDoc, string estado)
          //{
          //     using (SGRDA.Servicios.Proxy.SEPreImpresionClient servPrePrint = new Servicios.Proxy.SEPreImpresionClient())
          //    {
          //        PreImpresion objs = servPrePrint.ObtenerPreImpresion(idActualizar);
          //        if (objs != null && objs.FECHA_IMP == null)
          //        {
          //            servPrePrint.ActualizarEstado(idActualizar, localhost, estado);
          //        }
          //    }
          //}

          //static string getIpv4(string host)
          //{
          //    string IP4Address = "";
          //    foreach (IPAddress IPA in Dns.GetHostAddresses(host))
          //    {
          //        if (IPA.AddressFamily.ToString() == "InterNetwork")
          //        {
          //            IP4Address = IPA.ToString();
          //            break;
          //        }
          //    }
          //    return IP4Address;
          //}

          //public   StringCollection GetPrintersCollection()
          //{
          //    StringCollection printerNameCollection = new StringCollection();
          //    string searchQuery = "SELECT * FROM Win32_Printer";
          //    ManagementObjectSearcher searchPrinters =
          //          new ManagementObjectSearcher(searchQuery);
          //    ManagementObjectCollection printerCollection = searchPrinters.Get();
          //    foreach (ManagementObject printer in printerCollection)
          //    {
          //        printerNameCollection.Add(printer.Properties["Name"].Value.ToString());
          //    }
          //    return printerNameCollection;
          //}

          //public   List<KeyValuePair<string, string>> GetPrintJobsCollection(string printerName, string documentoPrint, out bool isPrinterOnline, bool validatePrinOnline = true)
          //{
          //    List<KeyValuePair<string, string>> printJobCollection = new List<KeyValuePair<string, string>>();

          //    if (validatePrinOnline)
          //    {
          //        isPrinterOnline = false;
          //        isPrinterOnline = ImpresoraActiva(printerName, isPrinterOnline);
          //    }
          //    else
          //    {
          //        isPrinterOnline = true;
          //    }
          //    if (isPrinterOnline)
          //    {
          //        string searchQuery = string.Format("SELECT * FROM Win32_PrintJob   WHERE   Document='{0}' ", documentoPrint);
          //        ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(searchQuery);
          //        ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();

          //        foreach (ManagementObject prntJob in prntJobCollection)
          //        {
          //            string jobName = prntJob.Properties["Name"].Value.ToString();
          //            string documentName = prntJob.Properties["Document"].Value.ToString();

          //            if (fileSentPrint == documentName)
          //            {
          //                char[] splitArr = new char[1];
          //                splitArr[0] = Convert.ToChar(",");
          //                string prnterName = jobName.Split(splitArr)[0];
          //                string estado = prntJob.Properties["status"].Value.ToString();
          //                string m_JobID = prntJob.Properties["JobId"].Value.ToString();
          //                if (String.Compare(prnterName, printerName, true) == 0)
          //                {
          //                    printJobCollection.Add(new KeyValuePair<string, string>(documentName, string.Format("{0}*{1}", estado, m_JobID)));
          //                }
          //            }
          //        }
          //    }
          //    return printJobCollection;
          //}

          //private bool ImpresoraActiva(string printerName, bool isPrinterOnline)
          //{
          //    isPrinterOnline = false;
          //    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
          //    string xprinterName = "";
          //    foreach (ManagementObject printer in searcher.Get())
          //    {
          //        xprinterName = printer["Name"].ToString().ToLower();
          //        if (xprinterName.Equals(printerName.ToLower()))
          //        {
          //            if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
          //            {
          //                isPrinterOnline = false;
          //            }
          //            else
          //            {
          //                isPrinterOnline = true;
          //            }
          //        }
          //    }
          //    return isPrinterOnline;
          //}
          //public  string GetDefaultPrinterName()
          //{
          //    var query = new ObjectQuery("SELECT * FROM Win32_Printer");
          //    var searcher = new ManagementObjectSearcher(query);

          //    foreach (ManagementObject mo in searcher.Get())
          //    {
          //        if (((bool?)mo["Default"]) ?? false)
          //        {
          //            return mo["Name"] as string;
          //        }
          //    }

          //    return null;
          //}
    }
}
