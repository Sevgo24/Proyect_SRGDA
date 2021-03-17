using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLLoteTrabajo
    {
        public List<BELoteTrabajo> ListarLoteTrabajo(string owner, decimal Idcampania)
        {
            return new DALoteTrabajo().ListarLoteTrabajo(owner, Idcampania);
        }

        public BELoteTrabajo ObtenerLoteTrabajo(string owner, decimal IdLote)
        {
            return new DALoteTrabajo().ObtenerLoteTrabajo(owner, IdLote);
        }

        public List<BELoteTrabajo> ListaLoteAgente(string owner, decimal IdCampania)
        {
            return new DALoteTrabajo().ListaLoteAgente(owner, IdCampania);
        }
    }
}
