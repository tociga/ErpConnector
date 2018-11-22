using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common;
using ErpConnector.Common.Util;
using ErpConnector.Common.ErpTasks;

namespace ErpConnector.Ax.Modules
{
    public class ItemCategoryTransfer
    {
        public static AxBaseException WriteCategories(int actionId)
        {
            var roles = ServiceConnector.CallOdataEndpoint<CategoryRoleDTO>("RetailEcoResCategoryHierarchyRole", "", "[ax].[ECORESCATEGORYHIERARCHYROLE]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            //if (roles != null)
            //{
            //    return roles;
            //}
            var hierarchy = ServiceConnector.CallOdataEndpoint<RetailEcoResCategoryHierarchy>("RetailEcoResCategoryHierarchy", "", "[ax].[ECORESCATEGORYHIERARCHY]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            //if (hierarchy != null)
            //{
            //    return hierarchy;
            //}

            var category = ServiceConnector.CallOdataEndpoint<RetailEcoResCategory>("RetailEcoResCategory", "", "[ax].[ECORESCATEGORY]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            //if (category != null)
            //{
            //    return category;
            //}

            var prodCat = ServiceConnector.CallOdataEndpoint<AGREcoResProductCategory>("AGREcoResProductCategories", "", "[ax].[ECORESPRODUCTCATEGORY]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            //if (prodCat != null)
            //{
            //    return prodCat;
            //}
            return null;
        }


    }
}
