using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.Entities;
using SGRDA.DA;
using System.Transactions;

namespace SGRDA.BL
{
    public class BLEstablecimiento
    {

        public List<BEEstablecimiento> Listar_Establecimiento_Principal(string Owner, decimal tipoEst, decimal? subTipoEst, decimal? IdEstablecimiento, string nombre, int st, decimal bpsId, decimal division, decimal subtipo1, decimal subtipo2, decimal subtipo3,int pagina, int cantRegxPag)
        {
            return new DAEstablecimiento().Listar_Establecimiento_Principal(Owner, tipoEst, subTipoEst, IdEstablecimiento, nombre, st, bpsId, division, subtipo1, subtipo2, subtipo3, pagina, cantRegxPag);
        }

        public List<BEEstablecimiento> usp_Get_PorSocioNegocioPage(string Owner, decimal? IdSocio, int st, int pagina, int cantRegxPag)
        {
            return new DAEstablecimiento().usp_Get_PorSocioNegocioPage(Owner, IdSocio, st, pagina, cantRegxPag);
        }

        public List<BEEstablecimiento> usp_Get_PorEstablecimientoPage(string Owner, decimal tipoEst, decimal? subTipoEst, decimal? IdEstablecimiento, string nombre, int st, int pagina, int cantRegxPag)
        {
            return new DAEstablecimiento().usp_Get_PorEstablecimientoPage(Owner, tipoEst, subTipoEst, IdEstablecimiento, nombre, st, pagina, cantRegxPag);
        }

        public List<BEEstablecimiento> usp_Get_DivisionAdministrativaPage(string owner, string divTipo, decimal? divAdmin, int st, int pagina, int cantRegxPag)
        {
            return new DAEstablecimiento().usp_Get_DivisionAdministrativaPage(owner, divTipo, divAdmin, st, pagina, cantRegxPag);
        }

        public List<BEEstablecimiento> usp_Get_PorDireccionesPage(string Owner, string NombreViaDir, decimal tipodireccion, decimal TipoUrbanizacion, string NombreUrbanizacion, string Manzana, decimal? Numero, string Lote, string TipoInterior, string NumeroInterior, string CodigoViaDir, decimal TipoEtapa, string NombreEtapa, decimal TerritorioDir, string ReferenciaDir, decimal? ubigeo, int st, int pagina, int cantRegxPag)
        {
            return new DAEstablecimiento().usp_Get_PorDireccionesPage(Owner, NombreViaDir, tipodireccion, TipoUrbanizacion, NombreUrbanizacion, Manzana, Numero, Lote, TipoInterior, NumeroInterior, CodigoViaDir, TipoEtapa, NombreEtapa, TerritorioDir, ReferenciaDir, ubigeo, st, pagina, cantRegxPag);
        }

        public List<BEEstablecimiento> UPS_BUSCAR_ESTABLECIMIENTO_X_NOMBRE(string Owner, string datos)
        {
            return new DAEstablecimiento().UPS_BUSCAR_ESTABLECIMIENTO_X_NOMBRE(Owner, datos);
        }

        public List<BELicencias> usp_Get_LicenciaPage(string owner, decimal? EstablecimientoId, decimal? Off_Id, int pagina, int cantRegxPag)
        {
            return new DALicencias().usp_Get_LicenciaPage(owner, EstablecimientoId, Off_Id, pagina, cantRegxPag);
        }

        public int Insertar(BEEstablecimiento en)
        {
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DAEstablecimiento().Insertar(en);
                en.EST_ID = codigoGen;

                if (en.Caracteristicas != null)
                {
                    foreach (var caracteristica in en.Caracteristicas)
                    {
                        var result = new DACaracteristicaEst().Insertar(new BECaracteristicaEst
                        {
                            CHAR_ID = caracteristica.CHAR_ID,
                            EST_ID = codigoGen,
                            ESTT_ID = caracteristica.ESTT_ID,
                            SUBE_ID = caracteristica.SUBE_ID,
                            OWNER = en.OWNER,
                            VALUE = caracteristica.VALUE,
                            LOG_USER_CREAT = en.LOG_USER_CREAT
                        });
                    }
                }
                if (en.Parametros != null)
                {
                    foreach (var parametro in en.Parametros)
                    {
                        var codigoGenParam = new DAParametroGral().Insertar(parametro);
                        var result = new DAParametrosEst().Insertar(new BEParametrosEst
                        {
                            PAR_ID = codigoGenParam,
                            EST_ID = codigoGen,
                            OWNER = en.OWNER,
                            LOG_USER_CREAT = en.LOG_USER_CREAT
                        });
                    }
                }
                if (en.Observaciones != null)
                {
                    foreach (var observacion in en.Observaciones)
                    {
                        var codigoGenObs = new DAObservationGral().InsertarObsGrl(observacion);
                        var result = new DAObservationEst().Insertar(new BEObservationEst
                        {
                            EST_ID = codigoGen,
                            OBS_ID = codigoGenObs,
                            OWNER = en.OWNER,
                            LOG_USER_CREAT = en.LOG_USER_CREAT
                        });
                    }
                }
                if (en.Documentos != null)
                {
                    foreach (var documento in en.Documentos)
                    {
                        var codigoGenDoc = new DADocumentoGral().Insertar(documento);
                        var result = new DADocumentoEst().Insertar(new BEDocumentoEst
                        {
                            EST_ID = codigoGen,
                            DOC_ID = codigoGenDoc,
                            OWNER = en.OWNER,
                            LOG_USER_CREAT = en.LOG_USER_CREAT

                        });
                    }
                }

                if (en.Direccion != null)
                {
                    foreach (var direccion in en.Direccion)
                    {
                        var codigoGenAdd = new DADirecciones().Insertar(direccion);
                        var result = new DADireccionEst().Insertar(new BEDireccionEst
                        {
                            EST_ID = codigoGen,
                            ADD_ID = codigoGenAdd,
                            OWNER = en.OWNER,
                            LOG_USER_CREAT = en.LOG_USER_CREAT
                        });
                    }
                }

                if (en.Telefonos != null)
                {
                    foreach (var telefono in en.Telefonos)
                    {
                        var codigoGenDoc = new DATelefono().Insertar(telefono);
                        var result = new DATelefonoEst().Insertar(new BETelefonoEst
                        {
                            EST_ID = codigoGen,
                            PHONE_ID = codigoGenDoc,
                            OWNER = en.OWNER,
                            LOG_USER_CREAT = en.LOG_USER_CREAT
                        });
                    }
                }

                if (en.Correos != null)
                {
                    foreach (var correo in en.Correos)
                    {
                        var codigoGenDoc = new DACorreo().Insertar(correo);
                        var result = new DACorreoEst().Insertar(new BECorreoEst
                        {
                            EST_ID = codigoGen,
                            MAIL_ID = codigoGenDoc,
                            OWNER = en.OWNER,
                            LOG_USER_CREAT = en.LOG_USER_CREAT
                        });
                    }
                }

                if (en.RedSocial != null)
                {
                    foreach (var redsocial in en.RedSocial)
                    {
                        var codigoGenDoc = new DARedSocial().Insertar(redsocial);
                        var result = new DARedSocialEst().Insertar(new BERedSocialEst
                        {
                            EST_ID = codigoGen,
                            CONT_ID = codigoGenDoc,
                            OWNER = en.OWNER,
                            LOG_USER_CREAT = en.LOG_USER_CREAT
                        });
                    }
                }

                DAAsociado proxyAso = new DAAsociado();
                if (en.Asociados != null)
                {
                    foreach (var item in en.Asociados)
                    {
                        var result = new DAAsociadosEst().Insertar(new BEAsociadosEst
                        {
                            OWNER = en.OWNER,
                            BPS_ID = item.BPS_ID,
                            EST_ID = en.EST_ID,
                            ROL_ID = item.ROL_ID,
                            BPS_MAIN = item.BPS_MAIN,
                            LOG_USER_CREAT = item.LOG_USER_CREAT
                        });
                    }
                }

                DADivisionesEst proxyDiv = new DADivisionesEst();
                if (en.Divisiones != null)
                {
                    foreach (var item in en.Divisiones)
                    {
                        var result = new DADivisionesEst().Insertar(new BEDivisionesEst
                        {
                            OWNER = item.OWNER,
                            EST_ID = codigoGen,
                            idDIVISIONVAL = item.idDIVISIONVAL,
                            LOG_USER_CREAT = en.LOG_USER_CREAT
                        });
                    }
                }

                DADifusionEst proxyDif = new DADifusionEst();

                if (en.Difusion != null)
                {
                    foreach (var item in en.Difusion)
                    {
                        var result = new DADifusionEst().Insertar(new BEDifusionEst
                        {
                            OWNER = item.OWNER,
                            EST_ID = codigoGen,
                            BROAD_ID = item.BROAD_ID,
                            BROADE_NUM = item.BROADE_NUM,
                            BROADE_STORAGE = item.BROADE_STORAGE,
                            LOG_USER_CREAT = en.LOG_USER_CREAT
                        });
                    }
                }
                transa.Complete();
            }
            return codigoGen;
        }

        public BEEstablecimiento ObtenerNombreEstablecimiento(string owner, decimal idEstablecimiento)
        {
            return new DAEstablecimiento().ObetnerNombre(owner, idEstablecimiento);
        }

        public BEEstablecimiento ObtenerCabeceraEstablecimiento(decimal idEstablecimiento, string owner)
        {
            return new DAEstablecimiento().cabeceraEstablecimiento(idEstablecimiento, owner);
        }

        public BEEstablecimiento Obtiene(decimal idEntidad, string owner, decimal tipoEntidad)
        {
            var objEstablecimiento = new DAEstablecimiento().cabeceraEstablecimiento(idEntidad, owner);
            if (objEstablecimiento != null)
            {
                objEstablecimiento.Observaciones = new DAObservationGral().ObservacionXEstablecimiento(objEstablecimiento.EST_ID, owner, tipoEntidad);
                objEstablecimiento.Parametros = new DAParametroGral().ParametroXEstablecimiento(objEstablecimiento.EST_ID, owner, tipoEntidad);
                objEstablecimiento.Documentos = new DADocumentoGral().DocumentoXEstablecimiento(objEstablecimiento.EST_ID, owner, tipoEntidad);
                objEstablecimiento.Direccion = new DADirecciones().DireccionXEstablecimiento(objEstablecimiento.EST_ID, owner, tipoEntidad);
                objEstablecimiento.Telefonos = new DATelefono().TelefonoXEst(objEstablecimiento.EST_ID, owner, tipoEntidad);
                objEstablecimiento.Correos = new DACorreo().CorreoXEst(objEstablecimiento.EST_ID, owner, tipoEntidad);
                objEstablecimiento.RedSocial = new DARedSocial().RedSocialXEst(objEstablecimiento.EST_ID, owner, tipoEntidad);
                //objEstablecimiento.Inspection = new DAInspectionEst().InspeccionXEstablecimiento(objEstablecimiento.EST_ID, owner);
                objEstablecimiento.Caracteristicas = new DACaracteristicaEst().CaracteristicaXEstablecimiento(objEstablecimiento.EST_ID, owner);
                objEstablecimiento.Asociados = new DAAsociadosEst().AsociadoXEstablecimiento(objEstablecimiento.EST_ID, owner);
                //objEstablecimiento.Divisiones = new DADivisionesEst().DivisionesXEstablecimiento(owner, objEstablecimiento.EST_ID);
                objEstablecimiento.Difusion = new DADifusionEst().DifusionXEstablecimiento(owner, objEstablecimiento.EST_ID);
            }
            return objEstablecimiento;
        }

        public BEEstablecimiento ObtieneCaracteristicasInpeccion(decimal idEstablecimiento, string owner)
        {
            BEEstablecimiento obj = new BEEstablecimiento();
            obj.Caracteristicas = new DACaracteristicaEst().CaracteristicaXEstablecimiento(idEstablecimiento, owner);
            return obj;
        }

        public int Update(BEEstablecimiento establecimiento,
                                List<BEDireccion> dirEliminar, List<BEDireccion> listDirActivar,
                                List<BEObservationGral> obsEliminar, List<BEObservationGral> listObsActivar,
                                List<BEDocumentoGral> docEliminar, List<BEDocumentoGral> listDocActivar,
                                List<BEParametroGral> parEliminar, List<BEParametroGral> listParActivar,
                                List<BETelefono> telEliminar, List<BETelefono> listTelActivar,
                                List<BECorreo> mailEliminar, List<BECorreo> listMailActivar,
                                List<BERedes_Sociales> RedSocialEliminar, List<BERedes_Sociales> listRedSocialActivar,
                                List<BECaracteristicaEst> carEliminar, List<BECaracteristicaEst> listCarActivar,
                                List<BEAsociadosEst> asoEliminar, List<BEAsociadosEst> listAsociadoActivar,
                                //List<BEDivisionesEst> divEliminar, List<BEDivisionesEst> listDivisionActivar,
                                List<BEDifusionEst> difEliminar, List<BEDifusionEst> listDifusionActivar)
        {
            int upd = 0;

            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DAEstablecimiento().Update(establecimiento);

                DADirecciones proxyDir = new DADirecciones();
                DADireccionEst prxyDirEst = new DADireccionEst();

                if (establecimiento.Direccion != null)
                {
                    foreach (var direccion in establecimiento.Direccion)
                    {
                        BEDireccion ent = proxyDir.ObtenerDirEst(establecimiento.OWNER, direccion.ADD_ID, establecimiento.EST_ID);
                        if (ent == null)
                        {
                            var codigoGenAdd = proxyDir.Insertar(direccion);
                            var result = new DADireccionEst().Insertar(new BEDireccionEst
                            {
                                EST_ID = establecimiento.EST_ID,
                                ADD_ID = codigoGenAdd,
                                OWNER = establecimiento.OWNER,
                                LOG_USER_CREAT = establecimiento.LOG_USER_CREAT
                            });
                        }
                        else if (ent.ADD_TYPE != direccion.ADD_TYPE || ent.ENT_ID != direccion.ENT_ID ||
                                 ent.TIS_N != direccion.TIS_N || ent.GEO_ID != direccion.GEO_ID ||
                                 ent.ROU_ID != direccion.ROU_ID || ent.ROU_NAME != direccion.ROU_NAME ||
                                 ent.ROU_NUM != direccion.ROU_NUM || ent.HOU_TURZN != direccion.HOU_TURZN ||
                                 ent.HOU_URZN != direccion.HOU_URZN || ent.HOU_NRO != direccion.HOU_NRO ||
                                 ent.HOU_MZ != direccion.HOU_MZ || ent.HOU_LOT != direccion.HOU_LOT ||
                                 ent.HOU_TETP != direccion.HOU_TETP || ent.HOU_NETP != direccion.HOU_NETP ||
                                 ent.ADD_TINT != direccion.ADD_TINT || ent.ADD_INT != direccion.ADD_INT ||
                                 ent.ADD_REFER != direccion.ADD_REFER || ent.CPO_ID != direccion.CPO_ID ||
                                 ent.MAIN_ADD != direccion.MAIN_ADD
                                 )
                        {
                            var result = proxyDir.Update(direccion);
                        }
                    }
                }

                if (dirEliminar != null)
                {
                    foreach (var item in dirEliminar)
                    {
                        proxyDir.Eliminar(establecimiento.OWNER, item.ADD_ID, establecimiento.LOG_USER_CREAT);
                    }
                }
                if (listDirActivar != null)
                {
                    foreach (var item in listDirActivar)
                    {
                        proxyDir.Activar(establecimiento.OWNER, item.ADD_ID, establecimiento.LOG_USER_UPDAT);
                    }
                }

                DAObservationGral proxyObs = new DAObservationGral();
                DAObservationEst proxyObsEst = new DAObservationEst();

                if (establecimiento.Observaciones != null)
                {
                    foreach (var item in establecimiento.Observaciones)
                    {
                        BEObservationGral proxyObsObtener = proxyObs.ObtenerObsEst(establecimiento.OWNER, item.OBS_ID, establecimiento.EST_ID);
                        if (proxyObsObtener == null)
                        {
                            var codigoGenAdd = proxyObs.InsertarObsGrl(item);
                            var result = proxyObsEst.Insertar(new BEObservationEst
                            {
                                EST_ID = establecimiento.EST_ID,
                                OBS_ID = codigoGenAdd,
                                OWNER = establecimiento.OWNER,
                                LOG_USER_CREAT = establecimiento.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyObs.Update(item);
                        }
                    }
                }
                if (obsEliminar != null)
                {
                    foreach (var item in obsEliminar)
                    {
                        proxyObs.Eliminar(establecimiento.OWNER, item.OBS_ID, establecimiento.LOG_USER_UPDAT);
                    }
                }
                if (listObsActivar != null)
                {
                    foreach (var item in listObsActivar)
                    {
                        proxyObs.Activar(establecimiento.OWNER, item.OBS_ID, establecimiento.LOG_USER_UPDAT);
                    }
                }

                DADocumentoGral proxyDoc = new DADocumentoGral();
                DADocumentoEst proxyDocEst = new DADocumentoEst();

                if (establecimiento.Documentos != null)
                {
                    foreach (var item in establecimiento.Documentos)
                    {
                        BEDocumentoGral proxyDocObtener = proxyDoc.ObtenerDocEst(establecimiento.OWNER, item.DOC_ID, establecimiento.EST_ID);
                        if (proxyDocObtener == null)
                        {
                            var codigoGenAdd = proxyDoc.Insertar(item);
                            var result = proxyDocEst.Insertar(new BEDocumentoEst
                            {
                                EST_ID = establecimiento.EST_ID,
                                DOC_ID = codigoGenAdd,
                                OWNER = establecimiento.OWNER,
                                LOG_USER_CREAT = establecimiento.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyDoc.Update(item);
                        }
                    }
                }

                if (docEliminar != null)
                {
                    docEliminar.ForEach(x => { proxyDoc.Eliminar(establecimiento.OWNER, x.DOC_ID, establecimiento.LOG_USER_UPDAT); });                   
                }
                if (listDocActivar != null)
                {
                    listDocActivar.ForEach(x => { proxyDoc.Activar(establecimiento.OWNER, x.DOC_ID, establecimiento.LOG_USER_UPDAT); });
                }

                DAParametroGral proxyPar = new DAParametroGral();
                DAParametrosEst proxyParEst = new DAParametrosEst();

                if (establecimiento.Parametros != null)
                {
                    foreach (var item in establecimiento.Parametros)
                    {
                        BEParametroGral proxyParObtener = proxyPar.ObtenerParEst(establecimiento.OWNER, item.PAR_ID, establecimiento.EST_ID);
                        if (proxyParObtener == null)
                        {
                            var codigoGenAdd = proxyPar.Insertar(item);
                            var result = proxyParEst.Insertar(new BEParametrosEst
                            {
                                PAR_ID = codigoGenAdd,
                                EST_ID = establecimiento.EST_ID,
                                OWNER = establecimiento.OWNER,
                                LOG_USER_CREAT = establecimiento.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyPar.Update(item);
                        }
                    }
                }
                if (parEliminar != null)
                {
                    foreach (var item in parEliminar)
                    {
                        proxyPar.Eliminar(establecimiento.OWNER, item.PAR_ID, establecimiento.LOG_USER_UPDAT);
                    }
                }
                if (listParActivar != null)
                {
                    listParActivar.ForEach(x => { proxyPar.Activar(establecimiento.OWNER, x.PAR_ID, establecimiento.LOG_USER_UPDAT); });
                }

                ///logica de negocio para actualizar telefono
                DATelefono proxyTel = new DATelefono();
                DATelefonoEst proxyTelEst = new DATelefonoEst();

                if (establecimiento.Telefonos != null)
                {
                    foreach (var item in establecimiento.Telefonos)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva Observacion
                        BETelefono proxyTelObtener = proxyTel.ObtenerTelEst(establecimiento.OWNER, item.PHONE_ID, establecimiento.EST_ID, item.ENT_ID);
                        if (proxyTelObtener == null)
                        {
                            var codigoGenAdd = proxyTel.Insertar(item);
                            var result = proxyTelEst.Insertar(new BETelefonoEst
                            {
                                EST_ID = Convert.ToInt32(establecimiento.EST_ID),
                                PHONE_ID = codigoGenAdd,
                                OWNER = establecimiento.OWNER,
                                LOG_USER_CREAT = establecimiento.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion de la direccion 
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyTel.Update(item);
                        }
                    }
                }
                /// se elimina las telefonos
                if (telEliminar != null)
                {
                    // parEliminar.ForEach(x => { proxyPar.Eliminar(bps.OWNER, x.PAR_ID, bps.LOG_USER_UPDATE); });
                    foreach (var item in telEliminar)
                    {
                        proxyTel.Eliminar(establecimiento.OWNER, item.PHONE_ID, establecimiento.LOG_USER_UPDAT);
                    }
                }
                if (listTelActivar != null)
                {
                    listTelActivar.ForEach(x => { proxyTel.Activar(establecimiento.OWNER, x.PHONE_ID, establecimiento.LOG_USER_UPDAT); });
                }

                ///logica de negocio para actualizar correos
                DACorreo proxyMail = new DACorreo();
                DACorreoEst proxyMailEst = new DACorreoEst();

                if (establecimiento.Correos != null)
                {
                    foreach (var item in establecimiento.Correos)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva Observacion
                        BECorreo proxyMailObtener = proxyMail.ObtenerMailEst(establecimiento.OWNER, item.MAIL_ID, establecimiento.EST_ID, item.ENT_ID);
                        if (proxyMailObtener == null)
                        {
                            var codigoGenAdd = proxyMail.Insertar(item);
                            var result = proxyMailEst.Insertar(new BECorreoEst
                            {
                                EST_ID = Convert.ToInt32(establecimiento.EST_ID),
                                MAIL_ID = codigoGenAdd,
                                OWNER = establecimiento.OWNER,
                                LOG_USER_CREAT = establecimiento.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion de la direccion 
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyMail.Update(item);
                        }
                    }
                }
                ///// se elimina las mail
                if (mailEliminar != null)
                {
                    // parEliminar.ForEach(x => { proxyPar.Eliminar(bps.OWNER, x.PAR_ID, bps.LOG_USER_UPDATE); });
                    foreach (var item in mailEliminar)
                    {
                        proxyMail.Eliminar(establecimiento.OWNER, item.MAIL_ID, establecimiento.LOG_USER_UPDAT);
                    }
                }
                if (listMailActivar != null)
                {
                    //foreach (var item in listDocActivar)
                    //{
                    //    proxyDoc.Activar(bps.OWNER, item.DOC_ID, bps.LOG_USER_UPDATE);
                    //}
                    listMailActivar.ForEach(x => { proxyMail.Activar(establecimiento.OWNER, x.MAIL_ID, establecimiento.LOG_USER_UPDAT); });
                }

                /////logica de negocio para actualizar Redes Sociales
                DARedSocial proxyRedSocial = new DARedSocial();
                DARedSocialEst proxyRedSocialEst = new DARedSocialEst();

                if (establecimiento.RedSocial != null)
                {
                    foreach (var item in establecimiento.RedSocial)
                    {
                        //verifica si  no existe la Observacion para el socio
                        //si no existe se registra y asocia la nueva Observacion
                        BERedes_Sociales proxyRedSocialObtener = proxyRedSocial.ObtenerRedSocialEST(establecimiento.OWNER, item.CONT_ID, establecimiento.EST_ID, item.ENT_ID);
                        if (proxyRedSocialObtener == null)
                        {
                            var codigoGenAdd = proxyRedSocial.Insertar(item);
                            var result = proxyRedSocialEst.Insertar(new BERedSocialEst
                            {
                                EST_ID = Convert.ToInt32(establecimiento.EST_ID),
                                CONT_ID = codigoGenAdd,
                                OWNER = establecimiento.OWNER,
                                LOG_USER_CREAT = establecimiento.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion de la direccion 
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            var result = proxyRedSocial.Update(item);
                        }
                    }
                }
                ///// se elimina las redes sociales
                if (RedSocialEliminar != null)
                {
                    // parEliminar.ForEach(x => { proxyPar.Eliminar(bps.OWNER, x.PAR_ID, bps.LOG_USER_UPDATE); });
                    foreach (var item in RedSocialEliminar)
                    {
                        proxyRedSocial.Eliminar(establecimiento.OWNER, item.CONT_ID, establecimiento.LOG_USER_UPDAT);
                    }
                }
                if (listRedSocialActivar != null)
                {
                    listRedSocialActivar.ForEach(x => { proxyRedSocial.Activar(establecimiento.OWNER, x.CONT_ID, establecimiento.LOG_USER_UPDAT); });
                }

                DACaracteristicaEst proxyCar = new DACaracteristicaEst();
                if (establecimiento.Caracteristicas != null)
                {
                    foreach (var item in establecimiento.Caracteristicas)
                    {
                        BECaracteristicaEst ent = proxyCar.ObtenerCarEst(establecimiento.OWNER, item.CHAR_ID, establecimiento.EST_ID);

                        if (ent == null)
                        {
                            item.EST_ID = establecimiento.EST_ID;
                            var codigoGenAdd = proxyCar.Insertar(item);
                        }
                        else if (ent.VALUE != item.VALUE)
                        {
                            item.EST_ID = establecimiento.EST_ID;
                            item.LOG_USER_UPDAT = item.LOG_USER_CREAT;
                            var result = proxyCar.Actualizar(item);
                        }
                    }
                }
                if (carEliminar != null)
                {
                    foreach (var item in carEliminar)
                    {
                        proxyCar.Eliminar(establecimiento.OWNER, item.CHAR_ID, establecimiento.LOG_USER_UPDAT);
                    }
                }
                if (listCarActivar != null)
                {
                    listCarActivar.ForEach(x => { proxyCar.Activar(establecimiento.OWNER, x.CHAR_ID, establecimiento.LOG_USER_UPDAT); });
                }


                DAAsociadosEst proxyAso = new DAAsociadosEst();
                if (establecimiento.Asociados != null)
                {
                    foreach (var item in establecimiento.Asociados)
                    {
                        BEAsociadosEst ent = proxyAso.ObtenerAsoEst(establecimiento.OWNER, item.Id, establecimiento.EST_ID);
                        if (ent == null)
                        {
                            item.EST_ID = establecimiento.EST_ID;
                            var result = proxyAso.Insertar(item);
                        }
                        else if (ent.BPS_ID != item.BPS_ID || ent.ROL_ID != item.ROL_ID || ent.BPS_MAIN != item.BPS_MAIN)
                        {
                            item.OWNER = GlobalVars.Global.OWNER;
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            item.EST_ID = establecimiento.EST_ID;
                            var result = proxyAso.Update(item);
                        }
                    }
                }

                if (asoEliminar != null)
                {
                    asoEliminar.ForEach(x => { proxyAso.Eliminar(establecimiento.OWNER, x.Id, establecimiento.EST_ID, establecimiento.LOG_USER_UPDAT); });
                }
                if (listAsociadoActivar != null)
                {
                    listAsociadoActivar.ForEach(x => { proxyAso.Activar(establecimiento.OWNER, x.Id, establecimiento.EST_ID, establecimiento.LOG_USER_UPDAT); });
                }



                //DADivisionesEst proxyDiv = new DADivisionesEst();
                //if (establecimiento.Divisiones != null)
                //{
                //    foreach (var item in establecimiento.Divisiones)
                //    {
                //        BEDivisionesEst proxyDivObtener = proxyDiv.ObtenerDivEst(establecimiento.OWNER, establecimiento.EST_ID, item.idDIVISIONVAL);
                //        if (proxyDivObtener == null)
                //        {
                //            item.EST_ID = establecimiento.EST_ID;
                //            var result = proxyDiv.Insertar(item);
                //        }
                //        else
                //        {
                //            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                //            item.EST_ID = establecimiento.EST_ID;
                //            var result = proxyDiv.Actualizar(item);
                //        }
                //    }
                //}

                //if (divEliminar != null)
                //{
                //    divEliminar.ForEach(x => { proxyDiv.Eliminar(establecimiento.OWNER, establecimiento.EST_ID, x.Id, establecimiento.LOG_USER_UPDAT); });
                //}
                //if (listDivisionActivar != null)
                //{
                //    listDivisionActivar.ForEach(x => { proxyDiv.Activar(establecimiento.OWNER, establecimiento.EST_ID, x.Id, establecimiento.LOG_USER_UPDAT); });
                //}


                DADifusionEst proxyDif = new DADifusionEst();
                if (establecimiento.Difusion != null)
                {
                    foreach (var item in establecimiento.Difusion)
                    {
                        BEDifusionEst proxyDifObtener = proxyDif.ObtenerDifEst(establecimiento.OWNER, establecimiento.EST_ID, item.SEQUENCE);
                        if (proxyDifObtener == null)
                        {
                            item.EST_ID = establecimiento.EST_ID;
                            var result = proxyDif.Insertar(item);
                        }
                        else
                        {
                            item.LOG_USER_UPDAT = item.LOG_USER_CREAT;
                            item.EST_ID = establecimiento.EST_ID;
                            var result = proxyDif.Actualizar(item);
                        }
                    }
                }
                if (difEliminar != null)
                {
                    difEliminar.ForEach(x => { proxyDif.Eliminar(establecimiento.OWNER, establecimiento.EST_ID, x.SEQUENCE, establecimiento.LOG_USER_UPDAT); });
                }
                if (listDifusionActivar != null)
                {
                    listDifusionActivar.ForEach(x => { proxyDif.Activar(establecimiento.OWNER, establecimiento.EST_ID, x.SEQUENCE, establecimiento.LOG_USER_UPDAT); });
                }

                #region Inspeccion
                //logica de negocio para actualizar Inscepccion
                //DAInspectionEst proxyIns = new DAInspectionEst();
                //if (establecimiento.Inspection != null)
                //{
                //    foreach (var item in establecimiento.Inspection)
                //    {
                //        ///verifica si  no existe la inspeccion para el establecimiento
                //        ///si no existe se registra y asocia la nueva inspeccion
                //        BEInspectionEst proxyInsObtener = proxyIns.Obtener(establecimiento.OWNER, item.INSP_ID, establecimiento.EST_ID);
                //        if (proxyInsObtener == null)
                //        {
                //            item.EST_ID = establecimiento.EST_ID;
                //            item.BPS_ID = establecimiento.BPS_ID;
                //            var codigoGenAdd = proxyIns.Insertar(item);
                //        }
                //        else
                //        {
                //            ///sino  solo se actualiza la informacion de la direccion 
                //            item.EST_ID = establecimiento.EST_ID;
                //            item.LOG_USER_UPDAT = item.LOG_USER_CREAT;
                //            var result = proxyIns.Actualizar(item);
                //        }
                //    }
                //}
                //se elimina las inspection
                //if (insEliminar != null)
                //{
                //    // parEliminar.ForEach(x => { proxyPar.Eliminar(bps.OWNER, x.PAR_ID, bps.LOG_USER_UPDATE); });
                //    foreach (var item in insEliminar)
                //    {
                //        proxyIns.Eliminar(establecimiento.OWNER, item.INSP_ID, establecimiento.LOG_USER_UPDAT);
                //    }
                //}
                //if (listInsActivar != null)
                //{
                //    //foreach (var item in listDocActivar)
                //    //{
                //    //    proxyDoc.Activar(bps.OWNER, item.DOC_ID, bps.LOG_USER_UPDATE);
                //    //}
                //    listInsActivar.ForEach(x => { proxyIns.Activar(establecimiento.OWNER, x.INSP_ID, establecimiento.LOG_USER_UPDAT); });
                //} 
                #endregion

                transa.Complete();
            }
            return upd;
        }

        public int InactivarEstablecimiento(BEEstablecimiento en)
        {
            return new DAEstablecimiento().InactivarEstablecimiento(en);
        }

        public List<BEEstablecimiento> ConsultaGeneralEstablecimiento(decimal Tipoestablecimiento,
                                        decimal? SubTipoestableimiento,
                                        string Nombreestablecimiento,
                                        decimal Socio,
                                        string Tipodivision,
                                        decimal? Division,
                                        int estado,
                                        decimal oficina,
                                        int pagina,
                                        int cantRegxPag)
        {
            var datos = new DAEstablecimiento().ConsultaGeneralEstablecimiento(Tipoestablecimiento, SubTipoestableimiento, Nombreestablecimiento, Socio, Tipodivision, Division, estado, oficina, pagina, cantRegxPag);
            return datos;
        }

        public List<BEEstablecimiento> ConsultaEstablecimiento(string owner, decimal idEst, string nombre, decimal idSoc,
                                                   decimal tipo, decimal subTipo, decimal idDivision,
                                                   decimal ubigeo, decimal pagina, decimal cantRegistro)
        {
            return new DAEstablecimiento().ConsultaEstablecimiento(owner, idEst, nombre, idSoc,
                                                                    tipo, subTipo, idDivision,
                                                                    ubigeo, pagina, cantRegistro);
        }

        public BEEstablecimiento CabeceraConsultaEst(decimal idEstablecimiento, string owner, decimal tipoEntidad)
        {
            decimal? idEst = idEstablecimiento;
            var objEstablecimiento = new DAEstablecimiento().CabeceraConsultaEst(idEstablecimiento, owner);
            if (objEstablecimiento != null)
            {
                objEstablecimiento.UBIGEO = new DAUbigeo().ObtenerDescripcion(objEstablecimiento.TIS_N, objEstablecimiento.GEO_ID).NOMBRE_UBIGEO;
                objEstablecimiento.Direccion = new DADirecciones().DireccionXEstablecimiento(objEstablecimiento.EST_ID, owner, tipoEntidad);
                objEstablecimiento.Caracteristicas = new DACaracteristicaEst().CaracteristicaXEstablecimiento(objEstablecimiento.EST_ID, owner);
                objEstablecimiento.Parametros = new DAParametroGral().ParametroXEstablecimiento(objEstablecimiento.EST_ID, owner, tipoEntidad);
                objEstablecimiento.Asociados = new DAAsociadosEst().AsociadoXEstablecimiento(objEstablecimiento.EST_ID, owner);
                objEstablecimiento.Licencias = new DALicencias().usp_Get_LicenciaPage(owner, idEst,0, 1, 1000);
                objEstablecimiento.Observaciones = new DAObservationGral().ObservacionXEstablecimiento(objEstablecimiento.EST_ID, owner, tipoEntidad);
                objEstablecimiento.Documentos = new DADocumentoGral().DocumentoXEstablecimiento(objEstablecimiento.EST_ID, owner, tipoEntidad);
                objEstablecimiento.Difusion = new DADifusionEst().DifusionXEstablecimiento(owner, objEstablecimiento.EST_ID);
            }
            return objEstablecimiento;
        }
        //CONSULTAR ESTABLECIMIENTOS 
        public List<BEEstablecimiento> ConsultaEstablecimientoSocioEmpr(
                                      decimal Socio,
                                      int pagina,
                                      int cantRegxPag)
        {
            var datos = new DAEstablecimiento().ConsultaEstablecimientoSocioEmpr(Socio, pagina, cantRegxPag);
            return datos;
        }



        //Validar Establecimiento david
        public int ValidarEstablecimientoCaracteristica(int IdEstablecimiento)
        {
            return new DAEstablecimiento().ValidarEstablecimientoCaracteristicas(IdEstablecimiento);
        }


        //david
        #region cadenas
        #region establecimientoSocioEmpresarial
        //CONSULTAR ESTABLECIMIENTOS 
        public List<BEEstablecimiento> ConsultaEstablecimientoSocioEmpr(decimal Socio, decimal? licmas)
        {
            var datos = new DAEstablecimiento().ConsultaEstablecimientoSocioEmpresarial(Socio, licmas);
            return datos;
        }

        public List<BEEstablecimiento> ConsultaEstablecimientoSocioEmprGrabado(decimal Socio, decimal licmas)
        {
            var datos = new DAEstablecimiento().ConsultaEstablecimientoSocioEmpresarialGrabados(Socio, licmas);
            return datos;
        }


        #endregion

        #region CaracteristicasPredefinidas
        //Lista CaracteristicasxEstablecimiento
        public List<BECaracteristicaEst> ListaCaracteristicasxEstablecimiento(decimal idTipodeEstablecimiento, decimal idSubtipoEstablecimiento)
        {
            return new DAEstablecimiento().Listar_Caracteristicas_PredefinidasxEst(idTipodeEstablecimiento, idSubtipoEstablecimiento);
        }

        public List<BECaracteristicaLic> ListarCaracteristicaRegxLic(decimal IdLic)
        {
            return new DAEstablecimiento().ListaDecarcatxLic(IdLic);
        }
        #endregion



        #endregion

        #region Descuentos Socio
        public int ValidaEstablecimientoModif(decimal ESTID)
        {

            return new DAEstablecimiento().ValidaEstablecimientoModif(ESTID);
        }
        #endregion

        public List<BEEstablecimiento> ObtenerEstablecimientoxRuc(string ruc)
        {
            return new DAEstablecimiento().ObtenerEstablecimientoxRuc(ruc);
        }

        public bool AgruparEstablecimiento(string owner, List<BEEstablecimiento> Establecimiento, decimal est_origen)
        {
            var exito = false;
            foreach (var item in Establecimiento)
            {
                exito = new DAEstablecimiento().AgruparEstablecimiento(owner, est_origen, item.EST_ID);
            }
            return exito;
        }
    }
}
