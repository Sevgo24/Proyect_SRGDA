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
    public class Resultado
    {
        [DataMember]
        public string result { get; set; }
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string mensaje { get; set; }
    }
}