using Entities.CommonModels;
using Entities.CommonModels.AccountsModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MainModels
{
    public class AccountsModel
    {
        public List<BankMasterEntity>? BankMasterList { get; set; }
        public List<BankAccountAttachmentEntity>? BankAccountAttachmentEntityList { get; set; }
        public List<AccountTransAttachmentEntity>? AccountTransAttachmentList { get; set; }
        public List<BankAccountEntity>? BankAccountList { get; set; }
        public List<BankAccountTranEntity>? BankAccountTransList { get; set; }
        public List<BankIndustryTypeEntity>? BankIndustryTypeList { get; set; }
        public List<BankAccountType>? BankAccountTypesList { get; set; }
        public List<BankStatusEntity>? BankStatusList { get; set; }
        public List<CountryEntity>? CountriesList { get; set; }
        public List<UserTypesEntity>? UserTypesList { get; set; }
        public List<UserEntity>? UsersList { get; set; }
        public List<VendorsPayments>? VendorsPaymentsList { get; set; }
        public List<VendorsOrdersTotalReceivedBalance>? VendorsOrdersTotalReceivedBalanceList { get; set; }
        public List<AccountTransactionsDetail>? AccountTransactionsDetailList { get; set; }
        public List<PaymentMethodEntity>? PaymentMethodsList { get; set; }
        public List<BankTransEventEntity>? BankTransEventsList { get; set; }
        public List<VendorsCommissionSetupEntity>? VendorsCommissionSetupList { get; set; }
        public BankAccountEntity? BankAccountEntityObj { get; set; }
        public AccountTransactionsDetail? AccountTransactionsDetailObj { get; set; }
        public UserEntity? UserObj { get; set; }

        public PageBasicInfo? PageBasicInfoObj { get; set; }
        public PagerHelper? pageHelperObj { get; set; }
        public SuccessErrorMsgEntity? SuccessErrorMsgEntityObj { get; set; }
    }
}
