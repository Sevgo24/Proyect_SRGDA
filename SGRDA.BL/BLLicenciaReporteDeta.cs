using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLLicenciaReporteDeta
    {
        public int Insertar(BELicenciaReporteDeta entidad)
        {
            return new DALicenciaReporteDeta().Insertar(entidad);
        }

        public int Actualizar(BELicenciaReporteDeta entidad)
        {
            return new DALicenciaReporteDeta().Actualizar(entidad);
        }

        public List<BELicenciaReporteDeta> Listar(string Owner, decimal IdCab)
        {
            return new DALicenciaReporteDeta().Listar(Owner, IdCab);
        }

        public BELicenciaReporteDeta Obtener(string Owner, decimal IdDet, decimal Idcab)
        {
            return new DALicenciaReporteDeta().Obtener(Owner, IdDet, Idcab);
        }

        public int Eliminar(BELicenciaReporteDeta entidad)
        {
            return new DALicenciaReporteDeta().Eliminar(entidad);
        }

        public int Activar(string owner, decimal IdDeta, decimal IdRepCab,string usuModi)
        {
            return new DALicenciaReporteDeta().Activar( owner,  IdDeta,  IdRepCab, usuModi);
        }

        public int ValidaLicenciaFactCancelada(decimal LIC_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicenciaReporteDeta().ValidaLicenciaFactCancelada(LIC_ID);
        }
        
        public int ValidaLicenciaFactValorizada(decimal LIC_ID)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicenciaReporteDeta().ValidaLicenciaFactValorizada(LIC_ID);
        }
        public List<BELicenciaReporte> ListarPlaneamientoxLicenciaOpcion(decimal CodigoLicencia,int Opcion)
        {
            //string owner = GlobalVars.Global.OWNER;
            return new DALicenciaReporteDeta().ListarPlaneamientoxLicenciaOpcion(CodigoLicencia,Opcion);
        }
    }
}
