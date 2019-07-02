using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.ErpTasks
{
    public class ErpTaskStepDetails
    {
        public int id { get; set; }
        public int erp_action_task_step_id { get; set; }
        public string nested_property_name { get; set; }
        public string db_table { get; set; }
        public string return_type { get; set; }
        public string return_type_assembly { get; set; }
        public string base_type_procedure { get; set; }
        public string injection_property_name { get; set; }
        public string stored_procedure { get; set; }
        public string stored_procedure_parameters { get; set; }

        public Type GetReturnType()
        {
            if (string.IsNullOrEmpty(return_type) || string.IsNullOrEmpty(return_type_assembly))
            {
                return null;
            }
            string fullName = return_type + "," + return_type_assembly;
            return Type.GetType(fullName);
        }
    }
}
