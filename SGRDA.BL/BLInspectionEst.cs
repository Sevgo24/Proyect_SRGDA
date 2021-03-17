using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;

namespace SGRDA.BL
{
    public class BLInspectionEst
    {
        public List<BEInspectionEst> usp_Get_InspectionPage(string owner, decimal insId, decimal estId, decimal tipoest, decimal? subtipoest, decimal socio, string tipodiv, decimal? division, int pagina, int cantRegxPag)
        {
            return new DAInspectionEst().usp_Get_InspectionPage(owner, insId, estId, tipoest, subtipoest, socio, tipodiv, division, pagina, cantRegxPag);
        }

        public int Insertar(BEInspectionEst en, BEEstablecimiento objEstablecimiento)
        {
            if (objEstablecimiento.Caracteristicas != null)
            {
                foreach (var caracteristica in objEstablecimiento.Caracteristicas)
                {
                    var result = new DACaracteristicaEst().Insertar(new BECaracteristicaEst
                    {
                        CHAR_ID = caracteristica.CHAR_ID,
                        EST_ID = caracteristica.EST_ID,
                        ESTT_ID = objEstablecimiento.ESTT_ID,
                        SUBE_ID = objEstablecimiento.SUBE_ID,
                        OWNER = en.OWNER,
                        VALUE = caracteristica.VALUE,
                        LOG_USER_CREAT = en.LOG_USER_CREAT
                    });
                }
            }

            return new DAInspectionEst().Insertar(en);
        }

        public int Actualizar(BEInspectionEst en, BEEstablecimiento objEstablecimiento, List<BECaracteristicaEst> carEliminar, List<BECaracteristicaEst> listCarActivar)
        {
            DACaracteristicaEst proxyCar = new DACaracteristicaEst();
            if (objEstablecimiento.Caracteristicas != null)
            {
                foreach (var item in objEstablecimiento.Caracteristicas)
                {
                    BECaracteristicaEst ent = proxyCar.ObtenerCarEst(objEstablecimiento.OWNER, item.CHAR_ID, objEstablecimiento.EST_ID);
                    if (ent == null)
                    {
                        item.EST_ID = objEstablecimiento.EST_ID;
                        item.INSP_ID = en.INSP_ID;
                        item.ESTT_ID = objEstablecimiento.ESTT_ID;
                        item.SUBE_ID = objEstablecimiento.SUBE_ID;
                        var codigoGenAdd = proxyCar.InsertarInspeccionCaracteristica(item);
                    }
                    else if (ent.VALUE != item.VALUE)
                    {
                        item.EST_ID = objEstablecimiento.EST_ID;
                        //item.INSP_ID = en.INSP_ID;
                        //item.ESTT_ID = objEstablecimiento.ESTT_ID;
                        //item.SUBE_ID = objEstablecimiento.SUBE_ID;
                        item.LOG_USER_UPDAT = item.LOG_USER_CREAT;
                        var result = proxyCar.Actualizar(item);
                    }
                }
            }
            if (carEliminar != null)
            {
                foreach (var item in carEliminar)
                {
                    proxyCar.Eliminar(objEstablecimiento.OWNER, item.CHAR_ID, objEstablecimiento.LOG_USER_UPDAT);
                }
            }
            if (listCarActivar != null)
            {
                listCarActivar.ForEach(x => { proxyCar.Activar(objEstablecimiento.OWNER, x.CHAR_ID, objEstablecimiento.LOG_USER_UPDAT); });
            }

            return new DAInspectionEst().Actualizar(en);
        }

        public int Activar(string owner, decimal insId, string user)
        {
            return new DAInspectionEst().Activar(owner, insId, user);
        }

        public int Eliminar(BEInspectionEst en)
        {
            return new DAInspectionEst().Eliminar(en);
        }

        public BEInspectionEst Obtener(string owner, decimal idIns)
        {
            return new DAInspectionEst().Obtener(owner, idIns);
        }
    }
}
