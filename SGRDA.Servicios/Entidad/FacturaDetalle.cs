using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SGRDA.Servicios.Entidad
{
    [DataContract]
    public class FacturaDetalle
    {

        [DataMember]
        public string Item { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Cantidad { get; set; }
        [DataMember]
        public decimal SubTotal { get; set; }
       

    }
}