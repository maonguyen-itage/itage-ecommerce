using Entities.DBModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DBInheritedModels
{
    public class InheritedEntitiesLevelTwo
    {
        public int id { get; set; }
    }

    public class ProductEntity : Product
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public int? DataExportType { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }

        //--Other Fields
        public string? SelectedCategoriesJson { get; set; }
        public string? SelectedTagsJson { get; set; }
        public string? SelectedDiscountsJson { get; set; }
        public string? SelectedShippingMethodsJson { get; set; }

        public string? ProductAttributesJson { get; set; }
        public int? CategoryId { get; set; }

        public IFormFile[]? ProductImages { get; set; }
        public string? ProductImagesJson { get; set; }
        public string? DigitalProductFilesUrlJson { get; set; }
        public string? AttachmentURL { get; set; }
        public string? DownloadUrlOption { get; set; }
        public string? DigitalProductExistingUrl { get; set; }
        public IFormFile[]? DigitalProductNewFileUpload { get; set; }
        
        public bool IsDiscountCreatePageSearchEnabled { get; set; }
        public decimal Rating { get; set; }
        public int TotalReviews { get; set; }
    }

    public class UserEntity : User
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public IFormFile? ProfilePictureFile { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public int? StateProvinceId { get; set; }
        public int? CityId { get; set; }
        public int? RoleId { get; set; }
        public string? AddressLineOne { get; set; }
        public string? AddressLineTwo { get; set; }
        public string? PostalCode { get; set; }
        public int? DataOperationType { get; set; }
        public string? UserTypeName { get; set; }
    }

    public class DiscountEntity : Discount
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }

        public int? IsActiveSelected { get; set; } 
        public int? TotalUsage { get; set; } 

        public string? DiscountTypeName { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }

        public int? CategoryID { get; set; }
        
        public string? DiscountAssociatedProductsJson { get; set; }
        public string? DiscountAssociatedCategoriesJson { get; set; }
    }

    public class EntityEntity : Entity
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? EntityTypeName { get; set; }
      
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }
    }


    public class OrderEntity : Order
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public int? DataExportType { get; set; }
        public int? VendorId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmailAddress { get; set; }
        public string? CustomerMobileNo { get; set; }

        public string? ProductName { get; set; }
        public int? ProductId { get; set; }
        public string? LatestStatusName { get; set; }
        public string? OrderShippingMasterDataJson { get; set; }
        public string? OrderShippingDetailsDataJson { get; set; }
        public string? OrdersItemsJson { get; set; }
        public string? OrderPaymentDetailsJson { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }
    }

    public class OrderShippingDetailEntity : OrderShippingDetail
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? VendorId { get; set; }

        public string? VendorFirstName { get; set; }
        public string? VendorLastName { get; set; }

        public string? ProductName { get; set; }
        public string? ProductDefaultImage { get; set; }
        public int? ProductId { get; set; }
        public string? ProductShippingMethods { get; set; }
        public string? OrderShippingDetailItemsJson { get; set; }

        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }
    }

    public class OrderItemEntity : OrderItem
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? VendorId { get; set; }

        public string? VendorFirstName { get; set; }
        public string? VendorLastName { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDefaultImage { get; set; }
      

        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }
    }

    public class OrdersPaymentEntity : OrdersPayment
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? PaymentMethodName { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }
    }


    public class ScrnsLocalizationEntity : ScrnsLocalization
    {
        public int? EntityId { get; set; }
        public string? ScreenName { get; set; }
        public string? LanguageName { get; set; }
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    } 
    public class VendorsAccountRequestEntity : VendorsAccountRequest
    {
      
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }
}
