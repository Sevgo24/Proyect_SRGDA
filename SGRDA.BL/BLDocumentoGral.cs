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
    public class BLDocumentoGral
    {
        public int Insertar(BEDocumentoGral doc,BEDocumentoLic docLic)
        {
            var idGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                idGen = new DADocumentoGral().Insertar(doc);
                docLic.DOC_ID = idGen;
                var result = new DADocumentoLic().Insertar(docLic);
                transa.Complete();
            }
          return idGen;
        }

        public int InsertarEst(BEDocumentoGral doc, BEDocumentoLic docLic)
        {
            var idGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                idGen = new DADocumentoGral().Insertar(doc);
                docLic.DOC_ID = idGen;
                var result = new DADocumentoLic().InsertarEST(docLic);
                transa.Complete();
            }
            return idGen;
        }

        public int InsertarDocBps(BEDocumentoGral doc, BEDocumentoBps docBps)
        {
            var idGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                idGen = new DADocumentoGral().Insertar(doc);
                docBps.DOC_ID = idGen;
                var result = new DADocumentoBps().Insertar(docBps);
                transa.Complete();
            }
            return idGen;
        }
        
        public BETipoDocumento ObtenerTipoDocumento(string Owner, decimal idTipo)
        {

            return new DADocumentoGral().ObtenerTipoDocumento(Owner, idTipo);
        }
        public List<BEDocumentoGral> ObtenerDocXLicencia(decimal codigoLic, string owner, decimal tipoEntidad)
        {
            return new DADocumentoGral().DocumentoXLicencia(codigoLic, owner, tipoEntidad);
        }
        public BEDocumentoGral ObtenerDocLic(string owner, decimal idDoc, decimal idLic, decimal idEntidad)
        {
            return new DADocumentoGral().ObtenerDocLic(owner,idDoc,idLic,idEntidad);
        }
        public int Update(BEDocumentoGral doc)
        {
            return new DADocumentoGral().Update(doc);
        }
        public int Eliminar(string owner, decimal docId, string user)
        {
            return new DADocumentoGral().Eliminar(owner, docId, user);
        }
        public int Activar(string owner, decimal docId, string user)
        {
            return new DADocumentoGral().Activar(owner, docId, user);
        }
        public int UpdatePath(string owner, decimal docId, string fileName) {

            return new DADocumentoGral().UpdatePath(owner, docId, fileName);
        }
        public BEDocumentoGral Obtener(string owner, decimal idDoc)
        {
            return new DADocumentoGral().Obtener(owner, idDoc);
        }

        /// <summary>
        /// Eliminacion fisica del registro al no completar correctamente la accion de documento de entrada - tab aproceso
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="idDoc"></param>
        /// <returns></returns>
        public int EliminarFisico(string owner, decimal idDoc)
        {
            return new DADocumentoGral().EliminarFisico(owner, idDoc);
        }

        public int InsertarBec(BEDocumentoGral doc, BEDocumentoLic docLic)
        {
            var idGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                idGen = new DADocumentoGral().Insertar(doc);
                docLic.DOC_ID = idGen;
                var result = new DADocumentoLic().InsertarDocBec(docLic);
                transa.Complete();
            }
            return idGen;
        }
    }
}
