using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax
{
    public interface IGenericDataReader : IDataReader
    {
        bool HasRows();
    }
}
