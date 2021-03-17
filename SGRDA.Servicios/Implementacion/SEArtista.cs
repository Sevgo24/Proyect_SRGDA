using SGRDA.BL;
using SGRDA.Entities;
using SGRDA.Servicios.Contrato;
using SGRDA.Servicios.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGRDA.Servicios.Implementacion
{
    public class SEArtista : ISEArtista
    {
        public List<Artista> ListaArtistaOracle(string owner, int flag, string nombre,
                                                int pagina, int cantRegxPag)
        {
            List<Artista> listaArtista = new List<Artista>();
            Artista artista = new Artista();

            List<BEArtista> BLlistaArtista = new List<BEArtista>();
            BLlistaArtista = new BLArtista().ListaArtistaOracle(owner, flag, nombre, pagina, cantRegxPag);

            foreach (var item in BLlistaArtista)
            {
                artista = new Artista();
                artista.COD_ARTIST_SQ = item.COD_ARTIST_SQ;

                if (!string.IsNullOrEmpty(item.NAME))
                    artista.NAME = item.NAME;

                if (!string.IsNullOrEmpty(item.IP_NAME))
                    artista.IP_NAME = item.IP_NAME;

                if (!string.IsNullOrEmpty(item.FIRST_NAME))
                    artista.FIRST_NAME = item.FIRST_NAME;

                if (!string.IsNullOrEmpty(item.ART_COMPLETE))
                    artista.ART_COMPLETE = item.ART_COMPLETE;
                artista.ESTADO = item.ESTADO;
                listaArtista.Add(artista);
            }
            return listaArtista;
        }

    }
}