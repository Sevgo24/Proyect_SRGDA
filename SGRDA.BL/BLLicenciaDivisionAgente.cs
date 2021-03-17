using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;


namespace SGRDA.BL
{
    public class BLLicenciaDivisionAgente
    {
        public int Insertar(BELicenciaDivisionAgente entidad)
        {
            return new DALicenciaDivisionAgente().Insertar(entidad);
        }

        //public int Eliminar(BELicenciaDivisionAgente entidad)
        //{
        //    return new DALicenciaDivisionAgente().Eliminar(entidad);
        //}
        
        //public int Actualizar(BELicenciaDivisionAgente entidad)
        //{
        //    return new DALicenciaDivisionAgente().Actualizar(entidad);
        //}

        public List<BELicenciaDivisionAgente> Listar(string owner,decimal idLicencia)
        {
            return new DALicenciaDivisionAgente().Listar(owner,idLicencia);
        }

        public List<BELicenciaDivisionAgente> Obtener_Agente_X_Division(BELicenciaDivisionAgente DivisionAgente)
        {
            return new DALicenciaDivisionAgente().Obtener_Agente_X_Division(DivisionAgente);
        }


        public int ActualizarEstado(BELicenciaDivisionAgente entidad)
        {
            return new DALicenciaDivisionAgente().ActualizarEstado(entidad);
        }
    }
}
