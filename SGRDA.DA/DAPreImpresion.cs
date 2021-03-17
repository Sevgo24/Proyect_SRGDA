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
    public class DAPreImpresion
    {

        public List<BEPreImpresion> Pendientes( string localhost)
        {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_IMPRESION_PENDIENTE");
                //oDataBase.AddInParameter(oDbCommand, "@USER_ID", DbType.String, idUsuario);
                oDataBase.AddInParameter(oDbCommand, "@HOSTNAME", DbType.String, localhost);

                var lista = new List<BEPreImpresion>();
                var preImp = new BEPreImpresion();
                using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
                {
                    while (dr.Read())
                    {
                        preImp = new BEPreImpresion();
                        preImp.CodigoImpresion = dr.GetInt32(dr.GetOrdinal("ID"));
                        preImp.CodigoDocumento = dr.GetDecimal(dr.GetOrdinal("ID_DOCUMENTO"));
                        preImp.CodigLocal = dr.GetDecimal(dr.GetOrdinal("ID_LOCAL"));
                        preImp.FechaSel = dr.GetDateTime(dr.GetOrdinal("FECHA_SEL"));
                        preImp.Estado = dr.GetString(dr.GetOrdinal("ESTADO"));
                        if (!dr.IsDBNull(dr.GetOrdinal("HOSTNAME")))
                            preImp.Host = dr.GetString(dr.GetOrdinal("HOSTNAME"));

                        if (!dr.IsDBNull(dr.GetOrdinal("FECHA_IMP")))
                            preImp.FechaImp = dr.GetDateTime(dr.GetOrdinal("FECHA_IMP"));
                        
                        lista.Add(preImp);
                    }
                }
                return lista;

            
        }


        public BEPreImpresion ObtenerPreImpresion(decimal idImpresion)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDASS_OBTIENE_PREIMPRESION");
            oDataBase.AddInParameter(oDbCommand, "@ID", DbType.Decimal, idImpresion);


            var preImp = new BEPreImpresion();
            using (IDataReader dr = oDataBase.ExecuteReader(oDbCommand))
            {
                if (dr.Read())
                {
                    preImp = new BEPreImpresion();
                    preImp.CodigoImpresion = dr.GetInt32(dr.GetOrdinal("ID"));
                    preImp.CodigoDocumento = dr.GetDecimal(dr.GetOrdinal("ID_DOCUMENTO"));
                    preImp.CodigLocal = dr.GetDecimal(dr.GetOrdinal("ID_LOCAL"));
                    preImp.FechaSel = dr.GetDateTime(dr.GetOrdinal("FECHA_SEL"));
                    preImp.Estado = dr.GetString(dr.GetOrdinal("ESTADO"));
                    if (!dr.IsDBNull(dr.GetOrdinal("HOSTNAME")))
                        preImp.Host = dr.GetString(dr.GetOrdinal("HOSTNAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("FECHA_IMP")))
                        preImp.FechaImp = dr.GetDateTime(dr.GetOrdinal("FECHA_IMP"));


                }
            }
            return preImp;


        }



     
        //public int Eliminar(BEAgente agente)
        //{
        //        Database oDatabase = new DatabaseProviderFactory().Create("conexion");
        //        DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_AGENTE_ESTADO");
        //        oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, agente.OWNER);
        //        oDatabase.AddInParameter(oDbCommand, "@LEVEL_ID", DbType.Int32, agente.LEVEL_ID);
        //        oDatabase.AddInParameter(oDbCommand, "@LOG_USER_UPDAT", DbType.String, agente.LOG_USER_UPDAT);

        //        int r = oDatabase.ExecuteNonQuery(oDbCommand);
        //        return r;
           
        //}

       

        //public int Insertar(BEAgente agente)
        //{
        //        Database oDatabase = new DatabaseProviderFactory().Create("conexion");
        //        DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASI_AGENTE");
        //        oDatabase.AddInParameter(oDbCommand, "@OWNER", DbType.String, agente.OWNER);
        //        oDatabase.AddInParameter(oDbCommand, "@DESCRIPTION", DbType.String, agente.DESCRIPTION.ToUpper());
        //        oDatabase.AddInParameter(oDbCommand, "@LEVEL_DEP", DbType.Decimal, agente.LEVEL_DEP);
        //        oDatabase.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, agente.LOG_USER_CREAT.ToUpper());

        //        int n = oDatabase.ExecuteNonQuery(oDbCommand);

        //        return n;          

        //}



        public int ActualizarEstado(decimal codigoImpresion,string host,string estado)
        {
            Database oDatabase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("SGRDASU_IMPRESION_ESTADO");
            oDatabase.AddInParameter(oDbCommand, "@ID", DbType.String, codigoImpresion);
            oDatabase.AddInParameter(oDbCommand, "@HOST", DbType.String, host);
            oDatabase.AddInParameter(oDbCommand, "@ESTADO", DbType.String, estado);
            
            


            int r = oDatabase.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public int RegistrarPreImpresion(decimal idFactura, decimal idUsuario, decimal idLocal, string estado, string hostname)
        {
            Database db = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASI_PRE_IMPRESION");
            db.AddInParameter(oDbCommand, "@ID_DOCUMENTO", DbType.String, idFactura);
            db.AddInParameter(oDbCommand, "@ID_USUARIO", DbType.Decimal, idUsuario);
            db.AddInParameter(oDbCommand, "@ID_LOCAL", DbType.Decimal, idLocal);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.String, estado);
            db.AddInParameter(oDbCommand, "@HOSTNAME", DbType.String, hostname);
            int r = db.ExecuteNonQuery(oDbCommand);
            return r;
        }

        public bool RegistrarPreImpresionXML(string xml)
        {
            bool exito = false;
            Database db = new DatabaseProviderFactory().Create("conexion");
            try
            {
                using (DbCommand cm = db.GetStoredProcCommand("SGRDASI_PRE_IMPRESION_XML"))
                {
                    db.AddInParameter(cm, "xmlLst", DbType.Xml, xml);
                    exito = db.ExecuteNonQuery(cm) > 0;
                }
                exito = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return exito;
        }
      

    }
}


