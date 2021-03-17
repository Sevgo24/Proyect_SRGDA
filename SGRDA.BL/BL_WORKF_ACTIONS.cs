using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using System.Transactions;
using SGRDA.Entities.WorkFlow;
using SGRDA.DA.WorkFlow;

namespace SGRDA.BL.WorkFlow
{
    public class BL_WORKF_ACTIONS
    {

        public bool CumplePreRequisito(string owner, decimal wrkf_ref1, decimal wrkf_amid)
        {
            return new DA_WORKF_ACTIONS().CumplePreRequisito(owner, wrkf_ref1, wrkf_amid);
        }

        public List<WORKF_ACTIONS> Listar(string owner, string nombre, string etiqueta,
                              decimal idTipoAccion, decimal idTipoDato, decimal idProceso, string idAuto,
                             int estado, int pagina, int cantRegxPag)
        {
            return new DA_WORKF_ACTIONS().Listar(owner, nombre, etiqueta,
                                                     idTipoAccion, idTipoDato, idProceso, idAuto,
                                                    estado, pagina, cantRegxPag);
        }

        public WORKF_ACTIONS Obtener(string owner, decimal? id)
        {
            return new DA_WORKF_ACTIONS().Obtener(owner, id);
        }

        public int Insertar(WORKF_ACTIONS en)
        {
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DA_WORKF_ACTIONS().Insertar(en);

                if (en.AgenteAccion != null)
                {
                    foreach (var detalle in en.AgenteAccion)
                    {
                        detalle.LOG_USER_UPDATE = en.LOG_USER_CREAT;
                        detalle.WRKF_AID = codigoGen;
                        var codigoGenVal = new DAAgenteAccion().InsertarDetalle(detalle);
                    }
                }
                transa.Complete();
            }
            return codigoGen;
        }

        public int Actualizar(WORKF_ACTIONS form, List<BEAgenteAccion> detEliminar, List<BEAgenteAccion> listDelActivar)
        {
            int upd = 0;
            //return new DA_WORKF_ACTIONS().Actualizar(form);

            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DA_WORKF_ACTIONS().Actualizar(form);

                ///logica de negocio para actualizar los Valores
                DAAgenteAccion Det = new DAAgenteAccion();
                if (form.AgenteAccion != null)
                {
                    foreach (var item in form.AgenteAccion)
                    {
                        ///verifica si no existe el valor
                        ///si no existe se registra
                        BEAgenteAccion DetObtener = Det.ObtenerDetalle(form.OWNER, form.WRKF_AID, item.WRKF_AGID);
                        if (DetObtener == null)
                        {
                            item.LOG_USER_UPDATE = form.LOG_USER_CREAT;
                            item.WRKF_AID = form.WRKF_AID;
                            var codigoGenAdd = Det.InsertarDetalle(item);
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion
                            item.LOG_USER_UPDATE = form.LOG_USER_CREAT;
                            var result = Det.ActualizarDetalle(item);
                        }
                    }
                }
                /// se elimina los valores
                if (detEliminar != null)
                {
                    foreach (var item in detEliminar)
                    {
                        Det.Eliminar(form.OWNER, item.WRKF_AGAC_ID, form.LOG_USER_CREAT);
                    }
                }
                /// activa los valores
                if (listDelActivar != null)
                {
                    foreach (var item in listDelActivar)
                    {
                        Det.Activar(form.OWNER, item.WRKF_AGAC_ID, form.LOG_USER_UPDATE);
                    }
                }
                transa.Complete();
            }
            return upd;
        }

        public int RollBackStateLic(decimal idTrace)
        {
            return new DA_WORKF_ACTIONS().RollBackStateLic(idTrace);
        }

        public string ObtenerProcMod(string owner, decimal wrkfid)
        {
            return new DA_WORKF_ACTIONS().ObtenerProcMod(owner, wrkfid);
        }
    }
}
