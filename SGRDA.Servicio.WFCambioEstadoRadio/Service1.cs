using SGRD.Componente.Integracion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ucLogApp;

namespace SGRDA.Servicio.WFCambioEstadoRadio
{
    public partial class Service1 : ServiceBase
    {

        System.Timers.Timer myTimer = null;
        bool flgInitTimer1 = false;
        string OWNER = "";
        string ServTitLog = "SRGRDA-SERVICIO WINDOWS / CAMBIAR ESTADO AT RADIO /  InitializeServices / "; 
        int time1 = 0;

        public Service1()
        {
            InitializeComponent();

             if (validarVariables())
             {
                 myTimer = new System.Timers.Timer(time1);
                 myTimer.Elapsed += myTimer_Elapsed;
                 flgInitTimer1 = true;
             }

        }

        void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                WorkFlowBotRadio serv = new WorkFlowBotRadio();
                serv.CambiarATEstadoWFRadioAsync(OWNER);
            }
            catch (Exception ex)
            {
                ucLog.GrabarLogError("SRGRDA-SERVICIO WINDOWS", "CAMBIAR ESTADO AT RADIO", "myTimer_Elapsed", ex);
            }

        }

        protected override void OnStart(string[] args)
        {
            myTimer.Start();

        }

        protected override void OnStop()
        {
            myTimer.Stop();
        }

        private bool validarVariables()
        {
            bool flgVal = false;
            string msj = string.Empty;

            if (System.Configuration.ConfigurationManager.AppSettings["PrefijoOWNER"] != null)
            {
                OWNER = System.Configuration.ConfigurationManager.AppSettings["PrefijoOWNER"];
                flgVal = true;
            }
            else
            {
                msj = ServTitLog+ " La variable PrefijoFact1, no está inicializada en el App.Config \n";
            }

            int valMin = 0;
            bool flgParse=false;
            if (System.Configuration.ConfigurationManager.AppSettings["MinTiempoEjecutarProceso"] != null)
            {
                flgParse = Int32.TryParse(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MinTiempoEjecutarProceso"]), out valMin);
                if (flgParse)
                {
                    if (valMin >= 10000) //minimo cada 10 segundos 
                    {
                        flgVal = true;
                    }
                    else {
                        
                        msj = ServTitLog + " La variable MinTiempoEjecutarProceso debe contener el Valor  mayor igual a 10000 \n";
                        flgVal = false;
                    }
                   
                }
                else
                {
                    msj = ServTitLog + " La variable MinTiempoEjecutarProceso, no contiene el valor esperado en el App.Config. Valor esperado es númerico \n";
                    flgVal = false;
                }
            }
            else
            {
                msj = ServTitLog + " La variable MinTiempoEjecutarProceso, no está inicializada en el App.Config \n";
                flgVal = false;
            }

            if (System.Configuration.ConfigurationManager.AppSettings["TiempoEjecutarProceso"] != null)
            {
              
                bool flg1 = Int32.TryParse(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TiempoEjecutarProceso"]), out time1);

                if (flg1 && flgParse)
                {
                    if (time1 >= valMin)
                    {
                        flgVal = true;
                    }
                    else
                    {
                        msj = ServTitLog + string.Format(" El valor de la variable TiempoEjecutarProceso({0}) debe ser mayor igual a la variable MinTiempoEjecutarProceso({1}) en el App.Config \n", time1, valMin);
                        flgVal = false;
                    }
                }
            }
            else {
                flgVal = false;
                msj = ServTitLog + " No existe la variable TiempoEjecutarProceso en el App.Config \n";
            }
            

            if (!flgVal)
            {
                ucLog.GrabarLogTexto(msj);
            }
            return flgVal;
        }
    }
}
