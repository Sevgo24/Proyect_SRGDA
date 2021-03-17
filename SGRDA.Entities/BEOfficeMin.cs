using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities
{
    public class BEOfficeMin
    {

        public int cod { get; set; }
        public string text { get; set; }
        public int? ManagerID { get; set; }

        public IList<BEOfficeMin> items { get; set; }

        public BEOfficeMin()
        {
            items = new List<BEOfficeMin>();
        }
    }
}
