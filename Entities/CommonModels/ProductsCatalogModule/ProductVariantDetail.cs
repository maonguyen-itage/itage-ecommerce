using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels.ProductsCatalogModule
{
    public class ProductVariantDetail
    {
        public int ProductAttributeId { get; set; }
        public int PrimaryKeyValue { get; set; } //-- this is for the different attribute primary value, like AttrRam table primary key column value
        public string? PrimaryKeyName { get; set; }
        public string? DisplayText { get; set; }
        public string? AttributeName { get; set; }
        public string? AttributeDisplayName { get; set; }
        public string? AttributeSqlTableName { get; set; }
     
        public int TotalRecords { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
   
        public int? DataExportType { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }
}
