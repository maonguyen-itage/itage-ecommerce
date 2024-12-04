using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels.AccountsModule
{
    public class VendorsPayments
    {
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int OrderId { get; set; }
        public int OrderItemID { get; set; }
        public DateTime OrderDateUTC { get; set; }
        public int VendorId { get; set; }
        public decimal OrderItemTotal { get; set; }
        public decimal OrderItemTotalAfterCommission { get; set; }
        public decimal CommissionValue { get; set; }

        public string? VendorFirstName { get; set; }
        public string? VendorLastName { get; set; }
        public string? EmailAddress { get; set; }

        #region extra fields for total
        public decimal VendorTotalCredit { get; set; }
        public decimal VendorTotalDebit { get; set; }
        public decimal VendorOrdersTotal { get; set; }
        public decimal TotalReceived { get; set; }
        public decimal TotalBalance { get; set; }

        #endregion

        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
        public int? CreatedBy { get; set; }
    }

    public class VendorsOrdersTotalReceivedBalance
    {
        public int VendorId { get; set; }
        public decimal VendorTotalCredit { get; set; }
        public decimal VendorTotalDebit { get; set; }
        public decimal VendorOrdersTotal { get; set; }
        public decimal TotalReceived { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
