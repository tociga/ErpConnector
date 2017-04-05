﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxConnect.Microsoft.Dynamics.DataEntities;
using AxConnect.DTO;


namespace AxConnect.Modules
{
    public class CreateItems
    {
        public static void CreateSku(Resources context, string adalHeader, DistinctProduct distinctProduct, ReleasedDistinctProductsDTO releasedDistinctProduct,
            List<ReleasedProductVariantDTO> variants)
        {
            //First add distinct product
            //context.AddToDistinctProducts(distinctProduct);
            //context.SaveChanges();

            //var result = AXServiceConnector.CallOdataEndpointPost<ReleasedDistinctProductsDTO>("ReleasedDistinctProducts", null, adalHeader, releasedDistinctProduct).Result;
            //Add distinct product
            //context.AddToReleasedDistinctProducts(releasedDistinctProduct);
            //context.SaveChanges();


            foreach(var variant in variants)
            {
                //context.AddToReleasedProductVariants(variant);
                //context.SaveChanges();
                var variantResult = AXServiceConnector.CallOdataEndpointPost<ReleasedProductVariantDTO>("ReleasedProductVariants", null, adalHeader, variant).Result;
            }
        }
    }
}
