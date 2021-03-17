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
    public class BLREC_BANKS_BRANCH
    {
        public List<BEREC_BANKS_BRANCH> Get_REC_BANKS_BRANCH()
        {
            return new DAREC_BANKS_BRANCH().Get_REC_BANKS_BRANCH();
        }

        //public BEREC_BANKS_BRANCH cabeceraSucursal(string OWNER, string BNK_ID, string BRCH_ID)
        //{
        //    return new DAREC_BANKS_BRANCH().cabeceraSucursal(OWNER, BNK_ID, BRCH_ID);
        //}

        public BEREC_BANKS_BRANCH Obtiene(string id, string owner)
        {
            var objSucursal = new DAREC_BANKS_BRANCH().cabeceraSucursal(id, owner);

            return objSucursal;
        }

        public List<BEREC_BANKS_BRANCH> REC_BANKS_BRANCH_Page(string owner, string param, decimal? idBanco, int st, int pagina, int cantRegxPag)
        {
            return new DAREC_BANKS_BRANCH().REC_BANKS_BRANCH_Page(owner, param, idBanco, st, pagina, cantRegxPag);
        }

        public bool InsertarSucursal(BEREC_BANKS_BRANCH en)
        {
            //var lista = new DAREC_BANKS_BRANCH().REC_BANKS_BRANCH_GET_by_BNK_ID_BRCH_ID(GlobalVars.Global.OWNER, en.BNK_ID, en.BRCH_ID);
            //if (lista.Count == 0)
            //    return new DAREC_BANKS_BRANCH().REC_BANKS_BRANCH_Ins(en);
            //else
            //    return false;

            decimal codigoGenAdd = 0;

            using (TransactionScope transa = new TransactionScope())
            {
                if (en.Direccion != null)
                {
                    foreach (var direccion in en.Direccion)
                    {
                        codigoGenAdd = new DADirecciones().Insertar(direccion);
                    }
                }

                en.ADD_ID = codigoGenAdd;
                new DAREC_BANKS_BRANCH().InsertarSucursal(en);

                if (en.Contacto != null)
                {
                    foreach (var contacto in en.Contacto)
                    {
                        var cod = new DASocioNegocioBanco().InsertarSocioNegocioBanco(contacto);
                    }
                }
                transa.Complete();
            }

            return true;
        }

        public bool ActualizarSucursal(BEREC_BANKS_BRANCH sucursal, List<BEREC_BANKS_BPS> conEliminar, List<BEDireccion> dirEliminar, List<BEDireccion> listDirActivar, List<BEREC_BANKS_BPS> listConActivar)
        {
            using (TransactionScope transa = new TransactionScope())
            {
                DADirecciones proxyDir = new DADirecciones();
                DASocioNegocioBanco proxyCon = new DASocioNegocioBanco();
                DAREC_BANKS_BRANCH proxuSuc = new DAREC_BANKS_BRANCH();
                string auxBNK_ID = proxuSuc.ObtenerBancoIdAntiguo(sucursal.OWNER, sucursal.BRCH_ID);

                if (sucursal.BNK_ID != Convert.ToDecimal(auxBNK_ID))
                {
                    if (sucursal.Contacto != null)
                    {
                        foreach (var contacto in sucursal.Contacto)
                        {
                            List<BEREC_BANKS_BPS> proxyConObtener = proxyCon.SocioNegocioBancoXSucursalesObtener(contacto.BRCH_ID, contacto.OWNER, contacto.BNK_ID, contacto.BPS_ID);
                            if (proxyConObtener.Count != 0)
                            {
                                var result = proxyCon.ActualizarSocioNegocioBancoId(contacto, sucursal.BNK_ID, Convert.ToDecimal(auxBNK_ID));
                            }
                        }
                    }
                }

                var updCuentaBancaria = new DAREC_BANKS_BRANCH().ActualizarBanco(GlobalVars.Global.OWNER, sucursal.BNK_ID, Convert.ToDecimal(auxBNK_ID));

                var upd = new DAREC_BANKS_BRANCH().REC_BANKS_BRANCH_Upd(sucursal);

                if (sucursal.Direccion != null)
                {
                    foreach (var direccion in sucursal.Direccion)
                    {
                        List<BEDireccion> proxyDirObtener = proxyDir.DireccionXSucursales(sucursal.OWNER, sucursal.BRCH_ID, sucursal.BNK_ID);
                        if (proxyDirObtener.Count == 0)
                        {
                            var codigoGenAdd = proxyDir.Insertar(direccion);
                        }
                        else
                        {
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
                        proxyDir.Eliminar(sucursal.OWNER, item.ADD_ID, sucursal.LOG_USER_UPDATE);
                    }
                }

                /// activa las direcciones
                if (listDirActivar != null)
                {
                    foreach (var item in listDirActivar)
                    {
                        proxyDir.Activar(sucursal.OWNER, item.ADD_ID, sucursal.LOG_USER_UPDATE);
                    }
                    //listDirActivar.ForEach(x => { proxyDir.Activar(bps.OWNER, x.ADD_ID, bps.LOG_USER_UPDATE); });
                }

                if (sucursal.Contacto != null)
                {
                    foreach (var contacto in sucursal.Contacto)
                    {
                        if (sucursal.BNK_ID != Convert.ToDecimal(auxBNK_ID)) contacto.BNK_ID = sucursal.BNK_ID;
                        List<BEREC_BANKS_BPS> proxyConObtener = proxyCon.SocioNegocioBancoXSucursalesObtener(sucursal.BRCH_ID, contacto.OWNER, contacto.BNK_ID, contacto.BPS_ID);
                        if (proxyConObtener.Count == 0)
                        {
                            var codigoGenAdd = proxyCon.InsertarSocioNegocioBanco(contacto);
                        }
                        else
                        {
                            var result = proxyCon.ActualizarSocioNegocioBanco(contacto);
                        }
                    }
                }

                if (conEliminar != null)
                {
                    foreach (var item in conEliminar)
                    {
                        proxyCon.Eliminar(sucursal.OWNER, sucursal.BRCH_ID, item.BPS_ID.ToString(), sucursal.LOG_USER_UPDATE);
                    }
                }

                if (listConActivar != null)
                {
                    foreach (var item in listConActivar)
                    {
                        proxyCon.Activar(sucursal.OWNER, sucursal.BRCH_ID, item.BPS_ID.ToString(), sucursal.LOG_USER_UPDATE);
                    }
                }
                transa.Complete();
            }
            return true;
        }

        public bool REC_BANKS_BRANCH_Upd(BEREC_BANKS_BRANCH en)
        {
            return new DAREC_BANKS_BRANCH().REC_BANKS_BRANCH_Upd(en);
        }

        public bool REC_BANKS_BRANCH_Del(BEREC_BANKS_BRANCH en)
        {
            return new DAREC_BANKS_BRANCH().REC_BANKS_BRANCH_Del(en);
        }

        public List<BEREC_BPS_BANKS_ACC> ListarCuentaBancaria(string owner, string sucbnkId)
        {
            return new DAREC_BANKS_BRANCH().ListarCuentaBancaria(owner, sucbnkId);
        }

        public List<BEREC_BPS_BANKS_ACC> ListarCuentaBancariaXBanco(string owner, decimal bnkId, string moneda)
        {
            return new DAREC_BANKS_BRANCH().ListarCuentaBancariaXbanco(owner, bnkId, moneda);
        }
    }
}
