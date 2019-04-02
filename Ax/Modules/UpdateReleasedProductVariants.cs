using System;
using System.Collections.Generic;
using System.Linq;
using ErpConnector.Ax.Authentication;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using ErpConnector.Common.Exceptions;
using Microsoft.OData.Client;
using Newtonsoft.Json;

namespace ErpConnector.Ax.Modules
{
    public class UpdateReleasedProductVariants<T> : AXODataContextConnector<T> where T : ReleasedProductVariant
    {
        public UpdateReleasedProductVariants(OAuthHelper oAuthenticationHelper, LogMessage logMessageHandler, bool enableCrossCompany) : base(oAuthenticationHelper, logMessageHandler, enableCrossCompany)
        {
        }

        protected override bool CreateRecords(string targetAXLegalEntity, List<T> dataFile)
        {
            bool ret = false;

            foreach (var v in dataFile)
            {
                if (!RecordExsits(v))
                {                    
                    ret = false;
                }
                else
                {
                    // <update> 
                    UpdateVariantRecord(v);
                    SaveChanges();
                    var variant = GetRecord(v);
                    if (variant != null)
                    {
                        DataWriter.UpdateProductVariantLifecycleState(variant.ProductMasterNumber, variant.ProductSizeId,
                        variant.ProductColorId, variant.ProductStyleId, variant.ProductConfigurationId, variant.ProductLifecycleStateId);
                    }
                    ret = false;
                }
            }

            return ret;
        }

        private ReleasedProductVariant GetRecord(ReleasedProductVariant v)
        {
            var query = from entity in this.context.ReleasedProductVariants
                        where entity.ProductMasterNumber == v.ProductMasterNumber
                            && entity.ProductSizeId == v.ProductSizeId
                            && entity.ProductColorId == v.ProductColorId
                            && entity.ProductStyleId == v.ProductStyleId
                            && entity.ProductConfigurationId == v.ProductConfigurationId
                        select entity;
            if (query.Count() == 0)
            {
                return null;
            }
            else if (query.Count() == 1)
            {
                return query.First();
            }
            else
            {
                throw new Exception("The input parameters did return multiple variants, should only return one.");
            }
            
        }
        private bool RecordExsits(ReleasedProductVariant v)
        {
            var entity = GetRecord(v);
            return entity != null;
        }

        private void UpdateVariantRecord(ReleasedProductVariant v)
        {
            // update customer
            ReleasedProductVariant m;

            var query = from entity in context.ReleasedProductVariants
                        where entity.ProductMasterNumber == v.ProductMasterNumber && entity.ProductSizeId == v.ProductSizeId
                            && entity.ProductColorId == v.ProductColorId && entity.ProductStyleId == v.ProductStyleId
                            && entity.ProductConfigurationId == v.ProductConfigurationId
                        select entity;

            var ie = query.GetEnumerator();
            ie.MoveNext();

            m = ie.Current;

            // use tracned entity to only update change fields
            context.TrackEntityInstance(m);

            //m.RetailProductCategoryName = master.RetailProductCategoryName;
            m.ProductLifecycleStateId = v.ProductLifecycleStateId;

            logMessageHandler(string.Format("Update Variant '{0}', to plc state = {1}", m.ProductVariantNumber, m.ProductLifecycleStateId));
        }

        protected override AxBaseException SaveChanges()
        {
            try
            {
                var result = context.SaveChanges(SaveChangesOptions.PostOnlySetProperties | SaveChangesOptions.BatchWithSingleChangeset);                

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

    }
}
