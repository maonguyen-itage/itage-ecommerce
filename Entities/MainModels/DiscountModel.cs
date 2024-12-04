using Entities.CommonModels;
using Entities.DBInheritedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MainModels
{
    public class DiscountModel
    {
        public List<DiscountEntity>? DiscountsList { get; set; }
        public List<DiscountTypeEntity>? DiscountTypeList { get; set; }
        public ProductEntity? ProductObj { get; set; }
        public DiscountEntity? DiscountObj { get; set; }
        public List<ProductEntity>? ProductsList { get; set; }
        public List<DiscountsCampaignEntity>? DiscountsCampaignList { get; set; }
        public DiscountsCampaignEntity? DiscountsCampaignObj { get; set; }
        public List<DiscountProductsMappingEntity>? DiscountProductsMappingList { get; set; }
        public List<DiscountCategoriesMappingEntity>? DiscountCategoriesMappingList { get; set; }
        public List<CategoryEntity>? CategoryList { get; set; }
        public List<ContactUsEntity>? ContactUsList { get; set; }
        public List<SubscriberEntity>? SubscribersList { get; set; }
        public List<HomeScreenBannerEntity>? HomeScreenBannersList { get; set; }

        public PageBasicInfo? PageBasicInfoObj { get; set; }
        public PagerHelper? pageHelperObj { get; set; }
        public SuccessErrorMsgEntity? SuccessErrorMsgEntityObj { get; set; }
    }
}
