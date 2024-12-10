using DAL.DBContext;
using DAL.Repository.IServices;
using Dapper;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Services
{
    public class DiscountsServicesDAL : IDiscountsServicesDAL
    {
        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;
        private readonly IDapperConnectionHelper _dapperConnectionHelper;

        //--Constructor of the class
        public DiscountsServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper, IDapperConnectionHelper dapperConnectionHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
            _dapperConnectionHelper = dapperConnectionHelper;
        }

        public async Task<List<DiscountEntity>> GetDiscountsListDAL(DiscountEntity FormData)
        {
            List<DiscountEntity> result = new List<DiscountEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.DiscountId > 0)
                    {
                        SearchParameters.Append("AND MTBL.DiscountId =  @0 ", FormData.DiscountId);
                    }

                    if (FormData.DiscountTypeId > 0)
                    {
                        SearchParameters.Append("AND dt.DiscountTypeId =  @0 ", FormData.DiscountTypeId);
                    }

                    if (FormData.IsActiveSelected != null && (FormData.IsActiveSelected == 1 || FormData.IsActiveSelected == 0))
                    {
                        SearchParameters.Append("AND dt.IsActive =  @0 ", FormData.IsActiveSelected);
                    }

                    if (!String.IsNullOrEmpty(FormData.Title))
                    {
                        SearchParameters.Append("AND MTBL.Title LIKE  @0", "%" + FormData.Title + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.CouponCode))
                    {
                        SearchParameters.Append("AND MTBL.CouponCode LIKE  @0", "%" + FormData.CouponCode + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.StartDate AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.EndDate AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.* ,dt.DiscountTypeName , DUH.TotalUsage AS TotalUsage ")
                      .From(" Discounts MTBL")
                      .InnerJoin("DiscountTypes dt").On("MTBL.DiscountTypeId=dt.DiscountTypeId")
                      .Append("OUTER APPLY(")
                      .Append("select count(*) as TotalUsage from DiscountUsageHistory DUH where DUH.DiscountID=MTBL.DiscountID")
                      .Append(") DUH")
                      .Where("MTBL.DiscountTypeId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.CreatedOn DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<DiscountEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                    throw;
                }
            }
        }

        public async Task<List<DiscountTypeEntity>> GetDiscountTypesListDAL(DiscountTypeEntity FormData)
        {
            List<DiscountTypeEntity> result = new List<DiscountTypeEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    if (FormData.DiscountTypeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.DiscountTypeId =  @0 ", FormData.DiscountTypeId);
                    }

                    if (!String.IsNullOrEmpty(FormData.DiscountTypeName))
                    {
                        SearchParameters.Append("AND MTBL.DiscountTypeName LIKE  @0", "%" + FormData.DiscountTypeName + "%");
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" DiscountTypes MTBL")
                      .Where("MTBL.DiscountTypeId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.DiscountTypeId ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<DiscountTypeEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                    throw;
                }
            }
        }
        public async Task<List<ProductEntity>> GetProductsListForDiscountDAL(ProductEntity FormData)
        {
            List<ProductEntity> result = new List<ProductEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    context.EnableAutoSelect = false;
                    result = context.Fetch<ProductEntity>(@";EXEC [dbo].[SP_AdmPanel_GetProductsListForDiscount] @ProductId,@ProductName,@CategoryId,@PageNo,@PageSize",
                          new
                          {
                              ProductId = FormData.ProductId,
                              ProductName = FormData.ProductName,
                              CategoryId = FormData.CategoryId,
                              PageNo = FormData.PageNo,
                              PageSize = FormData.PageSize,
                          }).ToList();

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<CategoryEntity>> GetCategoriesListForDiscountDAL(CategoryEntity FormData)
        {
            List<CategoryEntity> result = new List<CategoryEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    if (FormData.CategoryId > 0)
                    {
                        SearchParameters.Append("AND MTBL.CategoryID =  @0 ", FormData.CategoryId);
                    }

                    if (FormData.CreatedBy != null && FormData.CreatedBy > 0)
                    {
                        SearchParameters.Append("AND MTBL.CreatedBy =  @0 ", FormData.CreatedBy);
                    }

                    if (FormData.ParentCategoryId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ParentCategoryId =  @0 ", FormData.ParentCategoryId);
                    }

                    if (!String.IsNullOrEmpty(FormData.Name))
                    {
                        SearchParameters.Append("AND MTBL.Name LIKE  @0", "%" + FormData.Name + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*, par.Name AS ParentCategoryName")
                      .From(" Categories MTBL")
                      .LeftJoin("Categories par").On("MTBL.ParentCategoryID = par.CategoryID")
                      .Where("MTBL.CategoryID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.CategoryID DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);
                    result = context.Fetch<CategoryEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<string> InsertUpdateDiscountDAL(DiscountEntity FormData)
        {
            string result = "";

            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    dbConnection.Execute("SP_AdmPanel_InsertUpdateDiscount",
                        new
                        {
                            DiscountId = FormData.DiscountId,
                            Title = FormData.Title,
                            DiscountTypeId = FormData.DiscountTypeId,
                            DiscountValueType = FormData.DiscountValueType,
                            DiscountValue = FormData.DiscountValue,
                            StartDate = FormData.StartDate,
                            EndDate = FormData.EndDate,
                            IsCouponCodeRequired = FormData.IsCouponCodeRequired,
                            CouponCode = FormData.CouponCode,
                            IsBoundToMaxQuantity = FormData.IsBoundToMaxQuantity,
                            MaxQuantity = FormData.MaxQuantity,
                            IsActive = FormData.IsActive,
                            DiscountAssociatedProductsJson = FormData.DiscountAssociatedProductsJson,
                            DiscountAssociatedCategoriesJson = FormData.DiscountAssociatedCategoriesJson,

                            UserId = FormData.UserId,
                        }
                        , commandType: CommandType.StoredProcedure);
                    dbConnection.Close();

                    result = "Saved Successfully!";

                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DiscountEntity?> GetDiscountDetailsById(int DiscountId)
        {
            DiscountEntity? result = new DiscountEntity();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    context.EnableAutoSelect = false;

                    result = context.Fetch<DiscountEntity>(@";EXEC [dbo].[SP_AdmPanel_GetDiscountDetailsById] @DiscountId",
                          new
                          {
                              DiscountId = DiscountId

                          }).FirstOrDefault();
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<DiscountProductsMappingEntity>> GetDiscountProductsMappingListDAL(DiscountProductsMappingEntity FormData)
        {
            List<DiscountProductsMappingEntity> result = new List<DiscountProductsMappingEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.DiscountId != 0)
                    {
                        SearchParameters.Append("AND MTBL.DiscountId =  @0 ", FormData.DiscountId);
                    }

                    if (FormData.ProductId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ProductId =  @0", FormData.ProductId);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords, MTBL.DiscountProductMappingID, MTBL.DiscountID, MTBL.ProductID,P.ProductName")
                      .From(" DiscountProductsMapping MTBL")
                      .InnerJoin("Products p").On("MTBL.ProductID = p.ProductID")
                      .Where("MTBL.DiscountProductMappingID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.DiscountProductMappingID ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);
                    result = context.Fetch<DiscountProductsMappingEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<DiscountCategoriesMappingEntity>> GetDiscountCategoriesMappingListDAL(DiscountCategoriesMappingEntity FormData)
        {
            List<DiscountCategoriesMappingEntity> result = new List<DiscountCategoriesMappingEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.DiscountId != 0)
                    {
                        SearchParameters.Append("AND MTBL.DiscountId =  @0 ", FormData.DiscountId);
                    }

                    if (FormData.CategoryId > 0)
                    {
                        SearchParameters.Append("AND MTBL.CategoryId =  @0", FormData.CategoryId);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords, MTBL.DiscountCategoryMappingID, MTBL.DiscountID, MTBL.CategoryID, c.Name , par.Name AS ParentCategoryName")
                      .From(" DiscountCategoriesMapping MTBL")
                      .InnerJoin("Categories c").On("MTBL.CategoryID = c.CategoryID")
                      .LeftJoin("Categories par").On("c.ParentCategoryID = par.CategoryID")
                      .Where("MTBL.DiscountCategoryMappingID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.DiscountCategoryMappingID ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<DiscountCategoriesMappingEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<ContactUsEntity>> GetContactUsListDAL(ContactUsEntity FormData)
        {
            List<ContactUsEntity> result = new List<ContactUsEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.ContactId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ContactId =  @0 ", FormData.ContactId);
                    }

                    if (!String.IsNullOrEmpty(FormData.FullName))
                    {
                        SearchParameters.Append("AND MTBL.FullName LIKE  @0", "%" + FormData.FullName + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.Email))
                    {
                        SearchParameters.Append("AND MTBL.Email LIKE  @0", "%" + FormData.Email + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" ContactUs MTBL")
                      .Where("MTBL.ContactId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.ContactId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<ContactUsEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<SubscriberEntity>> GetSubscribersListDAL(SubscriberEntity FormData)
        {
            List<SubscriberEntity> result = new List<SubscriberEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.SubscriptionId > 0)
                    {
                        SearchParameters.Append("AND MTBL.SubscriptionId =  @0 ", FormData.SubscriptionId);
                    }

                    if (!String.IsNullOrEmpty(FormData.Email))
                    {
                        SearchParameters.Append("AND MTBL.Email LIKE  @0", "%" + FormData.Email + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" Subscribers MTBL")
                      .Where("MTBL.SubscriptionId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.SubscriptionId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<SubscriberEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<HomeScreenBannerEntity>> GetHomeScreenBannersListDAL(HomeScreenBannerEntity FormData)
        {
            List<HomeScreenBannerEntity> result = new List<HomeScreenBannerEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.BannerId > 0)
                    {
                        SearchParameters.Append("AND MTBL.BannerId =  @0 ", FormData.BannerId);
                    }

                    if (FormData.ThemeTypeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ThemeTypeId =  @0 ", FormData.ThemeTypeId);
                    }

                    if (!String.IsNullOrEmpty(FormData.MainTitle))
                    {
                        SearchParameters.Append("AND MTBL.MainTitle LIKE  @0", "%" + FormData.MainTitle + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.* , ATC.AttachmentURL AS BannerImgUrl")
                      .From(" HomeScreenBanners MTBL")
                      .InnerJoin("Attachments ATC").On("ATC.AttachmentID=MTBL.AttachmentID")
                      .Where("MTBL.BannerId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.BannerId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<HomeScreenBannerEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<string> SaveUpdateHomeScreenBannerDAL(HomeScreenBannerEntity FormData, int DataOperationType)
        {
            string result = "";
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"Insert into HomeScreenBanners(TopTitle,	MainTitle,	BottomTitle,	LeftButtonText, LeftButtonUrl,RightButtonText,RightButtonUrl,
                        DisplaySeqNo, IsActive, ThemeTypeId , AttachmentId,  CreatedOn,	CreatedBy)
                        VALUES(@TopTitle,	@MainTitle,	@BottomTitle,	@LeftButtonText,  @LeftButtonUrl, @RightButtonText , @RightButtonUrl , @DisplaySeqNo,
                         @IsActive , @ThemeTypeId , @AttachmentId,     getdate(),	@UserId)",
                        new
                        {
                            TopTitle = FormData.TopTitle,
                            MainTitle = FormData.MainTitle,
                            BottomTitle = FormData.BottomTitle,
                            LeftButtonText = FormData.LeftButtonText,
                            LeftButtonUrl = FormData.LeftButtonUrl,
                            RightButtonText = FormData.RightButtonText,
                            RightButtonUrl = FormData.RightButtonUrl,
                            DisplaySeqNo = FormData.DisplaySeqNo,
                            IsActive = FormData.IsActive,
                            ThemeTypeId = FormData.ThemeTypeId,
                            AttachmentId = FormData.AttachmentId,
                            UserId = FormData.UserId,

                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"UPDATE TOP(1) HomeScreenBanners SET TopTitle = @TopTitle ,MainTitle = @MainTitle  ,BottomTitle = @BottomTitle ,LeftButtonText = @LeftButtonText,
                                    LeftButtonUrl=@LeftButtonUrl      , RightButtonText=@RightButtonText, RightButtonUrl = @RightButtonUrl , DisplaySeqNo=@DisplaySeqNo ,
                                    IsActive=@IsActive   ,   ThemeTypeId = @ThemeTypeId , AttachmentId = @AttachmentId , ModifiedOn=GETDATE(), ModifiedBy=@UserId   WHERE BannerId = @BannerId;",
                       new
                       {
                           BannerId = FormData.BannerId,
                           TopTitle = FormData.TopTitle,
                           MainTitle = FormData.MainTitle,
                           BottomTitle = FormData.BottomTitle,
                           LeftButtonText = FormData.LeftButtonText,
                           LeftButtonUrl = FormData.LeftButtonUrl,
                           RightButtonText = FormData.RightButtonText,
                           RightButtonUrl = FormData.RightButtonUrl,
                           DisplaySeqNo = FormData.DisplaySeqNo,
                           IsActive = FormData.IsActive,
                           ThemeTypeId = FormData.ThemeTypeId,
                           AttachmentId = FormData.AttachmentId,
                           UserId = FormData.UserId,
                       });
                        result = "Saved Successfully!";
                    }
                    else
                    {
                        result = "Please define data operation type!";
                    }

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<DiscountsCampaignEntity>> GetDiscountsCampaignDAL(DiscountsCampaignEntity FormData)
        {
            List<DiscountsCampaignEntity> result = new List<DiscountsCampaignEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.CampaignId > 0)
                    {
                        SearchParameters.Append("AND MTBL.CampaignId =  @0 ", FormData.CampaignId);
                    }

                    if (!String.IsNullOrEmpty(FormData.MainTitle))
                    {
                        SearchParameters.Append("AND MTBL.MainTitle LIKE  @0", "%" + FormData.MainTitle + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.DisplayStartDate AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.DisplayEndDate AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.* , ATC.AttachmentURL AS CoverPictureUrl")
                      .From(" DiscountsCampaign MTBL")
                      .LeftJoin("Attachments ATC").On("ATC.AttachmentID = MTBL.CoverPictureID")
                      .Where("MTBL.CampaignId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.CampaignId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<DiscountsCampaignEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<DiscountsCampaignEntity?> GetCampaignDetailByIdDAL(int CampaignId)
        {
            DiscountsCampaignEntity? result = new DiscountsCampaignEntity();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var ppSql = PetaPoco.Sql.Builder.Select(@" MTBL.* , ATC.AttachmentURL AS CoverPictureUrl")
                      .From(" DiscountsCampaign MTBL")
                      .LeftJoin("Attachments ATC").On("ATC.AttachmentID = MTBL.CoverPictureID")
                      .Where("MTBL.CampaignId = @0", CampaignId);

                    result = context.Fetch<DiscountsCampaignEntity>(ppSql).FirstOrDefault();

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<string> SaveUpdateDiscountsCampaignDAL(DiscountsCampaignEntity FormData, int DataOperationType)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"insert into DiscountsCampaign(MainTitle,	DiscountTitle,	Body,	IsActive,	DisplayStartDate,	DisplayEndDate,	CoverPictureID,	CreatedOn	,CreatedBy)
                        VALUES(@MainTitle,	@DiscountTitle,	@Body,	@IsActive,  @DisplayStartDate, @DisplayEndDate,@CoverPictureId	 ,getdate(),	@UserId)",
                        new
                        {
                            MainTitle = FormData.MainTitle,
                            DiscountTitle = FormData.DiscountTitle,
                            Body = FormData.Body,
                            DisplayStartDate = FormData.DisplayStartDate,
                            DisplayEndDate = FormData.DisplayEndDate,
                            IsActive = FormData.IsActive,
                            CoverPictureId = FormData.CoverPictureId,
                            UserId = FormData.UserId,

                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"update top(1) DiscountsCampaign
                        set MainTitle= @MainTitle,
                        DiscountTitle= @DiscountTitle,
                        Body= @Body,	
                        IsActive= @IsActive,
                        DisplayStartDate= @DisplayStartDate,
                        DisplayEndDate= @DisplayEndDate,	
                        CoverPictureId= @CoverPictureId,
                        ModifiedOn = getdate(),
                        ModifiedBy = @UserId
                        WHERE CampaignId= @CampaignId;",
                       new
                       {
                           CampaignId = FormData.CampaignId,
                           MainTitle = FormData.MainTitle,
                           DiscountTitle = FormData.DiscountTitle,
                           Body = FormData.Body,
                           DisplayStartDate = FormData.DisplayStartDate,
                           DisplayEndDate = FormData.DisplayEndDate,
                           IsActive = FormData.IsActive,
                           CoverPictureId = FormData.CoverPictureId,
                           UserId = FormData.UserId,

                       });
                        result = "Saved Successfully!";
                    }
                    else
                    {
                        result = "Please define data operation type!";
                    }

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
