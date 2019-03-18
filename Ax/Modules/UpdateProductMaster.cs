using ErpConnector.Ax.Authentication;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.Modules
{
    public class UpdateProductMaster : AXODataContextConnector
    {
        public UpdateProductMaster(OAuthHelper oAuthenticationHelper, LogMessage logMessageHandler, bool enableCrossCompany) : base(oAuthenticationHelper, logMessageHandler, enableCrossCompany)
        {
        }
        protected override bool CreateRcords(string targetAXLegalEntity, System.Collections.ArrayList dataFile)
        {
            bool ret = false;

            foreach (ProductMasterWriteDTO master in dataFile)
            {
                if (!RecordExsits(master.ProductNumber))
                {
                    //this.CreateMasterRecord(master);
                    ret = true;
                }
                else
                {
                    // <update> 
                    this.UpdateMasterRecord(master);

                    ret = true;
                }
            }

            return ret;
        }

        private bool RecordExsits(string custID)
        {
            var query = from entity in this.context.ProductMasters
                        where entity.ProductNumber == custID                         
                        select entity;

            return query.Count() > 0;
        }

        private void CreateMasterRecord(ProductMasterWriteDTO m)
        {
            ProductMaster master = context.CreateTrackedEntityInstance<ProductMaster>();
            master.ProductNumber = m.ProductNumber; // "Firsthh";
            master.ProductName = m.ProductName;
            master.ProductSearchName = m.ProductSearchName;
            master.ProductSizeGroupId = m.ProductSizeGroupId;
            master.ProductColorGroupId = m.ProductColorGroupId; // possible to use color_group_no                
            master.RetailProductCategoryName = m.RetailProductCategoryName;
            master.ProductDescription = m.ProductDescription;
            master.VariantConfigurationTechnology = m.VariantConfigurationTechnology;
            master.ProductDimensionGroupName = m.ProductDimensionGroupName;
            //RetailProductCategoryName = "";
            master.ProductType = m.ProductType;


            //foreach (AGROrderLineDTO agrOrderLine in argOrder.ArgOrderLine)
            //{
            //    AGROrderLine line = context.CreateTrackedEntityInstance<AGROrderLine>();
            //    line.AGRId = master.ARGId;
            //    line.Color = agrOrderLine.Color;
            //    line.Config = agrOrderLine.Config;
            //    line.ItemId = agrOrderLine.ItemId;
            //    line.LineNum = agrOrderLine.LineNum;
            //    line.Qty = agrOrderLine.Qty;
            //    line.Size = agrOrderLine.Size;
            //    line.Style = agrOrderLine.Style;
            //}

            logMessageHandler(string.Format("Created distinct Product Master '{0}'.", master.ProductNumber));
        }

        private void UpdateMasterRecord(ProductMasterWriteDTO master)
        {
            // update customer
            ReleasedProductMaster m;

            var query = from entity in context.ReleasedProductMasters
                        where entity.ProductNumber == master.ProductNumber
                        select entity;

            var ie = query.GetEnumerator();
            ie.MoveNext();

            m = ie.Current;

            // use tracned entity to only update change fields
            context.TrackEntityInstance(m);

            //m.RetailProductCategoryName = master.RetailProductCategoryName;
            //m.ProductLifeCycleStateId = master.ProductLifeCycleStateId;

            logMessageHandler(string.Format("Update AGROrder '{0}'.", master.ProductNumber));
        }


    }
}
