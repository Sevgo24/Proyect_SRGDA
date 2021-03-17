using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SGRDA.Entities
{
    public partial class BEOffices
    {
        public IList<BEOffices> BEREC_OFFICE { get; set; }

        public BEOffices()
        {
            BEREC_OFFICE = new List<BEOffices>();
        }

        public BEOffices(IDataReader Reader)
        {

            OFF_ID = Convert.ToInt32(Reader["OFF_ID"]);
            OFF_NAME = Convert.ToString(Reader["OFF_NAME"]);
            HQ_IND = Convert.ToString(Reader["HQ_IND"]);
            SOFF_ID = Convert.ToInt32(Reader["SOFF_ID"]);
            ADD_ID = Convert.ToInt32(Reader["ADD_ID"]);
        }

        public BEOffices(IDataReader Reader, int flag)
        {
            OFF_ID = Convert.ToInt32(Reader["OFF_ID"]);            
            HQ_IND = Convert.ToString(Reader["HQ_IND"]).ToUpper() == "Y" ? "Principal" : "Dependiente";
            SOFF_ID = Convert.ToInt32(Reader["SOFF_ID"]);
            ADD_ID = Convert.ToInt32(Reader["ADD_ID"]);
            OFF_NAME = Convert.ToString(Reader["OFF_NAME"]);
            ADDRESS = Convert.ToString(Reader["ADDRESS"]) + " " + Convert.ToString(Reader["NUMBER"]);
            if (string.IsNullOrEmpty(Convert.ToString(Reader["ENDS"])))
                ENDSDES = "ACTIVO";
            else
                ENDSDES = "INACTIVO";

            TotalVirtual = flag;
        }


    }
}
