using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_RATE_FREQUENCY
    {
        public List<BEREC_RATE_FREQUENCY> Get_REC_RATE_FREQUENCY()
        {
            return new DAREC_RATE_FREQUENCY().Get_REC_RATE_FREQUENCY();
        }

        public List<BEREC_RATE_FREQUENCY> REC_RATE_FREQUENCY_GET_by_RAT_FID(decimal RAT_FID)
        {
            return new DAREC_RATE_FREQUENCY().REC_RATE_FREQUENCY_GET_by_RAT_FID(RAT_FID);
        }

        public List<BEREC_RATE_FREQUENCY> REC_RATE_FREQUENCY_Page(string owner, string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_RATE_FREQUENCY().REC_RATE_FREQUENCY_Page(owner,param, st, pagina, cantRegxPag);
        }

        public BEREC_RATE_FREQUENCY Obtener(decimal codTarifa)
        {
            return new DAREC_RATE_FREQUENCY().Obtener(codTarifa);
        }

        public bool REC_RATE_FREQUENCY_Ins(BEREC_RATE_FREQUENCY en)
        {
            int cod=0;
            cod = new DAREC_RATE_FREQUENCY().REC_RATE_FREQUENCY_Ins(en);

            if (en.PeriodoFrecuencia != null)
            {
                foreach (var periodo in en.PeriodoFrecuencia)
                {
                    periodo.RAT_FID = cod;
                    new DAPeriodoFrecuencia().Insertar(periodo);
                }
            }

            return true;
        }

        public bool REC_RATE_FREQUENCY_Upd(BEREC_RATE_FREQUENCY periodo, List<BEPeriodoFrecuencia> perEliminar, List<BEPeriodoFrecuencia> listperActivar)
        {
            DAPeriodoFrecuencia proxyPer = new DAPeriodoFrecuencia();
            var upd = new DAREC_RATE_FREQUENCY().REC_RATE_FREQUENCY_Upd(periodo);

            if (periodo.PeriodoFrecuencia != null)
            {
                foreach (var periodicidad in periodo.PeriodoFrecuencia)
                {
                    BEPeriodoFrecuencia ent = proxyPer.ObtenerPeriodicidadTarifa(periodo.OWNER, periodicidad.RAT_FID, periodicidad.FRQ_NPER_ANT);
                    if (ent == null)
                    {
                        periodicidad.RAT_FID = periodo.RAT_FID;
                        var codigoGenAdd = proxyPer.Insertar(periodicidad);                       
                    }
                    else if(ent.FRQ_NPER != periodicidad.FRQ_NPER || ent.FRQ_DESC != periodicidad.FRQ_DESC || ent.FRQ_DAYS != periodicidad.FRQ_DAYS
                           || ent.FRQ_DATE != periodicidad.FRQ_DATE || ent.FRQ_NPER != periodicidad.FRQ_NPER)
                    {

                        var result = proxyPer.Actualizar(periodicidad);
                    }
                }
            }

            if (perEliminar != null)
            {
                foreach (var item in perEliminar)
                {
                    proxyPer.Eliminar(periodo.OWNER, item.RAT_FID, periodo.LOG_USER_UPDAT, item.FRQ_NPER);
                }
            }

            if (listperActivar != null)
            {
                foreach (var item in listperActivar)
                {
                    proxyPer.Activar(periodo.OWNER, item.RAT_FID, periodo.LOG_USER_UPDAT, item.FRQ_NPER);
                }
            }
            return true;
        }

        public int Eliminar(BEREC_RATE_FREQUENCY en)
        {
            return new DAREC_RATE_FREQUENCY().Eliminar(en);
        }

        public List<BEREC_RATE_FREQUENCY> Listar(string owner)
        {
            return new DAREC_RATE_FREQUENCY().Listar(owner);
        }

        public BEREC_RATE_FREQUENCY ObtenerXTarifa(decimal idTarifa)
        {
            return new DAREC_RATE_FREQUENCY().ObtenerXTarifa(idTarifa);
        }
        public List<BEREC_RATE_FREQUENCY> ListarPeriodocidad(string owner)
        {
            return new DAREC_RATE_FREQUENCY().ListarPeriodocidad(owner);
        }
    }
}
