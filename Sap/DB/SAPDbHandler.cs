using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DB
{
    public static class SAPDbHandler
    {
        static string STG_ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString;
            }
        }

        static string PROD_ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["prod_connection"].ConnectionString;
            }
        }

    }
}
