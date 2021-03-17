using SGRDA.DA.Empadronamiento;
using SGRDA.Entities.Empadronamiento;
using SGRDA.Entities.Reporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRDA.BL.Empadronamiento
{
    public class BLMatrizEmpadronamiento
    {
        public List<BEMatrizEmpadronamiento> ObtenerLista_Matriz_EMPADRONAMIENTO(string anio, string mes, int ID_OFICINA, int oficina_id, string TIPO_PAGO,int LIC_ID, int pagina, int cantRegxPag)
        {
            return new DAMatrizEmpadronamiento().ObtenerLista_Matriz_EMPADRONAMIENTO(anio, mes, ID_OFICINA, oficina_id, TIPO_PAGO, LIC_ID, pagina, cantRegxPag);
        }
        public List<Be_BecsEspeciales> ListarAnios()
        {
            return new DAMatrizEmpadronamiento().ListarAnios();
        }
        public List<Be_BecsEspeciales> ListarMeses(int anio)
        {
            return new DAMatrizEmpadronamiento().ListarMeses(anio);
        }
        public List<BETabla_Comision> Lista_Tabla_Comision()
        {
            return new DAMatrizEmpadronamiento().Lista_Tabla_Comision();
        }
        public List<BETabla_Comision> Listar_Combo_RangoComision()
        {
            return new DAMatrizEmpadronamiento().Listar_Combo_RangoComision();
        }
        public int Insertar_RangoComision(int ID_RANGO, decimal MONTO_DESDE, decimal MONTO_HASTA, decimal PORCENTAJE, string User)
        {
            return new DAMatrizEmpadronamiento().Insertar_RangoComision(ID_RANGO, MONTO_DESDE, MONTO_HASTA, PORCENTAJE, User);
        }
        public int Delete_RangoComision(int ID_COMISION, string User)
        {
            return new DAMatrizEmpadronamiento().Delete_RangoComision(ID_COMISION, User);
        }
        public int Insertar_TipoRango(string user)
        {
            return new DAMatrizEmpadronamiento().Insertar_TipoRango(user);
        }

        public int Desactivar_TipoRango(string user)
        {
            return new DAMatrizEmpadronamiento().Desactivar_TipoRango(user);
        }

        public List<BEMatrizEmpadronamiento> ObtenerLista_Modulo_EMPADRONAMIENTO(int anio, int mes, int ID_OFICINA
            , int oficina_id, string TIPO_PAGO, int LIC_ID, int pagina, int cantRegxPag)
        {
            return new DAMatrizEmpadronamiento().ObtenerLista_Modulo_EMPADRONAMIENTO(anio, mes, ID_OFICINA, oficina_id, TIPO_PAGO, LIC_ID, pagina, cantRegxPag);
        }
                        
    }
}
