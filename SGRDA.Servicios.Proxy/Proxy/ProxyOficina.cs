
namespace SGRDA.Servicios.Proxy
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "ISEOficina")]
    public interface ISEOficina
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEOficina/ListarOficina", ReplyAction = "http://tempuri.org/ISEOficina/ListarOficinaResponse")]
        SGRDA.Servicios.Proxy.Entidad.Oficina[] ListarOficina(string owner);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEOficina/ListarOficina", ReplyAction = "http://tempuri.org/ISEOficina/ListarOficinaResponse")]
        System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.Oficina[]> ListarOficinaAsync(string owner);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEOficina/ObtenerOficina", ReplyAction = "http://tempuri.org/ISEOficina/ObtenerOficinaResponse")]
        SGRDA.Servicios.Proxy.Entidad.Oficina ObtenerOficina(string owner, decimal codigo);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEOficina/ObtenerOficina", ReplyAction = "http://tempuri.org/ISEOficina/ObtenerOficinaResponse")]
        System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.Oficina> ObtenerOficinaAsync(string owner, decimal codigo);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISEOficinaChannel : ISEOficina, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SEOficinaClient : System.ServiceModel.ClientBase<ISEOficina>, ISEOficina
    {

        public SEOficinaClient()
        {
        }

        public SEOficinaClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public SEOficinaClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEOficinaClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEOficinaClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public SGRDA.Servicios.Proxy.Entidad.Oficina[] ListarOficina(string owner)
        {
            return base.Channel.ListarOficina(owner);
        }

        public System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.Oficina[]> ListarOficinaAsync(string owner)
        {
            return base.Channel.ListarOficinaAsync(owner);
        }

        public SGRDA.Servicios.Proxy.Entidad.Oficina ObtenerOficina(string owner, decimal codigo)
        {
            return base.Channel.ObtenerOficina(owner, codigo);
        }

        public System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.Oficina> ObtenerOficinaAsync(string owner, decimal codigo)
        {
            return base.Channel.ObtenerOficinaAsync(owner, codigo);
        }
    }
}

