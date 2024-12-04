using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels.ProductsCatalogModule
{
    public class ProductDigitalFileMappingEntity
    {
        public int ProductDigitalFileMappingId { get; set; }
        public int ProductId { get; set; }
        public int AttachmentId { get; set; }
        public string? AttachmentName { get; set; }
        public string? AttachmentURL { get; set; }
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }
}
