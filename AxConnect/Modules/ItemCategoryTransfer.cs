using AxConCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpDTO.DTO;
using ErpDTO.Microsoft.Dynamics.DataEntities;

namespace AxConnect.Modules
{
    public class ItemCategoryTransfer
    {
        public static void WriteCategories()
        {
            var roles = AXServiceConnector.CallOdataEndpoint<CategoryRoleDTO>("RetailEcoResCategoryHierarchyRole", "").Result.value;
            DataAccess.DataWriter.WriteToTable(roles.GetDataReader(), "[ax].[ECORESCATEGORYHIERARCHYROLE]");

            var hierarchy = AXServiceConnector.CallOdataEndpoint<RetailEcoResCategoryHierarchy>("RetailEcoResCategoryHierarchy", "").Result.value;
            DataAccess.DataWriter.WriteToTable<RetailEcoResCategoryHierarchy>(hierarchy.GetDataReader(), "[ax].[ECORESCATEGORYHIERARCHY]");

            var category = AXServiceConnector.CallOdataEndpoint<RetailEcoResCategory>("RetailEcoResCategory", "").Result.value;
            DataAccess.DataWriter.WriteToTable<RetailEcoResCategory>(category.GetDataReader(), "[ax].[ECORESCATEGORY]");

            var prodCat = AXServiceConnector.CallOdataEndpoint<AGREcoResProductCategory>("AGREcoResProductCategories", "").Result.value;
            DataAccess.DataWriter.WriteToTable< AGREcoResProductCategory>(prodCat.GetDataReader(), "[ax].[ECORESPRODUCTCATEGORY]");
        }

        private static IGenericDataReader ReadHierarchy(Resources context)
        {
            var cat = from c in context.RetailEcoResCategoryHierarchy select c;            
            List<dynamic> list = new List<dynamic>();
            foreach (var category in cat)
            {
                list.Add(new
                {
                    RecVersion = category.AxRecId,
                    Name = category.Name,
                    HierarchyModifier = (int)category.HierarchyModifier.GetValueOrDefault()
                });
            }
            return list.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadCategoryRole(Resources context)
        {
            var catRoles = from cr in context.RetailEcoResCategoryHierarchyRole select cr;
            List<dynamic> list = new List<dynamic>();
            foreach (var role in catRoles)
            {
                list.Add(new
                {
                    CategryHierarchy = role.CategoryHierarchy,
                    NamedCategoryHierarchyRole = role.NamedCategoryHierarchyRoleAsInt
                });
            }
            return list.GetDataReader<dynamic>();
        }
        private static IGenericDataReader ReadCategory(Resources context)
        {
            var categories = from c in context.RetailEcoResCategory select c;
            List<dynamic> list = new List<dynamic>();            
            foreach (var category in categories)
            {
                list.Add(new
                {
                    CATEGORYHIERARCHY = category.CategoryHierarchy,
                    PARENTCATEGORY = category.ParentCategory,
                    NAME = category.Name,
                    CODE = category.Code,
                    ISACTIVE = category.IsActive.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
                    CHANGESTATUS = (int)category.ChangeStatus.GetValueOrDefault(),
                    LEVEL_ = category.Level,
                    RECVERSION = category.AxRecId,
                    RELATIONTYPE = category.InstanceRelationType,
                });
            }
            return list.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadProductCategory(Resources context)
        {
            var prodCat = context.AGREcoResProductCategories;
            var list = new List<dynamic>();
            foreach (var pc in prodCat)
            {
                list.Add(new
                {
                    CATEGORYHIERARCHY = pc.CategoryHierarchy,
                    CATEGROY = pc.Category,
                    PRODUCT = pc.Product,
                    MODIFIEDDATETIME = pc.ModifiedDateAndTime.DateTime
                });
            }

            return list.GetDataReader<dynamic>();
        }

    }
}
