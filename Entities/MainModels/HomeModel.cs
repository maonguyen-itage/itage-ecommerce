using Entities.CommonModels;
using Entities.CommonModels.HomeModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MainModels
{
    public class HomeModel
    {
        public List<TwoDimensionChart>? SalesPerMonthData { get; set; }
        public List<TwoDimensionChart>? OrderRevenuePerMonth { get; set; }
        public List<SalesPopularCategoriesChart>? SalesPopularCategoriesData { get; set; }
        public List<TwoDimensionChart>? CustomersLocationWiseData { get; set; }
        public List<TwoDimensionChart>? PopularProductsData { get; set; }

        public Dictionary<string, object>? distinctPopularCategoriesChartDic { get; set; }
        public DashboardLifeTimeStatistics? dashboardLifeTimeStatistics { get; set; }
        public PageBasicInfo? PageBasicInfoObj { get; set; }
        public PagerHelper? pageHelperObj { get; set; }
        public SuccessErrorMsgEntity? SuccessErrorMsgEntityObj { get; set; }
    }
}
