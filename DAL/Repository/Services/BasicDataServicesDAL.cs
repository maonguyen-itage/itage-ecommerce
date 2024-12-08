using DAL.DBContext;
using DAL.Repository.IServices;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Services
{
    public class BasicDataServicesDAL : IBasicDataServicesDAL
    {
        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;

        //--Constructor of the class
        public BasicDataServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
        }

        public UserEntity? GetUserDataByUserID(int UserId)
        {
            UserEntity? result = new UserEntity();

            using (var repo = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var ppSql = PetaPoco.Sql.Builder.Select(@"TOP 1 usr.* , UROL.RoleID AS RoleId , ATC.AttachmentURL AS ProfilePictureUrl")
                        .From("Users usr")
                        .LeftJoin("UserRoles UROL").On("USR.UserID = UROL.UserID")
                        .LeftJoin("Attachments ATC").On("USR.ProfilePictureID = ATC.AttachmentID")
                        .Where("USR.UserID=@0", UserId);
                    result = repo.Query<UserEntity>(ppSql).FirstOrDefault();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public List<MenuNavigation> GetNavMenusList(MenuNavigation FormData)
        {
            List<MenuNavigation> result = new List<MenuNavigation>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.MenuNavigationId > 0)
                    {
                        SearchParameters.Append("AND MTBL.MenuNavigationId =  @0 ", FormData.MenuNavigationId);
                    }

                    if (!String.IsNullOrEmpty(FormData.MenuNavigationName))
                    {
                        SearchParameters.Append("AND MTBL.MenuNavigationName LIKE  @0", "%" + FormData.MenuNavigationName + "%");
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" MenuNavigations MTBL")
                      .Where("MTBL.MenuNavigationId is not null AND MTBL.IsActive = 1")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.DisplaySeqNo, MTBL.MenuNavigationID ASC");
                    result = context.Fetch<MenuNavigation>(ppSql);

                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public List<RoleRightEntity>? GetUserRoleRightsForSession(int UserID)
        {
            List<RoleRightEntity>? result = new List<RoleRightEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var ppSql = PetaPoco.Sql.Builder.Select(@"RR.RoleRightID   , RR.RoleID, RR.RightID, RR.EntityID , RGT.RightName")
                        .From("RoleRights RR")
                        .InnerJoin("Roles RL").On("RR.RoleID=RL.RoleID")
                        .InnerJoin("UserRoles USR").On("USR.RoleID=RL.RoleID")
                        .InnerJoin("Rights RGT").On("RR.RightID=RGT.RightID")
                        .Where("USR.UserID=@0", UserID);
                    result = context.Fetch<RoleRightEntity>(ppSql).ToList();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<ColorEntity>> GetColorsListDAL(ColorEntity FormData)
        {
            List<ColorEntity> result = new List<ColorEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.ColorId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ColorId =  @0 ", FormData.ColorId);
                    }

                    if (!String.IsNullOrEmpty(FormData.ColorName))
                    {
                        SearchParameters.Append("AND MTBL.ColorName LIKE  @0", "%" + FormData.ColorName + "%");
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
                      .From(" Colors MTBL")
                      .Where("MTBL.ColorID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.ColorID DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<ColorEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<CategoryEntity>> GetCategoriesListDAL(CategoryEntity FormData)
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

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*, par.Name AS ParentCategoryName, ATC.AttachmentURL")
                      .From(" Categories MTBL")
                      .LeftJoin("Categories par").On("MTBL.ParentCategoryID = par.CategoryID")
                      .LeftJoin("Attachments ATC").On("MTBL.AttachmentID = ATC.AttachmentID")
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
        public async Task<List<CategoryEntity>> GetParentCategoriesListDAL()
        {
            List<CategoryEntity> result = new List<CategoryEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var ppSql = PetaPoco.Sql.Builder.Select(@"MTBL.*")
                      .From(" Categories MTBL")
                      .Where("MTBL.ParentCategoryID is null")
                     .OrderBy("MTBL.ParentCategoryID DESC");

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

        public async Task<string> SaveUpdateColorDAL(ColorEntity FormData, int DataOperationType)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"Insert into Colors(ColorName,	HexCode,	IsActive,	DisplaySeqNo,	CreatedOn,	CreatedBy)
                        VALUES(@ColorName,	@HexCode,	@IsActive,	@DisplaySeqNo,	getdate(),	@UserId)",
                        new
                        {
                            ColorName = FormData.ColorName,
                            HexCode = FormData.HexCode,
                            IsActive = FormData.IsActive,
                            DisplaySeqNo = FormData.DisplaySeqNo,
                            UserId = FormData.UserId,

                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"UPDATE TOP(1) Colors SET ColorName = @ColorName ,HexCode = @HexCode  ,IsActive = @IsActive ,DisplaySeqNo = @DisplaySeqNo, ModifiedOn=GetDate(), ModifiedBy = @UserId WHERE ColorID = @ColorId;",
                       new
                       {
                           ColorId = FormData.ColorId,
                           ColorName = FormData.ColorName,
                           HexCode = FormData.HexCode,
                           IsActive = FormData.IsActive,
                           DisplaySeqNo = FormData.DisplaySeqNo,
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
        public async Task<string> SaveUpdateCategoryDAL(CategoryEntity FormData, int DataOperationType)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"Insert into Categories(Name,	ParentCategoryID,	IsActive,	DisplaySeqNo, AttachmentID,	CreatedOn,	CreatedBy)
                        VALUES(@Name,	@ParentCategoryID,	@IsActive,	@DisplaySeqNo,  @AttachmentId	 ,getdate(),	@UserId)",
                        new
                        {
                            Name = FormData.Name,
                            ParentCategoryID = FormData.ParentCategoryId,
                            IsActive = FormData.IsActive,
                            DisplaySeqNo = FormData.DisplaySeqNo,
                            AttachmentId = FormData.AttachmentId,
                            UserId = FormData.UserId,

                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"UPDATE TOP(1) Categories SET Name = @Name ,ParentCategoryID = @ParentCategoryID  ,IsActive = @IsActive ,DisplaySeqNo = @DisplaySeqNo, AttachmentId=@AttachmentId      , ModifiedOn=GetDate(), ModifiedBy = @UserId WHERE CategoryId = @CategoryId;",
                       new
                       {
                           CategoryId = FormData.CategoryId,
                           Name = FormData.Name,
                           ParentCategoryID = FormData.ParentCategoryId,
                           IsActive = FormData.IsActive,
                           DisplaySeqNo = FormData.DisplaySeqNo,
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
        public async Task<List<SizeEntity>> GetSizeListDAL(SizeEntity FormData)
        {
            List<SizeEntity> result = new List<SizeEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    if (FormData.SizeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.SizeId =  @0 ", FormData.SizeId);
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

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" Sizes MTBL")
                      .Where("MTBL.SizeID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.SizeId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<SizeEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<string> SaveUpdateSizeDAL(SizeEntity FormData, int DataOperationType)
        {
            string result = "";
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"Insert into Sizes(Name, ShortName,	Inches, Centimeters,	IsActive,	DisplaySeqNo,	CreatedOn,	CreatedBy)
                        VALUES(@Name, @ShortName ,	@Inches,  @Centimeters,	@IsActive,	@DisplaySeqNo,	getdate(),	@UserId)",
                        new
                        {
                            Name = FormData.Name,
                            ShortName = FormData.ShortName,
                            Inches = FormData.Inches,
                            Centimeters = FormData.Centimeters,
                            IsActive = FormData.IsActive,
                            DisplaySeqNo = FormData.DisplaySeqNo,
                            UserId = FormData.UserId,

                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"UPDATE TOP(1) Sizes SET Name = @Name , ShortName = @ShortName  ,Inches = @Inches ,Centimeters=@Centimeters  ,IsActive = @IsActive ,DisplaySeqNo = @DisplaySeqNo, ModifiedOn=GetDate(), ModifiedBy = @UserId WHERE SizeId = @SizeId;",
                       new
                       {
                           SizeId = FormData.SizeId,
                           Name = FormData.Name,
                           ShortName = FormData.ShortName,
                           Inches = FormData.Inches,
                           Centimeters = FormData.Centimeters,
                           IsActive = FormData.IsActive,
                           DisplaySeqNo = FormData.DisplaySeqNo,
                           UserId = FormData.UserId,

                       });
                        result = "Saved Successfully!";
                    }
                    else
                    {
                        result = "Please define data operation type!";
                    }

                    //context.EnableAutoSelect = false;

                    //var QueryResponse = context.Execute(@";EXEC [dbo].[SP_LogRunTimeException] @ExceptionMessage,@StackTrace , @Source",
                    //    new { ExceptionMessage = ExceptionMessage, StackTrace = StackTrace, Source = Source });

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<List<ManufacturerEntity>> GetManufacturerListDAL(ManufacturerEntity FormData)
        {
            List<ManufacturerEntity> result = new List<ManufacturerEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.ManufacturerId > 0)
                    {
                        SearchParameters.Append("AND MTBL.ManufacturerId =  @0 ", FormData.ManufacturerId);
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

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" Manufacturers MTBL")
                      .Where("MTBL.ManufacturerId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.CreatedOn DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<ManufacturerEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<string> SaveUpdateManufacturerDAL(ManufacturerEntity FormData, int DataOperationType)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"Insert into Manufacturers(Name,	IsActive,	DisplaySeqNo,	CreatedOn,	CreatedBy)
                        VALUES(@Name,	@IsActive,	@DisplaySeqNo,	getdate(),	@UserId)",
                        new
                        {
                            Name = FormData.Name,
                            IsActive = FormData.IsActive,
                            DisplaySeqNo = FormData.DisplaySeqNo,
                            UserId = FormData.UserId,
                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"UPDATE TOP(1) Manufacturers SET Name = @Name ,IsActive = @IsActive ,DisplaySeqNo = @DisplaySeqNo, ModifiedOn=GetDate(), ModifiedBy = @UserId WHERE ManufacturerId = @ManufacturerId;",
                       new
                       {
                           ManufacturerId = FormData.ManufacturerId,
                           Name = FormData.Name,
                           IsActive = FormData.IsActive,
                           DisplaySeqNo = FormData.DisplaySeqNo,
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
        public async Task<List<CurrencyEntity>> GetCurrenciesListDAL(CurrencyEntity FormData)
        {
            List<CurrencyEntity> result = new List<CurrencyEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.CurrencyId > 0)
                    {
                        SearchParameters.Append("AND MTBL.CurrencyId =  @0 ", FormData.CurrencyId);
                    }

                    if (!String.IsNullOrEmpty(FormData.CurrencyName))
                    {
                        SearchParameters.Append("AND MTBL.CurrencyName LIKE  @0", "%" + FormData.CurrencyName + "%");
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
                      .From(" Currencies MTBL")
                      .Where("MTBL.CurrencyId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.CurrencyId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<CurrencyEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<string> SaveUpdateCurrencyDAL(CurrencyEntity FormData, int DataOperationType)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"Insert into Currencies(CurrencyName,CurrencyCode,	IsActive,	DisplaySeqNo,	CreatedOn,	CreatedBy)
                        VALUES(@CurrencyName,  @CurrencyCode,	@IsActive,	@DisplaySeqNo,	getdate(),	@UserId)",
                        new
                        {
                            CurrencyName = FormData.CurrencyName,
                            CurrencyCode = FormData.CurrencyCode,
                            IsActive = FormData.IsActive,
                            DisplaySeqNo = FormData.DisplaySeqNo,
                            UserId = FormData.UserId,

                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"UPDATE TOP(1) Currencies SET CurrencyName = @CurrencyName , CurrencyCode=@CurrencyCode ,IsActive = @IsActive ,DisplaySeqNo = @DisplaySeqNo, ModifiedOn=GetDate(), ModifiedBy = @UserId WHERE CurrencyId = @CurrencyId;",
                       new
                       {
                           CurrencyId = FormData.CurrencyId,
                           CurrencyName = FormData.CurrencyName,
                           CurrencyCode = FormData.CurrencyCode,
                           IsActive = FormData.IsActive,
                           DisplaySeqNo = FormData.DisplaySeqNo,
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
        public async Task<List<AttachmentTypeEntity>> GetAttachmentTypesListDAL(AttachmentTypeEntity FormData)
        {
            List<AttachmentTypeEntity> result = new List<AttachmentTypeEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.AttachmentTypeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.AttachmentTypeId =  @0 ", FormData.AttachmentTypeId);
                    }

                    if (!String.IsNullOrEmpty(FormData.AttachmentTypeName))
                    {
                        SearchParameters.Append("AND MTBL.AttachmentTypeName LIKE  @0", "%" + FormData.AttachmentTypeName + "%");
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
                      .From(" AttachmentTypes MTBL")
                      .Where("MTBL.AttachmentTypeId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.AttachmentTypeId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<AttachmentTypeEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<List<PaymentMethodEntity>> GetPaymentMethodsListDAL(PaymentMethodEntity FormData)
        {
            List<PaymentMethodEntity> result = new List<PaymentMethodEntity>();

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.PaymentMethodId > 0)
                    {
                        SearchParameters.Append("AND MTBL.PaymentMethodId =  @0 ", FormData.PaymentMethodId);
                    }

                    if (!String.IsNullOrEmpty(FormData.PaymentMethodName))
                    {
                        SearchParameters.Append("AND MTBL.PaymentMethodName LIKE  @0", "%" + FormData.PaymentMethodName + "%");
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
                      .From(" PaymentMethods MTBL")
                      .Where("MTBL.PaymentMethodId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.PaymentMethodId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<PaymentMethodEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public async Task<string> SaveUpdatePaymentMethodDAL(PaymentMethodEntity FormData, int DataOperationType)
        {
            string result = "";
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"Insert into PaymentMethods(PaymentMethodName,	IsActive,	DisplaySeqNo,	CreatedOn,	CreatedBy)
                        VALUES(@PaymentMethodName,	@IsActive,	@DisplaySeqNo,	getdate(),	@UserId)",
                        new
                        {
                            PaymentMethodName = FormData.PaymentMethodName,
                            IsActive = FormData.IsActive,
                            DisplaySeqNo = FormData.DisplaySeqNo,
                            UserId = FormData.UserId,

                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"UPDATE TOP(1) PaymentMethods SET PaymentMethodName = @PaymentMethodName ,IsActive = @IsActive ,DisplaySeqNo = @DisplaySeqNo, ModifiedOn=GetDate(), ModifiedBy = @UserId WHERE PaymentMethodId = @PaymentMethodId;",
                       new
                       {
                           PaymentMethodId = FormData.PaymentMethodId,
                           PaymentMethodName = FormData.PaymentMethodName,
                           IsActive = FormData.IsActive,
                           DisplaySeqNo = FormData.DisplaySeqNo,
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

        public async Task<List<TagEntity>> GetTagsListDAL(TagEntity FormData)
        {
            List<TagEntity> result = new List<TagEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.TagId > 0)
                    {
                        SearchParameters.Append("AND MTBL.TagId =  @0 ", FormData.TagId);
                    }

                    if (FormData.CreatedBy != null && FormData.CreatedBy > 0)
                    {
                        SearchParameters.Append("AND MTBL.CreatedBy =  @0 ", FormData.CreatedBy);
                    }

                    if (!String.IsNullOrEmpty(FormData.TagName))
                    {
                        SearchParameters.Append("AND MTBL.TagName LIKE  @0", "%" + FormData.TagName + "%");
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
                      .From(" Tags MTBL")
                      .Where("MTBL.TagId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.TagId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<TagEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<string> SaveUpdateTagDAL(TagEntity FormData, int DataOperationType)
        {
            string result = "";

            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"IF NOT EXISTS(SELECT TOP 1 EXC.TagId FROM Tags EXC WHERE EXC.TagName=@TagName)
                            BEGIN
                                INSERT INTO Tags(TagName,	IsActive,	CreatedOn,	CreatedBy)
                                VALUES(@TagName,	@IsActive,	getdate(),	@UserId)
                            END",
                        new
                        {
                            TagName = FormData.TagName,
                            IsActive = FormData.IsActive,
                            UserId = FormData.UserId,
                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"IF NOT EXISTS(SELECT TOP 1 EXC.TagId FROM Tags EXC WHERE EXC.TagName=@TagName AND TagId!=@TagId)
                            BEGIN
                                 UPDATE TOP(1) Tags SET TagName = @TagName  ,IsActive = @IsActive , ModifiedOn=GetDate(), ModifiedBy = @UserId WHERE TagId = @TagId;
                            END",
                        new
                        {
                            TagId = FormData.TagId,
                            TagName = FormData.TagName,
                            IsActive = FormData.IsActive,
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
