using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BETreeview
    {
         public int cod { get; set; }
        public string text { get; set; }
        public int? ManagerID { get; set; }

        public IList<BETreeview> items { get; set; }

        public BETreeview()
        {
            items = new List<BETreeview>();
        }
    }
}
