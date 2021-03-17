using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.BL;
using SGRDA.Entities;
// using  Proyect_Apdayc.Clases;
//using Proyect_Apdayc.Clases.DTO;
using System.Data;
using System.Collections.Specialized;
using System.Management;

using ucLogApp;
using System.Timers;
using System.Drawing.Printing;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Printing;
using System.Printing.IndexedProperties;
using System.Collections;
using System.Configuration;
using SGRDA.Servicios.Proxy.Entidad;
using SGRD.Componente.Integracion;
namespace SGRDA.Test
{
    public class Program
    {
      

      
        static System.Timers.Timer myTimer = new System.Timers.Timer();
        static System.Timers.Timer myTimer2 = new System.Timers.Timer();

        static int codigoUser = 3;
        static string K_PREF1 = "INVOICE";
        static string K_PREF2 = "LYRICS";
        static string FormatoNameDoc = K_PREF1+"-{0}-"+K_PREF2+"-{1}";
        static string NombImpresora = "EPSON FX-2190 ESC/P";

        static private Font fuente = new Font("Arial", 9);
        static private string Nombre;
        static private string Direccion;
        static private string Telefono;

        static int veces = 0;

        static List<SGRDA.Servicios.Proxy.Entidad.PreImpresion> docsImprimir;
        static int tmpContador = 0;
        static string fileSentPrint = "";
        static Thread t = new Thread(ImprimirTmp);
        static int nn = 1;
        static int tiempo = 1;

        static Thread tPrint;

               public static bool flgInitTimer1 = false;
               public static bool flgInitTimer2 = false;
             static   string localhost;

             public static string GetDefaultPrinterName()
             {
                 var query = new ObjectQuery("SELECT * FROM Win32_Printer");
                 var searcher = new ManagementObjectSearcher(query);

                 foreach (ManagementObject mo in searcher.Get())
                 {
                     if (((bool?)mo["Default"]) ?? false)
                     {
                         return mo["Name"] as string;
                     }
                 }

                 return null;
             }
        static void Main(string[] args)
        {


           // WorkFlowBotRadio serv = new WorkFlowBotRadio();
            var DeudaFactura = new SGRDA.BL.Reporte.BLReporte().TotalDeudaFactura("APD", 2);

            Console.WriteLine("resultado de deuda :  {0}", DeudaFactura);
 

          //  BLREF_ROLES obj = new BLREF_ROLES();

         // Console.WriteLine("tiene acceso oficina 48 socio 28229 :  {0}",  obj.TienePermiso(48, 28229));
 
             // localhost = getIpv4(Dns.GetHostName());
           //  Console.WriteLine("{0}",GetDefaultPrinterName());

            //Program obj = new Program();
            //var tarifa = obj.ObtenerValorTestTarifa(105, 98);
            //Console.WriteLine("Formula tarifa: {0} ", tarifa.Formula);
            //Console.WriteLine("ValorFormula tarifa: {0} ", tarifa.ValorFormula);
            //Console.WriteLine("ValorMinimo tarifa: {0} ", tarifa.ValorMinimo);

         

            //myTimer.Enabled = true;
            //myTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            //myTimer.Interval = 10000;
            //myTimer.Start();


            //myTimer2.Enabled = true;
            //myTimer2.Elapsed += new ElapsedEventHandler(DisplayTimeEvent2);
            //myTimer2.Interval = 15000;
            //myTimer2.Start();



            //bool flg = validarVariables();

            //if (flg)
            //{
            //    string msj = string.Empty;
            //    if (System.Configuration.ConfigurationManager.AppSettings["EjecutarImpresion1"] != null)
            //    {
            //        int time1 = 0;
            //        bool flg1 = Int32.TryParse(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EjecutarImpresion1"]), out time1);

            //        if (flg1)
            //        {
            //            if (time1 >= 1000)
            //            {
            //                myTimer = new System.Timers.Timer(time1);
            //                myTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            //                myTimer.Start();
            //                flgInitTimer1 = true;
            //            }
            //            else
            //            {
            //                msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " El valor de la variable EjecutarImpresion1 debe ser mayor a 1000 en el App.Config";
            //                ucLog.GrabarLogTexto(msj);
            //            }
            //        }
            //        else
            //        {
            //            msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable EjecutarImpresion1, no contiene el valor esperado en el App.Config";
            //            ucLog.GrabarLogTexto(msj);
            //        }
            //    }
            //    else
            //    {
            //        msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " No existe la variable EjecutarImpresion1 en el App.Config";
            //        ucLog.GrabarLogTexto(msj);
            //    }

            //    if (System.Configuration.ConfigurationManager.AppSettings["EjecutarImpresion2"] != null)
            //    {
            //        int time2 = 0;
            //        bool flg2 = Int32.TryParse(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EjecutarImpresion2"]), out time2);

            //        if (flg2)
            //        {
            //            if (time2 >= 1000)
            //            {
            //                myTimer2 = new System.Timers.Timer(time2);
            //                myTimer2.Elapsed += new ElapsedEventHandler(DisplayTimeEvent2);
            //                myTimer2.Start();//eliminar en el servicio
            //                flgInitTimer2 = true;
            //            }
            //            else
            //            {
            //                msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " El valor de la variable EjecutarImpresion2 debe ser mayor a 1000 en el App.Config";
            //                ucLog.GrabarLogTexto(msj);
            //            }
            //        }
            //        else
            //        {
            //            msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable EjecutarImpresion2, no contiene el valor esperado en el App.Config";
            //            ucLog.GrabarLogTexto(msj);
            //        }
            //    }
            //    else
            //    {
            //        msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " No existe la variable EjecutarImpresion2 en el App.Config";
            //        ucLog.GrabarLogTexto(msj);
            //    }
            //}



            Console.ReadKey();
            
        }
       
      
        static void doc_PrintPage(object sender, PrintPageEventArgs ev)
        {

            using (SGRDA.Servicios.Proxy.SEFacturaClient servFact = new Servicios.Proxy.SEFacturaClient())
            {
                Factura factura = servFact.ObtenerFactura(docsImprimir[tmpContador].ID_DOCUMENTO);

                if (factura != null && factura.Detalle != null)
                {

                    float pos_x = 100;
                    float pos_y = 10;
                    var socio = factura.RazonSocial;
                    var direccion = factura.Direccion;
                    var RUM = "49584995849" + factura.RUM;
                    var RUC = factura.RUC;
                    var local = "-" + docsImprimir[tmpContador].ID_DOCUMENTO;
                    string numero = Convert.ToString(factura.NumFact);
                    var factor = 17;


                    pos_x = 110;
                    pos_y = 160;

                    ev.Graphics.DrawString(numero, fuente, Brushes.Black, pos_x + 350, (pos_y - 20), new StringFormat());
                    ev.Graphics.DrawString(DateTime.Now.ToString(), fuente, Brushes.Black, pos_x + 370, (pos_y), new StringFormat());
                    ev.Graphics.DrawString(RUM, fuente, Brushes.Black, pos_x, (pos_y), new StringFormat());
                    ev.Graphics.DrawString(socio, fuente, Brushes.Black, pos_x, (pos_y + (factor * 1)), new StringFormat());
                    ev.Graphics.DrawString(RUC, fuente, Brushes.Black, pos_x, (pos_y + (factor * 2)), new StringFormat());
                    ev.Graphics.DrawString(local, fuente, Brushes.Black, pos_x, (pos_y + (factor * 3)), new StringFormat());
                    ev.Graphics.DrawString(direccion, fuente, Brushes.Black, pos_x, (pos_y + (factor * 4)), new StringFormat());

                    factor = 20;
                    var pos_x_deta_ini = 1;
                    var pos_y_deta_ini = (pos_y + (factor * 5));
                  //  decimal acumTotal = 0;
                    int i = 1;

                    foreach (var item in factura.Detalle)
                    {
 
                        var valor = 100 * i;
                        var fila = (pos_y_deta_ini + (factor * i));
                        ev.Graphics.DrawString(Convert.ToString(item.Item), fuente, Brushes.Black, pos_x_deta_ini, fila, new StringFormat());
                        ev.Graphics.DrawString(item.Descripcion, fuente, Brushes.Black, pos_x_deta_ini + 70, fila, new StringFormat());
                        ev.Graphics.DrawString(String.Format("{0:0.##}", item.SubTotal), fuente, Brushes.Black, pos_x_deta_ini + 650, fila, new StringFormat());
                     
                        i++;
                    }
                    ev.Graphics.DrawString(String.Format("{0:0.##}", factura.Total), fuente, Brushes.Black, pos_x_deta_ini + 650, 440, new StringFormat());
                 

                }
            } 
            
 

        }
  


       public static void DisplayTimeEvent2(object source, ElapsedEventArgs e)
       {
           Console.WriteLine("****************  :: DisplayTimeEvent2***** {0} *********************",tmpContador);
           checkPrintSuccessful();
       }
       public static void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("****************  :: DisplayTimeEvent***** {0} *********************", tmpContador);
            ImprimirTmp();
          

        }


        
        private static void ImprimirTmp()
        {
            using (SGRDA.Servicios.Proxy.SEPreImpresionClient preImp = new SGRDA.Servicios.Proxy.SEPreImpresionClient())
            {
                tmpContador = 0;

                docsImprimir = preImp.ListarPendientes(Dns.GetHostName()).ToList();
                if (docsImprimir == null || docsImprimir.Count == 0) Console.WriteLine("No hay factura para imprimir..");
                foreach (var item in docsImprimir)
                {
                    bool existeFilPrinting = false;
                    fileSentPrint = string.Format(FormatoNameDoc, item.ID_DOCUMENTO, item.ID);
                    bool idPrinterOnline;
                    List<KeyValuePair<string, string>> jobs = GetPrintJobsCollection(NombImpresora, fileSentPrint, out idPrinterOnline, false);

                    /*Verifica si cola de impresion */
                    if (jobs != null && jobs.Count > 0) existeFilPrinting = true;

                    /*Verifica si ya existe un documento en cola con el mismo nombre. De ser asi no imprime */
                    if (!existeFilPrinting)
                    {
                        PrintDocument doc = new PrintDocument();

                        doc.PrintPage += doc_PrintPage;
                        doc.QueryPageSettings += doc_QueryPageSettings;
                        doc.DocumentName = fileSentPrint;
                        Margins margins = new Margins(20, 10, 20, 20);
                        doc.DefaultPageSettings.Margins = margins;
                        doc.OriginAtMargins = true;


                        //  //Create an instance of our printer class
                        //  if(tPrint==null)         tPrint = new Thread(doc.Print);
                        ////s  Console.WriteLine("estado hilo ejecucuión IsAlive...{0}...", tPrint.IsAlive);
                        //  if (!tPrint.IsAlive)
                        //  {
                        //      tPrint = new Thread(doc.Print);
                        //      tPrint.Start();
                        //      checkPrintSuccessful();
                        //      tmpContador++;
                        //      tPrint.Join();
                        //  }
                        ////  Console.WriteLine("hilo ejecucuión IsAlive...{0}", tPrint.IsAlive);
                        ////  Console.WriteLine("hilo eid {1}...estado {0}", tPrint.ThreadState,tPrint.ManagedThreadId);

                        doc.Print();
                        //checkPrintSuccessful();
                        tmpContador++;
                    }
                    else
                    {
                        Console.WriteLine(" file ya existe en colaa ...{0}", fileSentPrint);
                    }
                }
            }
        }

       

        static void doc_EndPrint(object sender, PrintEventArgs e)
        {
            Console.WriteLine("print action...{0}", e.PrintAction.ToString());

        }

        static void doc_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            //Console.WriteLine("****************entró a doc_QueryPageSettings******{0}********************", tmpContador);
             checkPrintSuccessful();
        }

        public static class Constante
        {
            public static class EstadoImpresion
            {
                public static string FINALIZADO = "FIN";
                public static string ERROR = "ERR";
                public static string CANCELADO = "CAN";
                public static string ANULADO = "ANU";
                public static string PENDIENTE = "PEN";
                public static string BLOQUEADO = "BLO";
                public static string OFFLINE_PRINT = "OFF";
                public static string NO_JOB = "NOJ";
                public static string OFFLINE_PRINT2 = "OF2";
            }
        }
        static void checkPrintSuccessful()
        {
            if (docsImprimir != null && docsImprimir.Count > 0 && tmpContador < docsImprimir.Count)
            {
                bool idPrinterOnline;
                List<KeyValuePair<string, string>> jobs = GetPrintJobsCollection(NombImpresora, fileSentPrint, out idPrinterOnline);

                var idActualizar = docsImprimir[tmpContador].ID;
                var idDoc = docsImprimir[tmpContador].ID_DOCUMENTO;
                var pcLocal = Dns.GetHostName();
                var ipV4 = getIpv4(pcLocal);

                if (idPrinterOnline)
                {
                     
                    //       BLPreImpresion preImp = new BLPreImpresion();

                    if (jobs.Count > 0)
                    {
                        Console.WriteLine(" Proceso ImprimirTmp Nro: {0} BEGIN: {1}", nn, DateTime.Now.ToString("HH:mm:ss.ffff"));
                        for (int i = 0; i < jobs.Count; i++)
                        {
                            var nombre = jobs[i];

                            string[] param = nombre.Key.Split('-');

                            if (param.Length == 4 && param[0] == K_PREF1 && param[2] == K_PREF2)
                            {
                                if (docsImprimir[tmpContador] != null &&
                                    string.Format(FormatoNameDoc, idDoc, idActualizar) == string.Format(FormatoNameDoc, param[1], param[3]))
                                {
                                    bool existeQueAndPend = false;
                                    using (SGRDA.Servicios.Proxy.SEPreImpresionClient servPrePrint = new Servicios.Proxy.SEPreImpresionClient())
                                    {
                                        List<SGRDA.Servicios.Proxy.Entidad.PreImpresion> docsImprimirVal = servPrePrint.ListarPendientes(Dns.GetHostName()).ToList();
                                        existeQueAndPend = docsImprimirVal.Exists(x => x.ID_DOCUMENTO == docsImprimir[tmpContador].ID_DOCUMENTO);

                                    }
                                    string[] EstadoJob = nombre.Value.Split('*');
                                    string estadoPrint = EstadoJob[0];
                                    int idJob = Convert.ToInt32(EstadoJob[1]);
                                    //if ((nombre.Value == "5" || nombre.Value == "OK") && existeQueAndPend)
                                    if ((estadoPrint == "5" || estadoPrint == "OK") && existeQueAndPend)
                                    {
                                        /*VALIDAR SI LA IMPRESORA ESTA ACTIVA LUEGO DE PASAR LAS VALIDACIONES DE IMPRESION COMO  OFFLINE.*/
                                        bool isPrinterOnline = false;
                                        var result = ImpresoraActiva(NombImpresora, isPrinterOnline);
                                        if (result)
                                        {   /*SI ENTRÓ A ESTA VALIDACION SE INTERPRETA QUE LA IMPRESORA EESTUVO ACTIVA Y SE IMPRIMIÓ CORRECTAMENTE*/
                                            ActualizarTMPImpresion(pcLocal, idActualizar, idDoc, Constante.EstadoImpresion.FINALIZADO);
                                            //  tPrint.Join();
                                            //var resultCancel = CancelPrintJob(idJob);
                                            //Console.WriteLine("Estado FIN ...Canceló JOB {0} .. Resultado: {1}", idJob, resultCancel);
                                        }
                                        else
                                        {
                                            //var resultCancel = CancelPrintJob(idJob);
                                            //Console.WriteLine("Estado OFF ...Canceló JOB {0} .. Resultado: {1}", idJob, resultCancel);
                                            ActualizarTMPImpresion( pcLocal, idActualizar, idDoc, Constante.EstadoImpresion.OFFLINE_PRINT2);
                                        }
                                    }
                                    else
                                    {
                                        var resultCancel = CancelPrintJob(idJob);
                                        //Console.WriteLine("Estado ERR ...Canceló JOB {0} .. Resultado: {1}", idJob, resultCancel);
                                        // ActualizarTMPImpresion(preImp, pcLocal, idActualizar, idDoc, Constante.EstadoImpresion.ERROR);
                                    }
                                }
                                else
                                {
                                    ActualizarTMPImpresion( pcLocal, idActualizar, idDoc, Constante.EstadoImpresion.ANULADO);
                                }
                            }
                            else
                            {
                                ActualizarTMPImpresion( pcLocal, idActualizar, idDoc, Constante.EstadoImpresion.CANCELADO);

                            }

                        }
                        Console.WriteLine(" Proceso ImprimirTmp Nro: {0} END: {1}", nn, DateTime.Now.ToString("HH:mm:ss.ffff"));
                    }
                    else
                    {
                        ActualizarTMPImpresion( pcLocal, idActualizar, idDoc, Constante.EstadoImpresion.NO_JOB);
                    }
                }
                else
                {
                    ActualizarTMPImpresion( pcLocal, idActualizar, idDoc, Constante.EstadoImpresion.OFFLINE_PRINT);

                }
            }
        }

        private static void ActualizarTMPImpresion(string pcLocal, decimal idActualizar, decimal idDoc, string estado)
        {
         
            using (SGRDA.Servicios.Proxy.SEPreImpresionClient servPrePrint = new Servicios.Proxy.SEPreImpresionClient())
            {
                PreImpresion objs = servPrePrint.ObtenerPreImpresion(idActualizar);
                if (objs != null && objs.FECHA_IMP == null)
                {
                    servPrePrint.ActualizarEstado(idActualizar, pcLocal, estado);
                    Console.WriteLine("Codigo : {0}...Estado : {1}", idDoc, estado);

                    //finalizo el hilo lanzado

                }
            }
        }

        static string getIpv4(string host)
        {
            string IP4Address = "";
            foreach (IPAddress IPA in Dns.GetHostAddresses(host))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            return IP4Address;
        }



        public static StringCollection GetPrintersCollection()
        {
            StringCollection printerNameCollection = new StringCollection();
            string searchQuery = "SELECT * FROM Win32_Printer";
            ManagementObjectSearcher searchPrinters =
                  new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection printerCollection = searchPrinters.Get();
            foreach (ManagementObject printer in printerCollection)
            {
                printerNameCollection.Add(printer.Properties["Name"].Value.ToString());
            }
            return printerNameCollection;
        }
        public static List<KeyValuePair<string, string>> GetPrintJobsCollection(string printerName, string documentoPrint, out bool isPrinterOnline,bool validatePrinOnline=true)
        {
            List<KeyValuePair<string, string>> printJobCollection = new List<KeyValuePair<string, string>>();

            if (validatePrinOnline)
            {
                isPrinterOnline = false;
                isPrinterOnline = ImpresoraActiva(printerName, isPrinterOnline);
            }
            else {
                isPrinterOnline = true;
            }
            if (isPrinterOnline)
            {
                string searchQuery = string.Format("SELECT * FROM Win32_PrintJob   WHERE   Document='{0}' " , documentoPrint);
                ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(searchQuery);
                ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();

                foreach (ManagementObject prntJob in prntJobCollection)
                {
                    string jobName = prntJob.Properties["Name"].Value.ToString();
                    string documentName = prntJob.Properties["Document"].Value.ToString();

                    if (fileSentPrint == documentName)
                    {
                        char[] splitArr = new char[1];
                        splitArr[0] = Convert.ToChar(",");
                        string prnterName = jobName.Split(splitArr)[0];
                        string estado = prntJob.Properties["status"].Value.ToString();
                        string m_JobID = prntJob.Properties["JobId"].Value.ToString();
                        if (String.Compare(prnterName, printerName, true) == 0)
                        {
                            printJobCollection.Add(new KeyValuePair<string, string>(documentName, string.Format("{0}*{1}",estado,m_JobID)));
                           // Console.WriteLine("{0}*{1} ....{2}", estado, m_JobID, documentName);
                        } 
                    }
                }
            }
            return printJobCollection;
        }

        private static bool ImpresoraActiva(string printerName, bool isPrinterOnline)
        {
            isPrinterOnline = false;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            string xprinterName = "";
            foreach (ManagementObject printer in searcher.Get())
            {
                xprinterName = printer["Name"].ToString().ToLower();
                if (xprinterName.Equals(printerName.ToLower()))
                {
                    //   Thread.Sleep(3000);
                    if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                    {
                        isPrinterOnline = false;
                    }
                    else
                    {
                        isPrinterOnline = true;
                    }
                }
            }
            return isPrinterOnline;
        }

        #region CancelPrintJob

        /// <summary>
        /// Cancel the print job. This functions accepts the job number.
        /// An exception will be thrown if access denied.
        /// </summary>
        /// <param name="printJobID">int: Job number to cancel printing for.</param>
        /// <returns>bool: true if cancel successfull, else false.</returns>
        public static bool CancelPrintJob(int printJobID)
        {
            // Variable declarations.
            bool isActionPerformed = false;
            string searchQuery;
            String jobName;
            char[] splitArr;
            int prntJobID;
            ManagementObjectSearcher searchPrintJobs;
            ManagementObjectCollection prntJobCollection;
            try
            {
                // Query to get all the queued printer jobs.
                searchQuery = "SELECT * FROM Win32_PrintJob";
                // Create an object using the above query.
                searchPrintJobs = new ManagementObjectSearcher(searchQuery);
                // Fire the query to get the collection of the printer jobs.
                prntJobCollection = searchPrintJobs.Get();

                // Look for the job you want to delete/cancel.
                foreach (ManagementObject prntJob in prntJobCollection)
                {
                    jobName = prntJob.Properties["Name"].Value.ToString();
                    // Job name would be of the format [Printer name], [Job ID]
                    splitArr = new char[1];
                    splitArr[0] = Convert.ToChar(",");
                    // Get the job ID.
                    prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                    // If the Job Id equals the input job Id, then cancel the job.
                    if (prntJobID == printJobID)
                    {
                        // Performs a action similar to the cancel
                        // operation of windows print console
                        prntJob.Delete();
                        isActionPerformed = true;
                        break;
                    }
                }
                return isActionPerformed;
            }
            catch  
            {
                // Log the exception.
                return false;
            }
        }
        internal static void HandlePausedJob(PrintSystemJobInfo theJob)
        {
            // If there's no good reason for the queue to be paused, resume it and  
            // give user choice to resume or cancel the job.
            //Console.WriteLine("The user or someone with administrative rights to the queue" +
            //     "\nhas paused the job or queue." +
            //     "\nResume the queue? (Has no effect if queue is not paused.)" +
            //     "\nEnter \"Y\" to resume, otherwise press return: ");
            //String resume = Console.ReadLine();
            //if (resume == "Y")
            //{
                theJob.HostingPrintQueue.Resume();

                //// It is possible the job is also paused. Find out how the user wants to handle that.
                //Console.WriteLine("Does user want to resume print job or cancel it?" +
                //    "\nEnter \"Y\" to resume (any other key cancels the print job): ");
                //String userDecision = Console.ReadLine();
                //if (userDecision == "Y")
                //{
                //    theJob.Resume();
                //}
                //else
                //{
                    theJob.Cancel();
                //}
           // }//end if the queue should be resumed

        }//end HandlePausedJob
        #endregion CancelPrintJob


      
        //#region TESTTARIFA






        //public class Seleccion
        //{
        //    public const string NO = "0";
        //    public const string SI = "1";
        //    public const string MANUAL = "2";
        //}

        //public class LetraCar
        //{
        //    public const string A = "A";
        //    public const string B = "B";
        //    public const string C = "C";
        //    public const string D = "D";
        //    public const string E = "E";
        //}


        //List<DTOTarifaTest> matrizTestTmp;
        //public List<DTOTarifaTest> MatrizTestTmp
        //{
        //    get
        //    {
        //        return (List<DTOTarifaTest>)matrizTestTmp;
        //    }
        //    set
        //    {
        //        matrizTestTmp = value;
        //    }
        //}
        //// GET: /TarifaTest/
        //public decimal VUM = new BLValormusica().ObtenerActivo(GlobalVars.Global.OWNER).VUM_VAL;
        //DTOTarifa dtoTarifa = new DTOTarifa();
        //List<DTOTarifaTestCaracteristica> caracteristica = new List<DTOTarifaTestCaracteristica>();
        //List<DTOTarifaManReglaAsoc> reglaAsoc = new List<DTOTarifaManReglaAsoc>();
        //List<DTOTarifaTest> matrizTest = new List<DTOTarifaTest>();
        //private const string K_SESION_TARIFA = "___DTOTarifa";
        //private const string K_SESION_TARIFA_REGLA = "___DTOTarifaRegla";
        //private const string K_SESION_TARIFA_CAR = "___DTOTarifaCar";
        //private const string K_SESION_TARIFA_MATRIZ_TEST = "___DTOTarifaMatrizTest";

        //DTOTarifa tarifaTmp = new DTOTarifa();

        //List<DTOTarifaTestCaracteristica> caracteristicaTmp;
        //public List<DTOTarifaTestCaracteristica> CaracteristicaTmp
        //{
        //    get
        //    {
        //        return (List<DTOTarifaTestCaracteristica>)caracteristicaTmp;
        //    }
        //    set
        //    {
        //        caracteristicaTmp = value;
        //    }
        //}
        //public DTOTarifa TarifaTmp
        //{
        //    get
        //    {
        //        return (DTOTarifa)tarifaTmp;
        //    }
        //    set
        //    {
        //        tarifaTmp = value;
        //    }
        //}

        //List<DTOTarifaManReglaAsoc> reglaAsocTmp = new List<DTOTarifaManReglaAsoc>();
        //public List<DTOTarifaManReglaAsoc> ReglaAsocTmp
        //{
        //    get
        //    {
        //        return (List<DTOTarifaManReglaAsoc>)reglaAsocTmp;
        //    }
        //    set
        //    {
        //        reglaAsocTmp = value;
        //    }
        //}

        //DTOTarifa ObtenerValorTestTarifa(decimal idTarifa, decimal idLicencia)
        //{

        //    decimal retorno = 0;
        //    BEREC_RATES_GRAL tarifa = new BEREC_RATES_GRAL();
        //    tarifa = new BLTarifaTest().Obtener(GlobalVars.Global.OWNER, idTarifa);

        //    if (tarifa != null)
        //    {
        //        dtoTarifa = new DTOTarifa();
        //        dtoTarifa.idTarifa = tarifa.RATE_ID;
        //        dtoTarifa.TarifaDesc = tarifa.RATE_DESC;
        //        dtoTarifa.CantVariable = tarifa.RATE_NVAR;
        //        dtoTarifa.CantCaracteristica = tarifa.RATE_NCAL;
        //        dtoTarifa.Formula = tarifa.RATE_FORMULA;
        //        dtoTarifa.Minima = tarifa.RATE_MINIMUM;
        //        dtoTarifa.FormulaTipo = tarifa.RATE_FTIPO;
        //        dtoTarifa.MinimoTipo = tarifa.RATE_MTIPO;
        //        dtoTarifa.FormulaDec = tarifa.RATE_FDECI;
        //        dtoTarifa.MinimoDec = tarifa.RATE_MDECI;
        //        TarifaTmp = dtoTarifa;


        //        #region INIT REGLAS
        //        if (tarifa.Regla != null)
        //        {
        //            reglaAsoc = new List<DTOTarifaManReglaAsoc>();
        //            tarifa.Regla.ForEach(s =>
        //            {
        //                reglaAsoc.Add(new DTOTarifaManReglaAsoc
        //                {
        //                    IdRegla = s.CALR_ID,
        //                    IdPlantilla = s.TEMP_ID,
        //                    Elemento = s.CALR_DESC,
        //                    Letra = s.RATE_CALC_VAR,
        //                    Variables = s.CALR_NVAR,
        //                    AjustarUnidades = s.CALR_ADJUST,
        //                    AcumularTramos = s.CALR_ACCUM,
        //                    Formula = s.CALC_FORMULA,
        //                    FormulaTipo = s.CALR_FOR_TYPE,
        //                    FormulaDec = s.CALR_FOR_DEC,
        //                    Minimo = s.CALC_MINIMUM,
        //                    MinimoTipo = s.CALR_MIN_TYPE,
        //                    MinimoDec = s.CALR_MIN_DEC,
        //                    EnBD = true,
        //                });
        //            });
        //            ReglaAsocTmp = reglaAsoc;
        //        }
        //        #endregion

        //        #region INIT CARACTERISTICAS


        //        if (tarifa.Caracteristica != null)
        //        {
        //            ///nuevo para intgrar con licencias - addon dbs
        //            var caracteristicas = new BLCaracteristica().ListarCaractLicencia(GlobalVars.Global.OWNER, idLicencia, "0");
        //            caracteristica = new List<DTOTarifaTestCaracteristica>();
        //            List<BETarifaCaracteristica> listChar = null;
        //            tarifa.Caracteristica.ForEach(chars =>
        //            {
        //                if (caracteristicas.Exists(c => c.CHAR_ID == chars.RATE_CALC_AR))
        //                {
        //                    if (listChar == null) listChar = new List<BETarifaCaracteristica>();
        //                    chars.VALUE = caracteristicas.Find(x => x.CHAR_ID == chars.RATE_CALC_AR).LIC_CHAR_VAL;
        //                    listChar.Add(chars);
        //                };
        //                // listChar.Add(chars);
        //            });

        //            foreach (var item in listChar)
        //            {
        //                Console.WriteLine("id carac from tarifa test: {0} - Valor : {1} - desc : {2} ", item.RATE_CALC_AR, item.VALUE, item.RATE_CHAR_DESCVAR);
        //            }
        //            //Console.WriteLine("*******************************");
        //            //foreach (var item in caracteristicas)
        //            //{
        //            //    Console.WriteLine("id carac from Licencia: {0} - Valor : {1}", item.CHAR_ID,item.LIC_CHAR_VAL);
        //            //}

        //            ///fin - nuevo para intgrar con licencias - addon dbs
        //            if (listChar != null)
        //            {
        //                //tarifa.Caracteristica.ForEach(s =>
        //                listChar.ForEach(s =>
        //                {
        //                    caracteristica.Add(new DTOTarifaTestCaracteristica
        //                    {
        //                        Id = s.RATE_CHAR_ID,
        //                        IdElemento = s.RATE_CALC_ID,
        //                        IdRegla = s.RATE_CALC,
        //                        IdCaracteristica = s.RATE_CALC_AR,
        //                        Letra = s.RATE_CHAR_TVAR,
        //                        Tipo = (s.RATE_CALCT == "R") ? "CARACTERISTICA" : "VARIABLE",
        //                        DescripcionCorta = s.RATE_CHAR_DESCVAR,
        //                        DescripcionLarga = s.RATE_CHAR_DESCVAR,
        //                        EnBD = true,
        //                        Activo = s.ENDS.HasValue ? false : true,
        //                        Tramo = s.IND_TR,
        //                        LetraOrigenRegla = s.CHAR_ORI_REG,
        //                        Valor = Convert.ToDecimal(s.VALUE)
        //                    });
        //                });
        //                CaracteristicaTmp = caracteristica;
        //            }
        //        }
        //        #endregion

        //        #region INIT TARIFA

        //        if (tarifa.Test != null)
        //        {
        //            matrizTest = new List<DTOTarifaTest>();
        //            tarifa.Test.ForEach(s =>
        //            {
        //                matrizTest.Add(new DTOTarifaTest
        //                {
        //                    //Id = s.RATE_CHAR_ID,
        //                    IdPlantilla = s.TEMP_ID,
        //                    IdRegla = s.CALR_ID,

        //                    IdVal_1 = s.TEMPS1_ID,
        //                    IdCaracteristica1 = s.CHAR1_ID,
        //                    Tramo_1 = s.IND1_TR,
        //                    Desde_1 = s.CRI1_FROM,
        //                    Hasta_1 = s.CRI1_TO,

        //                    IdVal_2 = s.TEMPS2_ID,
        //                    IdCaracteristica2 = s.CHAR2_ID,
        //                    Tramo_2 = s.IND2_TR,
        //                    Desde_2 = s.CRI2_FROM,
        //                    Hasta_2 = s.CRI2_TO,

        //                    IdVal_3 = s.TEMPS3_ID,
        //                    IdCaracteristica3 = s.CHAR3_ID,
        //                    Tramo_3 = s.IND3_TR,
        //                    Desde_3 = s.CRI3_FROM,
        //                    Hasta_3 = s.CRI3_TO,

        //                    IdVal_4 = s.TEMPS4_ID,
        //                    IdCaracteristica4 = s.CHAR4_ID,
        //                    Tramo_4 = s.IND4_TR,
        //                    Desde_4 = s.CRI4_FROM,
        //                    Hasta_4 = s.CRI4_TO,

        //                    IdVal_5 = s.TEMPS5_ID,
        //                    IdCaracteristica5 = s.CHAR5_ID,
        //                    Tramo_5 = s.IND5_TR,
        //                    Desde_5 = s.CRI5_FROM,
        //                    Hasta_5 = s.CRI5_TO,

        //                    Tarifa = s.VAL_FORMULA,
        //                    Minimo = s.VAL_MINIMUM
        //                });
        //            });
        //            MatrizTestTmp = matrizTest;
        //        }
        //        #endregion

        //        #region REALIZAR SETTING REGLAS
        //        foreach (var regla in ReglaAsocTmp)
        //        {
        //            if (regla.AcumularTramos == Seleccion.SI && regla.AjustarUnidades == Seleccion.SI)
        //            {
        //                ATsiAUsi(regla);
        //            }
        //            else if (regla.AcumularTramos == Seleccion.SI && regla.AjustarUnidades == Seleccion.NO)
        //            {
        //                ATsiAUno(regla);
        //            }
        //            else if (regla.AcumularTramos == Seleccion.NO && regla.AjustarUnidades == Seleccion.SI)
        //            {
        //                ATnoAUsi(regla);
        //            }
        //            else if (regla.AcumularTramos == Seleccion.NO && regla.AjustarUnidades == Seleccion.NO)
        //            {
        //                ATnoAUno(regla);
        //            }
        //        }
        //        #endregion


        //        CalcularTarifa(TarifaTmp);

        //        // retorno =  TarifaTmp.ValorMinimo;
        //    }
        //    return TarifaTmp;
        //}

        //public DTOTarifaManReglaAsoc ATsiAUsi(DTOTarifaManReglaAsoc regla)
        //{
        //    #region Obtener Valores
        //    decimal? vA = 0; string tA = string.Empty;
        //    decimal? vB = 0; string tB = string.Empty;
        //    decimal? vC = 0; string tC = string.Empty;
        //    decimal? vD = 0; string tD = string.Empty;
        //    decimal? vE = 0; string tE = string.Empty;

        //    var valores = MatrizTestTmp.Where(t => t.IdRegla == regla.IdRegla);
        //    var caracteristicas = CaracteristicaTmp.Where(c => c.IdRegla == regla.IdRegla);

        //    int totCaracteristicas = caracteristicas.ToList().Count;
        //    if (totCaracteristicas > 0)
        //    {
        //        vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
        //        tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 1)
        //    {
        //        vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
        //        tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 2)
        //    {
        //        vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
        //        tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 3)
        //    {
        //        vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
        //        tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 4)
        //    {
        //        vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
        //        tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
        //    }


        //    //Buscar R
        //    List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
        //    if (totCaracteristicas > 0)
        //    {
        //        if (tA == Seleccion.SI)
        //            listaObtenerR = valores.Where(
        //                            v => (vA >= v.Desde_1 && vA <= v.Hasta_1) || (v.Hasta_1 <= vA)
        //                            ).ToList();
        //        else if (tA == Seleccion.NO)
        //            listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
        //    }

        //    if (totCaracteristicas > 1)
        //    {
        //        if (tB == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(
        //                            v => (vB >= v.Desde_2 && vB <= v.Hasta_2) || (v.Hasta_2 <= vB)
        //                            ).ToList();
        //        else if (tB == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
        //    }

        //    if (totCaracteristicas > 2)
        //    {
        //        if (tC == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(
        //                            v => (vC >= v.Desde_3 && vC <= v.Hasta_3) || (v.Hasta_3 <= vC)
        //                            ).ToList();
        //        else if (tC == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
        //    }

        //    if (totCaracteristicas > 3)
        //    {
        //        if (tD == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(
        //                            v => (vD >= v.Desde_4 && vD <= v.Hasta_4) || (v.Hasta_4 <= vD)
        //                            ).ToList();
        //        else if (tD == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
        //    }

        //    if (totCaracteristicas > 4)
        //    {
        //        if (tE == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(
        //                v => (vE >= v.Desde_5 && vE <= v.Hasta_5) || (v.Hasta_5 <= vE)
        //                ).ToList();
        //        else if (tE == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_4).ToList();
        //    }
        //    #endregion

        //    #region Calcular
        //    decimal? acumularR = 0;
        //    decimal? acumularRminimo = 0;
        //    decimal? vFormula = 0; decimal? vFormulaTemp = 0;
        //    decimal? vMinimo = 0; decimal? vMinimoTemp = 0;
        //    decimal? valorR = 0;
        //    decimal? valorRmin = 0;//

        //    foreach (var item in listaObtenerR)
        //    {
        //        acumularR += item.Tarifa;
        //        acumularRminimo += item.Minimo;
        //    }
        //    vFormula = listaObtenerR.FirstOrDefault().Tarifa;
        //    vMinimo = listaObtenerR.FirstOrDefault().Minimo;

        //    valorR = vFormula;
        //    valorRmin = vMinimo;//

        //    //if (vMinimo > vFormula)
        //    //    valorR = vMinimo;
        //    //else
        //    //    valorR = vFormula;

        //    if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
        //        tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
        //    {
        //        decimal? Ra = 0;
        //        decimal? Rb = 0;
        //        decimal? Rc = 0;
        //        decimal? Rd = 0;
        //        decimal? Re = 0;

        //        string formula;
        //        string formulaMinima;
        //        string[] listaOperandos = new string[10];
        //        double[] listaValores = new double[10];
        //        DataTable Tbl = new DataTable();

        //        formula = regla.Formula;
        //        formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);//

        //        foreach (var car in CaracteristicaTmp)
        //        {
        //            if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
        //        }

        //        listaOperandos[0] = LetraCar.A;
        //        listaOperandos[1] = LetraCar.B;
        //        listaOperandos[2] = LetraCar.C;
        //        listaOperandos[3] = LetraCar.D;
        //        listaOperandos[4] = LetraCar.E;
        //        listaOperandos[5] = LetraReg.R;
        //        listaOperandos[6] = LetraReg.V;
        //        listaOperandos[7] = LetraReg.Rmin;//

        //        listaValores[0] = Convert.ToDouble(Ra);
        //        listaValores[1] = Convert.ToDouble(Rb);
        //        listaValores[2] = Convert.ToDouble(Rc);
        //        listaValores[3] = Convert.ToDouble(Rd);
        //        listaValores[4] = Convert.ToDouble(Re);

        //        listaValores[5] = Convert.ToDouble(valorR);
        //        listaValores[6] = Convert.ToDouble(VUM);
        //        listaValores[7] = Convert.ToDouble(valorRmin);//

        //        //Tbl.Columns.Add("variable", typeof(string));
        //        Tbl.Columns.Add(listaOperandos[0], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[1], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[2], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[3], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[4], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[5], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[6], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[7], typeof(double));
        //        Tbl.Columns.Add("Tarifa", typeof(double), formula);
        //        Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

        //        {
        //            // crea una nueva línea 
        //            DataRow linea = Tbl.NewRow();
        //            linea[0] = listaValores[0];
        //            linea[1] = listaValores[1];
        //            linea[2] = listaValores[2];
        //            linea[3] = listaValores[3];
        //            linea[4] = listaValores[4];
        //            linea[5] = listaValores[5];
        //            linea[6] = listaValores[6];
        //            linea[7] = listaValores[7];//
        //            Tbl.Rows.Add(linea);
        //        }

        //        regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
        //        regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

        //        if (regla.ValorMinimo > regla.ValorFormula)
        //            regla.ValorR = regla.ValorMinimo;
        //        else
        //            regla.ValorR = regla.ValorFormula;
        //    }
        //    else
        //    {
        //        regla.ValorFormula = vFormula;
        //        regla.ValorMinimo = vMinimo;
        //        if (regla.ValorMinimo > regla.ValorFormula)
        //            regla.ValorR = regla.ValorMinimo;
        //        else
        //            regla.ValorR = regla.ValorFormula;
        //    }
        //    #endregion
        //    return regla;
        //}  // CASO 1 -OK

        //public DTOTarifaManReglaAsoc ATsiAUno(DTOTarifaManReglaAsoc regla)
        //{
        //    #region Obtener Valores
        //    decimal? vA = 0; string tA = string.Empty;
        //    decimal? vB = 0; string tB = string.Empty;
        //    decimal? vC = 0; string tC = string.Empty;
        //    decimal? vD = 0; string tD = string.Empty;
        //    decimal? vE = 0; string tE = string.Empty;

        //    var valores = MatrizTestTmp.Where(t => t.IdRegla == regla.IdRegla);
        //    var caracteristicas = CaracteristicaTmp.Where(c => c.IdRegla == regla.IdRegla);

        //    int totCaracteristicas = caracteristicas.ToList().Count;
        //    if (totCaracteristicas > 0)
        //    {
        //        vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
        //        tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 1)
        //    {
        //        vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
        //        tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 2)
        //    {
        //        vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
        //        tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 3)
        //    {
        //        vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
        //        tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 4)
        //    {
        //        vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
        //        tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
        //    }


        //    //Buscar R
        //    List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
        //    if (totCaracteristicas > 0)
        //    {
        //        if (tA == Seleccion.SI)
        //            listaObtenerR = valores.Where(
        //                            v => (vA >= v.Desde_1 && vA <= v.Hasta_1) || (v.Hasta_1 <= vA)
        //                            ).ToList();
        //        else if (tA == Seleccion.NO)
        //            listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
        //    }

        //    if (totCaracteristicas > 1)
        //    {
        //        if (tB == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(
        //                            v => (vB >= v.Desde_2 && vB <= v.Hasta_2) || (v.Hasta_2 <= vB)
        //                            ).ToList();
        //        else if (tB == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
        //    }

        //    if (totCaracteristicas > 2)
        //    {
        //        if (tC == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(
        //                            v => (vC >= v.Desde_3 && vC <= v.Hasta_3) || (v.Hasta_3 <= vC)
        //                            ).ToList();
        //        else if (tC == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
        //    }

        //    if (totCaracteristicas > 3)
        //    {
        //        if (tD == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(
        //                            v => (vD >= v.Desde_4 && vD <= v.Hasta_4) || (v.Hasta_4 <= vD)
        //                            ).ToList();
        //        else if (tD == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
        //    }

        //    if (totCaracteristicas > 4)
        //    {
        //        if (tE == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(
        //                v => (vE >= v.Desde_5 && vE <= v.Hasta_5) || (v.Hasta_5 <= vE)
        //                ).ToList();
        //        else if (tE == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_4).ToList();
        //    }
        //    #endregion

        //    #region CalcularAcumulado
        //    decimal? tempR = 1;
        //    decimal? tempRminimo = 1;
        //    decimal? sumarR = 0;
        //    decimal? sumarRminimo = 0;
        //    foreach (var item in listaObtenerR)
        //    {
        //        tempR = 1;
        //        tempRminimo = 1;

        //        if (tA == Seleccion.SI)
        //        {
        //            if (item.Hasta_1 < vA)
        //            {
        //                tempR *= item.Hasta_1;
        //                tempRminimo *= item.Hasta_1;
        //            }
        //            else
        //            {
        //                tempR *= (vA - (item.Desde_1 - 1));
        //                tempRminimo *= (vA - (item.Desde_1 - 1));
        //            }
        //        }

        //        if (tB == Seleccion.SI)
        //        {
        //            if (item.Hasta_2 < vB)
        //            {
        //                tempR *= item.Hasta_2;
        //                tempRminimo *= item.Hasta_2;
        //            }
        //            else
        //            {
        //                tempR *= (vB - (item.Desde_2 - 1));
        //                tempRminimo *= (vB - (item.Desde_2 - 1));
        //            }
        //        }

        //        if (tC == Seleccion.SI)
        //        {
        //            if (item.Hasta_3 < vC)
        //            {
        //                tempR *= item.Hasta_3;
        //                tempRminimo *= item.Hasta_3;
        //            }
        //            else
        //            {
        //                tempR *= (vC - (item.Desde_3 - 1));
        //                tempRminimo *= (vC - (item.Desde_3 - 1));
        //            }
        //        }

        //        if (tD == Seleccion.SI)
        //        {
        //            if (item.Hasta_4 < vD)
        //            {
        //                tempR *= item.Hasta_4;
        //                tempRminimo *= item.Hasta_4;
        //            }
        //            else
        //            {
        //                tempR *= (vD - (item.Desde_4 - 1));
        //                tempRminimo *= (vD - (item.Desde_4 - 1));
        //            }
        //        }

        //        if (tE == Seleccion.SI)
        //        {
        //            if (item.Hasta_5 < vE)
        //            {
        //                tempR *= item.Hasta_5;
        //                tempRminimo *= item.Hasta_5;
        //            }
        //            else
        //            {
        //                tempR *= (vE - (item.Desde_5 - 1));
        //                tempRminimo *= (vE - (item.Desde_5 - 1));
        //            }
        //        }

        //        sumarR += (tempR * item.Tarifa);
        //        sumarRminimo += (tempRminimo * item.Minimo);
        //    }
        //    #endregion

        //    decimal? vFormula = 0;
        //    decimal? vMinimo = 0;
        //    decimal? valorR = 0;
        //    decimal? valorRmin = 0;//

        //    #region Calcular
        //    vFormula = sumarR;
        //    vMinimo = sumarRminimo;

        //    valorR = vFormula;
        //    valorRmin = vMinimo;//

        //    //if (vMinimo > vFormula)
        //    //    valorR = vMinimo;
        //    //else
        //    //    valorR = vFormula;

        //    if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
        //        tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
        //    {
        //        decimal? Ra = 0;
        //        decimal? Rb = 0;
        //        decimal? Rc = 0;
        //        decimal? Rd = 0;
        //        decimal? Re = 0;

        //        string formula;
        //        string formulaMinima;
        //        string[] listaOperandos = new string[10];
        //        double[] listaValores = new double[10];
        //        DataTable Tbl = new DataTable();

        //        formula = regla.Formula;
        //        formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);//

        //        foreach (var car in CaracteristicaTmp)
        //        {
        //            if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
        //        }

        //        listaOperandos[0] = LetraCar.A;
        //        listaOperandos[1] = LetraCar.B;
        //        listaOperandos[2] = LetraCar.C;
        //        listaOperandos[3] = LetraCar.D;
        //        listaOperandos[4] = LetraCar.E;
        //        listaOperandos[5] = LetraReg.R;
        //        listaOperandos[6] = LetraReg.V;
        //        listaOperandos[7] = LetraReg.Rmin;//

        //        listaValores[0] = Convert.ToDouble(Ra);
        //        listaValores[1] = Convert.ToDouble(Rb);
        //        listaValores[2] = Convert.ToDouble(Rc);
        //        listaValores[3] = Convert.ToDouble(Rd);
        //        listaValores[4] = Convert.ToDouble(Re);

        //        listaValores[5] = Convert.ToDouble(valorR);
        //        listaValores[6] = Convert.ToDouble(VUM);
        //        listaValores[7] = Convert.ToDouble(valorRmin);//

        //        //Tbl.Columns.Add("variable", typeof(string));
        //        Tbl.Columns.Add(listaOperandos[0], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[1], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[2], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[3], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[4], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[5], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[6], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[7], typeof(double));//
        //        Tbl.Columns.Add("Tarifa", typeof(double), formula);
        //        Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

        //        {
        //            // crea una nueva línea 
        //            DataRow linea = Tbl.NewRow();
        //            linea[0] = listaValores[0];
        //            linea[1] = listaValores[1];
        //            linea[2] = listaValores[2];
        //            linea[3] = listaValores[3];
        //            linea[4] = listaValores[4];
        //            linea[5] = listaValores[5];
        //            linea[6] = listaValores[6];
        //            linea[7] = listaValores[7];//
        //            Tbl.Rows.Add(linea);
        //        }

        //        regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
        //        regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

        //        if (regla.ValorMinimo > regla.ValorFormula)
        //            regla.ValorR = regla.ValorMinimo;
        //        else
        //            regla.ValorR = regla.ValorFormula;
        //    }
        //    else
        //    {
        //        regla.ValorFormula = sumarR;
        //        regla.ValorMinimo = sumarRminimo;

        //        if (sumarRminimo > sumarR)
        //            regla.ValorR = sumarRminimo;
        //        else
        //            regla.ValorR = sumarR;

        //    }
        //    #endregion

        //    return regla;
        //}  // CASO 2

        //public DTOTarifaManReglaAsoc ATnoAUsi(DTOTarifaManReglaAsoc regla)
        //{
        //    #region Obtener Valores
        //    decimal vA = 0; string tA = string.Empty; // v=valor; t=tramo    
        //    decimal vB = 0; string tB = string.Empty;
        //    decimal vC = 0; string tC = string.Empty;
        //    decimal vD = 0; string tD = string.Empty;
        //    decimal vE = 0; string tE = string.Empty;

        //    var valores = MatrizTestTmp.Where(t => t.IdRegla == regla.IdRegla);
        //    var caracteristicas = CaracteristicaTmp.Where(c => c.IdRegla == regla.IdRegla);

        //    int totCaracteristicas = caracteristicas.ToList().Count;
        //    if (totCaracteristicas > 0)
        //    {
        //        vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
        //        tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 1)
        //    {
        //        vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
        //        tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 2)
        //    {
        //        vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
        //        tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 3)
        //    {
        //        vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
        //        tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 4)
        //    {
        //        vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
        //        tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
        //    }

        //    //Buscar R
        //    List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
        //    if (totCaracteristicas > 0)
        //    {
        //        if (tA == Seleccion.SI)
        //            listaObtenerR = valores.Where(v => vA >= v.Desde_1 && vA <= v.Hasta_1).ToList();
        //        else if (tA == Seleccion.NO)
        //            listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
        //    }

        //    if (totCaracteristicas > 1)
        //    {
        //        if (tB == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(v => vB >= v.Desde_2 && vB <= v.Hasta_2).ToList();
        //        else if (tB == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
        //    }

        //    if (totCaracteristicas > 2)
        //    {
        //        if (tC == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(v => vC >= v.Desde_3 && vB <= v.Hasta_3).ToList();
        //        else if (tC == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
        //    }

        //    if (totCaracteristicas > 3)
        //    {
        //        if (tD == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(v => vD >= v.Desde_4 && vB <= v.Hasta_4).ToList();
        //        else if (tD == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
        //    }

        //    if (totCaracteristicas > 4)
        //    {
        //        if (tE == Seleccion.SI)
        //            listaObtenerR = listaObtenerR.Where(v => vE >= v.Desde_5 && vB <= v.Hasta_5).ToList();
        //        else if (tE == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_5).ToList();
        //    }
        //    #endregion

        //    decimal? vFormula = 0;
        //    decimal? vMinimo = 0;
        //    decimal? valorR = 0;
        //    decimal? valorRmin = 0;

        //    #region Calcular
        //    vFormula = listaObtenerR != null && listaObtenerR.Count > 0 ? listaObtenerR.FirstOrDefault().Tarifa : 0;
        //    vMinimo = listaObtenerR != null && listaObtenerR.Count > 0 ? listaObtenerR.FirstOrDefault().Minimo : 0;

        //    valorR = vFormula;
        //    valorRmin = vMinimo;

        //    //if (vMinimo > vFormula)
        //    //    valorR = vMinimo;
        //    //else
        //    //    valorR = vFormula;

        //    if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
        //        tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
        //    {
        //        decimal? Ra = 0;
        //        decimal? Rb = 0;
        //        decimal? Rc = 0;
        //        decimal? Rd = 0;
        //        decimal? Re = 0;

        //        string formula;
        //        string formulaMinima;
        //        string[] listaOperandos = new string[10];
        //        double[] listaValores = new double[10];
        //        DataTable Tbl = new DataTable();

        //        formula = regla.Formula;
        //        formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);

        //        foreach (var car in CaracteristicaTmp)
        //        {
        //            if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
        //        }

        //        listaOperandos[0] = LetraCar.A;
        //        listaOperandos[1] = LetraCar.B;
        //        listaOperandos[2] = LetraCar.C;
        //        listaOperandos[3] = LetraCar.D;
        //        listaOperandos[4] = LetraCar.E;
        //        listaOperandos[5] = LetraReg.R;
        //        listaOperandos[6] = LetraReg.V;
        //        listaOperandos[7] = LetraReg.Rmin;

        //        listaValores[0] = Convert.ToDouble(Ra);
        //        listaValores[1] = Convert.ToDouble(Rb);
        //        listaValores[2] = Convert.ToDouble(Rc);
        //        listaValores[3] = Convert.ToDouble(Rd);
        //        listaValores[4] = Convert.ToDouble(Re);

        //        listaValores[5] = Convert.ToDouble(valorR);
        //        listaValores[6] = Convert.ToDouble(VUM);
        //        listaValores[7] = Convert.ToDouble(valorRmin);

        //        //Tbl.Columns.Add("variable", typeof(string));
        //        Tbl.Columns.Add(listaOperandos[0], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[1], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[2], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[3], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[4], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[5], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[6], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[7], typeof(double));
        //        Tbl.Columns.Add("Tarifa", typeof(double), formula);
        //        Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

        //        {
        //            // crea una nueva línea 
        //            DataRow linea = Tbl.NewRow();
        //            linea[0] = listaValores[0];
        //            linea[1] = listaValores[1];
        //            linea[2] = listaValores[2];
        //            linea[3] = listaValores[3];
        //            linea[4] = listaValores[4];
        //            linea[5] = listaValores[5];
        //            linea[6] = listaValores[6];
        //            linea[7] = listaValores[7];
        //            Tbl.Rows.Add(linea);
        //        }

        //        regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
        //        regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

        //        if (regla.ValorMinimo > regla.ValorFormula)
        //            regla.ValorR = regla.ValorMinimo;
        //        else
        //            regla.ValorR = regla.ValorFormula;
        //    }
        //    else
        //    {
        //        regla.ValorFormula = vFormula;
        //        regla.ValorMinimo = vMinimo;
        //        if (regla.ValorMinimo > regla.ValorFormula)
        //            regla.ValorR = regla.ValorMinimo;
        //        else
        //            regla.ValorR = regla.ValorFormula;
        //    }
        //    #endregion
        //    return regla;
        //}  // CASO 3 - OK

        //public DTOTarifaManReglaAsoc ATnoAUno(DTOTarifaManReglaAsoc regla)
        //{
        //    #region Obtener Valores
        //    decimal? vA = 0; string tA = string.Empty;
        //    decimal? vB = 0; string tB = string.Empty;
        //    decimal? vC = 0; string tC = string.Empty;
        //    decimal? vD = 0; string tD = string.Empty;
        //    decimal? vE = 0; string tE = string.Empty;
        //    decimal? vFormulaTemp = 0;
        //    decimal? vMinimoTemp = 0;
        //    decimal? acumular = 1;

        //    var valores = MatrizTestTmp.Where(t => t.IdRegla == regla.IdRegla);
        //    var caracteristicas = CaracteristicaTmp.Where(c => c.IdRegla == regla.IdRegla);

        //    int totCaracteristicas = caracteristicas.ToList().Count;
        //    if (totCaracteristicas > 0)
        //    {
        //        vA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Valor;
        //        tA = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.A).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 1)
        //    {
        //        vB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Valor;
        //        tB = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.B).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 2)
        //    {
        //        vC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Valor;
        //        tC = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.C).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 3)
        //    {
        //        vD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Valor;
        //        tD = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.D).FirstOrDefault().Tramo;
        //    }
        //    if (totCaracteristicas > 4)
        //    {
        //        vE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Valor;
        //        tE = caracteristicas.Where(c => c.LetraOrigenRegla == LetraCar.E).FirstOrDefault().Tramo;
        //    }

        //    //Buscar R
        //    List<DTOTarifaTest> listaObtenerR = new List<DTOTarifaTest>();
        //    if (totCaracteristicas > 0)
        //    {
        //        if (tA == Seleccion.SI)
        //        {
        //            listaObtenerR = valores.Where(v => vA >= v.Desde_1 && vA <= v.Hasta_1).ToList();
        //            acumular *= vA;
        //        }
        //        else if (tA == Seleccion.NO)
        //            listaObtenerR = valores.Where(v => vA == v.Desde_1).ToList();
        //    }

        //    if (totCaracteristicas > 1)
        //    {
        //        if (tB == Seleccion.SI)
        //        {
        //            listaObtenerR = listaObtenerR.Where(v => vB >= v.Desde_2 && vB <= v.Hasta_2).ToList();
        //            acumular *= vB;
        //        }
        //        else if (tB == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vB == v.Desde_2).ToList();
        //    }

        //    if (totCaracteristicas > 2)
        //    {
        //        if (tC == Seleccion.SI)
        //        {
        //            listaObtenerR = listaObtenerR.Where(v => vC >= v.Desde_3 && vB <= v.Hasta_3).ToList();
        //            acumular *= vC;
        //        }
        //        else if (tC == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vC == v.Desde_3).ToList();
        //    }

        //    if (totCaracteristicas > 3)
        //    {
        //        if (tD == Seleccion.SI)
        //        {
        //            listaObtenerR = listaObtenerR.Where(v => vD >= v.Desde_4 && vB <= v.Hasta_4).ToList();
        //            acumular *= vD;
        //        }
        //        else if (tD == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vD == v.Desde_4).ToList();
        //    }

        //    if (totCaracteristicas > 4)
        //    {
        //        if (tE == Seleccion.SI)
        //        {
        //            listaObtenerR = listaObtenerR.Where(v => vE >= v.Desde_5 && vB <= v.Hasta_5).ToList();
        //            acumular *= vE;
        //        }
        //        else if (tE == Seleccion.NO)
        //            listaObtenerR = listaObtenerR.Where(v => vE == v.Desde_5).ToList();
        //    }
        //    #endregion

        //    #region Calcular

        //    decimal? vFormula = 0;
        //    decimal? vMinimo = 0;
        //    decimal? valorR = 0;
        //    decimal? valorRmin = 0;

        //    vFormulaTemp = listaObtenerR.FirstOrDefault().Tarifa;
        //    vMinimoTemp = listaObtenerR.FirstOrDefault().Minimo;

        //    vFormula = acumular * vFormulaTemp;
        //    vMinimo = acumular * vMinimoTemp;

        //    valorR = vFormula;
        //    valorRmin = vMinimo;//

        //    //if (vMinimo > vFormula)
        //    //    valorR = vMinimo;
        //    //else
        //    //    valorR = vFormula;

        //    if (tA == Seleccion.MANUAL || tB == Seleccion.MANUAL ||
        //        tC == Seleccion.MANUAL || tD == Seleccion.MANUAL || tE == Seleccion.MANUAL)
        //    {
        //        decimal? Ra = 0;
        //        decimal? Rb = 0;
        //        decimal? Rc = 0;
        //        decimal? Rd = 0;
        //        decimal? Re = 0;

        //        string formula;
        //        string formulaMinima;
        //        string[] listaOperandos = new string[10];
        //        double[] listaValores = new double[10];
        //        DataTable Tbl = new DataTable();

        //        formula = regla.Formula;
        //        formulaMinima = regla.Minimo.Replace(LetraReg.R, LetraReg.Rmin);//

        //        foreach (var car in CaracteristicaTmp)
        //        {
        //            if (car.LetraOrigenRegla == LetraCar.A) Ra = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.B) Rb = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.C) Rc = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.D) Rd = car.Valor;
        //            if (car.LetraOrigenRegla == LetraCar.E) Re = car.Valor;
        //        }

        //        listaOperandos[0] = LetraCar.A;
        //        listaOperandos[1] = LetraCar.B;
        //        listaOperandos[2] = LetraCar.C;
        //        listaOperandos[3] = LetraCar.D;
        //        listaOperandos[4] = LetraCar.E;
        //        listaOperandos[5] = LetraReg.R;
        //        listaOperandos[6] = LetraReg.V;
        //        listaOperandos[7] = LetraReg.Rmin;//

        //        listaValores[0] = Convert.ToDouble(Ra);
        //        listaValores[1] = Convert.ToDouble(Rb);
        //        listaValores[2] = Convert.ToDouble(Rc);
        //        listaValores[3] = Convert.ToDouble(Rd);
        //        listaValores[4] = Convert.ToDouble(Re);

        //        listaValores[5] = Convert.ToDouble(valorR);
        //        listaValores[6] = Convert.ToDouble(VUM);
        //        listaValores[7] = Convert.ToDouble(valorRmin);//

        //        //Tbl.Columns.Add("variable", typeof(string));
        //        Tbl.Columns.Add(listaOperandos[0], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[1], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[2], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[3], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[4], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[5], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[6], typeof(double));
        //        Tbl.Columns.Add(listaOperandos[7], typeof(double));
        //        Tbl.Columns.Add("Tarifa", typeof(double), formula);
        //        Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

        //        {
        //            // crea una nueva línea 
        //            DataRow linea = Tbl.NewRow();
        //            linea[0] = listaValores[0];
        //            linea[1] = listaValores[1];
        //            linea[2] = listaValores[2];
        //            linea[3] = listaValores[3];
        //            linea[4] = listaValores[4];
        //            linea[5] = listaValores[5];
        //            linea[6] = listaValores[6];
        //            linea[7] = listaValores[7];//
        //            Tbl.Rows.Add(linea);
        //        }

        //        regla.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][8].ToString());
        //        regla.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][9].ToString());

        //        if (regla.ValorMinimo > regla.ValorFormula)
        //            regla.ValorR = regla.ValorMinimo;
        //        else
        //            regla.ValorR = regla.ValorFormula;
        //    }
        //    else
        //    {
        //        regla.ValorFormula = vFormula;
        //        regla.ValorMinimo = vMinimo;

        //        if (regla.ValorMinimo > regla.ValorFormula)
        //            regla.ValorR = regla.ValorMinimo;
        //        else
        //            regla.ValorR = regla.ValorFormula;
        //    }


        //    #endregion

        //    return regla;
        //}  // CASO 4 - OK

        //public void CalcularTarifa(DTOTarifa tarifa)
        //{
        //    decimal? Rt = 0;
        //    decimal? Rw = 0;
        //    decimal? Rx = 0;
        //    decimal? Ry = 0;
        //    decimal? Rz = 0;

        //    string formula;
        //    string formulaMinima;
        //    string[] listaOperandos = new string[10];
        //    double[] listaValores = new double[10];
        //    DataTable Tbl = new DataTable();

        //    formula = tarifa.Formula;
        //    formulaMinima = tarifa.Minima;

        //    foreach (var regla in ReglaAsocTmp)
        //    {
        //        if (regla.Letra == LetraReg.T) Rt = regla.ValorR;
        //        if (regla.Letra == LetraReg.W) Rw = regla.ValorR;
        //        if (regla.Letra == LetraReg.X) Rx = regla.ValorR;
        //        if (regla.Letra == LetraReg.Y) Ry = regla.ValorR;
        //        if (regla.Letra == LetraReg.Z) Rz = regla.ValorR;
        //    }

        //    listaOperandos[0] = LetraReg.T;
        //    listaOperandos[1] = LetraReg.W;
        //    listaOperandos[2] = LetraReg.X;
        //    listaOperandos[3] = LetraReg.Y;
        //    listaOperandos[4] = LetraReg.Z;
        //    listaOperandos[5] = LetraReg.R;
        //    listaOperandos[6] = LetraReg.V;

        //    listaValores[0] = Convert.ToDouble(Rt);
        //    listaValores[1] = Convert.ToDouble(Rw);
        //    listaValores[2] = Convert.ToDouble(Ry);
        //    listaValores[3] = Convert.ToDouble(Rx);
        //    listaValores[4] = Convert.ToDouble(Rz);

        //    listaValores[5] = 0;
        //    listaValores[6] = Convert.ToDouble(VUM);

        //    Tbl.Columns.Add(listaOperandos[0], typeof(double));
        //    Tbl.Columns.Add(listaOperandos[1], typeof(double));
        //    Tbl.Columns.Add(listaOperandos[2], typeof(double));
        //    Tbl.Columns.Add(listaOperandos[3], typeof(double));
        //    Tbl.Columns.Add(listaOperandos[4], typeof(double));
        //    Tbl.Columns.Add(listaOperandos[5], typeof(double));
        //    Tbl.Columns.Add(listaOperandos[6], typeof(double));
        //    Tbl.Columns.Add("Tarifa", typeof(double), formula);
        //    Tbl.Columns.Add("Minimo", typeof(double), formulaMinima);

        //    {
        //        // crea una nueva línea 
        //        DataRow linea = Tbl.NewRow();
        //        linea[0] = listaValores[0];
        //        linea[1] = listaValores[1];
        //        linea[2] = listaValores[2];
        //        linea[3] = listaValores[3];
        //        linea[4] = listaValores[4];
        //        linea[5] = listaValores[5];
        //        linea[6] = listaValores[6];
        //        Tbl.Rows.Add(linea);
        //    }

        //    tarifa.ValorFormula = Convert.ToDecimal(Tbl.Rows[0][7].ToString());
        //    tarifa.ValorMinimo = Convert.ToDecimal(Tbl.Rows[0][8].ToString());

        //}


        //#endregion


        public static bool validarVariables()
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
                msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable PrefijoFact1, no está inicializada en el App.Config";
                ucLog.GrabarLogTexto(msj);
            }
            if (System.Configuration.ConfigurationManager.AppSettings["PrefijoFact2"] != null && flgVal == true)
            {
                K_PREF2 = System.Configuration.ConfigurationManager.AppSettings["PrefijoFact2"];
                flgVal = true;
            }
            else
            {
                msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable PrefijoFact2, no está inicializada en el App.Config";
                ucLog.GrabarLogTexto(msj);
            }
            if (System.Configuration.ConfigurationManager.AppSettings["PrintName"] != null && flgVal == true)
            {
                NombImpresora = System.Configuration.ConfigurationManager.AppSettings["PrintName"];
                flgVal = true;
            }
            else
            {
                msj = "SRGRDA-SERVICIO WINDOWS /" + " LOCAL PRINT /" + " InitializeServices /" + " La variable PrintName, no está inicializada en el App.Config";
                ucLog.GrabarLogTexto(msj);
            }

            return flgVal;
        }
        public class LetraReg
        {
            public const string R = "R";
            public const string Rmin = "Rmin";
            public const string V = "V";
            public const string T = "T";
            public const string W = "W";
            public const string X = "X";
            public const string Y = "Y";
            public const string Z = "Z";
        }
       
    }
}
