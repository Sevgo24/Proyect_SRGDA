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
    public class Artista
    {
        [DataMember]
        public string OWNER { get; set; }
        [DataMember]
        public decimal COD_ARTIST_SQ { get; set; }
        [DataMember]
        public string NAME { get; set; }
        [DataMember]
        public string IP_NAME { get; set; }
        [DataMember]
        public string FIRST_NAME { get; set; }
        [DataMember]
        public string ART_COMPLETE { get; set; }
        [DataMember]
        public string ESTADO { get; set; }
        [DataMember]
        public DateTime? LOG_DATE_CREAT { get; set; }
        [DataMember]
        public DateTime? LOG_DATE_UPDATE { get; set; }
        [DataMember]
        public string LOG_USER_CREAT { get; set; }
        [DataMember]
        public string LOG_USER_UPDATE { get; set; }
        [DataMember]
        public List<Artista> listaArtista = null;

        public Artista()
        {
            listaArtista = new List<Artista>();
        }

    }
}