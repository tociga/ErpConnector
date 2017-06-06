using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.Modules
{
    public class ItemAttributeLookup
    {
        public static void ReadItemAttributes(Resources context, string autheader)
        {
            var colorGroups = AXServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductColorGroups", "", autheader).Result.value;
            DataWriter.WriteToTable<VariantGroupDTO>(colorGroups.GetDataReader(), "[ax].[ProductColorGroup]");

            var sizeGroups = AXServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductSizeGroups", "", autheader).Result.value;
            DataWriter.WriteToTable<VariantGroupDTO>(sizeGroups.GetDataReader(), "[ax].[ProductSizeGroup]");

            var styleGroups = AXServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductStyleGroups", "", autheader).Result.value;
            DataWriter.WriteToTable<VariantGroupDTO>(styleGroups.GetDataReader(), "[ax].[ProductStyleGroup]");

            var seasonGroups = AXServiceConnector.CallOdataEndpoint<VariantGroupDTO>("RetailSeasonGroups", "", autheader).Result.value;
            DataWriter.WriteToTable<VariantGroupDTO>(seasonGroups.GetDataReader(), "[ax].[SeasonGroup]");

            var season = ReadSeasonTable(context);
            DataWriter.WriteToTable(season, "[ax].[SeasonTable]");

            var colorGroupLines = ReadColorGroupLines(context);
            DataWriter.WriteToTable(colorGroupLines, "[ax].[ProductColorGroupLine]");

            var sizeGroupLines = ReadSizeGroupLines(context);
            DataWriter.WriteToTable(sizeGroupLines, "[ax].[ProductSizeGroupLine]");

            var styleGroupLines = ReadStyleGroupLines(context);
            DataWriter.WriteToTable(styleGroupLines, "[ax].[ProductStyleGroupLine]");
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
