
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
   public class Be_BecsEspeciales
    {

        public string Mes               {get;set;}
        public decimal BEC { get;set;}
        public string RazonSocial { get;set;}
        public string Territorio { get;set;}
        public string TD                {get;set;}
        public string SERIE             {get;set;}
        public decimal Correlativo       {get;set;}
        public string FechaEmision      {get;set;}
        public string FechaDeposito     {get;set;}
        public string FechaCreacionBec { get;set;}
        public string FechaConfirmacion    {get;set;}
        public string BANCO { get;set;}
        public string CUENTA { get;set;}
        public string Voucher { get;set;}
        public decimal Deposito         {get;set;}
        public decimal Afecto           {get;set;}
        public string Rubro             {get;set;}
        public decimal TotalDeposito    {get;set;}
        public decimal TotalAfecto      {get;set;}
        public string FechaInicio       {get;set;}
        public string FechaFin          {get;set;}
        public decimal ACCOUNTANT_YEAR { get; set; }
        public int ACCOUNTANT_MONTH { get; set; }
        public string NOMBRE_MES { get; set; }
    }
}
