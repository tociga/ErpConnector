using ErpConnector.Ax;
using ErpConnector.Ax.Authentication;
//using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Utils;
using ErpConnector.Common.AGREntities;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Client;
using ErpConnector.Common.Util;
using ErpConnector.Nav.DTO;
using ErpConnector.Common.DTO;
using ErpConnector.Common;
using ErpConnector.Common.ErpTasks;
using ErpConnector.Sap.DTO;
using Newtonsoft.Json;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //AGR_ItemCategory
            var tos = new List<POTOCreate>();
            tos.Add(
                new POTOCreate
                {
                    item_no = "15468",
                    order_id = 1,
                    description = "Test TO1",
                    location_no = "0302",
                    order_from_location_no = "0200",
                    vendor_location_type = "warehouse",
                    est_delivery_date = DateTime.Now.Date,
                    unit = "ST",
                    unit_qty_chg = 100m,                    
                }
            ) ;

            tos.Add(
                new POTOCreate
                {
                    item_no = "25331",
                    order_id = 1,
                    description = "Test TO1",
                    location_no = "0302",
                    order_from_location_no = "0200",
                    vendor_location_type = "warehouse",
                    est_delivery_date = DateTime.Now.Date,
                    unit = "ST",
                    unit_qty_chg = 50m,
                }
            );

            var sapCreate = new ErpConnector.Sap.SAPDataConnector();
            //sapCreate.CreatePoTo(tos, -1);

            var pos = new List<POTOCreate>();
            pos.Add(
                new POTOCreate
                {
                    item_no = "42165",
                    order_id = 2,
                    description = "Test PO1",
                    location_no = "0200",
                    order_from_location_no = "1000081935",
                    vendor_location_type = "vendor",
                    est_delivery_date = DateTime.Now.Date,
                    unit = "ST",
                    unit_qty_chg = 50m,
                }
            );
            sapCreate.CreatePoTo(pos, -1);
            //var sapReservation = new SAPReservationWriteDTO
            //{
            //    Header = new SAPResHeaderDTO { CreatedBy = "AGR", MovePlant = "1000", MoveStloc = "0302", Plant = "1000", MoveType = "311", ProfitCtr = "0000916299", ResDate = DateTime.Now.ToString("yyyy-MM-dd") },
            //    Lines = new List<SAPResLineDTO>()
            //};
            //sapReservation.Lines.Add(new SAPResLineDTO
            //{
            //    Material = "15468",
            //    Plant = "1000",
            //    Quantity = 100m,
            //    ShortText = "Description from AGR",
            //    StoreLoc = "0200",
            //    Unit = "ST"
            //});

            //sapReservation.Lines.Add(new SAPResLineDTO
            //{
            //    Material = "25331",
            //    Plant = "1000",
            //    Quantity = 50m,
            //    ShortText = "Description from AGR",
            //    StoreLoc = "0200",
            //    Unit = "ST"
            //});

            //var reservation = Newtonsoft.Json.JsonConvert.SerializeObject(sapReservation);
            //System.Diagnostics.Debug.WriteLine(reservation);

            //List<SAPRequisitionDTO> req = new List<SAPRequisitionDTO>();
            //req.Add(
            //    new SAPRequisitionDTO
            //    {
            //        CreatedBy = "AGR",
            //        DocType = "NB",
            //        PreqName = "AGR",
            //        ShortText = "Description from AGR",
            //        Material = "Item no 1",
            //        Plant = "1000",
            //        StoreLoc = "Location no",
            //        Quantity = 150m,
            //        Unit = "M or ST",
            //        DelivDate = DateTime.Now.Date.AddDays(10).ToString("yyyy-MM-dd"),
            //        GrPrTime = 340m,
            //        CAmtBapi = 0m,
            //        PriceUnit = 0m
            //    }
            //);
            //req.Add(
            //    new SAPRequisitionDTO
            //    {
            //        CreatedBy = "AGR",
            //        DocType = "NB",
            //        PreqName = "AGR",
            //        ShortText = "Description from AGR",
            //        Material = "Item no 2",
            //        Plant = "1000",
            //        StoreLoc = "Location no",
            //        Quantity = 150m,
            //        Unit = "M or ST",
            //        DelivDate = DateTime.Now.Date.AddDays(10).ToString("yyyy-MM-dd"),
            //        GrPrTime = 340m,
            //        CAmtBapi = 0m,
            //        PriceUnit = 0m
            //    }
            //);
            //var requisition = JsonConvert.SerializeObject(req);
            //System.Diagnostics.Debug.WriteLine(requisition);
            //ServiceData authData = new ServiceData
            //{
            //    AuthMethod = "Basic",                
            //    AuthToken = "QUdSX0VJTkFSOkE1ZzZyN192b3I=",
            //    BaseUrl = "http://sap-s4d.siminn.is:8000/siminn/agr/",
            //    AuthType = ErpConnector.Common.ErpTasks.ErpTaskStep.AuthenticationType.BC
            //};
            //List<ErpTaskStepDetails> mapping = new List<ErpTaskStepDetails>();
            //mapping.Add(new ErpTaskStepDetails { nested_property_name = "tLfa1", db_table = "sap.Vendor_refresh", return_type = "SAPVendorDTO" });
            //mapping.Add(new ErpTaskStepDetails { nested_property_name = "tT001l", db_table = "sap.Locations_refresh", return_type = "SAPLocationsDTO" });
            //var result = ServiceConnector.CallOdataEndpointComplex<GenericJsonOdata<SAPSuppliersDTO>, SAPSuppliersDTO>("Suppliers", null, mapping, -1, authData, "test").Result;
            ////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ////ErpConnector.Common.Util.EmailSender.SendEmail(2031, DateTime.Now);
            ////var useTsl = System.Configuration.ConfigurationManager.AppSettings["use_security_tsl"];
            ////if (useTsl == "true")
            //{

            //}
            //string axBaseUrl = ConfigurationManager.AppSettings["base_url"];
            //var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
            //                                           ConfigurationManager.AppSettings["client_secret"],
            //                                           axBaseUrl,
            //                                           ConfigurationManager.AppSettings["ax_oauth_token_url"],
            //                                           ConfigurationManager.AppSettings["client_key"]);

            //ErpConnector.Ax.Authentication.OAuthHelper helper = new ErpConnector.Ax.Authentication.OAuthHelper(clientconfig);
            //helper.GetAuthenticationHeaderUserPass();
            //ErpConnector.Ax.Microsoft.Dynamics.DataEntities.ReleasedDistinctProduct rd = new ErpConnector.Ax.Microsoft.Dynamics.DataEntities.ReleasedDistinctProduct();
            //Type entityObject = typeof(AGR_Vendor);

            //if (entityObject != null)
            //{
            //    ScriptGenerator sg = new ScriptGenerator(entityObject);
            //    string str = sg.CreateScript("[nav].[purchase_line]");
            //    System.Diagnostics.Trace.WriteLine(str);
            //}

            //entityObject = typeof(ErpConnector.Ax.DTO.SalesTableDTO);

            //if (entityObject != null)
            //{
            //    ScriptGenerator sg = new ScriptGenerator(entityObject);
            //    string str = sg.CreateScript("[ax].[SalesTable]");
            //    System.Diagnostics.Trace.WriteLine(str);
            //}

            //entityObject = typeof(ErpConnector.Ax.DTO.SalesLineDTO);

            //if (entityObject != null)
            //{
            //    ScriptGenerator sg = new ScriptGenerator(entityObject);
            //    string str = sg.CreateScript("[ax].[SalesLine]");
            //    System.Diagnostics.Trace.WriteLine(str);
            //}

            //entityObject = typeof(ErpConnector.Ax.DTO.ProdTableDTO);

            //if (entityObject != null)
            //{
            //    ScriptGenerator sg = new ScriptGenerator(entityObject);
            //    string str = sg.CreateScript("[ax].[ProdTable]");
            //    System.Diagnostics.Trace.WriteLine(str);
            //}

            //DataWriter.ValidateColumnMapping<ErpConnector.Ax.Microsoft.Dynamics.DataEntities.AGROrder>("[ax].[AGROrderTable]");
            //AxODataConnector connector = new AxODataConnector();
            //////connector.PimFull(1);

            //List<POTOCreate> list = new List<POTOCreate>();

            ////list.Add(new POTOCreate { order_id = 60, item_no = "0140", size = "Large", color = "Black", unit_qty_chg = 10m, location_no = "DC-WEST", order_from_location_no = "1004", est_delivery_date = new DateTime(2017, 11, 15),
            ////    vendor_location_type = "vendor"});
            //list.Add(new POTOCreate
            //{
            //    order_id = 11,
            //    item_no = "2002079",
            //    size = "",
            //    color = "",
            //    unit_qty_chg = 105m,
            //    location_no = "DC",
            //    order_from_location_no = "RI001",
            //    est_delivery_date = new DateTime(2018, 6, 30),
            //    vendor_location_type = "vendor"
            //});

            //list.Add(new POTOCreate
            //{
            //    order_id = 11,
            //    item_no = "2002121",
            //    size = "",
            //    color = "",
            //    unit_qty_chg = 100m,
            //    location_no = "DC",
            //    order_from_location_no = "RI001",
            //    est_delivery_date = new DateTime(2016, 6, 30),
            //    vendor_location_type = "vendor"
            //});


            //list.Add(new POTOCreate
            //{
            //    order_id = 21,
            //    item_no = "0001",
            //    size = "",
            //    color = "",
            //    unit_qty_chg = 105m,
            //    location_no = "ANNAPOL",
            //    order_from_location_no = "1004",
            //    est_delivery_date = new DateTime(2018, 6, 30),
            //    vendor_location_type = "vendor"
            //});

            //list.Add(new POTOCreate
            //{
            //    order_id = 21,
            //    item_no = "0001",
            //    size = "",
            //    color = "",
            //    unit_qty_chg = 100m,
            //    location_no = "ANNAPOL",
            //    order_from_location_no = "1004",
            //    est_delivery_date = new DateTime(2016, 6, 30),
            //    vendor_location_type = "vendor"
            //});


            ////PO
            ////list.Add(new POTOCreate { order_id = 21, item_no = "010611", size = "010", color = "53", unit_qty_chg = 10m, location_no = "GMOOR", order_from_location_no = "SUP00000030", est_delivery_date = new DateTime(2017, 11, 30),
            ////    vendor_location_type = "vendor", style="-" });

            ////TO
            ////list.Add(new POTOCreate { order_id = 23, item_no = "010611", size = "010", color = "53", unit_qty_chg = 10m, location_no = "W0041-LIVE", order_from_location_no = "GMOOR", est_delivery_date = new DateTime(2017, 11, 30),
            ////    vendor_location_type = "warehose", style="-" });

            //var result = connector.CreatePoTo(list, -1);


            //// W0041 - LIVE

            //// Create item
            //var item = new ItemToCreate
            //{
            //    temp_id = 4,
            //    product_no = "thordur test 1",
            //    product_name = "thordur test 1",
            //    description = "thordur test 1",
            //    division_no = "5637145328",
            //    division = "GFR",
            //    department_no = "5637145329",
            //    department = "01 OUTERWEAR",
            //    sup_department_no = "5637149076",
            //    sup_department = "01-FORMAL",
            //    option_name_no = "CHARTREUSE",
            //    option_name = "CHARTREUSE",
            //    size = "ONE",
            //    size_no = "ONE",
            //    color_no = "CHARTREUSE",
            //    color = "CHARTREUSE",
            //    color_group_no = "SS17 WK 6",
            //    color_group = "GARDEN PARTY",
            //    size_group_no = "ONE SIZE",
            //    size_group = "ONE SIZE",
            //    master_status = 0,
            //    primar_vendor_no = "SUP00000001",
            //    sale_price = null,
            //    cost_price = null,
            //    min_order_qty = 10m,
            //    pack_size = 10m,
            //    display_stock = null,
            //    option_id = 102455
            //};
            //var list2 = new List<ItemToCreate>();
            //list2.Add(item);
            //var r = connector.CreateItems(list2, -1);



            //entityObject = typeof(ErpConnector.Ax.DTO.SalesLineDTO);

            //if (entityObject != null)
            //{
            //    ScriptGenerator sg = new ScriptGenerator(entityObject);
            //    string str = sg.CreateScript("[ax].[SalesLine]");
            //    System.Diagnostics.Trace.WriteLine(str);
            //}

            //entityObject = typeof(ErpConnector.Ax.DTO.ProdTableDTO);

            //if (entityObject != null)
            //{
            //    ScriptGenerator sg = new ScriptGenerator(entityObject);
            //    string str = sg.CreateScript("[ax].[ProdTable]");
            //    System.Diagnostics.Trace.WriteLine(str);
            //}

            //DataWriter.ValidateColumnMapping<ErpConnector.Ax.Microsoft.Dynamics.DataEntities.AGROrder>("[ax].[AGROrderTable]");
            //AxODataConnector connector = new AxODataConnector();
            //////connector.PimFull(1);

            //List<POTOCreate> list = new List<POTOCreate>();

            ////list.Add(new POTOCreate { order_id = 60, item_no = "0140", size = "Large", color = "Black", unit_qty_chg = 10m, location_no = "DC-WEST", order_from_location_no = "1004", est_delivery_date = new DateTime(2017, 11, 15),
            ////    vendor_location_type = "vendor"});
            //list.Add(new POTOCreate
            //{
            //    order_id = 11,
            //    item_no = "2002079",
            //    size = "",
            //    color = "",
            //    unit_qty_chg = 105m,
            //    location_no = "DC",
            //    order_from_location_no = "RI001",
            //    est_delivery_date = new DateTime(2018, 6, 30),
            //    vendor_location_type = "vendor"
            //});

            //list.Add(new POTOCreate
            //{
            //    order_id = 11,
            //    item_no = "2002121",
            //    size = "",
            //    color = "",
            //    unit_qty_chg = 100m,
            //    location_no = "DC",
            //    order_from_location_no = "RI001",
            //    est_delivery_date = new DateTime(2016, 6, 30),
            //    vendor_location_type = "vendor"
            //});


            //list.Add(new POTOCreate
            //{
            //    order_id = 21,
            //    item_no = "0001",
            //    size = "",
            //    color = "",
            //    unit_qty_chg = 105m,
            //    location_no = "ANNAPOL",
            //    order_from_location_no = "1004",
            //    est_delivery_date = new DateTime(2018, 6, 30),
            //    vendor_location_type = "vendor"
            //});

            //list.Add(new POTOCreate
            //{
            //    order_id = 21,
            //    item_no = "0001",
            //    size = "",
            //    color = "",
            //    unit_qty_chg = 100m,
            //    location_no = "ANNAPOL",
            //    order_from_location_no = "1004",
            //    est_delivery_date = new DateTime(2016, 6, 30),
            //    vendor_location_type = "vendor"
            //});


            ////PO
            ////list.Add(new POTOCreate { order_id = 21, item_no = "010611", size = "010", color = "53", unit_qty_chg = 10m, location_no = "GMOOR", order_from_location_no = "SUP00000030", est_delivery_date = new DateTime(2017, 11, 30),
            ////    vendor_location_type = "vendor", style="-" });
            ///

            ////TO
            ////list.Add(new POTOCreate { order_id = 23, item_no = "010611", size = "010", color = "53", unit_qty_chg = 10m, location_no = "W0041-LIVE", order_from_location_no = "GMOOR", est_delivery_date = new DateTime(2017, 11, 30),
            ////    vendor_location_type = "warehose", style="-" });

            //var result = connector.CreatePoTo(list, -1);


            //// W0041 - LIVE

            //// Create item
            //var item = new ItemToCreate
            //{
            //    temp_id = 4,
            //    product_no = "thordur test 1",
            //    product_name = "thordur test 1",
            //    description = "thordur test 1",
            //    division_no = "5637145328",
            //    division = "GFR",
            //    department_no = "5637145329",
            //    department = "01 OUTERWEAR",
            //    sup_department_no = "5637149076",
            //    sup_department = "01-FORMAL",
            //    option_name_no = "CHARTREUSE",
            //    option_name = "CHARTREUSE",
            //    size = "ONE",
            //    size_no = "ONE",
            //    color_no = "CHARTREUSE",
            //    color = "CHARTREUSE",
            //    color_group_no = "SS17 WK 6",
            //    color_group = "GARDEN PARTY",
            //    size_group_no = "ONE SIZE",
            //    size_group = "ONE SIZE",
            //    master_status = 0,
            //    primar_vendor_no = "SUP00000001",
            //    sale_price = null,
            //    cost_price = null,
            //    min_order_qty = 10m,
            //    pack_size = 10m,
            //    display_stock = null,
            //    option_id = 102455
            //};
            //var list2 = new List<ItemToCreate>();
            //list2.Add(item);
            //var r = connector.CreateItems(list2, -1);

            //UpdateMaster();

        }

        public static void UpdateMasterV2()
        {
            //string axBaseUrl = ConfigurationManager.AppSettings["base_url"];
            //var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
            //                                               ConfigurationManager.AppSettings["client_secret"],
            //                                               axBaseUrl,
            //                                               ConfigurationManager.AppSettings["ax_oauth_token_url"],
            //                                               ConfigurationManager.AppSettings["client_key"]);
            //var oAuthHelper = new OAuthHelper(clientconfig);
            //AXODataContext context = new AXODataContext(oAuthHelper, false);

            //ReleasedProductMasterV2 master = new ReleasedProductMasterV2();
            //AGRRetailSeasonTable r = new AGRRetailSeasonTable();


            //var query = from m in context.ReleasedProductMastersV2
            //            where m.ItemNumber == "0140" && m.DataAreaId == "USRT"
            //            select m;
            //var ie = query.GetEnumerator();
            //ie.MoveNext();
            //master = ie.Current;

            //context.TrackEntityInstance(master);
            //master.SalesPrice = 400;

            //context.SaveChanges(SaveChangesOptions.PostOnlySetProperties | SaveChangesOptions.BatchWithSingleChangeset);

        }
        public static void UpdateMaster()
        {
            //string axBaseUrl = ConfigurationManager.AppSettings["base_url"];
            //var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
            //                                               ConfigurationManager.AppSettings["client_secret"],
            //                                               axBaseUrl,
            //                                               ConfigurationManager.AppSettings["ax_oauth_token_url"],
            //                                               ConfigurationManager.AppSettings["client_key"]);
            //var oAuthHelper = new OAuthHelper(clientconfig);
            //AXODataContext context = new AXODataContext(oAuthHelper, false);

            //ReleasedProductMaster master = new ReleasedProductMaster();

            //var query = from m in context.ReleasedProductMasters
            //            where m.ItemNumber == "0140" && m.DataAreaId == "USRT"
            //            select m;
            //var ie = query.GetEnumerator();
            //ie.MoveNext();
            //master = ie.Current;

            //context.TrackEntityInstance(master);
            //master.SalesPrice = 444;

            //context.SaveChanges(SaveChangesOptions.PostOnlySetProperties | SaveChangesOptions.BatchWithSingleChangeset);

        }

    }


}
