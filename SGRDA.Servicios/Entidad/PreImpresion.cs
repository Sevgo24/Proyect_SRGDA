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
    public class PreImpresion
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public decimal ID_DOCUMENTO { get; set; }
        [DataMember]
        public decimal ID_USUARIO { get; set; }
        [DataMember]
        public decimal ID_LOCAL { get; set; }
        [DataMember]
        public DateTime FECHA_SEL { get; set; }
        [DataMember]
        public string ESTADO { get; set; }
        [DataMember]
        public DateTime? FECHA_IMP { get; set; }
        [DataMember]
        public string HOSTNAME { get; set; }
    }
}