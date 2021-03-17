using SGRDA.DA;
using SGRDA.DA.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SGRDA.BL
{
    public class BLCorreo
    {
    
        /// <summary>
        /// Lista de correos de un usuario x licencia
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idLic"></param>
        /// <returns></returns>
        public List<string> CorreoNotificarUsuLic(string owner, decimal idLic)
        {
            return new DACorreo().CorreoNotificarUsuLic(idLic, owner);
        }
        /// <summary>
        /// lista de correos de usuarios agentes configurado en los parametros
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idAm"></param>
        /// <returns></returns>
        public List<string> CorreoNotificarAgente(string owner, decimal idAm)
        {
            List<string> correos=null;
            var items= new DA_WORKF_PARAMETERS().ListarParameterXActions(owner, idAm);

            if (items != null && items.Count > 0) {
                correos = new List<string>();
                items.ForEach(x => { if (isValidEmail(x.WRKF_PVALUE))correos.Add(x.WRKF_PVALUE);});
            }
            return correos;
        }
        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
    }
}
