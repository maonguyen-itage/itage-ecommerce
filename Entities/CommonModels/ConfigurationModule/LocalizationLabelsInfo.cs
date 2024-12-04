using Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels.ConfigurationModule
{
    //--Do not add any extra columns to this class. All the columns in this class should matched columns in ScrnsLocalization table json columns
    public class LocalizationLabelsInfo 
    {
        public string? labelHtmlId { get; set; }
        public string? text { get; set; }
        public string? description { get; set; }
        public string? toolTip { get; set; }
    }

    public class LocalizationLabelsInfoEntity : LocalizationLabelsInfo
    {
        public int? LanguageId { get; set; }
        public int? EntityId { get; set; }
        public int? AppModuleId { get; set; }
        public string? ScreenName { get; set; }
        public string? LanguageName { get; set; }
        public int ScrnLocalizationId { get; set; }

        public int TotalRecords { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }

    public class LocalizationLabelInfoBase //--Do not add more columns to this entity
    {
        public int? langId { get; set; }
        public string? text { get; set; }
    }

    public class LocalizationMenuLabelInfoChild : LocalizationLabelInfoBase
    {
        public string? MenuNavigationName { get; set; }
        public int MenuNavigationId { get; set; }
        public string? LanguageName { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }

    public class LocalizationDynamicLabelInfoChild : LocalizationLabelInfoBase
    {
        public int LocalCommonDataId { get; set; }
        public int LocalizationTableId { get; set; }
        public int PrimaryKeyId { get; set; }
        public string? Title { get; set; }
        public string? LanguageName { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LoginUserId { get; set; }
    }
}
