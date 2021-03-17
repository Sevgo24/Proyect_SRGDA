using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA.Comision;
using SGRDA.Entities.Comision;

namespace SGRDA.BL.Comision
{
    public class BLComisionEscala
    {
        public decimal Insertar(BEComisionEscala comision, List<BEComisionEscalaRango> ListaRangoDetalle)
        {
            decimal idGeneradoCabecera = 0;
            idGeneradoCabecera = new DAComisionEscala().Insertar(comision);
            if (idGeneradoCabecera > 0)
            {
                foreach (var item in ListaRangoDetalle)
                {
                    item.SET_ID = idGeneradoCabecera;
                    int idGeneradoDetalle = new DAComisionEscalaRango().Insertar(item);
                }
            }
            return idGeneradoCabecera;
        }

        public int Actualizar(BEComisionEscala comision, List<BEComisionEscalaRango> ListaRangoDetalle)
        {
            return new DAComisionEscala().Actualizar(comision);
        }

        public BEComisionEscala ObtenerComision(string owner, decimal id)
        {
            BEComisionEscala comisionEscala = new BEComisionEscala();
            comisionEscala = new DAComisionEscala().ObtenerComision(owner, id);
            comisionEscala.ListaComisionRango = new DAComisionEscalaRango().ObtenerListaDetalle(owner, id);
            return comisionEscala;
        }

    }
}
