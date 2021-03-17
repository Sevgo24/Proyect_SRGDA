using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLREF_DIVISIONES
    {
        public List<BEREF_DIVISIONES> Get_REF_DIVISIONES()
        {
            return new DAREF_DIVISIONES().Get_REF_DIVISIONES();
        }
        public List<BEREF_DIVISIONES> GET_REF_DIVISIONES_BY_DAD_TYPE(string DAD_TYPE, string OWNER)
        {
            return new DAREF_DIVISIONES().GET_REF_DIVISIONES_BY_DAD_TYPE(DAD_TYPE, OWNER);
        }

        public List<BEREF_DIVISIONES> REF_DIVISIONES_GET_by_DAD_ID(decimal DAD_ID)
        {
            return new DAREF_DIVISIONES().REF_DIVISIONES_GET_by_DAD_ID(DAD_ID);
        }

        public List<BEREF_DIVISIONES> REF_DIVISIONES_Page(string param, int pagina, int cantRegxPag)
        {
            return new DAREF_DIVISIONES().REF_DIVISIONES_Page(param, pagina, cantRegxPag);
        }

        public bool REF_DIVISIONES_Ins(BEREF_DIVISIONES en)
        {
            return new DAREF_DIVISIONES().REF_DIVISIONES_Ins(en);
        }

        public bool REF_DIVISIONES_Upd(BEREF_DIVISIONES en)
        {
            return new DAREF_DIVISIONES().REF_DIVISIONES_Upd(en);
        }

        public bool REF_DIVISIONES_Del(decimal DAD_ID)
        {
            return new DAREF_DIVISIONES().REF_DIVISIONES_Del(DAD_ID);
        }

        public List<BEREF_DIVISIONES> ListarDivisonesTipo(string owner, string tipo)
        {
            return new DAREF_DIVISIONES().ListarTipoDivision(owner, tipo);
        }

        //public List<SocioNegocio> ListarDivisionAdm(string owner, string div, int pagina, int cantRegxPag)
        //{
        //    return new DAREF_DIVISIONES().ListarDivisionAdm(owner,  div,  pagina,  cantRegxPag);
        //} 

        public List<BEREF_DIVISIONES> Listar(string owner, string tipo, string nombre,int estado, int pagina, int cantRegxPag)
        {
            return new DAREF_DIVISIONES().Listar(owner,  tipo,  nombre, estado, pagina,  cantRegxPag);
        }

        public BEREF_DIVISIONES Obtener(string owner, decimal id)
        {
            return new DAREF_DIVISIONES().Obtener(owner, id);
        }

        public int Actualizar(BEREF_DIVISIONES valores)
        {
            return new DAREF_DIVISIONES().Actualizar(valores);
        }

        public int Eliminar(BEREF_DIVISIONES valores)
        {
            return new DAREF_DIVISIONES().Eliminar(valores);
        }

        public List<BEREF_DIVISIONES> ListarDivisionesFiscales(string owner)
        {
            return new DAREF_DIVISIONES().ListarDivisionesFiscales(owner);
        }

        public List<BEREF_DIVISIONES> ListarDivisioneXtipo(string owner, string tipo)
        {
            return new DAREF_DIVISIONES().ListarDivisioneXtipo(owner, tipo);
        }
    }
}
