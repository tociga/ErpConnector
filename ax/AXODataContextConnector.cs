using ErpConnector.Ax.Authentication;
using Microsoft.OData.Client;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.Xml;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Common.Exceptions;
using System.Collections.Generic;

namespace ErpConnector.Ax
{
    public delegate void LogMessage(string msg, ConsoleColor color = ConsoleColor.Green, bool leadingLine = true);

    public abstract class AXODataContextConnector<T>
    {
        protected LogMessage logMessageHandler;

        protected AXODataContext context;

        private OAuthHelper oAuthHelper;
        public AXODataContextConnector(OAuthHelper oAuthenticationHelper, LogMessage logMessageHandler, bool enableCrossCompany)
        {
            this.oAuthHelper = oAuthenticationHelper;

            this.logMessageHandler = logMessageHandler;

            this.context = new AXODataContext(oAuthHelper, enableCrossCompany);
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

        protected virtual AxBaseException SaveChanges()
        {
            try
            {
                context.SaveChanges(SaveChangesOptions.PostOnlySetProperties | SaveChangesOptions.BatchWithSingleChangeset);

                return null;
            }
            catch (DataServiceRequestException ex)
            {
                return JsonConvert.DeserializeObject<AxBaseException>(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                return new AxBaseException { ApplicationException = ex };
            }
        }

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
