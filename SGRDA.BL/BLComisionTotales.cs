using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLComisionTotales
    {
        public List<BEComisionTotales> ListarPage(string Owner, decimal ProgramaId, DateTime Ultfecha, decimal IdRepresentante, int st, int pagina, int cantRegxPag)
        {
            return new DAComisionTotales().ListarPage(Owner, ProgramaId, Ultfecha, IdRepresentante, st, pagina, cantRegxPag);
        }

        public BEComisionTotales Obtiene(string owner, decimal IdPrograma)
        {
            var objComision = new DAComisionTotales().ObtenerComisionTotales(owner, IdPrograma);
            if (objComision != null)
            {
                objComision.Representantes = new DAComisionTotales().ListaRepresentante(owner, IdPrograma);
                objComision.Rangos = new DAComisionTotales().ListaRangoRepresentante(owner, IdPrograma);
            }
            return objComision;
        }

        public int Insertar(BEComisionTotales en)
        {
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DAComisionTotales().Insertar(en);
                en.PRG_ID = codigoGen;

                if (en.Representantes != null)
                {
                    foreach (var representante in en.Representantes)
                    {
                        var result = new DAComisionTotales().InsertarRepresentante(new BEComisionRepresentantes
                            {
                                OWNER = en.OWNER,
                                PRG_ID = codigoGen,
                                BPS_ID = representante.BPS_ID,
                                STARTS = representante.STARTS,
                                ENDS = representante.ENDS,
                                LOG_USER_CREAT = en.LOG_USER_CREAT
                            });
                    }
                }

                transa.Complete();
            }
            return codigoGen;
        }

        public int Actualizar(BEComisionTotales en, List<BEComisionRepresentantes> repEliminar, List<BEComisionRepresentantes> listRepActivar, List<BEComisionRecaudadorRango> ragEliminar, List<BEComisionRecaudadorRango> listRagActivar)
        {
            int upd = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DAComisionTotales().Actualizar(en);

                if (en.Rangos != null)
                {
                    foreach (var item in en.Rangos)
                    {

                        BEComisionRecaudadorRango ent = new DAComisionTotales().ObtenerRangos(item.OWNER, item.SEQUENCE, en.PRG_ID);
                        if (ent.PRG_ID == 0)
                        {
                            var result = new DAComisionTotales().InsertarRango(new BEComisionRecaudadorRango
                            {
                                OWNER = item.OWNER,
                                PRG_ID = en.PRG_ID,
                                BPS_ID = item.BPS_ID,
                                PRG_ORDER = item.PRG_ORDER,
                                PRG_VALUEI = item.PRG_VALUEI,
                                PRG_VALUEF = item.PRG_VALUEF,
                                PRG_PERC = item.PRG_PERC,
                                PRG_VALUEC = item.PRG_VALUEC,
                                LOG_USER_CREAT = en.LOG_USER_CREAT
                            });
                        }
                        else if (ent.BPS_ID != item.BPS_ID || ent.PRG_ORDER != item.PRG_ORDER || ent.PRG_VALUEI != item.PRG_VALUEI
                            || ent.PRG_VALUEF != item.PRG_VALUEF || ent.PRG_PERC != item.PRG_PERC || ent.PRG_VALUEC != item.PRG_VALUEC)
                        {
                            var result = new DAComisionTotales().ActualizarRango(item);
                        }
                    }
                }

                if (en.Representantes != null)
                {
                    foreach (var item in en.Representantes)
                    {
                        BEComisionRepresentantes ent = new DAComisionTotales().ObtenerRepresentante(item.OWNER, item.SEQUENCE, en.PRG_ID);
                       
                        if (ent.PRG_ID == 0)
                        {
                            var result = new DAComisionTotales().InsertarRepresentante(new BEComisionRepresentantes
                            {
                                OWNER = item.OWNER,
                                PRG_ID = en.PRG_ID,
                                BPS_ID = item.BPS_ID,
                                STARTS = item.STARTS,
                                ENDS = item.ENDS,
                                LOG_USER_CREAT = en.LOG_USER_CREAT
                            });
                        }
                        else if (ent.BPS_ID != item.BPS_ID || ent.STARTS != item.STARTS || ent.ENDS != item.ENDS)
                        {
                            var result = new DAComisionTotales().ActualizarRepresentante(item);
                        }
                    }
                }               

                if (repEliminar != null)
                {
                    foreach (var item in repEliminar)
                    {
                        BEComisionRepresentantes rep = new BEComisionRepresentantes();
                        rep.OWNER = en.OWNER;
                        rep.SEQUENCE = item.SEQUENCE;
                        rep.PRG_ID = en.PRG_ID;
                        rep.LOG_USER_UPDATE = en.LOG_USER_UPDATE;
                        var result = new DAComisionTotales().InactivarRepresentante(rep);
                    }
                }
                if (listRepActivar != null)
                {
                    foreach (var item in listRepActivar)
                    {
                        BEComisionRepresentantes rep = new BEComisionRepresentantes();
                        rep.OWNER = en.OWNER;
                        rep.SEQUENCE = item.SEQUENCE;
                        rep.PRG_ID = en.PRG_ID;
                        rep.LOG_USER_UPDATE = en.LOG_USER_UPDATE;
                        var result = new DAComisionTotales().ActivarRepresentante(rep);
                    }
                }

                if (ragEliminar != null)
                {
                    foreach (var item in ragEliminar)
                    {
                        BEComisionRecaudadorRango rep = new BEComisionRecaudadorRango();
                        rep.OWNER = en.OWNER;
                        rep.SEQUENCE = item.SEQUENCE;
                        rep.PRG_ID = en.PRG_ID;
                        rep.LOG_USER_UPDATE = en.LOG_USER_UPDATE;
                        var result = new DAComisionTotales().InactivarRango(rep);
                    }
                }
                if (listRagActivar != null)
                {
                    foreach (var item in listRagActivar)
                    {
                        BEComisionRecaudadorRango rep = new BEComisionRecaudadorRango();
                        rep.OWNER = en.OWNER;
                        rep.SEQUENCE = item.SEQUENCE;
                        rep.PRG_ID = en.PRG_ID;
                        rep.LOG_USER_UPDATE = en.LOG_USER_UPDATE;
                        var result = new DAComisionTotales().ActivarRango(rep);
                    }
                }

                transa.Complete();
            }
            return upd;
        }

        public List<BEComisionRecaudadorRango> ListaRangoRepresentante(string Owner, decimal IdPrograma)
        {
            return new DAComisionTotales().ListaRangoRepresentante(Owner, IdPrograma);
        }

        public int InsertarRango(BEComisionRecaudadorRango en)
        {
            return new DAComisionTotales().InsertarRango(en);
        }

        public int ActualizarRango(BEComisionRecaudadorRango en)
        {
            return new DAComisionTotales().ActualizarRango(en);
        }
    }
}
