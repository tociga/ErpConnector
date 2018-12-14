using ErpConnector.Common.ErpTasks;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.Util;
using System;
using System.Diagnostics;

namespace ErpConnector.Common.ExternalTask
{
    public class ExternalTaskExec
    {
        public static AxBaseException ExecTask(ErpTaskStep taskStep)
        {
            Process pr = new Process();
            AxBaseException result = null;
            try
            {
                
                pr.StartInfo.FileName = taskStep.ExternalProcess;
                pr.StartInfo.Arguments = taskStep.ExternalProcessArgument ?? "";
                pr.StartInfo.UseShellExecute = false;
                pr.StartInfo.RedirectStandardError = true;
                pr.StartInfo.RedirectStandardOutput = true;
                //pr.StartInfo.Verb = "runas";                
                bool success = pr.Start();
                pr.WaitForExit();
                string standardError = pr.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(standardError))
                {
                    result = new AxBaseException { ApplicationException = new ApplicationException(standardError) };
                }
                return result;
            }
            catch (Exception e)
            {
                return new AxBaseException { ApplicationException = e };
            }
        }
    }
}
