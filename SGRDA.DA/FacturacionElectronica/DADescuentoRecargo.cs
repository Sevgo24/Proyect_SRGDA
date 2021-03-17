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
    public class DADescuentoRecargo
    {
        Database db = new DatabaseProviderFactory().Create("conexion");

        public List<BEDescuentoRecargo> ListarDescuentos(string owner, decimal IdFactura)
        {
            var lista = new List<BEDescuentoRecargo>();
            BEDescuentoRecargo factura = null;
            factura = new BEDescuentoRecargo();
            factura.NroLindDR = "-"; //dr.GetString(dr.GetOrdinal("NroLindDR"));
            factura.TpoMov = "-"; //dr.GetString(dr.GetOrdinal("TpoMov"));
            factura.ValorDR = "-"; //dr.GetString(dr.GetOrdinal("ValorDR"));
            lista.Add(factura);
            return lista;

            //DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_DESCUENTOS_INTERFAZ");
            //db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            //db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, IdFactura);
            //db.ExecuteNonQuery(oDbCommand);
            //var lista = new List<BEDescuentosRecargos>();
            //using (IDataReader dr = db.ExecuteReader(oDbCommand))
            //{
            //    BEDescuentosRecargos factura = null;
            //    while (dr.Read())
            //    {
            //        factura = new BEDescuentosRecargos();
            //        factura.NroLindDR = dr.GetString(dr.GetOrdinal("NroLindDR"));
            //        factura.TpoMov = dr.GetString(dr.GetOrdinal("TpoMov"));
            //        factura.ValorDR = dr.GetString(dr.GetOrdinal("ValorDR"));
            //        lista.Add(factura);
            //    }
            //}
            //return lista;
        }
    }
}
