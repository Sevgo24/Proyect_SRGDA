using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGRDA.DA;
using SGRDA.Entities;
using SGRDA.Utility;

namespace SGRDA.BL
{
    public class BLREF_CURRENCY_VALUES
    {
        public List<REF_CURRENCY_VALUES> usp_Get_REF_CURRENCY_VALUES(string CUR_ALPHA, int YEAR, int MONTH)
        {
            try
            {
                List<REF_CURRENCY_VALUES> Lstcur = new List<REF_CURRENCY_VALUES>();
                Lstcur = new DAREF_CURRENCY_VALUES().usp_Get_REF_CURRENCY_VALUES(CUR_ALPHA, YEAR, MONTH);
                List<REF_CURRENCY_VALUES> Lst = new List<REF_CURRENCY_VALUES>();
                int dias = DateTime.DaysInMonth(YEAR, MONTH); // Nro de días de un mes y año determinado

                for (int i = 1; i <= dias; i++)
                {
                    REF_CURRENCY_VALUES be = new REF_CURRENCY_VALUES();
                    be.CUR_DATE = Convert.ToDateTime(i + "/" + MONTH + "/" + YEAR);
                    be.CUR_VALUE = 0;
                    

                    foreach (REF_CURRENCY_VALUES element in Lstcur)
                    {
                        var dateA = String.Format("{0:MM/dd/yyyy}", element.CUR_DATE);
                        var dateB = String.Format("{0:MM/dd/yyyy}", be.CUR_DATE);
                        if (dateA == dateB) // si la fecha ya existe en la BD se coloca la misma
                        {
                            be.CUR_DATE = element.CUR_DATE;
                            be.CUR_VALUE = element.CUR_VALUE;
                        }
                    }
                    Lst.Add(be);
                }
                return Lst;
            }
            catch (Exception)
            {
                throw;
            }
            //int dias = DateTime.DaysInMonth(anyo, mes);
        }

        public bool Insert_Update_REF_CURRENCY_VALUES_XML(List<REF_CURRENCY_VALUES> Lst, string CUR_ALPHA, int YEAR, int MONTH)
        {
            List<REF_CURRENCY_VALUES> Lstcur = new List<REF_CURRENCY_VALUES>();
            bool exito = false;
            DAREF_CURRENCY_VALUES da = new DAREF_CURRENCY_VALUES();
            string xmlDetalle = string.Empty;
            try
            {
                Lstcur = new DAREF_CURRENCY_VALUES().usp_Get_REF_CURRENCY_VALUES(CUR_ALPHA, YEAR, MONTH);
                xmlDetalle = Util.SerializarEntity(Lst);
                if (Lstcur.Count == 0) exito = da.usp_Ins_REF_CURRENCY_VALUES_XML(xmlDetalle);
                else exito = da.usp_Upd_REF_CURRENCY_VALUES_XML(xmlDetalle);
            }
            catch (Exception)
            {
                throw;
            }
            return exito;
        }

        public bool usp_Upd_REF_CURRENCY_VALUES_XML(List<REF_CURRENCY_VALUES> item)
        {
            bool exito = false;
            DAREF_CURRENCY_VALUES da = new DAREF_CURRENCY_VALUES();
            string xmlDetalle = string.Empty;
            try
            {
                xmlDetalle = Util.SerializarEntity(item);
                exito = da.usp_Upd_REF_CURRENCY_VALUES_XML(xmlDetalle);
            }
            catch (Exception)
            {
                throw;
            }
            return exito;
        }

        public bool usp_Ins_REF_CURRENCY_VALUES_XML(List<REF_CURRENCY_VALUES> item)
        {
            bool exito = false;
            DAREF_CURRENCY_VALUES da = new DAREF_CURRENCY_VALUES();
            string xmlDetalle = string.Empty;
            try
            {
                xmlDetalle = Util.SerializarEntity(item);
                exito = da.usp_Ins_REF_CURRENCY_VALUES_XML(xmlDetalle);
            }
            catch (Exception)
            {
                throw;
            }
            return exito;
        }

        public bool usp_Upd_REF_CURRENCY_VALUES(REF_CURRENCY_VALUES en)
        {
            bool exito = false;
            DAREF_CURRENCY_VALUES da = new DAREF_CURRENCY_VALUES();           
            try
            {
                exito = da.usp_Upd_REF_CURRENCY_VALUES(en);
            }
            catch (Exception)
            {
                throw;
            }
            return exito;
        }

        public bool insertar(REF_CURRENCY_VALUES en)
        {
            bool exito = false;
            DAREF_CURRENCY_VALUES da = new DAREF_CURRENCY_VALUES();
            try
            {
                exito = da.insertar(en);
            }
            catch (Exception)
            {
                throw;
            }
            return exito;
        }

        public List<REF_CURRENCY_VALUES> usp_REF_CURRENCY_VALUES_GET(DateTime CUR_DATE)
        {
            return new DAREF_CURRENCY_VALUES().usp_REF_CURRENCY_VALUES_GET(CUR_DATE);
        }


        public REF_CURRENCY_VALUES ObtenerTipoCambioActual( )
        {
            return new DAREF_CURRENCY_VALUES().ObtenerTipoCambioActual();
        }



        #region MODULO ADMINISTRACION -TASA DE CAMBIO
        public List<BEREF_CURRENCY_VALUES> ListaTasaDeCambio(string fecha_ini,string fecha_fin)
        {
            return new DAREF_CURRENCY_VALUES().ListaTasaDeCambio(fecha_ini,fecha_fin);
        }

        public bool GrabarTasaCambio(BEREF_CURRENCY_VALUES en )
        {
            return new DAREF_CURRENCY_VALUES().GrabarTasaCambio(en);
        }

        public int ConsultaTasaCambio()
        {
            return new DAREF_CURRENCY_VALUES().ConsultaTasaCambio();
        }
        #endregion
    }
}
