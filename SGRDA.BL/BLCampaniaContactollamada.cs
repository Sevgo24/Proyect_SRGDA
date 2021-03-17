using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLCampaniaContactollamada
    {
        public BECampaniaContactollamada Obtiene(string owner, decimal IdLote, decimal entidad)
        {
            BECampaniaContactollamada en = new BECampaniaContactollamada();
            en.LoteCliente = new DACampaniaContactollamada().ListarLoteCliente(owner, IdLote);
            if (en.LoteCliente != null)
                en.Observaciones = new DACampaniaContactollamada().ObservacionContactos(owner);
                en.Documentos = new BLDocumentoContactoLlamada().DocumentoXContactollamada(IdLote, GlobalVars.Global.OWNER, entidad);
            return en;
        }

        public int Insertar(BECampaniaContactollamada en)
        {
            var codigoGen = 0;
            var codigoGenObs = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                if (en.Observacion != null)
                {
                    if (!string.IsNullOrEmpty(en.Observacion.OBS_VALUE))
                    {
                        codigoGenObs = new DAObservationGral().InsertarObsGrl(en.Observacion);
                        var result = new DAObservationEst().Insertar(new BEObservationEst
                        {
                            EST_ID = codigoGen,
                            OBS_ID = codigoGenObs,
                            OWNER = en.OWNER,
                            LOG_USER_CREAT = en.LOG_USER_CREAT
                        });
                    }
                }
                
                en.OBS_ID = Convert.ToDecimal(codigoGenObs);
                codigoGen = new DACampaniaContactollamada().Insertar(en);
                en.CONC_MID = codigoGen;

                if (en.Documentos != null)
                {
                    var listaDoc = en.Documentos.Where(x => x.CONC_MID == en.CONC_CID).ToList();

                    foreach (var documento in listaDoc)
                    {
                        var codigoGenDoc = new DADocumentoGral().Insertar(documento);
                        var result = new DADocumentoContactoLlamada().Insertar(new BEDocumentoContactoLlamada
                            {
                                OWNER = en.OWNER,
                                CONC_MID = en.CONC_MID,
                                DOC_ID = codigoGenDoc,
                                LOG_USER_CREAT = en.LOG_USER_CREAT
                            });
                    }
                }

                transa.Complete();
            }
            return codigoGen;
        }

        public int Actualizar(BECampaniaContactollamada en, List<BEDocumentoGral> docEliminar, List<BEDocumentoGral> listDocActivar)
        {
            int upd = 0;
            decimal codigoGenObs = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                if (en.Observacion != null)
                {
                    BEObservationGral enObs = new BEObservationGral();
                    enObs.OWNER = en.Observacion.OWNER;
                    enObs.OBS_ID = en.Observacion.OBS_ID;
                    en.OBS_ID = en.Observacion.OBS_ID;
                    enObs.OBS_TYPE = en.Observacion.OBS_TYPE;
                    enObs.OBS_VALUE = en.Observacion.OBS_VALUE;
                    enObs.OBS_USER = en.Observacion.OBS_USER;
                    enObs.LOG_USER_UPDATE = en.Observacion.LOG_USER_UPDATE;
                    enObs.LOG_USER_CREAT = en.Observacion.LOG_USER_CREAT;
                    enObs.ENT_ID = en.Observacion.ENT_ID;

                    var obs = new DACampaniaContactollamada().obtenerObservacion(en.OWNER, en.CONC_MID, en.Observacion.OBS_ID);
                    if (obs == null)
                    {
                        codigoGenObs = new DAObservationGral().InsertarObsGrl(enObs);
                        en.OBS_ID = codigoGenObs;
                    }
                    else
                    {                    
                        var result = new DAObservationGral().Update(enObs);
                    }
                }

                upd = new DACampaniaContactollamada().Actualizar(en);

                DADocumentoGral proxyDoc = new DADocumentoGral();
                DADocumentoContactoLlamada proxyDocCon = new DADocumentoContactoLlamada();

                if (en.Documentos != null)
                {
                    foreach (var item in en.Documentos)
                    {
                        BEDocumentoContactoLlamada proxyDocObtener = proxyDocCon.ObtenerDocumento(en.OWNER, item.CONC_MID, item.DOC_ID);
                        if (proxyDocObtener == null)
                        {
                            var codigoGenAdd = proxyDoc.Insertar(item);
                            var result = proxyDocCon.Insertar(new BEDocumentoContactoLlamada
                            {
                                OWNER = en.OWNER,
                                CONC_MID = en.CONC_MID,
                                DOC_ID = codigoGenAdd,
                                LOG_USER_CREAT = en.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyDoc.Update(item);
                        }
                    }
                }

                if (docEliminar != null)
                {
                    docEliminar.ForEach(x => { proxyDoc.Eliminar(en.OWNER, x.DOC_ID, en.LOG_USER_UPDAT); });
                }
                if (listDocActivar != null)
                {
                    listDocActivar.ForEach(x => { proxyDoc.Activar(en.OWNER, x.DOC_ID, en.LOG_USER_UPDAT); });
                }

                transa.Complete();
            }
            return upd;
        }
    }
}
