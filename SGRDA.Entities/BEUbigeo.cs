using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SGRDA.Entities
{
    public partial class BEUbigeo : Paginacion
    {
        public decimal ID_UBIGEO { get; set; }
        public string NOMBRE_UBIGEO { get; set; }
    }
}
