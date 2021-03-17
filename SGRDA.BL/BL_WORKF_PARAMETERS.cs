using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.DA.WorkFlow;
using SGRDA.Entities;
using SGRDA.Entities.WorkFlow;
using System.Transactions;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_PARAMETERS
    {
        public List<WORKF_PARAMETERS> ListarParameterXActions(string owner, decimal wrkf_aid)
        {
            return new DA_WORKF_PARAMETERS().ListarParameterXActions(owner, wrkf_aid);
        }
        public WORKF_PARAMETERS ObtenerParameterXActions(string owner, decimal wrkf_aid)
        {
            return new DA_WORKF_PARAMETERS().ObtenerParameterXActions(owner, wrkf_aid);
        }

        public List<WORKF_PARAMETERS> ListarXObjects(string owner, decimal oid)
        {
            return new DA_WORKF_PARAMETERS().ListarXObjetcs(owner, oid);
        }

        public WORKF_PARAMETERS ObtenerParametroTransicion(string owner, decimal mid, decimal wrkfdtid)
        {
            return new DA_WORKF_PARAMETERS().ObtenerParametroTransicion(owner, mid, wrkfdtid);
        }

        public int InsertarParametroCorreo(WORKF_PARAMETERS en)
        {
            return new DA_WORKF_PARAMETERS().InsertarParametro(en);
        }

        public int ActualizarParametro(WORKF_PARAMETERS en)
        {
            return new DA_WORKF_PARAMETERS().ActualizarParametro(en);
        }

        public WORKF_PARAMETERS ObtenerParameterXId(string owner, decimal wrkf_pid)
        {
            return new DA_WORKF_PARAMETERS().ObtenerParameterXId(owner, wrkf_pid);
        }

        public int EliminarParametro(WORKF_PARAMETERS en)
        {
            return new DA_WORKF_PARAMETERS().EliminarParametro(en);
        }

        public int InsertarCorreo(WORKF_PARAMETERS en)
        {
            int result = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                if (en.Parametros != null)
                {
                    foreach (var item in en.Parametros)
                    {
                        WORKF_PARAMETERS ent = new BL_WORKF_PARAMETERS().ObtenerParameterXId(item.OWNER, item.WRKF_PID);

                        if (ent == null)
                        {
                            result = new DA_WORKF_PARAMETERS().InsertarParametro(new WORKF_PARAMETERS
                            {
                                OWNER = GlobalVars.Global.OWNER,
                                WRKF_PID = item.WRKF_PID,
                                WRKF_PNAME = item.WRKF_PNAME,
                                WRKF_PVALUE = item.WRKF_PVALUE,
                                WRKF_PORDER = item.WRKF_PORDER,
                                WRKF_AMID = item.WRKF_AMID,
                                WRKF_DTID = item.WRKF_DTID,
                                WRKF_PTID = item.WRKF_PTID,
                                WRKF_OID = item.WRKF_OID,
                                PROC_MOD = item.PROC_MOD,
                                LOG_USER_CREAT = item.LOG_USER_CREAT
                            });
                        }
                        else if (item.WRKF_PVALUE != ent.WRKF_PVALUE)
                        {
                            result = new DA_WORKF_PARAMETERS().ActualizarParametro(new WORKF_PARAMETERS
                            {
                                OWNER = GlobalVars.Global.OWNER,
                                WRKF_PID = item.WRKF_PID,
                                WRKF_PVALUE = item.WRKF_PVALUE,
                                LOG_USER_UPDATE = item.LOG_USER_UPDATE
                            });
                        }
                    }
                    transa.Complete();
                }
            }
            return result;
        }

        public int InsertarParametroTransicion(WORKF_PARAMETERS en)
        {
            int result = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                if (en.Parametros != null)
                {
                    foreach (var item in en.Parametros)
                    {
                        WORKF_PARAMETERS ent = new BL_WORKF_PARAMETERS().ObtenerParameterXId(item.OWNER, item.WRKF_PID);

                        if (ent == null)
                        {
                            result = new DA_WORKF_PARAMETERS().InsertarParametro(new WORKF_PARAMETERS
                            {
                                OWNER = GlobalVars.Global.OWNER,
                                WRKF_PID = item.WRKF_PID,
                                WRKF_PNAME = item.WRKF_PNAME,                                                                                                                                                                                                                                                                   
                                WRKF_PVALUE = item.WRKF_PVALUE,
                                WRKF_PORDER = item.WRKF_PORDER,
                                WRKF_AMID = item.WRKF_AMID,
                                WRKF_DTID = item.WRKF_DTID,
                                WRKF_PTID = item.WRKF_PTID,
                                WRKF_OID = item.WRKF_OID,
                                PROC_MOD = item.PROC_MOD,
                                LOG_USER_CREAT = item.LOG_USER_CREAT
                            });
                        }
                        else if (item.WRKF_PVALUE != ent.WRKF_PVALUE)
                        {
                            result = new DA_WORKF_PARAMETERS().ActualizarParametro(new WORKF_PARAMETERS
                            {
                                OWNER = GlobalVars.Global.OWNER,
                                WRKF_PID = item.WRKF_PID,
                                WRKF_PVALUE = item.WRKF_PVALUE,
                                LOG_USER_UPDATE = item.LOG_USER_UPDATE
                            });
                        }
                    }
                    transa.Complete();
                }
            }
            return result;
        }

        public int EliminarTransicionParametro(string owner, decimal idmapping, string user)
        {
            return new DA_WORKF_PARAMETERS().EliminarTransicionParametro(owner, idmapping, user);
        }

        public int ParametroXObjeto(string owner, decimal wrkfId, decimal wrkfsId, decimal objId)
        {
            return new DA_WORKF_PARAMETERS().ParametroXObjeto(owner, wrkfId, wrkfsId, objId);
        }

        public List<WORKF_PARAMETERS> ListarParametroTransicion(decimal idTipo, string referencia)
        {
            return new DA_WORKF_PARAMETERS().ListarParametroTransicion(idTipo, referencia);
        }


        /// <summary>
        /// se creó replica del original para corregir. Impacto no determinado al cambiar logica
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int InsertarParametroTransicionB(WORKF_PARAMETERS en)
        {
            int result = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                if (en.Parametros != null)
                {
                    foreach (var item in en.Parametros)
                    {
                        WORKF_PARAMETERS ent = new BL_WORKF_PARAMETERS().ObtenerParameterXId(item.OWNER, item.WRKF_PID);

                        if (ent == null)
                        {
                            result = new DA_WORKF_PARAMETERS().InsertarParametro(new WORKF_PARAMETERS
                            {
                                OWNER = GlobalVars.Global.OWNER,
                                WRKF_PID = item.WRKF_PID,
                                WRKF_PNAME = item.WRKF_PNAME,
                                WRKF_PVALUE = item.WRKF_PVALUE,
                                WRKF_PORDER = item.WRKF_PORDER,
                                WRKF_AMID = item.WRKF_AMID,
                                WRKF_DTID = item.WRKF_DTID,
                                WRKF_PTID = item.WRKF_PTID,
                                WRKF_OID = item.WRKF_OID,
                                PROC_MOD = item.PROC_MOD,
                                LOG_USER_CREAT = item.LOG_USER_CREAT
                            });
                        }
                        else  //if (item.WRKF_PVALUE == ent.WRKF_PVALUE)
                        {
                            result = new DA_WORKF_PARAMETERS().ActualizarParametroB(new WORKF_PARAMETERS
                            {
                                OWNER = GlobalVars.Global.OWNER,
                                WRKF_PID = item.WRKF_PID,
                                WRKF_PVALUE = item.WRKF_PVALUE,
                                LOG_USER_UPDATE = item.LOG_USER_UPDATE,
                                WRKF_PORDER=item.WRKF_PORDER
                            });
                        }
                    }
                    transa.Complete();
                }
            }
            return result;
        }
    }
}
