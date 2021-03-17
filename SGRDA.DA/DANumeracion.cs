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
    public class DANumeracion
    {

        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<BENumerador> NumeracionXOficina(decimal codigoOff, string owner)
        {
            List<BENumerador> numeraciones = null;
            try
            {
                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OFF_NUM"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, owner);
                    oDataBase.AddInParameter(cm, "@OFF_ID", DbType.Decimal, codigoOff);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {

                        BENumerador ObjObs = null;
                        numeraciones = new List<BENumerador>();
                        while (dr.Read())
                        {
                            ObjObs = new BENumerador();

                            ObjObs.NMR_ID = dr.GetDecimal(dr.GetOrdinal("NMR_ID"));
                            ObjObs.NMR_NAME = dr.GetString(dr.GetOrdinal("NMR_NAME"));
                            ObjObs.NMR_SERIAL = dr.GetString(dr.GetOrdinal("NMR_SERIAL"));
                            ObjObs.NMR_TYPE = dr.GetString(dr.GetOrdinal("NMR_TYPE"));


                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                ObjObs.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }

                            numeraciones.Add(ObjObs);

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return numeraciones;
        }


        public BETipoNumerador ObtenerTipoNumeracion(string Owner, string NmrType)
        {
            try
            {
                BETipoNumerador Obj = null;
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");

                using (DbCommand cm = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_TIPO_NUM"))
                {
                    oDataBase.AddInParameter(cm, "@OWNER", DbType.String, Owner);
                    oDataBase.AddInParameter(cm, "@NMR_TYPE", DbType.String, NmrType);

                    using (IDataReader dr = oDataBase.ExecuteReader(cm))
                    {
                        while (dr.Read())
                        {
                            Obj = new BETipoNumerador();
                            Obj.NMR_TYPE = Convert.ToString(dr["NMR_TYPE"]);
                            Obj.NMR_TDESC = Convert.ToString(dr["NMR_TDESC"]);

                            if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                            {
                                Obj.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                            }
                        }
                    }
                }

                return Obj;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
