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
    public class SEOficina :ISEOficina
    {


        public List<Oficina> ListarOficinaService(string owner,int Oficina)
        {
            BLOffices oficina = new BLOffices();
            List<Oficina> lista = new List<Oficina>();
            var items = oficina.ListarOffActivasSERVICE(owner, Oficina);
            if (items != null)
            {
                items.ForEach(x =>
                {
                    lista.Add(new Oficina { Codigo = x.OFF_ID, CodigoPadre = x.SOFF_ID, Nombre = x.OFF_NAME });
                });
            }
            return lista;

        }
        public List<Oficina> ListarOficina(string owner)
        {
            BLOffices oficina = new BLOffices();
            List<Oficina> lista = new List<Oficina>();
            var items = oficina.ListarOffActivas(owner);
            if (items != null)
            {
                items.ForEach(x =>
                {
                    lista.Add(new Oficina { Codigo = x.OFF_ID, CodigoPadre = x.SOFF_ID, Nombre = x.OFF_NAME });
                });
            }
            return lista;

        }

        public Oficina ObtenerOficina(string owner,decimal codigo)
        {

            Oficina oficina = null;
            BLOffices oficinaBl = new BLOffices();

            var dato = oficinaBl.Obtener(owner, codigo);
            if (dato != null)
            {
                oficina = new Oficina { Codigo = dato.OFF_ID, CodigoPadre = dato.SOFF_ID, Nombre=dato.OFF_NAME };
            }


            return oficina;

        }
    }
}