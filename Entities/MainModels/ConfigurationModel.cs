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
    public class ConfigurationModel
    {
        public List<RoleRightEntity>? RoleRightList { get; set; }
        public List<EntityEntity>? EntityList { get; set; }
        public List<EntityEntity>? EntityListSearchDropDown { get; set; }
        public List<RightEntity>? RightsList { get; set; }
        public List<LanguageEntity>? LanguagesList { get; set; }
        public List<AppModuleEntity>? AppModulesList { get; set; }
        public List<RolesEntity>? RolesList { get; set; }
        public List<AppConfigEntity>? AppConfigList { get; set; }
        public List<ScrnsLocalizationEntity>? ScrnsLocalizationList { get; set; }
        public List<LocalizationLabelsInfoEntity>? LocalizationList { get; set; }
        public List<LocalizationLabelInfoBase>? LocalizationMenuLabelsBaseList { get; set; }
        public List<LocalizationMenuLabelInfoChild>? LocalizationMenuLabelsChildList { get; set; }
        public List<MenuNavigationEntity>? menuNavigationList { get; set; }
       
        public MenuNavigationEntity? MenuNavigationObj { get; set; }
        public PageBasicInfo? PageBasicInfoObj { get; set; }
        public PagerHelper? pageHelperObj { get; set; }
        public SuccessErrorMsgEntity? SuccessErrorMsgEntityObj { get; set; }
    }
}
