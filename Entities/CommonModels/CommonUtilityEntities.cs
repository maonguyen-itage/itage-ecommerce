using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.CommonModels
{
    public class CommonUtilityEntities
    {
        public int id { get; set; }
    }

    public class ListingDeleteButton
    {
        public int EntityId { get; set; }
        public string? primarykeyValue { get; set; }
        public string? primaryKeyColumn { get; set; }
        public string? tableName { get; set; }
        public int SqlDeleteType { get; set; }
    }

    public class HtmlFormFieldsEntity
    {
        public string? FieldID { get; set; }
        public string? FieldName { get; set; }
        public string? PlaceHolderText { get; set; }
        public string? ToolTipText { get; set; }
        public string? LabelText { get; set; }
        public string? LabelID { get; set; }
        public string? FieldType { get; set; }
        public short FieldTypeID { get; set; }
        public bool IsRequired { get; set; }
        public bool IsReadOnly { get; set; }

        public string? DefaultValue { get; set; }
        public bool ShowDropdownDefaultSelect { get; set; } = true;
        public string? GridColumnClass { get; set; }
        public string? FieldOnChangeFunction { get; set; }
        public string? FieldOnFocusFunction { get; set; }

        //--Dic key will be drop down value and Dic value will drop down label.
        public Dictionary<string, string>? DropdownData { get; set; }
        public int DropdownSelectType { get; set; } = 1; //--Normal, Select2
    }

    public class SearchFilterModel
    {
        public List<HtmlFormFieldsEntity>? SearchFilterEntityList { get; set; }
        public string? SearchJSFunctionName { get; set; }
        public string? SearchButtonType { get; set; } //--button | submit

        public string? SearchURL { get; set; }
        public int? EntityId { get; set; }
    }

    public class SuccessErrorMsgEntity
    {
        public string? SuccessMsg { get; set; }
        public string? ErrorMsg { get; set; }
        public List<string>? validationList { get; set; }
    }

    public class PageHeader
    {
        public int EntityId { get; set; }
        public string? PageTitle { get; set; }

        public bool ShowActionsButton { get; set; }
        public bool ShowExportToPdfButton { get; set; }
        public bool ShowExportToExcelButton { get; set; }
        public bool ShowPrintInvoiceButton { get; set; }
        public bool ShowGoBackButton { get; set; }

        public bool ShowAddNewButton { get; set; }
        public string AddNewButtonType { get; set; } = "OpenModal";    //--OpenModal, OpenNewLink
        public string? AddNewButtonNewLinkUrl { get; set; }
        public string? DataExportUrl { get; set; }

        public  List<HtmlTags>? HtmlTagsList { get; set; }
    }

    public class PageBasicInfo
    {
        public string? PageTitle { get; set; }
        public int EntityId { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoKeywords { get; set; } //--Comma seperated
        public string? SeoDescription { get; set; }
        public string langCode { get; set; } = "en";
        public string? TitleTagHtmlId { get; set; }
    }

    public class HtmlBootstrapModalEntity
    {
        public List<HtmlFormFieldsEntity>? htmlFormFieldsEntities { get; set; }

        public string? ModalTitle { get; set; }
        public string? ModalDivId { get; set; } 

        public string? ModalMainDivHtmlClasses { get; set; }
        public string? SubmitButtonText { get; set; }
        public string? CancelButtonText { get; set; }
        public bool ShowSubmitButton { get; set; } = true;
        public bool ShowCancelButton { get; set; } = true;

        public string? SubmitBtnJsFunctionName { get; set; }
        public string? FormId { get; set; }
        public string? FormType { get; set; }//--Post, Get
        public int? ModalSizeType { get; set; } //--LargeModal, MediumModal, SmallModal
    }

    public class HtmlDropDownRemoteData
    {
        public string? DisplayValue { get; set; }
        public string? DisplayText { get; set; }
    }

    public class HtmlTags
    {
        public string? FieldID { get; set; }
        public string? FieldName { get; set; }
        public string? PlaceHolderText { get; set; }
        public string? ToolTipText { get; set; }
        public string? LabelText { get; set; }
        public string? TagType { get; set; }

        public string? HtmlClass { get; set; }
        public string? IconClass { get; set; }
        public string? FieldOnChangeFunction { get; set; }
        public string? FieldOnFocusFunction { get; set; }
        public string? FieldOnClickFunction { get; set; }
    }
}
