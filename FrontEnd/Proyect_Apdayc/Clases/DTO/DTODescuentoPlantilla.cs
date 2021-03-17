using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTODescuentoPlantilla
    {
        public Int32 Orden { get; set; }
        public int Origen { get; set; }
        public string DesOrigen { get; set; }

        /// <summary>
        /// ID PK de tabla REC_LIC_DISCOUNTS
        /// </summary>
        public decimal IdLicDesc { get; set; }
        /// <summary>
        /// ID correspondiente al descuento
        /// </summary>
        public decimal Id { get; set; }
        public string Tipo { get; set; }
        public string Descuento { get; set; }
        public string Formato { get; set; }
        public decimal Valor { get; set; }
        public decimal Base { get; set; }
        public decimal Cuenta { get; set; }
        /// <summary>
        ///  Descuento aplicable al NETO [N] o BASE [B]
        /// </summary>
        public string Aplicable { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

        /// <summary>
        /// True si el valor del signo es suma , en todo caso restará al total
        /// </summary>
        public bool esSuma { get; set; }


        public bool esAutomatico { get; set; }
        public decimal LicId { get; set; }
        /// <summary>
        /// AQUI SE GUARDA EL ID DE LA PLANTILLA SI TUVIERA
        /// </summary>
        public decimal TEMP_ID_DSC { get; set; }
    }
}