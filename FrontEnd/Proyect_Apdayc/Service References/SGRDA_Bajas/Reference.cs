//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyect_Apdayc.SGRDA_Bajas {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfBajas", Namespace="http://www.dbnet.cl", ItemName="bajas")]
    [System.SerializableAttribute()]
    public class ArrayOfBajas : System.Collections.Generic.List<Proyect_Apdayc.SGRDA_Bajas.bajas> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="bajas", Namespace="http://www.dbnet.cl")]
    [System.SerializableAttribute()]
    public partial class bajas : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Tipo_DocuField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Serie_InteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Foli_InteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Fech_EmisField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Motiv_AnulField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string tipo_refeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Tipo_REEMField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Serie_REEMField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Foli_REEMField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Tipo_Docu {
            get {
                return this.Tipo_DocuField;
            }
            set {
                if ((object.ReferenceEquals(this.Tipo_DocuField, value) != true)) {
                    this.Tipo_DocuField = value;
                    this.RaisePropertyChanged("Tipo_Docu");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Serie_Inte {
            get {
                return this.Serie_InteField;
            }
            set {
                if ((object.ReferenceEquals(this.Serie_InteField, value) != true)) {
                    this.Serie_InteField = value;
                    this.RaisePropertyChanged("Serie_Inte");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Foli_Inte {
            get {
                return this.Foli_InteField;
            }
            set {
                if ((object.ReferenceEquals(this.Foli_InteField, value) != true)) {
                    this.Foli_InteField = value;
                    this.RaisePropertyChanged("Foli_Inte");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Fech_Emis {
            get {
                return this.Fech_EmisField;
            }
            set {
                if ((object.ReferenceEquals(this.Fech_EmisField, value) != true)) {
                    this.Fech_EmisField = value;
                    this.RaisePropertyChanged("Fech_Emis");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Motiv_Anul {
            get {
                return this.Motiv_AnulField;
            }
            set {
                if ((object.ReferenceEquals(this.Motiv_AnulField, value) != true)) {
                    this.Motiv_AnulField = value;
                    this.RaisePropertyChanged("Motiv_Anul");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string tipo_refe {
            get {
                return this.tipo_refeField;
            }
            set {
                if ((object.ReferenceEquals(this.tipo_refeField, value) != true)) {
                    this.tipo_refeField = value;
                    this.RaisePropertyChanged("tipo_refe");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string Tipo_REEM {
            get {
                return this.Tipo_REEMField;
            }
            set {
                if ((object.ReferenceEquals(this.Tipo_REEMField, value) != true)) {
                    this.Tipo_REEMField = value;
                    this.RaisePropertyChanged("Tipo_REEM");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string Serie_REEM {
            get {
                return this.Serie_REEMField;
            }
            set {
                if ((object.ReferenceEquals(this.Serie_REEMField, value) != true)) {
                    this.Serie_REEMField = value;
                    this.RaisePropertyChanged("Serie_REEM");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string Foli_REEM {
            get {
                return this.Foli_REEMField;
            }
            set {
                if ((object.ReferenceEquals(this.Foli_REEMField, value) != true)) {
                    this.Foli_REEMField = value;
                    this.RaisePropertyChanged("Foli_REEM");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Mensaje", Namespace="http://www.dbnet.cl")]
    [System.SerializableAttribute()]
    public partial class Mensaje : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodigoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MensajesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TrackIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Codigo {
            get {
                return this.CodigoField;
            }
            set {
                if ((object.ReferenceEquals(this.CodigoField, value) != true)) {
                    this.CodigoField = value;
                    this.RaisePropertyChanged("Codigo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Mensajes {
            get {
                return this.MensajesField;
            }
            set {
                if ((object.ReferenceEquals(this.MensajesField, value) != true)) {
                    this.MensajesField = value;
                    this.RaisePropertyChanged("Mensajes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string TrackId {
            get {
                return this.TrackIdField;
            }
            set {
                if ((object.ReferenceEquals(this.TrackIdField, value) != true)) {
                    this.TrackIdField = value;
                    this.RaisePropertyChanged("TrackId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.dbnet.cl", ConfigurationName="SGRDA_Bajas.WssCargaBajasSoap")]
    public interface WssCargaBajasSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento RUC del espacio de nombres http://www.dbnet.cl no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/cargaBajas", ReplyAction="*")]
        Proyect_Apdayc.SGRDA_Bajas.cargaBajasResponse cargaBajas(Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/cargaBajas", ReplyAction="*")]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_Bajas.cargaBajasResponse> cargaBajasAsync(Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class cargaBajasRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="cargaBajas", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequestBody Body;
        
        public cargaBajasRequest() {
        }
        
        public cargaBajasRequest(Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.dbnet.cl")]
    public partial class cargaBajasRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string RUC;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public Proyect_Apdayc.SGRDA_Bajas.ArrayOfBajas ArchivoBajas;
        
        public cargaBajasRequestBody() {
        }
        
        public cargaBajasRequestBody(string RUC, Proyect_Apdayc.SGRDA_Bajas.ArrayOfBajas ArchivoBajas) {
            this.RUC = RUC;
            this.ArchivoBajas = ArchivoBajas;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class cargaBajasResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="cargaBajasResponse", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_Bajas.cargaBajasResponseBody Body;
        
        public cargaBajasResponse() {
        }
        
        public cargaBajasResponse(Proyect_Apdayc.SGRDA_Bajas.cargaBajasResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.dbnet.cl")]
    public partial class cargaBajasResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Proyect_Apdayc.SGRDA_Bajas.Mensaje cargaBajasResult;
        
        public cargaBajasResponseBody() {
        }
        
        public cargaBajasResponseBody(Proyect_Apdayc.SGRDA_Bajas.Mensaje cargaBajasResult) {
            this.cargaBajasResult = cargaBajasResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WssCargaBajasSoapChannel : Proyect_Apdayc.SGRDA_Bajas.WssCargaBajasSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WssCargaBajasSoapClient : System.ServiceModel.ClientBase<Proyect_Apdayc.SGRDA_Bajas.WssCargaBajasSoap>, Proyect_Apdayc.SGRDA_Bajas.WssCargaBajasSoap {
        
        public WssCargaBajasSoapClient() {
        }
        
        public WssCargaBajasSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WssCargaBajasSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WssCargaBajasSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WssCargaBajasSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Proyect_Apdayc.SGRDA_Bajas.cargaBajasResponse Proyect_Apdayc.SGRDA_Bajas.WssCargaBajasSoap.cargaBajas(Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequest request) {
            return base.Channel.cargaBajas(request);
        }
        
        public Proyect_Apdayc.SGRDA_Bajas.Mensaje cargaBajas(string RUC, Proyect_Apdayc.SGRDA_Bajas.ArrayOfBajas ArchivoBajas) {
            Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequest inValue = new Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequestBody();
            inValue.Body.RUC = RUC;
            inValue.Body.ArchivoBajas = ArchivoBajas;
            Proyect_Apdayc.SGRDA_Bajas.cargaBajasResponse retVal = ((Proyect_Apdayc.SGRDA_Bajas.WssCargaBajasSoap)(this)).cargaBajas(inValue);
            return retVal.Body.cargaBajasResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_Bajas.cargaBajasResponse> Proyect_Apdayc.SGRDA_Bajas.WssCargaBajasSoap.cargaBajasAsync(Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequest request) {
            return base.Channel.cargaBajasAsync(request);
        }
        
        public System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_Bajas.cargaBajasResponse> cargaBajasAsync(string RUC, Proyect_Apdayc.SGRDA_Bajas.ArrayOfBajas ArchivoBajas) {
            Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequest inValue = new Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_Bajas.cargaBajasRequestBody();
            inValue.Body.RUC = RUC;
            inValue.Body.ArchivoBajas = ArchivoBajas;
            return ((Proyect_Apdayc.SGRDA_Bajas.WssCargaBajasSoap)(this)).cargaBajasAsync(inValue);
        }
    }
}
