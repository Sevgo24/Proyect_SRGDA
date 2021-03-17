 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyect_Apdayc.Clases;
using SGRDA.Utility;


namespace Proyect_Apdayc.Clases
{
    public class Base:Controller
    {

        public string UsuarioActual
        {
            get
            {
               return Convert.ToString(Session[Constantes.Sesiones.Usuario]).ToUpper();
            }
        }
        public decimal PerfilUsuarioActual
        {
            get
            {
                return Convert.ToDecimal(Session[Constantes.Sesiones.CodigoPerfil]);
            }
        }

        public decimal CodigoUsuarioOficina
        {
            get
            {
                return Convert.ToDecimal(Session[Constantes.Sesiones.CodigoUsuarioOficina]);
            }
        }
        /// <summary>
        /// Funcion que redirecciona segun el tipo de retorno de la  funcion donde se invoca
        /// </summary>
        /// <param name="esJson"></param>
        public  void Init(bool esJson=true){

             if (Session[Constantes.Sesiones.Usuario] == null)
             {
                 if (!esJson)
                     Response.Redirect("~/Login");
                 else
                     RedirectToAction("Index", "Login");
             }
             else {

                 setIdPerfil();
             }
         }

 

        
         public string CleanInput(string strIn)
         {
             // Replace invalid characters with empty strings.
             try
             {
                 var cadena= System.Text.RegularExpressions.Regex.Replace(strIn, @"[^\w\.@-]", "",
                                      System.Text.RegularExpressions.RegexOptions.None, TimeSpan.FromSeconds(1.5));

                 cadena = cadena.Replace("á","a");
                 cadena = cadena.Replace("é", "e");
                 cadena = cadena.Replace("í", "i");
                 cadena = cadena.Replace("ó", "o");
                 cadena = cadena.Replace("ú", "u");
                 cadena = cadena.Replace("Á", "A");
                 cadena = cadena.Replace("É", "E");
                 cadena = cadena.Replace("Í", "I");
                 cadena = cadena.Replace("Ó", "O");
                 cadena = cadena.Replace("Ú", "U");
                 cadena = cadena.Replace("ñ", "n");
                 cadena = cadena.Replace("Ñ", "N");
                 return cadena;
             }
             // If we timeout when replacing invalid characters, 
             // we should return Empty.
             catch (System.Text.RegularExpressions.RegexMatchTimeoutException)
             {
                 return String.Empty;
             }
         }


         #region Seguridad Y Accesos
         public bool isLogout(ref Resultado resultado)
         {
             if (Session[Constantes.Sesiones.Usuario] == null)
             {
                 resultado.result = Constantes.MensajeRetorno.LOGOUT;
                 resultado.message = Constantes.MensajeGenerico.MSG_LOGOUT;
                 resultado.isRedirect = true;
                 resultado.redirectUrl = Url.Action("Index", "Login");
                 return true;
             }
             else
                 return false;
         }

         public void setIdPerfil()
         {
             if (Request.QueryString["idPer"] != null && Request.QueryString["idPerUsu"] != null)
             {
                 Session[Constantes.Sesiones.CodigoPerfil] = Request.QueryString["idPer"];
                 Session[Constantes.Sesiones.CodigoPerdilUsuario] = Request.QueryString["idPerUsu"];
                 Session[Constantes.Sesiones.MenuCambiaRol] = "S";
             }
             else
             {
                 Session[Constantes.Sesiones.MenuCambiaRol] = "N";
             }
         }

         #endregion

        /// <summary>
        /// Obtiene la cantidad de decimales del valor
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
         public int getCantDecimal(double value)
         {
             string outPut = "";
             try
             {
                 if (value.ToString().Split('.').Length == 2)
                 {
                     outPut = value.ToString().Split('.')[1].Substring(0, value.ToString().Split('.')[1].Length);
                 }
             }
             catch
             {
                 outPut = "";
             }
             return outPut.Length;
         }

        /// <summary>
        /// muestra la cantidad de decimales sin redondear
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimalPlaces"></param>
        /// <returns></returns>
         public  double Truncate(double value, int decimalPlaces)
         {
             double integralValue = Math.Truncate(value);

             double fraction = value - integralValue;

             int factor = (int)Math.Pow(10, decimalPlaces);

             double truncatedFraction = Math.Truncate(fraction * factor) / factor;

             double result = integralValue + truncatedFraction;

             return result;
         }   

       
    }
}
