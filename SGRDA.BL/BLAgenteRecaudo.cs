using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using SGRDA.Entities.Reporte;

namespace SGRDA.BL
{
    public class BLAgenteRecaudo
    {
        public decimal Insertar(BEAgenteRecaudo agente)
        {
            decimal idAgenteGenerado = 0;
            idAgenteGenerado = new DAAgenteRecaudo().Insertar(agente);

            #region OBSERVACION
            if (agente.ListaObservacion != null)
            {
                foreach (var observacion in agente.ListaObservacion)
                {
                    var codigoGenObs = new DAObservationGral().InsertarObsGrl(observacion);
                    var result = new DAObservacionAgenteRecaudo().InsertarObs(new BEObservationAgenteRecaudo
                    {
                        OWNER = observacion.OWNER,
                        OBS_ID = codigoGenObs,
                        BPS_ID = agente.BPS_ID,
                        LOG_USER_CREAT = observacion.LOG_USER_CREAT
                    });
                }
            }
            #endregion

            #region DIRECCION
            if (agente.ListaDireccion != null)
            {
                foreach (var direccion in agente.ListaDireccion)
                {
                    var codigoGenAdd = new DADirecciones().Insertar(direccion);
                    var result = new DADireccionAgenteRecaudo().Insertar(new BEDireccionAgenteRecaudo
                    {
                        BPS_ID = agente.BPS_ID,
                        ADD_ID = codigoGenAdd,
                        OWNER = agente.OWNER,
                        LOG_USER_CREAT = agente.LOG_USER_CREAT
                    });
                }
            }
            #endregion

            return idAgenteGenerado;
        }

        public decimal Actualizar(BEAgenteRecaudo agente,
                                  List<BEDireccion> dirEliminar, List<BEDireccion> listDirActivar,
                                  List<BEObservationGral> obsEliminar, List<BEObservationGral> listObsActivar)
        {
            decimal upd = 0;
            upd = new DAAgenteRecaudo().Actualizar(agente);

            #region DIRECCION
            DADirecciones proxyDir = new DADirecciones();
            if (agente.ListaDireccion != null)
            {
                foreach (var direccion in agente.ListaDireccion)
                {   ///verifica si  no existe la direccion para el socio
                    ///si no existe se registra y asocia la nueva direcion
                    BEDireccion proxyDirObtener = proxyDir.ObtenerDirAgenteRecaudo(agente.OWNER, direccion.ADD_ID, agente.BPS_ID, direccion.ENT_ID);
                    if (proxyDirObtener == null)
                    {
                        var codigoGenAdd = proxyDir.Insertar(direccion);
                        var result = new DADireccionAgenteRecaudo().Insertar(new BEDireccionAgenteRecaudo
                        {
                            BPS_ID = agente.BPS_ID,
                            ADD_ID = codigoGenAdd,
                            OWNER = agente.OWNER,
                            LOG_USER_CREAT = agente.LOG_USER_UPDAT
                        });
                    }
                    else
                    {   ///sino  solo se actualiza la informacion de la direccion                         
                        var result = proxyDir.Update(direccion);
                    }
                }
            }

            // se elimina las direcciones
            if (dirEliminar != null)
            {
                foreach (var item in dirEliminar)
                {
                    proxyDir.Eliminar(agente.OWNER, item.ADD_ID, agente.LOG_USER_UPDAT);
                }
            }
            // activa las direcciones
            if (listDirActivar != null)
            {
                foreach (var item in listDirActivar)
                {
                    proxyDir.Activar(agente.OWNER, item.ADD_ID, agente.LOG_USER_UPDAT);
                }
            }

            #endregion

            #region OBSERVACION
            ///logica de negocio para actualizar Observaciones
            DAObservationGral proxyObs = new DAObservationGral();
            DAObservationOff proxyObsOff = new DAObservationOff();
            if (agente.ListaObservacion != null)
            {
                foreach (var observacion in agente.ListaObservacion)
                {
                    ///verifica si  no existe la Observacion para el socio
                    ///si no existe se registra y asocia la nueva Observacion
                    BEObservationGral proxyObsObtener = proxyObs.ObtenerObsAgenteRecaudo(observacion.OWNER, observacion.OBS_ID, agente.BPS_ID);
                    if (proxyObsObtener == null)
                    {
                        var codigoGenObs = proxyObs.InsertarObsGrl(observacion);
                        var result = new DAObservacionAgenteRecaudo().InsertarObs(new BEObservationAgenteRecaudo
                        {
                            OWNER = observacion.OWNER,
                            OBS_ID = codigoGenObs,
                            BPS_ID = agente.BPS_ID,
                            LOG_USER_CREAT = observacion.LOG_USER_CREAT
                        });
                    }
                    else
                    {
                        observacion.LOG_USER_UPDATE = observacion.LOG_USER_CREAT;
                        var result = proxyObs.Update(observacion);
                    }
                }
            }

            /// se elimina las direcciones
            if (obsEliminar != null)
            {
                foreach (var item in obsEliminar)
                {
                    proxyObs.Eliminar(agente.OWNER, item.OBS_ID, agente.LOG_USER_UPDAT);
                }
            }
            /// activa las direcciones
            if (listObsActivar != null)
            {
                foreach (var item in listObsActivar)
                {
                    proxyObs.Activar(agente.OWNER, item.OBS_ID, agente.LOG_USER_UPDAT);
                }
            }
            #endregion
            //ObtenerDirAgenteRecaudo

            return upd;
        }

        public List<BEAgenteRecaudo> ListarAgenteRecaudoXDivision(string owner, decimal idOficina, decimal idDivision, string agenteRecaudador, int pagina, int cantRegxPag)
        {
            return new DAAgenteRecaudo().ListarAgenteRecaudoXDivision(owner, idOficina, idDivision, agenteRecaudador, pagina, cantRegxPag);
        }

        public BEAgenteRecaudo Obtener(BEAgenteRecaudo agente)
        {
            var agenteRecaudo = new DAAgenteRecaudo().Obtener(agente);
            if (agenteRecaudo != null)
            {
                agenteRecaudo.ListaObservacion = new DAObservationGral().ObservacionXAgenteRecaudo(agente.OWNER, agenteRecaudo.BPS_ID);
                agenteRecaudo.ListaDireccion = new DADirecciones().DireccionXAgenteRecaudo(agente.OWNER, agenteRecaudo.BPS_ID);
            }
            return agenteRecaudo;
        }

        public List<BEAgenteRecaudo> Obtener_Division_Modalidad_Agente(string usuario)
        {
            return new DAAgenteRecaudo().Obtener_Division_Modalidad_Agente(usuario);
        }

        public BEAgenteRecaudo ObtenerAgente(BEAgenteRecaudo agente)
        {
            var agenteRecaudo = new DAAgenteRecaudo().Obtener(agente);
            return agenteRecaudo;
        }

        public List<BEAgenteRecaudo> ListarAgenteRecaudoXDivisionObligatorio(string owner, decimal idOficina, decimal idDivision, string agenteRecaudador)
        {
            return new DAAgenteRecaudo().ListarAgenteRecaudoXDivisionObligatorio(owner, idOficina, idDivision, agenteRecaudador);
        }
    }
}
