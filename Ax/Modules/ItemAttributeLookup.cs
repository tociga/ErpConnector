using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using ErpConnector.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ErpConnector.Ax.Modules
{
    public class ItemAttributeLookup
    {
        public static AxBaseException ReadItemAttributes(bool includesFashion)
        {
            var colorGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductColorGroups", "", "[ax].[ProductColorGroup]").Result;
            if (colorGroups!= null)
            {
                return colorGroups;
            }
            var sizeGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductSizeGroups", "", "[ax].[ProductSizeGroup]").Result;
            if (sizeGroups != null)
            {
                return sizeGroups;
            }

            var styleGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductStyleGroups", "", "[ax].[ProductStyleGroup]").Result;
            if (styleGroups != null)
            {
                return styleGroups;
            }

            if (includesFashion)
            {
                var seasonGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("RetailSeasonGroups", "", "[ax].[SeasonGroup]").Result;
                if (seasonGroups != null)
                {
                    return seasonGroups;
                }

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
            var colorGroupLines = ServiceConnector.CallOdataEndpoint<ProductColorGroupLine>("ProductColorGroupLines", "", "[ax].[ProductColorGroupLine]").Result;
            if (colorGroupLines != null)
            {
                return colorGroupLines;
            }

            var sizeGroupLines = ServiceConnector.CallOdataEndpoint<ProductSizeGroupLine>("ProductSizeGroupLines", "", "[ax].[ProductSizeGroupLine]").Result;
            if (sizeGroupLines != null)
            {
                return sizeGroupLines;
            }

            var styleGroupLines = ServiceConnector.CallOdataEndpoint<ProductStyleGroupLine>("ProductStyleGroupLines", "", "[ax].[ProductStyleGroupLine]").Result;
            if (styleGroupLines != null)
            {
                return styleGroupLines;
            }
            var prodAttribute = ServiceConnector.CallOdataEndpoint<ProductAttribute>("ProductAttributes", "", "[ax].[ProductAttributes]").Result;
            if (prodAttribute != null)
            {
                return prodAttribute;
            }

            var attrValue = ServiceConnector.CallOdataEndpoint<ProductAttributeValue>("ProductAttributeValues", "", "[ax][ProductAttributeValues]").Result;
            if (attrValue != null)
            {
                return attrValue;
            }
            return null;
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
