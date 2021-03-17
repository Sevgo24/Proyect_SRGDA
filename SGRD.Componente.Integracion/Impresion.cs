using SGRDA.Servicios.Proxy.Entidad;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SGRD.Componente.Integracion
{
    public class Impresion
    {

        public  Impresion()
        {
            
        }


        /// <summary>
        /// GENERA EL FORMATO PARA ENVIAR A IMPRESION LOS DATOS
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="codigo"></param>
       public void PrintPage(PrintPageEventArgs ev, decimal codigo)
        {
            Font fuente = new Font("Arial", 9);
            using (SGRDA.Servicios.Proxy.SEFacturaClient servFact = new SGRDA.Servicios.Proxy.SEFacturaClient())
            {
                Factura factura = servFact.ObtenerFactura(codigo);
                if (factura != null && factura.Detalle != null)
                {
                    if (factura.TipoFactura == "FA")
                    {
                        float pos_x = 100;
                        float pos_y = 10;
                        var socio = factura.RazonSocial;
                        var direccion = factura.Direccion;
                        var RUM = factura.RUM;
                        var RUC = factura.RUC;
                        var local = factura.Local;
                        string numero = Convert.ToString(factura.NumFact);
                        var factor = 17;
                        var factorFecha = 380;// 370;

                        pos_x = 165;//inicio del eje x inicio de texto
                        pos_y = 155;//160 inicio del eje y fila de texto

                        ev.Graphics.DrawString(numero, fuente, Brushes.Black, pos_x + 350, (pos_y - 20), new StringFormat());

                        ev.Graphics.DrawString(factura.FechaEmision.HasValue ? factura.FechaEmision.Value.ToShortDateString() : "-", fuente, Brushes.Black, pos_x + factorFecha, (pos_y), new StringFormat());
                        ev.Graphics.DrawString(RUM, fuente, Brushes.Black, pos_x, (pos_y), new StringFormat());
                        ev.Graphics.DrawString(socio, fuente, Brushes.Black, pos_x, (pos_y + (factor * 1)), new StringFormat());
                        ev.Graphics.DrawString(RUC, fuente, Brushes.Black, pos_x, (pos_y + (factor * 2)), new StringFormat());
                        ev.Graphics.DrawString(local, fuente, Brushes.Black, pos_x, (pos_y + (factor * 3)), new StringFormat());
                        ev.Graphics.DrawString(direccion, fuente, Brushes.Black, pos_x, (pos_y + (factor * 4)) + 4, new StringFormat());

                        factor = 20;
                        var pos_x_deta_ini = 1;
                        var pos_y_deta_ini = (pos_y + (factor * 5));
                        var factorTotalDeta = 710;// 650;680
                        var factorIniDesDeta = 50;// 70;
                        int i = 1;

                        pos_y_deta_ini = pos_y_deta_ini + 5;
                        foreach (var item in factura.Detalle)
                        {
                            // var valor = 100 * i;
                            var fila = (pos_y_deta_ini + (factor * i));
                            ev.Graphics.DrawString(Convert.ToString(item.Item), fuente, Brushes.Black, pos_x_deta_ini, fila, new StringFormat());
                            ev.Graphics.DrawString(item.Descripcion, fuente, Brushes.Black, pos_x_deta_ini + factorIniDesDeta, fila, new StringFormat());
                            ev.Graphics.DrawString(String.Format("{0:N}", item.SubTotal), fuente, Brushes.Black, pos_x_deta_ini + factorTotalDeta, fila, new StringFormat());

                            i++;
                        }
                        ev.Graphics.DrawString(String.Format("SON: {0}", factura.TotalLetras), fuente, Brushes.Black, pos_x_deta_ini + factorIniDesDeta, 400, new StringFormat());
                        ev.Graphics.DrawString(String.Format("{0:N}", factura.Total), fuente, Brushes.Black, pos_x_deta_ini + factorTotalDeta, 440, new StringFormat());
                    }

                    if (factura.TipoFactura == "BO") // boleta
                    {
                        float pos_x = 100;
                        float pos_y = 10;
                        var socio = factura.RazonSocial;
                        var direccion = factura.Direccion;
                        var RUM = factura.RUM;
                        var RUC = factura.RUC;
                        var local = factura.Local;
                        string numero = Convert.ToString(factura.NumFact);
                        var factor = 25;
                        var factorFecha = 280;// 370;

                        pos_x = 170;//165inicio del eje x inicio de texto
                        pos_y = 225;//155 inicio del eje y fila de texto

                        ev.Graphics.DrawString(numero, fuente, Brushes.Black, pos_x + 250, (pos_y - 30), new StringFormat());

                        ev.Graphics.DrawString(factura.FechaEmision.HasValue ? factura.FechaEmision.Value.ToShortDateString() : "-", fuente, Brushes.Black, pos_x + factorFecha, (pos_y-30), new StringFormat());
                        ev.Graphics.DrawString(RUM, fuente, Brushes.Black, pos_x, (pos_y), new StringFormat());
                        ev.Graphics.DrawString(socio, fuente, Brushes.Black, pos_x, (pos_y + (factor * 1)), new StringFormat());
                        ev.Graphics.DrawString(direccion, fuente, Brushes.Black, pos_x, (pos_y + (factor * 2)), new StringFormat());
                        ev.Graphics.DrawString(local, fuente, Brushes.Black, pos_x, (pos_y + (factor * 3)), new StringFormat());
                        

                        factor = 20;
                        var pos_x_deta_ini = 1;
                        var pos_y_deta_ini = (pos_y + (factor * 5));
                        var factorTotalDeta = 650;// 650;680
                        var factorIniDesDeta = 100;// 70;
                        int i = 1;

                        pos_y_deta_ini = pos_y_deta_ini + 5;
                        foreach (var item in factura.Detalle)
                        {
                            // var valor = 100 * i;
                            var fila = (pos_y_deta_ini + (factor * i));
                            ev.Graphics.DrawString(Convert.ToString(item.Item), fuente, Brushes.Black, pos_x_deta_ini, fila, new StringFormat());
                            ev.Graphics.DrawString(item.Descripcion, fuente, Brushes.Black, pos_x_deta_ini + factorIniDesDeta, fila, new StringFormat());
                            ev.Graphics.DrawString(String.Format("{0:N}", item.SubTotal), fuente, Brushes.Black, pos_x_deta_ini + factorTotalDeta, fila, new StringFormat());

                            i++;
                        }
                        ev.Graphics.DrawString(String.Format("SON: {0}", factura.TotalLetras), fuente, Brushes.Black, pos_x_deta_ini + factorIniDesDeta, 480, new StringFormat());
                        ev.Graphics.DrawString(String.Format("{0:N}", factura.Total), fuente, Brushes.Black, pos_x_deta_ini + factorTotalDeta, 510, new StringFormat());
                    }

                }

            }
        }

        /// <summary>
        /// ACTUALIZA LA TABLA DE IMPRESSION CON EL ESTADO RESPECTIVO
        /// </summary>
        /// <param name="idActualizar"></param>
        /// <param name="idDoc"></param>
        /// <param name="estado"></param>
        public void ActualizarTMPImpresion(decimal idActualizar, decimal idDoc, string estado)
        {
            using (SGRDA.Servicios.Proxy.SEPreImpresionClient servPrePrint = new SGRDA.Servicios.Proxy.SEPreImpresionClient())
            {
                PreImpresion objs = servPrePrint.ObtenerPreImpresion(idActualizar);
                if (objs != null && objs.FECHA_IMP == null)
                {
                    servPrePrint.ActualizarEstado(idActualizar,  getIpv4(Dns.GetHostName()), estado);
                }
            }
        }
       
        public string getIpv4(string host)
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


        /// <summary>
        /// LISTA LAS IMPRESORAS INSTALADAS EN LA PC DEL USUARIO
        /// </summary>
        /// <returns></returns>
        public StringCollection GetPrintersCollection()
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

        /// <summary>
        /// OBTIENE UNA LISTA DE JOBS PENDIENTES
        /// </summary>
        /// <param name="printerName"></param>
        /// <param name="documentoPrint"></param>
        /// <param name="isPrinterOnline"></param>
        /// <param name="validatePrinOnline"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> GetPrintJobsCollection(string printerName, string documentoPrint, out bool isPrinterOnline, bool validatePrinOnline = true)
        {
            List<KeyValuePair<string, string>> printJobCollection = new List<KeyValuePair<string, string>>();

            if (validatePrinOnline)
            {
                isPrinterOnline = false;
                isPrinterOnline = ImpresoraActiva(printerName, isPrinterOnline);
            }
            else
            {
                isPrinterOnline = true;
            }
            if (isPrinterOnline)
            {
                string searchQuery = string.Format("SELECT * FROM Win32_PrintJob   WHERE   Document='{0}' ", documentoPrint);
                ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(searchQuery);
                ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();

                foreach (ManagementObject prntJob in prntJobCollection)
                {
                    string jobName = prntJob.Properties["Name"].Value.ToString();
                    string documentName = prntJob.Properties["Document"].Value.ToString();

                    if (documentoPrint == documentName)
                    {
                        char[] splitArr = new char[1];
                        splitArr[0] = Convert.ToChar(",");
                        string prnterName = jobName.Split(splitArr)[0];
                        string estado = prntJob.Properties["status"].Value.ToString();
                        string m_JobID = prntJob.Properties["JobId"].Value.ToString();
                        if (String.Compare(prnterName, printerName, true) == 0)
                        {
                            printJobCollection.Add(new KeyValuePair<string, string>(documentName, string.Format("{0}*{1}", estado, m_JobID)));
                        }
                    }
                }
            }
            return printJobCollection;
        }

        private bool ImpresoraActiva(string printerName, bool isPrinterOnline)
        {
            isPrinterOnline = false;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            string xprinterName = "";
            foreach (ManagementObject printer in searcher.Get())
            {
                xprinterName = printer["Name"].ToString().ToLower();
                if (xprinterName.Equals(printerName.ToLower()))
                {
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
        public string GetDefaultPrinterName()
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


        /// <summary>
        /// BOT QUE EVALUA SI LA IMPRESION FUE EXITOSA O NO PARA ACTUALIZAR LA TABLA DE IMPRESION 
        /// </summary>
        /// <param name="docsImprimir"></param>
        /// <param name="tmpContador"></param>
        /// <param name="NombImpresora"></param>
        /// <param name="fileSentPrint"></param>
        /// <param name="FormatoNameDoc"></param>
        /// <param name="K_PREF1"></param>
        /// <param name="K_PREF2"></param>
        /// <param name="localhost"></param>
        public void checkPrintSuccessful(List<PreImpresion> docsImprimir, int tmpContador, string NombImpresora, string fileSentPrint, string FormatoNameDoc, string K_PREF1, string K_PREF2, string localhost)
        {

            if (docsImprimir != null && docsImprimir.Count > 0 && tmpContador < docsImprimir.Count)
            {
                bool idPrinterOnline;
                List<KeyValuePair<string, string>> jobs = GetPrintJobsCollection(NombImpresora, fileSentPrint, out idPrinterOnline);

                var idActualizar = docsImprimir[tmpContador].ID;
                var idDoc = docsImprimir[tmpContador].ID_DOCUMENTO;

                if (idPrinterOnline)
                {
                    if (jobs.Count > 0)
                    {
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
                                    using (SGRDA.Servicios.Proxy.SEPreImpresionClient servPrePrint = new SGRDA.Servicios.Proxy.SEPreImpresionClient())
                                    {
                                        List<SGRDA.Servicios.Proxy.Entidad.PreImpresion> docsImprimirVal = servPrePrint.ListarPendientes(localhost).ToList();
                                        existeQueAndPend = docsImprimirVal.Exists(x => x.ID_DOCUMENTO == docsImprimir[tmpContador].ID_DOCUMENTO);
                                    }

                                    string[] EstadoJob = nombre.Value.Split('*');
                                    string estadoPrint = EstadoJob[0];
                                    int idJob = Convert.ToInt32(EstadoJob[1]);

                                    if ((estadoPrint == "5" || estadoPrint == "OK") && existeQueAndPend)
                                    {
                                        /*VALIDAR SI LA IMPRESORA ESTA ACTIVA LUEGO DE PASAR LAS VALIDACIONES DE IMPRESION COMO  OFFLINE.*/
                                        bool isPrinterOnline = false;
                                        var result = ImpresoraActiva(NombImpresora, isPrinterOnline);
                                        if (result)
                                        {   /*SI ENTRÓ A ESTA VALIDACION SE INTERPRETA QUE LA IMPRESORA EESTUVO ACTIVA Y SE IMPRIMIÓ CORRECTAMENTE*/
                                            ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.FINALIZADO);
                                        }
                                        else
                                        {
                                            ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.OFFLINE_PRINT2);
                                        }
                                    }
                                    else
                                    {
                                        ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.ERROR);
                                    }
                                }
                                else
                                {
                                    ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.ANULADO);
                                }
                            }
                            else
                            {
                                ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.CANCELADO);
                            }
                        }
                    }
                    else
                    {
                        ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.NO_JOB);
                    }
                }
                else
                {
                    ActualizarTMPImpresion(idActualizar, idDoc, Constante.EstadoImpresion.OFFLINE_PRINT);

                }
            }
        }
    }
    public static class Constante
    {
        public class EstadoImpresion
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
}
