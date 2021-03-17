﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using SGRDA.Entities;

namespace SGRDA.DA
{
    public class DAParametroLic
    {
        public int Insertar(BEParametroLic obs)
        {
            int retorno = 0;
            try
            {
                Database oDataBase = new DatabaseProviderFactory().Create("conexion");
                DbCommand oDbComand = oDataBase.GetStoredProcCommand("SGRDASI_PARAMETRO_LIC");
                oDataBase.AddInParameter(oDbComand, "@OWNER", DbType.String, obs.OWNER);
                oDataBase.AddInParameter(oDbComand, "@LIC_ID", DbType.Int32, obs.LIC_ID);
                oDataBase.AddInParameter(oDbComand, "@PAR_ID", DbType.Int32, obs.PAR_ID);
                oDataBase.AddInParameter(oDbComand, "@LOG_USER_CREAT", DbType.String, obs.LOG_USER_CREAT);

                retorno = oDataBase.ExecuteNonQuery(oDbComand);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retorno;
        }
    }
}
