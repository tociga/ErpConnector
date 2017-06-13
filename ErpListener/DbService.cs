using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ErpConnector.Listener
{
    public class DbService
    {
        public bool? Sync()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["stg_connection"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("[erp].[pending_transfers]", connection) {
                    CommandType = CommandType.StoredProcedure
                };
                var pendingParam = command.Parameters.Add("@pendingDataTransfer", SqlDbType.Bit);
                pendingParam.Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                var pendingDataTransfer = false;
                bool.TryParse(pendingParam.Value.ToString(), out pendingDataTransfer);
            
                if (pendingDataTransfer)
                {
                    // Run transfer
                    var erpType = ConfigurationManager.AppSettings["erp_type"];
                    var connector = new GenericConnector(erpType);
                    connector.RunTransfer();

                    // Mark as transferred in db
                    var markTransferredParam = command.Parameters.Add("@markTransferred", SqlDbType.Bit);
                    markTransferredParam.Direction = ParameterDirection.Input;
                    markTransferredParam.Value = true;

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                return null;
            }
        }
    }
}
