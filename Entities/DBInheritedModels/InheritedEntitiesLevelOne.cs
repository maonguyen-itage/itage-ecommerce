using Entities.DBModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DBInheritedModels
{
    public class InheritedEntitiesLevelOne  //--In this name space, there are just simple classes. Basically we devided the inherited models in two levels
    {
        public int SimpleId { get; set; }
    }

    public class ColorEntity : Color
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class CategoryEntity : Category
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? AttachmentURL { get; set; }
        public int? DataExportType { get; set; } 
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
        public string? ParentCategoryName { get; set; }

        public bool IsDiscountCreatePageSearchEnabled { get; set; }

        public IFormFile? AttachmentFile { get; set; }
      //  public int? AttachmentId { get; set; }
    }

    public class SizeEntity : Size
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }
     public class MenuNavigationEntity : MenuNavigation
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? ParentMenuNavigationName { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? LoginUserId { get; set; }

    }  
    public class LocalizationCommonJsonEntity : LocalizationCommonJson
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public int? DataExportType { get; set; }

        public string? TableName { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? LoginUserId { get; set; }
    }

    public class ManufacturerEntity : Manufacturer
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class CurrencyEntity : Currency
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class AttachmentTypeEntity : AttachmentType
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class PaymentMethodEntity : PaymentMethod
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }


    public class AddressTypeEntity : AddressType
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class CountryEntity : Country
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class StateProvinceEntity : StateProvince
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public string? CountryName { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }

    }

    public class CityEntity : City
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }
    }

    public class TagEntity : Tag
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class ShippingMethodEntity : ShippingMethod
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }
    public class InventoryMethodEntity : InventoryMethod
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class WarehouseEntity : Warehouse
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class ProductAttributeEntity : ProductAttribute
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int ProductAttributeMappingID { get; set; } 
        public string? AttributeValue { get; set; }
        public string? PriceAdjustmentType { get; set; }
        public decimal? PriceAdjustment { get; set; }
        public string? AttributeDisplayText { get; set; }
        public int? DataExportType { get; set; }

        public int? UserId { get; set; }
    }

    public class ProductPicturesMappingEntity : ProductPicturesMapping
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? AttachmentURL { get; set; }
        public string? ProductsImgColorsMappingItemsJson { get; set; }
       
        public int? UserId { get; set; }
    }
     public class UserTypesEntity : UserType
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }


    public class DiscountTypeEntity : DiscountType
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class DiscountProductsMappingEntity : DiscountProductsMapping
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? ProductName { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }


    public class DiscountCategoriesMappingEntity : DiscountCategoriesMapping
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? Name { get; set; } // -- Name of category
        public string? ParentCategoryName { get; set; } 
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class AttachmentEntity : Attachment
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
     

        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public IFormFile? AttachmentFile { get; set; }
        public int? UserId { get; set; }
    }

    public class ContactUsEntity : ContactU
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class SubscriberEntity : Subscriber
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class HomeScreenBannerEntity : HomeScreenBanner
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public IFormFile? BannerImgUrlFile { get; set; }
        public string? BannerImgUrl { get; set; }

        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class RoleRightEntity : RoleRight
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? RightName { get; set; }
        public string? EntityName { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class RightEntity : Right
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
     
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class RolesEntity : Role
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class OrderStatusesEntity : OrderStatus
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class UserAddressEntity : UserAddress
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class OrderNoteEntity : OrderNote
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? CreatedByFirstName { get; set; }
        public string? CreatedByLastName { get; set; }
      
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }
    }

    public class AdminPanelNotificationEntity : AdminPanelNotification
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? ReadByFirstName { get; set; }
        public bool? IsReadNullProperty { get; set; }
        public string? NotificationTypeName { get; set; }
        public string? SelectedNotificationsIdsForReadJson { get; set; }

        public int HeaderUnreadNotificationCount { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class DiscountsCampaignEntity : DiscountsCampaign
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? CoverPictureUrl { get; set; }
        public IFormFile? CoverPictureFile { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? UserId { get; set; }
    }

    public class BankMasterEntity : BankMaster
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? IndustryName { get; set; }
        public string? StatusName { get; set; }

        public string? CountryName { get; set; }

        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }

    public class BankAccountEntity : BankAccount
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? BankName { get; set; }
        public string? StatusName { get; set; }
        public string? AccountHolderName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? UserTypeName { get; set; }
        public string? AccountTypeName { get; set; }
        public string? StateName { get; set; }
        public string? CityName { get; set; }
        public IFormFile[]? BankAttachementFiles { get; set; }
        public string? BankAttachementsJson { get; set; }
        public int? DataOperationType { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? LoginUserId { get; set; }
    }

    public class BankAccountTranEntity : BankAccountTran
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? AccountNo { get; set; }
        public string? EventName { get; set; }
        public string? CurrencyName { get; set; }
        public string? TransStatusName { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? LoginUserId { get; set; }
    }

    public class BankIndustryTypeEntity : BankIndustryType
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
     
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? LoginUserId { get; set; }
    }
    public class BankStatusEntity : BankStatus
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? LoginUserId { get; set; }
    }

    public class BankAccountAttachmentEntity : BankAccountAttachment
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? AttachmentURL { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }

    public class VendorsCommissionSetupEntity : VendorsCommissionSetup
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }

    public class BankTransEventEntity : BankTransEvent
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
      
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }


    public class AccountTransAttachmentEntity : AccountTransAttachment
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? AttachmentURL { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }

    public class AppConfigEntity : AppConfig
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public IFormFile? AttachmentFile { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }

    public class ProductReviewEntity : ProductReview
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public bool IsProductReviewPageSearchEnabled { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }

    public class LanguageEntity : Language
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    } 
    public class AppModuleEntity : AppModule
	{
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }

    public class RequestsQueueEntity : RequestsQueue
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? RequestTypeName { get; set; }
        public string? StatusKeyName { get; set; }

        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }

    public class RequestTypeEntity : RequestType
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }

    public class RequestStatusEntity : RequestStatus
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }
    public class OrderRefundRequestEntity : OrderRefundRequest
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? ReasonName { get; set; }
        public string? PaymentMethodName { get; set; }

        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }
}
