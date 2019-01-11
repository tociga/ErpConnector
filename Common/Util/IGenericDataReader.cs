using System.Collections.Generic;
using System.Data;

namespace ErpConnector.Common.Util
{
    public interface IGenericDataReader<T> : IDataReader
    {
        bool HasRows();

        IEnumerable<T> GetGenericList();
    }
}
