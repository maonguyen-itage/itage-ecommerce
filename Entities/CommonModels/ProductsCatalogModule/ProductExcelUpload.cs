using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels.ProductsCatalogModule
{
    public class ProductExcelUpload
    {
        public string? ProductId { get; set; }
        public string? ProductName { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public string? FullDescription { get; set; } = null!;
        public string? VendorId { get; set; }
        public string? ManufacturerId { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
        public string Price { get; set; }
        public string? OldPrice { get; set; }
        public string? IsTaxExempt { get; set; }
        public string? IsShippingFree { get; set; }
        public string? EstimatedShippingDays { get; set; }
        public string? ShippingCharges { get; set; }
        public string? ShowOnHomePage { get; set; }
        public string? AllowCustomerReviews { get; set; }
        public string? ProductViewCount { get; set; }
        public string? ProductSalesCount { get; set; }
        public string? IsReturnAble { get; set; }
        public string? IsDigitalProduct { get; set; }
        public string? IsDiscountAllowed { get; set; }
        public string? SellStartDatetimeUtc { get; set; }
        public string? SellEndDatetimeUtc { get; set; }
        public string? Sku { get; set; }
        public string? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public string? WarehouseId { get; set; }
        public string? InventoryMethodId { get; set; }
        public string? StockQuantity { get; set; }
        public string? IsBoundToStockQuantity { get; set; }
        public string? DisplayStockQuantity { get; set; }
        public string? OrderMinimumQuantity { get; set; }
        public string? OrderMaximumQuantity { get; set; }
        public string? MarkAsNew { get; set; }
        public string? DisplaySeqNo { get; set; }
        public string? IsActive { get; set; }
        public string? CategoriesIdsCommaSeperated { get; set; }
        public string? TagsIdsCommaSeperated { get; set; }
        public string? ShippingMethodsIdsCommaSeperated { get; set; }
        public string? ColorsIdsCommaSeperated { get; set; }
        public string? SizeIdsCommaSeperated { get; set; }
        public string? ImagesIdsCommaSeperated { get; set; }
        public string? LoginUserId { get; set; }
    }
}
