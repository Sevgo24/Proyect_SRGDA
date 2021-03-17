using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLEstadoLicencia
    {
        public List<BEEstadoLicencia> Listar_Page_EstadoLicencia(string parametro, int st, int pagina, int cantRegxPag)
        {
            return new DAEstadoLicencia().Listar_Page_EstadoLicencia(parametro, st, pagina, cantRegxPag);
        }

        public List<BEEstadoLicencia> Obtener(string owner, decimal id)
        {
            return new DAEstadoLicencia().Obtener(owner, id);
        }

        public int Insertar(BEEstadoLicencia ins)
        {            
            var cod = new DAEstadoLicencia().Insertar(ins);

            if (ins.ListaTab != null)
            {
                foreach (var item in ins.ListaTab)
                {
                    item.LICS_ID = ins.LICS_ID;
                    new DALicenciaEstadoTab().Insertar(item);
                }
            }
            return cod;
        }

        public int Actualizar(BEEstadoLicencia tab, List<BELicenciaEstadoTab> tabEliminar, List<BELicenciaEstadoTab> listtabActivar)
        {
            DALicenciaEstadoTab proxyTab = new DALicenciaEstadoTab();
            int codigoGenAdd=0;
            new DAEstadoLicencia().Actualizar(tab);

            if (tab.ListaTab != null)
            {
                foreach (var item in tab.ListaTab)
                {
                    BELicenciaEstadoTab ent = proxyTab.ObtenerEstadoLicenciaTab(item.OWNER, item.antTAB_ID, tab.LICS_ID);
                    if (ent == null)
                    {
                        item.LICS_ID = tab.LICS_ID;
                        codigoGenAdd = proxyTab.Insertar(item);
                    }
                    else if (ent.TAB_ID != item.TAB_ID)
                    {
                        var result = proxyTab.Actualizar(item);
                    }
                }
            }

            if (tabEliminar != null)
            {
                foreach (var item in tabEliminar)
                {
                    proxyTab.Eliminar(tab.OWNER, item.SECUENCIA, tab.LOG_USER_UPDATE);
                }
            }

            if (listtabActivar != null)
            {
                foreach (var item in listtabActivar)
                {
                    proxyTab.Activar(tab.OWNER, item.SECUENCIA, tab.LOG_USER_UPDATE);
                }
            }
            
            return codigoGenAdd;
        }

        public int Eliminar(BEEstadoLicencia del)
        {
            return new DAEstadoLicencia().Eliminar(del);
        }

        public List<BEEstadoLicencia> ListarEstado(string owner)
        {
            return new DAEstadoLicencia().ListarEstado(owner);
        }
    }
}
