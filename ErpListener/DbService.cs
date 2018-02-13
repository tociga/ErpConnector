using ErpConnector.Common.AGREntities;
using ErpConnector.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ErpConnector.Listener
{
    public class DbService
    {
        public bool? Sync()
        {            
            try
            {
                bool includeBAndM = false;
                Boolean.TryParse(ConfigurationManager.AppSettings["includeBAndM"], out includeBAndM);
                var actions = GetPendingActions(includeBAndM);
                foreach (var action in actions)
                {
                    if (action.status == 0)
                    {
                        // Run transfer
                        var erpType = ConfigurationManager.AppSettings["erp_type"];
                        var connector = new GenericConnector(erpType);


                        switch (action.action_type)
                        {
                            case "daily_refresh":
                                UpdateActionStatus(action.id, 1, null);
                                var connectorTask = connector.DailyRefresh(GetDateById(action.reference_id), action.id);
                                connectorTask.ContinueWith((m) => UpdateActionStatus(action.id, 2, m));
                                break;
                            case "full_refresh":
                                UpdateActionStatus(action.id, 1, null);
                                connectorTask = connector.FullTransfer(action.id);
                                connectorTask.ContinueWith((m) => UpdateActionStatus(action.id, 2, m));
                                break;
                            case "transaction_full":
                                UpdateActionStatus(action.id, 1, null);
                                connectorTask = connector.TransactionFull(action.id);
                                connectorTask.ContinueWith((m) => UpdateActionStatus(action.id, 2, m));
                                break;
                            case "transaction_refresh":
                                UpdateActionStatus(action.id, 1, null);
                                connectorTask = connector.TransfactionRefresh(GetDateById(action.reference_id), action.id);
                                connectorTask.ContinueWith((m) => UpdateActionStatus(action.id, 2, m));
                                break;
                            case "pim_full":
                                UpdateActionStatus(action.id, 1, null);
                                connectorTask = connector.PimFull(action.id);
                                connectorTask.ContinueWith((mark) => UpdateActionStatus(action.id, 2, mark)).Wait();
                                break;
                            case "create_po_to":
                                UpdateActionStatus(action.id, 1, null);
                                var orders = GetPoToToCreate(action.reference_id);
                                connectorTask = connector.CreatePoTo(orders, action.id);
                                connectorTask.ContinueWith((mark) => UpdateActionStatus(action.id, 2, mark)).Wait();
                                UpdateOrderStatus(action.reference_id);
                                break;
                            case "create_item":
                                if (includeBAndM)
                                {
                                    UpdateActionStatus(action.id, 1, null);
                                    var itemsToCreate = GetItemsToCreate(action.reference_id);
                                    connectorTask = connector.CreateItem(itemsToCreate, action.id);
                                    connectorTask.ContinueWith((mark) => UpdateActionStatus(action.id, 2, mark)).Wait();
                                    var options = itemsToCreate.Select(x => x.option_id).Distinct();
                                    foreach (int option in options)
                                    {
                                        UpdateCreatedProductStatus(action.reference_id, option, connectorTask.Result);
                                    }
                                }
                                break;
                            case "update_product":
                                if (includeBAndM)
                                {
                                    UpdateActionStatus(action.id, 1, null);
                                    connectorTask = connector.UpdateProductAttributes(action.id);
                                    connectorTask.ContinueWith((mark) => UpdateActionStatus(action.id, 2, mark)).Wait();
                                }
                                break;
                            case "confirm_items":
                                if (includeBAndM)
                                {
                                    var itemsToCreate = GetItemsToCreate(action.reference_id);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
            catch(Exception e)
            {
                LogCommError(e.Message + " " + e.StackTrace, -99);
            }
            return true;
        }

        public DateTime GetDateById(int dateId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["prod_connection"].ConnectionString;
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[get_date_by_id]"))
                {
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date_id",dateId);

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
        public void LogCommError(string errorMessage, int temp_id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["prod_connection"].ConnectionString;
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("[dbo].[log_action_execution]"))
                {
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action_exec_id", -99);
                    cmd.Parameters.AddWithValue("@extra_info", errorMessage);
                    cmd.Parameters.AddWithValue("@action_type", "create_erp_item");
                    cmd.Parameters.AddWithValue("@reference_id", temp_id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateOrderStatus(int order_id)
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
        public void UpdateCreatedProductStatus(int temp_id, int option_id, AxBaseException ex)
        {
            int status = 2;
            if (ex != null && ex.error != null && ex.error.innererror != null)
            {
                status = -1;
                LogCommError(ex.error.innererror.stacktrace, temp_id);
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

        public void UpdateActionStatus(int id, int status, Task<AxBaseException> a)
        {
            string errorMessage = null;
            string errorStackTrace = null;
            if ( a != null && a.IsFaulted)
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
            else if (a != null && a.Result != null && a.Result.ApplicationException != null)
            {
                status = 3;
                errorMessage = a.Result.ApplicationException.Message;
                errorStackTrace = a.Result.ApplicationException.StackTrace;
            }
            else if (a != null && a.Result != null && a.Result.error != null && a.Result.error.innererror != null)
            {
                status = 3;
                errorMessage = a.Result.error.innererror.message; 
                errorStackTrace = a.Result.error.innererror.stacktrace;
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
        private List<ErpActions> GetPendingActions(bool includeBAndM)
        {
            if (includeBAndM)
            {
                var pendingItems = GetItemsToCreate(null);
                foreach (var item in pendingItems)
                {
                    InsertAction("create_item", item.temp_id);
                }
            }          

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
                        actions.Add(a);
                    }
                }
                return actions;
            }
        }

        private static void InsertAction(string action_type, int? reference_id)
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
                        while(reader.Read())
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
                                warehouse = ReadString(reader, 13)
                            };
                            orders.Add(order);
                        }
                    }
                }
            }
            return orders;
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
                        while(reader.Read())
                        {
                            if (temp_id.HasValue)
                            {
                                var item = new ItemToCreate
                                {
                                    product_no = ReadString(reader, 0),
                                    product_name = ReadString(reader, 1),
                                    description = ReadString(reader, 2),
                                    division_no = ReadString(reader, 3),
                                    division = ReadString(reader, 4),
                                    department_no = ReadString(reader, 5),
                                    department = ReadString(reader, 6),
                                    sup_department_no = ReadString(reader, 7),
                                    sup_department = ReadString(reader, 8),
                                    option_name_no = ReadString(reader, 9),
                                    option_name = ReadString(reader, 10),
                                    size_no = ReadString(reader, 11),
                                    size = ReadString(reader, 12),
                                    color_no = ReadString(reader, 13),
                                    color = ReadString(reader, 14),
                                    color_group_no = ReadString(reader, 15),
                                    color_group = ReadString(reader, 16),
                                    size_group_no = ReadString(reader, 17),
                                    size_group = ReadString(reader, 18),
                                    temp_id = ReadInt(reader, 19).Value,
                                    master_status = ReadInt(reader, 20).Value,
                                    min_order_qty = ReadDecimal(reader, 21),
                                    pack_size = ReadDecimal(reader, 22),
                                    display_stock = ReadDecimal(reader, 23),
                                    option_id = ReadInt(reader, 24).Value,
                                    primar_vendor_no = ReadString(reader, 25),
                                    sale_price = ReadDecimal(reader, 26),
                                    cost_price = ReadDecimal(reader, 27)
                                };
                                items.Add(item);
                            }
                            else
                            {
                                var item = new ItemToCreate
                                {
                                    temp_id = ReadInt(reader, 0).Value
                                };
                                items.Add(item);
                            }
                            
                        }
                    }
                }
            }
            return items;
        }
        private static string ReadString(IDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return null;
            }
            return reader.GetString(index);
        }
        private static int? ReadInt(IDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return null;
            }
            return reader.GetInt32(index);
        }
        private static decimal? ReadDecimal(IDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return null;
            }
            return reader.GetDecimal(index);
        }
    }


}
