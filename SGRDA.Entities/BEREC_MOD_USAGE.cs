﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGRDA.Entities
{
    public partial class BEREC_MOD_USAGE:Paginacion
    {
        public string OWNER { get; set; }
        public string MOD_USAGE { get; set; }
        public string MOD_DUSAGE { get; set; }
        public string LOG_USER_CREAT { get; set; }
        public string LOG_USET_UPDAT { get; set; }
        public DateTime? ENDS { get; set; }
        public DateTime LOG_DATE_CREAT { get; set; }
        public DateTime? LOG_DATE_UPDATE { get; set; }

    }
}
