using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using SGRDA.Servicios.Entidad;


namespace SGRDA.Servicios.Contrato
{

    [ServiceContract]
    public interface ISEFactura
    {

        [OperationContract]
        Factura ObtenerFactura(decimal idDocumento);

    }
}
