using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.Exceptions
{
    public class AxBaseException
    {
        public AxWebExceptions error { get; set; }

        public Exception ApplicationException { get; set; }
<<<<<<< HEAD

=======
>>>>>>> erp_listener_ax_lss
        public string ErrorMessage
        {
            get
            {
                if (ApplicationException != null)
                {
                    return ApplicationException.Message;
                }
                else if (error != null && error.innererror != null)
                {
                    return error.innererror.message;
                }
                return "";
            }
        }
<<<<<<< HEAD

=======
>>>>>>> erp_listener_ax_lss
        public string StackTrace
        {
            get
            {
                if (ApplicationException != null)
                {
                    return ApplicationException.StackTrace;
                }
                else if (error != null && error.innererror != null)
                {
                    return error.innererror.stacktrace;
                }
                return "";
            }
        }
    }
}
