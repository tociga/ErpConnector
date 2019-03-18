using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Ax.Authentication;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;

namespace ErpConnector.Ax.Modules
{
    public class UpdateReleasedProductVariants : AXODataContextConnector
    {
        public UpdateReleasedProductVariants(OAuthHelper oAuthenticationHelper, LogMessage logMessageHandler, bool enableCrossCompany) : base(oAuthenticationHelper, logMessageHandler, enableCrossCompany)
        {
        }

        protected override bool CreateRecords(string targetAXLegalEntity, System.Collections.ArrayList dataFile)
        {
            bool ret = false;

            foreach (ReleasedProductVariantDTO variant in dataFile)
            {
                if (!RecordExsits(variant.ItemNumber))
                {
                    //this.CreateMasterRecord(master);
                    ret = true;
                }
                else
                {
                    // <update> 
                    this.UpdateMasterRecord(variant);

                    ret = true;
                }
            }

            return ret;
        }

        private bool RecordExsits(string custID)
        {
            var query = from entity in this.context.ReleasedProductVariants
                        where entity.ItemNumber == custID
                        select entity;

            return query.Count() > 0;
        }

        private void UpdateMasterRecord(ReleasedProductVariantDTO variant)
        {
            // update customer
            ReleasedProductVariant m;

            var query = from entity in context.ReleasedProductVariants
                        where entity.ItemNumber == variant.ItemNumber
                        select entity;

            var ie = query.GetEnumerator();
            ie.MoveNext();

            m = ie.Current;

            // use tracned entity to only update change fields
            context.TrackEntityInstance(m);

            //m.RetailProductCategoryName = master.RetailProductCategoryName;
            //m.ProductLifeCycleStateId = variant.ProductLifeCycleStateId;

            logMessageHandler(string.Format("Update AGROrder '{0}'.", variant.ItemNumber));
        }

    }
}
