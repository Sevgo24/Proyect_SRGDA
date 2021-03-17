using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLLicenciaDivision
    {
        public decimal ObtenerUbigeoEstablecimiento(decimal idEstablecimiento)
        {
            return new DALicenciaDivision().ObtenerUbigeoEstablecimiento(idEstablecimiento);
        }

        public List<BELicenciaDivision> ObtenerDivisionesXModalidad(decimal idModalidad)
        {
            return new DALicenciaDivision().ObtenerDivisionesXModalidad(idModalidad);
        }

        public int ValidarDivsionXUbigeo(decimal idDivision, decimal ubigeo_est)
        {
            return new DALicenciaDivision().ValidarDivsionXUbigeo(idDivision, ubigeo_est);
        }

        public int Insertar(string owner, decimal idLic, decimal idDivision, string usuario)
        {
            return new DALicenciaDivision().Insertar(owner, idLic, idDivision, usuario);
        }

        public int Eliminar(string owner, decimal id, decimal idLic, decimal idDivision, string usuario)
        {
            return new DALicenciaDivision().Eliminar(owner, id, idLic, idDivision, usuario);
        }
        public List<BELicenciaDivision> ListarDivisionLicencia(string owner, decimal idLicencia)
        {
            List<BELicenciaDivision> Listar = new List<BELicenciaDivision>();
            Listar = new DALicenciaDivision().ListarDivisionLicencia(owner, idLicencia);
            return Listar;            
        }

        public int ActualizarEstado(string owner, decimal id, decimal idLic, decimal idDivision, string usuario, decimal indicador)
        {
            return new DALicenciaDivision().ActualizarEstado(owner, id, idLic, idDivision, usuario, indicador);
        }

    }
}
