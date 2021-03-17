using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Utility
{
    public class BEResponse<T>
    {
        public BEError Error { get; set; }
        public T Detalle { get; set; }
    }
}
