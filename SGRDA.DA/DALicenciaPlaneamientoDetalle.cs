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
using System.Data.Common;

namespace SGRDA.DA
{
    public class DALicenciaPlaneamientoDetalle
    {
        Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public bool InsertarXML( string xml)
        {
            bool exito = false;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASI_PLANIFICACION_DETALLE"))
            {
                oDataBase.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                exito = oDataBase.ExecuteNonQuery(cm) > 0;
            }
            exito = true;
            return exito;
        }

        public List<BELicenciaPlaneamientoDetalle> ObtenerPagosParciales(string owner,int idfactura)
        {
            List<BELicenciaPlaneamientoDetalle> lista = new List<BELicenciaPlaneamientoDetalle>();
            BELicenciaPlaneamientoDetalle item = null;
            using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTENER_PAGOS_PARCIALES"))
            {
                oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                oDataBase.AddInParameter(cm, "@INV_ID", DbType.Decimal, idfactura);
                using (IDataReader dr = oDataBase.ExecuteReader(cm))
                {
                    while (dr.Read())
                    {
                        item = new BELicenciaPlaneamientoDetalle();
                        item.LIC_PL_ID = dr.GetDecimal(dr.GetOrdinal("LIC_PL_ID"));
                        item.LIC_INVOICE_LINE = dr.GetDecimal(dr.GetOrdinal("LIC_INVOICE_LINE"));
                        lista.Add(item);
                    }
                }
            }
            return lista;
        }

        public bool ActualizarDetallePlanificacion(BELicenciaPlaneamientoDetalle detalle)
        {
            bool exito = false;
            //var lista = new List<BEFacturaDetalle>();
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDAU_DETALLE_PLANIFICACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@LIC_PL_ID", DbType.Decimal, detalle.LIC_PL_ID);
            db.AddInParameter(oDbCommand, "@INV_ID", DbType.Decimal, detalle.INV_ID);
            db.AddInParameter(oDbCommand, "@LIC_INVOICE_VAL", DbType.Decimal, detalle.LIC_INVOICE_VAL);
            db.AddInParameter(oDbCommand, "@LIC_INVOICE_LINE", DbType.Decimal, detalle.LIC_INVOICE_LINE);
            db.AddInParameter(oDbCommand, "@LIC_PL_PARTIAL", DbType.Boolean, detalle.LIC_PL_PARTIAL);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, detalle.LOG_USER_CREAT);
            exito = db.ExecuteNonQuery(oDbCommand) > 0;
            return exito;
        }

    }
}
