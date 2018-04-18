using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Linq;

namespace ErpConnector.Ax.Utils
{
    public static class DataWriter
    {
        static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString;
            }
        }

        public static void WriteToTable<T>(IGenericDataReader reader, string tableName)
        {
            if (reader.HasRows())
            {
                using (var con = new SqlConnection(ConnectionString))
                {
                    using (var copy = new SqlBulkCopy(con))
                    {
                        con.Open();
                        var mappings = GetDynamicBulkCopyColumnMapping<T>();
                        foreach(var m in mappings)
                        {
                            copy.ColumnMappings.Add(m);
                        }
                        copy.DestinationTableName = tableName;
                        copy.BulkCopyTimeout = 3600;
                        copy.WriteToServer(reader);

                    }
                }
            }
        }

        public static void LogErpActionStep(int actionId, string step, DateTime startTime, bool success, string errorMessage, string errorStackTrace)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand("erp.insert_erp_action_step", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("@action_id", actionId);
                    cmd.Parameters.AddWithValue("@step", step);
                    cmd.Parameters.AddWithValue("@success", success);
                    cmd.Parameters.AddWithValue("@start_time", startTime);
                    cmd.Parameters.AddWithValue("@error_message", errorMessage);
                    cmd.Parameters.AddWithValue("@error_stack_trace", errorStackTrace);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static void TruncateTables(bool clearItems, bool clearTrans, bool clearTransRefresh, bool clearLocations, bool clearLookup, bool clearBom, bool clearPOTO, bool clearPrice,
            bool clearAttributeRefresh)
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
                    cmd.Parameters.AddWithValue("@truncate_lookup_info", clearLookup);
                    cmd.Parameters.AddWithValue("@truncate_po_to", clearPOTO);
                    cmd.Parameters.AddWithValue("@truncate_bom", clearBom);
                    cmd.Parameters.AddWithValue("@truncate_price", clearPrice);
                    cmd.Parameters.AddWithValue("@truncate_attributes_refresh", clearAttributeRefresh);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Int64 GetMaxRecId(string tableName)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                using (var cmd = new SqlCommand("[ax].[find_max_rec_id]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
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

        public static List<SqlBulkCopyColumnMapping> GetDynamicBulkCopyColumnMapping<T>()
        {
            List<SqlBulkCopyColumnMapping> mappings = new List<SqlBulkCopyColumnMapping>();
            Type baseType = typeof(T);
            foreach (var pi in baseType.GetProperties(BindingFlags.Public|BindingFlags.Instance))
            {
                if (pi.PropertyType.IsValueType || pi.PropertyType == typeof(String))
                {
                    mappings.Add(new SqlBulkCopyColumnMapping(pi.Name, pi.Name));
                }
            }
            return mappings;
        }

        public static List<string> ValidateColumnMapping<T>(string destTable)
        {
            string query = "select top 1 * from " + destTable;
            List<string> result = new List<string>();
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (var adapter = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    Type baseType = typeof(T);
                    var columns = (from DataColumn c in dt.Columns
                            select c.ColumnName).ToList();
                    foreach (var pi in baseType.GetProperties(BindingFlags.Public| BindingFlags.Instance))
                    {
                        if (pi.PropertyType.IsValueType || pi.PropertyType == typeof(String))
                        {
                            var cols = columns.Where(x => x == pi.Name);
                            if (!cols.Any())
                            {
                                result.Add(pi.Name);
                            }
                        }
                    }                    
                }
            }
            return result;
        }
    }
}
