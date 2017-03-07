using AxConCommon.Extensions;
using AxConnect.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.Modules
{
    public class ItemAttributeLookup
    {
        public static void ReadItemAttributes(Resources context)
        {
            var season = ReadSeasonTable(context);
            DataAccess.DataWriter.WriteToTable(season, "[ax].[SeasonTable]");

            var inventSeason = ReadInventSeasonTable(context);
            DataAccess.DataWriter.WriteToTable(inventSeason, "[ax].[InventSeasonTable]");

            var colorGroups = ReadColorGroupLines(context);
            DataAccess.DataWriter.WriteToTable(colorGroups, "[ax].[ProductColorGroupLine]");

            var sizeGroups = ReadSizeGroupLines(context);
            DataAccess.DataWriter.WriteToTable(sizeGroups, "[ax].[ProductSizeGroupLine]");

            var styleGroups = ReadStyleGroupLines(context);
            DataAccess.DataWriter.WriteToTable(styleGroups, "[ax].[ProductStyleGroupLine]");
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

        private static IGenericDataReader ReadInventSeasonTable(Resources context)
        {
            var inventSeasons = context.InventSeasonTables.ToList();
            List<dynamic> list = new List<dynamic>();
            foreach(var i in inventSeasons)
            {
                list.Add(
                    new
                    {
                        ItemId = i.ItemId,
                        SeasonCode = i.SeasonCode,
                        IsDefault = i.IsDefault
                    });
            }
            return list.GetDataReader<dynamic>();
        }


    }
}
