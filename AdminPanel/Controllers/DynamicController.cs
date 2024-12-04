using DAL.Repository.IServices;
using DAL.Repository.Services;
using Entities.CommonModels;
using Entities.CommonModels.ConfigurationModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Entities.MainModels;
using Helpers.AuthorizationHelpers;
using Helpers.CommonHelpers;
using Helpers.CommonHelpers.Enums;
using Helpers.ConversionHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration.Internal;
using Newtonsoft.Json;
using System.ComponentModel;

namespace AdminPanel.Controllers
{
    public class DynamicController : BaseController
    {
        private readonly IBasicDataServicesDAL _basicDataDAL;
        private readonly IConstants _constants;
        private readonly ICommonServicesDAL _commonServicesDAL;
        private readonly ISessionManager _sessionManag;
        private readonly IConfigurationServicesDAL _configurationServicesDAL;

        public DynamicController(IBasicDataServicesDAL basicDataDAL, IConstants constants, ICommonServicesDAL commonServicesDAL, ISessionManager sessionManag, IConfigurationServicesDAL configurationServicesDAL)
        {
            this._basicDataDAL = basicDataDAL;
            this._constants = constants;
            this._commonServicesDAL = commonServicesDAL;
            this._sessionManag = sessionManag;
            _configurationServicesDAL = configurationServicesDAL;
        }

        // ✅ Delete any record using this method Dynamcially
        [HttpPost]
        [RolesRightsAuthorizationHelper(0, 0, 0, (short)UserRightsEnum.Delete, 0, 0)]
        public async Task<IActionResult> DeleteAnyRecord(string EntityId, string primarykeyValue, string primaryKeyColumn, string tableName, int SqlDeleteType = (short)SqlDeleteTypes.PlainTableDelete)
        {


            try
            {
                bool result = await _commonServicesDAL.DeleteAnyRecordDAL(primarykeyValue, primaryKeyColumn, tableName, SqlDeleteType);
                if (result)
                {
                    return Json(new { success = true, message = "Deleted Successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "An error occured on server side." });
                }
            }
            catch (Exception ex)
            {

                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);

                return Json(new { success = false, message = "An error occured on server side.", ExMsg = ex.Message });
            }


        }

        [HttpPost]
        public async Task<IActionResult> ChangeDataTableLength(int itemsPerPage)
        {


            try
            {

                HttpContext.Session.SetInt32("ITEMS_PER_PAGE", itemsPerPage);

                return Json(new { success = true, message = "Saved Successfully!" });

            }
            catch (Exception ex)
            {

                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);

                return Json(new { success = false, message = "An error occured on server side.", ExMsg = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLocalizationControlsJsonDataForScreen(int EntityId)
        {


            try
            {

                if (EntityId < 1)
                {
                    return Json(new { success = false, message = "Incorrect screen id!" });
                }

                string? langCode = await _sessionManag.GetLanguageCodeFromSession();
                if (String.IsNullOrWhiteSpace(langCode))
                {
                    return Json(new { success = false, message = "Incorrect language code!" });
                }


                ScrnsLocalizationEntity scrnsLocalization = new ScrnsLocalizationEntity()
                {
                    ScreenId = EntityId,
                    AppModuleId = (short)AppModulesEnum.AdminPanel,
                    LanguageId = CommonConversionHelper.GetLanguageIdbyLanguageCode(langCode)
                };
                var result = await _commonServicesDAL.GetScreenLocalizationJsonDataDAL(scrnsLocalization);

                if (result != null && !String.IsNullOrWhiteSpace(result.LabelsJsonData))
                {
                    return Json(new { success = true, message = "success", dataLocalization = result.LabelsJsonData });
                }
                else
                {
                    return Json(new { success = false, message = result });
                }
            }
            catch (Exception ex)
            {

                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);

                return Json(new { success = false, message = "An error occured on server side.", ExMsg = ex.Message });
            }
        }


        [HttpGet]
        [RolesRightsAuthorizationHelper((int)EntitiesEnum.DynamicLocalizationDetail, (short)UserRightsEnum.Add, 0, 0, 0, 0)]
        public async Task<IActionResult> DynamicLocalizationDetail(string PageTitle, int LocalizationTableId, int PKeyId)
        {
            // ✅ Main Model
            DynamicModel model = new DynamicModel();

            #region page basic info
            model.PageBasicInfoObj = new PageBasicInfo();
            model.PageBasicInfoObj.PageTitle = (PageTitle ?? "Page") + " Localization";
            model.PageBasicInfoObj.EntityId = (int)EntitiesEnum.DynamicLocalizationDetail;
            model.PageBasicInfoObj.langCode = await _sessionManag.GetLanguageCodeFromSession();
            #endregion

            try
            {


                LanguageEntity languageEntity = new LanguageEntity()
                {
                    PageNo = 1,
                    PageSize = 200
                };
                model.LanguagesList = await this._configurationServicesDAL.GetLanguagesListDAL(languageEntity);
                model.LanguagesList = model?.LanguagesList?.Where(x => x.IsActive == true).ToList();


                LocalizationCommonJsonEntity FormData = new LocalizationCommonJsonEntity()
                {
                    PageSize = 100,
                    LocalizationTableId = LocalizationTableId,
                    PrimaryKeyId = PKeyId,
                    PageNo = 1
                };


                model.LocalizationCommonJsonList = await _commonServicesDAL.GetCommonLocalizationJsonDataListDAL(FormData);
                model.LocalizationCommonJsonObj = model?.LocalizationCommonJsonList?.FirstOrDefault();



                if (model?.LocalizationCommonJsonObj != null && !String.IsNullOrEmpty(model?.LocalizationCommonJsonObj.LocalizationJsonData))
                {
                    model.LocalizationDynamicLabelsChildList = JsonConvert.DeserializeObject<List<LocalizationDynamicLabelInfoChild>?>(model?.LocalizationCommonJsonObj.LocalizationJsonData);

                    #region pagination data
                    model.pageHelperObj = new PagerHelper();
                    int TotalRecords = model?.LocalizationDynamicLabelsChildList?.Count() ?? 0;
                    model.LocalizationDynamicLabelsChildList = model?.LocalizationDynamicLabelsChildList?.Skip((FormData.PageNo - 1) * FormData.PageSize).Take(FormData.PageSize).ToList();
                    model.pageHelperObj = PagerHelper.Instance.MakePaginationObject(model?.LocalizationDynamicLabelsChildList?.Count() ?? 0, TotalRecords, _constants.ITEMS_PER_PAGE(), FormData.PageNo);
                    #endregion


                    foreach (var item in model.LocalizationDynamicLabelsChildList)
                    {
                        item.Title = model?.LocalizationCommonJsonObj?.TableName;
                        item.LanguageName = model?.LanguagesList?.Where(x => x.LanguageId == item.langId).FirstOrDefault()?.LanguageName;
                        item.LocalCommonDataId = model.LocalizationCommonJsonObj.LocalCommonDataId;
                        item.LocalizationTableId = model.LocalizationCommonJsonObj.LocalizationTableId;
                        item.PrimaryKeyId = model.LocalizationCommonJsonObj.PrimaryKeyId;
                    }

                }
                else
                {
                    model.LocalizationCommonJsonObj = new LocalizationCommonJsonEntity();
                    model.LocalizationCommonJsonObj.LocalizationTableId = LocalizationTableId;
                    model.LocalizationCommonJsonObj.PrimaryKeyId = PKeyId;



                }




            }
            catch (Exception ex)
            {
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);

                #region error model
                model.SuccessErrorMsgEntityObj = new SuccessErrorMsgEntity();
                model.SuccessErrorMsgEntityObj.ErrorMsg = "An error occured. Please try again.";
                #endregion

            }

            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")//if request is ajax
            {
                return PartialView("~/Views/dynamic/PartialViews/_DynamicLocalizationDetail.cshtml", model);
            }

            return View(model);
        }


        [HttpPost]
        [RolesRightsAuthorizationHelper((int)EntitiesEnum.DynamicLocalizationDetail, (short)UserRightsEnum.Add, 0, 0, 0, 0)]
        public async Task<IActionResult> SaveDynamicLocalizationLabelText(LocalizationDynamicLabelInfoChild FormData, int DataOperationType = (short)DataOperationType.Insert)
        {
            // ✅ Main Model
            DynamicModel model = new DynamicModel();

            try
            {

                #region validation region
                if (FormData.LocalizationTableId < 1)
                {
                    return Json(new { success = false, message = "Localization Table Id is null!" });
                }
                if (FormData.PrimaryKeyId < 1)
                {
                    return Json(new { success = false, message = "Primary Key Id is null!" });
                }
                if (String.IsNullOrWhiteSpace(FormData.text))
                {
                    return Json(new { success = false, message = "Text field is required!" });
                }

                if (FormData.langId == null || FormData.langId < 1)
                {
                    return Json(new { success = false, message = "Language is null" });
                }

                #endregion



                LocalizationCommonJsonEntity FormDataEntity = new LocalizationCommonJsonEntity()
                {
                    PageSize = 100,
                    LocalCommonDataId = FormData.LocalCommonDataId,
                    LocalizationTableId = FormData.LocalizationTableId,
                    PrimaryKeyId = FormData.PrimaryKeyId,
                    PageNo = 1
                };


                model.LocalizationCommonJsonList = await _commonServicesDAL.GetCommonLocalizationJsonDataListDAL(FormDataEntity);
                model.LocalizationCommonJsonObj = model?.LocalizationCommonJsonList?.FirstOrDefault();



                if (model?.LocalizationCommonJsonObj != null && !String.IsNullOrEmpty(model.LocalizationCommonJsonObj.LocalizationJsonData))
                {
                    model.LocalizationDynamicLabelsBaseList = JsonConvert.DeserializeObject<List<LocalizationLabelInfoBase>?>(model.LocalizationCommonJsonObj.LocalizationJsonData);

                    LocalizationLabelInfoBase? DynamicLableIfo = new LocalizationLabelInfoBase();
                    if (model.LocalizationDynamicLabelsBaseList != null && model.LocalizationDynamicLabelsBaseList.Count() > 0 &&
                        model.LocalizationDynamicLabelsBaseList.Where(x => x.langId == FormData.langId)?.ToList().Count() > 0)
                    {
                        DynamicLableIfo = model.LocalizationDynamicLabelsBaseList.Where(x => x.langId == FormData.langId).FirstOrDefault();
                        DynamicLableIfo.langId = FormData.langId;
                        DynamicLableIfo.text = FormData.text;
                        if (model == null || model.LocalizationDynamicLabelsBaseList == null)
                        {
                            model.LocalizationDynamicLabelsBaseList = new List<LocalizationLabelInfoBase>();
                        }


                    }
                    else
                    {
                        DynamicLableIfo.langId = FormData.langId;
                        DynamicLableIfo.text = FormData.text;
                        if (model == null || model.LocalizationDynamicLabelsBaseList == null)
                        {
                            model.LocalizationDynamicLabelsBaseList = new List<LocalizationLabelInfoBase>();
                        }
                        model.LocalizationDynamicLabelsBaseList.Add(DynamicLableIfo);
                    }

                    model.LocalizationCommonJsonObj.LocalizationJsonData = JsonConvert.SerializeObject(model.LocalizationDynamicLabelsBaseList);

                }
                else if (model?.LocalizationCommonJsonObj != null && model?.LocalizationCommonJsonObj.LocalCommonDataId > 0 && String.IsNullOrEmpty(model.LocalizationCommonJsonObj.LocalizationJsonData)) //-- if only json data is null but row exist for the table
                {
                    LocalizationLabelInfoBase? DynamicLableIfo = new LocalizationLabelInfoBase();
                    DynamicLableIfo.langId = FormData.langId;
                    DynamicLableIfo.text = FormData.text;

                    if (model == null || model.LocalizationDynamicLabelsBaseList == null)
                    {
                        model.LocalizationDynamicLabelsBaseList = new List<LocalizationLabelInfoBase>();
                    }
                    model.LocalizationDynamicLabelsBaseList.Add(DynamicLableIfo);
                    model.LocalizationCommonJsonObj.LocalizationJsonData = JsonConvert.SerializeObject(model.LocalizationDynamicLabelsBaseList);
                }
                else if (model?.LocalizationCommonJsonObj == null)//--New insert case. Now data exists for this table in "LocalizationCommonJson" table
                {
                    LocalizationLabelInfoBase? DynamicLableIfo = new LocalizationLabelInfoBase();
                    DynamicLableIfo.langId = FormData.langId;
                    DynamicLableIfo.text = FormData.text;

                    if (model == null || model.LocalizationDynamicLabelsBaseList == null)
                    {
                        model.LocalizationDynamicLabelsBaseList = new List<LocalizationLabelInfoBase>();
                    }
                    model.LocalizationDynamicLabelsBaseList.Add(DynamicLableIfo);

                    //--Initialize entity
                    model.LocalizationCommonJsonObj = new LocalizationCommonJsonEntity();
                    model.LocalizationCommonJsonObj.LocalizationTableId = FormData.LocalizationTableId;
                    model.LocalizationCommonJsonObj.PrimaryKeyId = FormData.PrimaryKeyId;

                    model.LocalizationCommonJsonObj.LocalizationJsonData = JsonConvert.SerializeObject(model.LocalizationDynamicLabelsBaseList);
                }
                else
                {
                    return Json(new { success = false, message = "No data/info exists" });
                }

                model.LocalizationCommonJsonObj.LoginUserId = await this._sessionManag.GetLoginUserIdFromSession();



                string result = await _commonServicesDAL.SaveDynamicLocalizationLabelDAL(model.LocalizationCommonJsonObj);
                if (!String.IsNullOrWhiteSpace(result) && result == "Saved Successfully!")
                {
                    return Json(new { success = true, message = "Saved Successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = result });
                }
            }
            catch (Exception ex)
            {

                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);

                return Json(new { success = false, message = "An error occured on server side.", ExMsg = ex.Message });
            }
        }


        [HttpPost]
        [RolesRightsAuthorizationHelper((int)EntitiesEnum.DynamicLocalizationDetail, 0, 0, (short)UserRightsEnum.Delete, 0, 0)]
        public async Task<IActionResult> DeleteDynamicLocalizationText(LocalizationDynamicLabelInfoChild FormData, int DataOperationType = (short)DataOperationType.Delete)
        {
            // ✅ Main Model
            DynamicModel model = new DynamicModel();

            try
            {

                #region validation region
                if (FormData.LocalCommonDataId < 1)
                {
                    return Json(new { success = false, message = "Local Common Data Id is null!" });
                }

                if (FormData.langId == null || FormData.langId < 1)
                {
                    return Json(new { success = false, message = "Language is null" });
                }

                #endregion

                LocalizationCommonJsonEntity FormDataEntity = new LocalizationCommonJsonEntity()
                {
                    PageSize = 100,
                    LocalCommonDataId = FormData.LocalCommonDataId,
                    LocalizationTableId = FormData.LocalizationTableId,
                    PrimaryKeyId = FormData.PrimaryKeyId,
                    PageNo = 1
                };


                model.LocalizationCommonJsonList = await _commonServicesDAL.GetCommonLocalizationJsonDataListDAL(FormDataEntity);
                model.LocalizationCommonJsonObj = model?.LocalizationCommonJsonList?.FirstOrDefault();



                if (model?.LocalizationCommonJsonObj != null && !String.IsNullOrEmpty(model.LocalizationCommonJsonObj.LocalizationJsonData))
                {
                    model.LocalizationDynamicLabelsBaseList = JsonConvert.DeserializeObject<List<LocalizationLabelInfoBase>?>(model.LocalizationCommonJsonObj.LocalizationJsonData);
                    var deleteMenuRow = model?.LocalizationDynamicLabelsBaseList?.Where(x => x.langId == FormData.langId)?.FirstOrDefault();

                    if (deleteMenuRow != null && model != null && model.LocalizationDynamicLabelsBaseList != null)
                    {
                        model.LocalizationDynamicLabelsBaseList.Remove(deleteMenuRow);

                        model.LocalizationCommonJsonObj.LocalizationJsonData = JsonConvert.SerializeObject(model.LocalizationDynamicLabelsBaseList);
                    }
                    else
                    {
                        return Json(new { success = false, message = "No menu information exists" });
                    }



                }
                else
                {
                    return Json(new { success = false, message = "No menu information exists" });
                }

                model.LocalizationCommonJsonObj.LoginUserId = await this._sessionManag.GetLoginUserIdFromSession();



                string result = await _commonServicesDAL.SaveDynamicLocalizationLabelDAL(model.LocalizationCommonJsonObj);
                if (!String.IsNullOrWhiteSpace(result) && result == "Saved Successfully!")
                {
                    return Json(new { success = true, message = "Deleted Successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = result });
                }


            }
            catch (Exception ex)
            {

                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);

                return Json(new { success = false, message = "An error occured on server side.", ExMsg = ex.Message });
            }
        }

    }


}
