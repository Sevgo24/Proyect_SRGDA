 
namespace SGRDA.Servicios.Proxy
{

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "ISEFactura")]
    public interface ISEFactura
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEFactura/ObtenerFactura", ReplyAction = "http://tempuri.org/ISEFactura/ObtenerFacturaResponse")]
        SGRDA.Servicios.Proxy.Entidad.Factura ObtenerFactura(decimal idDocumento);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISEFactura/ObtenerFactura", ReplyAction = "http://tempuri.org/ISEFactura/ObtenerFacturaResponse")]
        System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.Factura> ObtenerFacturaAsync(decimal idDocumento);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISEFacturaChannel : ISEFactura, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SEFacturaClient : System.ServiceModel.ClientBase<ISEFactura>, ISEFactura
    {

        public SEFacturaClient()
        {
        }

        public SEFacturaClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public SEFacturaClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEFacturaClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SEFacturaClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public SGRDA.Servicios.Proxy.Entidad.Factura ObtenerFactura(decimal idDocumento)
        {
            return base.Channel.ObtenerFactura(idDocumento);
        }

        public System.Threading.Tasks.Task<SGRDA.Servicios.Proxy.Entidad.Factura> ObtenerFacturaAsync(decimal idDocumento)
        {
            return base.Channel.ObtenerFacturaAsync(idDocumento);
        }
    }
    }

