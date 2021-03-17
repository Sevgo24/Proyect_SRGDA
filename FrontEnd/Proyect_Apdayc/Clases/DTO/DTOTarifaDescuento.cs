using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOTarifaDescuento
    {
        public decimal Id { get; set; }
        public decimal IdTarifa { get; set; }
        public decimal IdTipoDesc { get; set; }
        public decimal IdDesc { get; set; }
        public string TipoDescripcion { get; set; }
        public string Descripcion { get; set; }
        public string Formato { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Valor { get; set; }
        public string Signo { get; set; }
        public string CuentaContable { get; set; }
        public string Disc_Aut { get; set; }
        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }
    }
}