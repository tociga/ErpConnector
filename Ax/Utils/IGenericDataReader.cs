using System.Data;

namespace ErpConnector.Ax.Utils
{
    public interface IGenericDataReader : IDataReader
    {
        bool HasRows();
    }
}
