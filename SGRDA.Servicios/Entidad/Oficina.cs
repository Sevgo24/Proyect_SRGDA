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
    public class Oficina
    {
        public Oficina()
        {
           
        }
        [DataMember]
        public decimal Codigo { get; set; }
        [DataMember]
        public decimal CodigoPadre { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        
        
    }
}
