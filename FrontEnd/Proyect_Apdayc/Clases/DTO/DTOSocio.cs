using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyect_Apdayc.Clases.DTO
{
    public class DTOSocio
    {


        /// <summary>
        /// propiedad para el autocompletar
        /// </summary>
        public string value { get; set; }

        public decimal Codigo { get; set; }
        public string TipoPersona { get; set; }
        public decimal TipoDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string NumDocumento { get; set; }
        public string RazonSocial { get; set; }
        public bool EsUsuDerecho { get; set; }
        public bool EsGrupoEmp { get; set; }
        public bool EsRecaudador { get; set; }
        public bool EsEmpleador { get; set; }
        public bool EsProveedor { get; set; }
        public bool EsAsociacion { get; set; }
        public string TipoEntidad { get; set; }
        public string Nombres { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string NombreComercial { get; set; }

        /// <summary>
        /// Determina si esta registrado en BD o no
        /// </summary>
        public bool EnBD { get; set; }
        public bool Activo { get; set; }

        public string UsuarioCrea { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaCrea { get; set; }
        public DateTime? FechaModifica { get; set; }
        public decimal IdBps { get; set; }

        public Boolean Bps_Verificado { get; set; }
        public string Verificado { get; set; }
        public string UsuarioVerificacion { get; set; }
        public DateTime FechaVerificacion { get; set; }

        /// <summary>
        /// Cobros - Recibo
        /// </summary>
        public decimal IdRecibo { get; set; }
        public decimal ReciboBase { get; set; }
        public decimal ReciboImpuesto { get; set; }
        public decimal ReciboRetenciaones { get; set; }
        public decimal ReciboTotal { get; set; }

        public int valida { get; set; }

        //        oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, bps.OWNER);
        //oDataBase.AddInParameter(oDbComand, "@ENT_TYPE", DbType.String, bps.ENT_TYPE);
        //oDataBase.AddInParameter(oDbComand, "@BPS_NAME", DbType.String, bps.BPS_NAME);
        //oDataBase.AddInParameter(oDbComand, "@BPS_FIRST_NAME", DbType.String, bps.BPS_FIRST_NAME);
        //oDataBase.AddInParameter(oDbComand, "@BPS_FATH_SURNAME", DbType.String, bps.BPS_FATH_SURNAME);
        //oDataBase.AddInParameter(oDbComand, "@BPS_MOTH_SURNAME", DbType.String, bps.BPS_MOTH_SURNAME);
        //oDataBase.AddInParameter(oDbComand, "@TAXT_ID", DbType.Decimal, bps.TAXT_ID);
        //oDataBase.AddInParameter(oDbComand, "@TAX_ID", DbType.String, bps.TAX_ID);
        //oDataBase.AddInParameter(oDbComand, "@BPS_GROUP", DbType.String, bps.BPS_GROUP);
        //oDataBase.AddInParameter(oDbComand, "@ENDS", DbType.DateTime, bps.ENDS);
        //oDataBase.AddInParameter(oDbComand, "@BPS_INT", DbType.DateTime, bps.BPS_INT);
        //oDataBase.AddInParameter(oDbComand, "@BPS_INT_N", DbType.String, bps.BPS_INT_N);
        //oDataBase.AddInParameter(oDbComand, "@BPS_USER", DbType.String, bps.BPS_USER);
        //oDataBase.AddInParameter(oDbComand, "@BPS_COLLECTOR", DbType.String, bps.BPS_COLLECTOR);
        //oDataBase.AddInParameter(oDbComand, "@BPS_ASSOCIATION", DbType.String, bps.BPS_ASSOCIATION);
        //oDataBase.AddInParameter(oDbComand, "@BPS_EMPLOYEE", DbType.String, bps.BPS_EMPLOYEE);
        //oDataBase.AddInParameter(oDbComand, "@BPS_SUPPLIER", DbType.String, bps.BPS_SUPPLIER);




    }
}