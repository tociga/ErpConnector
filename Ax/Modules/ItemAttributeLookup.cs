using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common;
using ErpConnector.Common.Util;
using ErpConnector.Common.ErpTasks;

namespace ErpConnector.Ax.Modules
{
    public class ItemAttributeLookup
    {
        public static AxBaseException UpdateProductAttributes(int actionId)
        {
            var ecoResValue = ServiceConnector.CallService<AGREcoResValueDTO>(actionId, "GetValue", "AGRAttributeService", "[ax].[ECORESVALUE]", 10000, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
            if (ecoResValue != null)
            {
                return ecoResValue;
            }
            var ecoResAttribute = ServiceConnector.CallService<AGREcoResAttributeDTO>(actionId, "GetAttribute", "AGRAttributeService", "[ax].[ECORESATTRIBUTE]", 10000, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
            if (ecoResAttribute != null)
            {
                return ecoResAttribute;
            }
            var ecoResAttributeValue = ServiceConnector.CallService<AGREcoResAttributeValueDTO>(actionId, "GetAttributeValue", "AGRAttributeService", "[ax].[ECORESATTRIBUTEVALUE]", 10000, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
            if (ecoResAttributeValue != null)
            {
                return ecoResAttributeValue;
            }
            var ecoResAttributeType = ServiceConnector.CallService<AGREcoResAttributeTypeDTO>(actionId, "GetAttributeType", "AGRAttributeService", "[ax].[ECORESATTRIBUTEType]", 10000, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
            if (ecoResAttributeType != null)
            {
                return ecoResAttributeType;
            }
            var ecoResEnum = ServiceConnector.CallService<AGREcoResEnumerationAttributeValueDTO>(actionId,
                "GetEnumerationAttributeValue", "AGRAttributeService", "[ax].[ECORESENUMERATIONATTRIBUTETYPEVALUE]", 10000, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
            if (ecoResEnum != null)
            {
                return ecoResEnum;
            }
            var ecoResCatAttr = ServiceConnector.CallService<AGREcoResCategoryAttributeDTO>(actionId,
                 "GetCategoryAttribute", "AGRAttributeService", "[ax].[ECORESCATEGORYATTRIBUTE]", 10000, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
            if (ecoResCatAttr != null)
            {
                return ecoResCatAttr;
            }
            var ecoResProdInstance = ServiceConnector.CallService<AGREcoResProductInstanceDTO>(actionId,
                 "GetProductInstanceValue", "AGRAttributeService", "[ax].[ECORESPRODUCTINSTANCEVALUE]", 10000, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
            if (ecoResProdInstance != null)
            {
                return ecoResProdInstance;
            }

            //var prodAttributes = ServiceConnector.CallOdataEndpoint<ProductAttribute>("ProductAttributes", null, "[ax].[ProductAttributes]", actionId);
            //var prodAttributeValues = ServiceConnector.CallOdataEndpoint<ProductAttributeValue>("ProductAttributeValues", null, "[ax].[ProductAttributeValues]", actionId);
            return null;
        }
        public static AxBaseException ReadItemAttributes(bool includesFashion, bool includeBandM, int actionId)
        {
            var colorGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductColorGroups", "", "[ax].[ProductColorGroup]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            if (colorGroups!= null)
            {
                return colorGroups;
            }
            var sizeGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductSizeGroups", "", "[ax].[ProductSizeGroup]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            if (sizeGroups != null)
            {
                return sizeGroups;
            }

            var styleGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("ProductStyleGroups", "", "[ax].[ProductStyleGroup]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            if (styleGroups != null)
            {
                return styleGroups;
            }

            if (includesFashion)
            {
                var seasonGroups = ServiceConnector.CallOdataEndpoint<VariantGroupDTO>("RetailSeasonGroups", "", "[ax].[SeasonGroup]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
                if (seasonGroups != null)
                {
                    return seasonGroups;
                }

                //var season = ServiceConnector.CallOdataEndpoint<SeasonTable>("SeasonTables", "", "[ax].[SeasonTable]", actionId).Result;
                //if (season != null)
                //{
                //    return season;
                //}
                
            }
            var colorGroupLines = ServiceConnector.CallOdataEndpoint<ProductColorGroupLine>("ProductColorGroupLines", "", "[ax].[ProductColorGroupLine]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            if (colorGroupLines != null)
            {
                return colorGroupLines;
            }

            var sizeGroupLines = ServiceConnector.CallOdataEndpoint<ProductSizeGroupLine>("ProductSizeGroupLines", "", "[ax].[ProductSizeGroupLine]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            if (sizeGroupLines != null)
            {
                return sizeGroupLines;
            }

            var styleGroupLines = ServiceConnector.CallOdataEndpoint<ProductStyleGroupLine>("ProductStyleGroupLines", "", "[ax].[ProductStyleGroupLine]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            if (styleGroupLines != null)
            {
                return styleGroupLines;
            }

            var color = ServiceConnector.CallOdataEndpoint<ProductColor>("ProductColors", "", "[ax].[ProductColor]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            if (color != null)
            {
                return color;
            }

            var size = ServiceConnector.CallOdataEndpoint<ProductSize>("ProductSizes", "", "[ax].[ProductSize]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            if (size != null)
            {
                return size;
            }

            var style = ServiceConnector.CallOdataEndpoint<ProductStyle>("ProductStyles", "", "[ax].[ProductStyle]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            if (style != null)
            {
                return style;
            }
            if (includeBandM)
            {
                var prodAttribute = ServiceConnector.CallOdataEndpoint<ProductAttribute>("ProductAttributes", "", "[ax].[ProductAttributes]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
                if (prodAttribute != null)
                {
                    return prodAttribute;
                }

                var attrValue = ServiceConnector.CallOdataEndpoint<ProductAttributeValue>("ProductAttributeValues", "", "[ax].[ProductAttributeValues]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
                if (attrValue != null)
                {
                    return attrValue;
                }

                //var ecoResValue = ServiceConnector.CallService<AGREcoResValueDTO>(actionId,  "GetValue", "AGRAttributeService", "[ax].[ECORESVALUE]", 10000);
                //if (ecoResValue != null)
                //{
                //    return ecoResValue;
                //}

                var attrViaService = UpdateProductAttributes(actionId);

                if (attrViaService != null)
                {



                    return null;
                }





            }

            return null;
        }
       

    }
}
