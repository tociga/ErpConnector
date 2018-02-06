using ErpDTO.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxConCommon.GenerateScripts;

namespace AxConnect.Modules
{
    public class ScriptGeneratorModule
    {
        public static string GenerateScript(string entity)
        {
            
            //ErpDTO.Microsoft.Dynamics.DataEntities.PurchaseOrderLine
            Type entityObject = Type.GetType("ErpDTO.Microsoft.Dynamics.DataEntities." + entity+",ErpDTO");

            if (entityObject != null)
            {
                ScriptGenerator sg = new ScriptGenerator(entityObject);
                return sg.CreateScript("[ax].[" + entity +"]");
            }
            return "";            
        }
    }
}
