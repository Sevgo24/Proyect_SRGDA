
namespace SGRDA.Servicios.Proxy
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "ISEArtista")]
    public interface ISEArtista
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEArtista/ListaArtistaOracle", ReplyAction = "http://tempuri.org/ISEArtista/ListaArtistaOracleResponse")]
        SGRDA.Servicios.Proxy.Entidad.Artista[] ListaArtistaOracle(string owner, int flag, string nombre, int pagina, int cantRegxPag);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEArtista/ListaArtistaOracle", ReplyAction = "http://tempuri.org/ISEArtista/ListaArtistaOracleResponse")]
        System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.Artista[]> ListaArtistaOracleAsync(string owner, int flag, string nombre, int pagina, int cantRegxPag);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISEArtistaChannel : ISEArtista, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SEArtistaClient : System.ServiceModel.ClientBase<ISEArtista>, ISEArtista
    {

        public SEArtistaClient()
        {
        }

        public SEArtistaClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public SEArtistaClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEArtistaClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEArtistaClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public SGRDA.Servicios.Proxy.Entidad.Artista[] ListaArtistaOracle(string owner, int flag, string nombre, int pagina, int cantRegxPag)
        {
            return base.Channel.ListaArtistaOracle(owner, flag, nombre, pagina, cantRegxPag);
        }

        public System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.Artista[]> ListaArtistaOracleAsync(string owner, int flag, string nombre, int pagina, int cantRegxPag)
        {
            return base.Channel.ListaArtistaOracleAsync(owner, flag, nombre, pagina, cantRegxPag);
        }
    }
}
