using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLDocumentoContactoLlamada
    {
        public int Insertar(BEDocumentoContactoLlamada en)
        {
            return new DADocumentoContactoLlamada().Insertar(en);
        }

        public BEDocumentoContactoLlamada ObtenerDocumento(string owner, decimal Id, decimal IdDoc)
        {
            return new DADocumentoContactoLlamada().ObtenerDocumento(owner, Id, IdDoc);
        }

        public List<BEDocumentoGral> DocumentoXContactollamada(decimal Id, string owner, decimal tipoEntidad)
        {
            return new DADocumentoContactoLlamada().DocumentoXContactollamada(Id, owner, tipoEntidad);
        }
    }
}
