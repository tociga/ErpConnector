using System;

namespace ErpConnector.Common.Util
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
