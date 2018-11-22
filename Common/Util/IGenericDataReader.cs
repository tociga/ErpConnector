using System.Data;

namespace ErpConnector.Common.Util
{
    public interface IGenericDataReader : IDataReader
    {
        bool HasRows();
    }
}
