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
    public interface ISEArtista
    {
        [OperationContract]
        List<Artista> ListaArtistaOracle(string owner, int flag, string nombre,
                                                int pagina, int cantRegxPag);
    }
}