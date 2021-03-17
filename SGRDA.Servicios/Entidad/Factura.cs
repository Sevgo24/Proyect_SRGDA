using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SGRDA.Servicios.Entidad
{
    [DataContract]
    public class Factura
    {
        public Factura() {
            this.Detalle = new List<FacturaDetalle>();
        }

        [DataMember]
        public string RUM { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Local { get; set; }
        [DataMember]
        public string RUC { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public string NumFact { get; set; }
        [DataMember]
        public decimal Total { get; set; }
        [DataMember]
        public List<FacturaDetalle> Detalle { get; set; }
        [DataMember]
        public string TotalLetras { get; set; }
        [DataMember]
        public string TipoFactura { get; set; }
        [DataMember]
        public Nullable<DateTime> FechaEmision { get; set; }
    }
}
