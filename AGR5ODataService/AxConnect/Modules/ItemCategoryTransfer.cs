using AxConnect.Microsoft.Dynamics.DataEntities;
using AxConCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.Modules
{
    public class ItemCategoryTransfer
    {
        public static void WriteCategories(Resources context)
        {
            var roles = ReadCategoryRole(context);
            DataAccess.DataWriter.WriteToTable(roles, "[ax].[ECORESCATEGORYHIERARCHYROLE]");

            var hierarchy = ReadHierarchy(context);
            DataAccess.DataWriter.WriteToTable(hierarchy, "[ax].[ECORESCATEGORYHIERARCHY]");

            var category = ReadCategory(context);
            DataAccess.DataWriter.WriteToTable(category, "[ax].[ECORESCATEGORY]");

            var prodCat = ReadProductCategory(context);
            DataAccess.DataWriter.WriteToTable(prodCat, "[ax].[ECORESPRODUCTCATEGORY]");
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
