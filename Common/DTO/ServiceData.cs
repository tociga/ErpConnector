﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Common.ErpTasks;
using Newtonsoft.Json;

namespace ErpConnector.Common.DTO
{
    public class ServiceData
    {
        public string AuthMethod { get; set; }
        public string AuthHeader
        {
            get
            {
                return AuthMethod + AuthToken;
            }
        }
        public string AuthToken { get; set; }
        public string BaseUrl { get; set; }
        public string OdataUrlPostFix { get; set; }
        public ErpTaskStep.AuthenticationType AuthType { get; set; }
        public object InjectionPropertyValue { get; set; }
        public string InjectionPropertyName { get; set; }
        public JsonConverter CustomJsonConverter { get; set; }
    }
}
