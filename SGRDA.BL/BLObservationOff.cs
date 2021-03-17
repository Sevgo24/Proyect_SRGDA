using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{

    public class BLObservationOff
    {
        public int InsertarObsOff(BEObservationOff obsOff,BEObservationGral obsGral)
        {
            int id = new DAObservationGral().InsertarObsGrl(obsGral);

            if (id != 0)
            {
                obsOff.OBS_ID = id;
                return new DAObservationOff().InsertarObsOff(obsOff);
            }
            else
            {
                return 0;
            }

        }


    }

}
