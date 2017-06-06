using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class BaseComDTO
    {
        public BaseComDTO ()
        {
            ErrorMessages = new List<string>();
        }
        public List<string> ErrorMessages { get; set; }
    }
}
