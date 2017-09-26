using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using System.Collections.Generic;
using System.Linq;

namespace ErpConnector.Ax.Modules
{
    public class ItemAttributeLookup
    {
        public static void ReadItemAttributes(Resources context, bool includesFashion)
        {
            var colorGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductColorGroups", "").Result.value;
            DataWriter.WriteToTable<VariantGroupDTO>(colorGroups.GetDataReader(), "[ax].[ProductColorGroup]");

            var sizeGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductSizeGroups", "").Result.value;
            DataWriter.WriteToTable<VariantGroupDTO>(sizeGroups.GetDataReader(), "[ax].[ProductSizeGroup]");

            var styleGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductStyleGroups", "").Result.value;
            DataWriter.WriteToTable<VariantGroupDTO>(styleGroups.GetDataReader(), "[ax].[ProductStyleGroup]");

            if (includesFashion)
            {
                var seasonGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("RetailSeasonGroups", "").Result.value;
                DataWriter.WriteToTable<VariantGroupDTO>(seasonGroups.GetDataReader(), "[ax].[SeasonGroup]");

                //var season = ServiceConnector.CallOdataEndpoint<SeasonTable>("SeasonTables", "").Result.value;
                //DataWriter.WriteToTable<SeasonTable>(season.GetDataReader(), "[ax].[SeasonTable]");
            }
            //var season = ReadSeasonTable(context);
            //DataWriter.WriteToTable(season, "[ax].[SeasonTable]");

            //var colorGroupLines = ReadColorGroupLines(context);
            //DataWriter.WriteToTable(colorGroupLines, "[ax].[ProductColorGroupLine]");

            //var sizeGroupLines = ReadSizeGroupLines(context);
            //DataWriter.WriteToTable(sizeGroupLines, "[ax].[ProductSizeGroupLine]");

            //var styleGroupLines = ReadStyleGroupLines(context);
            //DataWriter.WriteToTable(styleGroupLines, "[ax].[ProductStyleGroupLine]");
            var colorGroupLines = ServiceConnector.CallOdataEndpoint<ProductColorGroupLine>("ProductColorGroupLines", "").Result.value;
            DataWriter.WriteToTable<ProductColorGroupLine>(colorGroupLines.GetDataReader(), "[ax].[ProductColorGroupLine]");
            var sizeGroupLines = ServiceConnector.CallOdataEndpoint<ProductSizeGroupLine>("ProductSizeGroupLines", "").Result.value;
            DataWriter.WriteToTable<ProductSizeGroupLine>(sizeGroupLines.GetDataReader(), "[ax].[ProductSizeGroupLine]");
            var styleGroupLines = ServiceConnector.CallOdataEndpoint<ProductStyleGroupLine>("ProductStyleGroupLines", "").Result.value;
            DataWriter.WriteToTable<ProductStyleGroupLine>(styleGroupLines.GetDataReader(), "[ax].[ProductStyleGroupLine]");
            var prodAttribute = ServiceConnector.CallOdataEndpoint<ProductAttribute>("ProductAttributes", "").Result.value;
            DataWriter.WriteToTable<ProductAttribute>(prodAttribute.GetDataReader(), "[ax].[ProductAttributes]");
            var attrValue = ServiceConnector.CallOdataEndpoint<ProductAttributeValue>("ProductAttributeValues", "").Result.value;
            DataWriter.WriteToTable<ProductAttributeValue>(attrValue.GetDataReader(), "[ax][ProductAttributeValues]");
        }

        private static IGenericDataReader ReadColorGroupLines(Resources context)
        {
            var colorGroupLine = context.ProductColorGroupLines.ToList();
            List<dynamic> list = new List<dynamic>();
            foreach(var c in colorGroupLine)
            {
                list.Add(
                    new
                    {
                        ProductColorGroupID = c.ProductColorGroupId,
                        ProductColorId = c.ProductColorId,
                        ColorName = c.ColorName,
                        ColorDescription = c.ColorDescription,
                        DisplayOrder = c.DisplayOrder,
                        BarcodeNumber = c.BarcodeNumber,
                        ReplenishmentWeight = c.ReplenishmentWeight
                    }
                );
            }
            return list.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadStyleGroupLines(Resources context)
        {
            var colorGroupLine = context.ProductStyleGroupLines.ToList();
            List<dynamic> list = new List<dynamic>();
            foreach (var c in colorGroupLine)
            {
                list.Add(
                    new
                    {
                        ProductColorGroupID = c.ProductStyleGroupId,
                        ProductColorId = c.ProductStyleId,
                        ColorName = c.StyleName,
                        ColorDescription = c.StyleDescription,
                        DisplayOrder = c.DisplayOrder,
                        BarcodeNumber = c.BarcodeNumber,
                        ReplenishmentWeight = c.ReplenishmentWeight
                    }
                );
            }
            return list.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadSizeGroupLines(Resources context)
        {
            var colorGroupLine = context.ProductSizeGroupLines.ToList();
            List<dynamic> list = new List<dynamic>();
            foreach (var c in colorGroupLine)
            {
                list.Add(
                    new
                    {
                        ProductColorGroupID = c.ProductSizeGroupId,
                        ProductColorId = c.ProductSizeId,
                        ColorName = c.SizeName,
                        ColorDescription = c.SizeDescription,
                        DisplayOrder = c.DisplayOrder,
                        BarcodeNumber = c.BarcodeNumber,
                        ReplenishmentWeight = c.ReplenishmentWeight
                    }
                );
            }
            return list.GetDataReader<dynamic>();
        }
        private static IGenericDataReader ReadSeasonTable(Resources context)
        {
            //var seasons = context.SeasonTables.ToList();
            List<dynamic> list = new List<dynamic>();
            //foreach (var season in seasons)
            //{
            //    list.Add(
            //        new
            //        {
            //            SEASONCODE = season.SeasonCode,
            //            STARTDATE = season.StartDate.DateTime,
            //            ENDDATE = season.EndDate.DateTime,
            //            KRFRetailSeasonGroupId = season.KRFRetailSeasonGroupId,
            //            Description = season.Description
            //        });
            //}
            return list.GetDataReader<dynamic>();
        }

    }
}
