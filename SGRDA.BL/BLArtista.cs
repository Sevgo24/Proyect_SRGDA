using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLArtista
    {
        public List<BEArtista> ListaArtistaPaginada(string owner, int flag, string nombre, int pagina, int cantRegxPag)
        {
            return new DAArtista().ListaArtistaPaginada(owner, flag, nombre, pagina, cantRegxPag);
        }
        public int Eliminar(string id, string owner, string usuDel,decimal SHOW_ID)
        {
            return new DAArtista().Eliminar(id, owner, usuDel, SHOW_ID);
        }

        public int Solicitud_Eliminar_Activar(string id, string Observacion, string usuDel, decimal SHOW_ID,int tipo,decimal Artist_ID)
        {
            return new DAArtista().Solicitud_Eliminar_Activar(id, Observacion, usuDel, SHOW_ID, tipo, Artist_ID);    
        }

        public int Activar(string id, string owner, string usuDel,decimal SHOW_ID)
        {
            return new DAArtista().Activar(id, owner, usuDel, SHOW_ID);
        }
        public int Prioridad(string id, string owner)
        {
            return new DAArtista().Prioridad(id, owner);
        }
        public decimal Insertar(decimal idShow, string nameIp, string ppalArtist, string owner, string usuario,string name)
        {
            return new DAArtista().Insertar(idShow, nameIp, ppalArtist, owner, usuario, name);
        }
        public decimal InsertarSolicitud(decimal idShow, string nameIp, string ppalArtist, string owner, string usuario, string name,string Observacion)
        {
            return new DAArtista().InsertarSolicitud(idShow, nameIp, ppalArtist, owner, usuario, name, Observacion);
        }
        public BEArtista Obtener(decimal id, string owner)
        {
            return new DAArtista().Obtener(id,  owner);
        }
        public List<BEArtista> ListaArtistaOracle(string owner, int flag, string nombre, int pagina, int cantRegxPag)
        {
            //return new DAArtista().ListaArtistaSQL(owner, flag, nombre, pagina, cantRegxPag);
            return new DAArtista().ListaArtistaOracle(owner, 0, nombre, pagina, cantRegxPag);
        }

        public int InsertarGeneral(string owner, string name, string ipname, string firstname, string artcomplete, string user) {

            return new DAArtista().InsertarGeneral(owner, name, ipname, firstname, artcomplete, user);
        }
        public BEArtista ObtenerXNombreCompleto(string nombre, string owner)
        {
            return new DAArtista().ObtenerXNombreCompleto(nombre, owner);
        }

        public BEArtista ObtenerArtistaOracle(decimal codigo)
        {
            string owner=GlobalVars.Global.OWNER;
            return new DAArtista().ObtenerArtistaOracle(owner, codigo);
        }
        public List<BEArtista> Listar_Artista_x_Show(decimal codshow)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAArtista().Listar_Artista_x_Show(owner, codshow);
        }

        public List<BEArtista> Listar_Artista_NO_CODSGS_PAGEJSON(string owner,string nombre, decimal LIC_ID,string SHOW_NAME, int pagina, int cantRegxPag)
        {
            return new DAArtista().Listar_Artista_NO_CODSGS_PAGEJSON(pagina, cantRegxPag,owner,nombre ,LIC_ID,SHOW_NAME);
        }

        public int ActualizarArtistaSGS(decimal COD_SGS, decimal codArtist)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAArtista().ActualizarArtistaSGS(owner, COD_SGS, codArtist);
        }

        public BEArtista ObtenerArtista(decimal id, string owner)
        {
            return new DAArtista().ObtenerArtista(id, owner);
        }

        #region Planilla Automatica x Artista

        public int InsertaPlanillaAutomatica(decimal LIC_ID,decimal ARTIST_ID,string LOG_USER_CREAT)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAArtista().InsertaPlanillaAutomatica(LIC_ID, ARTIST_ID, LOG_USER_CREAT, owner);
        }

        public int ValidamodEspectBaile(decimal LIC_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAArtista().ValidamodEspectBaile(owner, LIC_ID);
        }
        #endregion

        #region ValidaModalidad_GRABADA_EN_VIVO

        public int ValidaModalidadGrabadaenVIVO(decimal LIC_ID,decimal SHOW_ID)
        {
            return new DAArtista().ValidaModalidadGrabadaenVIVO(LIC_ID,SHOW_ID);
        }
        #endregion


        public List<BEArtista> Listar_Solicitud_Artista(decimal Lic_Id)
        {
            return new DAArtista().Listar_Solicitud_Artista(Lic_Id);
        }

        public int Aprobar_Solicitud_Artista(decimal Show_Id, decimal Artist_Id)
        {
            return new DAArtista().Aprobar_Solicitud_Artista(Show_Id, Artist_Id);
        }

        public int Rechazar_Solicitud_Artista(decimal Show_Id, decimal Artist_Id)
        {
            return new DAArtista().Rechazar_Solicitud_Artista(Show_Id, Artist_Id);
        }
        

    }
}
