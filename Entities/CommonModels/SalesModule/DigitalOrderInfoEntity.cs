using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels.SalesModule
{
    public class DigitalOrderInfoEntity
    {
        public int OrderID { get; set; }
        public int OrderItemID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public bool IsDigitalProduct { get; set; }
        public string? DigitalFileDownloadUrl { get; set; }
    }
}
