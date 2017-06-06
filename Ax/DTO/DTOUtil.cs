using System;

namespace ErpConnector.Ax.DTO
{
    public class DTOUtil
    {        
        public static T GetEnumFromObj<T>(object obj, T defaultValue) where T : struct, IConvertible
        {
            try
            {
                T enumType = (T)Enum.Parse(typeof(T), obj.ToString());
                return enumType;
            }
            catch(Exception)
            {
                return defaultValue;
            }            
        }
    }
}
