﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Linq;
using ErpConnector.Common.ErpTasks;
using ErpConnector.Common.AGREntities;
using ErpConnector.Common.Exceptions;
using System.Threading.Tasks;
using ErpConnector.Common.DTO;
using System.Collections;
using System.Threading;
using ErpConnector.Common.Constants;

namespace ErpConnector.Common.Util
{
    public static class DataWriter
    {
        static string StgConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString;
            }
        }
        static string ProdConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["prod_connection"].ConnectionString;
            }
        }

        public static DateTime GetDateById(int? dateId)
        {
            if (!dateId.HasValue)
            {
                return DateTime.MaxValue;
            }

            var connectionString = ConfigurationManager.ConnectionStrings["prod_connection"].ConnectionString;
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[get_date_by_id]"))
                {
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date_id", dateId);

                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        return reader.GetDateTime(0);
                    }
                    else
                    {
                        return DateTime.MaxValue;
                    }
                }
            }

        }
        public static void LogError(string message, string stackTrace, object sender, int hresult)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["prod_connection"].ConnectionString;
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[error_log_insert]"))
                {
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;                
                    cmd.Parameters.AddWithValue("@info", "Erp Connector Error message");
                    cmd.Parameters.AddWithValue("@message", message);
                    cmd.Parameters.AddWithValue("@source", sender.ToString());
                    cmd.Parameters.AddWithValue("@stack_trace", stackTrace);
                    cmd.Parameters.AddWithValue("@error_code", hresult);                    

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateActionStatus(int id, int status, Task a, AxBaseException e)
        {
            string errorMessage = null;
            string errorStackTrace = null;
            if (a != null && a.IsFaulted)
            {
                status = 3;
                if (a.Exception != null)
                {
                    if (!string.IsNullOrEmpty(a.Exception.StackTrace))
                    {
                        errorMessage = a.Exception.Message;
                        errorStackTrace = a.Exception.StackTrace;
                    }
                    else if (a.Exception.GetBaseException() == null)
                    {
                        errorMessage = a.Exception.GetBaseException().Message;
                        errorStackTrace = a.Exception.GetBaseException().StackTrace;
                    }
                }
            }
            else if (e != null && e.ApplicationException != null)
            {
                status = 3;
                errorMessage = e.ApplicationException.Message;
                errorStackTrace = e.ApplicationException.StackTrace;
            }
            else if (e != null && e.error != null && e.error.innererror != null)
            {
                status = 3;
                errorMessage = e.error.innererror.message;
                errorStackTrace = e.error.innererror.stacktrace;
            }

            var connectionString = ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("erp.update_transfer_status"))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action_id", id);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@error_message", errorMessage);
                    cmd.Parameters.AddWithValue("@error_stack_trace", errorStackTrace);
                    cmd.ExecuteNonQuery();
                }
            }

        }


        public static void UpdateCreatedProductStatus(int temp_id, int option_id, AxBaseException ex)
        {
            int status = 2;
            if (ex != null && ex.error != null && ex.error.innererror != null)
            {
                status = -1;
                LogError(ex.error.innererror.message, ex.error.innererror.stacktrace, "create_product", -99);
            }

            var connectionString = ConfigurationManager.ConnectionStrings["prod_connection"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("bm.products_created_update_status"))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@temp_id", temp_id);
                    cmd.Parameters.AddWithValue("@option_id", option_id);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateOrderStatus(int order_id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["prod_connection"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("dbo.orders_set_transfered"))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@order_id", order_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<ItemToCreate> GetItemsToCreate(int? temp_id)
        {
            List<ItemToCreate> items = new List<ItemToCreate>();
            var connectionString = ConfigurationManager.ConnectionStrings["prod_connection"].ConnectionString;
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("bm.get_products_to_transfer"))
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (temp_id.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@temp_id", temp_id);
                    }
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (temp_id.HasValue)
                            {
                                var item = new ItemToCreate
                                {
                                    product_no = DataWriter.ReadString(reader, 0),
                                    product_name = DataWriter.ReadString(reader, 1),
                                    description = DataWriter.ReadString(reader, 2),
                                    division_no = DataWriter.ReadString(reader, 3),
                                    division = DataWriter.ReadString(reader, 4),
                                    department_no = DataWriter.ReadString(reader, 5),
                                    department = DataWriter.ReadString(reader, 6),
                                    sup_department_no = DataWriter.ReadString(reader, 7),
                                    sup_department = DataWriter.ReadString(reader, 8),
                                    option_name_no = DataWriter.ReadString(reader, 9),
                                    option_name = DataWriter.ReadString(reader, 10),
                                    size_no = DataWriter.ReadString(reader, 11),
                                    size = DataWriter.ReadString(reader, 12),
                                    color_no = DataWriter.ReadString(reader, 13),
                                    color = DataWriter.ReadString(reader, 14),
                                    color_group_no = DataWriter.ReadString(reader, 15),
                                    color_group = DataWriter.ReadString(reader, 16),
                                    size_group_no = DataWriter.ReadString(reader, 17),
                                    size_group = DataWriter.ReadString(reader, 18),
                                    temp_id = DataWriter.ReadInt(reader, 19).Value,
                                    master_status = DataWriter.ReadInt(reader, 20).Value,
                                    min_order_qty = DataWriter.ReadDecimal(reader, 21),
                                    pack_size = DataWriter.ReadDecimal(reader, 22),
                                    display_stock = DataWriter.ReadDecimal(reader, 23),
                                    option_id = DataWriter.ReadInt(reader, 24).Value,
                                    primar_vendor_no = DataWriter.ReadString(reader, 25),
                                    sale_price = DataWriter.ReadDecimal(reader, 26),
                                    cost_price = DataWriter.ReadDecimal(reader, 27)
                                };
                                items.Add(item);
                            }
                            else
                            {
                                var item = new ItemToCreate
                                {
                                    temp_id = DataWriter.ReadInt(reader, 0).Value
                                };
                                items.Add(item);
                            }

                        }
                    }
                }
            }
            return items;
        }

        public static void WriteToTable<T>(IList list, string tableName, object value, string propertyName)
        {
            WriteToTable<T>(((List<T>)list).GetDataReader<T>(), tableName, value, propertyName);
        }

        public static void WriteToTable<T>(IGenericDataReader<T> reader, string tableName, object value = null, string propertyName = null)
        {
            if (reader.HasRows())
            {
                if (value != null && propertyName != null)
                {
                    foreach (var listItem in reader.GetGenericList())
                    {
                        PropertyInfo propInfo = listItem.GetType().GetProperty(propertyName);
                        propInfo.SetValue(listItem, Convert.ChangeType(value, propInfo.PropertyType));
                    }
                }
                using (var con = new SqlConnection(StgConnectionString))
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
                        copy.BulkCopyTimeout = 30;
                        copy.WriteToServer(reader);
                    }
                }
            }
        }
        public static string GetSetting(string settingId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(StgConnectionString))
                {
                    using (var cmd = new SqlCommand("ctr.getDataTransferSetting_Str", con))
                    {
                        cmd.Connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SettingId", settingId);
                        return (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                LogError("Error reading settingId from DB." + settingId,e.StackTrace, "ErpConnector", e.HResult);
                return null;
            }
        }

        public static void LogErpActionStep(int actionId, string step, DateTime startTime, bool success, string errorMessage, string errorStackTrace, int erpActionTaskStepId)
        {
            using (var con = new SqlConnection(StgConnectionString))
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
                    cmd.Parameters.AddWithValue("@erp_action_task_step_id", erpActionTaskStepId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Int64 GetMaxRecId(string tableName)
        {
            using (var con = new SqlConnection(StgConnectionString))
            {
                using (var cmd = new SqlCommand("[erp].[find_max_rec_id]", con))
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
        public static void TruncateSingleTable(string tableName)
        {
            using (var con = new SqlConnection(StgConnectionString))
            {
                using (var cmd = new SqlCommand("[erp].[truncate_single_erp_table]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@full_table_name", tableName);
                    cmd.ExecuteNonQuery();
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
            using (var con = new SqlConnection(StgConnectionString))
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
        public static List<string> ValidateColumnMappingDBSide<T>(string destTable)
        {
            string query = "select top 1 * from " + destTable;
            List<string> result = new List<string>();
            using (var con = new SqlConnection(StgConnectionString))
            {
                con.Open();
                using (var adapter = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    Type baseType = typeof(T);
                    var columns = (from DataColumn c in dt.Columns
                                   select c.ColumnName).ToList();
                    foreach (var pi in baseType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
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

        public static List<string> GetIdsFromEntities(string procedure)
        {
            List<string> result = new List<string>();
            using (var con = new SqlConnection(StgConnectionString))
            {
                using (var cmd = new SqlCommand(procedure, con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    var reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        result.Add(reader.GetString(0));
                    }
                }
            }
            return result;
        }
        #region Listener calls
        public static List<ErpActions> GetPendingActions(bool includeBAndM)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("[erp].[pending_transfers]", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                var reader = command.ExecuteReader();
                List<ErpActions> actions = new List<ErpActions>();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        ErpActions a = new ErpActions();
                        a.id = reader.GetInt32(0);
                        a.action_type = reader.GetString(1);
                        a.reference_id = reader.IsDBNull(2) ? -1 : reader.GetInt32(2);
                        a.user_id = reader.IsDBNull(3) ? -1 : reader.GetInt32(3);
                        a.status = reader.GetInt32(4);
                        a.created_at = reader.GetDateTime(5);
                        a.updated_at = reader.GetDateTime(6);
                        a.date_reference_id = ReadInt(reader, 7);
                        a.no_parallel_process = ReadInt(reader, 8);
                        a.on_failure_retry_attempts = ReadInt(reader, 9);
                        actions.Add(a);
                    }
                }
                return actions;
            }
        }

        public static void InsertAction(string action_type, int? reference_id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("erp.insert_erp_action"))
                {
                    connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@action_type", action_type);
                    cmd.Parameters.AddWithValue("@reference_id", reference_id);
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@action_id", Direction = ParameterDirection.Output, DbType = DbType.Int32 });
                    cmd.ExecuteNonQuery();

                }
            }

        }
        public static List<POTOCreate> GetPoToToCreate(int order_id)
        {
            List<POTOCreate> orders = new List<POTOCreate>();
            var connectionString = ConfigurationManager.ConnectionStrings["prod_connection"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("dbo.get_orders_to_transfer"))
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@order_id", order_id);
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var order = new POTOCreate
                            {
                                order_id = ReadInt(reader, 0).Value,
                                item_no = ReadString(reader, 1),
                                location_no = ReadString(reader, 2),
                                order_from_location_no = ReadString(reader, 3),
                                color = ReadString(reader, 4),
                                size = ReadString(reader, 5),
                                style = ReadString(reader, 6),
                                user_id = ReadInt(reader, 7).Value,
                                unit_qty_chg = ReadDecimal(reader, 8).Value,
                                est_delivery_date = reader.IsDBNull(9) ? DateTime.MaxValue : reader.GetDateTime(9),
                                vendor_location_type = ReadString(reader, 10),
                                site_id = ReadString(reader, 11),
                                channel_id = ReadString(reader, 12),
                                warehouse = ReadString(reader, 13),
                                unit = reader.IsDBNull(14) ? "ST" : reader.GetString(14),
                                description = ReadString(reader, 15)
                            };
                            orders.Add(order);
                        }
                    }
                }
            }
            return orders;
        }
        public static ErpTask GetTask(int taskId)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString))
            {
                using (var cmd = new SqlCommand("[erp].[get_action_task]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action_task_id", taskId);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ErpTask task = new ErpTask
                        {
                            Id = reader.GetInt32(0),
                            Name = ReadString(reader, 1),
                            truncate_items = reader.GetBoolean(2),
                            truncate_sales_trans_dump = reader.GetBoolean(3),
                            truncate_locations_and_vendors = reader.GetBoolean(4),
                            truncate_sales_trans_refresh = reader.GetBoolean(5),
                            truncate_lookup_info = reader.GetBoolean(6),
                            truncate_bom = reader.GetBoolean(7),
                            truncate_po_to = reader.GetBoolean(8),
                            truncate_price = reader.GetBoolean(9),
                            truncate_attribute_refresh = reader.GetBoolean(10),
                            no_of_parallel_processes = reader.GetInt32(11)
                        };
                        task.Steps = GetTaskSteps(taskId);
                        return task;
                    }
                    return null;
                }
            }
        }
        public static ErpTaskStep GetStep(int stepId)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString))
            {
                using (var cmd = new SqlCommand("erp.get_action_task_step", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action_step_id", stepId);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new ErpTaskStep
                        {
                            Id = reader.GetInt32(0),
                            StepName = reader.GetString(1),
                            EndPoint = ReadString(reader, 2),
                            ServiceMethod = ReadString(reader, 3),
                            ServiceName = ReadString(reader, 4),
                            TaskType = (ErpTaskStep.ErpTaskType)reader.GetInt32(5),
                            ReturnTypeStr = reader.GetString(6),
                            ReturnTypeAssembly = ReadString(reader, 7),
                            DbTable = ReadString(reader, 8),
                            EndpointFilter = ReadString(reader, 9),
                            MaxPageSize = ReadInt(reader, 10),
                            PeriodIncrement = (ErpTaskStep.PeriodIncrementType)(reader.IsDBNull(11) ? 0 : reader.GetInt32(11)),
                            Priority = reader.GetInt32(12),
                            AuthenitcationType = (ErpTaskStep.AuthenticationType)(reader.IsDBNull(13) ? 1 : reader.GetInt32(13)),
                            ExternalProcess = ReadString(reader, 14),
                            ExternalProcessArgument = ReadString(reader, 15),
                            BaseTypeProcedure = ReadString(reader, 16),
                            InjectionPropertyName = ReadString(reader, 17)
                        };
                    }
                    return null;
                }
            }
        }
        private static List<ErpTaskStepDetails> GetTaskStepDetails(int taskId)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString))
            {
                using (var cmd = new SqlCommand("erp.get_action_task_step_details", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@erp_action_task_id", taskId);
                    var reader = cmd.ExecuteReader();
                    var result = new List<ErpTaskStepDetails>();
                    while(reader.Read())
                    {
                        result.Add(new ErpTaskStepDetails
                        {
                            id = reader.GetInt32(0),
                            erp_action_task_step_id = reader.GetInt32(1),
                            return_type = ReadString(reader, 2),
                            return_type_assembly = ReadString(reader, 3),
                            nested_property_name = ReadString(reader, 4),
                            db_table = ReadString(reader, 5),
                            base_type_procedure = ReadString(reader, 6),
                            injection_property_name = ReadString(reader, 7),
                            stored_procedure = ReadString(reader, 8),
                            stored_procedure_parameters = ReadString(reader, 9)
                        });
                    }
                    return result;
                }
            }
        }
        private static List<ErpTaskStep> GetTaskSteps(int taskId)
        {
            var details = GetTaskStepDetails(taskId);
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString))
            {                
                using (var cmd = new SqlCommand("erp.get_action_task_steps", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action_task_id", taskId);
                    var reader = cmd.ExecuteReader();
                    List<ErpTaskStep> result = new List<ErpTaskStep>();
                    while (reader.Read())
                    {
                        result.Add(
                            new ErpTaskStep
                            {
                                Id = reader.GetInt32(0),
                                StepName = reader.GetString(1),
                                EndPoint = ReadString(reader, 2),
                                ServiceMethod = ReadString(reader, 3),
                                ServiceName = ReadString(reader, 4),
                                TaskType = (ErpTaskStep.ErpTaskType)reader.GetInt32(5),
                                ReturnTypeStr = reader.GetString(6),
                                ReturnTypeAssembly = ReadString(reader, 7),
                                DbTable = ReadString(reader, 8),
                                EndpointFilter = ReadString(reader, 9),
                                MaxPageSize = ReadInt(reader, 10),
                                PeriodIncrement = (ErpTaskStep.PeriodIncrementType)(reader.IsDBNull(11) ? 0 : reader.GetInt32(11)),
                                Priority = reader.GetInt32(12),
                                AuthenitcationType = (ErpTaskStep.AuthenticationType)(reader.IsDBNull(13) ? 1 : reader.GetInt32(13)),
                                ExternalProcess = ReadString(reader, 14),
                                ExternalProcessArgument = ReadString(reader, 15),
                                BaseTypeProcedure = ReadString(reader, 16),
                                InjectionPropertyName = ReadString(reader, 17),
                                Details = details.Where(x=>x.erp_action_task_step_id == reader.GetInt32(0)).ToList()
                            });
                    }
                    return result;
                }
            }
        }

        public static List<ErpTaskStep> GetFailedSteps(int taskId, int actionId, int iteration)
        {
            var details = GetTaskStepDetails(taskId);
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString))
            {
                using (var cmd = new SqlCommand("[erp].[get_failed_steps]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action_task_id", taskId);
                    cmd.Parameters.AddWithValue("@erp_action_id", actionId);
                    cmd.Parameters.AddWithValue("@iteration", iteration);
                    var reader = cmd.ExecuteReader();
                    List<ErpTaskStep> result = new List<ErpTaskStep>();
                    while (reader.Read())
                    {
                        result.Add(
                            new ErpTaskStep
                            {
                                Id = reader.GetInt32(0),
                                StepName = reader.GetString(1),
                                EndPoint = ReadString(reader, 2),
                                ServiceMethod = ReadString(reader, 3),
                                ServiceName = ReadString(reader, 4),
                                TaskType = (ErpTaskStep.ErpTaskType)reader.GetInt32(5),
                                ReturnTypeStr = reader.GetString(6),
                                ReturnTypeAssembly = ReadString(reader, 7),
                                DbTable = ReadString(reader, 8),
                                EndpointFilter = ReadString(reader, 9),
                                MaxPageSize = ReadInt(reader, 10),
                                PeriodIncrement = (ErpTaskStep.PeriodIncrementType)(reader.IsDBNull(11) ? 0 : reader.GetInt32(11)),
                                Priority = reader.GetInt32(12),
                                AuthenitcationType = (ErpTaskStep.AuthenticationType)(reader.IsDBNull(13) ? 1 : reader.GetInt32(13)),
                                ExternalProcess = ReadString(reader, 14),
                                ExternalProcessArgument = ReadString(reader, 15),
                                BaseTypeProcedure = ReadString(reader, 16),
                                InjectionPropertyName = ReadString(reader, 17),
                                Details = details.Where(x => x.erp_action_task_step_id == reader.GetInt32(0)).ToList()
                            });
                    }
                    return result;
                }
            }
        }
        public static List<ErpActionLogStep> GetActionSteps(int actionId)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString))
            {
                using (var cmd = new SqlCommand("log.get_erp_action_steps", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@erp_action_id", actionId);
                    var reader = cmd.ExecuteReader();
                    List<ErpActionLogStep> result = new List<ErpActionLogStep>();
                    while (reader.Read())
                    {
                        result.Add(
                            new ErpActionLogStep
                            {
                                StepName = ReadString(reader, 0),
                                DBTable = ReadString(reader, 1),
                                D365Endpoint = ReadString(reader, 2),
                                StartTime = reader.GetDateTime(3),
                                EndTime = ReadDateTime(reader, 4),
                                Success = ReadBoolean(reader, 5),
                                ErrorMessage = ReadString(reader, 6),                               
                                ErpActionTaskStepId = ReadInt(reader,8),
                                iteration = ReadInt(reader,9),
                            });
                    }
                    return result;
                }
            }
        }
        #endregion
        #region Api specific Functions
        public static void RunNonQueryWithoutParamsStg(string storedProcedure, bool longTimeOut)
        {
            using (var con = new SqlConnection(StgConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand(storedProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (longTimeOut)
                    {
                        cmd.CommandTimeout = 3600;
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static async Task<SyncDTO> MergeSyncTask(SyncDTO syncDTO)
        {
            using (var con = new SqlConnection(StgConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("erp.sync_log_update", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", syncDTO.id.HasValue ? (object)syncDTO.id.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", (short)syncDTO.status);
                    cmd.Parameters.AddWithValue("@message", syncDTO.message);

                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows && reader.Read())
                    {
                        syncDTO.id = reader.GetInt32(0);
                    }

                    return syncDTO;
                }
            }
        }

        public static async Task WriteDTO<T>(IDataReader reader, CancellationToken token, string fullTableName)
        {
            using (var con = new SqlConnection(StgConnectionString))
            {
                con.Open();
                using (var copy = new SqlBulkCopy(con))
                {
                    var mappings = GetDynamicBulkCopyColumnMapping<T>();
                    foreach (var m in mappings)
                    {
                        copy.ColumnMappings.Add(m);
                    }
                    copy.BulkCopyTimeout = 360;
                    copy.DestinationTableName = fullTableName;

                    await copy.WriteToServerAsync(reader, token);
                }
            }
        }
        public static async Task<IList<AGROrderHeaderDTO>> GetAGROrderHeaders(CancellationToken token)
        {
            using (var con = new SqlConnection(ProdConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("[dbo].[get_orders_to_transfer]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var reader = await cmd.ExecuteReaderAsync(token);
                    var results = new List<AGROrderHeaderDTO>();
                    while (reader.Read())
                    {
                        results.Add
                            (
                                new AGROrderHeaderDTO
                                {
                                    agr_order_id = reader.GetInt32(0),
                                    order_from_location_no = reader.GetString(2),
                                    order_to_location_no = reader.GetString(1),
                                    est_deliv_date = reader.GetDateTime(3),
                                    order_type = (AGRConstants.AGR_ORDER_TYPE)reader.GetInt32(4)
                                }
                            );
                    }
                    return results;
                }
            }
        }

        public static async Task<IList<AGROrderLineDTO>> GetAGROrderLines(int orderId, CancellationToken token)
        {
            using (var con = new SqlConnection(ProdConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("[dbo].[get_orders_to_transfer]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@order_id", orderId);
                    var reader = await cmd.ExecuteReaderAsync(token);
                    var results = new List<AGROrderLineDTO>();
                    while (reader.Read())
                    {
                        results.Add
                            (
                                new AGROrderLineDTO
                                {
                                    agr_order_id = reader.GetInt32(0),
                                    item_no = reader.GetString(1),
                                    order_to_location_no = ReadString(reader, 2),
                                    color = ReadString(reader, 4),
                                    size = ReadString(reader, 5),
                                    style = ReadString(reader, 6),
                                    qty = ReadDecimal(reader, 8).Value
                                }
                            );
                    }
                    return results;
                }
            }

        }
        public static async Task LogAGROrderCallback(AGROrderResponseDTO response, CancellationToken token)
        {
            await LogAGROrderAction(response, token, "call_back");
        }
        public static async Task LogAGROrderAction(AGROrderResponseDTO response, CancellationToken token, string action)
        {
            using (var con = new SqlConnection(StgConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("[log].[insert_order_transfer_log]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@order_id", response.agr_order_id);
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@success", response.erp_order_status == AGRConstants.ERP_ORDER_STATUS.COMPLETED);
                    cmd.Parameters.AddWithValue("@order_type", response.agr_order_type);
                    cmd.Parameters.AddWithValue("@error_message", response.error_message);
                    cmd.Parameters.AddWithValue("@error_stack_trace", response.error_message);
                    await cmd.ExecuteNonQueryAsync(token);
                }
            }
        }

        public static async Task UpdateOrderStatus(AGROrderResponseDTO response, CancellationToken token)
        {
            using (var con = new SqlConnection(ProdConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("[dbo].[orders_update_transfer_status]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@order_id", response.agr_order_id);
                    if (response.erp_order_status == AGRConstants.ERP_ORDER_STATUS.COMPLETED)
                    {
                        cmd.Parameters.AddWithValue("@status", 2);
                        cmd.Parameters.AddWithValue("@description", "ERP Order number: " + response.erp_order_no);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@status", 1);
                        cmd.Parameters.AddWithValue("@description", "Order creation failed on the ERP side. See [log].[order_transfer_log] for details");
                    }
                    await cmd.ExecuteNonQueryAsync(token);
                }
            }
        }
        #endregion
        #region Utility Functions
        public static string ReadString(IDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return null;
            }
            return reader.GetString(index);
        }
        public static int? ReadInt(IDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return null;
            }
            return reader.GetInt32(index);
        }
        public static decimal? ReadDecimal(IDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return null;
            }
            return reader.GetDecimal(index);
        }
        public static DateTime? ReadDateTime(IDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return null;
            }
            return reader.GetDateTime(index);
        }
        public static bool? ReadBoolean(IDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return null;
            }
            return reader.GetBoolean(index);
        }
        #endregion
    }
}
