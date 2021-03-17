using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebBasico.Content.Metadata
{
    public class MetadataUtil
    {

        private static global::System.Resources.ResourceManager resourceManEN;
        private static global::System.Resources.ResourceManager resourceManSP;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManagerEN
        {
            get
            {
                if (object.ReferenceEquals(resourceManEN, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AppWebBasico.Content.Metadata.ResourceEN", typeof(ResourceEN).Assembly);
                    resourceManEN = temp;
                }
                return resourceManEN;
            }
        }


        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManagerSP
        {
            get
            {
                if (object.ReferenceEquals(resourceManSP, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AppWebBasico.Content.Metadata.ResourceSP", typeof(ResourceSP).Assembly);
                    resourceManSP = temp;
                }
                return resourceManSP;
            }
        }

        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        public static string ObtenerMensaje(string codigo, int tipo)
        {
            string mensaje = string.Empty;
            switch(tipo)
            {
                case 1: mensaje = ResourceManagerSP.GetString(codigo, resourceCulture);
                    break;
                case 2: mensaje = ResourceManagerEN.GetString(codigo, resourceCulture);
                    break;
            }
            return mensaje;
        }
    }
}