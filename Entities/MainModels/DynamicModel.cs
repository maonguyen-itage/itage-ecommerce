using Entities.CommonModels;
using Entities.CommonModels.ConfigurationModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Entities.DBInheritedModels.InheritedEntitiesLevelTwo;

namespace Entities.MainModels
{
    public class DynamicModel
    {
        public List<LanguageEntity>? LanguagesList { get; set; }
        public List<AppModuleEntity>? AppModulesList { get; set; }
        public List<RolesEntity>? RolesList { get; set; }
        public List<AppConfigEntity>? AppConfigList { get; set; }
        public List<LocalizationCommonJsonEntity>? LocalizationCommonJsonList { get; set; }
        public LocalizationCommonJsonEntity? LocalizationCommonJsonObj { get; set; }
        public List<ScrnsLocalizationEntity>? ScrnsLocalizationList { get; set; }
        public List<LocalizationLabelsInfoEntity>? LocalizationList { get; set; }
        public List<LocalizationLabelInfoBase>? LocalizationDynamicLabelsBaseList { get; set; }
        public List<LocalizationDynamicLabelInfoChild>? LocalizationDynamicLabelsChildList { get; set; }

        public PageBasicInfo? PageBasicInfoObj { get; set; }
        public PagerHelper? pageHelperObj { get; set; }
        public SuccessErrorMsgEntity? SuccessErrorMsgEntityObj { get; set; }
    }
}
