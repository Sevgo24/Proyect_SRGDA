using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public partial class USUARIOS : Paginacion
    {
        public int USUA_ICODIGO_USUARIO { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre")]
        [DisplayName("Nombre")]
        public string USUA_VNOMBRE_USUARIO { get; set; }

        [Required(ErrorMessage = "Ingrese Apellido Paterno")]
        [DisplayName("Apellido Paterno")]
        public string USUA_VAPELLIDO_PATERNO_USUARIO { get; set; }

        [Required(ErrorMessage = "Ingrese Apellido Materno")]
        [DisplayName("Apellido Materno")]
        public string USUA_VAPELLIDO_MATERNO_USUARIO { get; set; }

        [Required(ErrorMessage = "Ingrese el Nombre Completo")]
        [DisplayName("Nombre Completo")]
        public string NOMBRE_COMPLETO_USUARIO { get; set; }

        [Required(ErrorMessage = "Seleccione un Rol")]
        [DisplayName("Rol")]
        public string NOMBRE_ROL { get; set; }

        //[Required(ErrorMessage = "Ingrese el Usuario de Red")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese el Usuario de Red")]
        [DisplayName("Usuario de Red")]
        public string USUA_VUSUARIO_RED_USUARIO { get; set; }

        [Required(ErrorMessage = "Ingrese su Contraseña")]
        [DisplayName("Password")]
        public string USUA_VPASSWORD_USUARIO { get; set; }

        [Required(ErrorMessage = "Seleccione un Rol")]
        [DisplayName("Rol")]
        public int ROL_ICODIGO_ROL { get; set; }

        public int TER_ICODIGO_TERRITORIO { get; set; }
        public int USUA_IUSUARIO_CREA { get; set; }

        [Required(ErrorMessage = "Seleccione Activo")]
        [DisplayName("Activo")]
        public Boolean USUA_CACTIVO_USUARIO { get; set; }

        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime LOG_DATE_UPDATE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USER_UPDATE { get; set; }
        public string BNK_ID { get; set; }
    }
}
