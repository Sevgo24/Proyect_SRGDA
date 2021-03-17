using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGRDA.Entities.FacturaElectronica;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace SGRDA.DA.FacturacionElectronica
{
    public class DAExtras
    {
        Database db = new DatabaseProviderFactory().Create("conexion");

        public List<BEExtras> ListarExtras(string owner, decimal IdFactura)
        {
            var lista = new List<BEExtras>();
            BEExtras factura = null;

            factura = new BEExtras();
            factura.MailEnvio = "-";
            factura.MailCopia = "-";
            factura.MailCopiaOculta = "-";
            factura.LineaReferencia = "-";
            factura.NombreAdjunto = "-";
            factura.DescripcionAdjunto = "-";
            lista.Add(factura);
            return lista;
        }
    }
}
