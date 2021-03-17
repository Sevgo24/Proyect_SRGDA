
namespace SGRDA.Servicios.Proxy
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "ISEWF_TvCable")]
    public interface ISEWF_TvCable
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEWF_TvCable/CambiarEstadoAutomatico", ReplyAction = "http://tempuri.org/ISEWF_TvCable/CambiarEstadoAutomaticoResponse")]
        bool CambiarEstadoAutomatico(string owner);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEWF_TvCable/CambiarEstadoAutomatico", ReplyAction = "http://tempuri.org/ISEWF_TvCable/CambiarEstadoAutomaticoResponse")]
        System.Threading.Tasks.Task<bool> CambiarEstadoAutomaticoAsync(string owner);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISEWF_TvCableChannel : ISEWF_TvCable, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SEWF_TvCableClient : System.ServiceModel.ClientBase<ISEWF_TvCable>, ISEWF_TvCable
    {

        public SEWF_TvCableClient()
        {
        }

        public SEWF_TvCableClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public SEWF_TvCableClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEWF_TvCableClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEWF_TvCableClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }
        public bool CambiarEstadoAutomatico(string owner)
        {
            return base.Channel.CambiarEstadoAutomatico(owner);
        }

        public System.Threading.Tasks.Task<bool> CambiarEstadoAutomaticoAsync(string owner)
        {
            return base.Channel.CambiarEstadoAutomaticoAsync(owner);
        }
    }
}
