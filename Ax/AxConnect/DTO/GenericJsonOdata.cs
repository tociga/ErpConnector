﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class GenericJsonOdata<T>
    {
        public List<T> value { get; set;}
    }
}
