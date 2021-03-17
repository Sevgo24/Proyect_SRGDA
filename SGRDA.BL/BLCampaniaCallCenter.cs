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
    public class BLCampaniaCallCenter
    {
        public List<BECampaniaCallCenter> ListaDropCampaniaContacto(string owner)
        {
            return new DACampaniaCallCenter().ListaDropCampaniaContacto(owner);
        }

        public List<BECampaniaCallCenter> ListarCampaniaCallCenter(string owner, decimal contacto, decimal tipoCamp, string estadoCamp, string nombre, decimal perfilCliente, DateTime fechaIni, DateTime fechaFin, int st, int pagina, int cantRegxPag)
        {
            return new DACampaniaCallCenter().ListarCampaniaCallCenter(owner, contacto, tipoCamp, estadoCamp, nombre, perfilCliente, fechaIni, fechaFin, st, pagina, cantRegxPag);
        }

        public List<BECampaniaCallCenter> ListarClientesAsignadosCampania(string owner, decimal Id)
        {
            return new DACampaniaCallCenter().ListarClientesAsignadosCampania(owner, Id);
        }

        public BECampaniaCallCenter ObtenerDatos(string owner, decimal Id)
        {
            var objCampania = new DACampaniaCallCenter().ObtenerDatos(owner, Id);
            if (objCampania != null)
            {
                objCampania.Documentos = new DADocumentoGral().DocumentoXCampania(owner, Id);
                objCampania.Asociados = new DAAgenteCampania().AgenteXCampania(owner, Id);
                objCampania.LoteTrabajo = new DALoteTrabajo().ListarLoteTrabajo(owner, Id);
            }
            return objCampania;
        }

        public int Insertar(BECampaniaCallCenter en)
        {
            var codigoGen = 0;

            using (TransactionScope transa = new TransactionScope())
            {
                codigoGen = new DACampaniaCallCenter().Insertar(en);

                if (en.Documentos != null)
                {
                    foreach (var documento in en.Documentos)
                    {
                        var codigoGenDoc = new DADocumentoGral().Insertar(documento);
                        var result = new DADocumentoCampania().Insertar(new BECampaniaDoc
                            {
                                CONC_CID = codigoGen,
                                DOC_ID = codigoGenDoc,
                                OWNER = GlobalVars.Global.OWNER,
                                LOG_USER_CREAT = en.LOG_USER_CREAT
                            });
                    }
                }

                if (en.Asociados != null)
                {
                    foreach (var item in en.Asociados)
                    {
                        var result = new DAAgenteCampania().Insertar(new BEAgenteCampania
                        {
                            OWNER = en.OWNER,
                            BPS_ID = item.BPS_ID,
                            CONC_CID = codigoGen,
                            ROL_ID = item.ROL_ID,
                            LOG_USER_CREAT = item.LOG_USER_CREAT
                        });
                    }
                }

                if (en.LoteTrabajo != null)
                {
                    foreach (var item in en.LoteTrabajo)
                    {
                        var result = new DALoteTrabajo().Insertar(new BELoteTrabajo
                            {
                                OWNER = en.OWNER,
                                CONC_SDATEINI = item.CONC_SDATEINI,
                                CONC_SDATEND = item.CONC_SDATEND,
                                BPS_ID = item.BPS_ID,
                                CONC_CID = item.CONC_CID == 0 ? codigoGen : item.CONC_CID,
                                LOG_USER_CREAT = item.LOG_USER_CREAT
                            });
                    }
                }

                transa.Complete();
            }
            return codigoGen;
        }

        public int Actualizar(BECampaniaCallCenter en, List<BEDocumentoGral> docEliminar, List<BEDocumentoGral> listDocActivar, List<BEAgenteCampania> asoEliminar, List<BEAgenteCampania> listAsociadoActivar, List<BELoteTrabajo> loteEliminar, List<BELoteTrabajo> lisloteActivar)
        {
            int upd = 0;

            using (TransactionScope transa = new TransactionScope())
            {
                upd = new DACampaniaCallCenter().Actualizar(en);

                DADocumentoGral proxyDoc = new DADocumentoGral();
                DADocumentoCampania proxyDocCamp = new DADocumentoCampania();

                if (en.Documentos != null)
                {
                    foreach (var item in en.Documentos)
                    {
                        BEDocumentoGral proxyDocObtener = proxyDoc.ObtenerDocCamp(en.OWNER, item.DOC_ID, en.CONC_CID);
                        if (proxyDocObtener == null)
                        {
                            var codigoGenAdd = proxyDoc.Insertar(item);
                            var result = proxyDocCamp.Insertar(new BECampaniaDoc
                            {
                                CONC_CID = en.CONC_CID,
                                DOC_ID = codigoGenAdd,
                                OWNER = item.OWNER,
                                LOG_USER_CREAT = item.LOG_USER_CREAT
                            });
                        }
                        else
                        {
                            item.LOG_USER_UPDATE = item.LOG_USER_UPDATE;
                            var result = proxyDoc.Update(item);
                        }
                    }
                }

                if (docEliminar != null)
                    docEliminar.ForEach(x => { proxyDoc.Eliminar(en.OWNER, x.DOC_ID, en.LOG_USER_UPDATE); });
                if (listDocActivar != null)
                    listDocActivar.ForEach(x => { proxyDoc.Activar(en.OWNER, x.DOC_ID, en.LOG_USER_UPDATE); });


                DAAgenteCampania proxyAso = new DAAgenteCampania();

                if (en.Asociados != null)
                {
                    foreach (var item in en.Asociados)
                    {
                        BEAgenteCampania ent = proxyAso.ObtenerAsoCamp(en.OWNER, item.SEQUENCE, en.CONC_CID);
                        if (ent == null)
                        {
                            item.CONC_CID = en.CONC_CID;
                            item.OWNER = en.OWNER;
                            var result = proxyAso.Insertar(item);
                        }
                        else if (ent.BPS_ID != item.BPS_ID || ent.ROL_ID != item.ROL_ID)
                        {
                            item.LOG_USER_UPDATE = item.LOG_USER_CREAT;
                            item.CONC_CID = en.CONC_CID;
                            var result = proxyAso.Update(item);
                        }
                    }
                }

                if (asoEliminar != null)
                    asoEliminar.ForEach(x => { proxyAso.Eliminar(en.OWNER, x.SEQUENCE, en.CONC_CID, en.LOG_USER_UPDATE); });
                if (listAsociadoActivar != null)
                    listAsociadoActivar.ForEach(x => { proxyAso.Activar(en.OWNER, x.SEQUENCE, en.CONC_CID, en.LOG_USER_UPDATE); });



                DALoteTrabajo proxyLote = new DALoteTrabajo();

                if (en.LoteTrabajo != null)
                {
                    foreach (var item in en.LoteTrabajo)
                    {
                        BELoteTrabajo ent = proxyLote.ObtenerLoteTrabajo(en.OWNER, item.CONC_SID);
                        if (ent == null)
                        {
                            item.CONC_CID = en.CONC_CID;
                            item.OWNER = en.OWNER;
                            var result = proxyLote.Insertar(item);
                        }
                        else if (ent.BPS_ID != item.BPS_ID || ent.CONC_SDATEINI != item.CONC_SDATEINI || ent.CONC_SDATEND != item.CONC_SDATEND)
                        {
                            item.LOG_USER_UPDAT = item.LOG_USER_CREAT;
                            item.CONC_CID = en.CONC_CID;
                            var result = proxyLote.Update(item);
                        }
                    }
                }

                if (loteEliminar != null)
                    loteEliminar.ForEach(x => { proxyLote.Eliminar(en.OWNER, x.CONC_SID, en.LOG_USER_UPDATE); });
                if (lisloteActivar != null)
                    lisloteActivar.ForEach(x => { proxyLote.Activar(en.OWNER, x.CONC_SID, en.LOG_USER_UPDATE); });

                transa.Complete();
            }
            return upd;
        }

        public int Eliminar(BECampaniaCallCenter en)
        {
            return new DACampaniaCallCenter().Eliminar(en);
        }

        //public List<BECampaniaConsultaAsignarSocio> ListaSociosAsignar(string owner, decimal idTipoLic, decimal idMod, string idGrupoMod, decimal idGrupoFac, decimal idTemp, decimal idSerie, decimal idEst,
        //    decimal idSubtipoEst, decimal idTipoEst, string idTipoPersona, decimal idTipoDoc, string numeroDoc, string socio, string usuario, string recaudador, string asociado, string empleado, string proveedor)
        //{
        //    return new DACampaniaCallCenter().ListaSociosAsignar(owner, idTipoLic, idMod, idGrupoMod, idGrupoFac, idTemp, idSerie, idEst,
        //    idSubtipoEst, idTipoEst, idTipoPersona, idTipoDoc, numeroDoc, socio, usuario, recaudador, asociado, empleado, proveedor);
        //}

        public BECampaniaConsultaAsignarSocio ObtenerListas(string owner, decimal idTipoLic, decimal idMod, string idGrupoMod, decimal idGrupoFac, decimal idTemp, decimal idSerie, decimal idEst,
            decimal idSubtipoEst, decimal idTipoEst, string idTipoPersona, decimal idTipoDoc, string numeroDoc, string socio, decimal idUbigeo, string usuario, string recaudador, string asociado, string grupo, string empleado, string proveedor, int Estado)
        {
            BECampaniaConsultaAsignarSocio en = new BECampaniaConsultaAsignarSocio();
            en.AsignarSocioCab = new DACampaniaCallCenter().ListaSociosAsignar(owner, idTipoLic, idMod, idGrupoMod, idGrupoFac, idTemp, idSerie, idEst,
            idSubtipoEst, idTipoEst, idTipoPersona, idTipoDoc, numeroDoc, socio, idUbigeo, usuario, recaudador, asociado, grupo, empleado, proveedor, Estado);

            if (en != null)
            {
                en.AsignarSocioDet = new DACampaniaCallCenter().ListaSociosAsignarDetalle(owner);
                en.AsignarSocioSubDet = new DACampaniaCallCenter().ListaSociosAsignarSubDetalle(owner);
            }
            return en;
        }

        public List<BEAgenteCampania> ListarAgenteRecaudador(string owner, decimal? IdCampania)
        {
            return new DAAgenteCampania().ListarAgenteRecaudador(owner, IdCampania);
        }

        public bool InsertarCampaniaAsignarXML(List<BEContactoAsignarCampania> lista)
        {
            bool result = false;
            string xmlLista = string.Empty;
            xmlLista = Utility.Util.SerializarEntity(lista);

            using (TransactionScope transa = new TransactionScope())
            {
                result = new DACampaniaCallCenter().InsertarCampaniaAsignarXML(xmlLista);
                transa.Complete();
            }
            return result;
        }

        public BEContactoAsignarCampania ObtenerSociosAsignados(string owner, decimal idCampania, decimal idSocio)
        {
            return new DACampaniaCallCenter().ObtenerSociosAsignados(owner, idCampania, idSocio);
        }

        public BECampaniaCallCenter obtenerNombreCampania(string owner, decimal idCampania)
        {
            return new DACampaniaCallCenter().obtenerNombreCampania(owner, idCampania);
        }

        public List<BECampaniaCallCenter> ListarCentroContacto(string owner)
        {
            return new DACampaniaCallCenter().ListarCentroContacto(owner);
        }

        public List<BECampaniaCallCenter> ListarCampaniaPorTipo(string owner, decimal idtipo)
        {
            return new DACampaniaCallCenter().ListarCampaniaPorTipo(owner, idtipo);
        }
    }
}
