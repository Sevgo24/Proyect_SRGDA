using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLUSUARIOS
    {
        public List<USUARIOS> USUARIOS_spBuscarLogin(USUARIOS en)
        {
            return new DAUSUARIOS().USUARIOS_spBuscarLogin(en.USUA_VUSUARIO_RED_USUARIO, en.USUA_VPASSWORD_USUARIO);
        }

        public List<USUARIOS> USUARIOS_spBuscar(int usua_icodigo_usuario, string usua_vnombre_usuario, string usua_vapellido_paterno_usuario, string usua_vapellido_materno_usuario, char usua_cactivo_usuario)
        {
            return new DAUSUARIOS().USUARIOS_spBuscar(usua_icodigo_usuario, usua_vnombre_usuario, usua_vapellido_paterno_usuario,usua_vapellido_materno_usuario,usua_cactivo_usuario);
        }

        public List<USUARIOS> usp_listar_Usuarios_by_codigo(int usua_icodigo_usuario)
        {
            return new DAUSUARIOS().usp_listar_Usuarios_by_codigo(usua_icodigo_usuario);
        }

        public int usp_Upd_Usuarios(USUARIOS user)
        {
            return new DAUSUARIOS().usp_Upd_Usuarios(user);
        }

        public int usp_Ins_Usuarios(USUARIOS user)
        {
            return new DAUSUARIOS().usp_Ins_Usuarios(user);
        }

        public List<USUARIOS> usp_Get_UsuariosPage(string usuario_red, string param, int pagina, int cantRegxPag)
        {
            return new DAUSUARIOS().usp_Get_UsuariosPage(usuario_red, param, pagina, cantRegxPag);
        }

        public int usp_Upd_estado_Usuarios(USUARIOS user)
        {
            return new DAUSUARIOS().usp_Upd_estado_Usuarios(user);
        }
    }
}
