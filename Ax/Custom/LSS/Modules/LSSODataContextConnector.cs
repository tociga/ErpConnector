using ErpConnector.Ax.Authentication;
using Microsoft.OData.Client;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.Xml;
using ErpConnector.Common.Exceptions;
using System.Collections.Generic;

namespace ErpConnector.Ax.Custom.LSS.Modules
{
    public delegate void LogMessage(string msg, ConsoleColor color = ConsoleColor.Green, bool leadingLine = true);

    public abstract class LSSODataContextConnector<T> 
    {
        protected LogMessage logMessageHandler;

        protected LSSODataContext context;

        private OAuthHelper oAuthHelper;
        public LSSODataContextConnector(OAuthHelper oAuthenticationHelper, LogMessage logMessageHandler, bool enableCrossCompany)
        {
            this.oAuthHelper = oAuthenticationHelper;

            this.logMessageHandler = logMessageHandler;

            this.context = new LSSODataContext(oAuthHelper, enableCrossCompany);
        }

        public AxBaseException CreateRecordInAX(string targetAXLegalEntity, List<T> dataFile)
        {
            if (dataFile.Count > 0)
            {
                bool recordCreated = this.CreateRecords(targetAXLegalEntity, dataFile);

                // Save everything as a single transaction (can be otherwise if required):
                if (recordCreated)
                {
                    return SaveChanges();
                }
            }

            return null;
        }

        protected abstract bool CreateRecords(string targetAXLegalEntity, List<T> dataFile);

        protected abstract AxBaseException SaveChanges();

        private void logException(Exception ex)
        {
            Exception exToLog = ex;

            while (exToLog.InnerException != null)
                exToLog = exToLog.InnerException;

            logMessageHandler(
                string.Format("Exception: {0}", exToLog.Message),
                ConsoleColor.Red);
        }
    }
}
