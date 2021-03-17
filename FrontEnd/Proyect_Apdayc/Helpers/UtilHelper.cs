using AppWebBasico.Content.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebBasico.Helpers
{
    public static class UtilHelper
    {
        public static string LimitarTexto(this HtmlHelper helper, string Texto, int Limite)
        {
            return Texto.Length>Limite? Texto.Substring(0,Limite) + "..." : Texto;
        }

        public static string Localizar(this HtmlHelper helper, string codigo, int tipo)
        {
            return MetadataUtil.ObtenerMensaje(codigo, tipo);
        }

    }
}