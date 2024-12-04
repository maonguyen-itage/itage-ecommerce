using Entities.DBModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels.AccountsModule
{
    public class AccountTransactionsDetail : BankAccountTran
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
      
        public decimal RemainingBalance { get; set; }
      
        public int VendorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EventName { get; set; }
        public string? BankBranchName { get; set; }
        public string? PaymentMethodName { get; set; }
        public string? DefaultCurrencyCode { get; set; }

        public IFormFile[]? AccountTransAttachementFile { get; set; }

        public string? AccountTransAttachementJson { get; set; }
        public int DataOperationType { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }
}
