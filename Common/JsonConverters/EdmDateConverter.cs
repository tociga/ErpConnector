﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.JsonConverters
{
    public class EdmDateConverter : DateTimeConverterBase
    {
        public override bool CanConvert(Type objectType)
        {
            var type = Nullable.GetUnderlyingType(objectType) ?? objectType;
            return type == typeof(global::Microsoft.OData.Edm.Date);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.SkipComments().TokenType == JsonToken.Null)
            {
                return null;
            }

            string dateStr = (string)reader.Value;
            return (global::Microsoft.OData.Edm.Date)global::Microsoft.OData.Edm.Date.Parse(dateStr);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string myDate = ((global::Microsoft.OData.Edm.Date)value).Year.ToString("D4");
            myDate += "-";
            myDate += ((global::Microsoft.OData.Edm.Date)value).Month.ToString("D2");
            myDate += "-";
            myDate += ((global::Microsoft.OData.Edm.Date)value).Day.ToString("D2");

            writer.WriteValue(myDate);
        }
    }
    public static partial class JsonExtensions
    {
        public static JsonReader SkipComments(this JsonReader reader)
        {
            while (reader.TokenType == JsonToken.Comment && reader.Read())
                ;
            return reader;
        }
    }
}
