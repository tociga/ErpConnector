using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using AxConCommon.Extensions;

namespace DataAccess
{
    public class DataWriter
    {
        protected static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString;
            }
        }

        public static void WriteToTable(IGenericDataReader reader, string tableName)
        {
            if (reader.HasRows())
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    using (var copy = new SqlBulkCopy(con))
                    {
                        con.Open();
                        copy.DestinationTableName = tableName;
                        copy.BulkCopyTimeout = 3600;
                        copy.WriteToServer(reader);
                 
                    }
                }
            }
        }

        public static void TruncateTables(bool clearItems, bool clearTrans, bool clearTransRefresh, bool clearLocations)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand("[ax].[truncate_ax_tables]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@truncate_items", clearItems);
                    cmd.Parameters.AddWithValue("@truncate_sales_trans_dumb", clearTrans);
                    cmd.Parameters.AddWithValue("@truncate_locations_and_vendors", clearLocations);
                    cmd.Parameters.AddWithValue("@truncate_sales_trans_refresh", clearTransRefresh);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Int64 GetMaxRecId(string schema, string tableName)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand("[ax].[find_max_rec_id]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@schema", schema);
                    cmd.Parameters.AddWithValue("@table_name", tableName);

                    var reader = cmd.ExecuteReader();
                    if(reader.HasRows)
                    {
                        reader.Read();
                        return reader.IsDBNull(0) ? 0 : reader.GetInt64(0);
                    }
                    return 0;
                }
            }
        }
    }
}
