using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLTarifaTest
    {
        public BEREC_RATES_GRAL Obtener(string owner, decimal idRegla)
        {
            var tarifa = new DATarifaTest().Obtener(owner, idRegla);
            if (tarifa != null)
            {
                tarifa.Regla = new DATarifaRegla().ListarReglaTarifaTest(owner, idRegla);
                tarifa.Caracteristica = new DATarifaTest().ListarCaracteristica(owner, idRegla);
                tarifa.Test = new DATarifaTest().Listar(owner, idRegla);                
            }
            return tarifa;
        }

        public int IdCaracteristicaOtro(string owner)
        {
            return new DATarifaTest().IdCaracteristicaOtro(owner);
        }

        public decimal ObtenerTarifaActual(string owner, decimal idTarifa,DateTime fecha)
        {
            return new DATarifaTest().ObtenerTarifaActual(owner, idTarifa, fecha);
         
            
        }
        public int ObtieneTarifaDescuentoEspecial(decimal  RATE_ID)
        {
            return new DATarifaTest().ObtieneTarifaDescuentoEspecial(RATE_ID);
        }


    }
}
