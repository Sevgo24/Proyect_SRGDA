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
    public class DALicenciaLocalidad
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public BELicenciaLocalidad ObtenerLicLocalidadXCod(string owner, decimal idLicenciaLocalidad)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_LICENCIA_LOCALIDAD_X_COD");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@LIC_SEC_ID", DbType.Decimal, idLicenciaLocalidad);

            BELicenciaLocalidad obj = null;

            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    obj = new BELicenciaLocalidad();
                    // obj.LIC_SEC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_SEC_ID"));
                    obj.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    //  obj.CAP_ID = dr.GetString(dr.GetOrdinal("CAP_ID"));
                    obj.SEC_ID = dr.GetDecimal(dr.GetOrdinal("SEC_ID"));
                    //  obj.SEC_PERFOMANCE = dr.GetString(dr.GetOrdinal("SEC_PERFOMANCE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("SEC_COLOR")))
                        obj.SEC_COLOR = dr.GetString(dr.GetOrdinal("SEC_COLOR")).ToUpper();
                    obj.SEC_TICKETS = dr.GetDecimal(dr.GetOrdinal("SEC_TICKETS"));
                    obj.SEC_VALUE = dr.GetDecimal(dr.GetOrdinal("SEC_VALUE"));
                    obj.SEC_GROSS = dr.GetDecimal(dr.GetOrdinal("SEC_GROSS"));
                    obj.SEC_TAXES = dr.GetDecimal(dr.GetOrdinal("SEC_TAXES"));
                    obj.SEC_NET = dr.GetDecimal(dr.GetOrdinal("SEC_NET"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                    {
                        obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                }
            }
            return obj;
        }

        public List<BELicenciaLocalidad> ListarLicenciaLocalidad(string owner, decimal idLic)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_LOCALIDAD_X_LICENCIA");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

            List<BELicenciaLocalidad> lst = new List<BELicenciaLocalidad>();
            BELicenciaLocalidad obj = null;

            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    obj = new BELicenciaLocalidad();
                    //     obj.LIC_SEC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_SEC_ID"));
                    obj.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
                    //     obj.CAP_ID = dr.GetString(dr.GetOrdinal("CAP_ID"));
                    obj.SEC_ID = dr.GetDecimal(dr.GetOrdinal("SEC_ID"));
                    //     obj.SEC_PERFOMANCE = dr.GetString(dr.GetOrdinal("SEC_PERFOMANCE"));
                    obj.SEC_COLOR = dr.GetString(dr.GetOrdinal("SEC_COLOR"));
                    obj.SEC_TICKETS = dr.GetDecimal(dr.GetOrdinal("SEC_TICKETS"));
                    obj.SEC_VALUE = dr.GetDecimal(dr.GetOrdinal("SEC_VALUE"));
                    obj.SEC_GROSS = dr.GetDecimal(dr.GetOrdinal("SEC_GROSS"));
                    obj.SEC_TAXES = dr.GetDecimal(dr.GetOrdinal("SEC_TAXES"));
                    obj.SEC_NET = dr.GetDecimal(dr.GetOrdinal("SEC_NET"));

                    if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                    {
                        obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                    }
                    lst.Add(obj);
                }
            }
            return lst;
        }
        public int Insertar(string owner, decimal CodigoLicencia, string CodigoTipoAforo, decimal CodigoTipoLocalidad, string Funcion, string Color, decimal Ticket, decimal PrecVenta, decimal ImporteBruto, decimal Impuesto, decimal ImporteNeto, string UsuarioCrea)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASI_LICENCIA_LOCALIDAD");

            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, CodigoLicencia);
            db.AddInParameter(cm, "@CAP_ID", DbType.String, CodigoTipoAforo);
            db.AddInParameter(cm, "@SEC_ID", DbType.Decimal, CodigoTipoLocalidad);
            db.AddInParameter(cm, "@SEC_PERFOMANCE", DbType.String, Funcion);
            db.AddInParameter(cm, "@SEC_COLOR", DbType.String, Color);
            db.AddInParameter(cm, "@SEC_TICKETS", DbType.Decimal, Ticket);
            db.AddInParameter(cm, "@SEC_VALUE", DbType.Decimal, PrecVenta);
            db.AddInParameter(cm, "@SEC_GROSS", DbType.Decimal, ImporteBruto);
            db.AddInParameter(cm, "@SEC_TAXES", DbType.Decimal, Impuesto);
            db.AddInParameter(cm, "@SEC_NET", DbType.Decimal, ImporteNeto);
            db.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, UsuarioCrea);

            int r = db.ExecuteNonQuery(cm);
            return r;
        }

        public int Actualizar(string owner, decimal idLicenciaLocalidad, string CodigoTipoAforo, decimal CodigoTipoLocalidad, string Funcion, string Color, decimal Ticket, decimal PrecVenta, decimal ImporteBruto, decimal Impuesto, decimal ImporteNeto, string UsuarioModifica)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASU_LICENCIA_LOCALIDAD");

            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@LIC_SEC_ID", DbType.Decimal, idLicenciaLocalidad);
            db.AddInParameter(cm, "@CAP_ID", DbType.String, CodigoTipoAforo);
            db.AddInParameter(cm, "@SEC_ID", DbType.Decimal, CodigoTipoLocalidad);
            db.AddInParameter(cm, "@SEC_PERFOMANCE", DbType.String, Funcion);
            db.AddInParameter(cm, "@SEC_COLOR", DbType.String, Color);
            db.AddInParameter(cm, "@SEC_TICKETS", DbType.Decimal, Ticket);
            db.AddInParameter(cm, "@SEC_VALUE", DbType.Decimal, PrecVenta);
            db.AddInParameter(cm, "@SEC_GROSS", DbType.Decimal, ImporteBruto);
            db.AddInParameter(cm, "@SEC_TAXES", DbType.Decimal, Impuesto);
            db.AddInParameter(cm, "@SEC_NET", DbType.Decimal, ImporteNeto);
            db.AddInParameter(cm, "@LOG_USER_UPDATE", DbType.String, UsuarioModifica);

            int r = db.ExecuteNonQuery(cm);
            return r;
        }

        public int Activar(string owner, decimal idLicenciaLocalidad)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASU_ACTIVAR_LICENCIA_LOCALIDAD");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@LIC_SEC_ID", DbType.String, idLicenciaLocalidad);

            int r = db.ExecuteNonQuery(cm);
            return r;
        }
        public int Eliminar(string owner, decimal idLicenciaLocalidad)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASU_ELIMINAR_LICENCIA_LOCALIDAD");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@SEC_ID", DbType.String, idLicenciaLocalidad);

            int r = db.ExecuteNonQuery(cm);
            return r;
        }

        public List<BELicenciaLocalidad> ListarLocalidad(string owner, decimal idLic)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_LOCALIDAD_LIC");
            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
            db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

            List<BELicenciaLocalidad> Lst = new List<BELicenciaLocalidad>();
            BELicenciaLocalidad localidad = null;
            using (IDataReader dr = db.ExecuteReader(cm))
            {
                while (dr.Read())
                {
                    localidad = new BELicenciaLocalidad();
                    localidad.SEC_ID = dr.GetDecimal(dr.GetOrdinal("SEC_ID"));
                    localidad.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));

                    if (!dr.IsDBNull(dr.GetOrdinal("SEC_DESC")))
                        localidad.SEC_DESC = dr.GetString(dr.GetOrdinal("SEC_DESC"));

                    localidad.SEC_VALUE = dr.GetDecimal(dr.GetOrdinal("SEC_VALUE"));
                    localidad.SEC_GROSS = dr.GetDecimal(dr.GetOrdinal("SEC_GROSS"));
                    localidad.SEC_TAXES = dr.GetDecimal(dr.GetOrdinal("SEC_TAXES"));
                    localidad.SEC_TICKETS = dr.GetDecimal(dr.GetOrdinal("SEC_TICKETS"));
                    localidad.SEC_NET = dr.GetDecimal(dr.GetOrdinal("SEC_NET"));

                    if (!dr.IsDBNull(dr.GetOrdinal("SEC_COLOR")))
                        localidad.SEC_COLOR = dr.GetString(dr.GetOrdinal("SEC_COLOR"));

                    localidad.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                    localidad.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        localidad.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                    if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        localidad.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                    Lst.Add(localidad);
                }
            }
            return Lst;
        }

        public int InsertarLocalidad(BELicenciaLocalidad localidad)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASI_LOCALIDAD_LIC");

            db.AddInParameter(cm, "@OWNER", DbType.String, localidad.OWNER);
            db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, localidad.LIC_ID);
            db.AddInParameter(cm, "@SEC_DESC", DbType.String, localidad.SEC_DESC);
            db.AddInParameter(cm, "@SEC_TICKETS", DbType.Decimal, localidad.SEC_TICKETS);
            db.AddInParameter(cm, "@SEC_VALUE", DbType.Decimal, localidad.SEC_VALUE);
            db.AddInParameter(cm, "@SEC_GROSS", DbType.Decimal, localidad.SEC_GROSS);
            db.AddInParameter(cm, "@SEC_TAXES", DbType.Decimal, localidad.SEC_TAXES);
            db.AddInParameter(cm, "@SEC_NET", DbType.Decimal, localidad.SEC_NET);
            db.AddInParameter(cm, "@SEC_COLOR", DbType.String, localidad.SEC_COLOR);
            db.AddInParameter(cm, "@LOG_USER_CREAT", DbType.String, localidad.LOG_USER_CREAT);

            int r = db.ExecuteNonQuery(cm);
            return r;
        }

        public int ActualizarLocalidad(BELicenciaLocalidad localidad)
        {
            DbCommand cm = db.GetStoredProcCommand("SGRDASU_LOCALIDAD_LIC");

            db.AddInParameter(cm, "@OWNER", DbType.String, localidad.OWNER);
            db.AddInParameter(cm, "@SEC_ID", DbType.Decimal, localidad.SEC_ID);
            db.AddInParameter(cm, "@SEC_DESC", DbType.String, localidad.SEC_DESC);
            db.AddInParameter(cm, "@SEC_TICKETS", DbType.Decimal, localidad.SEC_TICKETS);
            db.AddInParameter(cm, "@SEC_VALUE", DbType.Decimal, localidad.SEC_VALUE);
            db.AddInParameter(cm, "@SEC_GROSS", DbType.Decimal, localidad.SEC_GROSS);
            db.AddInParameter(cm, "@SEC_TAXES", DbType.Decimal, localidad.SEC_TAXES);
            db.AddInParameter(cm, "@SEC_NET", DbType.Decimal, localidad.SEC_NET);
            db.AddInParameter(cm, "@SEC_COLOR", DbType.String, localidad.SEC_COLOR);
            db.AddInParameter(cm, "@LOG_USER_UPDATE", DbType.String, localidad.LOG_USER_UPDATE);

            int r = db.ExecuteNonQuery(cm);
            return r;
        }

        public BELicenciaLocalidadConteo listarLicenciaConteo(string owner, decimal LICID, string tipo)
        {
            BELicenciaLocalidadConteo entidad = null;

            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASS_OBTENER_PYL_LIQUIDACION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@LICID", DbType.Decimal, LICID);
            db.AddInParameter(oDbCommand, "@CAP_IPRELQ", DbType.String, tipo);

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                while (dr.Read())
                {
                    entidad = new BELicenciaLocalidadConteo();

                    if (!dr.IsDBNull(dr.GetOrdinal("CAP_LIC_ID")))
                        entidad.CAP_LIC_ID = dr.GetDecimal(dr.GetOrdinal("CAP_LIC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CAP_DESC")))
                        entidad.CAP_DESC = dr.GetString(dr.GetOrdinal("CAP_DESC"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TOTAL_IPRELQ")))
                        entidad.TOTAL_IPRELQ = dr.GetDecimal(dr.GetOrdinal("TOTAL_IPRELQ"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CAP_IPRELQ")))
                        entidad.CAP_IPRELQ = dr.GetString(dr.GetOrdinal("CAP_IPRELQ"));
                    
                }


            }
            return entidad;

        }


        //public List<BELicenciaLocalidadConteo> ListarMatrizLocalidad(string owner, decimal idLic)
        //{
        //    DbCommand cm = db.GetStoredProcCommand("SGRDASS_LISTAR_MATRIZ_LOCALIDADES");
        //    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
        //    db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

        //    List<BELicenciaLocalidadConteo> Lst = new List<BELicenciaLocalidadConteo>();
        //    BELicenciaLocalidadConteo conteo = null;
        //    using (IDataReader dr = db.ExecuteReader(cm))
        //    {
        //        while (dr.Read())
        //        {
        //            conteo = new BELicenciaLocalidadConteo();
        //            conteo.OWNER = dr.GetString(dr.GetOrdinal("OWNER"));
        //            conteo.Nro = Convert.ToDecimal(dr.GetDecimal(dr.GetOrdinal("Nro")));
        //            conteo.LIC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_ID"));
        //            conteo.CAP_LIC_ID = dr.GetDecimal(dr.GetOrdinal("CAP_LIC_ID"));
        //            conteo.CAP_ID = dr.GetString(dr.GetOrdinal("CAP_ID"));
        //            conteo.LIC_SEC_ID = dr.GetDecimal(dr.GetOrdinal("LIC_SEC_ID"));
        //            conteo.SEC_ID = dr.GetDecimal(dr.GetOrdinal("SEC_ID"));

        //            if (!dr.IsDBNull(dr.GetOrdinal("CAP_DESC")))
        //                conteo.CAP_DESC = dr.GetString(dr.GetOrdinal("CAP_DESC")).ToUpper();
        //            if (!dr.IsDBNull(dr.GetOrdinal("SEC_DESC")))
        //                conteo.SEC_DESC = dr.GetString(dr.GetOrdinal("SEC_DESC")).ToUpper();

        //            conteo.LOC_SEC_GROSS = dr.GetDecimal(dr.GetOrdinal("LOC_SEC_GROSS"));
        //            conteo.LOC_SEC_TAXES = dr.GetDecimal(dr.GetOrdinal("LOC_SEC_TAXES"));
        //            conteo.LOC_SEC_NET = dr.GetDecimal(dr.GetOrdinal("LOC_SEC_NET"));
        //            if (!dr.IsDBNull(dr.GetOrdinal("SEC_TICKETS")))
        //                conteo.SEC_TICKETS = dr.GetDecimal(dr.GetOrdinal("SEC_TICKETS"));
        //            if (!dr.IsDBNull(dr.GetOrdinal("SEC_VALUE")))
        //                conteo.SEC_VALUE = dr.GetDecimal(dr.GetOrdinal("SEC_VALUE"));
        //            if (!dr.IsDBNull(dr.GetOrdinal("SEC_GROSS")))
        //                conteo.SEC_GROSS = dr.GetDecimal(dr.GetOrdinal("SEC_GROSS"));
        //            if (!dr.IsDBNull(dr.GetOrdinal("SEC_TAXES")))
        //                conteo.SEC_TAXES = dr.GetDecimal(dr.GetOrdinal("SEC_TAXES"));
        //            if (!dr.IsDBNull(dr.GetOrdinal("SEC_NET")))
        //                conteo.SEC_NET = dr.GetDecimal(dr.GetOrdinal("SEC_NET"));
        //            Lst.Add(conteo);
        //        }
        //    }
        //    return Lst;
        //}

        //public bool InsertarMatrizLocalidadesXML(string xml, string owner)
        //{
        //    bool exito = false;
        //    try
        //    {
        //        using (DbCommand cm = db.GetStoredProcCommand("SGRDASI_MATRIZ_LOCALIDADES"))
        //        {
        //            db.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
        //            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
        //            exito = db.ExecuteNonQuery(cm) > 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return exito;
        //}

        //public bool ActualizarMatrizLocalidadesXML(string xml, string owner)
        //{
        //    bool exito = false;
        //    try
        //    {
        //        using (DbCommand cm = db.GetStoredProcCommand("SGRDASU_MATRIZ_LOCALIDADES"))
        //        {
        //            db.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
        //            db.AddInParameter(cm, "@OWNER", DbType.String, owner);
        //            exito = db.ExecuteNonQuery(cm) > 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return exito;
        //}

        //public int ObtenerCantMatLocActivas(string owner, decimal idLic)
        //{
        //    DbCommand cm = db.GetStoredProcCommand("SGRDASS_OBTENER_CANT_LOC_MATRIZ");
        //    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
        //    db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);

        //    int r = Convert.ToInt32(db.ExecuteScalar(cm));
        //    return r;
        //}

        //public int EliminarMatrizLocalidades(string owner, decimal idLic)
        //{
        //    DbCommand cm = db.GetStoredProcCommand("SGRDASD_MATRIZ_LOCALIDADES");
        //    db.AddInParameter(cm, "@OWNER", DbType.String, owner);
        //    db.AddInParameter(cm, "@LIC_ID", DbType.Decimal, idLic);
        //    int r = db.ExecuteNonQuery(cm);
        //    return r;
        //}

    }
}
