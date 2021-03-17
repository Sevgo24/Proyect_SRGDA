using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLREC_NUMBERING
    {

        public List<BEREC_NUMBERING> Get_REC_NUMBERING()
        {
            return new DAREC_NUMBERING().Get_REC_NUMBERING();
        }

        public List<BEREC_NUMBERING> REC_NUMBERING_by_NMR_ID(decimal NMR_ID)
        {
            return new DAREC_NUMBERING().REC_NUMBERING_by_NMR_ID(NMR_ID);
        }

        public List<BEREC_NUMBERING> REC_NUMBERING_Page(string owner, string param, int st, string serie, decimal off_id,int tipo, int pagina, int cantRegxPag)
        {
            return new DAREC_NUMBERING().REC_NUMBERING_Page(owner, param, st, serie, off_id, tipo, pagina, cantRegxPag);
        }

        public List<BEREC_NUMBERING> ListarCorrelativosRecibo(string owner, string param, int? st, string serie, int pagina, int cantRegxPag)
        {
            return new DAREC_NUMBERING().ListarCorrelativosRecibo(owner, param, st, serie, pagina, cantRegxPag);
        }

        public List<BEREC_NUMBERING> ListarCorrelativosNotaCredito(string owner, string param, string serie, int? st, int pagina, int cantRegxPag)
        {
            return new DAREC_NUMBERING().ListarCorrelativosNotaCredito(owner, param, serie, st, pagina, cantRegxPag);
        }

        public bool REC_NUMBERING_Ins(BEREC_NUMBERING en)
        {
            return new DAREC_NUMBERING().REC_NUMBERING_Ins(en);
        }

        public bool REC_NUMBERING_Upd(BEREC_NUMBERING en)
        {
            return new DAREC_NUMBERING().REC_NUMBERING_Upd(en);
        }

        public bool REC_NUMBERING_Del(decimal NMR_ID)
        {
            return new DAREC_NUMBERING().REC_NUMBERING_Del(NMR_ID);
        }

        public List<BEREC_NUMBERING> ListarXtipo(string owner, string nmrType)
        {
            return new DAREC_NUMBERING().ListarXtipo(owner, nmrType);
        }

        public int ObtenerXSerie(BEREC_NUMBERING correlativo)
        {
            return new DAREC_NUMBERING().ObtenerXSerie(correlativo);
        }

        public BEREC_NUMBERING ObtenerNombre(string owner, decimal id)
        {
            return new DAREC_NUMBERING().ObtenerNombre(owner, id);
        }

        public List<BEREC_NUMBERING> ListarSerie(string owner)
        {
            return new DAREC_NUMBERING().ListarSerie(owner);
        }

        public List<BEREC_NUMBERING> ListarSerieXtipo(string owner, string tipo)
        {
            return new DAREC_NUMBERING().ListarSerieXtipo(owner, tipo);
        }

        public BEREC_NUMBERING ObtenerCorrelativoXtipo(string owner, string tipo)
        {
            return new DAREC_NUMBERING().ObtenerCorrelativoXtipo(owner, tipo);
        }

        public int ActualizarCorrelativoPlanilla(string owner, decimal reportNumber, decimal? idReport, string user)
        {
            return new DAREC_NUMBERING().ActualizarCorrelativoPlanilla(owner, reportNumber, idReport, user);
        }

        public int ObtenerCorrelativoPLanilla(string owner, decimal idReport)
        {
            return new DAREC_NUMBERING().ObtenerCorrelativoPLanilla(owner, idReport);
        }

        public List<BEREC_NUMBERING> ListarNumeradores(string owner, string param, int st, string serie, string tipoSerie, int pagina, int cantRegxPag)
        {
            return new DAREC_NUMBERING().ListarNumeradores(owner, param, st, serie, tipoSerie, pagina, cantRegxPag);
        }
        
        public int ValidarSerieNumero(decimal idSerie, decimal numero)
        {
            return new DAREC_NUMBERING().ValidarSerieNumero(idSerie, numero);
        }

        public int ValidarSerieTipoSocio(string idSerie, decimal documento)
        {
            return new DAREC_NUMBERING().ValidarSerieTipoSocio(idSerie, documento);
        }
    }
}
