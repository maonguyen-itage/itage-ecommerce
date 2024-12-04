using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels.HomeModule
{
    public class SalesPopularCategoriesChart
    {
        public int CategoryID { get; set; }
        public int? ParentCategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? ParentCategoryName { get; set; }
        public int TotalSale { get; set; }
    }

    public class DashboardLifeTimeStatistics
    {
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }
        public int TotalUsers { get; set; }
        public int TotalIncome { get; set; }
    }
}
