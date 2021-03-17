using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;


namespace SGRDA.BL
{
    public class BLREF_DIV_SUBTYPE
    {
        public List<BEREF_DIV_SUBTYPE> REF_DIV_SUBTYPE_Get()
        {
            return new DAREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_Get();
        }

        public List<BEREF_DIV_SUBTYPE> REF_DIV_SUBTYPE_GET_by_DAD_STYPE(decimal DAD_STYPE)
        {
            return new DAREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_GET_by_DAD_STYPE(DAD_STYPE);
        }

        public List<BEREF_DIV_SUBTYPE> REF_DIV_SUBTYPE_GET_by_DAD_ID(decimal DAD_ID)
        {
            return new DAREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_GET_by_DAD_ID(DAD_ID);
        }

        public List<BEREF_DIV_SUBTYPE> REF_DIV_SUBTYPE_Page(string param, int pagina, int cantRegxPag)
        {
            return new DAREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_Page(param, pagina, cantRegxPag);
        }

        public List<BEREF_DIV_SUBTYPE> REF_DIV_SUBTYPE_DAD_BELONGS_GET_by_DAD_ID(decimal DAD_ID)
        {
            return new DAREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_DAD_BELONGS_GET_by_DAD_ID(DAD_ID);
        }

        public bool REF_DIV_SUBTYPE_Ins(BEREF_DIV_SUBTYPE en)
        {
            return new DAREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_Ins(en);
        }

        public bool REF_DIV_SUBTYPE_Upd(BEREF_DIV_SUBTYPE en)
        {
            return new DAREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_Upd(en);
        }

        public bool REF_DIV_SUBTYPE_Del(string OWNER, decimal DAD_STYPE)
        {
            return new DAREF_DIV_SUBTYPE().REF_DIV_SUBTYPE_Del(OWNER, DAD_STYPE);
        }

        public List<BEREF_DIV_SUBTYPE> BUSCAR_UBIGEO(decimal codigo, string ubigeo)
        {
            return new DAREF_DIV_SUBTYPE().USP_BUSCAR_UBIGEO(codigo, ubigeo);
        }

        public List<BEREF_DIV_SUBTYPE> Listar(string owner, decimal id, int pagina, int cantRegxPag)
        {
            return new DAREF_DIV_SUBTYPE().Listar(owner, id, pagina, cantRegxPag);
        }

        public int Eliminar(BEREF_DIV_SUBTYPE division)
        {
            return new DAREF_DIV_SUBTYPE().Eliminar(division);
        }

        public int insertar(BEREF_DIV_SUBTYPE division)
        {
            return new DAREF_DIV_SUBTYPE().insertar(division);
        }

        public List<BEREF_DIV_SUBTYPE> ListarSubdivisionDep(string owner, decimal id)
        {
            return new DAREF_DIV_SUBTYPE().ListarSubdivisionDep(owner, id);
        }

        public List<BEREF_DIV_SUBTYPE> ListarSubdivision(string owner, decimal id)
        {
            return new DAREF_DIV_SUBTYPE().ListarSubdivision(owner, id);
        }

        public int ObtenerXAbrev(BEREF_DIV_SUBTYPE subdivision)
        {
            return new DAREF_DIV_SUBTYPE().ObtenerXAbrev(subdivision);
        }

        public List<BETreeview> ArbolSubReporte(string owner, decimal id)
        {
            return new DAREF_DIV_SUBTYPE().ArbolSubReporte(owner,id);
        }

    }
}
