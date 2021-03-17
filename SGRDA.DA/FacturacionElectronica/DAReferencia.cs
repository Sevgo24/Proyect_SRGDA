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
    public class DAReferencia
    {
        Database db = new DatabaseProviderFactory().Create("conexion");

        public List<BEReferencia> ListarRefFactura(string owner, decimal IdFactura)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_FACTURACION_REF");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, IdFactura);
            db.ExecuteNonQuery(oDbCommand);
            var lista = new List<BEReferencia>();
            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                BEReferencia factura = null;
                while (dr.Read())
                {
                    factura = new BEReferencia();
                    factura.NroLinRef = dr.GetString(dr.GetOrdinal("NroLinRef"));
                    factura.TpoDocRef = dr.GetString(dr.GetOrdinal("TpoDocRef"));
                    factura.SerieRef = dr.GetString(dr.GetOrdinal("SerieRef"));
                    factura.FolioRef = dr.GetString(dr.GetOrdinal("FolioRef"));
                    lista.Add(factura);
                }
            }
            return lista;
        }
    }
}
