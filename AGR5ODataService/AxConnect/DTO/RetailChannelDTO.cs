using AxConnect.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class RetailChannelDTO
    {
        private RetailClosingMethodType _closingMethod;
        private NoYes _inventoryLookup;
        private RetailChannelType _channelType;
        public string OnlineCatalogName { get; set; }
        public string StoreNumber { get; set; }
        public object closingMethod
        {
            get
            {
                return (int)_closingMethod;
            }
            set
            {
                if (value is RetailClosingMethodType)
                {
                    _closingMethod = (RetailClosingMethodType)value;
                }
                else
                {
                    _closingMethod = RetailClosingMethodType.DateTime;
                }
            }
        }
        public string functionalityProfile { get; set; }
        public object inventoryLookup
        {
            get
            {
                return (int)_inventoryLookup;
            }
            set
            {
                if (value is NoYes)
                {
                    _inventoryLookup = (NoYes)value;
                }
                else
                {
                    _inventoryLookup = NoYes.No;
                }
            }
        }
        public string removeAddTender { get; set; }
        public decimal StoreArea { get; set; }
        public string OperatingUnitNumber { get; set; }
        public string InventLocationDataAreaId { get; set; }
        public string InventLocation { get; set; }
        public string DefaultDimensionDisplayValue { get; set; }
        public object ChannelType
        {
            get
            {
                return (int)_channelType;
            }
            set
            {
                if(value is RetailChannelType)
                {
                    _channelType = (RetailChannelType)value;
                }
                else
                {
                    _channelType = RetailChannelType.RetailStore;
                }
            }
        }
        public string OperatingUnitPartyNumber { get; set; }
        public string RetailChannelId { get; set; }
        public long PublicOMOperatingUnitID { get; set; }
        public long PublicCategoryHierarchy { get; set; }
        public long PublicPartition { get; set; }



        //public string LiveDatabaseConnectionProfileName { get; set; }
        //public string statementMethod { get; set; }
        //public string ChannelProfileName { get; set; }
        //public string maximumTextLengthOnReceipt { get; set; }
        //public string maxRoundingAmount { get; set; }
        //public string generatesItemLabels { get; set; }
        //public string maxTransactionDifferenceAmount { get; set; }
        //public string openFrom { get; set; }
        //public string TaxGroupCode { get; set; }
        //public string PriceIncludesSalesTax { get; set; }
        //public string MCRCustomerCreditRetailInfocodeId { get; set; }
        //public string maxRoundingTaxAmount { get; set; }
        //public string roundingTaxAccount { get; set; }

        //public string itemIdOnReceipt { get; set; }

        //public string MCREnableOrderCompletion { get; set; }
        //public string EventNotificationProfileId { get; set; }
        //public string TransactionServiceProfile { get; set; }
        //public string TaxOverrideGroup { get; set; }
        //public string maximumPostingDifference { get; set; }
        //public string MCRPriceOverrideRetailInfocodeId { get; set; }
        //public string hideTrainingMode { get; set; }
        //public string separateStmtPerStaffTerminal { get; set; }
        //public string ReturnTaxGroup_W { get; set; }
        //public string dataAreaId { get; set; }
        //public string RetailReqPlanIdSched { get; set; }
        //public string TaxIdentificationNumber { get; set; }
        //public string EFTStoreNumber { get; set; }
        //public string oneStatementPerDay { get; set; }
        //public string ChannelTimeZone { get; set; }
        //public string UseCustomerBasedTax { get; set; }
        //public string createLabelsForZeroPrice { get; set; }
        //public string Currency { get; set; }

        //public string cultureName { get; set; }
        //public string UseDestinationBasedTax { get; set; }
        //public string stmtCalcBatchEndTime { get; set; }
        //public string stmtPostAsBusinessDay { get; set; }
        //public string SQLServerName { get; set; }
        //public string numberOfTopOrBottomLines { get; set; }
        //public string DefaultCustomerAccount { get; set; }
        //public string MCREnableDirectedSelling { get; set; }
        //public string ChannelTimeZoneInfoId { get; set; }
        //public string MCRReasonCodeRetailInfocodeId { get; set; }
        //public string tenderDeclarationCalculation { get; set; }
        //public string TaxGroupLegalEntity { get; set; }

        //public string OfflineProfileName { get; set; }
        //public string InventLocationIdForCustomerOrder { get; set; }
        //public string UseDefaultCustAccount { get; set; }
        //public string serviceChargePct { get; set; }
        //public string PaymMode { get; set; }
        //public string serviceChargePrompt { get; set; }
        //public string RoundingAccountLedgerDimensionDisplayValue { get; set; }

        //public string DatabaseName { get; set; }
        //public string maxShiftDifferenceAmount { get; set; }
        //public string openTo { get; set; }
        //public string MCREnableOrderPriceControl { get; set; }
        //public string UserName { get; set; }
        //public string poItemFilter { get; set; }
        //public string Payment { get; set; }
        //public string phone { get; set; }

        //public string DefaultCustomerLegalEntity { get; set; }
        //public string generatesShelfLabels { get; set; }
        //public string DisplayTaxPerTaxComponent { get; set; }
    }
}
