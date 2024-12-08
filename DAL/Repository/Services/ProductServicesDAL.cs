using DAL.DBContext;
using DAL.Repository.IServices;
using Dapper;
using Entities.CommonModels;
using Entities.CommonModels.ProductsCatalogModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DBInheritedModels.InheritedEntitiesLevelTwo;

namespace DAL.Repository.Services
{
    public class ProductServicesDAL : IProductServicesDAL
    {
        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;
        private readonly IDapperConnectionHelper _dapperConnectionHelper;

        //--Constructor of the class
        public ProductServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper, IDapperConnectionHelper dapperConnectionHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
            _dapperConnectionHelper = dapperConnectionHelper;
        }

        //--Save product using dapped method. I did not use here peta poco because it was creating issue for json parameters of stored procedure
        public async Task<string> CreateNewProductDAL(ProductEntity FormData)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();
                    dbConnection.Execute("SP_AdmPanel_CreateNewProduct",
                        new
                        {
                            ProductName = FormData.ProductName,
                            ShortDescription = FormData.ShortDescription,
                            FullDescription = FormData.FullDescription,
                            ManufacturerId = FormData.ManufacturerId,
                            VendorId = FormData.VendorId,
                            IsActive = FormData.IsActive,
                            ShowOnHomePage = FormData.ShowOnHomePage,
                            MarkAsNew = FormData.MarkAsNew,
                            AllowCustomerReviews = FormData.AllowCustomerReviews,
                            SellStartDatetimeUtc = FormData.SellStartDatetimeUtc,
                            SellEndDatetimeUtc = FormData.SellEndDatetimeUtc,
                            Sku = FormData.Sku,
                            Price = FormData.Price,
                            OldPrice = FormData.OldPrice,
                            IsDiscountAllowed = FormData.IsDiscountAllowed,
                            IsShippingFree = FormData.IsShippingFree,
                            ShippingCharges = FormData.ShippingCharges,
                            EstimatedShippingDays = FormData.EstimatedShippingDays,
                            IsReturnAble = FormData.IsReturnAble,
                            InventoryMethodID = FormData.InventoryMethodId,
                            WarehouseId = FormData.WarehouseId,
                            StockQuantity = FormData.StockQuantity,
                            IsBoundToStockQuantity = FormData.IsBoundToStockQuantity,
                            DisplayStockQuantity = FormData.DisplayStockQuantity,
                            OrderMinimumQuantity = FormData.OrderMinimumQuantity,
                            OrderMaximumQuantity = FormData.OrderMaximumQuantity,
                            MetaTitle = FormData.MetaTitle,
                            MetaKeywords = FormData.MetaKeywords,
                            MetaDescription = FormData.MetaDescription,
                            IsDigitalProduct = FormData.IsDigitalProduct,
                            DigitalProductFilesUrlJson = FormData.DigitalProductFilesUrlJson,
                            SelectedCategoriesJson = FormData.SelectedCategoriesJson,
                            SelectedTagsJson = FormData.SelectedTagsJson,
                            SelectedDiscountsJson = FormData.SelectedDiscountsJson,
                            SelectedShippingMethodsJson = FormData.SelectedShippingMethodsJson,
                            ProductAttributesJson = FormData.ProductAttributesJson,
                            ProductImagesJson = FormData.ProductImagesJson,
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
        //--Update product using dapped method. I did not use here peta poco because it was creating issue for json parameters of stored procedure
        public async Task<string> UpdateProductDAL(ProductEntity FormData)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();
                    dbConnection.Execute("SP_AdmPanel_UpdateProduct",
                        new
                        {
                            ProductId = FormData.ProductId,
                            ProductName = FormData.ProductName,
                            ShortDescription = FormData.ShortDescription,
                            FullDescription = FormData.FullDescription,
                            ManufacturerId = FormData.ManufacturerId,
                            VendorId = FormData.VendorId,
                            IsActive = FormData.IsActive,
                            ShowOnHomePage = FormData.ShowOnHomePage,
                            MarkAsNew = FormData.MarkAsNew,
                            AllowCustomerReviews = FormData.AllowCustomerReviews,
                            SellStartDatetimeUtc = FormData.SellStartDatetimeUtc,
                            SellEndDatetimeUtc = FormData.SellEndDatetimeUtc,
                            Sku = FormData.Sku,
                            Price = FormData.Price,
                            OldPrice = FormData.OldPrice,
                            IsDiscountAllowed = FormData.IsDiscountAllowed,
                            IsShippingFree = FormData.IsShippingFree,
                            ShippingCharges = FormData.ShippingCharges,
                            EstimatedShippingDays = FormData.EstimatedShippingDays,
                            IsReturnAble = FormData.IsReturnAble,
                            InventoryMethodID = FormData.InventoryMethodId,
                            WarehouseId = FormData.WarehouseId,
                            StockQuantity = FormData.StockQuantity,
                            IsBoundToStockQuantity = FormData.IsBoundToStockQuantity,
                            DisplayStockQuantity = FormData.DisplayStockQuantity,
                            OrderMinimumQuantity = FormData.OrderMinimumQuantity,
                            OrderMaximumQuantity = FormData.OrderMaximumQuantity,
                            MetaTitle = FormData.MetaTitle,
                            MetaKeywords = FormData.MetaKeywords,
                            MetaDescription = FormData.MetaDescription,
                            DigitalProductFilesUrlJson = FormData.DigitalProductFilesUrlJson,
                            SelectedCategoriesJson = FormData.SelectedCategoriesJson,
                            SelectedTagsJson = FormData.SelectedTagsJson,
                            SelectedDiscountsJson = FormData.SelectedDiscountsJson,
                            SelectedShippingMethodsJson = FormData.SelectedShippingMethodsJson,
                            ProductAttributesJson = FormData.ProductAttributesJson,
                            ProductImagesJson = FormData.ProductImagesJson,
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
        public async Task<List<ShippingMethodEntity>> GetShippingMethodsListDAL(ShippingMethodEntity FormData)
        {
            List<ShippingMethodEntity> result = new List<ShippingMethodEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    if (FormData.ShippingMethodId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ShippingMethodId =  @0 ", FormData.ShippingMethodId);
                    }

                    if (!String.IsNullOrEmpty(FormData.MethodName))
                    {
                        SearchParameters.Append("AND MTBL.MethodName LIKE  @0", "%" + FormData.MethodName + "%");
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
                      .From(" ShippingMethods MTBL")
                      .Where("MTBL.ShippingMethodID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.ShippingMethodID DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<ShippingMethodEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<InventoryMethodEntity>> GetInventoryMethodsListDAL(InventoryMethodEntity FormData)
        {
            List<InventoryMethodEntity> result = new List<InventoryMethodEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.InventoryMethodId > 0)
                    {
                        SearchParameters.Append("AND MTBL.InventoryMethodId =  @0 ", FormData.InventoryMethodId);
                    }

                    if (!String.IsNullOrEmpty(FormData.InventoryMethodName))
                    {
                        SearchParameters.Append("AND MTBL.InventoryMethodName LIKE  @0", "%" + FormData.InventoryMethodName + "%");
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
                      .From(" InventoryMethods MTBL")
                      .Where("MTBL.InventoryMethodId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.InventoryMethodId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<InventoryMethodEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<List<WarehouseEntity>> GetWarehousesListDAL(WarehouseEntity FormData)
        {
            List<WarehouseEntity> result = new List<WarehouseEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.WarehouseId > 0)
                    {
                        SearchParameters.Append("AND MTBL.WarehouseId =  @0 ", FormData.WarehouseId);
                    }

                    if (!String.IsNullOrEmpty(FormData.WarehouseName))
                    {
                        SearchParameters.Append("AND MTBL.WarehouseName LIKE  @0", "%" + FormData.WarehouseName + "%");
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
                      .From(" Warehouses MTBL")
                      .Where("MTBL.WarehouseId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.WarehouseId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<WarehouseEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<ProductAttributeEntity>> GetProductAttributesListDAL(ProductAttributeEntity FormData)
        {
            List<ProductAttributeEntity> result = new List<ProductAttributeEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.ProductAttributeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ProductAttributeID =  @0 ", FormData.ProductAttributeId);
                    }

                    if (!String.IsNullOrEmpty(FormData.AttributeName))
                    {
                        SearchParameters.Append("AND MTBL.AttributeName LIKE  @0", "%" + FormData.AttributeName + "%");
                    }

                    //if (!String.IsNullOrEmpty(FormData.FromDate))
                    //{
                    //    SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    //}

                    //if (!String.IsNullOrEmpty(FormData.ToDate))
                    //{
                    //    SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    //}

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" ProductAttributes MTBL")
                      .Where("MTBL.ProductAttributeID is not null")
                      .Append(SearchParameters)
                     .OrderBy(" MTBL.ProductAttributeID ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<ProductAttributeEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<TagEntity>> GetProductTagsListForCreateProductPageDAL(TagEntity FormData)
        {
            List<TagEntity> result = new List<TagEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (!String.IsNullOrEmpty(FormData.TagName))
                    {
                        SearchParameters.Append("AND MTBL.TagName LIKE  @0", "%" + FormData.TagName + "%");
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" Tags MTBL")
                      .Where("MTBL.TagId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.TagName ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<TagEntity>(ppSql);
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
        public async Task<List<HtmlDropDownRemoteData>> GetProductAttributeValuesByAttributeID(string ProductAttributeId)
        {
            List<HtmlDropDownRemoteData> result = new List<HtmlDropDownRemoteData>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    context.EnableAutoSelect = false;

                    result = context.Fetch<HtmlDropDownRemoteData>(@";EXEC [dbo].[SP_GetProductAttributeDropdownData] @ProductAttributeId",
                           new
                           {
                               ProductAttributeId = ProductAttributeId
                           }).ToList();

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

        public async Task<ProductEntity?> GetProductDetailsById(int ProductId)
        {
            ProductEntity? result = new ProductEntity();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    context.EnableAutoSelect = false;
                    result = context.Fetch<ProductEntity>(@";EXEC [dbo].[SP_AdmPanel_GetProductDetailsById] @ProductId",
                          new
                          {
                              ProductId = ProductId

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
        public async Task<List<ProductEntity>> GetProductList(ProductEntity FormData)
        {
            List<ProductEntity> result = new List<ProductEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    context.EnableAutoSelect = false;
                    result = context.Fetch<ProductEntity>(@";EXEC [dbo].[SP_AdmPanel_GetProductsList] @ProductId,@ProductName,@CategoryId,@IsActive,@FromDate,@ToDate,@PageNo,@PageSize , @CreatedBy",
                          new
                          {
                              ProductId = FormData.ProductId,
                              ProductName = FormData.ProductName,
                              CategoryId = FormData.CategoryId,
                              IsActive = FormData.IsActive,
                              FromDate = FormData.FromDate,
                              ToDate = FormData.ToDate,
                              PageNo = FormData.PageNo,
                              PageSize = FormData.PageSize,
                              CreatedBy = FormData.CreatedBy,
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
        public async Task<List<ProductEntity>> GetProductsReviewsDAL(ProductEntity FormData)
        {
            List<ProductEntity> result = new List<ProductEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.ProductId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ProductId =  @0 ", FormData.ProductId);
                    }

                    if (!String.IsNullOrEmpty(FormData.ProductName))
                    {
                        SearchParameters.Append("AND MTBL.ProductName LIKE  @0", "%" + FormData.ProductName + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    if (FormData.CreatedBy != null && FormData.CreatedBy > 0)
                    {
                        SearchParameters.Append("AND MTBL.VendorID =  @0", FormData.CreatedBy);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Append(@" ;WITH CTE_Main AS (
                    SELECT DISTINCT PRD.ProductID, PRD.ProductName, 
                    (SUM(PRV.Rating) OVER(PARTITION BY PRV.ProductID) /COUNT(*) OVER(PARTITION BY PRV.ProductID)) AS Rating, 
                    COUNT(*) OVER(PARTITION BY PRV.ProductID) AS TotalReviews , ProductPic.AttachmentURL 
                    FROM Products PRD
                    INNER JOIN ProductReviews PRV ON PRD.ProductID=  PRV.ProductID
                    OUTER APPLY(
			                    SELECT TOP 1 a.AttachmentURL FROM Attachments a
			                    INNER JOIN ProductPicturesMapping ppm ON a.AttachmentID = ppm.PictureID
			                    WHERE ppm.ProductID=PRD.ProductId
                    ) ProductPic
                    )
                    SELECT MTBL.* , COUNT(*) OVER () as TotalRecords
                    FROM  CTE_Main MTBL")
                      .Where("MTBL.ProductID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.Rating DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);
                    result = context.Fetch<ProductEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<ProductReviewEntity>> GetProductReviewsByProductIdDAL(ProductReviewEntity FormData)
        {
            List<ProductReviewEntity> result = new List<ProductReviewEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.ReviewId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ReviewId =  @0 ", FormData.ReviewId);
                    }

                    if (FormData.Rating > 0)
                    {
                        SearchParameters.Append("AND MTBL.Rating =  @0 ", FormData.Rating);
                    }

                    if (!String.IsNullOrEmpty(FormData.ReviewerName))
                    {
                        SearchParameters.Append("AND MTBL.ReviewerName LIKE  @0", "%" + FormData.ReviewerName + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.ReviewDate AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.ReviewDate AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" ProductReviews MTBL")
                      .Where("MTBL.ProductId = @0", FormData.ProductId)
                      .Append(SearchParameters)
                     .OrderBy("MTBL.ReviewId ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<ProductReviewEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<string> UploadBulkProductsFromExcelDAL(ProductExcelUpload FormData)
        {
            string result = "";

            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    dbConnection.Execute("SP_AdmPanel_UploadBulkProducts",
                        new
                        {
                            ProductName = FormData.ProductName,
                            ShortDescription = FormData.ShortDescription,
                            FullDescription = FormData.FullDescription,
                            ManufacturerId = FormData.ManufacturerId,
                            VendorId = FormData.VendorId,
                            IsActive = FormData.IsActive,
                            ShowOnHomePage = FormData.ShowOnHomePage,
                            MarkAsNew = FormData.MarkAsNew,
                            AllowCustomerReviews = FormData.AllowCustomerReviews,
                            SellStartDatetimeUtc = FormData.SellStartDatetimeUtc,
                            SellEndDatetimeUtc = FormData.SellEndDatetimeUtc,
                            Sku = FormData.Sku,
                            Price = FormData.Price,
                            OldPrice = FormData.OldPrice,
                            IsDiscountAllowed = FormData.IsDiscountAllowed,
                            IsShippingFree = FormData.IsShippingFree,
                            ShippingCharges = FormData.ShippingCharges,
                            EstimatedShippingDays = FormData.EstimatedShippingDays,
                            IsReturnAble = FormData.IsReturnAble,
                            InventoryMethodID = FormData.InventoryMethodId,
                            WarehouseId = FormData.WarehouseId,
                            StockQuantity = FormData.StockQuantity,
                            IsBoundToStockQuantity = FormData.IsBoundToStockQuantity,
                            DisplayStockQuantity = FormData.DisplayStockQuantity,
                            OrderMinimumQuantity = FormData.OrderMinimumQuantity,
                            OrderMaximumQuantity = FormData.OrderMaximumQuantity,
                            MetaTitle = FormData.MetaTitle,
                            MetaKeywords = FormData.MetaKeywords,
                            MetaDescription = FormData.MetaDescription,
                            CategoriesIdsCommaSeperated = FormData.CategoriesIdsCommaSeperated,
                            TagsIdsCommaSeperated = FormData.TagsIdsCommaSeperated,
                            ShippingMethodsIdsCommaSeperated = FormData.ShippingMethodsIdsCommaSeperated,
                            ColorsIdsCommaSeperated = FormData.ColorsIdsCommaSeperated,
                            SizeIdsCommaSeperated = FormData.SizeIdsCommaSeperated,
                            ImagesIdsCommaSeperated = FormData.ImagesIdsCommaSeperated,
                            LoginUserId = FormData.LoginUserId,
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

        public async Task<List<AttachmentEntity>> GetAttachmentsListForImageUploadPageDAL(AttachmentEntity FormData)
        {

            List<AttachmentEntity> result = new List<AttachmentEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.AttachmentId > 0)
                    {
                        SearchParameters.Append("AND MTBL.AttachmentId =  @0 ", FormData.AttachmentId);
                    }

                    if (FormData.CreatedBy != null && FormData.CreatedBy > 0)
                    {
                        SearchParameters.Append("AND MTBL.CreatedBy =  @0 ", FormData.CreatedBy);
                    }

                    if (!String.IsNullOrEmpty(FormData.AttachmentName))
                    {
                        SearchParameters.Append("AND MTBL.AttachmentName LIKE  @0", "%" + FormData.AttachmentName + "%");
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
                      .From(" Attachments MTBL")

                      .Where("MTBL.IsCommonImageUpload = 1")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.AttachmentId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<AttachmentEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<List<ProductAttributeEntity>> GetProductVariantsDAL(ProductAttributeEntity FormData)
        {
            List<ProductAttributeEntity> result = new List<ProductAttributeEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    if (FormData.ProductAttributeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ProductAttributeId =  @0 ", FormData.ProductAttributeId);
                    }

                    if (!String.IsNullOrEmpty(FormData.AttributeName))
                    {
                        SearchParameters.Append("AND MTBL.AttributeName LIKE  @0", "%" + FormData.AttributeName + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.DisplayName))
                    {
                        SearchParameters.Append("AND MTBL.DisplayName LIKE  @0", "%" + FormData.DisplayName + "%");
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
                      .From(" ProductAttributes MTBL")
                      .Where("MTBL.ProductAttributeId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.ProductAttributeId ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<ProductAttributeEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<ProductVariantDetail>> GetProductVariantsDetailByIdDAL(int ProductAttributeId)
        {
            List<ProductVariantDetail> result = new List<ProductVariantDetail>();
            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    var resultIEnumerable = await dbConnection.QueryAsync<ProductVariantDetail>(@"DECLARE @PrimaryKeyValue NVARCHAR(100);
                    DECLARE @DisplayTextColumn NVARCHAR(100);
                    DECLARE @AttributeSqlTableName NVARCHAR(100);
                    DECLARE @AttributeName NVARCHAR(100);
                    DECLARE @AttributeDisplayName NVARCHAR(100);

                    SET @PrimaryKeyValue=(SELECT TOP 1 PAC.ColumnName FROM ProductAttributeColumns PAC WHERE PAC.ProductAttributeID=@ProductAttributeId AND PAC.ColumnType='PrimaryKey');
                    SET @DisplayTextColumn=(SELECT TOP 1 PAC.ColumnName FROM ProductAttributeColumns PAC WHERE PAC.ProductAttributeID=@ProductAttributeId AND PAC.ColumnType='DisplayText');
                    SET @AttributeSqlTableName=(SELECT TOP 1 PRD.AttributeSqlTableName FROM ProductAttributes PRD WHERE PRD.ProductAttributeID=@ProductAttributeId);
                    SET @AttributeName=(SELECT TOP 1 PRD.AttributeName FROM ProductAttributes PRD WHERE PRD.ProductAttributeID=@ProductAttributeId);
                    SET @AttributeDisplayName=(SELECT TOP 1 PRD.DisplayName FROM ProductAttributes PRD WHERE PRD.ProductAttributeID=@ProductAttributeId);

                    IF(@PrimaryKeyValue IS NOT NULL AND @DisplayTextColumn IS NOT NULL AND @AttributeSqlTableName IS NOT NULL)
	                    BEGIN

		                    DECLARE @SqlQuery NVARCHAR(MAX);
		                    SET @SqlQuery=('SELECT ' + @PrimaryKeyValue + SPACE(1) + 'AS PrimaryKeyValue' +','+  
		                    @DisplayTextColumn + SPACE(1) + 'AS DisplayText' + SPACE(1)+ ','+  
		                    '##PrimaryKeyValue##' + SPACE(1) + 'AS PrimaryKeyName' + SPACE(1)+ ','+ 
		                    '##AttributeSqlTableName##' + SPACE(1) + 'AS AttributeSqlTableName' + SPACE(1)+ ','+ 
		                    '##AttributeName##' + SPACE(1) + 'AS AttributeName' + SPACE(1)+ ','+  
		                    '##DisplayName##' + SPACE(1) + 'AS AttributeDisplayName' + SPACE(1)+ 
		                    'FROM ' + @AttributeSqlTableName);
		                    SET @SqlQuery = REPLACE( @SqlQuery, '##PrimaryKeyValue##'  , '''' + @PrimaryKeyValue + '''');
		                    SET @SqlQuery = REPLACE( @SqlQuery, '##AttributeSqlTableName##'  , '''' + @AttributeSqlTableName + '''');
		                    SET @SqlQuery = REPLACE( @SqlQuery, '##AttributeName##'  , '''' + @AttributeName + '''');
		                    SET @SqlQuery = REPLACE( @SqlQuery, '##DisplayName##'  , '''' + @AttributeDisplayName + '''');
		
		                    EXECUTE sp_executesql @SqlQuery
	                    END",
                                                                                      new
                                                                                      {
                                                                                          ProductAttributeId = ProductAttributeId

                                                                                      }
                          , commandType: CommandType.Text);
                    dbConnection.Close();

                    result = resultIEnumerable.ToList();
                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> SaveUpdateProductVariantDAL(ProductVariantDetail FormData, int DataOperationType)
        {
            string result = "";

            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    if (DataOperationType == 1)
                    {
                        dbConnection.Open();

                        var resultIEnumerable = await dbConnection.ExecuteAsync(@"DECLARE @AttributeSqlTableName NVARCHAR(200);
                        DECLARE @SqlInsertQuery NVARCHAR(200);
                        DECLARE @ValueColumnName NVARCHAR(200);

                        SET @AttributeSqlTableName = (SELECT TOP 1 AttributeSqlTableName FROM ProductAttributes WHERE ProductAttributeID = @ProductAttributeId);
                        SET @ValueColumnName = (SELECT TOP 1 ColumnName FROM ProductAttributeColumns WHERE ProductAttributeID = @ProductAttributeId AND ColumnType  = 'DisplayText');

                        IF(@AttributeSqlTableName IS NOT NULL AND @ValueColumnName IS NOT NULL AND @DisplayText IS NOT NULL)
                        BEGIN
	                        SET @SqlInsertQuery = 'INSERT INTO ' + SPACE(1) + @AttributeSqlTableName + SPACE(1) + '(' + @ValueColumnName + ',' + 'IsActive , CreatedOn , CreatedBy )' +
						                          'VALUES (' + '''' + @DisplayText +'''' + ',' + SPACE(1) + '1, GETDATE(),' + CAST(@LoginUserId AS NVARCHAR(200)) + ')';
	                        EXECUTE sp_executesql @SqlInsertQuery
                        END",
                                                  new
                                                  {
                                                      ProductAttributeId = FormData.ProductAttributeId,
                                                      DisplayText = FormData.DisplayText,
                                                      LoginUserId = FormData.LoginUserId,

                                                  }
                              , commandType: CommandType.Text);
                        dbConnection.Close();

                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        dbConnection.Open();

                        var resultIEnumerable = await dbConnection.ExecuteAsync(@"DECLARE @AttributeSqlTableName NVARCHAR(200);
                        DECLARE @SqlUpdateQuery NVARCHAR(200);
                        DECLARE @ValueColumnName NVARCHAR(200);
                        DECLARE @PrimaryKeyColumnName  NVARCHAR(200);

                        SET @AttributeSqlTableName = (SELECT TOP 1 AttributeSqlTableName FROM ProductAttributes WHERE ProductAttributeID = @ProductAttributeId);
                        SET @ValueColumnName = (SELECT TOP 1 ColumnName FROM ProductAttributeColumns WHERE ProductAttributeID = @ProductAttributeId AND ColumnType  = 'DisplayText');
                        SET @PrimaryKeyColumnName = (SELECT TOP 1 ColumnName FROM ProductAttributeColumns WHERE ProductAttributeID = @ProductAttributeId AND ColumnType  = 'PrimaryKey');


                        IF(@AttributeSqlTableName IS NOT NULL AND @ValueColumnName IS NOT NULL AND @DisplayText IS NOT NULL AND @PrimaryKeyColumnName IS NOT NULL AND @PrimaryKeyValue IS NOT NULL)
                        BEGIN
	                        SET @SqlUpdateQuery = ('UPDATE ' + SPACE(1) + @AttributeSqlTableName + SPACE(1) + 'SET ' + @ValueColumnName + '=' + '''' + @DisplayText + '''' + SPACE(1) +
						                          'WHERE ' + @PrimaryKeyColumnName  + '=' + '''' + CAST(@PrimaryKeyValue AS NVARCHAR(MAX)) + '''');
	                        EXECUTE sp_executesql @SqlUpdateQuery
                        END",
                                                  new
                                                  {
                                                      ProductAttributeId = FormData.ProductAttributeId,
                                                      PrimaryKeyValue = FormData.PrimaryKeyValue,
                                                      DisplayText = FormData.DisplayText,
                                                      LoginUserId = FormData.LoginUserId,
                                                  }
                              , commandType: CommandType.Text);
                        dbConnection.Close();

                        result = "Saved Successfully!";
                    }
                    else
                    {
                        result = "Please define data operation type!";
                    }

                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProductsCategoriesMapping>> ReadProductCategoriesById(int ProductId)
        {
            List<ProductsCategoriesMapping> result = new List<ProductsCategoriesMapping>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (ProductId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ProductId =  @0 ", ProductId);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" MTBL.*")
                        .From(" ProductsCategoriesMapping MTBL")
                      .Where("MTBL.ProductID = @0", ProductId);


                    result = context.Fetch<ProductsCategoriesMapping>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<bool> DeleteAnyProductImage(int AttachmentID)
        {
            bool result = false;

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    string deleteQuery = String.Format("DELETE TOP(2) FROM ProductPicturesMapping WHERE PictureID ='{0}'; {1} DELETE TOP(1) FROM Attachments WHERE AttachmentID ='{0}';", AttachmentID, Environment.NewLine);
                    context.Execute(deleteQuery);
                    result = true;

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<string> UpdateProductImgColorMappingDAL(ProductPicturesMappingEntity FormData)
        {
            string result = "";

            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();

                    var resultIEnumerable = await dbConnection.ExecuteAsync(@"UPDATE TOP(30) ProductPicturesMapping
                        SET ColorID = JsonItem.prd_pic_color_id
                        FROM OPENJSON(@ProductsImgColorsMappingItemsJson)
		                    WITH (
			                    prd_pic_mapping_id	INT '$.prd_pic_mapping_id' ,
			                    prd_pic_color_id INT '$.prd_pic_color_id'
		                    )
	                    JsonItem
                        WHERE ProductPicturesMapping.ProductPictureMappingID = JsonItem.prd_pic_mapping_id AND ProductPicturesMapping.ProductID = @ProductId 
                        AND JsonItem.prd_pic_color_id IS NOT NULL AND JsonItem.prd_pic_color_id != 0",
                                              new
                                              {
                                                  ProductsImgColorsMappingItemsJson = FormData.ProductsImgColorsMappingItemsJson,
                                                  ProductId = FormData.ProductId,

                                              }
                          , commandType: CommandType.Text);
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
        public async Task<ProductDigitalFileMappingEntity?> GetProductDigitalFileInfoByIdDAL(int ProductId)
        {
            ProductDigitalFileMappingEntity? result = new ProductDigitalFileMappingEntity();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    var ppSql = PetaPoco.Sql.Builder.Select(@"PDFM.*, ATC.AttachmentName, ATC.AttachmentURL")
                        .From(" ProductDigitalFileMapping PDFM")
                        .InnerJoin("Attachments ATC").On("ATC.AttachmentID =  PDFM.AttachmentID")
                      .Where("PDFM.ProductID = @0", ProductId);


                    result = context.Fetch<ProductDigitalFileMappingEntity>(ppSql).FirstOrDefault();

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<bool> CheckIfProductDigitalDAL(int ProductId)
        {
            bool result = false;

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    var ppSql = PetaPoco.Sql.Builder.Select(@"PRD.ProductId, PRD.IsDigitalProduct")
                        .From("Products PRD")
                      .Where("PRD.ProductID = @0", ProductId);

                    var ProductObj = context.Fetch<Product>(ppSql).FirstOrDefault();
                    if (ProductObj != null && ProductObj.ProductId > 0 && ProductObj.IsDigitalProduct == true)
                    {
                        result = true;
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
