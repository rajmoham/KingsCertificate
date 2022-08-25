using System;
using System.Collections.Generic;

namespace KC.Models
{
    public class SearchResult
    {
        public string FullDisplayName { get; set; }
        public string DefinitionName { get; set; }
        public object SearchTerm { get; set; }
        public bool HasPriceRange { get; set; }
        public string PriceListId { get; set; }
        public double ListPrice { get; set; }
        public object Price { get; set; }
        public double ItemFormat { get; set; }
        public bool IsSeasonalProductAvailableForCollect { get; set; }
        public object QtyMaxReachMessage { get; set; }
        public string ProductId { get; set; }
        public object VariantId { get; set; }
        public bool HasVariants { get; set; }
        public string Sku { get; set; }
        public string DisplayName { get; set; }
        public object Brand { get; set; }
        public object BrandId { get; set; }
        public object Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string FallbackImageUrl { get; set; }
        public bool IsAvailableToSell { get; set; }
        public string CategoryId { get; set; }
        public bool IsRecurringOrderEligible { get; set; }
        public object RecurringOrderProgramName { get; set; }
        public string DisplayPrice { get; set; }
        public object DisplaySpecialPrice { get; set; }
        public bool IsOnSale { get; set; }
        public string SellingMethod { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool IsUnit { get; set; }
        public bool IsUnitMeasure { get; set; }
        public bool IsApproxUnit { get; set; }
        public string JsonContext { get; set; }
        public object DefaultListPrice { get; set; }
        public object CurrentListPrice { get; set; }
        public string SizeVolume { get; set; }
        public string UnitPriceDeclaration { get; set; }
        public object UnitPrice { get; set; }
        public object HasUnitPrice { get; set; }
        public object ImageRibbonText { get; set; }
        public object ImageBannerText { get; set; }
        public object ImageBadges { get; set; }
        public object ImageBadges_Facet { get; set; }
    }

    public class Quantity
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public string JsonContext { get; set; }
    }

    public class Product
    {
        public string productId { get; set; }
        public string CategoryId { get; set; }
        public string Sku { get; set; }
        public string CreateAccountUrl { get; set; }
        public double Weight { get; set; }
        public string WeightUOM { get; set; }
        public double ItemFormat { get; set; }
        public string SellingMethod { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool IsUnit { get; set; }
        public bool IsUnitMeasure { get; set; }
        public bool IsApproxUnit { get; set; }
        public Quantity Quantity { get; set; }
        public string Brand { get; set; }
        public string DisplayName { get; set; }
        public string GeneralDisclaimer { get; set; }
        public bool HasGeneralDisclaimer { get; set; }
        public object IsProofOfAgeRequired { get; set; }
        public string SizeVolume { get; set; }
        public double MaxPurchaseQuantity { get; set; }
        public string UnitPriceDeclaration { get; set; }
        public string ImageRibbonText { get; set; }
        public string ImageBannerText { get; set; }
        public object ImageBadges { get; set; }
        public object ImageBadges_Facet { get; set; }
        public string ProductUrl { get; set; }
        public string BrandName { get; set; }
        public string PrimaryCategoryId { get; set; }
        public string PrimaryCategoryName { get; set; }
        public string SecondaryCategoryId { get; set; }
        public string SecondaryCategoryName { get; set; }
        public string TertiaryCategoryId { get; set; }
        public string TertiaryCategoryName { get; set; }
        public SearchResult FurtherDetails { get; set; }
        public Dictionary<string, string> ProdInfoPairs { get; set; }
        public int QuantityInCart { get; set; }
        public string returnUrl { get; set; }
        public bool SameHeight { get; set; }
    }
}
