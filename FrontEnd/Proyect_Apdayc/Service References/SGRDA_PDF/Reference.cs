//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyect_Apdayc.SGRDA_PDF {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="http://www.dbnet.cl", ItemName="string")]
    [System.SerializableAttribute()]
    public class ArrayOfString : System.Collections.Generic.List<string> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.dbnet.cl", ConfigurationName="SGRDA_PDF.consultaDocumentosPeruSoap")]
    public interface consultaDocumentosPeruSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento lstDocs del espacio de nombres http://www.dbnet.cl no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/setRegistraPeticionDescarga", ReplyAction="*")]
        Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaResponse setRegistraPeticionDescarga(Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/setRegistraPeticionDescarga", ReplyAction="*")]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaResponse> setRegistraPeticionDescargaAsync(Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento _ruttEmpr del espacio de nombres http://www.dbnet.cl no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/getDocumentoPDF", ReplyAction="*")]
        Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFResponse getDocumentoPDF(Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/getDocumentoPDF", ReplyAction="*")]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFResponse> getDocumentoPDFAsync(Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento _ruttEmpr del espacio de nombres http://www.dbnet.cl no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/getDocumentoXML", ReplyAction="*")]
        Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLResponse getDocumentoXML(Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/getDocumentoXML", ReplyAction="*")]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLResponse> getDocumentoXMLAsync(Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento _ruttEmpr del espacio de nombres http://www.dbnet.cl no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/getDocumentoCDR", ReplyAction="*")]
        Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRResponse getDocumentoCDR(Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/getDocumentoCDR", ReplyAction="*")]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRResponse> getDocumentoCDRAsync(Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento ObtnerVersionResult del espacio de nombres http://www.dbnet.cl no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/ObtnerVersion", ReplyAction="*")]
        Proyect_Apdayc.SGRDA_PDF.ObtnerVersionResponse ObtnerVersion(Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.dbnet.cl/ObtnerVersion", ReplyAction="*")]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.ObtnerVersionResponse> ObtnerVersionAsync(Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class setRegistraPeticionDescargaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="setRegistraPeticionDescarga", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequestBody Body;
        
        public setRegistraPeticionDescargaRequest() {
        }
        
        public setRegistraPeticionDescargaRequest(Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.dbnet.cl")]
    public partial class setRegistraPeticionDescargaRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.ArrayOfString lstDocs;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string codiEmpr;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string ruc;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string user;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string guid;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string ruta;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string correo;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string rutaWeb;
        
        public setRegistraPeticionDescargaRequestBody() {
        }
        
        public setRegistraPeticionDescargaRequestBody(Proyect_Apdayc.SGRDA_PDF.ArrayOfString lstDocs, string codiEmpr, string ruc, string user, string guid, string ruta, string correo, string rutaWeb) {
            this.lstDocs = lstDocs;
            this.codiEmpr = codiEmpr;
            this.ruc = ruc;
            this.user = user;
            this.guid = guid;
            this.ruta = ruta;
            this.correo = correo;
            this.rutaWeb = rutaWeb;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class setRegistraPeticionDescargaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="setRegistraPeticionDescargaResponse", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaResponseBody Body;
        
        public setRegistraPeticionDescargaResponse() {
        }
        
        public setRegistraPeticionDescargaResponse(Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.dbnet.cl")]
    public partial class setRegistraPeticionDescargaResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool setRegistraPeticionDescargaResult;
        
        public setRegistraPeticionDescargaResponseBody() {
        }
        
        public setRegistraPeticionDescargaResponseBody(bool setRegistraPeticionDescargaResult) {
            this.setRegistraPeticionDescargaResult = setRegistraPeticionDescargaResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getDocumentoPDFRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getDocumentoPDF", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequestBody Body;
        
        public getDocumentoPDFRequest() {
        }
        
        public getDocumentoPDFRequest(Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.dbnet.cl")]
    public partial class getDocumentoPDFRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string _ruttEmpr;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string _folioDTE;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string _tipoDTE;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string _serieInte;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string _fechaDTE;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string _monTotal;
        
        public getDocumentoPDFRequestBody() {
        }
        
        public getDocumentoPDFRequestBody(string _ruttEmpr, string _folioDTE, string _tipoDTE, string _serieInte, string _fechaDTE, string _monTotal) {
            this._ruttEmpr = _ruttEmpr;
            this._folioDTE = _folioDTE;
            this._tipoDTE = _tipoDTE;
            this._serieInte = _serieInte;
            this._fechaDTE = _fechaDTE;
            this._monTotal = _monTotal;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getDocumentoPDFResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getDocumentoPDFResponse", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFResponseBody Body;
        
        public getDocumentoPDFResponse() {
        }
        
        public getDocumentoPDFResponse(Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.dbnet.cl")]
    public partial class getDocumentoPDFResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.ArrayOfString getDocumentoPDFResult;
        
        public getDocumentoPDFResponseBody() {
        }
        
        public getDocumentoPDFResponseBody(Proyect_Apdayc.SGRDA_PDF.ArrayOfString getDocumentoPDFResult) {
            this.getDocumentoPDFResult = getDocumentoPDFResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getDocumentoXMLRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getDocumentoXML", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequestBody Body;
        
        public getDocumentoXMLRequest() {
        }
        
        public getDocumentoXMLRequest(Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.dbnet.cl")]
    public partial class getDocumentoXMLRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string _ruttEmpr;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string _folioDTE;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string _tipoDTE;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string _serieInte;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string _fechaDTE;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string _monTotal;
        
        public getDocumentoXMLRequestBody() {
        }
        
        public getDocumentoXMLRequestBody(string _ruttEmpr, string _folioDTE, string _tipoDTE, string _serieInte, string _fechaDTE, string _monTotal) {
            this._ruttEmpr = _ruttEmpr;
            this._folioDTE = _folioDTE;
            this._tipoDTE = _tipoDTE;
            this._serieInte = _serieInte;
            this._fechaDTE = _fechaDTE;
            this._monTotal = _monTotal;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getDocumentoXMLResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getDocumentoXMLResponse", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLResponseBody Body;
        
        public getDocumentoXMLResponse() {
        }
        
        public getDocumentoXMLResponse(Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.dbnet.cl")]
    public partial class getDocumentoXMLResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.ArrayOfString getDocumentoXMLResult;
        
        public getDocumentoXMLResponseBody() {
        }
        
        public getDocumentoXMLResponseBody(Proyect_Apdayc.SGRDA_PDF.ArrayOfString getDocumentoXMLResult) {
            this.getDocumentoXMLResult = getDocumentoXMLResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getDocumentoCDRRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getDocumentoCDR", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequestBody Body;
        
        public getDocumentoCDRRequest() {
        }
        
        public getDocumentoCDRRequest(Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.dbnet.cl")]
    public partial class getDocumentoCDRRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string _ruttEmpr;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string _folioDTE;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string _tipoDTE;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string _serieInte;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string _fechaDTE;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string _monTotal;
        
        public getDocumentoCDRRequestBody() {
        }
        
        public getDocumentoCDRRequestBody(string _ruttEmpr, string _folioDTE, string _tipoDTE, string _serieInte, string _fechaDTE, string _monTotal) {
            this._ruttEmpr = _ruttEmpr;
            this._folioDTE = _folioDTE;
            this._tipoDTE = _tipoDTE;
            this._serieInte = _serieInte;
            this._fechaDTE = _fechaDTE;
            this._monTotal = _monTotal;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getDocumentoCDRResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="getDocumentoCDRResponse", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRResponseBody Body;
        
        public getDocumentoCDRResponse() {
        }
        
        public getDocumentoCDRResponse(Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.dbnet.cl")]
    public partial class getDocumentoCDRResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.ArrayOfString getDocumentoCDRResult;
        
        public getDocumentoCDRResponseBody() {
        }
        
        public getDocumentoCDRResponseBody(Proyect_Apdayc.SGRDA_PDF.ArrayOfString getDocumentoCDRResult) {
            this.getDocumentoCDRResult = getDocumentoCDRResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ObtnerVersionRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ObtnerVersion", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequestBody Body;
        
        public ObtnerVersionRequest() {
        }
        
        public ObtnerVersionRequest(Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class ObtnerVersionRequestBody {
        
        public ObtnerVersionRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ObtnerVersionResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ObtnerVersionResponse", Namespace="http://www.dbnet.cl", Order=0)]
        public Proyect_Apdayc.SGRDA_PDF.ObtnerVersionResponseBody Body;
        
        public ObtnerVersionResponse() {
        }
        
        public ObtnerVersionResponse(Proyect_Apdayc.SGRDA_PDF.ObtnerVersionResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.dbnet.cl")]
    public partial class ObtnerVersionResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ObtnerVersionResult;
        
        public ObtnerVersionResponseBody() {
        }
        
        public ObtnerVersionResponseBody(string ObtnerVersionResult) {
            this.ObtnerVersionResult = ObtnerVersionResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface consultaDocumentosPeruSoapChannel : Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class consultaDocumentosPeruSoapClient : System.ServiceModel.ClientBase<Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap>, Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap {
        
        public consultaDocumentosPeruSoapClient() {
        }
        
        public consultaDocumentosPeruSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public consultaDocumentosPeruSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public consultaDocumentosPeruSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public consultaDocumentosPeruSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaResponse Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap.setRegistraPeticionDescarga(Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequest request) {
            return base.Channel.setRegistraPeticionDescarga(request);
        }
        
        public bool setRegistraPeticionDescarga(Proyect_Apdayc.SGRDA_PDF.ArrayOfString lstDocs, string codiEmpr, string ruc, string user, string guid, string ruta, string correo, string rutaWeb) {
            Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequest inValue = new Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequestBody();
            inValue.Body.lstDocs = lstDocs;
            inValue.Body.codiEmpr = codiEmpr;
            inValue.Body.ruc = ruc;
            inValue.Body.user = user;
            inValue.Body.guid = guid;
            inValue.Body.ruta = ruta;
            inValue.Body.correo = correo;
            inValue.Body.rutaWeb = rutaWeb;
            Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaResponse retVal = ((Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap)(this)).setRegistraPeticionDescarga(inValue);
            return retVal.Body.setRegistraPeticionDescargaResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaResponse> Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap.setRegistraPeticionDescargaAsync(Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequest request) {
            return base.Channel.setRegistraPeticionDescargaAsync(request);
        }
        
        public System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaResponse> setRegistraPeticionDescargaAsync(Proyect_Apdayc.SGRDA_PDF.ArrayOfString lstDocs, string codiEmpr, string ruc, string user, string guid, string ruta, string correo, string rutaWeb) {
            Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequest inValue = new Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_PDF.setRegistraPeticionDescargaRequestBody();
            inValue.Body.lstDocs = lstDocs;
            inValue.Body.codiEmpr = codiEmpr;
            inValue.Body.ruc = ruc;
            inValue.Body.user = user;
            inValue.Body.guid = guid;
            inValue.Body.ruta = ruta;
            inValue.Body.correo = correo;
            inValue.Body.rutaWeb = rutaWeb;
            return ((Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap)(this)).setRegistraPeticionDescargaAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFResponse Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap.getDocumentoPDF(Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequest request) {
            return base.Channel.getDocumentoPDF(request);
        }
        
        public Proyect_Apdayc.SGRDA_PDF.ArrayOfString getDocumentoPDF(string _ruttEmpr, string _folioDTE, string _tipoDTE, string _serieInte, string _fechaDTE, string _monTotal) {
            Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequest inValue = new Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequestBody();
            inValue.Body._ruttEmpr = _ruttEmpr;
            inValue.Body._folioDTE = _folioDTE;
            inValue.Body._tipoDTE = _tipoDTE;
            inValue.Body._serieInte = _serieInte;
            inValue.Body._fechaDTE = _fechaDTE;
            inValue.Body._monTotal = _monTotal;
            Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFResponse retVal = ((Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap)(this)).getDocumentoPDF(inValue);
            return retVal.Body.getDocumentoPDFResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFResponse> Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap.getDocumentoPDFAsync(Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequest request) {
            return base.Channel.getDocumentoPDFAsync(request);
        }
        
        public System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFResponse> getDocumentoPDFAsync(string _ruttEmpr, string _folioDTE, string _tipoDTE, string _serieInte, string _fechaDTE, string _monTotal) {
            Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequest inValue = new Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_PDF.getDocumentoPDFRequestBody();
            inValue.Body._ruttEmpr = _ruttEmpr;
            inValue.Body._folioDTE = _folioDTE;
            inValue.Body._tipoDTE = _tipoDTE;
            inValue.Body._serieInte = _serieInte;
            inValue.Body._fechaDTE = _fechaDTE;
            inValue.Body._monTotal = _monTotal;
            return ((Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap)(this)).getDocumentoPDFAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLResponse Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap.getDocumentoXML(Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequest request) {
            return base.Channel.getDocumentoXML(request);
        }
        
        public Proyect_Apdayc.SGRDA_PDF.ArrayOfString getDocumentoXML(string _ruttEmpr, string _folioDTE, string _tipoDTE, string _serieInte, string _fechaDTE, string _monTotal) {
            Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequest inValue = new Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequestBody();
            inValue.Body._ruttEmpr = _ruttEmpr;
            inValue.Body._folioDTE = _folioDTE;
            inValue.Body._tipoDTE = _tipoDTE;
            inValue.Body._serieInte = _serieInte;
            inValue.Body._fechaDTE = _fechaDTE;
            inValue.Body._monTotal = _monTotal;
            Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLResponse retVal = ((Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap)(this)).getDocumentoXML(inValue);
            return retVal.Body.getDocumentoXMLResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLResponse> Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap.getDocumentoXMLAsync(Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequest request) {
            return base.Channel.getDocumentoXMLAsync(request);
        }
        
        public System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLResponse> getDocumentoXMLAsync(string _ruttEmpr, string _folioDTE, string _tipoDTE, string _serieInte, string _fechaDTE, string _monTotal) {
            Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequest inValue = new Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_PDF.getDocumentoXMLRequestBody();
            inValue.Body._ruttEmpr = _ruttEmpr;
            inValue.Body._folioDTE = _folioDTE;
            inValue.Body._tipoDTE = _tipoDTE;
            inValue.Body._serieInte = _serieInte;
            inValue.Body._fechaDTE = _fechaDTE;
            inValue.Body._monTotal = _monTotal;
            return ((Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap)(this)).getDocumentoXMLAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRResponse Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap.getDocumentoCDR(Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequest request) {
            return base.Channel.getDocumentoCDR(request);
        }
        
        public Proyect_Apdayc.SGRDA_PDF.ArrayOfString getDocumentoCDR(string _ruttEmpr, string _folioDTE, string _tipoDTE, string _serieInte, string _fechaDTE, string _monTotal) {
            Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequest inValue = new Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequestBody();
            inValue.Body._ruttEmpr = _ruttEmpr;
            inValue.Body._folioDTE = _folioDTE;
            inValue.Body._tipoDTE = _tipoDTE;
            inValue.Body._serieInte = _serieInte;
            inValue.Body._fechaDTE = _fechaDTE;
            inValue.Body._monTotal = _monTotal;
            Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRResponse retVal = ((Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap)(this)).getDocumentoCDR(inValue);
            return retVal.Body.getDocumentoCDRResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRResponse> Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap.getDocumentoCDRAsync(Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequest request) {
            return base.Channel.getDocumentoCDRAsync(request);
        }
        
        public System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRResponse> getDocumentoCDRAsync(string _ruttEmpr, string _folioDTE, string _tipoDTE, string _serieInte, string _fechaDTE, string _monTotal) {
            Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequest inValue = new Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_PDF.getDocumentoCDRRequestBody();
            inValue.Body._ruttEmpr = _ruttEmpr;
            inValue.Body._folioDTE = _folioDTE;
            inValue.Body._tipoDTE = _tipoDTE;
            inValue.Body._serieInte = _serieInte;
            inValue.Body._fechaDTE = _fechaDTE;
            inValue.Body._monTotal = _monTotal;
            return ((Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap)(this)).getDocumentoCDRAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Proyect_Apdayc.SGRDA_PDF.ObtnerVersionResponse Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap.ObtnerVersion(Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequest request) {
            return base.Channel.ObtnerVersion(request);
        }
        
        public string ObtnerVersion() {
            Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequest inValue = new Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequestBody();
            Proyect_Apdayc.SGRDA_PDF.ObtnerVersionResponse retVal = ((Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap)(this)).ObtnerVersion(inValue);
            return retVal.Body.ObtnerVersionResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.ObtnerVersionResponse> Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap.ObtnerVersionAsync(Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequest request) {
            return base.Channel.ObtnerVersionAsync(request);
        }
        
        public System.Threading.Tasks.Task<Proyect_Apdayc.SGRDA_PDF.ObtnerVersionResponse> ObtnerVersionAsync() {
            Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequest inValue = new Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequest();
            inValue.Body = new Proyect_Apdayc.SGRDA_PDF.ObtnerVersionRequestBody();
            return ((Proyect_Apdayc.SGRDA_PDF.consultaDocumentosPeruSoap)(this)).ObtnerVersionAsync(inValue);
        }
    }
}
