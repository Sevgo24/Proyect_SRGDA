using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SGRDA.Servicios.Entidad;

namespace SGRDA.Servicios.Contrato
{
    [ServiceContract]
    public interface ISEPreImpresion
    {
        [OperationContract]
        PreImpresion ObtenerPreImpresion(decimal idActualizar);
        [OperationContract]
        int ActualizarEstado(decimal idActualizar, string pcLocal, string estado);
        [OperationContract]
        List<PreImpresion> ListarPendientes(string locahost);
    }
}
