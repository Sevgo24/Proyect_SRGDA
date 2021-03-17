using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Empadronamiento
{
    public class BEDetalleEmpadronamiento
    {
        public int INV_ID                   {get;set;}
        public string TE                    {get;set;}
        public string TIPO                  {get;set;}
        public string SERIE                 {get;set;}
        public int NRO                      {get;set;}
        public string FECHA_EMISION         {get;set;}
        public string FECHA_CANCELACION     {get;set;}
        public string IDENT                 {get;set;}
        public string NRO_IDENT             {get;set;}
        public string SOCIO                 {get;set;}
        public string MONEDA                {get;set;}
        public decimal FACTURADO            {get;set;}
        public decimal COBRADO              {get;set;}
        public decimal SALDO                {get;set;}
        public int NC                       {get;set;}
        public int ESTADO                   {get;set;}
        public string ESTADO_SUNAT          {get;set;}
        public string PERIODO               {get;set; }
    }
}
