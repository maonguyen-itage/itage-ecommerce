using Entities.DBModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entities.DBInheritedModels
{
    public class InheritedEntitiesApiModule
    {
        public int SimpleId { get; set; }
    }

    public class ApiProductEntity : Product
    {
        public int TotalRecords { get; set; }
        public object? ProductImagesJson { get; set; }
        public object? ProductColorsJson { get; set; }
        public object? ProductSizesJson { get; set; }
        public object? ProductTagsJson { get; set; }
        public object? ProductShipMethodsJson { get; set; }
        public decimal? Rating { get; set; }
        public decimal? TotalReviews { get; set; }
      
        public int Quantity { get; set; }

        public int? DiscountId { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public decimal? OrderItemDiscount { get; set; }
        public decimal? ItemSubTotal { get; set; }
        public bool? IsDiscountCalculated { get; set; }
        public string? CouponCode { get; set; }
        public string? VendorName { get; set; }
        public string? ManufacturerName { get; set; }
        public int? CategoryID { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public string? CategoryName { get; set; }
        public string? DiscEndDate { get; set; }

        public List<CartProductAllAttributes>? ProductAllSelectedAttributes { get; set; }
    }

    public class CartProductSelectedAttributes
    {
        public int ProductId { get; set; }
        public int ProductAttributeID { get; set; }
        public int PrimaryKeyValue { get; set; }
    }

    public class CartProductAllAttributes
    {
        public int ProductAttributeMappingID { get; set; }
        public int ProductAttributeID { get; set; }
        public bool IsRequiredAttribute { get; set; }
        public bool PriceAdjustmentType { get; set; }
        public decimal PriceAdjustment { get; set; }
        public decimal AdditionalPrice { get; set; }
        public string? AttributeDisplayName { get; set; }
        public int PrimaryKeyValue { get; set; }
        public string? PrimaryKeyDisplayValue { get; set; }
    }

    public class CartCustomerProducts
    {
        public int ProductId { get; set; }
        public List<CartProductSelectedAttributes>? productSelectedAttributes { get; set; }
        public int Quantity { get; set; }
        public string? DefaultImage { get; set; }
    }

    //--Do not add any other attribute in below "ProductsIds" class, otherwise it will create issue because this class
    //--use in many places with same structure specially in dynamic api in sql and react site
    public class ProductsIds
    {
        public int ProductId { get; set; }
    }

    public class CustomerFinalOrderItemData
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ItemPriceTotal { get; set; }
       
        public bool IsShippingFree { get; set; }
        public decimal ShippingChargesTotal { get; set; }
        public decimal OrderItemAttributeChargesTotal { get; set; }
        public decimal ItemSubTotal { get; set; }
        public int? DiscountId { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public decimal? OrderItemDiscountTotal { get; set; }
        public bool? IsDiscountCalculated { get; set; }
        public string? CouponCode { get; set; }
        public List<CartProductAllAttributes>? ProductAllSelectedAttributes { get; set; }
    }
}
