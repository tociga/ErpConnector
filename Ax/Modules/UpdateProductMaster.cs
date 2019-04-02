using ErpConnector.Ax.Authentication;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.Modules
{
    public class UpdateProductMaster<T> : AXODataContextConnector<T> where T : ReleasedProductMaster
    {
        public UpdateProductMaster(OAuthHelper oAuthenticationHelper, LogMessage logMessageHandler, bool enableCrossCompany) : base(oAuthenticationHelper, logMessageHandler, enableCrossCompany)
        {
        }
        protected override bool CreateRecords(string targetAXLegalEntity, List<T> dataFile)
        {
            bool ret = false;

            foreach (var master in dataFile)
            {
                if (!RecordExsits(master.ProductNumber))
                {
                    //this.CreateMasterRecord(master);
                    ret = false;
                }
                else
                {
                    // <update> 
                    UpdateMasterRecord(master);
                    SaveChanges();
                    var m = GetRecord(master.ProductNumber);
                    DataWriter.UpdateProductMasterLifecycleState(m.ProductNumber, m.ProductLifecycleStateId);
                    ret = false;
                }
            }

            return ret;
        }
        private ReleasedProductMaster GetRecord(string productMasterNo)
        {
            var query = from entity in this.context.ReleasedProductMasters
                        where entity.ProductNumber == productMasterNo
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
                throw new Exception("The input parameters did return multiple product masters, should only return one.");
            }

        }

        private bool RecordExsits(string custID)
        {
            var entity = GetRecord(custID);
            return entity != null;
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

        private void UpdateMasterRecord(ReleasedProductMaster master)
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
            m.ProductLifecycleStateId = master.ProductLifecycleStateId;

            logMessageHandler(string.Format("Update ProductMaster '{0}', PLC state = {1}", master.ProductNumber, master.ProductLifecycleStateId));
        }


    }
}
