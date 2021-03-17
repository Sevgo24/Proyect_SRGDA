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
    public class BLOffices
    {
        public List<BEOffices> usp_Get_Rec_Offices_Get_By_Off_Name(string owner,decimal OFF_ID, string param, int estado, int pagina, int cantRegxPag)
        {
            return new DAREC_OFFICES().Usp_Get_Rec_Offices_Get_By_Off_Name(owner, OFF_ID, param, estado, pagina, cantRegxPag);
        }
        public BEOffices OficinaXID(int OFF_ID)
        {
            return new DAREC_OFFICES().OficinaXID(OFF_ID);
        }
        public int Usps_Del_Rec_Offices(BEOffices office)
        {
            return new DAREC_OFFICES().Usps_Del_Rec_Offices(office);
        }

        public List<BETreeview> Usp_Get_Rec_Offices_Get_By_Off_Name_By_Hq_Ind(string owner)
        {
            return new DAREC_OFFICES().Usp_Get_Rec_Offices_Get_By_Off_Name_By_Hq_Ind(owner);
        }

        public int ups_Ins_Rec_Offices(BEOffices office)
        {
            return new DAREC_OFFICES().ups_Ins_Rec_Offices(office);
        }

        public List<BEOffices> Usp_Get_Rec_Offices_By_Offname_Dep(string owner, string param, decimal offId, int pagina, int cantRegxPag)
        {
            return new DAREC_OFFICES().Usp_Get_Rec_Offices_By_Offname_Dep(owner, param, offId, pagina, cantRegxPag);
        }

        public int Usp_Upd_RecOffices_by_OffId_and_SoffId(BEOffices office)
        {
            return new DAREC_OFFICES().Usp_Upd_RecOffices_by_OffId_and_SoffId(office);
        }

        public List<BEObservationGral> ObtenerObsOficina(string owner, int offID, int pagina, int cantRegxPag)
        {
            return new DAREC_OFFICES().ObtenerObsOficina(owner, offID, pagina, cantRegxPag);
        }

        public int InsertarObs(BEObservationOff obsOff, BEObservationGral obsGral)
        {
            int id = new DAObservationGral().InsertarObsGrl(obsGral);
            if (id != 0)
            {
                obsOff.OBS_ID = id;
                return new DAREC_OFFICES().InsertarObsOff(obsOff);
            }
            else
            {
                return 0;
            }
        }

        public List<BEOffices> ListarOffActivas(string owner)
        {
            return new DAREC_OFFICES().ListarOffActivas(owner);
        }
        public List<BEOffices> ListarOffActivasSERVICE(string owner,decimal ID_OFICINA)
        {
            return new DAREC_OFFICES().ListarOffActivasLYRICS(owner, ID_OFICINA);
        }

        public int ObtenerPrincipales(string owner)
        {
            return new DAREC_OFFICES().ObtenerPrincipales(owner);
        }

        public List<BEParametroOff> ListarParametroOFF(string owner, decimal offid, int pagina, int cantRegxPag)
        {
            return new DAREC_OFFICES().ListarParametroOFF(owner, offid, pagina, cantRegxPag);
        }

        public List<BEDocumentoOfi> ListarDocumentoOfi(string owner, decimal offid, int pagina, int cantRegxPag)
        {
            return new DAREC_OFFICES().ListarDocumentoOfi(owner, offid, pagina, cantRegxPag);
        }

        public BETipoNumerador ObtenerTipoNumeracion(string Owner, string NmrType)
        {
            return new DANumeracion().ObtenerTipoNumeracion(Owner, NmrType);
        }

        public BEOffices Obtener(string owner, decimal off_id)
        {
            var objOficina = new DAREC_OFFICES().Obtener(owner, off_id);

            if (objOficina != null)
            {
                objOficina.Observaciones = new DAObservationGral().ObservacionXOficina(owner, off_id);
                objOficina.Parametros = new DAParametroGral().ParametroXOficina(off_id, owner);
                objOficina.Documentos = new DADocumentoGral().DocumentoXOficina(off_id, owner);
                //objOficina.Numeraciones = new DANumeracion().NumeracionXOficina(off_id, owner);
                objOficina.Direcciones = new DADirecciones().DireccionXOficina(off_id, owner);
                objOficina.Oficinas = new DAREC_OFFICES().DependenciaXOficina(off_id, owner);
                objOficina.Agentes = new DAREC_OFFICES().ListarAgente(off_id, owner);
                objOficina.Contacto = new DASocioNegocioOficina().ListarContacto(off_id, owner);
                objOficina.DireccionesHistorial = new DADirecciones().DireccionXOficinaHistorial(off_id, owner);
                //DIvisión Administrativa
                objOficina.DivisionAdm = new DAOficinaDivision().ObtenerDivAdm(off_id, owner);
                objOficina.DivisionAdmGrupoModalidad = new DAOficinaDivisionModalidad().ObtenerListaDivMod(off_id, owner);
                objOficina.DivisionAdmNumerador = new DANumeracionOfi().ObtenerListaDivNumerador(off_id, owner);

            }
            return objOficina;
        }

        public int Insertar(BEOffices office)
        {
            var codOficinaGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codOficinaGen = new DAREC_OFFICES().ups_Ins_Rec_Offices(office);
                #region OBSERVACION
                if (office.Observaciones != null)
                {
                    foreach (var observacion in office.Observaciones)
                    {
                        var codigoGenObs = new DAObservationGral().InsertarObsGrl(observacion);
                        var result = new DAREC_OFFICES().InsertarObsOff(new BEObservationOff
                        {
                            OWNER = office.OWNER,
                            LOG_USER_CREAT = office.LOG_USER_CREAT,
                            OBS_ID = codigoGenObs,
                            OFF_ID = codOficinaGen
                        });
                    }
                }
                #endregion
                #region PARAMETRO
                if (office.Parametros != null)
                {
                    foreach (var parametro in office.Parametros)
                    {
                        var codigoGenParam = new DAParametroGral().Insertar(parametro);
                        var result = new DAParametrosOfi().Insertar(new BEParametroOff
                        {
                            OWNER = office.OWNER,
                            OFF_ID = codOficinaGen,
                            PAR_ID = codigoGenParam,
                            LOG_USER_CREAT = parametro.LOG_USER_CREAT
                        });
                    }
                }
                #endregion
                #region DOCUMENTO
                if (office.Documentos != null)
                {
                    foreach (var documento in office.Documentos)
                    {
                        var codigoGenDoc = new DADocumentoGral().Insertar(documento);
                        var result = new DADocumentoOfi().Insertar(new BEDocumentoOfi
                        {
                            OWNER = office.OWNER,
                            OFF_ID = codOficinaGen,
                            DOC_ID = codigoGenDoc,
                            LOG_USER_CREAT = documento.LOG_USER_CREAT

                        });
                    }
                }
                #endregion
                #region DIRECCION
                if (office.Direcciones != null)
                {
                    foreach (var direccion in office.Direcciones)
                    {
                        var codigoGenAdd = new DADirecciones().Insertar(direccion);
                        var result = new DAREC_OFFICES().InsertarDireccionOfi(new BEDireccionOfi
                        {
                            OWNER = office.OWNER,
                            OFF_ID = codOficinaGen,
                            ADD_ID = codigoGenAdd,
                            LOG_USER_CREAT = office.LOG_USER_CREAT

                        });
                        office.OFF_ID = codOficinaGen;
                        office.ADD_ID = codigoGenAdd;
                    }
                    var upd = new DAREC_OFFICES().Actualizar(office);
                }
                #endregion         
                #region CONTACTO
                if (office.Contacto != null)
                {
                    foreach (var contacto in office.Contacto)
                    {
                        var result = new DASocioNegocioOficina().Insertar(new BESocioNegocioOficina
                        {
                            OWNER = office.OWNER,
                            BPS_ID = contacto.BPS_ID,
                            OFF_ID = codOficinaGen,
                            ROL_ID = contacto.ROL_ID,
                            LOG_USER_CREAT = office.LOG_USER_CREAT
                        });
                    }
                }
                #endregion

                #region DIVISION_ADMINISTRATIVA
                if (office.DivisionAdm != null)
                {
                    foreach (var divAdm in office.DivisionAdm)
                    {
                        //Registrando una División Adm.
                        var IdOficinaDivAdm = new DAOficinaDivision().InsertarDivAdm(new BEDivisionRecaudador
                        {
                            OWNER = office.OWNER,
                            OFF_ID = codOficinaGen,
                            DAD_ID = divAdm.DAD_ID,
                            LOG_USER_CREAT = divAdm.LOG_USER_CREAT
                        });

                        //Registrando las modalidaddes de la división adm.
                        #region GRUPO_MODALIDAD
                        foreach (var ofiGrupoModalidad in office.DivisionAdmGrupoModalidad.Where(x => x.ID_COLL_DIV == divAdm.ID_COLL_DIV))
                        {
                            ofiGrupoModalidad.OFF_ID = codOficinaGen;
                            ofiGrupoModalidad.ID_COLL_DIV = IdOficinaDivAdm;
                            var IdDivGrupoModalidad = new DAOficinaDivisionModalidad().InsertarDivMod(ofiGrupoModalidad);
                        }
                        #endregion

                        //Registrando los numeradores de la división adm.
                        #region NUMERADOR
                        foreach (var ofiNumerador in office.DivisionAdmNumerador.Where(n => n.ID_COLL_DIV == divAdm.ID_COLL_DIV))
                        {
                            ofiNumerador.OFF_ID = codOficinaGen;
                            ofiNumerador.ID_COLL_DIV = IdOficinaDivAdm;
                            var IdDivGrupoModalidad = new DANumeracionOfi().Insertar(ofiNumerador);
                        }
                        #endregion
                    }
                }



                #endregion
                transa.Complete();
            }
            return codOficinaGen;
        }

        public int Actualizar(BEOffices oficina,
                                 List<BEDireccion> dirEliminar, List<BEDireccion> listDirActivar,
                                 List<BEObservationGral> obsEliminar, List<BEObservationGral> listObsActivar,
                                 List<BEDocumentoGral> docEliminar, List<BEDocumentoGral> listDocActivar,
                                 List<BEParametroGral> parEliminar, List<BEParametroGral> listParActivar,

                                 List<BESocioNegocioOficina> contactoEliminar, List<BESocioNegocioOficina> listContactoActivar,
                                 List<BEDivisionRecaudador> divisionEliminar, List<BEDivisionRecaudador> divisionActivar,
                                 List<BEGrupoModalidadOficina> grupoModalidadEliminar, List<BEGrupoModalidadOficina> grupoModalidadActivar,
                                 List<BENumeradorOficina> numeradorEliminar, List<BENumeradorOficina> numeradorActivar
            )
        {
            int upd = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DAREC_OFFICES().Actualizar(oficina);

                #region DIRECCION
                DADirecciones proxyDir = new DADirecciones();
                DADireccionBps prxyDirBps = new DADireccionBps();
                if (oficina.Direcciones != null)
                {
                    foreach (var direccion in oficina.Direcciones)
                    {
                        var ent = new DADirecciones().ObtenerDireccionXOficina(oficina.ADD_ID, oficina.OWNER);
                        if (ent != null)
                        {
                            if (ent.ADD_TYPE != direccion.ADD_TYPE || ent.ENT_ID != direccion.ENT_ID ||
                                ent.TIS_N != direccion.TIS_N || ent.GEO_ID != direccion.GEO_ID ||
                                ent.ROU_ID != direccion.ROU_ID || ent.ROU_NAME != direccion.ROU_NAME ||
                                ent.ROU_NUM != direccion.ROU_NUM || ent.HOU_TURZN != direccion.HOU_TURZN ||
                                ent.HOU_URZN != direccion.HOU_URZN || ent.HOU_NRO != direccion.HOU_NRO ||
                                ent.HOU_MZ != direccion.HOU_MZ || ent.HOU_LOT != direccion.HOU_LOT ||
                                ent.HOU_TETP != direccion.HOU_TETP || ent.HOU_NETP != direccion.HOU_NETP ||
                                ent.ADD_TINT != direccion.ADD_TINT || ent.ADD_INT != direccion.ADD_INT ||
                                ent.ADD_REFER != direccion.ADD_REFER || ent.CPO_ID != direccion.CPO_ID ||
                                ent.ADD_REFER != direccion.ADD_REFER || ent.CPO_ID != direccion.CPO_ID
                                )
                            {
                                ///Eliminar Direccion la informacion de la direccion 
                                var result = proxyDir.Eliminar(direccion.OWNER, direccion.ADD_ID, oficina.LOG_USER_UPDAT);
                                var resultOffDir = new DAREC_OFFICES().EliminarOficinaDir(direccion.OWNER, oficina.OFF_ID, direccion.ADD_ID, oficina.LOG_USER_UPDAT);
                                //Insertar nuea direccion
                                var codigoGenAdd = new DADirecciones().Insertar(direccion);
                                var resultDir = new DAREC_OFFICES().InsertarDireccionOfi(new BEDireccionOfi
                                {
                                    OWNER = oficina.OWNER,
                                    OFF_ID = oficina.OFF_ID,
                                    ADD_ID = codigoGenAdd,
                                    LOG_USER_CREAT = oficina.LOG_USER_CREAT

                                });
                                oficina.OFF_ID = oficina.OFF_ID;
                                oficina.ADD_ID = codigoGenAdd;
                                var updDir = new DAREC_OFFICES().Actualizar(oficina);
                            }
                        }
                    }

                }
                #endregion
                #region OBSERVACION
                ///logica de negocio para actualizar Observaciones
                DAObservationGral proxyObs = new DAObservationGral();
                DAObservationOff proxyObsOff = new DAObservationOff();
                if (oficina.Observaciones != null)
                {
                    foreach (var item in oficina.Observaciones)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva Observacion
                        BEObservationGral proxyObsObtener = proxyObs.ObtenerObsOficina(oficina.OWNER, item.OBS_ID, oficina.OFF_ID);
                        if (proxyObsObtener == null)
                        {
                            var codigoGenAdd = proxyObs.InsertarObsGrl(item);
                            var result = proxyObsOff.InsertarObsOff(new BEObservationOff
                            {
                                OFF_ID = Convert.ToInt32(oficina.OFF_ID),
                                OBS_ID = codigoGenAdd,
                                OWNER = oficina.OWNER,
                                LOG_USER_CREAT = oficina.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion de la direccion 
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyObs.Update(item);
                        }
                    }
                }

                /// se elimina las direcciones
                if (obsEliminar != null)
                {
                    foreach (var item in obsEliminar)
                    {
                        proxyObs.Eliminar(oficina.OWNER, item.OBS_ID, oficina.LOG_USER_UPDAT);
                    }
                }
                /// activa las direcciones
                if (listObsActivar != null)
                {
                    foreach (var item in listObsActivar)
                    {
                        proxyObs.Activar(oficina.OWNER, item.OBS_ID, oficina.LOG_USER_UPDAT);
                    }
                }
                #endregion
                #region DOCUMENTO
                DADocumentoGral proxyDoc = new DADocumentoGral();
                DADocumentoOfi proxyDocOfi = new DADocumentoOfi();
                if (oficina.Documentos != null)
                {
                    foreach (var item in oficina.Documentos)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva Observacion
                        BEDocumentoGral proxyDocObtener = proxyDoc.ObtenerDocOFF(oficina.OWNER, item.DOC_ID, oficina.OFF_ID);
                        if (proxyDocObtener == null)
                        {
                            var codigoGenAdd = proxyDoc.Insertar(item);
                            var result = proxyDocOfi.Insertar(new BEDocumentoOfi
                            {
                                OFF_ID = Convert.ToInt32(oficina.OFF_ID),
                                DOC_ID = codigoGenAdd,
                                OWNER = oficina.OWNER,
                                LOG_USER_CREAT = oficina.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion de la direccion 
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyDoc.Update(item);
                        }
                    }
                }

                if (docEliminar != null)
                {
                    docEliminar.ForEach(x => { proxyDoc.Eliminar(oficina.OWNER, x.DOC_ID, oficina.LOG_USER_UPDAT); });
                }
                if (listDocActivar != null)
                {
                    listDocActivar.ForEach(x => { proxyDoc.Activar(oficina.OWNER, x.DOC_ID, oficina.LOG_USER_UPDAT); });
                }
                #endregion
                #region PARAMETRO
                DAParametroGral proxyPar = new DAParametroGral();
                DAParametrosOfi proxyParOfi = new DAParametrosOfi();
                if (oficina.Parametros != null)
                {
                    foreach (var item in oficina.Parametros)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva Observacion
                        BEParametroGral proxyParObtener = proxyPar.ObtenerParOFF(oficina.OWNER, item.PAR_ID, oficina.OFF_ID);
                        if (proxyParObtener == null)
                        {
                            var codigoGenAdd = proxyPar.Insertar(item);
                            var result = proxyParOfi.Insertar(new BEParametroOff
                            {
                                OFF_ID = Convert.ToInt32(oficina.OFF_ID),
                                PAR_ID = codigoGenAdd,
                                OWNER = oficina.OWNER,
                                LOG_USER_CREAT = oficina.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion de la direccion 
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyPar.Update(item);
                        }
                    }
                }
                if (parEliminar != null)
                {
                    foreach (var item in parEliminar)
                    {
                        proxyPar.Eliminar(oficina.OWNER, item.PAR_ID, oficina.LOG_USER_UPDAT);
                    }
                }
                if (listParActivar != null)
                {
                    listParActivar.ForEach(x => { proxyPar.Activar(oficina.OWNER, x.PAR_ID, oficina.LOG_USER_UPDAT); });
                }
                #endregion
                #region CONTACTO
                DASocioNegocioOficina proxyConOfi = new DASocioNegocioOficina();
                if (oficina.Contacto != null)
                {
                    foreach (var item in oficina.Contacto)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva Observacion
                        BESocioNegocioOficina proxyParObtener = proxyConOfi.Obtener(oficina.OWNER, oficina.OFF_ID, item.BPS_ID);
                        if (proxyParObtener == null)
                        {
                            var result = new DASocioNegocioOficina().Insertar(new BESocioNegocioOficina
                            {
                                OWNER = oficina.OWNER,
                                BPS_ID = item.BPS_ID,
                                OFF_ID = oficina.OFF_ID,
                                ROL_ID = item.ROL_ID,
                                LOG_USER_CREAT = oficina.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            if (proxyParObtener.BPS_ID != item.BPS_ID ||
                                proxyParObtener.OFF_ID != item.OFF_ID ||
                                proxyParObtener.ROL_ID != item.ROL_ID
                                )
                            {
                                ///sino  solo se actualiza la informacion de la direccion 
                                var result = new DASocioNegocioOficina().Actualizar(new BESocioNegocioOficina
                                {
                                    OWNER = oficina.OWNER,
                                    BPS_ID = item.BPS_ID,
                                    OFF_ID = oficina.OFF_ID,
                                    ROL_ID = item.ROL_ID,
                                    LOG_USER_CREAT = oficina.LOG_USER_CREAT
                                });
                            }
                        }

                    }
                }

                if (contactoEliminar != null)
                {
                    foreach (var item in contactoEliminar)
                    {
                        proxyConOfi.Eliminar(
                            new BESocioNegocioOficina
                            {
                                OWNER = oficina.OWNER,
                                BPS_ID = item.BPS_ID,
                                OFF_ID = oficina.OFF_ID,
                                LOG_USER_UPDAT = oficina.LOG_USER_UPDAT
                            });
                    }
                }
                if (listContactoActivar != null)
                {
                    listContactoActivar.ForEach(x =>
                    {
                        proxyConOfi.Activar(
                         new BESocioNegocioOficina
                         {
                             OWNER = oficina.OWNER,
                             OFF_ID = oficina.OFF_ID,
                             BPS_ID = x.BPS_ID,
                             LOG_USER_UPDAT = oficina.LOG_USER_UPDAT
                         }
                                          );
                    });
                }
                #endregion
                
                #region DIVISION
                DAOficinaDivision proxyDiv = new DAOficinaDivision();
                if (oficina.DivisionAdm != null)
                {
                    foreach (var divAdm in oficina.DivisionAdm)
                    {
                        divAdm.OFF_ID = oficina.OFF_ID;
                        BEDivisionRecaudador proxyGMObtener = proxyDiv.Obtener(oficina.OWNER, Convert.ToDecimal(divAdm.OFF_ID), divAdm.ID_COLL_DIV);
                        if (proxyGMObtener == null)
                        {   //Regsitrar nueva divisiòn.
                            var IdOficinaDivAdm = proxyDiv.InsertarDivAdm(new BEDivisionRecaudador
                            {
                                OWNER = oficina.OWNER,
                                OFF_ID = divAdm.OFF_ID,
                                DAD_ID = divAdm.DAD_ID,
                                LOG_USER_CREAT = divAdm.LOG_USER_CREAT
                            });
                            //Registrar modalidad.
                            foreach (var ofiGrupoModalidad in oficina.DivisionAdmGrupoModalidad.Where(x => x.ID_COLL_DIV == divAdm.ID_COLL_DIV))
                            {
                                ofiGrupoModalidad.OFF_ID = divAdm.OFF_ID; ofiGrupoModalidad.ID_COLL_DIV = IdOficinaDivAdm;
                                var IdDivGrupoModalidad = new DAOficinaDivisionModalidad().InsertarDivMod(ofiGrupoModalidad);
                            }
                            //Registrar numerador
                            foreach (var ofiNumerador in oficina.DivisionAdmNumerador.Where(n => n.ID_COLL_DIV == divAdm.ID_COLL_DIV))
                            {
                                ofiNumerador.OFF_ID = divAdm.OFF_ID; ofiNumerador.ID_COLL_DIV = IdOficinaDivAdm;
                                var IdDivGrupoModalidad = new DANumeracionOfi().Insertar(ofiNumerador);
                            }
                        }
                        else
                        {
                            //Modalidad
                            foreach (var ofiGrupoModalidad in oficina.DivisionAdmGrupoModalidad.Where(x => x.ID_COLL_DIV == divAdm.ID_COLL_DIV))
                            {
                                var obtenerGrupoModalidad = new DAOficinaDivisionModalidad().Obtener(oficina.OWNER, oficina.OFF_ID, ofiGrupoModalidad.DIV_RiGHTS_ID);
                                if (obtenerGrupoModalidad == null)
                                {
                                    ofiGrupoModalidad.OFF_ID = divAdm.OFF_ID; ofiGrupoModalidad.ID_COLL_DIV = divAdm.ID_COLL_DIV;
                                    var IdDivGrupoModalidad = new DAOficinaDivisionModalidad().InsertarDivMod(ofiGrupoModalidad);
                                }
                            }
                            //Numerador
                            foreach (var ofiNumerador in oficina.DivisionAdmNumerador.Where(x => x.ID_COLL_DIV == divAdm.ID_COLL_DIV))
                            {
                                var obtenerNumerador = new DANumeracionOfi().Obtener(oficina.OWNER, oficina.OFF_ID, ofiNumerador.NUM_ID);
                                if (obtenerNumerador == null)
                                {
                                    ofiNumerador.OFF_ID = divAdm.OFF_ID; 
                                    ofiNumerador.ID_COLL_DIV = divAdm.ID_COLL_DIV;
                                    var IdDivGrupoModalidad = new DANumeracionOfi().Insertar(ofiNumerador);
                                }
                            }
                        }

                    }
                }

                if (divisionActivar != null)
                {
                    divisionActivar.ForEach(x => { proxyDiv.Activar(oficina.OWNER, oficina.OFF_ID, x.ID_COLL_DIV, x.LOG_USER_UPDAT); });
                }
                if (divisionEliminar != null)
                {
                    divisionEliminar.ForEach(x => { proxyDiv.Eliminar(oficina.OWNER, oficina.OFF_ID, x.ID_COLL_DIV, x.LOG_USER_UPDAT); });
                }
                #endregion

                #region DIVISION_GRUPO_MODALIDAD
                DAOficinaDivisionModalidad proxyGruModDiv = new DAOficinaDivisionModalidad();
                if (grupoModalidadEliminar != null)
                    grupoModalidadEliminar.ForEach(x => { proxyGruModDiv.Eliminar(oficina.OWNER, oficina.OFF_ID, x.DIV_RiGHTS_ID, x.LOG_USER_UPDATE); });

                if (grupoModalidadActivar != null)
                    grupoModalidadActivar.ForEach(x => { proxyGruModDiv.Activar(oficina.OWNER, oficina.OFF_ID, x.DIV_RiGHTS_ID, x.LOG_USER_UPDATE); });

                #endregion

                #region DIVISION_NUMERADOR
                DANumeracionOfi proxyNumeradorDiv = new DANumeracionOfi();
                if (numeradorEliminar != null)
                    numeradorEliminar.ForEach(x => { proxyNumeradorDiv.Eliminar(oficina.OWNER, oficina.OFF_ID, x.NUM_ID, x.LOG_USER_UPDAT); });

                if (numeradorActivar != null)
                    numeradorActivar.ForEach(x => { proxyNumeradorDiv.Activar(oficina.OWNER, oficina.OFF_ID, x.NUM_ID, x.LOG_USER_UPDAT); });

                #endregion

                transa.Complete();
            }
            return upd;
        }

        public List<BEOffices> ObtenerXDescripcion(BEOffices oficna)
        {
            return new DAREC_OFFICES().ObtenerXDescripcion(oficna);
        }
        public SocioNegocio ObtenerSocioDocumento(string owner, decimal tipo, string nro_tipo)
        {
            return new DAREC_OFFICES().ObtenerSocioDocumento(owner, tipo, nro_tipo);
        }
        public BEOffices ObtenerNombre(string owner, decimal off_id)
        {
            return new DAREC_OFFICES().ObtenerNombre(owner, off_id);
        }

        #region VALIDA OFICINA

        public int ValidaOficina( int off_id)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DAREC_OFFICES().ValidaOficina(owner, off_id);
        }

        public int ValidaEmisionMensualOficina(int oficina)
        {

            return new DAREC_OFFICES().ValidaEmisionMensualOficina(oficina);
        }

        
        #endregion

    }
}
