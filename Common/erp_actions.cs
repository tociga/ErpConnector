//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ErpConnector.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class erp_actions
    {
        public int id { get; set; }
        public string action_type { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<bool> success { get; set; }
    }
}
