using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities;
using System.Xml;

namespace SGRDA.DA
{
    public class DAFacturaVencimiento
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public bool RegistrarVencimientoFactXML(string xml)
        {
            bool exito = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASI_FACTURA_VENCIMIENTO"))
            {
                oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                exito = oDataBase.ExecuteNonQuery(cm) > 0;
            }
            return exito;
        }

        public bool InsertarVencimiento(BEFactura en)
        {
            bool exito = false;
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("INSERTAR_VENCIMIENTO_FACTURA");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, en.INV_ID);
            db.AddInParameter(oDbCommand, "@PAY_ID", DbType.String, en.PAY_ID);
            db.AddInParameter(oDbCommand, "@INV_BASE", DbType.Decimal, en.INV_BASE);
            db.AddInParameter(oDbCommand, "@INV_TAXES", DbType.Decimal, en.INV_TAXES);
            db.AddInParameter(oDbCommand, "@INV_NET", DbType.Decimal, en.INV_NET);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_UPDATE.ToUpper());
            exito = db.ExecuteNonQuery(oDbCommand) > 0;
            return exito;
        }
    }
}
