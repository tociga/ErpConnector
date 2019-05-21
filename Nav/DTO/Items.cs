using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nav.DTO
{
    public class Items
    {
        [JsonProperty("No")]
        public string item_no { get; set; }
        [JsonProperty("Vendor_No")]
        public string primary_vendor_no { get; set; }
        [JsonProperty("Description")]
        public string description { get; set; }
        [JsonProperty("Description_2")]
        public string description_2 { get; set; }
        [JsonProperty("Blocked")]
        public bool closed { get; set; }
        [JsonProperty("Gross_Weight")]
        public decimal weight { get; set; }
        [JsonProperty("Unit_Volume")]
        public decimal volume { get; set; }
        [JsonProperty("Unit_Price")]
        public decimal sales_price { get; set; }                
        [JsonProperty("Vendor_Item_No")]    
        public string original_item_no { get; set; }
        [JsonProperty("Minimum_Order_Quantity")]
        public int min_order { get; set; }
        [JsonProperty("Unit_Cost")]
        public decimal cost_price { get; set; }
        [JsonProperty("Order_Multiple")]
        public int order_multiple { get; set; }
        [JsonProperty("Last_Date_Modified")]
        public DateTime last_date_modified { get; set; }
    }
}
