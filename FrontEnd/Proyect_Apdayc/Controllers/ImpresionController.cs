using Proyect_Apdayc.Clases;
using Proyect_Apdayc.Clases.DTO.Impresion;
using SGRDA.BL;
using SGRDA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect_Apdayc.Controllers
{
    public class ImpresionController : Controller
    {
        //
        // GET: /Impresion/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Facturar() {

            DTOFactura obj = new DTOFactura();

            obj.RUC = "10410025502";
            obj.RUM = "34566778";
            obj.Fecha = "10/10/2015";
            obj.Local = "RADIODIFUSORA COMERCIAL SINFONIA EN STEREO";
            obj.Nro = "000345";
            obj.Usuario="JUAN PEREZ ESTERIO FM SA";
            obj.Direccion = "MIRAFLORES 234 RICARDO PALMA AVENIDA 234";


            List<DTOFacturaDeta> list = new List<DTOFacturaDeta>();

            DTOFacturaDeta detalle = new DTOFacturaDeta();
            detalle.Codigo = "01";
            detalle.Cantidad = "1";
            detalle.Descripcion = "COMUNICACION PUBLICA : RADIO EXONERADO DEL IGV";
            detalle.Subtotal = "120.50";
            detalle.Total = "120.50";

            list.Add(detalle);
            detalle = new DTOFacturaDeta();
            detalle.Codigo = "02";
            detalle.Cantidad = "1";
            detalle.Descripcion = "COMUNICACION PUBLICA : TV  DEL IGV";
            detalle.Subtotal = "20";
            detalle.Total = "23";


            list.Add(detalle);
            detalle = new DTOFacturaDeta();
            detalle.Codigo = "02";
            detalle.Cantidad = "345.8";
            detalle.Descripcion = "COMUNICACION PRIVADA V";
            detalle.Subtotal = "20";
            detalle.Total = "345.8";




            list.Add(detalle);


            obj.Detalle = list;


            return View(obj);
        }

    }
}
