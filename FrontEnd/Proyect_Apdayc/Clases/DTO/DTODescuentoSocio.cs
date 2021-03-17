using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTODescuentoSocio
    {

        //Variable para poder ordenar las lsitas temporales
        public int Orden { get; set;}
        
        //
        public decimal DISC_ID {get;set;}

        public decimal Tipo { get; set; }
        public string Descuento { get; set; }
        public decimal DISC_VALUE { get; set; }
        public decimal DISC_PERC { get; set;  }
        public string esSuma { get; set; }
        public int DISC_ACC { get; set; }
        public char DISC_AUT { get; set;}
        public bool Activo { get; set;}
        public string OBSERVACION { get; set;}

        //Indicador que confirma si Viene de la Base de Datos
        public bool EnBD { get; set;}
    }
}