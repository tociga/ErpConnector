using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax
{
    public class EnumConverter : JsonConverter
    {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType() == typeof(NoYes))
            {
                writer.WriteValue(((NoYes)value).ToString());
            }

            if (value.GetType() == typeof(ItemNumAlternative))
            {
                writer.WriteValue(((ItemNumAlternative)value).ToString());
            }

            if (value.GetType() == typeof(PdsVendorCheckItem))
            {
                writer.WriteValue(((PdsVendorCheckItem)value).ToString());
            }

            if (value.GetType() == typeof(SalesPriceModelBasic))
            {
                writer.WriteValue(((SalesPriceModelBasic)value).ToString());
            }

            if (value.GetType() == typeof(InventBatchMergeDateCalculationMethod))
            {
                writer.WriteValue(((InventBatchMergeDateCalculationMethod)value).ToString());
            }

            if (value.GetType() == typeof(ABC))
            {
                writer.WriteValue(((ABC)value).ToString());
            }

            if (value.GetType() == typeof(ReqPOType))
            {
                writer.WriteValue(((ReqPOType)value).ToString());
            }

            if (value.GetType() == typeof(ProdFlushingPrincipItem))
            {
                writer.WriteValue(((ProdFlushingPrincipItem)value).ToString());
            }

            if (value.GetType() == typeof(RetailPriceKeyingRequirement))
            {
                writer.WriteValue(((RetailPriceKeyingRequirement)value).ToString());
            }

            if (value.GetType() == typeof(PDSPotencyAttribRecordingEnum))
            {
                writer.WriteValue(((PDSPotencyAttribRecordingEnum)value).ToString());
            }

            if (value.GetType() == typeof(RetailQtyKeyingRequirement))
            {
                writer.WriteValue(((RetailQtyKeyingRequirement)value).ToString());
            }

            if (value.GetType() == typeof(FITaxationOrigin_BR))
            {
                writer.WriteValue(((FITaxationOrigin_BR)value).ToString());
            }

            if (value.GetType() == typeof(WHSAllowMaterialOverPick))
            {
                writer.WriteValue(((WHSAllowMaterialOverPick)value).ToString());
            }

            if (value.GetType() == typeof(SalesPriceModel))
            {
                writer.WriteValue(((SalesPriceModel)value).ToString());
            }

            if (value.GetType() == typeof(PurchMatchingPolicyWithNotSetOption))
            {
                writer.WriteValue(((PurchMatchingPolicyWithNotSetOption)value).ToString());
            }

            if (value.GetType() == typeof(PmfProductType))
            {
                writer.WriteValue(((PmfProductType)value).ToString());
            }

            if (value.GetType() == typeof(EcoResVariantConfigurationTechnologyType))
            {
                writer.WriteValue(((EcoResVariantConfigurationTechnologyType)value).ToString());
            }

            if (value.GetType() == typeof(EcoResProductType))
            {
                writer.WriteValue(((EcoResProductType)value).ToString());
            }
        }

        public override bool CanConvert(Type t)
        {
            return t == typeof(NoYes) || t == typeof(ItemNumAlternative) || t==typeof(PdsVendorCheckItem) || t == typeof(SalesPriceModelBasic) 
                || t == typeof(InventBatchMergeDateCalculationMethod) || t == typeof(ABC) || t == typeof(ReqPOType) 
                || t == typeof(ProdFlushingPrincipItem) || t == typeof(RetailPriceKeyingRequirement) || t == typeof(RetailQtyKeyingRequirement)
                || t == typeof(PDSPotencyAttribRecordingEnum) || t == typeof(FITaxationOrigin_BR) || t == typeof(WHSAllowMaterialOverPick)
                || t == typeof(SalesPriceModel) || t == typeof(PurchMatchingPolicyWithNotSetOption) || t==typeof(PmfProductType)
                || t == typeof(EcoResVariantConfigurationTechnologyType) || t == typeof(EcoResProductType);
        }

        public override bool CanRead
        {
            get
            {
                return base.CanRead;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
