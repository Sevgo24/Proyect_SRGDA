using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEAgenteTreeview
    {
        public int cod { get; set; }
        public string text { get; set; }
        public int? ManagerID { get; set; }

        public IList<BEAgenteTreeview> items { get; set; }

        public BEAgenteTreeview()
        {
            items = new List<BEAgenteTreeview>();
        }

    }
}
