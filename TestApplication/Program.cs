using ErpConnector.Ax;
using ErpConnector.Ax.Authentication;
using ErpConnector.Ax.DTO;
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

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ErpConnector.Common.Util.EmailSender.SendEmail(1, DateTime.Now);
            //var useTsl = System.Configuration.ConfigurationManager.AppSettings["use_security_tsl"];
            //if (useTsl == "true")
            //{
                
            //}
            //string axBaseUrl = ConfigurationManager.AppSettings["ax_base_url"];
            //var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
            //                                           ConfigurationManager.AppSettings["ax_client_secret"],
            //                                           axBaseUrl,
            //                                           ConfigurationManager.AppSettings["ax_oauth_token_url"],
            //                                           ConfigurationManager.AppSettings["ax_client_key"]);

            //ErpConnector.Ax.Authentication.OAuthHelper helper = new ErpConnector.Ax.Authentication.OAuthHelper(clientconfig);
            //helper.GetAuthenticationHeaderUserPass();
            //ErpConnector.Ax.Microsoft.Dynamics.DataEntities.ReleasedDistinctProduct rd = new ErpConnector.Ax.Microsoft.Dynamics.DataEntities.ReleasedDistinctProduct();
            //Type entityObject = typeof(ErpConnector.Ax.DTO.VendorDTO);

            //if (entityObject != null)
            //{
            //    ScriptGenerator sg = new ScriptGenerator(entityObject);
            //    string str = sg.CreateScript("[ax].[VENDOR_SERVICE]");
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
            //string axBaseUrl = ConfigurationManager.AppSettings["ax_base_url"];
            //var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
            //                                               ConfigurationManager.AppSettings["ax_client_secret"],
            //                                               axBaseUrl,
            //                                               ConfigurationManager.AppSettings["ax_oauth_token_url"],
            //                                               ConfigurationManager.AppSettings["ax_client_key"]);
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
            //string axBaseUrl = ConfigurationManager.AppSettings["ax_base_url"];
            //var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
            //                                               ConfigurationManager.AppSettings["ax_client_secret"],
            //                                               axBaseUrl,
            //                                               ConfigurationManager.AppSettings["ax_oauth_token_url"],
            //                                               ConfigurationManager.AppSettings["ax_client_key"]);
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
