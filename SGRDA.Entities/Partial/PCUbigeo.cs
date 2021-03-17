using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace SGRDA.Entities
{
    public partial class BEUbigeo
    {
        public List<BEUbigeo> Ubigeo { get; set; }
        public BEUbigeo() {}

        public BEUbigeo(IDataReader Reader)
        {
            ID_UBIGEO = Convert.ToDecimal(Reader["VALUE"]);
            NOMBRE_UBIGEO = Convert.ToString(Reader["TEXT"]);
        }

        public BEUbigeo(IDataReader Reader, int flag)
        {
            ID_UBIGEO = Convert.ToDecimal(Reader["VALUE"]);
            NOMBRE_UBIGEO = Convert.ToString(Reader["TEXT"]);

            TotalVirtual = flag;
        }
    }
}
