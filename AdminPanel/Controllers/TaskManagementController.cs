using AdminPanel.Helpers.TaskManagement;
using DAL.Repository.IServices;
using DAL.Repository.Services;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using Entities.CommonModels;
using Entities.CommonModels.ConfigurationModule;
using Entities.CommonModels.SalesModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Entities.MainModels;
using Helpers.ApiHelpers;
using Helpers.AuthorizationHelpers;
using Helpers.CommonHelpers;
using Helpers.CommonHelpers.Enums;
using Helpers.ConversionHelpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509.Qualified;
using Org.BouncyCastle.Crypto;
using PetaPoco;
using Stripe;
using System.Drawing.Printing;
using System.Net.Mail;

namespace AdminPanel.Controllers
{
    public class TaskManagementController : BaseController
    {
        private readonly IBasicDataServicesDAL _basicDataDAL;
        private readonly IConfigurationServicesDAL _configurationServicesDAL;
        private readonly IConstants _constants;
        private readonly ICommonServicesDAL _commonServicesDAL;
        private readonly ISessionManager _sessionManag;
        private readonly IUserManagementServicesDAL _userManagementServicesDAL;
        private readonly IFilesHelpers _filesHelpers;
        private readonly ITasksManagementServices _tasksManagementServices;
        private readonly ISalesServicesDAL _salesServicesDAL;
        private readonly ITaskManagementExternsions _taskManagementExternsions;

        public TaskManagementController(IBasicDataServicesDAL basicDataDAL, IConfigurationServicesDAL configurationServicesDAL, IConstants constants, ICommonServicesDAL commonServicesDAL,
            ISessionManager sessionManag, IUserManagementServicesDAL userManagementServicesDAL, IFilesHelpers filesHelpers, ITasksManagementServices tasksManagementServices,
            ISalesServicesDAL salesServicesDAL, ITaskManagementExternsions taskManagementExternsions)
        {
            this._basicDataDAL = basicDataDAL;
            this._configurationServicesDAL = configurationServicesDAL;
            this._constants = constants;
            this._commonServicesDAL = commonServicesDAL;
            this._sessionManag = sessionManag;
            this._userManagementServicesDAL = userManagementServicesDAL;
            this._filesHelpers = filesHelpers;
            this._tasksManagementServices = tasksManagementServices;
            this._salesServicesDAL = salesServicesDAL;
            this._taskManagementExternsions = taskManagementExternsions;
        }

        [HttpGet]
        [RolesRightsAuthorizationHelper((int)EntitiesEnum.RequestsQueue, 0, 0, 0, (short)UserRightsEnum.View_All, (short)UserRightsEnum.View_Self)]
        public async Task<IActionResult> RequestsQueue(RequestsQueueEntity FormData)
        {
            // ✅ Main Model
            TasksManagementModel model = new TasksManagementModel();

            #region page basic info
            model.PageBasicInfoObj = new PageBasicInfo();
            model.PageBasicInfoObj.PageTitle = "Requests Queue";
            model.PageBasicInfoObj.EntityId = (int)EntitiesEnum.RequestsQueue;
            model.PageBasicInfoObj.langCode = await _sessionManag.GetLanguageCodeFromSession();
            #endregion

            try
            {
                // Get request types list
                RequestTypeEntity requestTypeEntity = new RequestTypeEntity()
                {
                    PageNo = 1,
                    PageSize = 5000
                };
                model.requestTypesList = await this._tasksManagementServices.GetRequestTypeListDAL(requestTypeEntity);

                // Get request status list
                RequestStatusEntity requestStatusEntity = new RequestStatusEntity()
                {
                    PageNo = 1,
                    PageSize = 5000
                };
                model.requestStatusesList = await this._tasksManagementServices.GetRequestStatusListDAL(requestStatusEntity);
                FormData.PageSize = this._constants.ITEMS_PER_PAGE();
                model.requestsQueueList = await _tasksManagementServices.GetRequestQueueListDAL(FormData);

                #region pagination data
                model.pageHelperObj = new PagerHelper();
                int TotalRecords = model?.requestsQueueList?.FirstOrDefault()?.TotalRecords ?? 0;
                model.pageHelperObj = PagerHelper.Instance.MakePaginationObject(model?.requestsQueueList?.Count() ?? 0, TotalRecords, _constants.ITEMS_PER_PAGE(), FormData.PageNo);
                #endregion
                if (FormData.DataExportType != null && FormData.DataExportType == (short)DataExportTypeEnum.Excel && model?.requestsQueueList?.Count > 0)
                {
                    var ExcelFileResutl = await this._filesHelpers.ExportToExcel(this, model.PageBasicInfoObj.PageTitle, model.requestsQueueList.Cast<dynamic?>().ToList());
                    return ExcelFileResutl;
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
                return PartialView("~/Views/TaskManagement/PartialViews/_RequestsQueue.cshtml", model);
            }

            return View(model);
        }

        [HttpGet]
        [RolesRightsAuthorizationHelper((int)EntitiesEnum.RequestsQueue, 0, 0, 0, (short)UserRightsEnum.View_All, (short)UserRightsEnum.View_Self)]
        public async Task<IActionResult> GetVendorRequestDataForRequestQueue(int TaskId)
        {
            // ✅ Main Model
            TasksManagementModel model = new TasksManagementModel();
            try
            {
                #region page basic info
                model.PageBasicInfoObj = new PageBasicInfo();
                model.PageBasicInfoObj.PageTitle = "Vendor Request";
                model.PageBasicInfoObj.EntityId = (int)EntitiesEnum.RequestsQueue;
                model.PageBasicInfoObj.langCode = await _sessionManag.GetLanguageCodeFromSession();
                #endregion

                // ✅ Task Data
                model.requestsQueueEntity = new RequestsQueueEntity();
                model.requestsQueueEntity.TaskId = TaskId;
                model.requestsQueueEntity.PageNo = 1;
                model.requestsQueueEntity.PageSize = 1;
                model.requestsQueueList = await this._tasksManagementServices.GetRequestQueueListDAL(model.requestsQueueEntity);
                var RequestQueueObj = model?.requestsQueueList?.FirstOrDefault();
                if (RequestQueueObj != null)
                {
                    model.requestsQueueEntity.RequestStatusId = RequestQueueObj.RequestStatusId;
                    model.requestsQueueEntity.RequestTypeId = RequestQueueObj.RequestTypeId;
                }
                // Get request status list
                RequestStatusEntity requestStatusEntity = new RequestStatusEntity()
                {
                    PageNo = 1,
                    PageSize = 5000
                };
                model.requestStatusesList = await this._tasksManagementServices.GetRequestStatusListDAL(requestStatusEntity);

                // Get vendor request detail
                model.vendorsAccountRequestObj = new VendorsAccountRequestEntity();
                model.vendorsAccountRequestObj = await this._tasksManagementServices.GetVendorAccountCreationRequestByTaskIdDAL(TaskId);

                //--Get countries list
                CountryEntity countryEntity = new CountryEntity()
                {
                    PageNo = 1,
                    PageSize = 1,
                    CountryId = model?.vendorsAccountRequestObj?.AddressOneCountryId ?? 0,
                };
                model.CountriesList = await this._userManagementServicesDAL.GetCountriesListDAL(countryEntity);

                // Get states list
                StateProvinceEntity stateProvinceEntity = new StateProvinceEntity()
                {
                    PageNo = 1,
                    PageSize = 1,
                    StateProvinceId = model?.vendorsAccountRequestObj?.AddressOneStateId ?? 0,
                };
                model.StatesList = await this._userManagementServicesDAL.GetStateProvinceListDAL(stateProvinceEntity);

                // Get cities list
                CityEntity cityEntity = new CityEntity()
                {
                    PageNo = 1,
                    PageSize = 1,
                    CityId = model?.vendorsAccountRequestObj?.AddressOneCityId ?? 0,
                };
                model.CititesList = await this._userManagementServicesDAL.GetCitiesListDAL(cityEntity);

                // Get address type
                AddressTypeEntity addressTypeEntity = new AddressTypeEntity()
                {
                    PageNo = 1,
                    PageSize = 1,
                    AddressTypeId = model?.vendorsAccountRequestObj?.AddressOneTypeId ?? 0,
                };
                model.AddressTypeList = await _userManagementServicesDAL.GetAddressTypesListDAL(addressTypeEntity);
            }
            catch (Exception ex)
            {
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);

                #region error model
                model.SuccessErrorMsgEntityObj = new SuccessErrorMsgEntity();
                model.SuccessErrorMsgEntityObj.ErrorMsg = "An error occured. Please try again.";
                #endregion
            }

            return PartialView("~/Views/TaskManagement/PartialViews/_VendorRequest.cshtml", model);
        }

        [HttpPost]
        [RolesRightsAuthorizationHelper((int)EntitiesEnum.RequestsQueue, (short)UserRightsEnum.Add, (short)UserRightsEnum.Update, 0, 0, 0)]
        public async Task<IActionResult> SubmitTaskManagementRequest(RequestsQueueEntity FormData, int DataOperationType = (short)DataOperationType.Update)
        {
            // ✅ Main Model
            TasksManagementModel model = new TasksManagementModel();
            try
            {
                if (FormData == null || FormData.TaskId < 1)
                {
                    return Json(new { success = false, message = "Empty Task Id! Please try again!" });
                }
                if (FormData.RequestStatusId < 1)
                {
                    return Json(new { success = false, message = "Please select request status id!" });
                }

                // Get task queue request row
                RequestsQueueEntity searchRequestEntity = new RequestsQueueEntity
                {
                    TaskId = FormData.TaskId,
                    PageNo = 1,
                    PageSize = 1,
                };
                model.requestsQueueList = await this._tasksManagementServices.GetRequestQueueListDAL(searchRequestEntity);
                var RequestQueueObj = model?.requestsQueueList?.FirstOrDefault();
                if (RequestQueueObj == null || RequestQueueObj.TaskId < 1)
                {
                    return Json(new { success = false, message = "Empty Task Id! Please try again!" });
                }
                RequestQueueObj.RequestStatusId = FormData.RequestStatusId;
                RequestQueueObj.LoginUserId = await this._sessionManag.GetLoginUserIdFromSession();

                string result = string.Empty;
                switch (RequestQueueObj.RequestTypeId)
                {
                    case (int)RequestTypesEnum.VendorRequest:
                        if (FormData.RequestStatusId == (int)RequestStatusEnum.Approved)
                        {
                            result = await _taskManagementExternsions.PersistVendorRequest(FormData.TaskId);
                            if (!String.IsNullOrWhiteSpace(result) && result == "Saved Successfully!")
                            {
                                result = await _tasksManagementServices.UpdateRequestsQueueStatusDAL(RequestQueueObj);
                            }
                        }
                        else
                        {
                            result = await _tasksManagementServices.UpdateRequestsQueueStatusDAL(RequestQueueObj);
                        }

                        break;

                    case (int)RequestTypesEnum.OrderRefundRequest:
                        if (FormData.RequestStatusId == (int)RequestStatusEnum.Approved)
                        {
                            result = await _taskManagementExternsions.PersistOrderRefundRequest(FormData.TaskId);
                            if (!String.IsNullOrWhiteSpace(result) && result == "Saved Successfully!")
                            {
                                result = await _tasksManagementServices.UpdateRequestsQueueStatusDAL(RequestQueueObj);
                            }
                        }
                        else
                        {
                            result = await _tasksManagementServices.UpdateRequestsQueueStatusDAL(RequestQueueObj);
                        }

                        break;
                    default:
                        //Console.WriteLine("The value is not 1, 2, or 3");
                        break;
                }

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
        [RolesRightsAuthorizationHelper((int)EntitiesEnum.OrderDetail, (short)UserRightsEnum.Add, 0, 0, 0, 0)]
        public async Task<IActionResult> RaiseOrderRefundRequest(OrderRefundParam FormData)
        {
            try
            {
                // ✅ Main Model
                TasksManagementModel model = new TasksManagementModel();
                string ValidationMsg = "Form is valid";
                List<string> validationList = new List<string>();

                #region validation area

                ValidationMsg = FormData == null ? "Form is null!" : "Form is valid";
                validationList.Add(ValidationMsg);
                ValidationMsg = FormData != null && !String.IsNullOrWhiteSpace(FormData.InputRefundReason) ? "Form is valid" : "Please fill refund reason detail!";
                validationList.Add(ValidationMsg);
                ValidationMsg = FormData != null && FormData.OrderId > 0 ? "Form is valid" : "Order id is null!";
                validationList.Add(ValidationMsg);
                ValidationMsg = FormData != null && FormData.RefundReasonTypeId > 0 ? "Form is valid" : "Please select refund reason type!";
                validationList.Add(ValidationMsg);


                if (validationList.Count() > 0 && validationList.Where(a => a != "Form is valid").ToList().Count > 0)
                {
                    return Json(new { success = false, message = validationList.FirstOrDefault(x => x != "Form is valid") });
                }

                #endregion

                #region check if request already exist
                RequestsQueueEntity RequestsQueueEntityParam = new RequestsQueueEntity
                {
                    PageNo = 1,
                    PageSize = 1,
                    RequestTypeId = (int)RequestTypesEnum.OrderRefundRequest,
                    ReferenceId = FormData.OrderId
                };
                var requestsQueueList = await _tasksManagementServices.GetRequestQueueListDAL(RequestsQueueEntityParam);
                if (requestsQueueList != null && requestsQueueList.Any())
                {
                    return Json(new { success = false, message = String.Format("A refund request already exist with status {0}", requestsQueueList?.FirstOrDefault()?.StatusKeyName) });
                }
                #endregion

                //-- Get order detail by id
                OrderEntity OrderFormData = new OrderEntity()
                {
                    OrderId = FormData.OrderId,
                };
                var OrderObj = await _salesServicesDAL.GetOrderDetailByIdDAL(OrderFormData);

                decimal RefundAmount = 0M;
                if (OrderObj != null)
                {
                    if (FormData.IsFullRefund == false && (FormData.RefundAmount < 1 || FormData.RefundAmount > OrderObj.OrderTotal))
                    {
                        return Json(new { success = false, message = "Please enter correct refund amount less than order total amount!" });
                    }
                    //--if partially refund then take the amount that we sent from front end
                    RefundAmount = Convert.ToDecimal(FormData.IsFullRefund == false ? FormData.RefundAmount : OrderObj.OrderTotal);
                }

                #region persist request queue
                string result = string.Empty;
                RequestsQueueEntity requestsQueueEntity = new RequestsQueueEntity
                {
                    RequestStatusId = (int)RequestStatusEnum.New,
                    ReferenceId = FormData.OrderId, //-- Order id as reference number in request queue
                    RequestTypeId = (int)RequestTypesEnum.OrderRefundRequest,
                    Comment = "A new refund request created from admin panel",
                    LoginUserId = await this._sessionManag.GetLoginUserIdFromSession()
                };
                var requestsQueueObj = await _tasksManagementServices.CreateRequestQueueDAL(requestsQueueEntity, (int)DataOperationType.Insert);
                if (requestsQueueObj != null && requestsQueueObj.TaskId > 0)
                {
                    bool IsOrderStatusActive = (OrderObj?.LatestStatusId ?? 0) == (int)OrderStatusesEnum.Active ? true : false;
                    var OrderPaymentsList = JsonConvert.DeserializeObject<List<OrdersPaymentEntity>>(OrderObj?.OrderPaymentDetailsJson ?? "[]");
                    var OrderPaymentDefault = OrderPaymentsList?.FirstOrDefault();

                    if (OrderObj != null && OrderObj.OrderId > 0 && OrderObj.OrderTotal > 0 && IsOrderStatusActive == true &&
                        OrderPaymentDefault != null && (OrderPaymentDefault.PaymentMethodId == (int)PaymentMethodsEnum.PayPal || OrderPaymentDefault.PaymentMethodId == (int)PaymentMethodsEnum.Stripe))
                    {
                        OrderRefundRequestEntity orderRefundRequestEntity = new OrderRefundRequestEntity
                        {
                            OrderId = FormData.OrderId,
                            TaskId = requestsQueueObj.TaskId,
                            RefundReasonDesc = FormData.InputRefundReason ?? "",
                            RefundReasonTypeId = FormData.RefundReasonTypeId,
                            IsFullRefund = FormData.IsFullRefund,
                            CurrencyId = OrderPaymentDefault?.CurrencyId ?? 0,
                            RefundAmount = RefundAmount,
                            LoginUserId = Convert.ToInt32(await this._sessionManag.GetLoginUserIdFromSession()),
                        };

                        result = await _tasksManagementServices.SaveOrderRefundRequestDataDAL(orderRefundRequestEntity);
                        ValidationMsg = result == "Saved Successfully!" ? "Saved Successfully!" : "An error occured. Please try again!";
                        validationList.Add(ValidationMsg);
                    }
                    else
                    {
                        ValidationMsg = "Order status is other than Active. Can not be refunded!";
                        validationList.Add(ValidationMsg);
                    }
                }
                else
                {
                    ValidationMsg = "An error occured. Please try again!";
                    validationList.Add(ValidationMsg);
                }

                #endregion
                if (validationList.Count() > 0 && validationList.Where(a => a != "Form is valid" && a != "Saved Successfully!").ToList().Count > 0)
                {
                    return Json(new { success = false, message = "An error occured. Please try again!" });
                }
                else
                {
                    return Json(new { success = true, message = "Saved Successfully!" });
                }
            }
            catch (Exception ex)
            {
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);
                return Json(new { success = false, message = "An error occured on server side.", ExMsg = ex.Message });
            }
        }

        [HttpGet]
        [RolesRightsAuthorizationHelper((int)EntitiesEnum.RequestsQueue, 0, 0, 0, (short)UserRightsEnum.View_All, (short)UserRightsEnum.View_Self)]
        public async Task<IActionResult> GetOrderRefundRequestDataForRequestQueue(int TaskId)
        {
            // ✅ Main Model
            TasksManagementModel model = new TasksManagementModel();

            try
            {
                #region page basic info
                model.PageBasicInfoObj = new PageBasicInfo();
                model.PageBasicInfoObj.PageTitle = "Order Refund Request";
                model.PageBasicInfoObj.EntityId = (int)EntitiesEnum.RequestsQueue;
                model.PageBasicInfoObj.langCode = await _sessionManag.GetLanguageCodeFromSession();
                #endregion
                // ✅ Task Data
                model.requestsQueueEntity = new RequestsQueueEntity();
                model.requestsQueueEntity.TaskId = TaskId;
                model.requestsQueueEntity.PageNo = 1;
                model.requestsQueueEntity.PageSize = 1;
                model.requestsQueueList = await this._tasksManagementServices.GetRequestQueueListDAL(model.requestsQueueEntity);
                var RequestQueueObj = model?.requestsQueueList?.FirstOrDefault();
                if (RequestQueueObj != null)
                {
                    model.requestsQueueEntity.RequestStatusId = RequestQueueObj.RequestStatusId;
                    model.requestsQueueEntity.RequestTypeId = RequestQueueObj.RequestTypeId;
                }
                // Get request status list
                RequestStatusEntity requestStatusEntity = new RequestStatusEntity()
                {
                    PageNo = 1,
                    PageSize = 5000
                };
                model.requestStatusesList = await this._tasksManagementServices.GetRequestStatusListDAL(requestStatusEntity);

                // Get order refund request detail
                model.OrderRefundRequestObj = new OrderRefundRequestEntity();
                model.OrderRefundRequestObj = await this._tasksManagementServices.GetOrderRefundRequestByTaskIdDAL(TaskId);

                //-- Get order detail by id
                if (model?.OrderRefundRequestObj != null)
                {
                    OrderEntity OrderFormData = new OrderEntity()
                    {
                        OrderId = model.OrderRefundRequestObj.OrderId,
                    };
                    model.OrderObj = await _salesServicesDAL.GetOrderDetailByIdDAL(OrderFormData);
                    //-- Get payment method
                    var PaymentMethodsList = JsonConvert.DeserializeObject<List<OrdersPaymentEntity>>(model?.OrderObj?.OrderPaymentDetailsJson ?? "[]");
                    model.OrderRefundRequestObj.PaymentMethodName = PaymentMethodsList?.FirstOrDefault()?.PaymentMethodName;
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

            return PartialView("~/Views/TaskManagement/PartialViews/_OrderRefundRequest.cshtml", model);
        }
    }
}
