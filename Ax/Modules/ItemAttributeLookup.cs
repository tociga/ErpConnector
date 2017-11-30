﻿using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using ErpConnector.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ErpConnector.Ax.Modules
{
    public class ItemAttributeLookup
    {
        public static AxBaseException ReadItemAttributes(bool includesFashion, int actionId)
        {
            var colorGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductColorGroups", "", "[ax].[ProductColorGroup]", actionId).Result;
            if (colorGroups!= null)
            {
                return colorGroups;
            }
            var sizeGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductSizeGroups", "", "[ax].[ProductSizeGroup]", actionId).Result;
            if (sizeGroups != null)
            {
                return sizeGroups;
            }

            var styleGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductStyleGroups", "", "[ax].[ProductStyleGroup]", actionId).Result;
            if (styleGroups != null)
            {
                return styleGroups;
            }

            if (includesFashion)
            {
                var seasonGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("RetailSeasonGroups", "", "[ax].[SeasonGroup]", actionId).Result;
                if (seasonGroups != null)
                {
                    return seasonGroups;
                }

                var season = ServiceConnector.CallOdataEndpoint<SeasonTable>("SeasonTables", "", "[ax].[SeasonTable]", actionId).Result;
                if (season != null)
                {
                    return season;
                }
                
            }
            var colorGroupLines = ServiceConnector.CallOdataEndpoint<ProductColorGroupLine>("ProductColorGroupLines", "", "[ax].[ProductColorGroupLine]", actionId).Result;
            if (colorGroupLines != null)
            {
                return colorGroupLines;
            }

            var sizeGroupLines = ServiceConnector.CallOdataEndpoint<ProductSizeGroupLine>("ProductSizeGroupLines", "", "[ax].[ProductSizeGroupLine]", actionId).Result;
            if (sizeGroupLines != null)
            {
                return sizeGroupLines;
            }

            var styleGroupLines = ServiceConnector.CallOdataEndpoint<ProductStyleGroupLine>("ProductStyleGroupLines", "", "[ax].[ProductStyleGroupLine]", actionId).Result;
            if (styleGroupLines != null)
            {
                return styleGroupLines;
            }

            var color = ServiceConnector.CallOdataEndpoint<ProductColor>("ProductColors", "", "[ax].[ProductColor]", actionId).Result;
            if (color != null)
            {
                return color;
            }

            var size = ServiceConnector.CallOdataEndpoint<ProductSize>("ProductSizes", "", "[ax].[ProductSize]", actionId).Result;
            if (size != null)
            {
                return size;
            }

            var style = ServiceConnector.CallOdataEndpoint<ProductStyle>("ProductStyles", "", "[ax].[ProductStyle]", actionId).Result;
            if (style != null)
            {
                return style;
            }
            var prodAttribute = ServiceConnector.CallOdataEndpoint<ProductAttribute>("ProductAttributes", "", "[ax].[ProductAttributes]", actionId).Result;
            if (prodAttribute != null)
            {
                return prodAttribute;
            }

            var attrValue = ServiceConnector.CallOdataEndpoint<ProductAttributeValue>("ProductAttributeValues", "", "[ax].[ProductAttributeValues]", actionId).Result;
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
