using ErpConnector.Common.Util;
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
        public static void SetAGR5OrderAsTransfered(int orderId, string erpTranNr, string transferType, string itemNo, string locationNo)
        {
            SqlCommand cmd = null;
            try
            {
                using (SqlConnection con = new SqlConnection(PROD_ConnectionString))
                {
                    con.Open();
                    using (cmd = new SqlCommand("cus_sap_orders_update_status", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@order_id", SqlDbType.Int);
                        cmd.Parameters.Add("@status", SqlDbType.Int);
                        cmd.Parameters.Add("@erp_transaction_nr", SqlDbType.NVarChar);
                        cmd.Parameters.Add("@transfer_type", SqlDbType.NVarChar);

                        cmd.Parameters["@order_id"].Value = orderId;
                        cmd.Parameters["@status"].Value = 2;
                        cmd.Parameters["@erp_transaction_nr"].Value = erpTranNr;
                        cmd.Parameters["@transfer_type"].Value = transferType;

                        if (!string.IsNullOrEmpty(itemNo) && !string.IsNullOrEmpty(locationNo))
                        {
                            cmd.Parameters.AddWithValue("@item_no", itemNo);
                            cmd.Parameters.AddWithValue("@location_no", locationNo);
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                DataWriter.LogError("Error when setting order status to transfered : " + orderId + e.Message, e.StackTrace, cmd == null ? "" : cmd.ToString(),e.HResult);
            }
        }

    }
}
