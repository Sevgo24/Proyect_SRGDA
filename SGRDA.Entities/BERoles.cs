using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class BERoles : Paginacion
    {
        public Decimal CodigoPerfil { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Boolean Anulado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public int CodigoModulo { get; set; }

        public string ACTIVO { get; set; }
    }
}
