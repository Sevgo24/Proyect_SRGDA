
namespace SGRDA.Servicios.Proxy
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "ISEWF_Radio")]
    public interface ISEWF_Radio
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEWF_Radio/ListaActualizarEstadoLicRadioDif", ReplyAction = "http://tempuri.org/ISEWF_Radio/ListaActualizarEstadoLicRadioDifResponse")]
        SGRDA.Servicios.Proxy.Entidad.WF_Radio[] ListaActualizarEstadoLicRadioDif(string owner);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEWF_Radio/ListaActualizarEstadoLicRadioDif", ReplyAction = "http://tempuri.org/ISEWF_Radio/ListaActualizarEstadoLicRadioDifResponse")]
        System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.WF_Radio[]> ListaActualizarEstadoLicRadioDifAsync(string owner);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEWF_Radio/CambiarATEstadoWFRadio", ReplyAction = "http://tempuri.org/ISEWF_Radio/CambiarATEstadoWFRadioResponse")]
        bool CambiarATEstadoWFRadio(string owner);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEWF_Radio/CambiarATEstadoWFRadio", ReplyAction = "http://tempuri.org/ISEWF_Radio/CambiarATEstadoWFRadioResponse")]
        System.Threading.Tasks.Task<bool> CambiarATEstadoWFRadioAsync(string owner);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISEWF_RadioChannel : ISEWF_Radio, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SEWF_RadioClient : System.ServiceModel.ClientBase<ISEWF_Radio>, ISEWF_Radio
    {

        public SEWF_RadioClient()
        {
        }

        public SEWF_RadioClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public SEWF_RadioClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEWF_RadioClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEWF_RadioClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public SGRDA.Servicios.Proxy.Entidad.WF_Radio[] ListaActualizarEstadoLicRadioDif(string owner)
        {
            return base.Channel.ListaActualizarEstadoLicRadioDif(owner);
        }

        public System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.WF_Radio[]> ListaActualizarEstadoLicRadioDifAsync(string owner)
        {
            return base.Channel.ListaActualizarEstadoLicRadioDifAsync(owner);
        }

        public bool CambiarATEstadoWFRadio(string owner)
        {
            return base.Channel.CambiarATEstadoWFRadio(owner);
        }

        public System.Threading.Tasks.Task<bool> CambiarATEstadoWFRadioAsync(string owner)
        {
            return base.Channel.CambiarATEstadoWFRadioAsync(owner);
        }
    }
}
