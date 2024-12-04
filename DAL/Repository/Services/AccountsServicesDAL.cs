using DAL.DBContext;
using DAL.Repository.IServices;
using Dapper;
using Entities.CommonModels.AccountsModule;
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
    public class AccountsServicesDAL : IAccountsServicesDAL
    {
        private readonly IConfiguration _configuration;
        private readonly IDataContextHelper _contextHelper;
        private readonly IDapperConnectionHelper _dapperConnectionHelper;

        //--Constructor of the class
        public AccountsServicesDAL(IConfiguration configuration, IDataContextHelper contextHelper, IDapperConnectionHelper dapperConnectionHelper)
        {
            _configuration = configuration;
            _contextHelper = contextHelper;
            _dapperConnectionHelper = dapperConnectionHelper;
        }
        public async Task<List<BankMasterEntity>> GetBankMasterDAL(BankMasterEntity FormData)
        {
            List<BankMasterEntity> result = new List<BankMasterEntity>();
            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.BankMasterId > 0)
                    {
                        SearchParameters.Append("AND MTBL.BankMasterId =  @0 ", FormData.BankMasterId);
                    }

                    if (FormData.CreatedBy > 0)
                    {
                        SearchParameters.Append("AND MTBL.CreatedBy =  @0 ", FormData.CreatedBy);
                    }

                    if (!String.IsNullOrEmpty(FormData.BankName))
                    {
                        SearchParameters.Append("AND MTBL.BankName LIKE  @0", "%" + FormData.BankName + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*, BS.StatusName , CTR.CountryName, BTYP.IndustryName")
                      .From(" BankMaster MTBL")
                      .LeftJoin("BankIndustryType BTYP").On("MTBL.IndustryTypeID =  BTYP.IndustryTypeID")
                      .LeftJoin("BankStatuses BS").On("BS.BankStatusID = MTBL.BankStatusID")
                      .LeftJoin("Countries CTR").On("MTBL.CountryID=  CTR.CountryID")
                      .Where("MTBL.BankMasterID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.BankMasterID DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<BankMasterEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BankIndustryTypeEntity>> GetBankIndustryTypeDAL(BankIndustryTypeEntity FormData)
        {
            List<BankIndustryTypeEntity> result = new List<BankIndustryTypeEntity>();

            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.IndustryTypeId > 0)
                    {
                        SearchParameters.Append("AND MTBL.IndustryTypeId =  @0 ", FormData.IndustryTypeId);
                    }

                    if (FormData.CreatedBy > 0)
                    {
                        SearchParameters.Append("AND MTBL.CreatedBy =  @0 ", FormData.CreatedBy);
                    }

                    if (!String.IsNullOrEmpty(FormData.IndustryName))
                    {
                        SearchParameters.Append("AND MTBL.IndustryName LIKE  @0", "%" + FormData.IndustryName + "%");
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
                      .From(" BankIndustryType MTBL")
                      .Where("MTBL.IndustryTypeID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.IndustryTypeID DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<BankIndustryTypeEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<BankStatusEntity>> GetBankStatusesDAL(BankStatusEntity FormData)
        {
            List<BankStatusEntity> result = new List<BankStatusEntity>();

            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.BankStatusId > 0)
                    {
                        SearchParameters.Append("AND MTBL.BankStatusId =  @0 ", FormData.BankStatusId);
                    }

                    if (FormData.CreatedBy > 0)
                    {
                        SearchParameters.Append("AND MTBL.CreatedBy =  @0 ", FormData.CreatedBy);
                    }

                    if (!String.IsNullOrEmpty(FormData.StatusName))
                    {
                        SearchParameters.Append("AND MTBL.StatusName LIKE  @0", "%" + FormData.StatusName + "%");
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
                      .From(" BankStatuses MTBL")
                      .Where("MTBL.BankStatusId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.BankStatusId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<BankStatusEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> SaveUpdateBankMasterDAL(BankMasterEntity FormData, int DataOperationType)
        {
            string result = "";
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    if (DataOperationType == 1)
                    {
                        context.Execute(@"INSERT INTO BankMaster(BankName	,IndustryTypeID	,BankCode,	SwiftCode,	BankStatusID,	CountryID,	WebUrl,	CreatedOn,	CreatedBy)
                        VALUES(@BankName	, @IndustryTypeId	,@BankCode,	@SwiftCode,	@BankStatusId,	@CountryId,	@WebUrl,	GETDATE(),	@LoginUserId)",
                        new
                        {
                            BankName = FormData.BankName,
                            IndustryTypeId = FormData.IndustryTypeId,
                            BankCode = FormData.BankCode,
                            SwiftCode = FormData.SwiftCode,
                            BankStatusId = FormData.BankStatusId,
                            CountryId = FormData.CountryId,
                            WebUrl = FormData.WebUrl,
                            LoginUserId = FormData.LoginUserId,

                        });
                        result = "Saved Successfully!";
                    }
                    else if (DataOperationType == 2)
                    {
                        context.Execute(@"UPDATE TOP(1) BankMaster SET BankName=@BankName	,IndustryTypeID=@IndustryTypeId	,BankCode=@BankCode,	SwiftCode=@SwiftCode,	BankStatusID=@BankStatusId,	CountryID=@CountryId,	WebUrl=@WebUrl , ModifiedOn = GETDATE() , ModifiedBy = @LoginUserId WHERE BankMasterId = @BankMasterId;",
                       new
                       {
                           BankMasterId = FormData.BankMasterId,
                           BankName = FormData.BankName,
                           IndustryTypeId = FormData.IndustryTypeId,
                           BankCode = FormData.BankCode,
                           SwiftCode = FormData.SwiftCode,
                           BankStatusId = FormData.BankStatusId,
                           CountryId = FormData.CountryId,
                           WebUrl = FormData.WebUrl,
                           LoginUserId = FormData.LoginUserId,
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
        public async Task<List<BankAccountEntity>> GetUserBankAccountsDAL(BankAccountEntity FormData)
        {
            List<BankAccountEntity> result = new List<BankAccountEntity>();

            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.BankAccountId > 0)
                    {
                        SearchParameters.Append("AND MTBL.BankAccountId =  @0 ", FormData.BankAccountId);
                    }

                    if (FormData.UserId > 0)
                    {
                        SearchParameters.Append("AND MTBL.UserId =  @0 ", FormData.UserId);
                    }

                    if (!String.IsNullOrEmpty(FormData.AccountNo))
                    {
                        SearchParameters.Append("AND MTBL.AccountNo LIKE  @0", "%" + FormData.AccountNo + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.BankName))
                    {
                        SearchParameters.Append("AND BM.BankName LIKE  @0", "%" + FormData.BankName + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.AcountTitle))
                    {
                        SearchParameters.Append("AND (MTBL.AcountTitle LIKE  @0)", "%" + FormData.AcountTitle + "%");
                    }

                    if (!String.IsNullOrEmpty(FormData.AccountHolderName))
                    {
                        SearchParameters.Append("AND (USR.FirstName LIKE  @0 OR USR.LastName LIKE  @0)", "%" + FormData.AccountHolderName + "%");
                    }

                    if (FormData.CreatedBy > 0)
                    {
                        SearchParameters.Append("AND MTBL.CreatedBy =  @0 ", FormData.CreatedBy);
                    }

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(MTBL.CreatedOn AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*, (ISNULL(USR.FirstName,'') + SPACE(1) + ISNULL(USR.LastName,'')) AS AccountHolderName , BTYP.AccountTypeName, BM.BankName")
                      .From(" BankAccounts MTBL")
                      .InnerJoin("Users USR").On("USR.UserID =  MTBL.UserID")
                      .InnerJoin("BankAccountType BTYP").On("MTBL.AccountTypeID= BTYP.AccountTypeID")
                      .InnerJoin("BankMaster BM").On("BM.BankMasterID = MTBL.BankMasterID")
                      .Where("MTBL.BankAccountID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.BankAccountID DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<BankAccountEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BankAccountType>> GetBankAccountTypesDAL(BankAccountType FormData)
        {
            List<BankAccountType> result = new List<BankAccountType>();

            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (!String.IsNullOrEmpty(FormData.AccountTypeName))
                    {
                        SearchParameters.Append("AND MTBL.AccountTypeName LIKE  @0", "%" + FormData.AccountTypeName + "%");
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*")
                      .From(" BankAccountType MTBL")
                      .Where("MTBL.AccountTypeId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.AccountTypeId DESC");

                    result = context.Fetch<BankAccountType>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> CreateUpdateUserBankAccount(BankAccountEntity FormData)
        {
            string result = "";

            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();
                    dbConnection.Execute("SP_AdmPanel_CreateUpdateBankAccount",
                        new
                        {
                            BankAccountId = FormData.BankAccountId,
                            UserId = FormData.UserId,
                            AccountTypeId = FormData.AccountTypeId,
                            BankBranchName = FormData.BankBranchName,
                            BankBranchCode = FormData.BankBranchCode,
                            AccountNo = FormData.AccountNo,
                            Iban = FormData.Iban,
                            AcountTitle = FormData.AcountTitle,
                            Description = FormData.Description,
                            BankMasterId = FormData.BankMasterId,
                            StateProvinceId = FormData.StateProvinceId,
                            CityId = FormData.CityId,
                            BranchAddress = FormData.BranchAddress,
                            IsActive = FormData.IsActive,
                            BankAttachementsJson = FormData.BankAttachementsJson,
                            DataOperationType = FormData.DataOperationType,
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

        public async Task<BankAccountEntity?> GetBankAccountDetailByIdDAL(int BankAccountId)
        {
            BankAccountEntity? result = new BankAccountEntity();
            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var ppSql = PetaPoco.Sql.Builder.Select(@" TOP 1 BA.* , USR.FirstName , USR.LastName, USR.EmailAddress, USRTYP.UserTypeName , BAT.AccountTypeName, BM.BankName, SP.StateName, CT.CityName")
                      .From(" BankAccounts BA")
                      .InnerJoin("Users USR").On(" BA.UserID= USR.UserID")
                       .LeftJoin("UserTypes USRTYP").On(" USR.UserTypeId= USRTYP.UserTypeId")
                      .InnerJoin("BankAccountType BAT").On(" BA.AccountTypeID = BAT.AccountTypeID")
                      .InnerJoin("BankMaster BM ").On(" BA.BankMasterID =  BM.BankMasterID")
                      .LeftJoin("StateProvinces SP ").On(" BA.StateProvinceID= SP.StateProvinceID")
                      .LeftJoin("Cities CT ").On(" BA.CityID= CT.CityID")
                      .Where("BA.BankAccountID = @0", BankAccountId);

                    result = context.Fetch<BankAccountEntity>(ppSql).FirstOrDefault();
                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<BankAccountAttachmentEntity>> GetBankAccountAttachmentListDAL(BankAccountAttachmentEntity FormData)
        {
            List<BankAccountAttachmentEntity> result = new List<BankAccountAttachmentEntity>();

            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.BankAccountId > 0)
                    {
                        SearchParameters.Append("AND MTBL.BankAccountId =  @0 ", FormData.BankAccountId);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*, ATC.AttachmentURL")
                      .From(" BankAccountAttachment MTBL")
                      .LeftJoin(" Attachments ATC").On(" ATC.AttachmentID =  MTBL.AttachmentID")
                      .Where("MTBL.BankAccountId is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.BankAccountId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<BankAccountAttachmentEntity>(ppSql);

                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<VendorsCommissionSetupEntity>> GetVendorsCommissionSetupDAL(VendorsCommissionSetupEntity FormData)
        {
            List<VendorsCommissionSetupEntity> result = new List<VendorsCommissionSetupEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    if (FormData.UserId > 0)
                    {
                        SearchParameters.Append("AND MTBL.UserID =  @0 ", FormData.UserId);
                    }

                    if (!String.IsNullOrEmpty(FormData.FirstName))
                    {
                        SearchParameters.Append("AND MTBL.FirstName LIKE  @0", "%" + FormData.FirstName + "%");
                    }
                    if (!String.IsNullOrEmpty(FormData.LastName))
                    {
                        SearchParameters.Append("AND MTBL.LastName LIKE  @0", "%" + FormData.LastName + "%");
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(MTBL.UserID) OVER () as TotalRecords, MTBL.UserID, MTBL.FirstName , MTBL.LastName, MTBL.EmailAddress , VCS.VendorCommissionID, VCS.CommissionType, VCS.CommissionValue, VCS.IsActive, VCS.ApplicableFrom,
                    VCS.ApplicableTo")
                      .From(" Users MTBL")
                      .LeftJoin(" VendorsCommissionSetup VCS").On(" MTBL.UserID = VCS.UserID AND VCS.IsActive = 1")
                      .Where("MTBL.UserTypeId = 3")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.UserId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<VendorsCommissionSetupEntity>(ppSql);
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
        public async Task<string> UpdateVendorCommissionDAL(VendorsCommissionSetupEntity FormData, int DataOperationType)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();
                    dbConnection.Execute(@"DECLARE @VendorCommissionId INT;
	                        INSERT INTO VendorsCommissionSetup(UserID , CommissionType , CommissionValue , ApplicableFrom , ApplicableTo, IsActive, CreatedOn , CreatedBy)
	                        VALUES(@UserId , 'Percentage' , @CommissionValue , @ApplicableFrom , @ApplicableTo, @IsActive, GETDATE() , @LoginUserId);
							SET @VendorCommissionId = SCOPE_IDENTITY();

							UPDATE top(40) VendorsCommissionSetup
							SET IsActive = 0
							WHERE VendorCommissionID != @VendorCommissionId AND UserID = @UserId",
                        new
                        {
                            UserId = FormData.UserId,
                            CommissionValue = FormData.CommissionValue,
                            ApplicableFrom = FormData.ApplicableFrom,
                            ApplicableTo = FormData.ApplicableTo,
                            IsActive = FormData.IsActive,
                            LoginUserId = FormData.LoginUserId,
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
        public async Task<List<VendorsPayments>> GetVendorsPaymentsListDAL(VendorsPayments FormData)
        {
            List<VendorsPayments> result = new List<VendorsPayments>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    context.EnableAutoSelect = false;
                    result = context.Fetch<VendorsPayments>(@";EXEC [dbo].[SP_AdmPanel_GetVendorsPaymentsList] @VendorId,@VendorFirstName,@VendorLastName, @PageNo,@PageSize ",
                          new
                          {
                              VendorId = FormData.VendorId,
                              VendorFirstName = FormData.VendorFirstName,
                              VendorLastName = FormData.VendorLastName,
                              PageNo = FormData.PageNo,
                              PageSize = FormData.PageSize

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
        public async Task<List<AccountTransactionsDetail>> GetAccountTransDetailByVendorIdDAL(AccountTransactionsDetail FormData)
        {
            List<AccountTransactionsDetail> result = new List<AccountTransactionsDetail>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (!String.IsNullOrEmpty(FormData.FromDate))
                    {
                        SearchParameters.Append("AND Cast(CTF.ProcessingDate AS Date)>=@0", FormData.FromDate);
                    }

                    if (!String.IsNullOrEmpty(FormData.ToDate))
                    {
                        SearchParameters.Append("AND Cast(CTF.ProcessingDate AS Date)<=@0", FormData.ToDate);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Append(@";WITH CTE_TransDetail AS (
	                    SELECT BATR.TransID ,BATR.TransType , BATR.TransAmount, 
	                    SUM(CASE WHEN BATR.TransType = 'Credit' then BATR.TransAmount ELSE -1*BATR.TransAmount END)OVER(PARTITION BY USR.UserID ORDER BY BATR.TransID ASC) AS RemainingBalance,
	                    BATR.ProcessingDate    ,USR.UserID as VendorId ,USR.FirstName,USR.LastName ,  BRE.EventName  , BA.BankBranchName , PaymentMethodName
	                    FROM BankAccountTrans BATR
	                    INNER JOIN BankAccounts BA ON BA.BankAccountID = BATR.BankAccountID
	                    INNER JOIN PaymentMethods PMT ON PMT.PaymentMethodID = BATR.PaymentMethodID
	                    INNER JOIN Users USR ON USR.UserID =  BA.UserID
	                    LEFT JOIN BankTransEvent BRE ON BRE.EventID = BATR.EventID
	                    WHERE USR.UserID = @0
                    ),

                    CTE_Final AS (
	                    SELECT * FROM CTE_TransDetail
                    )
                    SELECT CTF.*, COUNT(*) OVER () as TotalRecords FROM CTE_Final CTF ", FormData.VendorId)
                      .Where("CTF.TransId is not null")
                     .Append(SearchParameters)
                     .OrderBy("CTF.TransID ASC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<AccountTransactionsDetail>(ppSql);
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
        public async Task<List<BankTransEventEntity>> GetBankTransEventListDAL(BankTransEventEntity FormData)
        {
            List<BankTransEventEntity> result = new List<BankTransEventEntity>();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");

                    if (FormData.EventId > 0)
                    {
                        SearchParameters.Append("AND MTBL.EventId =  @0 ", FormData.EventId);
                    }

                    if (!String.IsNullOrEmpty(FormData.EventName))
                    {
                        SearchParameters.Append("AND MTBL.EventName LIKE  @0", "%" + FormData.EventName + "%");
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(MTBL.EventId) OVER () as TotalRecords, MTBL.* ")
                      .From(" BankTransEvent MTBL")
                      .Where("MTBL.EventId IS NOT NULL")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.EventId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<BankTransEventEntity>(ppSql);
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
        public async Task<string> CreateUpdateBankAccountTransDAL(AccountTransactionsDetail FormData)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = _dapperConnectionHelper.GetDapperContextHelper())
                {
                    dbConnection.Open();
                    dbConnection.Execute("SP_AdmPanel_CreateUpdateBankAccountTrans",
                        new
                        {
                            TransId = FormData.TransId,
                            VendorId = FormData.VendorId,
                            TransTitle = FormData.TransTitle,
                            TransType = FormData.TransType,
                            BankAccountId = FormData.BankAccountId,
                            EventId = FormData.EventId,
                            TransAmount = FormData.TransAmount,
                            PaymentMethodId = FormData.PaymentMethodId,
                            ProcessingDate = FormData.ProcessingDate,
                            Description = FormData.Description,
                            TransCurrencyId = FormData.TransCurrencyId,
                            DefaultCurrencyCode = FormData.DefaultCurrencyCode,
                            AccountTransAttachementJson = FormData.AccountTransAttachementJson,
                            DataOperationType = FormData.DataOperationType,
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

        public async Task<AccountTransactionsDetail?> GetAccountTransEditFormDataByIdDAL(int TransId)
        {
            AccountTransactionsDetail? result = new AccountTransactionsDetail();
            using (var context = _contextHelper.GetDataContextHelper())
            {
                try
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    var ppSql = PetaPoco.Sql.Builder.Append(@";WITH CTE_TransDetail AS (
	                    SELECT TOP 1 BATR.TransID ,BATR.TransType , BATR.TransAmount, BATR.BankAccountId   ,BATR.EventId ,
	                    SUM(CASE WHEN BATR.TransType = 'Credit' then BATR.TransAmount ELSE -1*BATR.TransAmount END)OVER(PARTITION BY USR.UserID ORDER BY BATR.TransID ASC) AS RemainingBalance,
	                    BATR.ProcessingDate, BATR.TransTitle, BATR.Description   ,USR.UserID as VendorId ,USR.FirstName,USR.LastName ,  BRE.EventName  , BA.BankBranchName , PaymentMethodName,
						PMT.PaymentMethodId
	                    FROM BankAccountTrans BATR
	                    INNER JOIN BankAccounts BA ON BA.BankAccountID = BATR.BankAccountID
	                    INNER JOIN PaymentMethods PMT ON PMT.PaymentMethodID = BATR.PaymentMethodID
	                    INNER JOIN Users USR ON USR.UserID =  BA.UserID
	                    LEFT JOIN BankTransEvent BRE ON BRE.EventID = BATR.EventID
	                    WHERE BATR.TransID = @0
                    ),

                    CTE_Final AS (
	                    SELECT * FROM CTE_TransDetail
                    )

                    SELECT CTF.* FROM CTE_Final CTF ", TransId);

                    result = context.Fetch<AccountTransactionsDetail>(ppSql).FirstOrDefault();
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

        public async Task<List<AccountTransAttachmentEntity>> GetAccountTransEditFormAttachmentsDAL(AccountTransAttachmentEntity FormData)
        {
            List<AccountTransAttachmentEntity> result = new List<AccountTransAttachmentEntity>();
            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    if (FormData.AcountTransAttachId > 0)
                    {
                        SearchParameters.Append("AND MTBL.AcountTransAttachId =  @0 ", FormData.AcountTransAttachId);
                    }

                    var ppSql = PetaPoco.Sql.Builder.Select(@" COUNT(*) OVER () as TotalRecords,MTBL.*, ATC.AttachmentURL")
                      .From(" AccountTransAttachment MTBL")
                      .LeftJoin(" Attachments ATC").On(" ATC.AttachmentID =  MTBL.AttachmentID")
                      .Where("MTBL.AcountTransAttachID is not null")
                      .Append(SearchParameters)
                     .OrderBy("MTBL.AcountTransAttachId DESC")
                    .Append(@"OFFSET (@0-1)*@1 ROWS
	                FETCH NEXT @1 ROWS ONLY", FormData.PageNo, FormData.PageSize);

                    result = context.Fetch<AccountTransAttachmentEntity>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<VendorsOrdersTotalReceivedBalance>> GetVendorsOrdersTotalReceivedBalanceDAL(string CommaSeperatedVendorsIds)
        {
            List<VendorsOrdersTotalReceivedBalance> result = new List<VendorsOrdersTotalReceivedBalance>();
            try
            {
                using (var context = _contextHelper.GetDataContextHelper())
                {
                    var SearchParameters = PetaPoco.Sql.Builder.Append(" ");
                    var ppSql = PetaPoco.Sql.Builder.Append(@" ;with CTE_MAIN AS (
                        SELECT USR.UserID,OI.OrderItemID, ISNULL(VCS.CommissionValue , 0) AS CommissionValue , OI.OrderItemTotal ,
                        OI.OrderItemTotal - (ISNULL(VCS.CommissionValue,0) * OI.OrderItemTotal/100) OrderItemTotalAfterCommission
                        FROM OrderItems OI
                        INNER JOIN Products PRD ON OI.ProductID =  PRD.ProductID
                        INNER JOIN USERS USR ON PRD.VendorID =  USR.UserID
                        INNER JOIN Orders ORD ON OI.OrderID = ORD.OrderID
                        OUTER APPLY (
		                        SELECT TOP 1 * FROM VendorsCommissionSetup VCS WHERE OI.VendorCommissionID = VCS.VendorCommissionID
	                        )VCS

                        WHERE USR.UserID  IN ( select value from STRING_SPLIT (@0, ','))

                        ),
                        CTE_VendorCreditDebit as (
                        select CTM.* , 
                        ( SELECT isnull(sum(BATR.TransAmount),0) FROM BankAccountTrans BATR
                          INNER JOIN BankAccounts BA ON BATR.BankAccountID = BA.BankAccountID AND BATR.TransType = 'Credit'
                          WHERE BA.UserID = CTM.UserID
                        ) AS VendorTotalCredit ,
                        ( SELECT isnull(sum(BATR.TransAmount),0) FROM BankAccountTrans BATR
                          INNER JOIN BankAccounts BA ON BATR.BankAccountID = BA.BankAccountID AND BATR.TransType = 'Debit'
                          WHERE BA.UserID = CTM.UserID
                        ) AS VendorTotalDebit

                        from CTE_MAIN CTM

                        )
                        ,
                        CTE_Final AS (
	                        SELECT DISTINCT CF.UserID as VendorId,  cf.VendorTotalCredit, cf.VendorTotalDebit , SUM(CF.OrderItemTotalAfterCommission )OVER(PARTITION BY CF.UserId) VendorOrdersTotal,
	                        (SUM(CF.VendorTotalCredit)OVER(PARTITION BY CF.UserId) - SUM(CF.VendorTotalDebit)OVER(PARTITION BY CF.UserId)) as TotalReceived,
	                         (SUM(CF.OrderItemTotalAfterCommission )OVER(PARTITION BY CF.UserId) - (SUM(CF.VendorTotalCredit)OVER(PARTITION BY CF.UserId) - SUM(CF.VendorTotalDebit)OVER(PARTITION BY CF.UserId)))  as TotalBalance
	                        FROM CTE_VendorCreditDebit CF
                        )

                        SELECT * FROM CTE_Final", CommaSeperatedVendorsIds);

                    result = context.Fetch<VendorsOrdersTotalReceivedBalance>(ppSql);
                    await Task.FromResult(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
