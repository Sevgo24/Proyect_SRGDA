using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;

namespace SGRDA.BL
{
    public class BLLicenciaLocalidad
    {
        public List<BELicenciaLocalidad> ListarLicenciaLocalidad(string owner, decimal idLic)
        {
            return new DALicenciaLocalidad().ListarLicenciaLocalidad(owner, idLic);
        }
        public BELicenciaLocalidad ObtenerLicLocalidadXCod(string owner, decimal idLicenciaLocalidad)
        {
            return new DALicenciaLocalidad().ObtenerLicLocalidadXCod(owner, idLicenciaLocalidad);
        }
        public int Insertar(string owner, decimal CodigoLicencia, string CodigoTipoAforo, decimal CodigoTipoLocalidad, string Funcion, string Color, decimal Ticket, decimal PrecVenta, decimal ImporteBruto, decimal Impuesto, decimal ImporteNeto, string UsuarioCrea)
        {
            return new DALicenciaLocalidad().Insertar(owner, CodigoLicencia, CodigoTipoAforo, CodigoTipoLocalidad, Funcion, Color, Ticket, PrecVenta, ImporteBruto, Impuesto, ImporteNeto, UsuarioCrea);
        }
        public int Actualizar(string owner, decimal idLicenciaLocalidad, string CodigoTipoAforo, decimal CodigoTipoLocalidad, string Funcion, string Color, decimal Ticket, decimal PrecVenta, decimal ImporteBruto, decimal Impuesto, decimal ImporteNeto, string UsuarioModifica)
        {
            return new DALicenciaLocalidad().Actualizar(owner, idLicenciaLocalidad, CodigoTipoAforo, CodigoTipoLocalidad, Funcion, Color, Ticket, PrecVenta, ImporteBruto, Impuesto, ImporteNeto, UsuarioModifica);
        }
        public int Activar(string owner, decimal idLicenciaLocalidad)
        {
            return new DALicenciaLocalidad().Activar(owner, idLicenciaLocalidad);
        }
        public int Eliminar(string owner, decimal idLicenciaLocalidad)
        {
            return new DALicenciaLocalidad().Eliminar(owner, idLicenciaLocalidad);
        }
        public List<BELicenciaLocalidad> ListarLocalidad(string owner, decimal idLic)
        {
            return new DALicenciaLocalidad().ListarLocalidad(owner,idLic);
        }
        public int InsertarLocalidad(BELicenciaLocalidad localidad)
        {
            return new DALicenciaLocalidad().InsertarLocalidad(localidad);
        }
        public int ActualizarLocalidad(BELicenciaLocalidad localidad)
        {
            return new DALicenciaLocalidad().ActualizarLocalidad(localidad);
        }

        public BELicenciaLocalidadConteo listarLicenciaConteo(decimal licid, string tipo)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DALicenciaLocalidad().listarLicenciaConteo(owner,licid,tipo);

        }



        //public List<BELicenciaLocalidadConteo> ListarMatrizLocalidad(string owner, decimal idLic)
        //{
        //    return new DALicenciaLocalidad().ListarMatrizLocalidad(owner, idLic);
        //}

        //public bool InsertarMatrizLocalidadesXML(List<BELicenciaLocalidadConteo> listaMatriz, string owner)
        //{
        //    bool exito = false;
        //    string xmlMatriz = string.Empty;
        //    xmlMatriz = Utility.Util.SerializarEntity(listaMatriz);
        //    exito = new DALicenciaLocalidad().InsertarMatrizLocalidadesXML(xmlMatriz, owner);
        //    return exito;
        //}

        //public bool ActualizarMatrizLocalidadesXML(List<BELicenciaLocalidadConteo> listaMatriz, string owner)
        //{
        //    bool exito = false;
        //    string xmlMatriz = string.Empty;
        //    xmlMatriz = Utility.Util.SerializarEntity(listaMatriz);
        //    exito = new DALicenciaLocalidad().ActualizarMatrizLocalidadesXML(xmlMatriz, owner);
        //    return exito;
        //}

        //public int ObtenerCantMatLocActivas(string owner,decimal idLic)
        //{
        //    return new DALicenciaLocalidad().ObtenerCantMatLocActivas(owner,idLic);
        //}

        //public int EliminarMatrizLocalidades(string owner, decimal idLic)
        //{
        //    return new DALicenciaLocalidad().EliminarMatrizLocalidades(owner, idLic);
        //}
    }
}
