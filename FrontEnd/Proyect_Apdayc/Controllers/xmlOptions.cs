using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SGRDA.Entities;
using SGRDA.BL;
using System.Xml.Linq;
using Proyect_Apdayc.Controllers;

namespace SGRDA.MVC.Controllers
{
    public class xmlOptions
    {
        public XElement menuOptions()
        {
            List<MODULO> lst = new List<MODULO>();

            lst = new BLMODULO().MODULO_spBuscarMenu(1, USUARIO_LOGINController.s);


            string elementMenu = string.Empty;

            elementMenu = "<Root>";

            foreach (var element in lst)
            {
                elementMenu += "<Node>";
                elementMenu += "<id>" + element.MODU_ICODIGO_MODULO + "</id>";
                //elementMenu += "<modID>" + element.CABE_ICODIGO_MODULO + "</modID>";
                elementMenu += "<patherID>" + element.MODU_ICODIGO_MODULO_DEPENDIENTE + "</patherID>";
                elementMenu += "<name>" + element.MODU_VNOMBRE_MODULO + "</name>";
                elementMenu += "<url>" + element.MODU_VRUTA_PAGINA + "</url>";
                elementMenu += "<level>" + element.MODU_INIVEL_MODULO + "</level>";
                elementMenu += "</Node>";
            }

            elementMenu += "</Root>";

            return XElement.Parse(elementMenu);
        }
    }
}