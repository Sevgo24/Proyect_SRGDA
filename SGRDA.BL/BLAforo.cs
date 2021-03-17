using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLAforo
    {
        public List<BEAforo> Listar(string owner)
        {
            return new DAAforo().Listar(owner);
        }
        public BEAforo ObtenerAforoXCod(string owner, string idAforo)
        {
            return new DAAforo().ObtenerAforoXCod(owner, idAforo);
        }
        /// <summary>
        /// PERMITE CALCULAR EL TOTAL DE AFORO POR TIPO DE AFORO
        /// </summary>
        /// <param name="CAP_ID"></param>
        /// <param name="LICID"></param>
        /// <returns></returns>
        public decimal CalculaMontoLiquidarAforo(string CAP_ID, decimal LICID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAAforo().CalculaMontoLiquidarAforo(owner, CAP_ID, LICID);
        }
        /// <summary>
        /// INSERTA EN LA TABLA  REC_CAPACITY_LIC
        /// </summary>
        /// <param name="owner">dueño</param>
        /// <param name="licid">licencia </param>
        /// <param name="capid">tipo de aforo</param>
        /// <param name="cap_iprelq"> PRELIQUIDACION / LIQUIDACION</param>
        /// <param name="total">EL TOTAL </param>
        /// <param name="log_user_create">USUARIO QUE INSERTO</param>
        /// <returns></returns>
        public int INSERTAR_AFORO_LIC(decimal licid,string capid,string cap_iprelq,decimal total,string log_user_create  )
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAAforo().INSERTAR_AFORO_LIC(owner,licid,capid,cap_iprelq,total,log_user_create);
        }
    }
}
