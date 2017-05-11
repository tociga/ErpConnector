using AxConCommon.Extensions;
using ErpDTO.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.Modules
{
	public class SalesValueTransactions
	{
		public static void WriteSalesValueTrans(Resources context)
		{
			//DataAccess.DataWriter.TruncateTables(false, true, true);
			var salesTransOld = ReadTransActionHeaders(context, new DateTimeOffset(new DateTime(2011,1,1)), 
				new DateTimeOffset(new DateTime(2013,1,1))
                //DateTime.Now.Date.AddDays(-30)
				);
			DataAccess.DataWriter.WriteToTable(salesTransOld, "[ax].[RetailTransactionTable]");

			//var salesTransNew = ReadTransActionHeaders(context, DateTime.Now.Date.AddDays(-30), DateTime.Now.Date);
			//DataAccess.DataWriter.WriteToTable(salesTransNew, "[ax].[RetailTransactionTable_increment]");
            //var variants = ReadVariants(context);

            WriteSalesValueLines(context);
            
		}

        private static void WriteSalesValueLines(Resources context)
        {
            var list = new List<dynamic>();
            int pagesize = 3000;
            int i = 49;
            bool hasData = true;
            while (hasData)
            {
                List<RetailTransactionSalesLine> lines = new List<RetailTransactionSalesLine>();
                if (i == 0)
                {
                    lines = context.RetailTransactionSalesLines.Take(pagesize).ToList();
                }
                else
                {
                    lines = context.RetailTransactionSalesLines.Skip(i * pagesize).Take(pagesize).ToList();
                }
                if (lines.Count > 0)
                {
                    foreach(var l in lines)
                    {
                        list.Add(GetDynamicFromSalesTrans(l));
                    }
                    //hasData = false;
                    DataAccess.DataWriter.WriteToTable(list.GetDataReader<dynamic>(), "[ax].[RetailTransactionSalesLineTable]");
                    list.Clear();
                }
                else
                {
                    hasData = false;
                }
                i++;
            }            
        }
		private static dynamic GetDynamicFromSalesTrans(RetailTransactionSalesLine line)
		{
			return new {
				SALESTAXGROUP = line.SalesTaxGroup,
				ITEMSALESTAXGROUP = line.ItemSalesTaxGroup,
				TERMINAL = line.Terminal,
				TRANSACTIONNUMBER = line.TransactionNumber,
				BARCODE = line.BarCode,
				COSTAMOUNT = line.CostAmount,
				CURRENCY = line.Currency,
				CUSTOMERACCOUNT = line.CustomerAccount,
				CUSTOMERDISCOUNT = line.CustomerDiscount,
				CUSTOMERINVOICEDISCOUNTAMOUNT = line.CustomerInvoiceDiscountAmount,
				CASHDISCOUNTAMOUNT = line.CashDiscountAmount,
				PRICEGROUPS = line.PriceGroups,
				OFFERNUMBER = line.OfferNumber,
				DISCOUNTAMOUNTFORPRINTING = line.DiscountAmountForPrinting,
				MODEOFDELIVERY = line.ModeOfDelivery,
				ELECTRONICDELIVERYEMAIL = line.ElectronicDeliveryEmail,
				RETAILEMAILADDRESSCONTENT = line.RetailEmailAddressContent,
				GIFTCARD = line.GiftCard.GetValueOrDefault() == NoYes.Yes ? 1 :0,
				REASONCODEDISCOUNT = line.ReasonCodeDiscount,
				WAREHOUSE = line.Warehouse,
				SERIALNUMBER = line.SerialNumber,
				SITEID = line.SiteId,
				INVENTORYSTATUS = (int)line.InventoryStatus.GetValueOrDefault(),
				LOTID = line.LotID,
				ITEMID = line.ItemId,
				PRODUCTSCANNED = line.ProductScanned.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
				ITEMRELATION = line.ItemRelation,
				KEYBOARDPRODUCTENTRY = line.KeyboardProductEntry.GetValueOrDefault() == NoYes.Yes ? 1: 0,
				LINEDISCOUNT = line.LineDiscount,
				LINEMANUALDISCOUNTAMOUNT = line.LineManualDiscountAmount,
				LINEMANUALDISCOUNTPERCENTAGE = line.LineManualDiscountPercentage,
				LINENUMBER = line.LineNumber,
				ISLINEDISCOUNTED = line.IsLineDiscounted.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
				ISLINKEDPRODUCTNOTORIGINAL = line.IsLinkedProductNotOriginal.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
				CHANNELLISTINGID = line.ChannelListingID,
				NETAMOUNT = line.NetAmount,
				NETAMOUNTINCLUSIVETAX = line.NetAmountInclusiveTax,
				NETPRICE = line.NetPrice,
				ISORIGINALOFLINKEDPRODUCTLIST = line.IsOriginalOfLinkedProductList.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
				ORIGINALPRICE = line.OriginalPrice,
				ORIGINALSALESTAXGROUP = line.OriginalSalesTaxGroup, 
				ORIGINALITEMSALESTAXGROUP = line.OriginalItemSalesTaxGroup,
				PERIODICDISCOUNTAMOUNT = line.PeriodicDiscountAmount,
				PERIODICDISCOUNTGROUP = line.PeriodicDiscountGroup,
				PERIODICDISCOUNTPERCENTAGE = line.PeriodicDiscountPercentage,
				PRICE = line.Price,
				ISPRICECHANGE = line.IsPriceChange.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
				PRICEINBARCODE = line.PriceInBarCode == NoYes.Yes ? 1 : 0,
				QUANTITY = line.Quantity,
				REQUESTEDRECEIPTDATE = line.RequestedReceiptDate.DateTime,
				RECEIPTNUMBER = line.ReceiptNumber,
				RETURNLINENUMBER = line.ReturnLineNumber,
				ISRETURNNOSALE = line.IsReturnNoSale.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
				RETURNQUANTITY = line.ReturnQuantity,
				RETURNTERMINAL = line.ReturnTerminal,
				RETURNTRANSACTIONNUMBER = line.ReturnTransactionNumber,
				RFIDTAGID = line.RFIDTagId,
				ISSCALEPRODUCT = line.IsScaleProduct.GetValueOrDefault() == NoYes.Yes ? 1: 0,
				SECTIONNUMBER = line.SectionNumber,
				SHELFNUMBER = line.ShelfNumber,
				REQUESTEDSHIPDATE = line.RequestedShipDate.DateTime,
				STANDARDNETPRICE = line.StandardNetPrice,
				SALESTAXAMOUNT = line.SalesTaxAmount,
				TOTALDISCOUNT = line.TotalDiscount,
				TOTALDISCOUNTINFOCODELINENUM = line.TotalDiscountInfoCodeLineNum,
				TOTALDISCOUNTPERCENTAGE = line.TotalDiscountPercentage,
				TRANSACTIONCODE = line.TransactionCode,
				TRANSACTIONSTATUS = (int)line.TransactionStatus.GetValueOrDefault(),
				UNIT = line.Unit,
				UNITPRICE = line.UnitPrice,
				UNITQUANTITY = line.UnitQuantity,
				VARIANTNUMBER = line.VariantNumber,
				ISWEIGHTPRODUCT = line.IsWeightProduct.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
				ISWEIGHTMANUALLYENTERED = line.IsWeightManuallyEntered.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
				CATEGORYNAME = line.CategoryName,
				CATEGORYHIERARCHYNAME = line.CategoryHierarchyName,
				LOGISTICSPOSTALADDRESSVALIDFROM = line.LogisticsPostalAddressValidFrom.DateTime,
				LOGISTICLOCATIONID = line.LogisticLocationId,
				OPERATINGUNITNUMBER = line.OperatingUnitNumber,
				RETURNOPERATINGUNITNUMBER = line.ReturnOperatingUnitNumber,
				DATAAREAID = line.DataAreaId
			};
		}
		private static dynamic GetDynamicFromTransAction(RetailTransaction trans)
		{
			return new
			{
				TERMINAL = trans.Terminal,
				AMOUNTPOSTEDTOACCOUNT = trans.AmountPostedToAccount,
				CHANNELREFERENCEID = trans.ChannelReferenceId,
				COSTAMOUNT = trans.CostAmount,
				CREATEDOFFLINE = trans.CreatedOffline.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
				CURRENCY = trans.Currency,
				CUSTOMERACCOUNT = trans.CustomerAccount,
				CUSTOMERDISCOUNTAMOUNT = trans.CustomerDiscountAmount,
				DISCOUNTAMOUNT = trans.DiscountAmount,
				DELIVERYMODE = trans.DeliveryMode,
				TRANSACTIONSTATUS = trans.TransactionStatus,
				EXCHANGERATE = trans.ExchangeRate,
				GROSSAMOUNT = trans.GrossAmount,
				INCOMEEXPENSEAMOUNT = trans.IncomeExpenseAmount,
				INFOCODEDISCOUNTGROUP = trans.InfocodeDiscountGroup,
				WAREHOUSE = trans.Warehouse,
				SITEID = trans.SiteId,
				INVOICEID = trans.InvoiceId,
				ITEMSPOSTED = trans.ItemsPosted,
				LOYALTYCARDID = trans.LoyaltyCardId,
				NETAMOUNT = trans.NetAmount,
				PAYMENTAMOUNT = trans.PaymentAmount,
				POSTASSHIPMENT = trans.PostAsShipment.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
				RRECEIPTID = trans.RreceiptId,
				REFUNDRECEIPTID = trans.RefundReceiptId,
				SALEISRETURNSALE = trans.SaleIsReturnSale,
				SALESINVOICEAMOUNT = trans.SalesInvoiceAmount,
				SALESORDERAMOUNT = trans.SalesOrderAmount,
				SALESORDERID = trans.SalesOrderId,
				SALESPAYMENTDIFFERENCE = trans.SalesPaymentDifference,
				SHIFT = trans.Shift,
				SHIPPINGDATEREQUESTED = trans.ShippingDateRequested.DateTime,
				STAFF = trans.Staff,
				TOACCOUNT = trans.ToAccount.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
				TOTALDISCOUNTAMOUNT = trans.TotalDiscountAmount,
				TOTALMANUALDISCOUNTAMOUNT = trans.TotalManualDiscountAmount,
				TOTALMANUALDISCOUNTPERCENTAGE = trans.TotalManualDiscountPercentage,
				TRANSACTIONNUMBER = trans.TransactionNumber,
				TRANSACTIONDATE = trans.TransactionDate.DateTime,
				TRANSACTIONTIME = trans.TransactionTime,
				TRANSACTIONTYPE = trans.TransactionType,
				LOGISTICSLOCATIONID = trans.LogisticsLocationId,
				LOGISTICSPOSTALCITY = trans.LogisticsPostalCity,
				LOGISTICSPOSTALCOUNTY = trans.LogisticsPostalCounty,
				LOGISTICSPOSTALSTATE = trans.LogisticsPostalState,
				LOGISTICSPOSTALSTREET = trans.LogisticsPostalStreet,
				LOGISTICSPOSTALZIPCODE = trans.LogisticsPostalZipCode,
				LOGISTICSPOSTALADDRESSVALIDFROM = trans.LogisticsPostalAddressValidFrom.DateTime,
				LOGISTICPOSTALADDRESSVALIDTO = trans.LogisticPostalAddressValidTo.DateTime,
				OPERATINGUNITNUMBER = trans.OperatingUnitNumber,
				DATAAREAID = trans.DataAreaId
			};
		}
		private static IGenericDataReader ReadTransActionHeaders(Resources context, DateTimeOffset startDate, DateTimeOffset endDate)
		{
			var list = new List<dynamic>();
			for (DateTimeOffset d = startDate; d <= endDate; d = d.AddDays(1))
			{
				var headers = (from h in context.RetailTransactions
							   where h.TransactionDate > d.AddDays(-1) && h.TransactionDate < d.AddDays(1)
							   select h);
				//var headers = context.RetailTransactions.Where(t=>t.TransactionDate <= d && t.TransactionDate > d.AddDays(-1));

				foreach (var h in headers)
				{
					list.Add(GetDynamicFromTransAction(h));
				}
			}
			return list.GetDataReader<dynamic>();
		}

        //private static IGenericDataReader ReadTransActions(Resources context)
        //{
        //    var list = context.RetailTr
        //}

	}
}
