using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.Entities.Reporte
{
    public class Be_Bec_Inactivas
    {
        public decimal BEC_ID        {get;set;}
        public decimal LIC_ID        {get;set;}
        public string SOCIO         {get;set;}
        public string SERIE         {get;set;}
        public decimal NRO           {get;set;}
        public string FECHA_EMISION {get;set;}
        public string ESTADO_FACT { get;set; }
        public string FECHA_RECHAZO { get; set; }
        public string MOTIVO { get; set; }
        public string FECHA_INACTIVACION { get; set; }
        public string SITUACION         {get;set;}
        public string BANCO_ORIGEN      {get;set;}
        public string FECHA_DEPOSITO    {get;set;}
        public string NRO_OPERACION     {get;set;}
        public decimal MONTO_DEPOSITO    {get;set;}
        public string BANCO_DESTINO     {get;set;}
        public string CUENTA_BANCARIA   {get;set;}
        public string OFICINA           {get;set;}
        public string DESC_SOLICITUD { get; set; }

    }
}
