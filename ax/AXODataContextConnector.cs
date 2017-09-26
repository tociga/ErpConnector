using ErpConnector.Ax.Authentication;
using Microsoft.OData.Client;
using System;
using System.Linq;
using System.Xml;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;

namespace ErpConnector.Ax
{
    public delegate void LogMessage(string msg, ConsoleColor color = ConsoleColor.Green, bool leadingLine = true);

    public abstract class AXODataContextConnector
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

        public bool CreateRecordInAX(string targetAXLegalEntity, System.Collections.ArrayList dataFile)
        {
            if (dataFile.Count > 0)
            {
                bool recordCreated = this.CreateRcords(targetAXLegalEntity, dataFile);

                // Save everything as a single transaction (can be otherwise if required):
                if (recordCreated && !SaveChanges()) return false;
            }


            return true;
        }

        protected abstract bool CreateRcords(string targetAXLegalEntity, System.Collections.ArrayList dataFile);

        private bool SaveChanges()
        {
            try
            {
                context.SaveChanges(SaveChangesOptions.PostOnlySetProperties | SaveChangesOptions.BatchWithSingleChangeset);

                return true;
            }
            catch (DataServiceRequestException ex)
            {
                logMessageHandler(
                    string.Format("Error occured while saving: {0}", ex.InnerException.Message),
                    ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                logException(ex);
            }

            return false;
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
