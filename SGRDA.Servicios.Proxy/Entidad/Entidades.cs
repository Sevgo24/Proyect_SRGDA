using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Servicios.Proxy.Entidad
{
    using System.Runtime.Serialization;

 
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Factura", Namespace = "http://schemas.datacontract.org/2004/07/SGRDA.Servicios.Entidad")]
    public partial class Factura : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private SGRDA.Servicios.Proxy.Entidad.FacturaDetalle[] DetalleField;

        private string DireccionField;

        private System.DateTime FechaField;

        private string LocalField;

        private string NumFactField;

        private string RUCField;

        private string RUMField;

        private string RazonSocialField;

        private decimal TotalField;

        private string TotalLetrasField;

        private string TipoFacturaField;

        private Nullable<DateTime> FechaEmisionField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public SGRDA.Servicios.Proxy.Entidad.FacturaDetalle[] Detalle
        {
            get
            {
                return this.DetalleField;
            }
            set
            {
                this.DetalleField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Direccion
        {
            get
            {
                return this.DireccionField;
            }
            set
            {
                this.DireccionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Fecha
        {
            get
            {
                return this.FechaField;
            }
            set
            {
                this.FechaField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Local
        {
            get
            {
                return this.LocalField;
            }
            set
            {
                this.LocalField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NumFact
        {
            get
            {
                return this.NumFactField;
            }
            set
            {
                this.NumFactField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RUC
        {
            get
            {
                return this.RUCField;
            }
            set
            {
                this.RUCField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RUM
        {
            get
            {
                return this.RUMField;
            }
            set
            {
                this.RUMField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RazonSocial
        {
            get
            {
                return this.RazonSocialField;
            }
            set
            {
                this.RazonSocialField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Total
        {
            get
            {
                return this.TotalField;
            }
            set
            {
                this.TotalField = value;
            }
        }
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TotalLetras
        {
            get
            {
                return this.TotalLetrasField;
            }
            set
            {
                this.TotalLetrasField = value;
            }
        }
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TipoFactura
        {
            get
            {
                return this.TipoFacturaField;
            }
            set
            {
                this.TipoFacturaField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public Nullable<DateTime> FechaEmision
        {
            get
            {
                return this.FechaEmisionField;
            }
            set
            {
                this.FechaEmisionField = value;
            }
        }
        
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FacturaDetalle", Namespace = "http://schemas.datacontract.org/2004/07/SGRDA.Servicios.Entidad")]
    public partial class FacturaDetalle : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string CantidadField;

        private string DescripcionField;

        private string ItemField;

        private decimal SubTotalField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Cantidad
        {
            get
            {
                return this.CantidadField;
            }
            set
            {
                this.CantidadField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Descripcion
        {
            get
            {
                return this.DescripcionField;
            }
            set
            {
                this.DescripcionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Item
        {
            get
            {
                return this.ItemField;
            }
            set
            {
                this.ItemField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal SubTotal
        {
            get
            {
                return this.SubTotalField;
            }
            set
            {
                this.SubTotalField = value;
            }
        }
    }


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PreImpresion", Namespace = "http://schemas.datacontract.org/2004/07/SGRDA.Servicios.Entidad")]
    public partial class PreImpresion : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string ESTADOField;

        private System.Nullable<System.DateTime> FECHA_IMPField;

        private System.DateTime FECHA_SELField;

        private string HOSTNAMEField;

        private int IDField;

        private decimal ID_DOCUMENTOField;

        private decimal ID_LOCALField;

        private decimal ID_USUARIOField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ESTADO
        {
            get
            {
                return this.ESTADOField;
            }
            set
            {
                this.ESTADOField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> FECHA_IMP
        {
            get
            {
                return this.FECHA_IMPField;
            }
            set
            {
                this.FECHA_IMPField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime FECHA_SEL
        {
            get
            {
                return this.FECHA_SELField;
            }
            set
            {
                this.FECHA_SELField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HOSTNAME
        {
            get
            {
                return this.HOSTNAMEField;
            }
            set
            {
                this.HOSTNAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ID_DOCUMENTO
        {
            get
            {
                return this.ID_DOCUMENTOField;
            }
            set
            {
                this.ID_DOCUMENTOField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ID_LOCAL
        {
            get
            {
                return this.ID_LOCALField;
            }
            set
            {
                this.ID_LOCALField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ID_USUARIO
        {
            get
            {
                return this.ID_USUARIOField;
            }
            set
            {
                this.ID_USUARIOField = value;
            }
        }
    }


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Oficina", Namespace = "http://schemas.datacontract.org/2004/07/SGRDA.Servicios.Entidad")]
    public partial class Oficina : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private decimal CodigoField;

        private decimal CodigoPadreField;

        private string NombreField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Codigo
        {
            get
            {
                return this.CodigoField;
            }
            set
            {
                this.CodigoField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal CodigoPadre
        {
            get
            {
                return this.CodigoPadreField;
            }
            set
            {
                this.CodigoPadreField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nombre
        {
            get
            {
                return this.NombreField;
            }
            set
            {
                this.NombreField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WF_Radio", Namespace = "http://schemas.datacontract.org/2004/07/SGRDA.Servicios.Entidad")]
    public partial class WF_Radio : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int CANT_FACT_DEUDAField;

        private decimal ID_BSPField;

        private decimal ID_LICField;

        private string OWNERField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CANT_FACT_DEUDA
        {
            get
            {
                return this.CANT_FACT_DEUDAField;
            }
            set
            {
                this.CANT_FACT_DEUDAField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ID_BSP
        {
            get
            {
                return this.ID_BSPField;
            }
            set
            {
                this.ID_BSPField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ID_LIC
        {
            get
            {
                return this.ID_LICField;
            }
            set
            {
                this.ID_LICField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OWNER
        {
            get
            {
                return this.OWNERField;
            }
            set
            {
                this.OWNERField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Artista", Namespace = "http://schemas.datacontract.org/2004/07/SGRDA.Servicios.Entidad")]
    public partial class Artista : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string ART_COMPLETEField;

        private decimal COD_ARTIST_SQField;

        private string ESTADOField;

        private string FIRST_NAMEField;

        private string IP_NAMEField;

        private System.Nullable<System.DateTime> LOG_DATE_CREATField;

        private System.Nullable<System.DateTime> LOG_DATE_UPDATEField;

        private string LOG_USER_CREATField;

        private string LOG_USER_UPDATEField;

        private string NAMEField;

        private string OWNERField;

        private SGRDA.Servicios.Proxy.Entidad.Artista[] listaArtistaField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ART_COMPLETE
        {
            get
            {
                return this.ART_COMPLETEField;
            }
            set
            {
                this.ART_COMPLETEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal COD_ARTIST_SQ
        {
            get
            {
                return this.COD_ARTIST_SQField;
            }
            set
            {
                this.COD_ARTIST_SQField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ESTADO
        {
            get
            {
                return this.ESTADOField;
            }
            set
            {
                this.ESTADOField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FIRST_NAME
        {
            get
            {
                return this.FIRST_NAMEField;
            }
            set
            {
                this.FIRST_NAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IP_NAME
        {
            get
            {
                return this.IP_NAMEField;
            }
            set
            {
                this.IP_NAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LOG_DATE_CREAT
        {
            get
            {
                return this.LOG_DATE_CREATField;
            }
            set
            {
                this.LOG_DATE_CREATField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LOG_DATE_UPDATE
        {
            get
            {
                return this.LOG_DATE_UPDATEField;
            }
            set
            {
                this.LOG_DATE_UPDATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LOG_USER_CREAT
        {
            get
            {
                return this.LOG_USER_CREATField;
            }
            set
            {
                this.LOG_USER_CREATField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LOG_USER_UPDATE
        {
            get
            {
                return this.LOG_USER_UPDATEField;
            }
            set
            {
                this.LOG_USER_UPDATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NAME
        {
            get
            {
                return this.NAMEField;
            }
            set
            {
                this.NAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OWNER
        {
            get
            {
                return this.OWNERField;
            }
            set
            {
                this.OWNERField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public SGRDA.Servicios.Proxy.Entidad.Artista[] listaArtista
        {
            get
            {
                return this.listaArtistaField;
            }
            set
            {
                this.listaArtistaField = value;
            }
        }
    }


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WF_TvCable", Namespace = "http://schemas.datacontract.org/2004/07/SGRDA.Servicios.Entidad")]
    public partial class WF_TvCable : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int CANT_FACT_DEUDAField;

        private decimal ID_BSPField;

        private decimal ID_LICField;

        private string OWNERField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CANT_FACT_DEUDA
        {
            get
            {
                return this.CANT_FACT_DEUDAField;
            }
            set
            {
                this.CANT_FACT_DEUDAField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ID_BSP
        {
            get
            {
                return this.ID_BSPField;
            }
            set
            {
                this.ID_BSPField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ID_LIC
        {
            get
            {
                return this.ID_LICField;
            }
            set
            {
                this.ID_LICField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OWNER
        {
            get
            {
                return this.OWNERField;
            }
            set
            {
                this.OWNERField = value;
            }
        }
    }


}
