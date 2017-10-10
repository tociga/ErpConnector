-- =============================================
-- Author:		Daði Arnason
-- Create date: 29.11.2016
-- Description:	Truncates ax base tables
-- =============================================
CREATE PROCEDURE [ax].[truncate_ax_tables]
	@truncate_items bit = 0,
	@truncate_sales_trans_dumb bit = 0,
	@truncate_locations_and_vendors bit = 0,
	@truncate_sales_trans_refresh bit = 0,
	@truncate_lookup_info bit = 0,
	@truncate_bom BIT = 0,
	@truncate_po_to BIT = 0
AS
BEGIN
	if @truncate_items = 1
	BEGIN
		truncate table ax.ProductMaster
		truncate table ax.ReleasedProductMaster
		truncate table ax.ReleasedDistinctProducts
		truncate table ax.DistinctProduct
		truncate table ax.INVENTDIM
		truncate table ax.ReleasedProductVariants
        truncate table ax.INVENTDIMCOMBINATIONS
		truncate table ax.ECORESCATEGORY
		truncate table ax.ECORESCATEGORYHIERARCHY
		truncate table ax.ECORESCATEGORYHIERARCHYROLE
		truncate table ax.ECORESPRODUCTCATEGORY
		truncate table ax.REQITEMTABLE
		truncate table ax.REQSAFETYKEY
		truncate table ax.REQSAFETYLINE
		truncate table ax.INVENTITEMINVENTSETUP
		truncate table ax.INVENTITEMPURCHSETUP
		truncate table ax.UNITOFMEASURE
		truncate table ax.UNITOFMEASURECONVERSION
		truncate table ax.RETAILASSORTMENTLOOKUPCHANNELGROUP
		truncate table ax.RETAILASSORTMENTLOOKUP
		truncate table [ax].[InventColorSeason]
		truncate table [ax].[InventSeasonTable]
		truncate table [ax].[INVENTTABLEMODULE]
	END



	if @truncate_sales_trans_dumb = 1
	BEGIN
		--truncate table ax.RetailTransactionTable
		--truncate table ax.RetailTransactionSalesLineTable
		truncate table ax.INVENTTRANS
		truncate table ax.INVENTTRANSORIGIN
	END

	if @truncate_locations_and_vendors = 1
	BEGIN
		truncate table ax.inventlocation
		truncate table [ax].[RETAILCHANNELTABLE]
		truncate table [ax].[DIRPARTYTABLE]
		truncate table [ax].[VENDTABLE]
		truncate table [ax].[RETAILASSORTMENTTABLE]
		truncate table [ax].[RETAILASSORTMENTCHANNELLINE]
		truncate table [ax].[RETAILASSORTMENTPRODUCTLINE]
	END

	if @truncate_sales_trans_refresh = 1
	BEGIN
		truncate table ax.RetailTransactionTable_increment
		truncate table ax.RetailTransactionSalesLineTable_increment
		truncate table ax.INVENTSUM_Increment
		truncate table ax.INVENTTRANS_Increment
		truncate table ax.PurchLine_Increment
	END



	if @truncate_lookup_info = 1
	BEGIN
		truncate table [ax].[SeasonTable]
		truncate table [ax].[SeasonGroup]
		truncate table [ax].[ProductColorGroupLine]
		truncate table [ax].[ProductSizeGroupLine]
		truncate table [ax].[ProductStyleGroupLine]
		truncate table [ax].[ProductColorGroup]
		truncate table [ax].[ProductSizeGroup]
		truncate table [ax].[ProductStyleGroup]
		truncate table [ax].[ProductAttributes]
		truncate table [ax].[ProductAttributeValues]
	END



	IF @truncate_bom = 1
	BEGIN
		TRUNCATE TABLE ax.BillOfMaterialsHeaders
		TRUNCATE TABLE ax.BillOfMaterialsLines
		TRUNCATE TABLE ax.BillOfMaterialsVersions
	END



	IF @truncate_po_to = 1
	BEGIN
		TRUNCATE TABLE ax.InventTransferTable
		TRUNCATE TABLE ax.InventTransferLine
		--TRUNCATE TABLE ax.PurchaseOrderHeaders
		--TRUNCATE TABLE ax.PurchaseOrderLines
		TRUNCATE TABLE ax.PurchLine
	END

END

GO
