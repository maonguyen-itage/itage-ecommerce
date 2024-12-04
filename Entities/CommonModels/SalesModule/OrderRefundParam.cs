using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels.SalesModule
{
    public class OrderRefundParam
    {
        public int OrderId { get; set; }
        public string? InputRefundReason { get; set; }
        public int RefundReasonTypeId { get; set; }
        public bool IsFullRefund { get; set; } = true;
        public decimal? RefundAmount { get; set; }
        public int? LoginUserId { get; set; }
    }
}
