﻿using ErpConnector.Common.AGREntities;
using ErpConnector.Common.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DB
{
    public class AxDbHandler
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

        public static List<ItemToCreate> GetItemsToCreate(int? temp_id)
        {
            List<ItemToCreate> items = new List<ItemToCreate>();
            using (var con = new SqlConnection(PROD_ConnectionString))
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

        public static List<AGRProductLifeCycleState> GetProductLifeCycleStateUpdates(int plc_update_id)
        {
            List<AGRProductLifeCycleState> plc = new List<AGRProductLifeCycleState>();
            using (var con = new SqlConnection(PROD_ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("bm.get_product_lifecycle_update", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@product_lifecycle_status_update_id", plc_update_id);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        plc.Add(
                            new AGRProductLifeCycleState
                            {
                                product_lifecycle_state_update_id = DataWriter.ReadInt(reader, 0).Value,
                                product_id = DataWriter.ReadInt(reader, 1).Value,
                                product_no = DataWriter.ReadString(reader, 2),
                                product_size_id = DataWriter.ReadString(reader, 3),
                                product_color_id = DataWriter.ReadString(reader, 4),
                                product_style_id = DataWriter.ReadString(reader, 5),
                                product_config_id = DataWriter.ReadString(reader, 6),
                                lifecycle_status = DataWriter.ReadString(reader, 7)
                            });
                    }
                }
            }
            return plc;
        }
        public static void UpdateProductLifeCycleState(int plcId, int actionId, int status)
        {            
            using (var con = new SqlConnection(PROD_ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand("bm.update_product_lifecycle_update", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", plcId);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@erp_action_id", actionId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateProductMasterLifecycleState(string productMasterNo, string plcState)
        {
            using (var con = new SqlConnection(STG_ConnectionString))
            {
                using (var cmd = new SqlCommand("[ax].[update_product_master_lifecycle_state]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@product_master_no", productMasterNo);
                    cmd.Parameters.AddWithValue("@lifecycle_state", plcState);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateProductVariantLifecycleState(string productMasterNo, string size, string color, string style, string config, string plcState)
        {
            using (var con = new SqlConnection(STG_ConnectionString))
            {
                using (var cmd = new SqlCommand("[ax].[update_product_variant_lifecycle_state]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("@product_master_no", productMasterNo);
                    cmd.Parameters.AddWithValue("@product_color_id", color);
                    cmd.Parameters.AddWithValue("@product_size_id", size);
                    cmd.Parameters.AddWithValue("@product_style_id", style);
                    cmd.Parameters.AddWithValue("@product_config_id", config);
                    cmd.Parameters.AddWithValue("@lifecycle_state", plcState);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void RefreshPLCStateBulkStg(string ids)
        {
            using (var con = new SqlConnection(PROD_ConnectionString))
            {
                using (var cmd = new SqlCommand("[bm].[stg_refresh_product_lifecycle_state_bulk]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 600;
                    cmd.Parameters.AddWithValue("@product_id", ids);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void RefreshPLCStateStg(string productNo)
        {
            using (var con = new SqlConnection(PROD_ConnectionString))
            {
                using (var cmd = new SqlCommand("[bm].[stg_refresh_product_lifecycle_state]", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 600;
                    cmd.Parameters.AddWithValue("@product_no", productNo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
