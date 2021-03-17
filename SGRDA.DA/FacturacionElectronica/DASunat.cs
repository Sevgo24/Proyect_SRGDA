using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using SGRDA.Entities;
using SGRDA.Entities.FacturaElectronica;
using System.Data.Common;
using System.Data;
using System.Xml;

namespace SGRDA.DA.FacturacionElectronica
{
    public class DASunat
    {
        public int ActualizarEstadoSunat(BESunat factura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_ESTADO_SUNAT");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, factura.OWNER);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, factura.INV_ID);
            db.AddInParameter(oDbCommand, "@ESTADO_SUNAT", DbType.String, factura.ESTADO_SUNAT.ToUpper());
            db.AddInParameter(oDbCommand, "@OBSERVACION_SUNAT", DbType.String, factura.OBSERVACION_SUNAT.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int ActualizarObs(BESunat factura)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_OBS_FACTURA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, factura.OWNER);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, factura.INV_ID);
            db.AddInParameter(oDbCommand, "@CODE_DESCRIPTION", DbType.String, factura.CODE_DESCRIPTION);
            db.AddInParameter(oDbCommand, "@INV_NULLREASON", DbType.String, factura.INV_NULLREASON.ToUpper());
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, factura.LOG_USER_UPDATE.ToUpper());
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }
    }
}
