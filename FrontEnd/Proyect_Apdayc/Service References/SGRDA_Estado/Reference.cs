﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyect_Apdayc.SGRDA_Estado {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.dbnet.cl", ConfigurationName="SGRDA_Estado.Service1Soap")]
    public interface Service1Soap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/ConsultaEstado", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ConsultaEstado(string ruc, int tipo_docu, int foli_inte, string serie_inte);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/ConsultaEstado", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ConsultaEstadoAsync(string ruc, int tipo_docu, int foli_inte, string serie_inte);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/ActualizaEstadoDoc", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int ActualizaEstadoDoc(string ruc, int CodiEmpr, int tipo_docu, int foli_inte, string serie_inte, string Estado);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/ActualizaEstadoDoc", ReplyAction="*")]
        System.Threading.Tasks.Task<int> ActualizaEstadoDocAsync(string ruc, int CodiEmpr, int tipo_docu, int foli_inte, string serie_inte, string Estado);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Service1SoapChannel : Proyect_Apdayc.SGRDA_Estado.Service1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1SoapClient : System.ServiceModel.ClientBase<Proyect_Apdayc.SGRDA_Estado.Service1Soap>, Proyect_Apdayc.SGRDA_Estado.Service1Soap {
        
        public Service1SoapClient() {
        }
        
        public Service1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string ConsultaEstado(string ruc, int tipo_docu, int foli_inte, string serie_inte) {
            return base.Channel.ConsultaEstado(ruc, tipo_docu, foli_inte, serie_inte);
        }
        
        public System.Threading.Tasks.Task<string> ConsultaEstadoAsync(string ruc, int tipo_docu, int foli_inte, string serie_inte) {
            return base.Channel.ConsultaEstadoAsync(ruc, tipo_docu, foli_inte, serie_inte);
        }
        
        public int ActualizaEstadoDoc(string ruc, int CodiEmpr, int tipo_docu, int foli_inte, string serie_inte, string Estado) {
            return base.Channel.ActualizaEstadoDoc(ruc, CodiEmpr, tipo_docu, foli_inte, serie_inte, Estado);
        }
        
        public System.Threading.Tasks.Task<int> ActualizaEstadoDocAsync(string ruc, int CodiEmpr, int tipo_docu, int foli_inte, string serie_inte, string Estado) {
            return base.Channel.ActualizaEstadoDocAsync(ruc, CodiEmpr, tipo_docu, foli_inte, serie_inte, Estado);
        }
    }
}