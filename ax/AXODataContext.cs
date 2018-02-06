using System;
using System.Collections.Generic;
using Microsoft.OData.Client;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Authentication;

namespace ErpConnector.Ax
{
    public class AXODataContext : Resources
    {
        private const string httpPost = "POST";
        private OAuthHelper OAuthHelper;
        /// <summary>
        /// Constructs an OData context object.
        /// </summary>
        public AXODataContext(OAuthHelper oAuthHelper, bool enableCrossCompany) : base(new Uri(oAuthHelper.UriString, UriKind.Absolute))
        {
            this.OAuthHelper = oAuthHelper;

            base.Timeout = 300; // in seconds - 5 minutes default

            this.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(AuthenticateRequest);

            this.BuildingRequest += new EventHandler<BuildingRequestEventArgs>(SetCrossCompany);

            this.EnableCrossCompany = enableCrossCompany;
        }

        public bool EnableCrossCompany { get; set; }

        public T CreateTrackedEntityInstance<T>()
            where T : class, new()
        {
            T entityInstance = new T();

            TrackEntityInstance<T>(entityInstance);

            return entityInstance;
        }

        public void TrackEntityInstance<T>(T entityInstance)
            where T : class
        {
            DataServiceCollection<T> dataServiceCollection = new DataServiceCollection<T>(this);
            dataServiceCollection.Add(entityInstance);
        }

        /// <summary>
        /// Executes an AX OData action for the given entity instance. Use when the action does not return a value.
        /// </summary>
        /// <typeparam name="EntityType">The type of the entity instance the action is invoked on.</typeparam>
        /// <param name="targetEntity">The entity instance to invoke the action on. This must be fully populated (i.e. has been read or has been written).</param>
        /// <param name="actionName">The name of the action to invoke.</param>
        /// <param name="actionParameters">The array of parameters which will be forwarded to the action. Each parameter should be a key (parameter name) / value (parameter value) pair.</param>
        public void ExecuteEntityBoundAction<EntityType>(EntityType targetEntity, string actionName, params KeyValuePair<string, object>[] actionParameters)
            where EntityType : BaseEntityType
        {
            // Get the URI for invoking the action:
            Uri actionUri = GetEntityActionUri<EntityType>(targetEntity, actionName);

            // Execute the action:
            this.Execute(actionUri, httpPost, GetActionParameters(actionParameters));
        }

        private Uri GetEntityActionUri<EntityType>(EntityType targetEntity, string actionName)
            where EntityType : BaseEntityType
        {
            // Build up the target URL for the http request:
            string[] targetEntityIdentitySegments = this.GetEntityDescriptor(targetEntity).Identity.Segments;
            string targetEntityUrl = targetEntityIdentitySegments[targetEntityIdentitySegments.Length - 1];
            string actionUrl = string.Format("/{0}/{1}.{2}", targetEntityUrl, targetEntity.GetType().Namespace, actionName);

            return new Uri(actionUrl, UriKind.Relative);
        }

        private BodyOperationParameter[] GetActionParameters(params KeyValuePair<string, object>[] args)
        {
            List<BodyOperationParameter> bodyOperationParameters = new List<BodyOperationParameter>(args.Length);

            foreach (KeyValuePair<string, object> arg in args)
            {
                bodyOperationParameters.Add(new BodyOperationParameter(arg.Key, arg.Value));
            }

            return bodyOperationParameters.ToArray();
        }

        private void AuthenticateRequest(object sender, SendingRequest2EventArgs e)
        {
            e.RequestMessage.SetHeader(
            OAuthHelper.OAuthHeader,
            this.OAuthHelper.GetAuthenticationHeader());
        }

        private void SetCrossCompany(object sender, BuildingRequestEventArgs e)
        {
            if (this.EnableCrossCompany)
            {
                string queryPartPrefix = string.IsNullOrEmpty(e.RequestUri.Query) ? "?" : "&";

                e.RequestUri = new Uri(string.Format("{0}{1}{2}",
                e.RequestUri.AbsoluteUri, queryPartPrefix, "cross-company=true"));
            }
        }
    }
}
