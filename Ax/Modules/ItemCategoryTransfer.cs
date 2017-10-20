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
        public static AxBaseException WriteCategories()
        {
            var roles = ServiceConnector.CallOdataEndpoint<CategoryRoleDTO>("RetailEcoResCategoryHierarchyRole", "", "[ax].[ECORESCATEGORYHIERARCHYROLE]").Result;
            if (roles != null)
            {
                return roles;
            }
            var hierarchy = ServiceConnector.CallOdataEndpoint<RetailEcoResCategoryHierarchy>("RetailEcoResCategoryHierarchy", "", "[ax].[ECORESCATEGORYHIERARCHY]").Result;
            if (hierarchy != null)
            {
                return hierarchy;
            }

            var category = ServiceConnector.CallOdataEndpoint<RetailEcoResCategory>("RetailEcoResCategory", "", "[ax].[ECORESCATEGORY]").Result;
            if (category != null)
            {
                return category;
            }

            var prodCat = ServiceConnector.CallOdataEndpoint<AGREcoResProductCategory>("AGREcoResProductCategories", "", "[ax].[ECORESPRODUCTCATEGORY]").Result;
            if (prodCat != null)
            {
                return prodCat;
            }
            return null;
        }


    }
}
