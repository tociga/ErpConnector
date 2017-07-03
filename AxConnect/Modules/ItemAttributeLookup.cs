﻿using AxConCommon.Extensions;
using ErpDTO.DTO;
using ErpDTO.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.Modules
{
    public class ItemAttributeLookup
    {
        public static void ReadItemAttributes(Resources context, bool includesFashion)
        {
            var colorGroups = AXServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductColorGroups", "").Result.value;
            DataAccess.DataWriter.WriteToTable<VariantGroupDTO>(colorGroups.GetDataReader(), "[ax].[ProductColorGroup]");

            var sizeGroups = AXServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductSizeGroups", "").Result.value;
            DataAccess.DataWriter.WriteToTable<VariantGroupDTO>(sizeGroups.GetDataReader(), "[ax].[ProductSizeGroup]");

            var styleGroups = AXServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductStyleGroups", "").Result.value;
            DataAccess.DataWriter.WriteToTable<VariantGroupDTO>(styleGroups.GetDataReader(), "[ax].[ProductStyleGroup]");

            if (includesFashion)
            {
                var seasonGroups = AXServiceConnector.CallOdataEndpoint<VariantGroupDTO>("RetailSeasonGroups", "").Result.value;
                DataAccess.DataWriter.WriteToTable<VariantGroupDTO>(seasonGroups.GetDataReader(), "[ax].[SeasonGroup]");

                var season = AXServiceConnector.CallOdataEndpoint<SeasonTable>("SeasonTables", "").Result.value;
                DataAccess.DataWriter.WriteToTable<SeasonTable>(season.GetDataReader(), "[ax].[SeasonTable]");
            }
            //var season = ReadSeasonTable(context);
            //DataAccess.DataWriter.WriteToTable(season, "[ax].[SeasonTable]");

            //var colorGroupLines = ReadColorGroupLines(context);
            //DataAccess.DataWriter.WriteToTable(colorGroupLines, "[ax].[ProductColorGroupLine]");

            //var sizeGroupLines = ReadSizeGroupLines(context);
            //DataAccess.DataWriter.WriteToTable(sizeGroupLines, "[ax].[ProductSizeGroupLine]");

            //var styleGroupLines = ReadStyleGroupLines(context);
            //DataAccess.DataWriter.WriteToTable(styleGroupLines, "[ax].[ProductStyleGroupLine]");


            var colorGroupLines = AXServiceConnector.CallOdataEndpoint<ProductColorGroupLine>("ProductColorGroupLines", "").Result.value;
            DataAccess.DataWriter.WriteToTable<ProductColorGroupLine>(colorGroupLines.GetDataReader(), "[ax].[ProductColorGroupLine]");

            var sizeGroupLines = AXServiceConnector.CallOdataEndpoint<ProductSizeGroupLine>("ProductSizeGroupLines", "").Result.value;
            DataAccess.DataWriter.WriteToTable<ProductSizeGroupLine>(sizeGroupLines.GetDataReader(), "[ax].[ProductSizeGroupLine]");

            var styleGroupLines = AXServiceConnector.CallOdataEndpoint<ProductStyleGroupLine>("ProductStyleGroupLines", "").Result.value;
            DataAccess.DataWriter.WriteToTable<ProductStyleGroupLine>(styleGroupLines.GetDataReader(), "[ax].[ProductStyleGroupLine]");

            var prodAttribute = AXServiceConnector.CallOdataEndpoint<ProductAttribute>("ProductAttributes", "").Result.value;
            DataAccess.DataWriter.WriteToTable<ProductAttribute>(prodAttribute.GetDataReader(), "[ax].[ProductAttributes]");

            var attrValue = AXServiceConnector.CallOdataEndpoint<ProductAttributeValue>("ProductAttributeValues", "").Result.value;
            DataAccess.DataWriter.WriteToTable<ProductAttributeValue>(attrValue.GetDataReader(), "[ax][ProductAttributeValues]");
            
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
            var seasons = context.SeasonTables.ToList();
            List<dynamic> list = new List<dynamic>();
            foreach (var season in seasons)
            {
                list.Add(
                    new
                    {
                        SEASONCODE = season.SeasonCode,
                        STARTDATE = season.StartDate.DateTime,
                        ENDDATE = season.EndDate.DateTime,
                        KRFRetailSeasonGroupId = season.KRFRetailSeasonGroupId,
                        Description = season.Description
                    });
            }
            return list.GetDataReader<dynamic>();
        }

    }
}
