using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Linq;
using ErpConnector.Common.Util;

namespace ErpConnector.Ax.Utils
{
    public static class AXDataWriter
    {
        static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString;
            }
        }



    }
}
