using AxConnect.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class CategoryDTO
    {
        private string _isActive;
        private EcoResCategoryChangeStatus? _changeStatus;
        public long CategoryHierarchy { get; set; }
        public long ParentCategory { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public object IsActive
        {
            get
            {
                return !string.IsNullOrEmpty(_isActive) && _isActive.ToLower() == "yes";
            }
            set
            {
                _isActive = value.ToString();
            }
        }
        public EcoResCategoryChangeStatus ChangeStatus
        {
            get
            {
                return _changeStatus.GetValueOrDefault();
            }
            set
            {
                _changeStatus = value;
            }
        }
        public long Level { get; set; }
        public long AxRecId { get; set; }
        public long InstanceRelationType { get; set; }
    }
}
