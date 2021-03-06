USE [agr5_bm_stg]
GO
/****** Object:  Table [ax].[VENDTABLE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[VENDTABLE]
GO
/****** Object:  Table [ax].[UNITOFMEASURECONVERSION]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[UNITOFMEASURECONVERSION]
GO
/****** Object:  Table [ax].[UNITOFMEASURE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[UNITOFMEASURE]
GO
/****** Object:  Table [ax].[SeasonTable]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[SeasonTable]
GO
/****** Object:  Table [ax].[SeasonGroup]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[SeasonGroup]
GO
/****** Object:  Table [ax].[RetailTransactionTable_increment]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[RetailTransactionTable_increment]
GO
/****** Object:  Table [ax].[RetailTransactionTable]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[RetailTransactionTable]
GO
/****** Object:  Table [ax].[RetailTransactionSalesLineTable_increment]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[RetailTransactionSalesLineTable_increment]
GO
/****** Object:  Table [ax].[RetailTransactionSalesLineTable]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[RetailTransactionSalesLineTable]
GO
/****** Object:  Table [ax].[RETAILCHANNELTABLE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[RETAILCHANNELTABLE]
GO
/****** Object:  Table [ax].[RETAILASSORTMENTTABLE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[RETAILASSORTMENTTABLE]
GO
/****** Object:  Table [ax].[RETAILASSORTMENTPRODUCTLINE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[RETAILASSORTMENTPRODUCTLINE]
GO
/****** Object:  Table [ax].[RETAILASSORTMENTLOOKUPCHANNELGROUP]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[RETAILASSORTMENTLOOKUPCHANNELGROUP]
GO
/****** Object:  Table [ax].[RETAILASSORTMENTLOOKUP]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[RETAILASSORTMENTLOOKUP]
GO
/****** Object:  Table [ax].[RETAILASSORTMENTCHANNELLINE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[RETAILASSORTMENTCHANNELLINE]
GO
/****** Object:  Table [ax].[REQSAFETYLINE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[REQSAFETYLINE]
GO
/****** Object:  Table [ax].[REQSAFETYKEY]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[REQSAFETYKEY]
GO
/****** Object:  Table [ax].[REQITEMTABLE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[REQITEMTABLE]
GO
/****** Object:  Table [ax].[ReleasedProductVariants]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ReleasedProductVariants]
GO
/****** Object:  Table [ax].[ReleasedProductMaster]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ReleasedProductMaster]
GO
/****** Object:  Table [ax].[ReleasedDistinctProducts]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ReleasedDistinctProducts]
GO
/****** Object:  Table [ax].[PurchLine_Increment]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[PurchLine_Increment]
GO
/****** Object:  Table [ax].[PurchLine]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[PurchLine]
GO
/****** Object:  Table [ax].[PurchaseOrderLines]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[PurchaseOrderLines]
GO
/****** Object:  Table [ax].[PurchaseOrderHeaders]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[PurchaseOrderHeaders]
GO
/****** Object:  Table [ax].[ProductStyleGroupLine]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ProductStyleGroupLine]
GO
/****** Object:  Table [ax].[ProductStyleGroup]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ProductStyleGroup]
GO
/****** Object:  Table [ax].[ProductSizeGroupLine]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ProductSizeGroupLine]
GO
/****** Object:  Table [ax].[ProductSizeGroup]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ProductSizeGroup]
GO
/****** Object:  Table [ax].[ProductMaster]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ProductMaster]
GO
/****** Object:  Table [ax].[ProductColorGroupLine]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ProductColorGroupLine]
GO
/****** Object:  Table [ax].[ProductColorGroup]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ProductColorGroup]
GO
/****** Object:  Table [ax].[ProductAttributeValues]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ProductAttributeValues]
GO
/****** Object:  Table [ax].[ProductAttributes]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ProductAttributes]
GO
/****** Object:  Table [ax].[INVENTTRANSORIGIN]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTTRANSORIGIN]
GO
/****** Object:  Table [ax].[INVENTTRANSFERTABLE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTTRANSFERTABLE]
GO
/****** Object:  Table [ax].[INVENTTRANSFERLINE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTTRANSFERLINE]
GO
/****** Object:  Table [ax].[INVENTTRANS_Increment]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTTRANS_Increment]
GO
/****** Object:  Table [ax].[INVENTTRANS]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTTRANS]
GO
/****** Object:  Table [ax].[INVENTTABLEMODULE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTTABLEMODULE]
GO
/****** Object:  Table [ax].[INVENTSUM_increment]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTSUM_increment]
GO
/****** Object:  Table [ax].[INVENTSUM]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTSUM]
GO
/****** Object:  Table [ax].[InventSeasonTable]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[InventSeasonTable]
GO
/****** Object:  Table [ax].[INVENTLOCATION]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTLOCATION]
GO
/****** Object:  Table [ax].[INVENTITEMPURCHSETUP]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTITEMPURCHSETUP]
GO
/****** Object:  Table [ax].[INVENTITEMINVENTSETUP]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTITEMINVENTSETUP]
GO
/****** Object:  Table [ax].[INVENTDIMCOMBINATIONS]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTDIMCOMBINATIONS]
GO
/****** Object:  Table [ax].[INVENTDIM]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[INVENTDIM]
GO
/****** Object:  Table [ax].[InventColorSeason]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[InventColorSeason]
GO
/****** Object:  Table [ax].[ECORESPRODUCTTRANSLATION]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ECORESPRODUCTTRANSLATION]
GO
/****** Object:  Table [ax].[ECORESPRODUCTCATEGORY]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ECORESPRODUCTCATEGORY]
GO
/****** Object:  Table [ax].[ECORESCATEGORYHIERARCHYROLE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ECORESCATEGORYHIERARCHYROLE]
GO
/****** Object:  Table [ax].[ECORESCATEGORYHIERARCHY]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ECORESCATEGORYHIERARCHY]
GO
/****** Object:  Table [ax].[ECORESCATEGORY]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[ECORESCATEGORY]
GO
/****** Object:  Table [ax].[DistinctProduct]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[DistinctProduct]
GO
/****** Object:  Table [ax].[DIRPARTYTABLE]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[DIRPARTYTABLE]
GO
/****** Object:  Table [ax].[CUSTVENDEXTERNALITEM]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[CUSTVENDEXTERNALITEM]
GO
/****** Object:  Table [ax].[BillOfMaterialsVersions]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[BillOfMaterialsVersions]
GO
/****** Object:  Table [ax].[BillOfMaterialsLines]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[BillOfMaterialsLines]
GO
/****** Object:  Table [ax].[BillOfMaterialsHeaders]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[BillOfMaterialsHeaders]
GO
/****** Object:  Table [ax].[AGRReqItemTable]    Script Date: 10.10.2017 12:17:17 ******/
DROP TABLE [ax].[AGRReqItemTable]
GO
/****** Object:  Table [ax].[AGRReqItemTable]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[AGRReqItemTable](
	[InventLocationIdReqMain] [nvarchar](50) NULL,
	[LeadTimeProduction] [int] NULL,
	[MaxSafetyKeyId] [nvarchar](50) NULL,
	[ReqGroupId] [nvarchar](50) NULL,
	[LeadTimeTransferActive] [int] NULL,
	[AuthorizationTimeFence] [int] NULL,
	[CovFieldsActive] [int] NULL,
	[TimeFenceBackRequisition] [int] NULL,
	[PmfPlanPriorityDefault] [int] NULL,
	[OnHandConsumptionStrategy] [int] NULL,
	[LeadTimeTransfer] [int] NULL,
	[CalendarDaysProduction] [int] NULL,
	[ReqPOType] [int] NULL,
	[MaxInventOnhand] [numeric](32, 16) NULL,
	[PmfPlanPriorityCurrent] [int] NULL,
	[MinSafetyPeriod] [int] NULL,
	[MinSafetyKeyId] [nvarchar](50) NULL,
	[CovInventDimId] [nvarchar](50) NULL,
	[ReqPOTypeActive] [int] NULL,
	[LeadTimePurchase] [int] NULL,
	[CovTimeFence] [int] NULL,
	[LeadTimeProductionActive] [int] NULL,
	[CapacityTimeFence] [int] NULL,
	[CalendarDaysPurchase] [int] NULL,
	[LeadTimePurchaseActive] [int] NULL,
	[OnHandActive] [int] NULL,
	[ItemId] [nvarchar](50) NULL,
	[MaxNegativeDays] [int] NULL,
	[ExplosionTimeFence] [int] NULL,
	[LockingTimeFence] [int] NULL,
	[PmfPlanPriorityDateChanged] [datetime] NULL,
	[CovRule] [int] NULL,
	[VendId] [nvarchar](50) NULL,
	[CalendarDaysTransfer] [int] NULL,
	[CovPeriod] [int] NULL,
	[MinSatisfy] [int] NULL,
	[MinInventOnhand] [numeric](32, 16) NULL,
	[TimeFenceFieldsActive] [int] NULL,
	[MaxPositiveDays] [int] NULL,
	[PmfPlanningItemId] [nvarchar](50) NULL,
	[DataAreaId] [nvarchar](50) NULL,
	[ItemCovFieldsActive] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[BillOfMaterialsHeaders]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[BillOfMaterialsHeaders](
	[ApproverPersonnelNumber] [nvarchar](50) NULL,
	[ProductGroupId] [nvarchar](50) NULL,
	[dataAreaId] [nvarchar](50) NULL,
	[IsApproved] [int] NULL,
	[ProductionSiteId] [nvarchar](50) NULL,
	[BOMName] [nvarchar](50) NULL,
	[BOMId] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[BillOfMaterialsLines]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[BillOfMaterialsLines](
	[VendorAccountNumber] [nvarchar](50) NULL,
	[ProductStyleId] [nvarchar](50) NULL,
	[WillManufacturedItemInheritShelfLifeDates] [int] NULL,
	[LineNumber] [int] NULL,
	[ItemNumber] [nvarchar](50) NULL,
	[PhysicalProductDensity] [int] NULL,
	[FlushingPrinciple] [int] NULL,
	[VariableScrapPercentage] [int] NULL,
	[QuantityRoundingUpMultiples] [int] NULL,
	[PhysicalProductHeight] [int] NULL,
	[IsConsumedAtOperationComplete] [int] NULL,
	[BOMId] [nvarchar](50) NULL,
	[ProductSizeId] [nvarchar](50) NULL,
	[WillManufacturedItemInheritBatchAttributes] [int] NULL,
	[PhysicalProductDepth] [int] NULL,
	[RoundingUpMethod] [int] NULL,
	[ConstantScrapQuantity] [int] NULL,
	[RouteOperationNumber] [int] NULL,
	[ConsumptionCalculationConstant] [int] NULL,
	[ConsumptionWarehouseId] [nvarchar](50) NULL,
	[PositionNumber] [nvarchar](50) NULL,
	[SubRouteId] [nvarchar](50) NULL,
	[WillCostCalculationIncludeLine] [int] NULL,
	[LineType] [int] NULL,
	[ProductConfigurationId] [nvarchar](50) NULL,
	[ValidToDate] [datetime] NULL,
	[CatchWeightQuantity] [int] NULL,
	[ProductUnitSymbol] [nvarchar](50) NULL,
	[IsResourceConsumptionUsed] [bit] NULL,
	[ValidFromDate] [datetime] NULL,
	[ConsumptionSiteId] [nvarchar](50) NULL,
	[SubBOMId] [nvarchar](50) NULL,
	[ProductColorId] [nvarchar](50) NULL,
	[Quantity] [int] NULL,
	[PhysicalProductWidth] [int] NULL,
	[QuantityDenominator] [int] NULL,
	[dataAreaId] [nvarchar](50) NULL,
	[ConsumptionType] [int] NULL,
	[ConsumptionCalculationMethod] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[BillOfMaterialsVersions]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[BillOfMaterialsVersions](
	[ValidFromDate] [datetime] NULL,
	[ProductConfigurationId] [nvarchar](50) NULL,
	[ManufacturedItemNumber] [nvarchar](50) NULL,
	[dataAreaId] [nvarchar](50) NULL,
	[CatchWeightSize] [numeric](32, 16) NULL,
	[IsSelectedForDesigner] [int] NULL,
	[ProductSizeId] [nvarchar](50) NULL,
	[IsActive] [int] NULL,
	[ProductColorId] [nvarchar](50) NULL,
	[ApproverPersonnelNumber] [nvarchar](50) NULL,
	[BOMId] [nvarchar](50) NULL,
	[ProductStyleId] [nvarchar](50) NULL,
	[IsApproved] [int] NULL,
	[FromCatchWeightQuantity] [numeric](32, 16) NULL,
	[ApproverId] [bigint] NULL,
	[VersionName] [nvarchar](50) NULL,
	[ProductionSiteId] [nvarchar](50) NULL,
	[ValidToDate] [datetime] NULL,
	[FromQuantity] [numeric](32, 16) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[CUSTVENDEXTERNALITEM]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[CUSTVENDEXTERNALITEM](
	[MODULETYPE] [int] NOT NULL,
	[EXTERNALITEMID] [nvarchar](20) NOT NULL,
	[CUSTVENDRELATION] [nvarchar](20) NOT NULL,
	[DESCRIPTION] [nvarchar](60) NOT NULL,
	[ABCCATEGORY] [int] NOT NULL,
	[EXTERNALITEMTXT] [nvarchar](1000) NOT NULL,
	[DATAAREAID] [nvarchar](4) NOT NULL,
	[ITEMID] [nvarchar](20) NOT NULL,
	[INVENTDIMID] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_CUSTVENDEXTERNALITEM] PRIMARY KEY CLUSTERED 
(
	[MODULETYPE] ASC,
	[CUSTVENDRELATION] ASC,
	[DATAAREAID] ASC,
	[ITEMID] ASC,
	[INVENTDIMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[DIRPARTYTABLE]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[DIRPARTYTABLE](
	[PublicRecId] [bigint] NOT NULL,
	[Language] [nvarchar](500) NULL,
	[LogisticsLocation_PrimaryAddress_LocationId] [nvarchar](500) NULL,
	[LogisticsElectronicAddress_Fax_Locator] [nvarchar](500) NULL,
	[LogisticsElectronicAddress_URL_Locator] [nvarchar](500) NULL,
	[PartyType] [bigint] NULL,
	[LogisticsElectronicAddress_Telex_Locator] [nvarchar](500) NULL,
	[KnownAs] [nvarchar](500) NULL,
	[LogisticsElectronicAddress_Phone_Locator] [nvarchar](500) NULL,
	[SearchName] [nvarchar](500) NULL,
	[PartyID] [nvarchar](500) NULL,
	[LogisticsElectronicAddress_Email_Locator] [nvarchar](500) NULL,
	[Name] [nvarchar](500) NULL,
 CONSTRAINT [PK_DIRPARTYTABLE] PRIMARY KEY CLUSTERED 
(
	[PublicRecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[DistinctProduct]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[DistinctProduct](
	[NMFCCode] [nvarchar](10) NULL,
	[ProductType] [int] NULL,
	[STCCCode] [nvarchar](10) NULL,
	[StorageDimensionGroupName] [nvarchar](50) NULL,
	[ProductNumber] [nvarchar](50) NULL,
	[IsCatchWeightProduct] [int] NULL,
	[ProductDescription] [nvarchar](500) NULL,
	[RetailProductCategoryName] [nvarchar](50) NULL,
	[TrackingDimensionGroupName] [nvarchar](50) NULL,
	[ProductSearchName] [nvarchar](100) NULL,
	[ProductName] [nvarchar](100) NULL,
	[HarmonizedSystemCode] [nvarchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ECORESCATEGORY]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ECORESCATEGORY](
	[NestedSetRight] [bigint] NULL,
	[CategoryHierarchy] [bigint] NULL,
	[EcoResCategory1_Name] [nvarchar](500) NULL,
	[DefaultThreshold_PSN] [numeric](32, 16) NULL,
	[NestedSetLeft] [bigint] NULL,
	[IsTangible] [int] NULL,
	[ParentCategory] [bigint] NULL,
	[IsActive] [int] NULL,
	[IsCategoryAttributesInherited] [int] NULL,
	[EcoResCategoryHierarchy_Name] [nvarchar](500) NULL,
	[SharedCategory_CategoryId] [nvarchar](500) NULL,
	[InstanceRelationType] [bigint] NULL,
	[AxRecId] [bigint] NULL,
	[Code] [nvarchar](500) NULL,
	[Level] [bigint] NULL,
	[Name] [nvarchar](500) NULL,
	[ChangeStatus] [int] NULL,
	[PKWiUCode] [nvarchar](500) NULL,
	[EcoResCategoryHierarchy1_Name] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ECORESCATEGORYHIERARCHY]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ECORESCATEGORYHIERARCHY](
	[AxRecId] [bigint] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[HierarchyModifier] [bigint] NULL,
 CONSTRAINT [PK_ECORESCATEGORYHIERARCHY] PRIMARY KEY CLUSTERED 
(
	[AxRecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ECORESCATEGORYHIERARCHYROLE]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ECORESCATEGORYHIERARCHYROLE](
	[EcoResCategoryHierarchy_Name] [nvarchar](255) NOT NULL,
	[NamedCategoryHierarchyRole] [nvarchar](255) NOT NULL,
	[NamedCategoryHierarchyRoleAsInt] [int] NOT NULL,
	[CategoryHierarchy] [bigint] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ECORESPRODUCTCATEGORY]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ECORESPRODUCTCATEGORY](
	[EcoResProduct_DisplayProductNumber] [nvarchar](500) NULL,
	[CategoryHierarchy] [bigint] NULL,
	[EcoResCategory_Name] [nvarchar](500) NULL,
	[Product] [bigint] NULL,
	[EcoResCategoryHierarchy_Name] [nvarchar](500) NULL,
	[Category] [bigint] NULL,
	[ModifiedDateAndTime] [datetime] NULL,
	[EcoResCategoryHierarchy1_Name] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ECORESPRODUCTTRANSLATION]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ECORESPRODUCTTRANSLATION](
	[LanguageId] [nvarchar](7) NOT NULL,
	[Product] [bigint] NOT NULL,
	[DisplayNumber] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_EcoResProductTranslation_1] PRIMARY KEY CLUSTERED 
(
	[Product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[InventColorSeason]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[InventColorSeason](
	[ItemId] [nvarchar](50) NULL,
	[SeasonCode] [nvarchar](50) NULL,
	[ColorId] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTDIM]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTDIM](
	[INVENTLOCATIONID] [nvarchar](10) NOT NULL,
	[INVENTSTATUSID] [nvarchar](10) NOT NULL,
	[LICENSEPLATEID] [nvarchar](25) NOT NULL,
	[INVENTSTYLEID] [nvarchar](10) NOT NULL,
	[INVENTSERIALID] [nvarchar](20) NOT NULL,
	[INVENTCOLORID] [nvarchar](10) NOT NULL,
	[INVENTBATCHID] [nvarchar](20) NOT NULL,
	[INVENTDIMID] [nvarchar](20) NOT NULL,
	[INVENTPROFILEID_RU] [nvarchar](10) NOT NULL,
	[WMSLOCATIONID] [nvarchar](10) NOT NULL,
	[CONFIGID] [nvarchar](10) NOT NULL,
	[SHA1HASHHEX] [nvarchar](40) NOT NULL,
	[INVENTSITEID] [nvarchar](10) NOT NULL,
	[DATAAREAID] [nvarchar](4) NOT NULL,
	[INVENTSIZEID] [nvarchar](10) NOT NULL,
	[WMSPALLETID] [nvarchar](18) NOT NULL,
	[INVENTGTDID_RU] [nvarchar](30) NOT NULL,
	[INVENTOWNERID_RU] [nvarchar](20) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTDIMCOMBINATIONS]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTDIMCOMBINATIONS](
	[ItemId] [nvarchar](20) NULL,
	[InventDimId] [nvarchar](100) NULL,
	[DisplayProductNumber] [nvarchar](100) NULL,
	[RetailVariantId] [nvarchar](100) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTITEMINVENTSETUP]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTITEMINVENTSETUP](
	[Stopped] [int] NULL,
	[MultipleQty] [numeric](32, 16) NULL,
	[HighestQty] [numeric](32, 16) NULL,
	[InventDimIdDefault] [nvarchar](50) NOT NULL,
	[StandardQty] [numeric](32, 16) NULL,
	[ATPBackwardDemandTimeFence] [int] NULL,
	[LowestQty] [numeric](32, 16) NULL,
	[CalendarDays] [int] NULL,
	[MandatoryInventSite] [int] NULL,
	[DataAreaId] [nvarchar](500) NULL,
	[ATPBackwardSupplyTimeFence] [int] NULL,
	[MandatoryInventLocation] [int] NULL,
	[ATPTimeFence] [int] NULL,
	[ItemId] [nvarchar](50) NOT NULL,
	[LeadTime] [int] NULL,
	[ATPApplyDemandTimeFence] [int] NULL,
	[Override] [int] NULL,
	[DeliveryDateControlType] [int] NULL,
	[InventDimId] [nvarchar](50) NOT NULL,
	[ATPInclPlannedOrders] [nvarchar](500) NULL,
	[ATPApplySupplyTimeFence] [int] NULL,
 CONSTRAINT [PK_INVENTITEMINVENTSETUP] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC,
	[InventDimId] ASC,
	[InventDimIdDefault] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTITEMPURCHSETUP]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTITEMPURCHSETUP](
	[Stopped] [int] NULL,
	[MultipleQty] [numeric](32, 16) NULL,
	[HighestQty] [numeric](32, 16) NULL,
	[InventDimIdDefault] [nvarchar](500) NULL,
	[StandardQty] [numeric](32, 16) NULL,
	[LowestQty] [numeric](32, 16) NULL,
	[CalendarDays] [int] NULL,
	[MandatoryInventSite] [int] NULL,
	[DataAreaId] [nvarchar](500) NULL,
	[MandatoryInventLocation] [int] NULL,
	[ItemId] [nvarchar](50) NOT NULL,
	[LeadTime] [int] NULL,
	[Override] [int] NULL,
	[InventDimId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_INVENTITEMPURCHSETUP_1] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC,
	[InventDimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTLOCATION]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTLOCATION](
	[Manual] [int] NULL,
	[InventLocationIdTransit] [nvarchar](500) NULL,
	[PrintBOLBeforeShipConfirm] [int] NULL,
	[BranchNumber] [nvarchar](500) NULL,
	[DefaultKanbanFinishedGoodsLocation] [nvarchar](500) NULL,
	[AllowLaborStandards] [int] NULL,
	[CustAccount_HU] [nvarchar](500) NULL,
	[WMSRackNameActive] [int] NULL,
	[DefaultShipMaintenanceLoc] [nvarchar](500) NULL,
	[WMSLocationIdDefaultReceipt] [nvarchar](500) NULL,
	[InventProfileId_RU] [nvarchar](500) NULL,
	[WMSLevelNameActive] [int] NULL,
	[DefaultProductionInputLocation] [nvarchar](500) NULL,
	[ReserveAtLoadPost] [int] NULL,
	[InventLocationIdReqMain] [nvarchar](500) NULL,
	[WMSAisleNameActive] [int] NULL,
	[VendAccount] [nvarchar](500) NULL,
	[EmptyPalletLocation] [nvarchar](500) NULL,
	[WarehouseAutoReleaseReservation] [int] NULL,
	[DefaultProductionFinishGoodsLocation] [nvarchar](500) NULL,
	[UniqueCheckDigits] [int] NULL,
	[InventLocationIdQuarantine] [nvarchar](500) NULL,
	[VendAccountCustom_RU] [nvarchar](500) NULL,
	[DecrementLoadLine] [int] NULL,
	[WMSRackFormat] [nvarchar](500) NULL,
	[InventLocationType] [int] NULL,
	[DefaultStatusId] [nvarchar](500) NULL,
	[WMSLocationIdGoodsInRoute_RU] [nvarchar](500) NULL,
	[NumberSequenceGroup_RU] [nvarchar](500) NULL,
	[MaxPickingRouteVolume] [numeric](32, 16) NULL,
	[RBODefaultWMSPalletId] [nvarchar](500) NULL,
	[ReqRefill] [int] NULL,
	[UseWMSOrders] [int] NULL,
	[ReqCalendarId] [nvarchar](500) NULL,
	[MaxPickingRouteTime] [int] NULL,
	[InventSiteId] [nvarchar](500) NULL,
	[InventLocationId] [nvarchar](500) NULL,
	[FSHStore] [int] NULL,
	[WMSlocationIdDefaultIssue] [nvarchar](500) NULL,
	[InventCountingGroup_BR] [int] NULL,
	[Name] [nvarchar](500) NULL,
	[RemoveInventBlockingOnStatusChange] [int] NULL,
	[AllowMarkingReservationRemoval] [int] NULL,
	[InventProfileType_RU] [int] NULL,
	[RBODefaultWMSLocationId] [nvarchar](500) NULL,
	[InventLocationIdGoodsInRoute_RU] [nvarchar](500) NULL,
	[ConsolidateShipAtRTW] [int] NULL,
	[RetailInventNegPhysical] [int] NULL,
	[WMSPositionFormat] [nvarchar](500) NULL,
	[WHSEnabled] [int] NULL,
	[RetailWMSPalletIdDefaultReturn] [nvarchar](500) NULL,
	[InventLocationLevel] [int] NULL,
	[WMSLevelFormat] [nvarchar](500) NULL,
	[WHSRawMaterialPolicy] [int] NULL,
	[ActivityType_RU] [nvarchar](500) NULL,
	[RetailWMSLocationIdDefaultReturn] [nvarchar](500) NULL,
	[RBODefaultInventProfileId_RU] [nvarchar](500) NULL,
	[CycleCountAllowPalletMove] [int] NULL,
	[DataAreaId] [nvarchar](500) NULL,
	[WMSPositionNameActive] [int] NULL,
	[ProdReserveOnlyWhse] [int] NULL,
	[CustAccount_BR] [nvarchar](500) NULL,
	[RetailInventNegFinancial] [int] NULL,
	[RetailWeightEx1] [numeric](32, 16) NULL,
	[DefaultReturnCreditOnlyLocation] [nvarchar](500) NULL,
	[PickingLineTime] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[InventSeasonTable]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[InventSeasonTable](
	[IsDefault] [int] NULL,
	[SeasonCode] [nvarchar](500) NULL,
	[DataAreaId] [nvarchar](500) NULL,
	[ItemId] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTSUM]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTSUM](
	[RecId] [bigint] NOT NULL,
	[DataAreaId] [nvarchar](4) NOT NULL,
	[Partition] [bigint] NOT NULL,
	[ItemId] [nvarchar](20) NOT NULL,
	[PostedQty] [numeric](32, 16) NOT NULL,
	[PostedValue] [numeric](32, 16) NOT NULL,
	[Deducted] [numeric](32, 16) NOT NULL,
	[Received] [numeric](32, 16) NOT NULL,
	[ReservPhysical] [numeric](32, 16) NOT NULL,
	[ReservOrdered] [numeric](32, 16) NOT NULL,
	[OnOrder] [numeric](32, 16) NOT NULL,
	[Ordered] [numeric](32, 16) NOT NULL,
	[InventDimId] [nvarchar](20) NOT NULL,
	[Closed] [int] NOT NULL,
	[AvailPhysical] [numeric](32, 16) NOT NULL,
	[PhysicalValue] [numeric](32, 16) NOT NULL,
	[PhysicalInvent] [numeric](32, 16) NOT NULL,
	[ClosedQty] [int] NOT NULL,
	[ModifiedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_InventSum] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTSUM_increment]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTSUM_increment](
	[RecId] [bigint] NOT NULL,
	[DataAreaId] [nvarchar](4) NOT NULL,
	[Partition] [bigint] NOT NULL,
	[ItemId] [nvarchar](20) NOT NULL,
	[PostedQty] [numeric](32, 16) NOT NULL,
	[PostedValue] [numeric](32, 16) NOT NULL,
	[Deducted] [numeric](32, 16) NOT NULL,
	[Received] [numeric](32, 16) NOT NULL,
	[ReservPhysical] [numeric](32, 16) NOT NULL,
	[ReservOrdered] [numeric](32, 16) NOT NULL,
	[OnOrder] [numeric](32, 16) NOT NULL,
	[Ordered] [numeric](32, 16) NOT NULL,
	[InventDimId] [nvarchar](20) NOT NULL,
	[Closed] [int] NOT NULL,
	[AvailPhysical] [numeric](32, 16) NOT NULL,
	[PhysicalValue] [numeric](32, 16) NOT NULL,
	[PhysicalInvent] [numeric](32, 16) NOT NULL,
	[ClosedQty] [int] NOT NULL,
	[ModifiedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_InventSum_increment] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTTABLEMODULE]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTTABLEMODULE](
	[AllocateMarkup] [int] NULL,
	[MultiLineDisc] [nvarchar](500) NULL,
	[TaxWithholdCalculate_TH] [int] NULL,
	[TaxGSTReliefCategory_MY_ReliefCategoryId] [nvarchar](500) NULL,
	[PDSPricingPrecision] [int] NULL,
	[PriceDate] [datetime] NULL,
	[UnitId] [nvarchar](500) NULL,
	[MarkupGroupId] [nvarchar](500) NULL,
	[SuppItemGroupId] [nvarchar](500) NULL,
	[PriceUnit] [numeric](32, 16) NULL,
	[LineDisc] [nvarchar](500) NULL,
	[Markup] [numeric](32, 16) NULL,
	[MaximumRetailPrice_IN] [numeric](32, 16) NULL,
	[PriceSecCur_RU] [numeric](32, 16) NULL,
	[MarkupSecCur_RU] [numeric](32, 16) NULL,
	[TaxItemGroupId] [nvarchar](500) NULL,
	[EndDisc] [int] NULL,
	[PriceQty] [numeric](32, 16) NULL,
	[Price] [numeric](32, 16) NULL,
	[UnderDeliveryPct] [numeric](32, 16) NULL,
	[TaxWithholdItemGroupHeading_TH_TaxWithholdItemGroup] [nvarchar](500) NULL,
	[ItemId] [nvarchar](50) NOT NULL,
	[OverDeliveryPct] [numeric](32, 16) NULL,
	[DataAreaId] [nvarchar](500) NULL,
	[ModuleType] [int] NOT NULL,
	[InterCompanyBlocked] [int] NULL,
 CONSTRAINT [PK_InventTableModule] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC,
	[ModuleType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTTRANS]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTTRANS](
	[RecId] [bigint] NOT NULL,
	[DataAreaId] [nvarchar](4) NOT NULL,
	[Partition] [bigint] NOT NULL,
	[ItemId] [nvarchar](20) NOT NULL,
	[StatusIssue] [int] NOT NULL,
	[DatePhysical] [datetime] NOT NULL,
	[Qty] [numeric](32, 16) NOT NULL,
	[DateFinancial] [datetime] NOT NULL,
	[InventDimId] [nvarchar](20) NOT NULL,
	[InvoiceId] [nvarchar](20) NOT NULL,
	[InventTransOrigin] [bigint] NOT NULL,
	[ModifiedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_InventTrans_2] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTTRANS_Increment]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTTRANS_Increment](
	[RecId] [bigint] NOT NULL,
	[DataAreaId] [nvarchar](4) NOT NULL,
	[Partition] [bigint] NOT NULL,
	[ItemId] [nvarchar](20) NOT NULL,
	[StatusIssue] [int] NOT NULL,
	[DatePhysical] [datetime] NOT NULL,
	[Qty] [numeric](32, 16) NOT NULL,
	[DateFinancial] [datetime] NOT NULL,
	[InventDimId] [nvarchar](20) NOT NULL,
	[InvoiceId] [nvarchar](20) NOT NULL,
	[InventTransOrigin] [bigint] NOT NULL,
	[ModifiedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_InventTrans_Increment] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTTRANSFERLINE]    Script Date: 10.10.2017 12:17:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTTRANSFERLINE](
	[AmountValue] [numeric](32, 6) NOT NULL,
	[AtpApplyDemandTimeFence] [int] NOT NULL,
	[AtpApplySupplyTimeFence] [int] NOT NULL,
	[AtpBackwardDemandTimeFence] [int] NOT NULL,
	[AtpBackwardSupplyTimeFence] [int] NOT NULL,
	[AtpInclPlannedOrders] [bit] NOT NULL,
	[AtpTimeFence] [int] NOT NULL,
	[AutoReservation] [int] NOT NULL,
	[CombinedTransferOrderDeliv] [bigint] NOT NULL,
	[DeliveryDateControlType] [int] NOT NULL,
	[HhtHandheldUserId] [nvarchar](25) NOT NULL,
	[HhtTransDate] [datetime] NOT NULL,
	[HhTTransTime] [int] NOT NULL,
	[IntraStatFullFillmentDate_HU] [datetime] NOT NULL,
	[IntrastatSpecMoxe_CZ] [nvarchar](2) NOT NULL,
	[InventDimId] [nvarchar](20) NOT NULL,
	[InventDimIdTo_RU] [nvarchar](20) NOT NULL,
	[InventTransId] [nvarchar](20) NOT NULL,
	[InventTransIdReceive] [nvarchar](20) NOT NULL,
	[InventTransIdScrap] [nvarchar](20) NOT NULL,
	[InventTransIdTransitFrom] [nvarchar](20) NOT NULL,
	[InventTransIdTransitTo] [nvarchar](20) NOT NULL,
	[ItemId] [nvarchar](20) NOT NULL,
	[LineAmount_RU] [numeric](32, 6) NOT NULL,
	[LineNum] [numeric](32, 16) NOT NULL,
	[OrigCountryRegionId] [nvarchar](10) NOT NULL,
	[OrigCountId] [nvarchar](10) NOT NULL,
	[OrigStateId] [nvarchar](10) NOT NULL,
	[OverDeliveryPct] [numeric](32, 6) NOT NULL,
	[PdsCWQtyReceived] [numeric](32, 6) NOT NULL,
	[PdsCWQtyReceiveNow] [numeric](32, 6) NOT NULL,
	[PdsCWQtyRemainReceive] [numeric](32, 6) NOT NULL,
	[PdsCWQtyRemainShip] [numeric](32, 6) NOT NULL,
	[PdsCWQtyScrapped] [numeric](32, 6) NOT NULL,
	[PdsCWQtyShipNow] [numeric](32, 6) NOT NULL,
	[PdsCWQtyShipped] [numeric](32, 6) NOT NULL,
	[PdsCWQtyTransfer] [numeric](32, 6) NOT NULL,
	[PdsOverrideFEFO] [int] NOT NULL,
	[Port] [nvarchar](10) NOT NULL,
	[Price_RU] [numeric](32, 6) NOT NULL,
	[PriceUnit_RU] [numeric](32, 6) NOT NULL,
	[QtyReceived] [numeric](32, 6) NOT NULL,
	[QtyReceivedNow] [numeric](32, 6) NOT NULL,
	[QtyRemain] [numeric](32, 6) NOT NULL,
	[QtyRemainShip] [numeric](32, 6) NOT NULL,
	[QtyScrapped] [numeric](32, 6) NOT NULL,
	[QtyShipNow] [numeric](32, 6) NOT NULL,
	[QtyShipped] [numeric](32, 6) NOT NULL,
	[QtyTransfer] [numeric](32, 6) NOT NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[RemainStatus] [int] NOT NULL,
	[RetailAreaId] [nvarchar](30) NOT NULL,
	[RetailInfocodeIdEx2] [nvarchar](10) NOT NULL,
	[RetailInformationSubcodeIdEx2] [nvarchar](10) NOT NULL,
	[RetailReplenishRefRecId] [bigint] NOT NULL,
	[RetailReplenishRefTableId] [int] NOT NULL,
	[ShipDate] [datetime] NOT NULL,
	[StatisticalValue] [numeric](32, 6) NOT NULL,
	[StatProcId] [nvarchar](10) NOT NULL,
	[TransactionCodeId] [nvarchar](10) NOT NULL,
	[TransferId] [nvarchar](20) NOT NULL,
	[Transport] [nvarchar](10) NOT NULL,
	[UnderDeliveryPct] [numeric](32, 6) NOT NULL,
	[UnitId] [nvarchar](10) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[DataAreaId] [nvarchar](4) NOT NULL,
	[RecVersion] [int] NOT NULL,
	[Partition] [bigint] NOT NULL,
	[RecId] [bigint] NOT NULL,
	[IntrastatCommodity] [bigint] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTTRANSFERTABLE]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTTRANSFERTABLE](
	[AtpApplyDemandTimeFence] [int] NOT NULL,
	[AtpApplySupplyTimeFence] [int] NOT NULL,
	[AtpBackwardDemandTimeFence] [int] NOT NULL,
	[AtpBackwardSupplyTimeFence] [int] NOT NULL,
	[AtpInclPlannedOrders] [bit] NOT NULL,
	[AtpTimeFence] [int] NOT NULL,
	[AutoReservation] [int] NOT NULL,
	[CargoDescription_RU] [nvarchar](60) NOT NULL,
	[CargoPacking_RU] [nvarchar](60) NOT NULL,
	[CarrierCode_RU] [nvarchar](20) NOT NULL,
	[CarrierType_RU] [int] NOT NULL,
	[CurrencyCode_RU] [nvarchar](3) NOT NULL,
	[DeliveryDate_RU] [datetime] NOT NULL,
	[DeliveryDateControlType] [int] NOT NULL,
	[DlvModelId] [nvarchar](10) NOT NULL,
	[DlvTermId] [nvarchar](10) NOT NULL,
	[DriverContact_RU] [nvarchar](60) NOT NULL,
	[DriverName_RU] [nvarchar](60) NOT NULL,
	[DriverLicenseNum_RU] [nvarchar](20) NOT NULL,
	[FreightSlipType] [int] NOT NULL,
	[FreightZoneId] [nvarchar](10) NOT NULL,
	[FromAddressName] [nvarchar](100) NOT NULL,
	[FromContactPerson] [bigint] NOT NULL,
	[FromPostalAddress] [bigint] NOT NULL,
	[IntrastatFulfillmentDate_HU] [datetime] NOT NULL,
	[IntrastatSpecMove_CZ] [nvarchar](2) NOT NULL,
	[InventLocationIdFrom] [nvarchar](10) NOT NULL,
	[InventLocationIdTo] [nvarchar](10) NOT NULL,
	[InventLocationIdTransit] [nvarchar](10) NOT NULL,
	[InventProfileId_RU] [nvarchar](10) NOT NULL,
	[InventProfileIdTo_RU] [nvarchar](10) NOT NULL,
	[InventProfileType_RU] [int] NOT NULL,
	[InventProfileUseRelated_RU] [int] NOT NULL,
	[LadingPostalAddress_RU] [bigint] NOT NULL,
	[LicenseCardNum_RU] [nvarchar](20) NOT NULL,
	[LicenseCardRegNum_RU] [nvarchar](10) NOT NULL,
	[LicenseCardSeries_RU] [nvarchar](20) NOT NULL,
	[LicenseCardType_RU] [int] NOT NULL,
	[PartyAccountNum_RU] [nvarchar](20) NOT NULL,
	[PartyAgreementHeaderExt_RU] [bigint] NOT NULL,
	[PdsOverrideFEFO] [int] NOT NULL,
	[PortId] [nvarchar](10) NOT NULL,
	[PriceGroupId_RU] [nvarchar](10) NOT NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[RetailReplenishRefRecId] [bigint] NOT NULL,
	[RetailReplenishRefTableId] [int] NOT NULL,
	[RetailRetailStatusType] [int] NOT NULL,
	[ShipDate] [datetime] NOT NULL,
	[StatProcId] [nvarchar](10) NOT NULL,
	[ToAddressName] [nvarchar](100) NOT NULL,
	[ToContactPersion] [bigint] NOT NULL,
	[ToPostalAddress] [bigint] NOT NULL,
	[TransactionCode] [nvarchar](10) NOT NULL,
	[TransferId] [nvarchar](20) NOT NULL,
	[TransferStatus] [int] NOT NULL,
	[TransferType_IN] [int] NOT NULL,
	[TransferType_RU] [int] NOT NULL,
	[Transport] [nvarchar](10) NOT NULL,
	[TransportationDocument] [bigint] NOT NULL,
	[TransportationPayer_RU] [nvarchar](20) NOT NULL,
	[TransportationPayerType_RU] [int] NOT NULL,
	[TransportationType_RU] [nvarchar](10) NOT NULL,
	[TransportInvoiceType_RU] [int] NOT NULL,
	[TrPackingSlipAutoNumbering_LT] [int] NOT NULL,
	[UnladingPostalAddress_RU] [bigint] NOT NULL,
	[VehicleModel_RU] [nvarchar](10) NOT NULL,
	[VehiclePlateNum_RU] [nvarchar](20) NOT NULL,
	[WaybillNum_RU] [nvarchar](20) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[DataAreaId] [nvarchar](4) NOT NULL,
	[RecVersion] [int] NOT NULL,
	[Partition] [bigint] NOT NULL,
	[RecId] [bigint] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[INVENTTRANSORIGIN]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[INVENTTRANSORIGIN](
	[RecId] [bigint] NOT NULL,
	[DataAreaId] [nvarchar](4) NOT NULL,
	[Partition] [bigint] NOT NULL,
	[InventTransId] [nvarchar](20) NOT NULL,
	[ReferenceCategory] [int] NOT NULL,
	[ReferenceId] [nvarchar](20) NOT NULL,
	[ItemId] [nvarchar](20) NOT NULL,
	[ItemInventDimId] [nvarchar](20) NOT NULL,
	[Party] [bigint] NOT NULL,
	[RecVersion] [int] NOT NULL,
 CONSTRAINT [PK_InventTransOrigin_1] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ProductAttributes]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ProductAttributes](
	[DefaultCurrencyCode] [nvarchar](500) NULL,
	[DefaultBooleanValue] [int] NULL,
	[DefaultCurrencyValue] [numeric](32, 16) NULL,
	[AttributeHelpText] [nvarchar](500) NULL,
	[AttributeDescription] [nvarchar](500) NULL,
	[DefaultDecimalValue] [numeric](32, 16) NULL,
	[DefaultIntegerValue] [int] NULL,
	[AttributeName] [nvarchar](500) NULL,
	[FriendlyAttributeName] [nvarchar](500) NULL,
	[ProductAttributeTypeName] [nvarchar](500) NULL,
	[DefaultTextValue] [nvarchar](500) NULL,
	[DefaultDateTimeValue] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ProductAttributeValues]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ProductAttributeValues](
	[IntegerValue] [int] NULL,
	[CurrencyCode] [nvarchar](500) NULL,
	[ProductNumber] [nvarchar](500) NULL,
	[DateTimeValue] [datetime] NULL,
	[DecimalValue] [numeric](32, 16) NULL,
	[AttributeName] [nvarchar](500) NULL,
	[BooleanValue] [int] NULL,
	[CurrencyValue] [numeric](32, 16) NULL,
	[TextValue] [nvarchar](500) NULL,
	[AttributeTypeName] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ProductColorGroup]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ProductColorGroup](
	[GroupId] [nvarchar](10) NULL,
	[GroupDescription] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ProductColorGroupLine]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ProductColorGroupLine](
	[ProductColorGroupId] [nvarchar](50) NULL,
	[ProductColorId] [nvarchar](50) NULL,
	[ColorName] [nvarchar](50) NULL,
	[ColorDescription] [nvarchar](255) NULL,
	[DisplayOrder] [numeric](32, 16) NULL,
	[BarcodeNumber] [nvarchar](100) NULL,
	[ReplenishmentWeight] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ProductMaster]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ProductMaster](
	[AreIdenticalConfigurationsAllowed] [int] NULL,
	[HarmonizedSystemCode] [nvarchar](10) NULL,
	[IsAutomaticVariantGenerationEnabled] [int] NULL,
	[IsCatchWeightProduct] [int] NULL,
	[IsProductKit] [int] NULL,
	[IsProductVariantUnitConversionEnabled] [int] NULL,
	[KPMInstructionGroupId] [nvarchar](10) NULL,
	[KRFColorRatioCurve] [nvarchar](20) NULL,
	[KRFSizeRatioCurve] [nvarchar](20) NULL,
	[KRFStyleRatioCurve] [nvarchar](20) NULL,
	[KRFUseRatioCurves] [int] NULL,
	[NMFCCode] [nvarchar](10) NULL,
	[ProductColorGroupId] [nvarchar](10) NULL,
	[ProductDescription] [nvarchar](500) NULL,
	[ProductDimensionGroupName] [nvarchar](50) NULL,
	[ProductName] [nvarchar](100) NULL,
	[ProductNumber] [nvarchar](50) NULL,
	[ProductSearchName] [nvarchar](100) NULL,
	[ProductSizeGroupId] [nvarchar](10) NULL,
	[ProductStyleGroupId] [nvarchar](10) NULL,
	[VariantConfigurationTechnology] [int] NULL,
	[RetailProductCategoryName] [nvarchar](50) NULL,
	[ProductType] [int] NULL,
	[STCCCode] [nvarchar](10) NULL,
	[TrackingDimensionGroupName] [nvarchar](50) NULL,
	[StorageDimensionGroupName] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ProductSizeGroup]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ProductSizeGroup](
	[GroupId] [nvarchar](10) NULL,
	[GroupDescription] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ProductSizeGroupLine]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ProductSizeGroupLine](
	[ProductSizeGroupId] [nvarchar](50) NULL,
	[ProductSizeId] [nvarchar](50) NULL,
	[SizeName] [nvarchar](50) NULL,
	[SizeDescription] [nvarchar](255) NULL,
	[DisplayOrder] [numeric](32, 16) NULL,
	[BarcodeNumber] [nvarchar](100) NULL,
	[ReplenishmentWeight] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ProductStyleGroup]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ProductStyleGroup](
	[GroupId] [nvarchar](10) NULL,
	[GroupDescription] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ProductStyleGroupLine]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ProductStyleGroupLine](
	[ProductStyleGroupId] [nvarchar](50) NULL,
	[ProductStyleId] [nvarchar](50) NULL,
	[StyleName] [nvarchar](50) NULL,
	[StyleDescription] [nvarchar](255) NULL,
	[DisplayOrder] [numeric](32, 16) NULL,
	[BarcodeNumber] [nvarchar](100) NULL,
	[ReplenishmentWeight] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[PurchaseOrderHeaders]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[PurchaseOrderHeaders](
	[ExpectedStoreAvailableSalesDate] [datetime] NULL,
	[VendorInvoiceDeclarationId] [nvarchar](50) NULL,
	[DeliveryModeId] [nvarchar](50) NULL,
	[OrderVendorAccountNumber] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[TransportationModeId] [nvarchar](50) NULL,
	[IsChangeManagementActive] [int] NULL,
	[AccountingDistributionTemplateName] [nvarchar](50) NULL,
	[DeliveryAddressDescription] [nvarchar](50) NULL,
	[VendorTransactionSettlementType] [int] NULL,
	[DeliveryCityInKana] [nvarchar](50) NULL,
	[DeliveryStreetInKana] [nvarchar](50) NULL,
	[ReasonComment] [nvarchar](50) NULL,
	[NumberSequenceGroupId] [nvarchar](50) NULL,
	[TransportationTemplateId] [nvarchar](50) NULL,
	[AccountingDate] [datetime] NULL,
	[CashDiscountPercentage] [numeric](32, 16) NULL,
	[PurchaseOrderName] [nvarchar](50) NULL,
	[RequestedDeliveryDate] [datetime] NULL,
	[DeliveryAddressCountryRegionId] [nvarchar](50) NULL,
	[DeliveryAddressLatitude] [numeric](32, 16) NULL,
	[MultilineDiscountVendorGroupCode] [nvarchar](50) NULL,
	[DeliveryAddressCity] [nvarchar](50) NULL,
	[ConfirmedDeliveryDate] [datetime] NULL,
	[PurchaseRebateVendorGroupId] [nvarchar](50) NULL,
	[ChargeVendorGroupId] [nvarchar](50) NULL,
	[RequesterPersonnelNumber] [nvarchar](50) NULL,
	[ProjectId] [nvarchar](50) NULL,
	[ShippingCarrierId] [nvarchar](50) NULL,
	[TotalDiscountPercentage] [numeric](32, 16) NULL,
	[PriceVendorGroupCode] [nvarchar](50) NULL,
	[DeliveryAddressDistrictName] [nvarchar](50) NULL,
	[DeliveryAddressCountyId] [nvarchar](50) NULL,
	[DeliveryAddressZipCode] [nvarchar](50) NULL,
	[IsConsolidatedInvoiceTarget] [int] NULL,
	[ConfirmingPurchaseOrderCode] [nvarchar](50) NULL,
	[DataAreaId] [nvarchar](50) NULL,
	[LanguageId] [nvarchar](50) NULL,
	[ReasonCode] [nvarchar](50) NULL,
	[DeliveryAddressDunsNumber] [nvarchar](50) NULL,
	[DeliveryTermsId] [nvarchar](50) NULL,
	[BankDocumentType] [int] NULL,
	[ExpectedStoreReceiptDate] [datetime] NULL,
	[PurchaseOrderNumber] [nvarchar](50) NULL,
	[DeliveryAddressName] [nvarchar](50) NULL,
	[ReplenishmentServiceCategoryId] [nvarchar](50) NULL,
	[PurchaseOrderPoolId] [nvarchar](50) NULL,
	[DeliveryAddressStreetNumber] [nvarchar](50) NULL,
	[ExpectedCrossDockingDate] [datetime] NULL,
	[IsDeliveryAddressPrivate] [int] NULL,
	[TaxExemptNumber] [nvarchar](50) NULL,
	[BuyerGroupId] [nvarchar](50) NULL,
	[CashDiscountCode] [nvarchar](50) NULL,
	[PaymentScheduleName] [nvarchar](50) NULL,
	[IntrastatTransactionCode] [nvarchar](50) NULL,
	[URL] [nvarchar](50) NULL,
	[ConfirmingPurchaseOrderCodeLanguageId] [nvarchar](50) NULL,
	[CurrencyCode] [nvarchar](50) NULL,
	[InvoiceType] [int] NULL,
	[ArePricesIncludingSalesTax] [int] NULL,
	[DeliveryAddressLocationId] [nvarchar](50) NULL,
	[IsDeliveredDirectly] [int] NULL,
	[IntrastatStatisticsProcedureCode] [nvarchar](50) NULL,
	[InvoiceVendorAccountNumber] [nvarchar](50) NULL,
	[DeliveryAddressStreet] [nvarchar](50) NULL,
	[VendorOrderReference] [nvarchar](50) NULL,
	[ReplenishmentWarehouseId] [nvarchar](50) NULL,
	[FixedDueDate] [datetime] NULL,
	[SalesTaxGroupCode] [nvarchar](50) NULL,
	[IsDeliveryAddressOrderSpecific] [int] NULL,
	[VendorPostingProfileId] [nvarchar](50) NULL,
	[VendorPaymentMethodSpecificationName] [nvarchar](50) NULL,
	[ShippingCarrierServiceGroupId] [nvarchar](50) NULL,
	[ContactPersonId] [nvarchar](50) NULL,
	[DefaultReceivingWarehouseId] [nvarchar](50) NULL,
	[EUSalesListCode] [int] NULL,
	[ImportDeclarationNumber] [nvarchar](50) NULL,
	[PurchaseOrderStatus] [int] NULL,
	[PaymentTermsName] [nvarchar](50) NULL,
	[DeliveryAddressLongitude] [numeric](32, 16) NULL,
	[ShippingCarrierServiceId] [nvarchar](50) NULL,
	[DefaultLedgerDimensionDisplayValue] [nvarchar](50) NULL,
	[DeliveryAddressTimeZone] [int] NULL,
	[AttentionInformation] [nvarchar](50) NULL,
	[DeliveryAddressStateId] [nvarchar](50) NULL,
	[DeliveryBuildingCompliment] [nvarchar](50) NULL,
	[IntrastatTransportModeCode] [nvarchar](50) NULL,
	[DeliveryAddressPostBox] [nvarchar](50) NULL,
	[IsOneTimeVendor] [int] NULL,
	[IntrastatPortId] [nvarchar](50) NULL,
	[OrdererPersonnelNumber] [nvarchar](50) NULL,
	[VendorPaymentMethodName] [nvarchar](50) NULL,
	[DefaultReceivingSiteId] [nvarchar](50) NULL,
	[LineDiscountVendorGroupCode] [nvarchar](50) NULL,
	[TransportationRoutePlanId] [nvarchar](50) NULL,
	[FormattedDeliveryAddress] [nvarchar](50) NULL,
	[TotalDiscountVendorGroupCode] [nvarchar](50) NULL,
	[KRFSeasonFixed] [int] NULL,
	[KRFSeasonCode] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[PurchaseOrderLines]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[PurchaseOrderLines](
	[ProcurementProductCategoryName] [nvarchar](500) NULL,
	[Tax1099SAddressOrLegalDescription] [nvarchar](500) NULL,
	[FixedAssetNumber] [nvarchar](500) NULL,
	[Tax1099GTaxYear] [int] NULL,
	[ProjectSalesUnitSymbol] [nvarchar](500) NULL,
	[OrderedPurchaseQuantity] [numeric](32, 16) NULL,
	[FormattedDelveryAddress] [nvarchar](500) NULL,
	[ProjectCategoryId] [nvarchar](500) NULL,
	[AccountingDistributionTemplateName] [nvarchar](500) NULL,
	[ItemNumber] [nvarchar](500) NULL,
	[DeliveryAddressDescription] [nvarchar](500) NULL,
	[MultilineDiscountPercentage] [numeric](32, 16) NULL,
	[DeliveryCityInKana] [nvarchar](500) NULL,
	[RetailProductVariantNumber] [nvarchar](500) NULL,
	[DeliveryStreetInKana] [nvarchar](500) NULL,
	[LineDiscountAmount] [numeric](32, 16) NULL,
	[ProductStyleId] [nvarchar](500) NULL,
	[IsTax1099SPropertyOrServices] [int] NULL,
	[ProjectTaxItemGroupCode] [nvarchar](500) NULL,
	[ProjectTaxGroupCode] [nvarchar](500) NULL,
	[Barcode] [nvarchar](500) NULL,
	[IsNewFixedAsset] [int] NULL,
	[ProductConfigurationId] [nvarchar](500) NULL,
	[Tax1099GVendorStateId] [nvarchar](500) NULL,
	[IsIntrastatTriangularDeal] [int] NULL,
	[Tax1099StateId] [nvarchar](500) NULL,
	[IsPartialDeliveryPrevented] [int] NULL,
	[MultilineDiscountAmount] [numeric](32, 16) NULL,
	[Tax1099Type] [int] NULL,
	[RequestedDeliveryDate] [datetime] NULL,
	[DeliveryAddressCountryRegionId] [nvarchar](500) NULL,
	[ItemBatchNumber] [nvarchar](500) NULL,
	[DeliveryAddressLatitude] [numeric](32, 16) NULL,
	[ReceivingWarehouseId] [nvarchar](500) NULL,
	[DeliveryAddressCity] [nvarchar](500) NULL,
	[ConfirmedDeliveryDate] [datetime] NULL,
	[PurchaseUnitSymbol] [nvarchar](500) NULL,
	[PurchaseRebateVendorGroupId] [nvarchar](500) NULL,
	[RequesterPersonnelNumber] [nvarchar](500) NULL,
	[ProjectId] [nvarchar](500) NULL,
	[IsTax1099GTradeOrBusinessIncome] [int] NULL,
	[ProjectLinePropertyId] [nvarchar](500) NULL,
	[DeliveryAddressDistrictName] [nvarchar](500) NULL,
	[DeliveryAddressCountyId] [nvarchar](500) NULL,
	[Tax1099SBuyerPartOfRealEstateTaxAmount] [numeric](32, 16) NULL,
	[ProductSizeId] [nvarchar](500) NULL,
	[DeliveryAddressZipCode] [nvarchar](500) NULL,
	[FixedPriceCharges] [numeric](32, 16) NULL,
	[UnitWeight] [numeric](32, 16) NULL,
	[Tax1099SClosingDate] [datetime] NULL,
	[DataAreaId] [nvarchar](500) NULL,
	[DeliveryAddressDunsNumber] [nvarchar](500) NULL,
	[PurchasePriceQuantity] [numeric](32, 16) NULL,
	[PurchaseOrderNumber] [nvarchar](500) NULL,
	[DeliveryAddressName] [nvarchar](500) NULL,
	[Tax1099BoxId] [nvarchar](500) NULL,
	[BOMId] [nvarchar](500) NULL,
	[FixedAssetTransactionType] [int] NULL,
	[DeliveryAddressStreetNumber] [nvarchar](500) NULL,
	[NGPCode] [int] NULL,
	[IsDeliveryAddressPrivate] [int] NULL,
	[OriginStateId] [nvarchar](500) NULL,
	[MainAccountIdDisplayValue] [nvarchar](500) NULL,
	[CatchWeightUnitSymbol] [nvarchar](500) NULL,
	[OrderedInventoryStatusId] [nvarchar](500) NULL,
	[ReceivingSiteId] [nvarchar](500) NULL,
	[ProjectSalesCurrencyCode] [nvarchar](500) NULL,
	[IntrastatTransactionCode] [nvarchar](500) NULL,
	[DeliveryAddressLocationId] [nvarchar](500) NULL,
	[SalesTaxItemGroupCode] [nvarchar](500) NULL,
	[RouteId] [nvarchar](500) NULL,
	[Tax1099GStateTaxWithheldAmount] [numeric](32, 16) NULL,
	[IntrastatStatisticsProcedureCode] [nvarchar](500) NULL,
	[LineDescription] [nvarchar](500) NULL,
	[GSTHSTTaxType] [int] NULL,
	[DeliveryAddressStreet] [nvarchar](500) NULL,
	[ConfirmedShippingDate] [datetime] NULL,
	[CustomerReference] [nvarchar](500) NULL,
	[SalesTaxGroupCode] [nvarchar](500) NULL,
	[IsDeliveryAddressOrderSpecific] [int] NULL,
	[CustomerRequisitionNumber] [nvarchar](500) NULL,
	[PurchasePrice] [numeric](32, 16) NULL,
	[WillProductReceivingCrossDockProducts] [int] NULL,
	[LineDiscountPercentage] [numeric](32, 16) NULL,
	[DIOTOperationType] [int] NULL,
	[FixedAssetValueModelId] [nvarchar](500) NULL,
	[OrderedCatchWeightQuantity] [numeric](32, 16) NULL,
	[ProjectWorkerPersonnelNumber] [nvarchar](500) NULL,
	[AllowedUnderdeliveryPercentage] [numeric](32, 16) NULL,
	[AllowedOverdeliveryPercentage] [numeric](32, 16) NULL,
	[DeliveryAddressLongitude] [numeric](32, 16) NULL,
	[FixedAssetGroupId] [nvarchar](500) NULL,
	[PurchaseOrderLineStatus] [int] NULL,
	[IntrastatCommodityCode] [nvarchar](500) NULL,
	[DefaultLedgerDimensionDisplayValue] [nvarchar](500) NULL,
	[LineNumber] [bigint] NULL,
	[DeliveryAddressTimeZone] [int] NULL,
	[ProductColorId] [nvarchar](500) NULL,
	[DeliveryAddressStateId] [nvarchar](500) NULL,
	[DeliveryBuildingCompliment] [nvarchar](500) NULL,
	[IntrastatTransportModeCode] [nvarchar](500) NULL,
	[Tax1099StateAmount] [numeric](32, 16) NULL,
	[DeliveryAddressPostBox] [nvarchar](500) NULL,
	[LineAmount] [numeric](32, 16) NULL,
	[OriginCountryRegionId] [nvarchar](500) NULL,
	[IntrastatPortId] [nvarchar](500) NULL,
	[IntrastatSpecialMovementCode] [nvarchar](500) NULL,
	[Tax1099Amount] [numeric](32, 16) NULL,
	[BarCodeSetupId] [nvarchar](500) NULL,
	[VendorInvoiceMatchingPolicy] [int] NULL,
	[ProjectSalesPrice] [numeric](32, 16) NULL,
	[Tax1099GVendorStateTaxId] [nvarchar](500) NULL,
	[RequestedShippingDate] [datetime] NULL,
	[ExternalItemNumber] [nvarchar](500) NULL,
	[IsProjectPayWhenPaid] [int] NULL,
	[IsLineStopped] [int] NULL,
	[IntrastatStatisticValue] [numeric](32, 16) NULL,
	[KRFMatrixNotProcessed] [int] NULL,
	[KRFSeasonCode] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[PurchLine]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[PurchLine](
	[InventTransId] [nvarchar](20) NOT NULL,
	[RecId] [bigint] NOT NULL,
	[DataAreaId] [nvarchar](4) NOT NULL,
	[Partition] [bigint] NOT NULL,
	[ItemId] [nvarchar](20) NOT NULL,
	[DeliveryDate] [datetime] NOT NULL,
	[PurchId] [nvarchar](20) NOT NULL,
	[QtyOrdered] [numeric](32, 16) NOT NULL,
	[RemainPurchPhysical] [numeric](32, 16) NOT NULL,
	[PurchStatus] [int] NOT NULL,
	[InventDimId] [nvarchar](20) NOT NULL,
	[ModifiedDateTime] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[PurchLine_Increment]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[PurchLine_Increment](
	[InventTransId] [nvarchar](20) NOT NULL,
	[RecId] [bigint] NOT NULL,
	[DataAreaId] [nvarchar](4) NOT NULL,
	[Partition] [bigint] NOT NULL,
	[ItemId] [nvarchar](20) NOT NULL,
	[DeliveryDate] [datetime] NOT NULL,
	[PurchId] [nvarchar](20) NOT NULL,
	[QtyOrdered] [numeric](32, 16) NOT NULL,
	[RemainPurchPhysical] [numeric](32, 16) NOT NULL,
	[PurchStatus] [int] NOT NULL,
	[InventDimId] [nvarchar](20) NOT NULL,
	[ModifiedDateTime] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ReleasedDistinctProducts]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ReleasedDistinctProducts](
	[DataAreaId] [nvarchar](4) NULL,
	[ItemNumber] [nvarchar](20) NOT NULL,
	[ProductType] [int] NULL,
	[GrossProductHeight] [numeric](32, 16) NULL,
	[GrossProductWidth] [numeric](32, 16) NULL,
	[PrimaryVendorAccountNumber] [nvarchar](20) NULL,
	[NetProductWeight] [numeric](32, 16) NULL,
	[GrossDepth] [numeric](32, 16) NULL,
	[ProductVolume] [numeric](32, 16) NULL,
	[RevenueABCCode] [int] NULL,
	[ValueABCCode] [int] NULL,
	[MarginABCCode] [int] NULL,
	[SearchName] [nvarchar](20) NULL,
	[ProductGroupId] [nvarchar](10) NULL,
	[ProjectCategoryId] [nvarchar](30) NULL,
	[StandardPalletQty] [int] NULL,
	[qtyPerLayer] [int] NULL,
	[BuyerGroupId] [nvarchar](10) NULL,
	[ProductRecId] [bigint] NULL,
	[FixedSalesPriceCharges] [numeric](32, 16) NULL,
	[IsPhantom] [int] NULL,
	[IsPurchasePriceIncludingCharges] [int] NULL,
	[ItemFiscalClassificationCode] [nvarchar](10) NULL,
	[IsICMSTaxAppliedOnService] [int] NULL,
	[ShippingAndReceivingSortOrderCode] [int] NULL,
	[ProductionConsumptionWidthConversionFactor] [int] NULL,
	[AlternativeProductSizeId] [nvarchar](10) NULL,
	[RawMaterialPickingPrinciple] [int] NULL,
	[ProductionConsumptionDepthConversionFactor] [int] NULL,
	[ItemModelGroupId] [nvarchar](50) NULL,
	[IsSalesWithholdingTaxCalculated] [int] NULL,
	[TrackingDimensionGroupName] [nvarchar](50) NULL,
	[PurchaseSalesTaxItemGroupCode] [nvarchar](10) NULL,
	[PlanningFormulaItemNumber] [nvarchar](50) NULL,
	[WarehouseMobileDeviceDescriptionLine1] [nvarchar](500) NULL,
	[WarehouseMobileDeviceDescriptionLine2] [nvarchar](500) NULL,
	[SalesItemWithholdingTaxGroupCode] [nvarchar](10) NULL,
	[IsPOSRegistrationQuantityNegative] [int] NULL,
	[POSRegistrationPlannedBlockedDate] [datetime] NULL,
	[SellEndDate] [datetime] NULL,
	[IsPurchaseWithholdingTaxCalculated] [int] NULL,
	[DefaultLedgerDimensionDisplayValue] [nvarchar](50) NULL,
	[CommissionProductGroupId] [nvarchar](50) NULL,
	[IsExemptFromAutomaticNotificationAndCancellation] [int] NULL,
	[ProductionType] [int] NULL,
	[ProductionPoolId] [nvarchar](10) NULL,
	[SalesSupplementaryProductProductGroupId] [nvarchar](10) NULL,
	[StorageDimensionGroupName] [nvarchar](10) NULL,
	[PurchasePricingPrecision] [int] NULL,
	[BOMUnitSymbol] [nvarchar](10) NULL,
	[SalesPriceCalculationContributionRatio] [int] NULL,
	[CatchWeightUnitSymbol] [nvarchar](10) NULL,
	[VendorInvoiceLineMatchingPolicy] [int] NULL,
	[SellStartDate] [datetime] NULL,
	[PhysicalDimensionGroupId] [nvarchar](10) NULL,
	[CarryingCostABCCode] [int] NULL,
	[TransferOrderOverdeliveryPercentage] [int] NULL,
	[UnitConversionSequenceGroupId] [nvarchar](10) NULL,
	[WillPickingWorkbenchApplyBoxingLogic] [int] NULL,
	[IsDeliveredDirectly] [int] NULL,
	[SalesGSTReliefCategoryCode] [nvarchar](10) NULL,
	[IsScaleProduct] [int] NULL,
	[AlternativeProductColorId] [nvarchar](10) NULL,
	[FixedPurchasePriceCharges] [int] NULL,
	[IsUnitCostIncludingCharges] [int] NULL,
	[ShipStartDate] [datetime] NULL,
	[SalesPrice] [numeric](32, 16) NULL,
	[SalesPriceCalculationModel] [int] NULL,
	[ArrivalHandlingTime] [int] NULL,
	[IntrastatCommodityCode] [nvarchar](10) NULL,
	[AreTransportationManagementProcessesEnabled] [int] NULL,
	[IsShipAloneEnabled] [int] NULL,
	[ProductionConsumptionDensityConversionFactor] [int] NULL,
	[PurchasePriceDate] [datetime] NULL,
	[SalesPricingPrecision] [int] NULL,
	[PurchaseChargesQuantity] [int] NULL,
	[ProductSearchName] [nvarchar](50) NULL,
	[UnitCostDate] [datetime] NULL,
	[VariableScrapPercentage] [int] NULL,
	[MaximumPickQuantity] [int] NULL,
	[AlternativeProductStyleId] [nvarchar](10) NULL,
	[BarcodeSetupId] [nvarchar](10) NULL,
	[IsSalesPriceIncludingCharges] [int] NULL,
	[PurchasePriceQuantity] [int] NULL,
	[PurchaseChargeProductGroupId] [nvarchar](10) NULL,
	[ContinuityScheduleId] [nvarchar](50) NULL,
	[FixedCostCharges] [int] NULL,
	[CostGroupId] [nvarchar](50) NULL,
	[SalesLineDiscountProductGroupCode] [nvarchar](50) NULL,
	[POSRegistrationActivationDate] [datetime] NULL,
	[MaximumCatchWeightQuantity] [int] NULL,
	[ServiceFiscalInformationCode] [nvarchar](50) NULL,
	[ProductLifeCycleValidToDate] [datetime] NULL,
	[PurchaseSupplementaryProductProductGroupId] [nvarchar](50) NULL,
	[InventoryUnitSymbol] [nvarchar](50) NULL,
	[WillTotalPurchaseDiscountCalculationIncludeProduct] [int] NULL,
	[PackSizeCategoryId] [nvarchar](50) NULL,
	[SalesChargesQuantity] [int] NULL,
	[BatchMergeDateCalculationMethod] [int] NULL,
	[SalesMultilineDiscountProductGroupCode] [nvarchar](50) NULL,
	[PurchasePrice] [numeric](32, 16) NULL,
	[SalesChargeProductGroupId] [nvarchar](50) NULL,
	[IsIntercompanyPurchaseUsageBlocked] [int] NULL,
	[AlternativeProductConfigurationId] [nvarchar](50) NULL,
	[SalesOverdeliveryPercentage] [int] NULL,
	[IsDiscountPOSRegistrationProhibited] [int] NULL,
	[BestBeforePeriodDays] [int] NULL,
	[PurchaseOverdeliveryPercentage] [int] NULL,
	[PurchaseUnitSymbol] [nvarchar](50) NULL,
	[SalesUnderdeliveryPercentage] [int] NULL,
	[NecessaryProductionWorkingTimeSchedulingPropertyId] [nvarchar](50) NULL,
	[InventoryGSTReliefCategoryCode] [nvarchar](50) NULL,
	[ApprovedVendorCheckMethod] [int] NULL,
	[SalesRebateProductGroupId] [nvarchar](50) NULL,
	[InventoryReservationHierarchyName] [nvarchar](50) NULL,
	[FlushingPrinciple] [int] NULL,
	[SalesPriceQuantity] [int] NULL,
	[YieldPercentage] [int] NULL,
	[TareProductWeight] [int] NULL,
	[ApproximateSalesTaxPercentage] [int] NULL,
	[PackingDutyQuantity] [int] NULL,
	[PurchaseLineDiscountProductGroupCode] [nvarchar](50) NULL,
	[WillInventoryIssueAutomaticallyReportAsFinished] [int] NULL,
	[ProductFiscalInformationType] [nvarchar](50) NULL,
	[PackageHandlingTime] [int] NULL,
	[DynamicsConnectorIntegrationKey] [uniqueidentifier] NULL,
	[ShelfLifePeriodDays] [int] NULL,
	[TransferOrderUnderdeliveryPercentage] [int] NULL,
	[DefaultReceivingQuantity] [int] NULL,
	[POSRegistrationBlockedDate] [datetime] NULL,
	[MustKeyInCommentAtPOSRegister] [int] NULL,
	[ConstantScrapQuantity] [int] NULL,
	[PotencyBaseAttributeValueEntryEvent] [int] NULL,
	[KeyInPriceRequirementsAtPOSRegister] [int] NULL,
	[IntrastatChargePercentage] [int] NULL,
	[ProductCoverageGroupId] [nvarchar](50) NULL,
	[PotencyBaseAttibuteTargetValue] [int] NULL,
	[IsIntercompanySalesUsageBlocked] [int] NULL,
	[PackingMaterialGroupId] [nvarchar](50) NULL,
	[PurchaseRebateProductGroupId] [nvarchar](50) NULL,
	[OriginCountryRegionId] [nvarchar](50) NULL,
	[AlternativeItemNumber] [nvarchar](50) NULL,
	[BOMLevel] [int] NULL,
	[PurchaseItemWithholdingTaxGroupCode] [nvarchar](50) NULL,
	[IsZeroPricePOSRegistrationAllowed] [int] NULL,
	[CostChargesQuantity] [int] NULL,
	[IsUnitCostAutomaticallyUpdated] [int] NULL,
	[DefaultDirectDeliveryWarehouse] [nvarchar](50) NULL,
	[ProductTaxationOrigin] [int] NULL,
	[IsVariantShelfLabelsPrintingEnabled] [int] NULL,
	[UnitCost] [numeric](32, 16) NULL,
	[SalesPriceDate] [datetime] NULL,
	[AlternativeProductUsageCondition] [int] NULL,
	[WillTotalSalesDiscountCalculationIncludeProduct] [int] NULL,
	[IsInstallmentEligible] [int] NULL,
	[PurchasePriceToleranceGroupId] [nvarchar](50) NULL,
	[BaseSalesPriceSource] [int] NULL,
	[SerialNumberGroupCode] [nvarchar](50) NULL,
	[ProductLifeCycleValidFromDate] [datetime] NULL,
	[ItemFiscalClassificationExceptionCode] [nvarchar](50) NULL,
	[NGPCode] [int] NULL,
	[SalesUnitSymbol] [nvarchar](50) NULL,
	[ProductionGroupId] [nvarchar](50) NULL,
	[KeyInQuantityRequirementsAtPOSRegister] [int] NULL,
	[DefaultOrderType] [int] NULL,
	[ProductionConsumptionHeightConversionFactor] [int] NULL,
	[ContinuityEventDuration] [int] NULL,
	[IsPOSRegistrationBlocked] [int] NULL,
	[BatchNumberGroupCode] [nvarchar](50) NULL,
	[PotencyBaseAttributeId] [nvarchar](50) NULL,
	[PurchaseUnderdeliveryPercentage] [int] NULL,
	[PackageClassId] [nvarchar](50) NULL,
	[PurchaseGSTReliefCategoryCode] [nvarchar](50) NULL,
	[SalesPriceCalculationChargesPercentage] [int] NULL,
	[PurchaseMultilineDiscountProductGroupCode] [nvarchar](50) NULL,
	[WillWorkCenterPickingAllowNegativeInventory] [int] NULL,
	[ProductLifeCycleSeasonCode] [nvarchar](50) NULL,
	[SalesSalesTaxItemGroupCode] [nvarchar](50) NULL,
	[IsRestrictedForCoupons] [int] NULL,
	[IsSalesPriceAdjustmentAllowed] [int] NULL,
	[IsPurchasePriceAutomaticallyUpdated] [int] NULL,
	[MinimumCatchWeightQuantity] [int] NULL,
	[WillInventoryReceiptIgnoreFlushingPrinciple] [int] NULL,
	[ProductNumber] [nvarchar](50) NULL,
	[UnitCostQuantity] [int] NULL,
	[FreightAllocationGroupId] [nvarchar](50) NULL,
	[ComparisonPriceBaseUnitSymbol] [nvarchar](50) NULL,
	[CostCalculationGroupId] [nvarchar](50) NULL,
	[ShelfAdvicePeriodDays] [int] NULL,
	[OriginStateId] [nvarchar](50) NULL,
 CONSTRAINT [PK_InventTable] PRIMARY KEY CLUSTERED 
(
	[ItemNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ReleasedProductMaster]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ReleasedProductMaster](
	[TransferOrderOverdeliveryPercentage] [numeric](32, 16) NULL,
	[SalesUnitSymbol] [nvarchar](500) NULL,
	[ProductionConsumptionWidthConversionFactor] [numeric](32, 16) NULL,
	[BOMLevel] [int] NULL,
	[IsPurchasePriceAutomaticallyUpdated] [int] NULL,
	[IsPurchaseWithholdingTaxCalculated] [int] NULL,
	[TransferOrderUnderdeliveryPercentage] [numeric](32, 16) NULL,
	[IsDeliveredDirectly] [int] NULL,
	[SalesSupplementaryProductProductGroupId] [nvarchar](500) NULL,
	[SalesMultilineDiscountProductGroupCode] [nvarchar](500) NULL,
	[WillTotalPurchaseDiscountCalculationIncludeProduct] [int] NULL,
	[IsVariantShelfLabelsPrintingEnabled] [int] NULL,
	[ProductionGroupId] [nvarchar](500) NULL,
	[OriginStateId] [nvarchar](500) NULL,
	[RevenueABCCode] [int] NULL,
	[NecessaryProductionWorkingTimeSchedulingPropertyId] [nvarchar](500) NULL,
	[IntrastatCommodityCode] [nvarchar](500) NULL,
	[SalesRebateProductGroupId] [nvarchar](500) NULL,
	[UnitCostDate] [datetime] NULL,
	[DefaultProductSizeId] [nvarchar](500) NULL,
	[AlternativeProductStyleId] [nvarchar](500) NULL,
	[PotencyBaseAttributeId] [nvarchar](500) NULL,
	[AlternativeProductUsageCondition] [int] NULL,
	[PurchaseUnderdeliveryPercentage] [numeric](32, 16) NULL,
	[DefaultProductStyleId] [nvarchar](500) NULL,
	[AlternativeProductSizeId] [nvarchar](500) NULL,
	[WillInventoryIssueAutomaticallyReportAsFinished] [int] NULL,
	[ProductVolume] [numeric](32, 16) NULL,
	[TareProductWeight] [numeric](32, 16) NULL,
	[ItemNumber] [nvarchar](500) NULL,
	[IsPhantom] [int] NULL,
	[DefaultProductConfigurationId] [nvarchar](500) NULL,
	[FlushingPrinciple] [int] NULL,
	[VariableScrapPercentage] [numeric](32, 16) NULL,
	[ArrivalHandlingTime] [int] NULL,
	[SearchName] [nvarchar](500) NULL,
	[IsUnitCostIncludingCharges] [int] NULL,
	[AlternativeProductColorId] [nvarchar](500) NULL,
	[PhysicalDimensionGroupId] [nvarchar](500) NULL,
	[MinimumCatchWeightQuantity] [numeric](32, 16) NULL,
	[SalesChargeProductGroupId] [nvarchar](500) NULL,
	[ProductSearchName] [nvarchar](500) NULL,
	[WillPickingWorkbenchApplyBoxingLogic] [int] NULL,
	[ItemFiscalClassificationExceptionCode] [nvarchar](500) NULL,
	[InventoryReservationHierarchyName] [nvarchar](500) NULL,
	[IsZeroPricePOSRegistrationAllowed] [int] NULL,
	[IntrastatChargePercentage] [numeric](32, 16) NULL,
	[IsScaleProduct] [int] NULL,
	[PlanningFormulaItemNumber] [nvarchar](500) NULL,
	[ProductFiscalInformationType] [nvarchar](500) NULL,
	[StorageDimensionGroupName] [nvarchar](500) NULL,
	[ShelfAdvicePeriodDays] [int] NULL,
	[ContinuityScheduleId] [nvarchar](500) NULL,
	[VendorInvoiceLineMatchingPolicy] [int] NULL,
	[InventoryGSTReliefCategoryCode] [nvarchar](500) NULL,
	[MustKeyInCommentAtPOSRegister] [int] NULL,
	[SalesPriceQuantity] [numeric](32, 16) NULL,
	[ServiceFiscalInformationCode] [nvarchar](500) NULL,
	[BarcodeSetupId] [nvarchar](500) NULL,
	[IsUnitCostAutomaticallyUpdated] [int] NULL,
	[PotencyBaseAttibuteTargetValue] [numeric](32, 16) NULL,
	[DefaultReceivingQuantity] [numeric](32, 16) NULL,
	[ProductionConsumptionDensityConversionFactor] [numeric](32, 16) NULL,
	[PurchaseItemWithholdingTaxGroupCode] [nvarchar](500) NULL,
	[PurchasePricingPrecision] [int] NULL,
	[UnitCostQuantity] [numeric](32, 16) NULL,
	[IsRestrictedForCoupons] [int] NULL,
	[SellStartDate] [datetime] NULL,
	[SellEndDate] [datetime] NULL,
	[ConstantScrapQuantity] [numeric](32, 16) NULL,
	[BatchNumberGroupCode] [nvarchar](500) NULL,
	[CostCalculationGroupId] [nvarchar](500) NULL,
	[PackingDutyQuantity] [numeric](32, 16) NULL,
	[AlternativeProductConfigurationId] [nvarchar](500) NULL,
	[SalesPrice] [numeric](32, 16) NULL,
	[DefaultProductColorId] [nvarchar](500) NULL,
	[IsSalesPriceIncludingCharges] [int] NULL,
	[ProductionType] [int] NULL,
	[WillTotalSalesDiscountCalculationIncludeProduct] [int] NULL,
	[PotencyBaseAttributeValueEntryEvent] [int] NULL,
	[ItemModelGroupId] [nvarchar](500) NULL,
	[PurchaseMultilineDiscountProductGroupCode] [nvarchar](500) NULL,
	[DynamicsConnectorIntegrationKey] [uniqueidentifier] NULL,
	[SalesChargesQuantity] [numeric](32, 16) NULL,
	[SalesPriceCalculationContributionRatio] [numeric](32, 16) NULL,
	[ProductGroupId] [nvarchar](500) NULL,
	[IsSalesPriceAdjustmentAllowed] [int] NULL,
	[SalesPriceCalculationChargesPercentage] [numeric](32, 16) NULL,
	[ShippingAndReceivingSortOrderCode] [int] NULL,
	[GrossProductHeight] [numeric](32, 16) NULL,
	[ProductLifeCycleValidFromDate] [datetime] NULL,
	[PurchaseGSTReliefCategoryCode] [nvarchar](500) NULL,
	[POSRegistrationPlannedBlockedDate] [datetime] NULL,
	[SalesItemWithholdingTaxGroupCode] [nvarchar](500) NULL,
	[PurchaseLineDiscountProductGroupCode] [nvarchar](500) NULL,
	[PurchasePriceDate] [datetime] NULL,
	[IsIntercompanyPurchaseUsageBlocked] [int] NULL,
	[NGPCode] [int] NULL,
	[ShelfLifePeriodDays] [int] NULL,
	[UnitConversionSequenceGroupId] [nvarchar](500) NULL,
	[ProductionConsumptionHeightConversionFactor] [numeric](32, 16) NULL,
	[BOMUnitSymbol] [nvarchar](500) NULL,
	[MaximumCatchWeightQuantity] [numeric](32, 16) NULL,
	[PurchasePriceQuantity] [numeric](32, 16) NULL,
	[PurchasePrice] [numeric](32, 16) NULL,
	[ProductLifeCycleSeasonCode] [nvarchar](500) NULL,
	[IsDiscountPOSRegistrationProhibited] [int] NULL,
	[ProductLifeCycleValidToDate] [datetime] NULL,
	[PurchaseOverdeliveryPercentage] [numeric](32, 16) NULL,
	[TrackingDimensionGroupName] [nvarchar](500) NULL,
	[FixedSalesPriceCharges] [numeric](32, 16) NULL,
	[ProductTaxationOrigin] [int] NULL,
	[ProductionPoolId] [nvarchar](500) NULL,
	[ValueABCCode] [int] NULL,
	[PurchaseUnitSymbol] [nvarchar](500) NULL,
	[PurchaseSupplementaryProductProductGroupId] [nvarchar](500) NULL,
	[AlternativeItemNumber] [nvarchar](500) NULL,
	[SalesSalesTaxItemGroupCode] [nvarchar](500) NULL,
	[ProductCoverageGroupId] [nvarchar](500) NULL,
	[CostGroupId] [nvarchar](500) NULL,
	[IsPurchasePriceIncludingCharges] [int] NULL,
	[IsShipAloneEnabled] [int] NULL,
	[ProductNumber] [nvarchar](500) NULL,
	[RawMaterialPickingPrinciple] [int] NULL,
	[FixedPurchasePriceCharges] [numeric](32, 16) NULL,
	[FreightAllocationGroupId] [nvarchar](500) NULL,
	[ContinuityEventDuration] [int] NULL,
	[PurchaseSalesTaxItemGroupCode] [nvarchar](500) NULL,
	[DefaultDirectDeliveryWarehouse] [nvarchar](500) NULL,
	[SalesGSTReliefCategoryCode] [nvarchar](500) NULL,
	[SalesPriceDate] [datetime] NULL,
	[OriginCountryRegionId] [nvarchar](500) NULL,
	[DefaultOrderType] [int] NULL,
	[IsPOSRegistrationQuantityNegative] [int] NULL,
	[PurchaseChargesQuantity] [numeric](32, 16) NULL,
	[PrimaryVendorAccountNumber] [nvarchar](500) NULL,
	[MaximumPickQuantity] [numeric](32, 16) NULL,
	[SalesUnderdeliveryPercentage] [numeric](32, 16) NULL,
	[IsInstallmentEligible] [int] NULL,
	[KeyInQuantityRequirementsAtPOSRegister] [int] NULL,
	[CommissionProductGroupId] [nvarchar](500) NULL,
	[IsIntercompanySalesUsageBlocked] [int] NULL,
	[YieldPercentage] [numeric](32, 16) NULL,
	[BaseSalesPriceSource] [int] NULL,
	[IsSalesWithholdingTaxCalculated] [int] NULL,
	[ApprovedVendorCheckMethod] [int] NULL,
	[BestBeforePeriodDays] [int] NULL,
	[GrossDepth] [numeric](32, 16) NULL,
	[PurchaseRebateProductGroupId] [nvarchar](500) NULL,
	[PackSizeCategoryId] [nvarchar](500) NULL,
	[PackageClassId] [nvarchar](500) NULL,
	[FixedCostCharges] [numeric](32, 16) NULL,
	[UnitCost] [numeric](32, 16) NULL,
	[SerialNumberGroupCode] [nvarchar](500) NULL,
	[CarryingCostABCCode] [int] NULL,
	[SalesLineDiscountProductGroupCode] [nvarchar](500) NULL,
	[IsPOSRegistrationBlocked] [int] NULL,
	[POSRegistrationBlockedDate] [datetime] NULL,
	[ProjectCategoryId] [nvarchar](500) NULL,
	[PurchasePriceToleranceGroupId] [nvarchar](500) NULL,
	[AreTransportationManagementProcessesEnabled] [int] NULL,
	[IsExemptFromAutomaticNotificationAndCancellation] [int] NULL,
	[PackingMaterialGroupId] [nvarchar](500) NULL,
	[InventoryUnitSymbol] [nvarchar](500) NULL,
	[ComparisonPriceBaseUnitSymbol] [nvarchar](500) NULL,
	[WillWorkCenterPickingAllowNegativeInventory] [int] NULL,
	[IsICMSTaxAppliedOnService] [int] NULL,
	[ProductType] [int] NULL,
	[DataAreaId] [nvarchar](500) NULL,
	[KeyInPriceRequirementsAtPOSRegister] [int] NULL,
	[ApproximateSalesTaxPercentage] [numeric](32, 16) NULL,
	[POSRegistrationActivationDate] [datetime] NULL,
	[WillInventoryReceiptIgnoreFlushingPrinciple] [int] NULL,
	[NetProductWeight] [numeric](32, 16) NULL,
	[CostChargesQuantity] [numeric](32, 16) NULL,
	[BatchMergeDateCalculationMethod] [int] NULL,
	[SalesPriceCalculationModel] [int] NULL,
	[PurchaseChargeProductGroupId] [nvarchar](500) NULL,
	[SalesOverdeliveryPercentage] [numeric](32, 16) NULL,
	[DefaultLedgerDimensionDisplayValue] [nvarchar](500) NULL,
	[SalesPricingPrecision] [int] NULL,
	[MarginABCCode] [int] NULL,
	[CatchWeightUnitSymbol] [nvarchar](500) NULL,
	[WarehouseMobileDeviceDescriptionLine1] [nvarchar](500) NULL,
	[WarehouseMobileDeviceDescriptionLine2] [nvarchar](500) NULL,
	[ProductionConsumptionDepthConversionFactor] [numeric](32, 16) NULL,
	[ItemFiscalClassificationCode] [nvarchar](500) NULL,
	[PackageHandlingTime] [int] NULL,
	[IsUnitCostProductVariantSpecific] [int] NULL,
	[BuyerGroupId] [nvarchar](500) NULL,
	[GrossProductWidth] [numeric](32, 16) NULL,
	[ShipStartDate] [datetime] NULL,
	[ProductRecId] [bigint] NULL,
	[StandardPalletQty] [numeric](32, 16) NULL,
	[QtyPerLayer] [numeric](32, 16) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[ReleasedProductVariants]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[ReleasedProductVariants](
	[ProductSizeId] [nvarchar](500) NULL,
	[ProductConfigurationId] [nvarchar](500) NULL,
	[ProductSearchName] [nvarchar](500) NULL,
	[ItemNumber] [nvarchar](500) NULL,
	[ProductVariantNumber] [nvarchar](500) NULL,
	[ProductStyleId] [nvarchar](500) NULL,
	[DataAreaId] [nvarchar](500) NULL,
	[ProductDescription] [nvarchar](500) NULL,
	[ProductMasterNumber] [nvarchar](500) NULL,
	[ProductName] [nvarchar](500) NULL,
	[ProductColorId] [nvarchar](500) NULL,
	[PublicRecId] [bigint] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[REQITEMTABLE]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[REQITEMTABLE](
	[InventLocationIdReqMain] [nvarchar](500) NULL,
	[LeadTimeProduction] [int] NULL,
	[MaxSafetyKeyId] [nvarchar](500) NULL,
	[ReqGroupId] [nvarchar](500) NULL,
	[LeadTimeTransferActive] [int] NULL,
	[AuthorizationTimeFence] [int] NULL,
	[CovFieldsActive] [int] NULL,
	[TimeFenceBackRequisition] [int] NULL,
	[PmfPlanPriorityDefault] [int] NULL,
	[OnHandConsumptionStrategy] [int] NULL,
	[LeadTimeTransfer] [int] NULL,
	[CalendarDaysProduction] [int] NULL,
	[ReqPOType] [int] NULL,
	[MaxInventOnhand] [numeric](32, 16) NULL,
	[PmfPlanPriorityCurrent] [int] NULL,
	[MinSafetyPeriod] [int] NULL,
	[MinSafetyKeyId] [nvarchar](500) NULL,
	[CovInventDimId] [nvarchar](500) NULL,
	[ReqPOTypeActive] [int] NULL,
	[LeadTimePurchase] [int] NULL,
	[CovTimeFence] [int] NULL,
	[LeadTimeProductionActive] [int] NULL,
	[CapacityTimeFence] [int] NULL,
	[CalendarDaysPurchase] [int] NULL,
	[LeadTimePurchaseActive] [int] NULL,
	[OnHandActive] [int] NULL,
	[ItemId] [nvarchar](500) NULL,
	[MaxNegativeDays] [int] NULL,
	[ExplosionTimeFence] [int] NULL,
	[LockingTimeFence] [int] NULL,
	[PmfPlanPriorityDateChanged] [datetime] NULL,
	[CovRule] [int] NULL,
	[VendId] [nvarchar](500) NULL,
	[CalendarDaysTransfer] [int] NULL,
	[CovPeriod] [int] NULL,
	[MinSatisfy] [int] NULL,
	[MinInventOnhand] [numeric](32, 16) NULL,
	[TimeFenceFieldsActive] [int] NULL,
	[MaxPositiveDays] [int] NULL,
	[PmfPlanningItemId] [nvarchar](500) NULL,
	[DataAreaId] [nvarchar](500) NULL,
	[ItemCovFieldsActive] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[REQSAFETYKEY]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[REQSAFETYKEY](
	[FIXED] [int] NOT NULL,
	[NAME] [nvarchar](60) NOT NULL,
	[SAFETYKEYID] [nvarchar](10) NOT NULL,
	[FIXEDDATE] [datetime] NOT NULL,
 CONSTRAINT [I_918KEYIDX] PRIMARY KEY CLUSTERED 
(
	[SAFETYKEYID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[REQSAFETYLINE]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[REQSAFETYLINE](
	[SAFETYFACTOR] [numeric](32, 16) NOT NULL,
	[SAFETYKEYID] [nvarchar](10) NOT NULL,
	[FREQCODE] [int] NOT NULL,
	[FREQ] [int] NOT NULL,
	[SORT1980] [datetime] NOT NULL,
	[DATAAREAID] [nvarchar](4) NOT NULL,
	[RECVERSION] [int] NOT NULL,
	[PARTITION] [bigint] NOT NULL,
	[RECID] [bigint] NOT NULL,
 CONSTRAINT [I_919RECID] PRIMARY KEY NONCLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[RETAILASSORTMENTCHANNELLINE]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[RETAILASSORTMENTCHANNELLINE](
	[AssortmentId] [nvarchar](500) NULL,
	[PartyNumber] [nvarchar](500) NULL,
	[Status] [int] NULL,
	[OrganizationName] [nvarchar](500) NULL,
	[OrganizationHierarchyType] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[RETAILASSORTMENTLOOKUP]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[RETAILASSORTMENTLOOKUP](
	[AssortmentId] [bigint] NOT NULL,
	[LineType] [int] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[ValidFrom] [datetime] NOT NULL,
	[ValidTo] [datetime] NOT NULL,
	[VariantId] [bigint] NOT NULL,
	[Recversion] [int] NOT NULL,
	[Partition] [bigint] NOT NULL,
	[RecId] [bigint] NOT NULL,
 CONSTRAINT [PK_RETAILASSORTMENTLOOKUP_RECID] PRIMARY KEY NONCLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[RETAILASSORTMENTLOOKUPCHANNELGROUP]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[RETAILASSORTMENTLOOKUPCHANNELGROUP](
	[AssortmentId] [bigint] NOT NULL,
	[OMOperatingUnitId] [bigint] NOT NULL,
	[RetailChannelTable] [bigint] NOT NULL,
	[Recversion] [int] NOT NULL,
	[Partition] [bigint] NOT NULL,
	[RecId] [bigint] NOT NULL,
 CONSTRAINT [PK_RETAILASSORTMENTLOOKUPCHANNELGROUP_RECID] PRIMARY KEY NONCLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[RETAILASSORTMENTPRODUCTLINE]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[RETAILASSORTMENTPRODUCTLINE](
	[LineType] [int] NULL,
	[Status] [int] NULL,
	[AssortmentId] [nvarchar](500) NULL,
	[ItemId] [nvarchar](500) NULL,
	[Color] [nvarchar](500) NULL,
	[Size] [nvarchar](500) NULL,
	[ConfigurationId] [nvarchar](500) NULL,
	[LineNumber] [numeric](32, 16) NULL,
	[CategoryHierarchyName] [nvarchar](500) NULL,
	[Style] [nvarchar](500) NULL,
	[CategoryName] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[RETAILASSORTMENTTABLE]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[RETAILASSORTMENTTABLE](
	[AssortmentID] [nvarchar](50) NOT NULL,
	[PublishedDateTime] [datetime] NULL,
	[Name] [nvarchar](500) NULL,
	[Status] [int] NULL,
	[ValidTo] [datetime] NULL,
	[ValidFrom] [datetime] NULL,
	[PublicPartition] [bigint] NULL,
	[PublicRecId] [bigint] NULL,
 CONSTRAINT [PK_RETAILASSORTMENTTABLE] PRIMARY KEY CLUSTERED 
(
	[AssortmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[RETAILCHANNELTABLE]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[RETAILCHANNELTABLE](
	[LiveDatabaseConnectionProfileName] [nvarchar](500) NULL,
	[StatementMethod] [int] NULL,
	[ChannelProfileName] [nvarchar](500) NULL,
	[MaximumTextLengthOnReceipt] [int] NULL,
	[OnlineCatalogName] [nvarchar](500) NULL,
	[MaxRoundingAmount] [numeric](32, 16) NULL,
	[GeneratesItemLabels] [int] NULL,
	[MaxTransactionDifferenceAmount] [numeric](32, 16) NULL,
	[OpenFrom] [int] NULL,
	[TaxGroupCode] [nvarchar](500) NULL,
	[PriceIncludesSalesTax] [int] NULL,
	[MCRCustomerCreditRetailInfocodeId] [nvarchar](500) NULL,
	[MaxRoundingTaxAmount] [numeric](32, 16) NULL,
	[RoundingTaxAccount] [nvarchar](500) NULL,
	[ChannelType] [int] NULL,
	[ItemIdOnReceipt] [int] NULL,
	[CategoryHierarchyName] [nvarchar](500) NULL,
	[MCREnableOrderCompletion] [int] NULL,
	[EventNotificationProfileId] [nvarchar](500) NULL,
	[TransactionServiceProfile] [nvarchar](500) NULL,
	[TaxOverrideGroup] [bigint] NULL,
	[InventLocation] [nvarchar](500) NULL,
	[MaximumPostingDifference] [numeric](32, 16) NULL,
	[MCRPriceOverrideRetailInfocodeId] [nvarchar](500) NULL,
	[HideTrainingMode] [int] NULL,
	[SeparateStmtPerStaffTerminal] [int] NULL,
	[ReturnTaxGroup_W] [nvarchar](500) NULL,
	[DataAreaId] [nvarchar](500) NULL,
	[RetailReqPlanIdSched] [nvarchar](500) NULL,
	[TaxIdentificationNumber] [nvarchar](500) NULL,
	[ClosingMethod] [int] NULL,
	[EFTStoreNumber] [nvarchar](500) NULL,
	[OneStatementPerDay] [int] NULL,
	[ChannelTimeZone] [int] NULL,
	[UseCustomerBasedTax] [int] NULL,
	[CreateLabelsForZeroPrice] [int] NULL,
	[Currency] [nvarchar](500) NULL,
	[RetailChannelId] [nvarchar](50) NOT NULL,
	[CultureName] [nvarchar](500) NULL,
	[UseDestinationBasedTax] [int] NULL,
	[StoreNumber] [nvarchar](500) NULL,
	[StmtCalcBatchEndTime] [int] NULL,
	[StmtPostAsBusinessDay] [int] NULL,
	[InventLocationDataAreaId] [nvarchar](500) NULL,
	[OperatingUnitNumber] [nvarchar](500) NULL,
	[SQLServerName] [nvarchar](500) NULL,
	[NumberOfTopOrBottomLines] [int] NULL,
	[DefaultCustomerAccount] [nvarchar](500) NULL,
	[MCREnableDirectedSelling] [int] NULL,
	[ChannelTimeZoneInfoId] [nvarchar](500) NULL,
	[MCRReasonCodeRetailInfocodeId] [nvarchar](500) NULL,
	[TenderDeclarationCalculation] [int] NULL,
	[TaxGroupLegalEntity] [nvarchar](500) NULL,
	[StoreArea] [numeric](32, 16) NULL,
	[InventoryLookup] [int] NULL,
	[OfflineProfileName] [nvarchar](500) NULL,
	[InventLocationIdForCustomerOrder] [nvarchar](500) NULL,
	[UseDefaultCustAccount] [int] NULL,
	[ServiceChargePct] [numeric](32, 16) NULL,
	[PaymMode] [nvarchar](500) NULL,
	[FunctionalityProfile] [nvarchar](500) NULL,
	[DefaultDimensionDisplayValue] [nvarchar](500) NULL,
	[ServiceChargePrompt] [nvarchar](500) NULL,
	[RoundingAccountLedgerDimensionDisplayValue] [nvarchar](500) NULL,
	[RemoveAddTender] [nvarchar](500) NULL,
	[DatabaseName] [nvarchar](500) NULL,
	[MaxShiftDifferenceAmount] [numeric](32, 16) NULL,
	[OpenTo] [int] NULL,
	[MCREnableOrderPriceControl] [int] NULL,
	[UserName] [nvarchar](500) NULL,
	[PoItemFilter] [int] NULL,
	[Payment] [nvarchar](500) NULL,
	[Phone] [nvarchar](500) NULL,
	[OperatingUnitPartyNumber] [nvarchar](500) NULL,
	[DefaultCustomerLegalEntity] [nvarchar](500) NULL,
	[GeneratesShelfLabels] [int] NULL,
	[DisplayTaxPerTaxComponent] [int] NULL,
	[PublicOMOperatingUnitID] [bigint] NULL,
	[PublicCategoryHierarchy] [bigint] NULL,
	[PublicPartition] [bigint] NULL,
	[Name] [nvarchar](500) NULL,
 CONSTRAINT [PK_RETAILCHANNELTABLE] PRIMARY KEY CLUSTERED 
(
	[RetailChannelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[RetailTransactionSalesLineTable]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[RetailTransactionSalesLineTable](
	[KeyboardProductEntry] [int] NULL,
	[OperatingUnitNumber] [nvarchar](8) NOT NULL,
	[ItemSalesTaxGroup] [nvarchar](500) NULL,
	[IsReturnNoSale] [int] NULL,
	[TotalDiscountPercentage] [numeric](32, 16) NULL,
	[GiftCard] [int] NULL,
	[ElectronicDeliveryEmail] [nvarchar](500) NULL,
	[IsWeightProduct] [int] NULL,
	[ItemStyle] [nvarchar](500) NULL,
	[ReturnLineNumber] [numeric](32, 16) NULL,
	[RetailEmailAddressContent] [nvarchar](500) NULL,
	[ModeOfDelivery] [nvarchar](500) NULL,
	[LineNumber] [numeric](32, 16) NOT NULL,
	[NetPrice] [numeric](32, 16) NULL,
	[UnitPrice] [numeric](32, 16) NULL,
	[OfferNumber] [nvarchar](500) NULL,
	[CustomerAccount] [nvarchar](500) NULL,
	[IsLineDiscounted] [int] NULL,
	[PeriodicDiscountPercentage] [numeric](32, 16) NULL,
	[VariantNumber] [nvarchar](500) NULL,
	[CostAmount] [numeric](32, 16) NULL,
	[PeriodicDiscountGroup] [nvarchar](500) NULL,
	[ChannelListingID] [nvarchar](500) NULL,
	[ReasonCodeDiscount] [numeric](32, 16) NULL,
	[RequestedReceiptDate] [datetime] NULL,
	[TransactionStatus] [int] NULL,
	[Warehouse] [nvarchar](500) NULL,
	[IsScaleProduct] [int] NULL,
	[PriceGroups] [nvarchar](500) NULL,
	[CashDiscountAmount] [numeric](32, 16) NULL,
	[RequestedShipDate] [datetime] NULL,
	[ShelfNumber] [nvarchar](500) NULL,
	[LotID] [nvarchar](500) NULL,
	[SiteId] [nvarchar](500) NULL,
	[PeriodicDiscountAmount] [numeric](32, 16) NULL,
	[LineManualDiscountAmount] [numeric](32, 16) NULL,
	[TotalDiscount] [numeric](32, 16) NULL,
	[NetAmountInclusiveTax] [numeric](32, 16) NULL,
	[IsLinkedProductNotOriginal] [int] NULL,
	[ItemConfigId] [nvarchar](500) NULL,
	[Unit] [nvarchar](500) NULL,
	[OriginalPrice] [numeric](32, 16) NULL,
	[SectionNumber] [nvarchar](500) NULL,
	[NetAmount] [numeric](32, 16) NULL,
	[IsPriceChange] [int] NULL,
	[SerialNumber] [nvarchar](500) NULL,
	[SalesTaxAmount] [numeric](32, 16) NULL,
	[SalesTaxGroup] [nvarchar](500) NULL,
	[ProductScanned] [int] NULL,
	[TransactionCode] [int] NULL,
	[IsOriginalOfLinkedProductList] [int] NULL,
	[ItemRelation] [nvarchar](500) NULL,
	[ReturnTransactionNumber] [nvarchar](500) NULL,
	[LogisticLocationId] [nvarchar](500) NULL,
	[InventoryStatus] [int] NULL,
	[Terminal] [nvarchar](10) NOT NULL,
	[ItemColor] [nvarchar](500) NULL,
	[CustomerDiscount] [numeric](32, 16) NULL,
	[IsWeightManuallyEntered] [int] NULL,
	[CustomerInvoiceDiscountAmount] [numeric](32, 16) NULL,
	[ItemId] [nvarchar](500) NULL,
	[PriceInBarCode] [int] NULL,
	[BarCode] [nvarchar](500) NULL,
	[DiscountAmountForPrinting] [numeric](32, 16) NULL,
	[ItemSize] [nvarchar](500) NULL,
	[OriginalItemSalesTaxGroup] [nvarchar](500) NULL,
	[StandardNetPrice] [numeric](32, 16) NULL,
	[TotalDiscountInfoCodeLineNum] [numeric](32, 16) NULL,
	[ReturnTerminal] [nvarchar](500) NULL,
	[Price] [numeric](32, 16) NULL,
	[ReturnQuantity] [numeric](32, 16) NULL,
	[UnitQuantity] [numeric](32, 16) NULL,
	[Quantity] [numeric](32, 16) NULL,
	[TransactionNumber] [nvarchar](44) NOT NULL,
	[RFIDTagId] [nvarchar](500) NULL,
	[LogisticsPostalAddressValidFrom] [datetime] NULL,
	[DataAreaId] [nvarchar](4) NOT NULL,
	[CategoryName] [nvarchar](500) NULL,
	[Currency] [nvarchar](500) NULL,
	[ReceiptNumber] [nvarchar](500) NULL,
	[ReturnOperatingUnitNumber] [nvarchar](500) NULL,
	[LineDiscount] [numeric](32, 16) NULL,
	[OriginalSalesTaxGroup] [nvarchar](500) NULL,
	[LineManualDiscountPercentage] [numeric](32, 16) NULL,
	[CategoryHierarchyName] [nvarchar](500) NULL,
 CONSTRAINT [PK_RetailTransactionSalesLineTable] PRIMARY KEY CLUSTERED 
(
	[OperatingUnitNumber] ASC,
	[LineNumber] ASC,
	[Terminal] ASC,
	[TransactionNumber] ASC,
	[DataAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[RetailTransactionSalesLineTable_increment]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[RetailTransactionSalesLineTable_increment](
	[SALESTAXGROUP] [nvarchar](10) NOT NULL,
	[ITEMSALESTAXGROUP] [nvarchar](10) NOT NULL,
	[TERMINAL] [nvarchar](10) NOT NULL,
	[TRANSACTIONNUMBER] [nvarchar](44) NOT NULL,
	[BARCODE] [nvarchar](80) NOT NULL,
	[COSTAMOUNT] [numeric](32, 6) NOT NULL,
	[CURRENCY] [nvarchar](3) NOT NULL,
	[CUSTOMERACCOUNT] [nvarchar](38) NOT NULL,
	[CUSTOMERDISCOUNT] [numeric](32, 6) NOT NULL,
	[CUSTOMERINVOICEDISCOUNTAMOUNT] [numeric](32, 6) NOT NULL,
	[CASHDISCOUNTAMOUNT] [numeric](32, 6) NOT NULL,
	[PRICEGROUPS] [nvarchar](10) NOT NULL,
	[OFFERNUMBER] [nvarchar](40) NOT NULL,
	[DISCOUNTAMOUNTFORPRINTING] [numeric](32, 6) NOT NULL,
	[MODEOFDELIVERY] [nvarchar](10) NOT NULL,
	[ELECTRONICDELIVERYEMAIL] [nvarchar](80) NOT NULL,
	[RETAILEMAILADDRESSCONTENT] [nvarchar](400) NOT NULL,
	[GIFTCARD] [int] NOT NULL,
	[REASONCODEDISCOUNT] [numeric](32, 6) NOT NULL,
	[WAREHOUSE] [nvarchar](10) NOT NULL,
	[SERIALNUMBER] [nvarchar](20) NOT NULL,
	[SITEID] [nvarchar](10) NOT NULL,
	[INVENTORYSTATUS] [int] NOT NULL,
	[LOTID] [nvarchar](20) NOT NULL,
	[ITEMID] [nvarchar](20) NOT NULL,
	[PRODUCTSCANNED] [int] NOT NULL,
	[ITEMRELATION] [nvarchar](20) NOT NULL,
	[KEYBOARDPRODUCTENTRY] [int] NOT NULL,
	[LINEDISCOUNT] [numeric](32, 6) NOT NULL,
	[LINEMANUALDISCOUNTAMOUNT] [numeric](32, 6) NOT NULL,
	[LINEMANUALDISCOUNTPERCENTAGE] [numeric](32, 6) NOT NULL,
	[LINENUMBER] [numeric](32, 16) NOT NULL,
	[ISLINEDISCOUNTED] [int] NOT NULL,
	[ISLINKEDPRODUCTNOTORIGINAL] [int] NOT NULL,
	[CHANNELLISTINGID] [nvarchar](50) NOT NULL,
	[NETAMOUNT] [numeric](32, 6) NOT NULL,
	[NETAMOUNTINCLUSIVETAX] [numeric](32, 6) NOT NULL,
	[NETPRICE] [numeric](32, 6) NOT NULL,
	[ISORIGINALOFLINKEDPRODUCTLIST] [int] NOT NULL,
	[ORIGINALPRICE] [numeric](32, 6) NOT NULL,
	[ORIGINALSALESTAXGROUP] [nvarchar](10) NOT NULL,
	[ORIGINALITEMSALESTAXGROUP] [nvarchar](10) NOT NULL,
	[PERIODICDISCOUNTAMOUNT] [numeric](32, 6) NOT NULL,
	[PERIODICDISCOUNTGROUP] [nvarchar](10) NOT NULL,
	[PERIODICDISCOUNTPERCENTAGE] [numeric](32, 6) NOT NULL,
	[PRICE] [numeric](32, 6) NOT NULL,
	[ISPRICECHANGE] [int] NOT NULL,
	[PRICEINBARCODE] [int] NOT NULL,
	[QUANTITY] [numeric](32, 6) NOT NULL,
	[REQUESTEDRECEIPTDATE] [datetime] NOT NULL,
	[RECEIPTNUMBER] [nvarchar](18) NOT NULL,
	[RETURNLINENUMBER] [numeric](32, 16) NOT NULL,
	[ISRETURNNOSALE] [int] NOT NULL,
	[RETURNQUANTITY] [numeric](32, 6) NOT NULL,
	[RETURNTERMINAL] [nvarchar](10) NOT NULL,
	[RETURNTRANSACTIONNUMBER] [nvarchar](44) NOT NULL,
	[RFIDTAGID] [nvarchar](24) NOT NULL,
	[ISSCALEPRODUCT] [int] NOT NULL,
	[SECTIONNUMBER] [nvarchar](10) NOT NULL,
	[SHELFNUMBER] [nvarchar](10) NOT NULL,
	[REQUESTEDSHIPDATE] [datetime] NOT NULL,
	[STANDARDNETPRICE] [numeric](32, 6) NOT NULL,
	[SALESTAXAMOUNT] [numeric](32, 6) NOT NULL,
	[TOTALDISCOUNT] [numeric](32, 6) NOT NULL,
	[TOTALDISCOUNTINFOCODELINENUM] [numeric](32, 16) NOT NULL,
	[TOTALDISCOUNTPERCENTAGE] [numeric](32, 6) NOT NULL,
	[TRANSACTIONCODE] [int] NOT NULL,
	[TRANSACTIONSTATUS] [int] NOT NULL,
	[UNIT] [nvarchar](10) NOT NULL,
	[UNITPRICE] [numeric](32, 6) NOT NULL,
	[UNITQUANTITY] [numeric](32, 6) NOT NULL,
	[VARIANTNUMBER] [nvarchar](10) NOT NULL,
	[ISWEIGHTPRODUCT] [int] NOT NULL,
	[ISWEIGHTMANUALLYENTERED] [int] NOT NULL,
	[CATEGORYNAME] [nvarchar](254) NOT NULL,
	[CATEGORYHIERARCHYNAME] [nvarchar](128) NOT NULL,
	[LOGISTICSPOSTALADDRESSVALIDFROM] [datetime] NOT NULL,
	[LOGISTICLOCATIONID] [nvarchar](30) NOT NULL,
	[OPERATINGUNITNUMBER] [nvarchar](8) NOT NULL,
	[RETURNOPERATINGUNITNUMBER] [nvarchar](8) NOT NULL,
	[DATAAREAID] [nvarchar](4) NOT NULL,
 CONSTRAINT [PK_RetailTransactionSalesLine_increment] PRIMARY KEY CLUSTERED 
(
	[TERMINAL] ASC,
	[TRANSACTIONNUMBER] ASC,
	[LINENUMBER] ASC,
	[OPERATINGUNITNUMBER] ASC,
	[DATAAREAID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[RetailTransactionTable]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[RetailTransactionTable](
	[OperatingUnitNumber] [nvarchar](50) NOT NULL,
	[Shift] [nvarchar](500) NULL,
	[RreceiptId] [nvarchar](500) NULL,
	[LogisticsPostalZipCode] [nvarchar](500) NULL,
	[DiscountAmount] [numeric](32, 16) NULL,
	[TotalManualDiscountPercentage] [numeric](32, 16) NULL,
	[CustomerAccount] [nvarchar](500) NULL,
	[CostAmount] [numeric](32, 16) NULL,
	[AmountPostedToAccount] [numeric](32, 16) NULL,
	[ChannelReferenceId] [nvarchar](500) NULL,
	[LogisticPostalAddressValidTo] [datetime] NULL,
	[TransactionDate] [datetime] NULL,
	[PaymentAmount] [numeric](32, 16) NULL,
	[TransactionStatus] [int] NULL,
	[Warehouse] [nvarchar](500) NULL,
	[ToAccount] [int] NULL,
	[TransactionType] [int] NULL,
	[ShippingDateRequested] [datetime] NULL,
	[LogisticsPostalState] [nvarchar](500) NULL,
	[SiteId] [nvarchar](500) NULL,
	[PostAsShipment] [int] NULL,
	[SalesOrderId] [nvarchar](500) NULL,
	[ItemsPosted] [int] NULL,
	[NetAmount] [numeric](32, 16) NULL,
	[LoyaltyCardId] [nvarchar](500) NULL,
	[LogisticsPostalStreet] [nvarchar](500) NULL,
	[LogisticsPostalCity] [nvarchar](500) NULL,
	[InfocodeDiscountGroup] [nvarchar](500) NULL,
	[DeliveryMode] [nvarchar](500) NULL,
	[Staff] [nvarchar](500) NULL,
	[CustomerDiscountAmount] [numeric](32, 16) NULL,
	[SalesPaymentDifference] [numeric](32, 16) NULL,
	[ExchangeRate] [numeric](32, 16) NULL,
	[SalesInvoiceAmount] [numeric](32, 16) NULL,
	[RefundReceiptId] [nvarchar](500) NULL,
	[TotalDiscountAmount] [numeric](32, 16) NULL,
	[InvoiceId] [nvarchar](500) NULL,
	[Terminal] [nvarchar](50) NOT NULL,
	[IncomeExpenseAmount] [numeric](32, 16) NULL,
	[GrossAmount] [numeric](32, 16) NULL,
	[LogisticsLocationId] [nvarchar](500) NULL,
	[TransactionTime] [int] NULL,
	[CreatedOffline] [int] NULL,
	[TransactionNumber] [nvarchar](50) NOT NULL,
	[LogisticsPostalAddressValidFrom] [datetime] NULL,
	[DataAreaId] [nvarchar](10) NOT NULL,
	[SaleIsReturnSale] [int] NULL,
	[Currency] [nvarchar](500) NULL,
	[LogisticsPostalCounty] [nvarchar](500) NULL,
	[SalesOrderAmount] [numeric](32, 16) NULL,
	[TotalManualDiscountAmount] [numeric](32, 16) NULL,
	[BatchID] [bigint] NULL,
 CONSTRAINT [PK_RetailTransactionTable] PRIMARY KEY CLUSTERED 
(
	[OperatingUnitNumber] ASC,
	[Terminal] ASC,
	[TransactionNumber] ASC,
	[DataAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[RetailTransactionTable_increment]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[RetailTransactionTable_increment](
	[TERMINAL] [nvarchar](50) NOT NULL,
	[AMOUNTPOSTEDTOACCOUNT] [numeric](32, 16) NULL,
	[CHANNELREFERENCEID] [nvarchar](500) NULL,
	[COSTAMOUNT] [numeric](32, 16) NULL,
	[CREATEDOFFLINE] [int] NULL,
	[CURRENCY] [nvarchar](500) NULL,
	[CUSTOMERACCOUNT] [nvarchar](500) NULL,
	[CUSTOMERDISCOUNTAMOUNT] [numeric](32, 16) NULL,
	[DISCOUNTAMOUNT] [numeric](32, 16) NULL,
	[DELIVERYMODE] [nvarchar](500) NULL,
	[TRANSACTIONSTATUS] [int] NULL,
	[EXCHANGERATE] [numeric](32, 16) NULL,
	[GROSSAMOUNT] [numeric](32, 16) NULL,
	[INCOMEEXPENSEAMOUNT] [numeric](32, 16) NULL,
	[INFOCODEDISCOUNTGROUP] [nvarchar](500) NULL,
	[WAREHOUSE] [nvarchar](500) NULL,
	[SITEID] [nvarchar](500) NULL,
	[INVOICEID] [nvarchar](500) NULL,
	[ITEMSPOSTED] [int] NULL,
	[LOYALTYCARDID] [nvarchar](500) NULL,
	[NETAMOUNT] [numeric](32, 16) NULL,
	[PAYMENTAMOUNT] [numeric](32, 16) NULL,
	[POSTASSHIPMENT] [int] NULL,
	[RRECEIPTID] [nvarchar](500) NULL,
	[REFUNDRECEIPTID] [nvarchar](500) NULL,
	[SALEISRETURNSALE] [int] NULL,
	[SALESINVOICEAMOUNT] [numeric](32, 16) NULL,
	[SALESORDERAMOUNT] [numeric](32, 16) NULL,
	[SALESORDERID] [nvarchar](500) NULL,
	[SALESPAYMENTDIFFERENCE] [numeric](32, 16) NULL,
	[SHIFT] [nvarchar](500) NULL,
	[SHIPPINGDATEREQUESTED] [datetime] NULL,
	[STAFF] [nvarchar](500) NULL,
	[TOACCOUNT] [int] NULL,
	[TOTALDISCOUNTAMOUNT] [numeric](32, 16) NULL,
	[TOTALMANUALDISCOUNTAMOUNT] [numeric](32, 16) NULL,
	[TOTALMANUALDISCOUNTPERCENTAGE] [numeric](32, 16) NULL,
	[TRANSACTIONNUMBER] [nvarchar](50) NOT NULL,
	[TRANSACTIONDATE] [datetime] NULL,
	[TRANSACTIONTIME] [int] NULL,
	[TRANSACTIONTYPE] [int] NULL,
	[LOGISTICSLOCATIONID] [nvarchar](500) NULL,
	[LOGISTICSPOSTALCITY] [nvarchar](500) NULL,
	[LOGISTICSPOSTALCOUNTY] [nvarchar](500) NULL,
	[LOGISTICSPOSTALSTATE] [nvarchar](500) NULL,
	[LOGISTICSPOSTALSTREET] [nvarchar](500) NULL,
	[LOGISTICSPOSTALZIPCODE] [nvarchar](500) NULL,
	[LOGISTICSPOSTALADDRESSVALIDFROM] [datetime] NULL,
	[LOGISTICPOSTALADDRESSVALIDTO] [datetime] NULL,
	[OPERATINGUNITNUMBER] [nvarchar](50) NOT NULL,
	[DATAAREAID] [nvarchar](10) NOT NULL,
	[BatchID] [bigint] NULL,
 CONSTRAINT [PK_RetailTransactionTable_increment] PRIMARY KEY CLUSTERED 
(
	[OPERATINGUNITNUMBER] ASC,
	[TERMINAL] ASC,
	[TRANSACTIONNUMBER] ASC,
	[DATAAREAID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[SeasonGroup]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[SeasonGroup](
	[GroupId] [nvarchar](10) NULL,
	[GroupDescription] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[SeasonTable]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[SeasonTable](
	[SeasonCode] [nvarchar](50) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[KRFRetailSeasonGroupId] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[DataAreaId] [nvarchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[UNITOFMEASURE]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[UNITOFMEASURE](
	[RecId] [bigint] NOT NULL,
	[Partition] [bigint] NOT NULL,
	[DecimalPrecision] [int] NOT NULL,
	[IsBaseUnit] [int] NOT NULL,
	[IsSystemUnit] [int] NOT NULL,
	[Symbol] [nvarchar](10) NOT NULL,
	[SystemOfUnits] [int] NOT NULL,
	[UnitOfMeasureClass] [int] NOT NULL,
 CONSTRAINT [PK_UnitOfMeasure_1] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[UNITOFMEASURECONVERSION]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[UNITOFMEASURECONVERSION](
	[RecId] [bigint] NOT NULL,
	[Partition] [bigint] NOT NULL,
	[Denominator] [int] NOT NULL,
	[Factor] [numeric](32, 16) NOT NULL,
	[FromUnitOfMeasure] [bigint] NOT NULL,
	[InnerOffset] [numeric](32, 16) NOT NULL,
	[Numerator] [int] NOT NULL,
	[OuterOffset] [numeric](32, 16) NOT NULL,
	[Product] [bigint] NOT NULL,
	[Rounding] [int] NOT NULL,
	[ToUnitOfMeasure] [bigint] NOT NULL,
 CONSTRAINT [PK_UnitofMeasureConversion_1] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ax].[VENDTABLE]    Script Date: 10.10.2017 12:17:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ax].[VENDTABLE](
	[OIDNomineeDetails] [nvarchar](500) NULL,
	[PaymentFeeGroupId] [nvarchar](500) NULL,
	[PaymentId] [nvarchar](500) NULL,
	[VendorInvoiceDeclarationId] [nvarchar](500) NULL,
	[AddressStateId] [nvarchar](500) NULL,
	[DefaultPaymentDayName] [nvarchar](500) NULL,
	[VendorName] [nvarchar](500) NULL,
	[AddressDescription] [nvarchar](500) NULL,
	[PurchaseWorkCalendarId] [nvarchar](500) NULL,
	[AddressBooks] [nvarchar](500) NULL,
	[IsMinorityOwned] [int] NULL,
	[VendorExceptionGroupId] [nvarchar](500) NULL,
	[PersonBirthYear] [int] NULL,
	[AddressCity] [nvarchar](500) NULL,
	[DefaultPurchaseOrderPoolId] [nvarchar](500) NULL,
	[IsChangeMangementOverrideByVendorAllowed] [int] NULL,
	[AddressLatitude] [numeric](32, 16) NULL,
	[AddressZipCode] [nvarchar](500) NULL,
	[IsVendorLocatedInHUBZone] [int] NULL,
	[PANReferenceNumber] [nvarchar](500) NULL,
	[MainContactPersonnelNumber] [nvarchar](500) NULL,
	[SiretNumber] [nvarchar](500) NULL,
	[IsIncomingFiscalDocumentGenerated] [int] NULL,
	[PersonPersonalSuffix] [nvarchar](500) NULL,
	[PersonAnniversaryDay] [int] NULL,
	[IsW9Received] [int] NULL,
	[PurchaseOrderConsolidationDayOfMonth] [int] NULL,
	[CUSIPIdentificationNumber] [nvarchar](500) NULL,
	[PersonProfessionalTitle] [nvarchar](500) NULL,
	[BusinessSegmentCode] [nvarchar](500) NULL,
	[DefaultTotalDiscountVendorGroupCode] [nvarchar](500) NULL,
	[DefaultDeliveryModeId] [nvarchar](500) NULL,
	[PrimaryPhoneNumber] [nvarchar](500) NULL,
	[Notes] [nvarchar](500) NULL,
	[PrimaryURLPurpose] [nvarchar](500) NULL,
	[NumberSequenceGroupId] [nvarchar](500) NULL,
	[ForeignVendor] [int] NULL,
	[IsServiceVeteranOwned] [int] NULL,
	[PrimaryTelexPurpose] [nvarchar](500) NULL,
	[BrazilianCCM] [nvarchar](500) NULL,
	[CISStatus] [int] NULL,
	[VendorInvoiceLineMatchingPolicy] [int] NULL,
	[VendorPriceToleranceGroupId] [nvarchar](500) NULL,
	[IsICMSContributor] [int] NULL,
	[LineOfBusinessId] [nvarchar](500) NULL,
	[Tax1099NameToUse] [int] NULL,
	[CompositionScheme] [int] NULL,
	[VendorPortalAdministratorContactPersonId] [nvarchar](500) NULL,
	[Tax1099Type] [int] NULL,
	[TCSGroup] [nvarchar](500) NULL,
	[PersonAnniversaryMonth] [int] NULL,
	[MultilineDiscountVendorGroupCode] [nvarchar](500) NULL,
	[AddressStreet] [nvarchar](500) NULL,
	[PurchaseRebateVendorGroupId] [nvarchar](500) NULL,
	[IsSmallBusiness] [int] NULL,
	[CompanyChainName] [nvarchar](500) NULL,
	[PersonGender] [int] NULL,
	[IsVendorPayingBankPaymentFee] [int] NULL,
	[ChargeVendorGroupId] [nvarchar](500) NULL,
	[VendorAccountNumber] [nvarchar](500) NULL,
	[AddressValidTo] [datetime] NULL,
	[IsVendorLocallyOwned] [int] NULL,
	[DefaultInventoryStatusId] [nvarchar](500) NULL,
	[AddressCountyId] [nvarchar](500) NULL,
	[PrimaryEmailAddressPurpose] [nvarchar](500) NULL,
	[CommercialRegisterInsetNumber] [nvarchar](500) NULL,
	[DefaultOffsetAccountType] [int] NULL,
	[FiscalOperationPresenceType] [int] NULL,
	[DestinationCode] [nvarchar](500) NULL,
	[PersonInitials] [nvarchar](500) NULL,
	[PersonMaritalStatus] [int] NULL,
	[DataAreaId] [nvarchar](500) NULL,
	[LanguageId] [nvarchar](500) NULL,
	[ForeignerId] [nvarchar](500) NULL,
	[AddressTimeZone] [int] NULL,
	[ForeignVendorTaxRegistrationId] [nvarchar](500) NULL,
	[AddressCountryRegionId] [nvarchar](500) NULL,
	[BirthCountyCode] [nvarchar](500) NULL,
	[CommercialRegisterName] [nvarchar](500) NULL,
	[RFCFederalTaxNumber] [nvarchar](500) NULL,
	[CUSIPDetails] [nvarchar](500) NULL,
	[PaymentTransactionCode] [nvarchar](500) NULL,
	[IsPurchaseOrderChangeRequestOverrideAllowed] [int] NULL,
	[OurAccountNumber] [nvarchar](500) NULL,
	[FormattedPrimaryAddress] [nvarchar](500) NULL,
	[Tax1099BoxId] [nvarchar](500) NULL,
	[CISCompanyRegistrationNumber] [nvarchar](500) NULL,
	[FiscalCode] [nvarchar](500) NULL,
	[DefaultDeliveryTermsCode] [nvarchar](500) NULL,
	[BusinessSubsegmentCode] [nvarchar](500) NULL,
	[OrganizationABCCode] [int] NULL,
	[CreditLimit] [numeric](32, 16) NULL,
	[CISUniqueTaxPayerReference] [nvarchar](500) NULL,
	[PrimaryTelex] [nvarchar](500) NULL,
	[HasOnlyTakenBids] [int] NULL,
	[CISVerificationDate] [datetime] NULL,
	[TaxExemptNumber] [nvarchar](500) NULL,
	[BrazilianINSSCEI] [nvarchar](500) NULL,
	[OIDInvestorType] [int] NULL,
	[VendorGroupId] [nvarchar](500) NULL,
	[PrimaryFaxNumberPurpose] [nvarchar](500) NULL,
	[PersonChildrenNames] [nvarchar](500) NULL,
	[BuyerGroupId] [nvarchar](500) NULL,
	[BankAccountId] [nvarchar](500) NULL,
	[UniquePopulationRegistryCode] [nvarchar](500) NULL,
	[FactoringVendorAccountNumber] [nvarchar](500) NULL,
	[CashDiscountCode] [nvarchar](500) NULL,
	[VendorPartyType] [nvarchar](500) NULL,
	[IsPrimaryPhoneNumberMobile] [int] NULL,
	[PersonPhoneticFirstName] [nvarchar](500) NULL,
	[PersonAnniversaryYear] [int] NULL,
	[CISVerificationNumber] [nvarchar](500) NULL,
	[TDSGroup] [nvarchar](500) NULL,
	[PersonProfessionalSuffix] [nvarchar](500) NULL,
	[DefaultPaymentScheduleName] [nvarchar](500) NULL,
	[CurrencyCode] [nvarchar](500) NULL,
	[PersonBirthMonth] [int] NULL,
	[NatureOfAssessee] [int] NULL,
	[NAFCode] [nvarchar](500) NULL,
	[ArePricesIncludingSalesTax] [int] NULL,
	[DefaultOffsetLedgerAccountDisplayValue] [nvarchar](500) NULL,
	[NationalRegistryNumberId] [nvarchar](500) NULL,
	[CentralBankPurposeText] [nvarchar](500) NULL,
	[AddressValidFrom] [datetime] NULL,
	[Tax1099IdType] [int] NULL,
	[DefaultPaymentTermsName] [nvarchar](500) NULL,
	[OrganizationEmployeeAmount] [int] NULL,
	[VendorPortalCollaborationMethod] [int] NULL,
	[PersonHobbies] [nvarchar](500) NULL,
	[InvoiceVendorAccountNumber] [nvarchar](500) NULL,
	[WillPurchaseOrderIncludePricesAndAmounts] [int] NULL,
	[AddressDistrictName] [nvarchar](500) NULL,
	[CommercialRegisterSection] [nvarchar](500) NULL,
	[SSIValidityDate] [datetime] NULL,
	[ProductDescriptionVendorGroupId] [nvarchar](500) NULL,
	[AddressLocationRoles] [nvarchar](500) NULL,
	[ISNationalRegistryNumber] [nvarchar](500) NULL,
	[PrimaryEmailAddress] [nvarchar](500) NULL,
	[NameControl] [nvarchar](500) NULL,
	[DefaultProcumentWarehouseId] [nvarchar](500) NULL,
	[Nationality] [nvarchar](500) NULL,
	[PrimaryPhoneNumberPurpose] [nvarchar](500) NULL,
	[SalesTaxGroupCode] [nvarchar](500) NULL,
	[CompanyType] [int] NULL,
	[DIOTCountryCode] [nvarchar](500) NULL,
	[DefaultSupplementaryProductVendorGroupId] [nvarchar](500) NULL,
	[PrimaryContactPersonId] [nvarchar](500) NULL,
	[PersonPersonalTitle] [nvarchar](500) NULL,
	[DIOTVendorType] [int] NULL,
	[DIOTOperationType] [int] NULL,
	[BrazilianCNAE] [nvarchar](500) NULL,
	[CreditRating] [nvarchar](500) NULL,
	[IsOwnerDisabled] [int] NULL,
	[OnHoldStatus] [int] NULL,
	[EnterpriseNumber] [nvarchar](500) NULL,
	[IsFlaggedWithSecondTIN] [int] NULL,
	[VendorKnownAsName] [nvarchar](500) NULL,
	[SSIVendor] [int] NULL,
	[IsForeignEntity] [int] NULL,
	[OrganizationPhoneticName] [nvarchar](500) NULL,
	[PANNumber] [nvarchar](500) NULL,
	[PaymentSpecificationId] [nvarchar](500) NULL,
	[DefaultVendorPaymentMethodName] [nvarchar](500) NULL,
	[PrimaryURL] [nvarchar](500) NULL,
	[CISNationalInsuranceNumber] [nvarchar](500) NULL,
	[VendorHoldReleaseDate] [datetime] NULL,
	[VendorSearchName] [nvarchar](500) NULL,
	[DefaultLedgerDimensionDisplayValue] [nvarchar](500) NULL,
	[Tax1099DoingBusinessAsName] [nvarchar](500) NULL,
	[PersonBirthDay] [int] NULL,
	[IsReportingTax1099] [int] NULL,
	[PreferentialVendor] [int] NULL,
	[DefaultPurchaseSiteId] [nvarchar](500) NULL,
	[AddressLongitude] [numeric](32, 16) NULL,
	[PriceVendorGroupId] [nvarchar](500) NULL,
	[ClearingPeriodPaymentTermsId] [nvarchar](500) NULL,
	[CentralBankPurposeCode] [nvarchar](500) NULL,
	[OrganizationNumber] [nvarchar](500) NULL,
	[BrazilianIE] [nvarchar](500) NULL,
	[DefaultCashDiscountUsage] [int] NULL,
	[IsOneTimeVendor] [int] NULL,
	[IsW9CheckingEnabled] [int] NULL,
	[PersonPhoneticMiddleName] [nvarchar](500) NULL,
	[WillReceiptsListProcessingSummaryUpdatePurchaseOrder] [int] NULL,
	[StateInscription] [nvarchar](500) NULL,
	[PANStatus] [int] NULL,
	[IsWomanOwner] [int] NULL,
	[VendorPartyNumber] [nvarchar](500) NULL,
	[WillPurchaseOrderProcessingSummaryUpdatePurchaseOrder] [int] NULL,
	[BrazilianCNPJOrCPF] [nvarchar](500) NULL,
	[BrazilianNIT] [nvarchar](500) NULL,
	[IsPrimaryEmailAddressIMEnabled] [int] NULL,
	[PrimaryFaxNumber] [nvarchar](500) NULL,
	[WillProductReceiptProcessingSummaryUpdatePurchaseOrder] [int] NULL,
	[IsWithholdingTaxCalculated] [int] NULL,
	[PersonPhoneticLastName] [nvarchar](500) NULL,
	[IsServiceDeliveryAddressBased] [int] NULL,
	[IsPurchaseConsumed] [int] NULL,
	[WillInvoiceProcessingSummaryUpdatePurchaseOrder] [int] NULL,
	[GTAVendor] [int] NULL,
	[IsCUSIPIdentificationNumberApplicable] [int] NULL,
	[LineDiscountVendorGroupCode] [nvarchar](500) NULL,
	[WithholdingTaxGroupCode] [nvarchar](500) NULL,
	[Tax1099FederalTaxId] [nvarchar](500) NULL,
	[EthnicOriginId] [nvarchar](500) NULL,
	[UPSFreightZone] [nvarchar](500) NULL,
	[AddressBrazilianCNPJOrCPF] [nvarchar](500) NULL,
	[WithholdingTaxVendorType] [int] NULL,
	[IsChangeManagementActivated] [int] NULL,
	[ZakatServiceType] [nvarchar](500) NULL,
	[BirthPlace] [nvarchar](500) NULL,
	[AddressLocationId] [nvarchar](500) NULL,
	[VendorPaymentFinancialInterestCode] [nvarchar](500) NULL,
	[FiscalDocumentIncomeCode] [nvarchar](500) NULL,
	[PrimaryTelexDescription] [nvarchar](500) NULL,
	[ZakatRegistrationNumber] [nvarchar](500) NULL,
	[PrimaryPhoneNumberExtension] [nvarchar](500) NULL,
	[PrimaryURLDescription] [nvarchar](500) NULL,
	[ZakatFileNumber] [nvarchar](500) NULL,
	[PrimaryFaxNumberExtension] [nvarchar](500) NULL,
	[PrimaryPhoneNumberDescription] [nvarchar](500) NULL,
	[AddressBrazilianIE] [nvarchar](500) NULL,
	[PrimaryFaxNumberDescription] [nvarchar](500) NULL,
	[VendorPaymentFineCode] [nvarchar](500) NULL,
	[PrimaryEmailAddressDescription] [nvarchar](500) NULL,
	[ResidenceForeignCountryRegionId] [nvarchar](500) NULL,
	[ElectronicLocationId] [nvarchar](500) NULL,
	[IsSubcontractor] [int] NULL
) ON [PRIMARY]

GO
