﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class SAPReservationDTO
    {
        public List<TReservationDTO> tReservations { get; set; }
        public List<TBapiretDTO> tBapiret { get; set; }
    }
}