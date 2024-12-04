using Entities.CommonModels;
using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MainModels
{
    public class TasksManagementModel
    {
        public List<RequestsQueueEntity>? requestsQueueList { get; set; }
        public List<RequestStatusEntity>?  requestStatusesList { get; set; }
        public List<RequestTypeEntity>? requestTypesList { get; set; }
        public List<CountryEntity>? CountriesList { get; set; }
        public List<StateProvinceEntity>? StatesList { get; set; }
        public List<CityEntity>? CititesList { get; set; }
        public List<AddressTypeEntity>? AddressTypeList { get; set; }
        public VendorsAccountRequestEntity? vendorsAccountRequestObj { get; set; }
        public OrderEntity? OrderObj { get; set; }
        public OrderRefundRequestEntity? OrderRefundRequestObj { get; set; }
        public RequestsQueueEntity? requestsQueueEntity { get; set; }
        public PageBasicInfo? PageBasicInfoObj { get; set; }
        public PagerHelper? pageHelperObj { get; set; }
        public SuccessErrorMsgEntity? SuccessErrorMsgEntityObj { get; set; }
    }
}
