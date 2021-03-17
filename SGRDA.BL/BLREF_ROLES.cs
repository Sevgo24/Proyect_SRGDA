using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREF_ROLES
    {
        public List<BEREF_ROLES> Get_REF_ROLES()
        {
            return new DAREF_ROLES().Get_REF_ROLES();
        }

        public List<BEREF_ROLES> REF_ROLES_GET_by_ROL_ID(decimal ROL_ID)
        {
            return new DAREF_ROLES().REF_ROLES_GET_by_ROL_ID(ROL_ID);
        }

        public List<BEREF_ROLES> REF_ROLES_Page(string param, int st, int pagina, int cantRegxPag)
        {
            return new DAREF_ROLES().REF_ROLES_Page(param, st, pagina, cantRegxPag);
        }

        public bool REF_ROLES_Ins(BEREF_ROLES en)
        {
            return new DAREF_ROLES().REF_ROLES_Ins(en);
        }

        public bool REF_ROLES_Upd(BEREF_ROLES en)
        {
            return new DAREF_ROLES().REF_ROLES_Upd(en);
        }

        public bool REF_ROLES_Del(decimal ROL_ID)
        {
            return new DAREF_ROLES().REF_ROLES_Del(ROL_ID);
        }
        //public BEREF_ROLES Obtener(decimal ROL_ID)
        //{
        //    return new DAREF_ROLES().Obtener(ROL_ID);
        //}

        /// <summary>
        ///  Valida si el socio que se va a editar puede ser modificado por la oficina del usuario logeado
        /// </summary>
        /// <param name="idOficina"></param>
        /// <param name="idSocio"></param>
        /// <returns></returns>
        public bool TienePermiso(decimal idOficina, decimal idSocio)
        {
            return new DAREF_ROLES().TienePermiso(idOficina, idSocio);
        }
        /// <summary>
        ///  Valida si tiene permiso para editar al socio recaudador  por la oficina del usuario logeado
        /// </summary>
        /// <param name="idOficina"></param>
        /// <param name="idSocio"></param>
        /// <returns></returns>
        public bool TienePermisoUsuRec(decimal idOficina,decimal idSocio)
        {
            return new DAREF_ROLES().TienePermisoUsuRec(idOficina,idSocio);
        }
        /// <summary>
        /// Valida si tiene permiso para editar una licencia  por la oficina del usuario logeado
        /// </summary>
        /// <param name="idOficinaLogin"></param>
        /// <param name="idLicenciaEdit"></param>
        /// <returns></returns>
        public bool TienePermisoEditarLic(decimal idOficinaLogin, decimal idLicenciaEdit)
        {
            return new DAREF_ROLES().TienePermisoEditarLic(idOficinaLogin, idLicenciaEdit);
        }
        /// <summary>
        /// Valida si tiene permiso para REGISTRAR una licencia  por la oficina del usuario logeado
        /// </summary>
        /// <param name="idOficinaLogin"></param>
        /// <param name="idLicenciaEdit"></param>
        /// <returns></returns>
        public bool TienePermisoRegistrarLic(decimal idOficinaLogin, decimal idEstablecimientoSel)
        {
            return new DAREF_ROLES().TienePermisoRegistrarLic(idOficinaLogin, idEstablecimientoSel);
        }
    }
}
