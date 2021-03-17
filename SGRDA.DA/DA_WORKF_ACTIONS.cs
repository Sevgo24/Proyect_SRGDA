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
using SGRDA.Entities.WorkFlow;
using System.Data.Common;

namespace SGRDA.DA.WorkFlow
{
    public class DA_WORKF_ACTIONS
    {
        private Database db = DatabaseFactory.CreateDatabase("conexion");

        public WORKF_ACTIONS ObtenerAction(string owner, decimal wrkfaid)
        {
            WORKF_ACTIONS item = null;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_OBTENER_ACTIONS"))
            {
                db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, wrkfaid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {
                        item = new WORKF_ACTIONS();
                        item.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                        item.WRKF_ANAME = dr.GetString(dr.GetOrdinal("WRKF_ANAME"));
                        item.WRKF_ALABEL = dr.GetString(dr.GetOrdinal("WRKF_ALABEL"));
                        item.WRKF_ATID = dr.GetDecimal(dr.GetOrdinal("WRKF_ATID"));
                        item.WRKF_AAPLIC = dr.GetString(dr.GetOrdinal("WRKF_AAPLIC"));
                        item.WRKF_ADESC = dr.GetString(dr.GetOrdinal("WRKF_ADESC"));
                        item.WRKF_DTID = dr.GetDecimal(dr.GetOrdinal("WRKF_DTID"));
                        item.PROC_ID = dr.GetDecimal(dr.GetOrdinal("PROC_ID"));
                        item.LOG_DATE_CREAT = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_DATE_UPDATE")))
                        {
                            item.LOG_DATE_UPDATE = dr.GetDateTime(dr.GetOrdinal("LOG_DATE_UPDATE"));
                        }
                        item.LOG_USER_CREAT = dr.GetString(dr.GetOrdinal("LOG_USER_CREAT"));
                        if (!dr.IsDBNull(dr.GetOrdinal("LOG_USER_UPDATE")))
                        {
                            item.LOG_USER_UPDATE = dr.GetString(dr.GetOrdinal("LOG_USER_UPDATE"));
                        }
                        if (!dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        {
                            item.ENDS = dr.GetDateTime(dr.GetOrdinal("ENDS"));
                        }
                    }
                }
            }
            return item;
        }

        public bool CumplePreRequisito(string owner, decimal wrkf_ref1, decimal wrkf_amid)
        {
            bool cumple = false;
            using (DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_CUMPLE_PREREQUISITO"))
            {
                // db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
                db.AddInParameter(oDbCommand, "@WRKF_REF1", DbType.Decimal, wrkf_ref1);
                db.AddInParameter(oDbCommand, "@WRKF_AMID", DbType.Decimal, wrkf_amid);

                using (IDataReader dr = db.ExecuteReader(oDbCommand))
                {
                    if (dr.Read())
                    {

                        if (!(dr.IsDBNull(dr.GetOrdinal("CUMPLE"))))
                        {
                            cumple = dr.GetBoolean(dr.GetOrdinal("CUMPLE"));
                        }


                    }
                }
            }
            return cumple;
        }

        public int CambiarEstado(decimal codModulo, decimal codRef, string owner, decimal codFlujo, decimal codEstadActual, decimal codAMID)
        {

            Database oDataBase = new DatabaseProviderFactory().Create("conexion");

            DbCommand oDbComand = oDataBase.GetStoredProcCommand("SWFSU_CAMBIA_ESTADO");
            oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbComand, "@WRKF_REF1", DbType.Decimal, codRef);
            oDataBase.AddInParameter(oDbComand, "@WRKF_ID", DbType.Decimal, codFlujo);
            oDataBase.AddInParameter(oDbComand, "@WRKF_SID", DbType.Decimal, codEstadActual);
            oDataBase.AddInParameter(oDbComand, "@WRKF_AMID", DbType.Decimal, codAMID);
            oDataBase.AddInParameter(oDbComand, "@PROC_MOD", DbType.Decimal, codModulo);

            oDataBase.AddOutParameter(oDbComand, "@RETORNO", DbType.String, 3);

            int r = oDataBase.ExecuteNonQuery(oDbComand);
            string results = Convert.ToString(oDataBase.GetParameterValue(oDbComand, "@RETORNO"));
            return Convert.ToInt32(results);


        }

        public List<WORKF_ACTIONS> Listar(string owner, string nombre, string etiqueta,
                                        decimal idTipoAccion, decimal idTipoDato, decimal idProceso, string idAuto,
                                       int estado, int pagina, int cantRegxPag)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSS_LISTAR_ACCIONES");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            db.AddInParameter(oDbCommand, "@WRKF_ANAME", DbType.String, nombre);
            db.AddInParameter(oDbCommand, "@WRKF_ALABEL", DbType.String, etiqueta);
            db.AddInParameter(oDbCommand, "@WRKF_ATID", DbType.Decimal, idTipoAccion);
            db.AddInParameter(oDbCommand, "@WRKF_DTID", DbType.Decimal, idTipoDato);
            db.AddInParameter(oDbCommand, "@PROC_ID", DbType.Decimal, idProceso);
            db.AddInParameter(oDbCommand, "@WRKF_AAPLIC", DbType.String, idAuto);
            db.AddInParameter(oDbCommand, "@ESTADO", DbType.Int32, estado);

            db.AddInParameter(oDbCommand, "@PageIndex", DbType.Int32, pagina);
            db.AddInParameter(oDbCommand, "@PageSize", DbType.Int32, cantRegxPag);
            db.AddOutParameter(oDbCommand, "@RecordCount", DbType.Int32, 0);
            db.ExecuteNonQuery(oDbCommand);

            string results = Convert.ToString(db.GetParameterValue(oDbCommand, "@RecordCount"));
            var lista = new List<WORKF_ACTIONS>();

            using (IDataReader dr = db.ExecuteReader(oDbCommand))
            {
                WORKF_ACTIONS accion = null;
                while (dr.Read())
                {
                    accion = new WORKF_ACTIONS();
                    accion.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                    accion.WRKF_ANAME = dr.GetString(dr.GetOrdinal("WRKF_ANAME")).ToUpper();
                    accion.WRKF_ALABEL = dr.GetString(dr.GetOrdinal("WRKF_ALABEL")).ToUpper();

                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ATID")))
                        accion.WRKF_ATID = dr.GetDecimal(dr.GetOrdinal("WRKF_ATID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ATNAME")))
                        accion.WRKF_ATNAME = dr.GetString(dr.GetOrdinal("WRKF_ATNAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_DTID")))
                        accion.WRKF_DTID = dr.GetDecimal(dr.GetOrdinal("WRKF_DTID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("WRKF_DTNAME")))
                        accion.WRKF_DTNAME = dr.GetString(dr.GetOrdinal("WRKF_DTNAME"));

                    if (!dr.IsDBNull(dr.GetOrdinal("PROC_ID")))
                        accion.PROC_ID = dr.GetDecimal(dr.GetOrdinal("PROC_ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("PROC_NAME")))
                        accion.PROC_NAME = dr.GetString(dr.GetOrdinal("PROC_NAME"));

                    if (dr.IsDBNull(dr.GetOrdinal("ENDS")))
                        accion.ESTADO = "ACTIVO";
                    else
                        accion.ESTADO = "INACTIVO";

                    accion.TIPO_ACCION = dr.GetString(dr.GetOrdinal("WRKF_AAPLIC")).ToUpper() == "A" ? "AUTOMATICO" : "MANUAL";
                    accion.TotalVirtual = Convert.ToInt32(results);
                    lista.Add(accion);
                }
            }
            return lista;
        }


        public WORKF_ACTIONS Obtener(string owner, decimal? id)
        {
            WORKF_ACTIONS accion = null;
            using (DbCommand cm = db.GetStoredProcCommand("SWFSS_OBTENER_ACCION"))
            {
                db.AddInParameter(cm, "@OWNER", DbType.String, owner);
                db.AddInParameter(cm, "@WRKF_AID", DbType.String, id);

                using (IDataReader dr = db.ExecuteReader(cm))
                {
                    if (dr.Read())
                    {
                        accion = new WORKF_ACTIONS();
                        accion.WRKF_AID = dr.GetDecimal(dr.GetOrdinal("WRKF_AID"));
                        accion.WRKF_ANAME = dr.GetString(dr.GetOrdinal("WRKF_ANAME"));
                        accion.WRKF_ALABEL = dr.GetString(dr.GetOrdinal("WRKF_ALABEL"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ATID")))                           //TIPO ACCION
                            accion.WRKF_ATID = dr.GetDecimal(dr.GetOrdinal("WRKF_ATID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_AAPLIC")))                         //CHK ACCION
                            accion.WRKF_AAPLIC = dr.GetString(dr.GetOrdinal("WRKF_AAPLIC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_ADESC")))                          //DESCRIPCION
                            accion.WRKF_ADESC = dr.GetString(dr.GetOrdinal("WRKF_ADESC"));
                        if (!dr.IsDBNull(dr.GetOrdinal("WRKF_DTID")))                           //TIPO DATO
                            accion.WRKF_DTID = dr.GetDecimal(dr.GetOrdinal("WRKF_DTID"));
                        if (!dr.IsDBNull(dr.GetOrdinal("PROC_ID")))                             //TIPO PROCESO
                            accion.PROC_ID = dr.GetDecimal(dr.GetOrdinal("PROC_ID"));
                    }
                }
            }

            return accion;
        }

        public int Insertar(WORKF_ACTIONS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSI_INSERTAR_ACCION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddOutParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, Convert.ToInt32(en.WRKF_AID));
            db.AddInParameter(oDbCommand, "@WRKF_ANAME", DbType.String, en.WRKF_ANAME.ToUpper());
            db.AddInParameter(oDbCommand, "@WRKF_ALABEL", DbType.String, en.WRKF_ALABEL.ToUpper());
            db.AddInParameter(oDbCommand, "@WRKF_ATID", DbType.Decimal, en.WRKF_ATID);
            db.AddInParameter(oDbCommand, "@WRKF_AAPLIC", DbType.String, en.WRKF_AAPLIC.ToUpper());
            db.AddInParameter(oDbCommand, "@WRKF_ADESC", DbType.String, en.WRKF_ADESC != null ? en.WRKF_ADESC.ToString().ToUpper() : en.WRKF_ADESC);
            db.AddInParameter(oDbCommand, "@WRKF_DTID", DbType.Decimal, en.WRKF_DTID);
            db.AddInParameter(oDbCommand, "@PROC_ID", DbType.Decimal, en.PROC_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_CREAT", DbType.String, en.LOG_USER_CREAT.ToUpper());
            int n = db.ExecuteNonQuery(oDbCommand);
            int id = Convert.ToInt32(db.GetParameterValue(oDbCommand, "@WRKF_AID"));
            return id;
        }

        public int Actualizar(WORKF_ACTIONS en)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SGRDASU_ACCION");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_AID", DbType.Decimal, en.WRKF_AID);
            db.AddInParameter(oDbCommand, "@WRKF_ANAME", DbType.String, en.WRKF_ANAME.ToUpper());
            db.AddInParameter(oDbCommand, "@WRKF_ALABEL", DbType.String, en.WRKF_ALABEL.ToUpper());
            db.AddInParameter(oDbCommand, "@WRKF_ATID", DbType.Decimal, en.WRKF_ATID);
            db.AddInParameter(oDbCommand, "@WRKF_AAPLIC", DbType.String, en.WRKF_AAPLIC.ToUpper());
            db.AddInParameter(oDbCommand, "@WRKF_ADESC", DbType.String, en.WRKF_ADESC != null ? en.WRKF_ADESC.ToString().ToUpper() : en.WRKF_ADESC);
            db.AddInParameter(oDbCommand, "@WRKF_DTID", DbType.Decimal, en.WRKF_DTID);
            db.AddInParameter(oDbCommand, "@PROC_ID", DbType.Decimal, en.PROC_ID);
            db.AddInParameter(oDbCommand, "@LOG_USER_UPDATE", DbType.String, en.LOG_USER_UPDATE.ToUpper());

            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public int RollBackStateLic(decimal idTrace)
        {
            DbCommand oDbCommand = db.GetStoredProcCommand("SWFSU_ROLLBACK_ESTADO_LIC");
            db.AddInParameter(oDbCommand, "@OWNER", DbType.String, GlobalVars.Global.OWNER);
            db.AddInParameter(oDbCommand, "@WRKF_TID", DbType.Decimal, idTrace);


            int n = db.ExecuteNonQuery(oDbCommand);
            return n;
        }

        public string ObtenerProcMod(string owner, decimal wrkfid)
        {
            Database oDataBase = new DatabaseProviderFactory().Create("conexion");
            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("SGRDAS_OBTENER_PROC_MOD");
            oDataBase.AddInParameter(oDbCommand, "@OWNER", DbType.String, owner);
            oDataBase.AddInParameter(oDbCommand, "@WRKF_ID", DbType.Decimal, wrkfid);
            string r = Convert.ToString(oDataBase.ExecuteScalar(oDbCommand));
            return r;
        }
    }
}
