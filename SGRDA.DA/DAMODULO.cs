using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common;
using SGRDA.Entities;
using System.Data.Common;

namespace SGRDA.DA
{
    public class DAMODULO
    {
        // MODULO modulo = new MODULO();
        private Database oDataBase = new DatabaseProviderFactory().Create("conexion");

        public List<MODULO> MODULO_spBuscarMenu(int usua_icodigo_usuario, int CABE_ICODIGO_MODULO)
        {
            MODULO be = null;
            List<MODULO> lista = new List<MODULO>();

            DbCommand oDbCommand = oDataBase.GetStoredProcCommand("USUARIO_ROL_spBuscarMenu");
            oDataBase.AddInParameter(oDbCommand, "@usua_icodigo_usuario", DbType.Int32, usua_icodigo_usuario);
            oDataBase.AddInParameter(oDbCommand, "@CABE_ICODIGO_MODULO", DbType.Int32, CABE_ICODIGO_MODULO);
            oDataBase.ExecuteNonQuery(oDbCommand);

            using (IDataReader reader = oDataBase.ExecuteReader(oDbCommand))
            {
                while (reader.Read())
                {
                    be = new MODULO();
                    be.MODU_ICODIGO_MODULO = reader.GetInt32(reader.GetOrdinal("codigo_modulo"));
                    be.MODU_INIVEL_MODULO = reader.GetInt32(reader.GetOrdinal("nivel_modulo"));
                    be.MODU_VNOMBRE_MODULO = reader.GetString(reader.GetOrdinal("nombre_modulo"));
                    be.MODU_VRUTA_PAGINA = reader.GetString(reader.GetOrdinal("ruta_pagina"));
                    be.MODU_VDESCRIPCION_MODULO = reader.GetString(reader.GetOrdinal("descripcion"));
                    be.MODU_ICODIGO_MODULO_DEPENDIENTE = reader.GetInt32(reader.GetOrdinal("codigo_padre"));
                    be.MODU_IORDEN_MODULO = reader.GetInt32(reader.GetOrdinal("orden"));
                    be.MODU_CACTIVO_MODULO = reader.GetString(reader.GetOrdinal("activo"));
                    be.CABE_ICODIGO_MODULO = reader.GetInt32(reader.GetOrdinal("CABE_ICODIGO_MODULO"));
                    lista.Add(be);
                }
            }
            return lista;
        }
    }
}