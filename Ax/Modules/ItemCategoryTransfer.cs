using System.Collections.Generic;
using System.Data;
using System.Linq;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using ErpConnector.Common.Exceptions;

namespace ErpConnector.Ax.Modules
{
    public class ItemCategoryTransfer
    {
        public static AxBaseException WriteCategories(int actionId)
        {
            var roles = ServiceConnector.CallOdataEndpoint<CategoryRoleDTO>("RetailEcoResCategoryHierarchyRole", "", "[ax].[ECORESCATEGORYHIERARCHYROLE]", actionId).Result;
            if (roles != null)
            {
                return roles;
            }
            var hierarchy = ServiceConnector.CallOdataEndpoint<RetailEcoResCategoryHierarchy>("RetailEcoResCategoryHierarchy", "", "[ax].[ECORESCATEGORYHIERARCHY]", actionId).Result;
            if (hierarchy != null)
            {
                return hierarchy;
            }

            var category = ServiceConnector.CallOdataEndpoint<RetailEcoResCategory>("RetailEcoResCategory", "", "[ax].[ECORESCATEGORY]", actionId).Result;
            if (category != null)
            {
                return category;
            }

            var prodCat = ServiceConnector.CallOdataEndpoint<AGREcoResProductCategory>("AGREcoResProductCategories", "", "[ax].[ECORESPRODUCTCATEGORY]", actionId).Result;
            if (prodCat != null)
            {
                return prodCat;
            }
            return null;
        }


    }
}
