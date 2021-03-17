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
    public class BLSocioNegocio
    {
        /// <summary>
        /// Funcion para Validar si el socio de negocio tiene asignado un numero de documento
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="bps"></param>
        /// <returns></returns>
        public bool ValidarTieneDocumento(string owner, decimal bps)
        {
            return new DASocioNegocio().ValidarTieneDocumento(owner, bps);
        }
        public List<SocioNegocio> USP_SOCIOS_LISTARPAGE(decimal tipo, string nro_tipo, string nombre, int pagina, int cantRegxPag, string BPS_USER, string BPS_ASSOCIATION, string BPS_GROUP, string BPS_COLLECTOR, string BPS_SUPPLIER, string BPS_EMPLOYEE, int estado)
        {
            var datos = new DASocioNegocio().USP_SOCIOS_LISTARPAGE(tipo, nro_tipo, nombre, pagina, cantRegxPag, BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE, estado);

            datos.ForEach(e =>
            {
                var ue = new BLUsuarioEmpleado().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var ud = new BLUsuarioDerecho().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var ua = new BLUsuarioAsociacion().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var up = new BLUsuarioProveedor().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var ur = new BLUsuarioRecaudador().ObtenerSinRelacion(e.BPS_ID, e.OWNER);

                if (ue != null)
                {
                    if (ue.ENDS.HasValue)
                    {
                        e.BPS_EMPLOYEE = '2';
                    }
                }
                if (ud != null)
                {
                    if (ud.ENDS.HasValue)
                    {
                        e.BPS_USER = '2';
                    }
                }
                if (ua != null)
                {
                    if (ua.ENDS.HasValue)
                    {
                        e.BPS_ASSOCIATION = '2';
                    }
                }
                if (up != null)
                {
                    if (up.ENDS.HasValue)
                    {
                        e.BPS_SUPPLIER = '2';
                    }
                }
                if (ur != null)
                {
                    if (ur.ENDS.HasValue)
                    {
                        e.BPS_COLLECTOR = '2';
                    }
                }
            });
            return datos;
        }
        public SocioNegocio ObtenerSocioDocumento(string owner, decimal tipo, string nro_tipo)
        {
            return new DASocioNegocio().ObtenerSocioDocumento(owner, tipo, nro_tipo);
        }

        public List<SocioNegocio> UPS_BUSCAR_SOCIOS_X_RAZONSOCIAL(string owner, string datos)
        {
            return new DASocioNegocio().UPS_BUSCAR_SOCIOS_X_RAZONSOCIAL(owner, datos);
        }

        public int Insertar(SocioNegocio bps)
        {
            var codigoGen = 0;
            using (TransactionScope transa = new TransactionScope())
            {

                codigoGen = new DASocioNegocio().Insertar(bps);

                if (bps.Observaciones != null)
                {
                    foreach (var observacion in bps.Observaciones)
                    {
                        var codigoGenObs = new DAObservationGral().InsertarObsGrl(observacion);
                        var result = new DAObservationBps().Insertar(new BEObservationBps
                        {
                            BPS_ID = codigoGen,
                            OBS_ID = codigoGenObs,
                            OWNER = bps.OWNER,
                            LOG_USER_CREAT = bps.LOG_USER_CREAT
                        });
                    }
                }
                if (bps.Parametros != null)
                {
                    foreach (var parametro in bps.Parametros)
                    {
                        var codigoGenParam = new DAParametroGral().Insertar(parametro);
                        var result = new DAParametroBps().Insertar(new BEParametroBps
                        {
                            BPS_ID = codigoGen,
                            PAR_ID = codigoGenParam,
                            OWNER = bps.OWNER,
                            LOG_USER_CREAT = bps.LOG_USER_CREAT
                        });
                    }
                }
                if (bps.Documentos != null)
                {
                    foreach (var documento in bps.Documentos)
                    {
                        var codigoGenDoc = new DADocumentoGral().Insertar(documento);
                        var result = new DADocumentoBps().Insertar(new BEDocumentoBps
                        {
                            BPS_ID = codigoGen,
                            DOC_ID = codigoGenDoc,
                            OWNER = bps.OWNER,
                            LOG_USER_CREAT = bps.LOG_USER_CREAT
                        });
                    }
                }
                if (bps.Direcciones != null)
                {
                    foreach (var direccion in bps.Direcciones)
                    {
                        var codigoGenAdd = new DADirecciones().Insertar(direccion);
                        var result = new DADireccionBps().Insertar(new BEDireccionBps
                        {
                            BPS_ID = codigoGen,
                            ADD_ID = codigoGenAdd,
                            OWNER = bps.OWNER,
                            LOG_USER_CREAT = bps.LOG_USER_CREAT
                        });
                    }
                }

                if (bps.Telefonos != null)
                {
                    foreach (var telefono in bps.Telefonos)
                    {
                        var codigoGenDoc = new DATelefono().Insertar(telefono);
                        var result = new DATelefonoBps().Insertar(new BETelefonoBps
                        {
                            BPS_ID = codigoGen,
                            PHONE_ID = codigoGenDoc,
                            OWNER = bps.OWNER,
                            LOG_USER_CREAT = bps.LOG_USER_CREAT
                        });
                    }
                }

                if (bps.Correos != null)
                {
                    foreach (var correo in bps.Correos)
                    {
                        var codigoGenDoc = new DACorreo().Insertar(correo);
                        var result = new DACorreoBps().Insertar(new BECorreoBps
                        {
                            BPS_ID = codigoGen,
                            MAIL_ID = codigoGenDoc,
                            OWNER = bps.OWNER,
                            LOG_USER_CREAT = bps.LOG_USER_CREAT
                        });
                    }
                }

                if (bps.RedSocial != null)
                {
                    foreach (var redsocial in bps.RedSocial)
                    {
                        var codigoGenDoc = new DARedSocial().Insertar(redsocial);
                        var result = new DARedSocialBps().Insertar(new BERedSocialBps
                        {
                            BPS_ID = codigoGen,
                            CONT_ID = codigoGenDoc,
                            OWNER = bps.OWNER,
                            LOG_USER_CREAT = bps.LOG_USER_CREAT
                        });
                    }
                }
                transa.Complete();
            }
            return codigoGen;
        }

        public int Actualizar(SocioNegocio bps,
                                List<BEDireccion> dirEliminar, List<BEDireccion> listDirActivar,
                                List<BEObservationGral> obsEliminar, List<BEObservationGral> listObsActivar,
                                List<BEDocumentoGral> docEliminar, List<BEDocumentoGral> listDocActivar,
                                List<BEParametroGral> parEliminar, List<BEParametroGral> listParActivar,
                                List<BETelefono> telEliminar, List<BETelefono> listTelActivar,
                                List<BECorreo> mailEliminar, List<BECorreo> listMailActivar,
                                List<BERedes_Sociales> RedSocialEliminar, List<BERedes_Sociales> listRedSocialActivar
                                )
        {

            int upd = 0;

            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DASocioNegocio().Actualizar(bps);

                DADirecciones proxyDir = new DADirecciones();
                DADireccionBps prxyDirBps = new DADireccionBps();

                if (bps.Direcciones != null)
                {
                    //DADirecciones proxyDir = new DADirecciones();
                    //DADireccionBps prxyDirBps = new DADireccionBps();
                    foreach (var direccion in bps.Direcciones)
                    {
                        ///verifica si  no existe la direccion para el socio
                        ///si no existe se registra y asocia la nueva direcion
                        BEDireccion proxyDirObtener = proxyDir.ObtenerDirBPS(bps.OWNER, direccion.ADD_ID, bps.BPS_ID, direccion.ENT_ID);
                        if (proxyDirObtener == null)
                        {
                            var codigoGenAdd = proxyDir.Insertar(direccion);
                            var result = prxyDirBps.Insertar(new BEDireccionBps
                            {
                                BPS_ID = bps.BPS_ID,
                                ADD_ID = codigoGenAdd,
                                OWNER = bps.OWNER,
                                LOG_USER_CREAT = bps.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            ///sino  solo se actualiza la informacion de la direccion 
                            var result = proxyDir.Update(direccion);
                        }
                    }
                }
                /// se elimina las direcciones
                if (dirEliminar != null)
                {
                    //dirEliminar.ForEach(x => { proxyDir.Eliminar(bps.OWNER, x.ADD_ID, bps.LOG_USER_CREAT); });
                    foreach (var item in dirEliminar)
                    {
                        proxyDir.Eliminar(bps.OWNER, item.ADD_ID, bps.LOG_USER_CREAT);
                    }
                }
                /// activa las direcciones
                if (listDirActivar != null)
                {
                    foreach (var item in listDirActivar)
                    {
                        proxyDir.Activar(bps.OWNER, item.ADD_ID, bps.LOG_USER_UPDATE);
                    }

                    //listDirActivar.ForEach(x => { proxyDir.Activar(bps.OWNER, x.ADD_ID, bps.LOG_USER_UPDATE); });

                }
                // }
                ///logica de negocio para actualizar Observaciones
                DAObservationGral proxyObs = new DAObservationGral();
                DAObservationBps proxyObsBps = new DAObservationBps();

                if (bps.Observaciones != null)
                {
                    foreach (var item in bps.Observaciones)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva Observacion
                        BEObservationGral proxyObsObtener = proxyObs.ObtenerObsBPS(bps.OWNER, item.OBS_ID, bps.BPS_ID, item.ENT_ID);
                        if (proxyObsObtener == null)
                        {
                            var codigoGenAdd = proxyObs.InsertarObsGrl(item);
                            var result = proxyObsBps.Insertar(new BEObservationBps
                            {
                                BPS_ID = Convert.ToInt32(bps.BPS_ID),
                                OBS_ID = codigoGenAdd,
                                OWNER = bps.OWNER,
                                LOG_USER_CREAT = bps.LOG_USER_CREAT
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
                    //dirEliminar.ForEach(x => { proxyDir.Eliminar(bps.OWNER, x.ADD_ID, bps.LOG_USER_CREAT); });
                    foreach (var item in obsEliminar)
                    {
                        proxyObs.Eliminar(bps.OWNER, item.OBS_ID, bps.LOG_USER_UPDATE);
                    }
                }
                /// activa las direcciones
                if (listObsActivar != null)
                {
                    foreach (var item in listObsActivar)
                    {
                        proxyObs.Activar(bps.OWNER, item.OBS_ID, bps.LOG_USER_UPDATE);
                    }
                    //listDirActivar.ForEach(x => { proxyDir.Activar(bps.OWNER, x.ADD_ID, bps.LOG_USER_UPDATE); });
                }

                ///logica de negocio para actualizar documentos
                DADocumentoGral proxyDoc = new DADocumentoGral();
                DADocumentoBps proxyDocBps = new DADocumentoBps();

                if (bps.Documentos != null)
                {
                    foreach (var item in bps.Documentos)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva Observacion
                        BEDocumentoGral proxyDocObtener = proxyDoc.ObtenerDocBPS(bps.OWNER, item.DOC_ID, bps.BPS_ID, item.ENT_ID);
                        if (proxyDocObtener == null)
                        {
                            var codigoGenAdd = proxyDoc.Insertar(item);
                            var result = proxyDocBps.Insertar(new BEDocumentoBps
                            {
                                BPS_ID = Convert.ToInt32(bps.BPS_ID),
                                DOC_ID = codigoGenAdd,
                                OWNER = bps.OWNER,
                                LOG_USER_CREAT = bps.LOG_USER_CREAT
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
                /// se elimina las direcciones
                if (docEliminar != null)
                {
                    docEliminar.ForEach(x => { proxyDoc.Eliminar(bps.OWNER, x.DOC_ID, bps.LOG_USER_UPDATE); });
                    //foreach (var item in docEliminar)
                    //{
                    //    proxyDoc.Eliminar(bps.OWNER, item.DOC_ID, bps.LOG_USER_UPDATE);
                    //}
                }
                if (listDocActivar != null)
                {
                    //foreach (var item in listDocActivar)
                    //{
                    //    proxyDoc.Activar(bps.OWNER, item.DOC_ID, bps.LOG_USER_UPDATE);
                    //}
                    listDocActivar.ForEach(x => { proxyDoc.Activar(bps.OWNER, x.DOC_ID, bps.LOG_USER_UPDATE); });
                }

                ///logica de negocio para actualizar parametro
                DAParametroGral proxyPar = new DAParametroGral();
                DAParametroBps proxyParBps = new DAParametroBps();

                if (bps.Parametros != null)
                {
                    foreach (var item in bps.Parametros)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva Observacion
                        BEParametroGral proxyParObtener = proxyPar.ObtenerParBPS(bps.OWNER, item.PAR_ID, bps.BPS_ID, item.ENT_ID);
                        if (proxyParObtener == null)
                        {
                            var codigoGenAdd = proxyPar.Insertar(item);
                            var result = proxyParBps.Insertar(new BEParametroBps
                            {
                                BPS_ID = Convert.ToInt32(bps.BPS_ID),
                                PAR_ID = codigoGenAdd,
                                OWNER = bps.OWNER,
                                LOG_USER_CREAT = bps.LOG_USER_CREAT
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
                /// se elimina las direcciones
                if (parEliminar != null)
                {
                    // parEliminar.ForEach(x => { proxyPar.Eliminar(bps.OWNER, x.PAR_ID, bps.LOG_USER_UPDATE); });
                    foreach (var item in parEliminar)
                    {
                        proxyPar.Eliminar(bps.OWNER, item.PAR_ID, bps.LOG_USER_UPDATE);
                    }
                }
                if (listParActivar != null)
                {
                    //foreach (var item in listDocActivar)
                    //{
                    //    proxyDoc.Activar(bps.OWNER, item.DOC_ID, bps.LOG_USER_UPDATE);
                    //}
                    listParActivar.ForEach(x => { proxyPar.Activar(bps.OWNER, x.PAR_ID, bps.LOG_USER_UPDATE); });
                }

                ///logica de negocio para actualizar telefono
                DATelefono proxyTel = new DATelefono();
                DATelefonoBps proxyTelBps = new DATelefonoBps();

                if (bps.Telefonos != null)
                {
                    foreach (var item in bps.Telefonos)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva Observacion
                        BETelefono proxyTelObtener = proxyTel.ObtenerTelBPS(bps.OWNER, item.PHONE_ID, bps.BPS_ID, item.ENT_ID);
                        if (proxyTelObtener == null)
                        {
                            var codigoGenAdd = proxyTel.Insertar(item);
                            var result = proxyTelBps.Insertar(new BETelefonoBps
                            {
                                BPS_ID = Convert.ToInt32(bps.BPS_ID),
                                PHONE_ID = codigoGenAdd,
                                OWNER = bps.OWNER,
                                LOG_USER_CREAT = bps.LOG_USER_CREAT
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
                        proxyTel.Eliminar(bps.OWNER, item.PHONE_ID, bps.LOG_USER_UPDATE);
                    }
                }
                if (listTelActivar != null)
                {
                    //foreach (var item in listDocActivar)
                    //{
                    //    proxyDoc.Activar(bps.OWNER, item.DOC_ID, bps.LOG_USER_UPDATE);
                    //}
                    listTelActivar.ForEach(x => { proxyTel.Activar(bps.OWNER, x.PHONE_ID, bps.LOG_USER_UPDATE); });
                }

                ///logica de negocio para actualizar correos
                DACorreo proxyMail = new DACorreo();
                DACorreoBps proxyMailBps = new DACorreoBps();

                if (bps.Correos != null)
                {
                    foreach (var item in bps.Correos)
                    {
                        ///verifica si  no existe la Observacion para el socio
                        ///si no existe se registra y asocia la nueva Observacion
                        BECorreo proxyMailObtener = proxyMail.ObtenerMailBPS(bps.OWNER, item.MAIL_ID, bps.BPS_ID, item.ENT_ID);
                        if (proxyMailObtener == null)
                        {
                            var codigoGenAdd = proxyMail.Insertar(item);
                            var result = proxyMailBps.Insertar(new BECorreoBps
                            {
                                BPS_ID = Convert.ToInt32(bps.BPS_ID),
                                MAIL_ID = codigoGenAdd,
                                OWNER = bps.OWNER,
                                LOG_USER_CREAT = bps.LOG_USER_CREAT
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
                /// se elimina las telefonos
                if (mailEliminar != null)
                {
                    // parEliminar.ForEach(x => { proxyPar.Eliminar(bps.OWNER, x.PAR_ID, bps.LOG_USER_UPDATE); });
                    foreach (var item in mailEliminar)
                    {
                        proxyMail.Eliminar(bps.OWNER, item.MAIL_ID, bps.LOG_USER_UPDATE);
                    }
                }
                if (listMailActivar != null)
                {
                    //foreach (var item in listDocActivar)
                    //{
                    //    proxyDoc.Activar(bps.OWNER, item.DOC_ID, bps.LOG_USER_UPDATE);
                    //}
                    listMailActivar.ForEach(x => { proxyMail.Activar(bps.OWNER, x.MAIL_ID, bps.LOG_USER_UPDATE); });
                }

                ///logica de negocio para actualizar Redes Sociales
                DARedSocial proxyRedSocial = new DARedSocial();
                DARedSocialBps proxyRedSocialBps = new DARedSocialBps();

                if (bps.RedSocial != null)
                {
                    foreach (var item in bps.RedSocial)
                    {
                        //verifica si  no existe la Observacion para el socio
                        //si no existe se registra y asocia la nueva Observacion
                        BERedes_Sociales proxyRedSocialObtener = proxyRedSocial.ObtenerRedSocialBPS(bps.OWNER, item.CONT_ID, bps.BPS_ID, item.ENT_ID);
                        if (proxyRedSocialObtener == null)
                        {
                            var codigoGenAdd = proxyRedSocial.Insertar(item);
                            var result = proxyRedSocialBps.Insertar(new BERedSocialBps
                            {
                                BPS_ID = Convert.ToInt32(bps.BPS_ID),
                                CONT_ID = codigoGenAdd,
                                OWNER = bps.OWNER,
                                LOG_USER_CREAT = bps.LOG_USER_CREAT
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
                /// se elimina las telefonos
                if (RedSocialEliminar != null)
                {
                    // parEliminar.ForEach(x => { proxyPar.Eliminar(bps.OWNER, x.PAR_ID, bps.LOG_USER_UPDATE); });
                    foreach (var item in RedSocialEliminar)
                    {
                        proxyRedSocial.Eliminar(bps.OWNER, item.CONT_ID, bps.LOG_USER_UPDATE);
                    }
                }
                if (listRedSocialActivar != null)
                {
                    listRedSocialActivar.ForEach(x => { proxyRedSocial.Activar(bps.OWNER, x.CONT_ID, bps.LOG_USER_UPDATE); });
                }
                transa.Complete();
            }
            return upd;
        }

        public int Eliminar(SocioNegocio bps)
        {
            return new DASocioNegocio().Eliminar(bps);
        }

        /// <summary>
        /// addon dbs 20140727
        /// </summary>
        /// <param name="codigoBps"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public SocioNegocio Obtener(decimal codigoBps, string owner, decimal tipoEntidad)
        {
            var objSocio = new DASocioNegocio().Obtener(codigoBps, owner);
            if (objSocio != null)
            {
                objSocio.Observaciones = new DAObservationGral().ObservacionXSocio(objSocio.BPS_ID, objSocio.OWNER, tipoEntidad);
                objSocio.Parametros = new DAParametroGral().ParametroXSocio(objSocio.BPS_ID, objSocio.OWNER, tipoEntidad);
                objSocio.Documentos = new DADocumentoGral().DocumentoXSocio(objSocio.BPS_ID, objSocio.OWNER, tipoEntidad);
                objSocio.Direcciones = new DADirecciones().DireccionXSocio(objSocio.BPS_ID, objSocio.OWNER, tipoEntidad);
                objSocio.Telefonos = new DATelefono().TelefonoXSocio(objSocio.BPS_ID, objSocio.OWNER, tipoEntidad);
                objSocio.Correos = new DACorreo().CorreoXSocio(objSocio.BPS_ID, objSocio.OWNER, tipoEntidad);
                objSocio.RedSocial = new DARedSocial().RedSocialXSocio(objSocio.BPS_ID, objSocio.OWNER, tipoEntidad);
            }

            return objSocio;
        }
        /// <summary>
        /// OBTIENE DATOS DEL SOCIO SIN LAS RELACIONES DE OBSERVACION,PARAMETRO,DOCUMENTO,CORREO,TELEFONO
        /// </summary>
        /// <param name="codigoBps"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public SocioNegocio ObtenerDatos(decimal codigoBps, string owner)
        {
            var objSocio = new DASocioNegocio().Obtener(codigoBps, owner);

            return objSocio;
        }
        public List<SocioNegocio> ObtenerGrupoEmp(string Owner)
        {

            return new DASocioNegocio().ObtenerGrupoEmp(Owner);
        }

        public SocioNegocio BuscarXtipodocumento(decimal idTipoDocumento, string nroDocumento)
        {
            return new DASocioNegocio().BuscarXtipodocumento(idTipoDocumento, nroDocumento);
        }

        public List<SocioNegocio> AutoCompletarRecaudadores(string Owner, string datos)
        {
            return new DASocioNegocio().AutoCompletarRecaudadores(Owner, datos);
        }

        public SocioNegocio BuscarXtipodocumentoRecaudador(decimal idTipoDocumento, string nroDocumento)
        {
            return new DASocioNegocio().BuscarXtipodocumentoRecaudador(idTipoDocumento, nroDocumento);
        }

        public List<SocioNegocio> BuscarSocio(string tipoPersona,
                                                decimal? tipoId,
                                                string nro_tipo,
                                                string nombre,
                                                decimal ubigeo,
                                                int estado,
                                                int pagina,
                                                int cantRegxPag,
            string BPS_USER, string BPS_ASSOCIATION, string BPS_GROUP, string BPS_COLLECTOR, string BPS_SUPPLIER, string BPS_EMPLOYEE)
        {
            var datos = new DASocioNegocio().BuscarSocio(tipoPersona, tipoId, nro_tipo, nombre, ubigeo, estado, pagina, cantRegxPag, BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE);


            datos.ForEach(e =>
            {
                var ue = new BLUsuarioEmpleado().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var ud = new BLUsuarioDerecho().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var ua = new BLUsuarioAsociacion().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var up = new BLUsuarioProveedor().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var ur = new BLUsuarioRecaudador().ObtenerSinRelacion(e.BPS_ID, e.OWNER);

                if (ue != null)
                {
                    if (ue.ENDS.HasValue)
                    {
                        e.BPS_EMPLOYEE = '2';
                    }
                }
                if (ud != null)
                {
                    if (ud.ENDS.HasValue)
                    {
                        e.BPS_USER = '2';
                    }
                }
                if (ua != null)
                {
                    if (ua.ENDS.HasValue)
                    {
                        e.BPS_ASSOCIATION = '2';
                    }
                }
                if (up != null)
                {
                    if (up.ENDS.HasValue)
                    {
                        e.BPS_SUPPLIER = '2';
                    }
                }
                if (ur != null)
                {
                    if (ur.ENDS.HasValue)
                    {
                        e.BPS_COLLECTOR = '2';
                    }
                }
            });
            return datos;
        }

        public List<SocioNegocio> GET_REC_BUSINESS_PARTNER_GRAL()
        {
            return new DASocioNegocio().GET_REC_BUSINESS_PARTNER_GRAL();
        }

        /// <summary>
        /// Verifica si el existe un socio con numero de documento y mismo tipo doc
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="idTipoDocumento"></param>
        /// <param name="nroDocumento"></param>
        /// <returns>1= si existe el numero y tipo de documentos, caso contrario 0</returns>
        public bool existeSocioXDocumento(string Owner, decimal idTipoDocumento, string nroDocumento)
        {
            return new DASocioNegocio().existeSocioXDocumento(Owner, idTipoDocumento, nroDocumento);
        }
        /// <summary>
        /// Verifica si el existe un socio con numero de documento y mismo tipo doc 
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="idTipoDocumento"></param>
        /// <param name="nroDocumento"></param>
        /// <param name="bpsId"></param>
        /// <returns></returns>
        public bool existeSocioXDocumento(string Owner, decimal idTipoDocumento, string nroDocumento, decimal bpsId)
        {
            return new DASocioNegocio().existeSocioXDocumento(Owner, idTipoDocumento, nroDocumento, bpsId);
        }
        public SocioNegocio ObtenerTipoPersonaXCodigoLic(decimal idLicencia, string owner)
        {
            return new DASocioNegocio().ObtenerTipoPersonaXCodigoLic(idLicencia, owner);
        }

        public List<SocioNegocio> ObtenerSocioxRuc(string ruc, string razsocial)
        {
            return new DASocioNegocio().ObtenerSocioxRuc(ruc, razsocial);
        }

        public bool AgruparSocio(string owner, List<SocioNegocio> socio, decimal bps_origen)
        {
            var exito = false;
            foreach (var item in socio)
            {
                exito = new DASocioNegocio().AgruparSocio(owner, bps_origen, item.BPS_ID);
            }

            new DASocioNegocio().VerificarSocio(owner, bps_origen, socio.FirstOrDefault().BPS_VERIFICADO, socio.FirstOrDefault().LOG_USER_VERIFICADO);

            return exito;
        }

        #region GRUPO EMPRESARIAL
        public List<SocioNegocio> BuscarSocioEmpresarial(string tipoPersona,
                                                decimal? tipoId,
                                                string nro_tipo,
                                                string nombre,
                                                decimal ubigeo,
                                                int estado,
                                                int pagina,
                                                int cantRegxPag,
            string BPS_USER, string BPS_ASSOCIATION, string BPS_GROUP, string BPS_COLLECTOR, string BPS_SUPPLIER, string BPS_EMPLOYEE, string nombreComercial)
        {
            var datos = new DASocioNegocio().BuscarSocioEmpresarial(tipoPersona, tipoId, nro_tipo, nombre, ubigeo, estado, pagina, cantRegxPag, BPS_USER, BPS_ASSOCIATION, BPS_GROUP, BPS_COLLECTOR, BPS_SUPPLIER, BPS_EMPLOYEE, nombreComercial);


            datos.ForEach(e =>
            {
                var ue = new BLUsuarioEmpleado().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var ud = new BLUsuarioDerecho().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var ua = new BLUsuarioAsociacion().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var up = new BLUsuarioProveedor().ObtenerSinRelacion(e.BPS_ID, e.OWNER);
                var ur = new BLUsuarioRecaudador().ObtenerSinRelacion(e.BPS_ID, e.OWNER);

                if (ue != null)
                {
                    if (ue.ENDS.HasValue)
                    {
                        e.BPS_EMPLOYEE = '2';
                    }
                }
                if (ud != null)
                {
                    if (ud.ENDS.HasValue)
                    {
                        e.BPS_USER = '2';
                    }
                }
                if (ua != null)
                {
                    if (ua.ENDS.HasValue)
                    {
                        e.BPS_ASSOCIATION = '2';
                    }
                }
                if (up != null)
                {
                    if (up.ENDS.HasValue)
                    {
                        e.BPS_SUPPLIER = '2';
                    }
                }
                if (ur != null)
                {
                    if (ur.ENDS.HasValue)
                    {
                        e.BPS_COLLECTOR = '2';
                    }
                }
            });
            return datos;
        }

        #endregion

        #region DescuentosSocio
        //lista los bpsid por grupo empresarial

        public List<SocioNegocio> BpsIdxGrupoEmpresarial(string owner, decimal bpsid)
        {
            return new DASocioNegocio().ListaBPSIDxGrupoEmpresarial(owner, bpsid);
        }
        //valida Socio Empresarial
        public int ValidaSocioEmpresarial(string owner, decimal bpsid)
        {
            return new DASocioNegocio().ValidaSocioEmpresarial(owner, bpsid);
        }

        #region Usuario de Derecho Descuento
        public List<BELicencias> ListarLicenciasxBPSID(decimal? BPSID, int page, int pagesize)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DASocioNegocio().ListarLicenciasxBPSID(owner, BPSID, page, pagesize);
        }
        public List<BEEstablecimiento> ListarEstablecimientosxBPSID(decimal? BPSID, int page, int pagesize)
        {
            string owner = GlobalVars.Global.OWNER;
            return new DASocioNegocio().ListarEstablecimientosxBPSID(owner, BPSID, page, pagesize);
        }
        #endregion

        #endregion

        public int ValidaPermisoModificacionSocion()
        {
            return new DASocioNegocio().ValidaPermisoModificacionSocion();
        }

    }
}
