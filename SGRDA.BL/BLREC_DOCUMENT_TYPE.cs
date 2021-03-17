using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREC_DOCUMENT_TYPE
    {
        public List<BEREC_DOCUMENT_TYPE> REC_DOCUMENT_TYPE_GET()
        {
            return new DAREC_DOCUMENT_TYPE().REC_DOCUMENT_TYPE_GET();
        }

        public List<BEREC_DOCUMENT_TYPE> ListarPage(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_DOCUMENT_TYPE().ListarPage(param, st, pagina, cantRegxPag);
        }

        public int Insertar(BEREC_DOCUMENT_TYPE en)
        {
            var lista = new DAREC_DOCUMENT_TYPE().REC_DOCUMENT_TYPE_GET_by_DOC_TYPE(en.DOC_TYPE);
            if (lista.Count != 0) return 0;
            return new DAREC_DOCUMENT_TYPE().Insertar(en);
        }

        public bool existeTipoDocumento(string Owner, string nombre)
        {
            return new DAREC_DOCUMENT_TYPE().existeTipoDocumento(Owner, nombre);
        }

        public bool existeTipoDocumento(string Owner, decimal id, string nombre)
        {
            return new DAREC_DOCUMENT_TYPE().existeTipoDocumento(Owner, id, nombre);
        }

        public int Actualizar(BEREC_DOCUMENT_TYPE en)
        {
            return new DAREC_DOCUMENT_TYPE().Actualizar(en); 
        }

        public List<BEREC_DOCUMENT_TYPE> REC_DOCUMENT_TYPE_GET_by_DOC_TYPE(decimal DOC_TYPE)
        {
            return new DAREC_DOCUMENT_TYPE().REC_DOCUMENT_TYPE_GET_by_DOC_TYPE(DOC_TYPE);
        }

        public int Eliminar(BEREC_DOCUMENT_TYPE en)
        {
            return new DAREC_DOCUMENT_TYPE().Eliminar(en);
        }

        public BEREC_DOCUMENT_TYPE Obtener(string Owner, decimal idTipo)
        {
            return new DAREC_DOCUMENT_TYPE().Obtener(  Owner,   idTipo);
        }

        public List<BEREC_DOCUMENT_TYPE> ListarComboTipoDoc(string owner)
        {
            return new DAREC_DOCUMENT_TYPE().ListarComboTipoDoc(owner);
        }

        public List<BEREC_DOCUMENT_TYPE> ListarComboTipoDocAlfresco(string owner)
        {
            return new DAREC_DOCUMENT_TYPE().ListarComboTipoDocAlfresco(owner);
        }
    }
}
