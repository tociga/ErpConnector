using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxConnect.Microsoft.Dynamics.DataEntities;

namespace AxConnect.DTO
{
    public class CustVendExternalItemsDTO
    {
        private ModuleInventPurchSalesVendCustGroup _moduleType;
        private ABC _abc;
        public object ModuleType
        {
            get { return (int)_moduleType; }
            set { _moduleType = DTOUtil.GetEnumFromObj<ModuleInventPurchSalesVendCustGroup>(value, ModuleInventPurchSalesVendCustGroup.Invent); }
        }
        public string ExternalItemId { get; set; }
        public string CustVendRelation { get; set; }
        public string Description { get; set; }
        public object ABCCategory
        {
            get { return (int)_abc; }
            set { _abc = DTOUtil.GetEnumFromObj<ABC>(value, ABC.None); }
        }
        public string ExternalItemTxt { get; set; }
        public string dataAreaId { get; set; }
        public string ItemId { get; set; }
        public string InventDimId { get; set; }
    }
}
