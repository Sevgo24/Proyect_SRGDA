
namespace SGRDA.Servicios.Proxy
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "ISEPreImpresion")]
    public interface ISEPreImpresion
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEPreImpresion/ObtenerPreImpresion", ReplyAction = "http://tempuri.org/ISEPreImpresion/ObtenerPreImpresionResponse")]
        SGRDA.Servicios.Proxy.Entidad.PreImpresion ObtenerPreImpresion(decimal idActualizar);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEPreImpresion/ObtenerPreImpresion", ReplyAction = "http://tempuri.org/ISEPreImpresion/ObtenerPreImpresionResponse")]
        System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.PreImpresion> ObtenerPreImpresionAsync(decimal idActualizar);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEPreImpresion/ActualizarEstado", ReplyAction = "http://tempuri.org/ISEPreImpresion/ActualizarEstadoResponse")]
        int ActualizarEstado(decimal idActualizar, string pcLocal, string estado);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEPreImpresion/ActualizarEstado", ReplyAction = "http://tempuri.org/ISEPreImpresion/ActualizarEstadoResponse")]
        System.Threading.Tasks.Task<int> ActualizarEstadoAsync(decimal idActualizar, string pcLocal, string estado);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEPreImpresion/ListarPendientes", ReplyAction = "http://tempuri.org/ISEPreImpresion/ListarPendientesResponse")]
        SGRDA.Servicios.Proxy.Entidad.PreImpresion[] ListarPendientes(string locahost);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEPreImpresion/ListarPendientes", ReplyAction = "http://tempuri.org/ISEPreImpresion/ListarPendientesResponse")]
        System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.PreImpresion[]> ListarPendientesAsync(string locahost);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISEPreImpresionChannel : ISEPreImpresion, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SEPreImpresionClient : System.ServiceModel.ClientBase<ISEPreImpresion>, ISEPreImpresion
    {

        public SEPreImpresionClient()
        {
        }

        public SEPreImpresionClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public SEPreImpresionClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEPreImpresionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEPreImpresionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public SGRDA.Servicios.Proxy.Entidad.PreImpresion ObtenerPreImpresion(decimal idActualizar)
        {
            return base.Channel.ObtenerPreImpresion(idActualizar);
        }

        public System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.PreImpresion> ObtenerPreImpresionAsync(decimal idActualizar)
        {
            return base.Channel.ObtenerPreImpresionAsync(idActualizar);
        }

        public int ActualizarEstado(decimal idActualizar, string pcLocal, string estado)
        {
            return base.Channel.ActualizarEstado(idActualizar, pcLocal, estado);
        }

        public System.Threading.Tasks.Task<int> ActualizarEstadoAsync(decimal idActualizar, string pcLocal, string estado)
        {
            return base.Channel.ActualizarEstadoAsync(idActualizar, pcLocal, estado);
        }

        public SGRDA.Servicios.Proxy.Entidad.PreImpresion[] ListarPendientes(string locahost)
        {
            return base.Channel.ListarPendientes(locahost);
        }

        public System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.PreImpresion[]> ListarPendientesAsync(string locahost)
        {
            return base.Channel.ListarPendientesAsync(locahost);
        }
    }
    
}
