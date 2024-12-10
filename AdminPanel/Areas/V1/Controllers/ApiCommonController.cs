using AdminPanel.Helpers.EmailSenderHelper;
using DAL.Repository.IServices;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Helpers.ApiHelpers;
using Helpers.AuthorizationHelpers;
using Helpers.AuthorizationHelpers.JwtTokenHelper;
using Helpers.CommonHelpers;
using Helpers.CommonHelpers.Enums;
using Helpers.CommonHelpers.ICommonHelpers;
using Helpers.ConversionHelpers;
using Helpers.ConversionHelpers.IConversionHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Stripe;
using Stripe.FinancialConnections;
using System.Diagnostics;
using System.Security.Policy;

namespace AdminPanel.Areas.V1.Controllers
{
    [Route("api/v1/common")] //-- "common" is controller name with out api keyword"
    [ApiController]
    [Area("V1")]
    public class ApiCommonController : ControllerBase
    {
        private readonly IApiOperationServicesDAL _apiOperationServicesDAL;
        private readonly ICalculationHelper _calculationHelper;
        private readonly ICommonServicesDAL _commonServicesDAL;
        private readonly ISessionManager _sessionManag;
        private readonly IConstants _constants;
        private readonly IUserManagementServicesDAL _userManagementServicesDAL;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly IOrderHelper _orderHelper;
        private readonly IProductServicesDAL _productServicesDAL;
        private readonly IBasicDataServicesDAL _basicDataServicesDAL;
        private readonly ISalesServicesDAL _salesServicesDAL;
        private readonly IFilesHelpers _filesHelpers;

        public ApiCommonController(IApiOperationServicesDAL apiOperationServices, ICommonServicesDAL commonServicesDAL, ISessionManager sessionManag,
            IConstants constants, ICalculationHelper calculationHelper, IUserManagementServicesDAL userManagementServicesDAL, IConfiguration configuration,
            IEmailSender emailSender, IOrderHelper orderHelper, IProductServicesDAL productServicesDAL, IBasicDataServicesDAL basicDataServicesDAL, ISalesServicesDAL salesServicesDAL,
            IFilesHelpers filesHelpers)
        {
            this._apiOperationServicesDAL = apiOperationServices;
            this._commonServicesDAL = commonServicesDAL;
            this._sessionManag = sessionManag;
            this._constants = constants;
            this._calculationHelper = calculationHelper;
            this._userManagementServicesDAL = userManagementServicesDAL;
            this._configuration = configuration;
            this._emailSender = emailSender;
            this._orderHelper = orderHelper;
            this._productServicesDAL = productServicesDAL;
            this._basicDataServicesDAL = basicDataServicesDAL;
            this._salesServicesDAL = salesServicesDAL;
            this._filesHelpers = filesHelpers;
        }

        [Route("validate-email-send-otp/{UrlName?}")]
        [ServiceFilter(typeof(CustomerApiCallsAuthorization))]
        public async Task<APIActionResult> ValidateEmailAndSendOTP(string? UrlName, [FromBody] Dictionary<string, object> param)
        {
            #region Basic declaration
            //--Api result type declared in resultType variable
            string resultType = "json";

            NoorAppAPIResult result = new NoorAppAPIResult();
            APIActionResult apiActionResult;

            result.ActionType = (ActionTypeEnum)Enum.Parse(typeof(ActionTypeEnum), resultType, true);

            //--This data variable will be used for storing data
            string? data = string.Empty;
            #endregion

            try
            {
                if (param != null && param.Count != 0)
                {
                    Dictionary<string, object>? requestParameters = new Dictionary<string, object>();
                    if (param.ContainsKey("requestParameters"))
                    {
                        string? ParamKeyValue = param["requestParameters"].ToString();
                        if (!String.IsNullOrWhiteSpace(ParamKeyValue))
                        {
                            requestParameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(ParamKeyValue);
                        }
                    }

                    string? Email = requestParameters != null ? requestParameters["Email"].ToString() : "";
                    if (String.IsNullOrEmpty(Email))
                    {
                        result.StatusCode = 204;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "Please fill email field!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;
                    }

                    //-- 1. Valiedate email from data base if exists
                    var user = await _userManagementServicesDAL.GetUserByEmailAddressDAL(Email);
                    if (user == null || user.UserId < 1)
                    {
                        result.StatusCode = 204;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "Incorrect email. Please enter your correct email address!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;
                    }

                    //-- 2. Generate OTP and save in database
                    int OTP = CommonConversionHelper.GenerateRandomNumber();
                    string OTPResponseFromDB = await this._userManagementServicesDAL.SaveOTPLogInformationDAL((short)ApiStatusCodes.OK, "Peding", "OTP Generated", OTP, null, Email, null, null, true, null);

                    if (String.IsNullOrEmpty(OTPResponseFromDB) || OTPResponseFromDB != "Saved Successfully!")
                    {
                        result.StatusCode = 501;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "An error occured in saving OTP. Please try again!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;
                    }

                    //-- 3. Send OTP in email to user
                    try
                    {
                        List<EmailAddressEntity> emailAddresses = new List<EmailAddressEntity>();
                        emailAddresses.Add(new EmailAddressEntity { DisplayName = "User", Address = Email });
                        string SiteTitle = _configuration.GetSection("AppSetting").GetSection("WebsiteTitle").Value;
                        var message = new EmailMessage(emailAddresses, "Recover Password", String.Format("Your OTP for password recovery is: {0}", OTP), String.Format("{0} , Recover Password", SiteTitle));
                        _emailSender.SendEmail(message);
                    }
                    catch (Exception ex)
                    {
                        await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);

                        result.StatusCode = 501;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "An error occured in sending email. Please try again!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;
                    }

                    //-- 4. return user success and lets user enter otp that he recieve in email and password and confirm password
                    #region result

                    result.Data = "[]";
                    result.StatusCode = 200;
                    result.StatusMessage = "Ok";
                    result.Message = "Sent Successfully";
                    result.ErrorMessage = String.Empty;
                    apiActionResult = new APIActionResult(result);

                    #endregion
                }
                else
                {
                    result.StatusCode = 501;
                    result.StatusMessage = "Error";
                    result.ErrorMessage = "An error is occured while processing your request.";
                    apiActionResult = new APIActionResult(result);
                }
            }
            catch (Exception ex)
            {
                #region log error
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);
                #endregion

                result.StatusCode = 501;
                result.StatusMessage = "Error";
                result.ErrorMessage = "An error is occured while processing your request.";
                apiActionResult = new APIActionResult(result);
            }

            return apiActionResult;
        }

        [Route("validate-otp-change-password/{UrlName?}")]
        [ServiceFilter(typeof(CustomerApiCallsAuthorization))]
        public async Task<APIActionResult> ValidateOTPAndChangePassword(string? UrlName, [FromBody] Dictionary<string, object> param)
        {
            #region Basic declaration
            //--Api result type declared in resultType variable
            string resultType = "json";

            NoorAppAPIResult result = new NoorAppAPIResult();
            APIActionResult apiActionResult;

            result.ActionType = (ActionTypeEnum)Enum.Parse(typeof(ActionTypeEnum), resultType, true);

            //--This data variable will be used for storing data
            string? data = string.Empty;
            #endregion

            try
            {
                if (param != null && param.Count != 0)
                {
                    Dictionary<string, object>? requestParameters = new Dictionary<string, object>();
                    if (param.ContainsKey("requestParameters"))
                    {
                        string? ParamKeyValue = param["requestParameters"].ToString();
                        if (!String.IsNullOrWhiteSpace(ParamKeyValue))
                        {
                            requestParameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(ParamKeyValue);
                        }
                    }

                    string? Email = requestParameters != null ? requestParameters["Email"].ToString() : "";
                    int? Otp = requestParameters != null ? Convert.ToInt32(requestParameters["Otp"].ToString()) : 0;
                    string? Password = requestParameters != null ? requestParameters["Password"].ToString() : "";
                    string? ConfirmPassword = requestParameters != null ? requestParameters["ConfirmPassword"].ToString() : "";

                    #region validation area

                    if (String.IsNullOrEmpty(Email))
                    {
                        result.StatusCode = 204;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "Please fill email field!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;

                    }

                    if (Otp == null)
                    {
                        result.StatusCode = 204;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "Please fill OTP field!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;

                    }

                    if (String.IsNullOrEmpty(Password))
                    {
                        result.StatusCode = 204;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "Please enter password!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;

                    }

                    if (String.IsNullOrEmpty(ConfirmPassword))
                    {
                        result.StatusCode = 204;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "Please enter confirm password!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;

                    }

                    if (Password.Length < 6 || ConfirmPassword.Length < 6)
                    {
                        result.StatusCode = 204;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "Password & Confirm Password fields lenght should not be less than 6 characters!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;

                    }

                    if (Password != ConfirmPassword)
                    {
                        result.StatusCode = 204;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "Password does not match!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;

                    }

                    #endregion

                    //-- 1. Valiedate email from data base if exists
                    var user = await _userManagementServicesDAL.GetUserByEmailAddressDAL(Email);
                    if (user == null || user.UserId < 1)
                    {
                        result.StatusCode = 204;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "Incorrect email. Please try again!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;
                    }

                    //-- 2. Validate the OTP from data base
                    var IsValidOTP = await this._userManagementServicesDAL.ValidateOTPByEmailDAL(Email, Convert.ToInt32(Otp));

                    //--Update the OTP Count by Email
                    string UpdateOTPResponse = await this._userManagementServicesDAL.UpdateOTPAttemptsByEmailDAL(Email);

                    if (IsValidOTP != null && !String.IsNullOrWhiteSpace(IsValidOTP.EmailAddress))
                    {
                        string PasswordResetResponse = "";
                        //-- 3. Reset user password
                        Password = CommonConversionHelper.Encrypt(Password);
                        PasswordResetResponse = await this._userManagementServicesDAL.ResetUserPasswordDAL(Email, Password);

                        //--De activate otps by email address
                        string DeActivateResponse = await this._userManagementServicesDAL.DeActivateOTPsByEmail(Email);

                        if (PasswordResetResponse == "Saved Successfully!")
                        {
                            #region result

                            result.Data = "[]";
                            result.StatusCode = 200;
                            result.StatusMessage = "Ok";
                            result.Message = "Password reset successfully";
                            result.ErrorMessage = String.Empty;
                            apiActionResult = new APIActionResult(result);

                            #endregion
                        }
                        else
                        {
                            result.StatusCode = 204;
                            result.StatusMessage = "Error";
                            result.ErrorMessage = "An error occured. Please try again";
                            apiActionResult = new APIActionResult(result);
                            return apiActionResult;
                        }
                    }
                    else
                    {
                        result.StatusCode = 204;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "Invalid OTP that you enter!";
                        apiActionResult = new APIActionResult(result);
                        return apiActionResult;
                    }
                }
                else
                {
                    result.StatusCode = 501;
                    result.StatusMessage = "Error";
                    result.ErrorMessage = "An error is occured while processing your request.";
                    apiActionResult = new APIActionResult(result);
                }
            }
            catch (Exception ex)
            {
                #region log error
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);
                #endregion

                result.StatusCode = 501;
                result.StatusMessage = "Error";
                result.ErrorMessage = "An error is occured while processing your request.";
                apiActionResult = new APIActionResult(result);
            }

            return apiActionResult;
        }

        [Route("post-order/{UrlName?}")]
        [ServiceFilter(typeof(CustomerApiCallsAuthorization))]
        public async Task<APIActionResult> PostCustomerOrder(string UrlName, [FromBody] Dictionary<string, object> param)
        {
            #region Basic declaration
            //--Api result type declared in resultType variable
            string resultType = "json";

            NoorAppAPIResult result = new NoorAppAPIResult();
            APIActionResult apiActionResult;

            result.ActionType = (ActionTypeEnum)Enum.Parse(typeof(ActionTypeEnum), resultType, true);

            //--This data variable will be used for storing data
            string? data = string.Empty;
            #endregion
            try
            {
                StripeConfiguration.ApiKey = _constants.GetAppSettingKeyValue("AppSetting", "StripeSecretKey");
                // strip implementation url
                // https://stripe.com/docs/payments/accept-a-payment-charges?html-or-react=react

                var paymentToken = "";
                int PaymentMethod = 0;

                Dictionary<string, object>? requestParameters = new Dictionary<string, object>();
                if (param != null && param.Count != 0)
                {
                    if (param.ContainsKey("requestParameters"))
                    {
                        string? ParamKeyValue = param["requestParameters"].ToString();
                        if (!String.IsNullOrWhiteSpace(ParamKeyValue))
                        {
                            requestParameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(ParamKeyValue);

                            paymentToken = requestParameters["paymentToken"].ToString(); // Using ASP.NET MVC 
                            PaymentMethod = Convert.ToInt32(requestParameters["PaymentMethod"].ToString());
                        }
                    }
                }
                //--strip testing card urls: https://stripe.com/docs/testing?numbers-or-method-or-token=card-numbers#visa
                #region new
                string CouponCode = requestParameters["CouponCode"].ToString() ?? "";
                string? Description = "Order of customer id: " + requestParameters["UserID"].ToString() + " at " + DateTime.Now.ToString();
                string cartJsonData = "[]";
                decimal? OrderTotal = 0m;
                decimal? ItemSubTotal = 0;

                cartJsonData = requestParameters != null ? requestParameters["cartJsonData"].ToString() ?? "[]" : "[]";
                var cartCustomerProducts = new List<CartCustomerProducts>();
                cartCustomerProducts = JsonConvert.DeserializeObject<List<CartCustomerProducts>>(cartJsonData);
                if (cartCustomerProducts != null)
                {
                    List<ProductsIds>? ProductIds = new List<ProductsIds>();

                    foreach (var item in cartCustomerProducts)
                    {
                        var rowData = new ProductsIds();
                        rowData.ProductId = item.ProductId;
                        ProductIds.Add(rowData);
                    }
                    string ProductIdsJson = JsonConvert.SerializeObject(ProductIds);

                    //-- get products list by ids
                    Dictionary<string, object>? requestParametersAllProducts = new Dictionary<string, object>();
                    requestParametersAllProducts.Add("ProductsIds", ProductIdsJson);
                    var ApiConfigurationForGetAllProducts = await this._apiOperationServicesDAL.GetAPIConfiguration("get-products-list-by-ids");
                    string? allProductsDataJson = "[]";
                    if (ApiConfigurationForGetAllProducts != null)
                    {
                        allProductsDataJson = await this._apiOperationServicesDAL.GetApiData(requestParametersAllProducts, ApiConfigurationForGetAllProducts);
                    }

                    //--Calcualte Discount for products
                    string productsAfterDiscount = await _calculationHelper.CalculateDiscountsForProducts((allProductsDataJson ?? "[]"));
                    var CartItems = JsonConvert.DeserializeObject<List<ApiProductEntity?>>(productsAfterDiscount ?? "[]");
                    List<CustomerFinalOrderItemData> customerFinalOrderItemDataList = new List<CustomerFinalOrderItemData>();

                    if (CartItems != null)
                    {
                        if (CartItems.Any(x => x.DiscountedPrice > 0))
                        {
                            foreach (var item in CartItems)
                            {
                                //--get product attributes by product id
                                var ApiConfigForProductAttributes = await this._apiOperationServicesDAL.GetAPIConfiguration("get-product-all-attributes-by-productId");
                                var requestParametersAllAttributes = new Dictionary<string, object>();
                                requestParametersAllAttributes.Add("ProductID", item?.ProductId ?? 0);
                                string? productAllAttributesJson = await this._apiOperationServicesDAL.GetApiData(requestParametersAllAttributes, ApiConfigForProductAttributes);
                                var _cartProductAllAttributes = JsonConvert.DeserializeObject<List<CartProductAllAttributes?>>(productAllAttributesJson ?? "[]");
                                var _productSelectedAttributes = cartCustomerProducts?.FirstOrDefault(x => x.ProductId == item?.ProductId)?.productSelectedAttributes;

                                item.Price = item.Price;
                                item.Quantity = Convert.ToInt32(cartCustomerProducts?.FirstOrDefault(x => x.ProductId == item.ProductId)?.Quantity);
                                item.ShippingCharges = item.ShippingCharges * item.Quantity;

                                decimal additionalAttributesCharges = 0;
                                if (_productSelectedAttributes != null)
                                {
                                    for (int index = 0; index < _productSelectedAttributes.Count(); index++)
                                    {
                                        var priceData = _cartProductAllAttributes?.Where(x => x.ProductAttributeID == _productSelectedAttributes[index].ProductAttributeID
                                                        && x.PrimaryKeyValue == _productSelectedAttributes[index].PrimaryKeyValue)?.FirstOrDefault();
                                        additionalAttributesCharges = Convert.ToDecimal(additionalAttributesCharges + priceData?.AdditionalPrice);
                                    }
                                }
                                additionalAttributesCharges = additionalAttributesCharges * item.Quantity;

                                if (item.DiscountId > 0 && item.DiscountedPrice != null && item.DiscountedPrice > 0)
                                {
                                    item.DiscountedPrice = item.DiscountedPrice;
                                    item.OrderItemDiscount = item.Price - item.DiscountedPrice;
                                    item.OrderItemDiscount = item.OrderItemDiscount * item.Quantity;
                                }

                                item.ItemSubTotal = Convert.ToDecimal((item.DiscountedPrice > 0 ? item.DiscountedPrice : item.Price) * (item.Quantity));
                                item.ItemSubTotal = item.ItemSubTotal + (item.ShippingCharges ?? 0);
                                item.ItemSubTotal = item.ItemSubTotal + (additionalAttributesCharges);

                                OrderTotal = Convert.ToDecimal(OrderTotal + (item.ItemSubTotal));
                                //--Set All Selected attributes data for this row
                                item.ProductAllSelectedAttributes = new List<CartProductAllAttributes>();
                                if (_productSelectedAttributes != null)
                                {
                                    foreach (var attr in _productSelectedAttributes)
                                    {
                                        var fullDataAttribue = _cartProductAllAttributes.Where(x => x.ProductAttributeID == attr.ProductAttributeID && x.PrimaryKeyValue == attr.PrimaryKeyValue).FirstOrDefault();
                                        item.ProductAllSelectedAttributes.Add(fullDataAttribue);
                                    }
                                }
                                //--Fill final order item
                                CustomerFinalOrderItemData customerFinalOrderItemData = new CustomerFinalOrderItemData()
                                {
                                    ProductId = item.ProductId,
                                    Quantity = item.Quantity,
                                    Price = item.Price,
                                    ItemPriceTotal = item.Price * item.Quantity,
                                    ItemSubTotal = item.ItemSubTotal ?? 0,
                                    IsShippingFree = item.IsShippingFree ?? true,
                                    ShippingChargesTotal = item.ShippingCharges ?? 0,
                                    OrderItemAttributeChargesTotal = additionalAttributesCharges,
                                    DiscountId = item.DiscountId ?? 0,
                                    DiscountedPrice = item.DiscountedPrice ?? 0,
                                    OrderItemDiscountTotal = item.OrderItemDiscount ?? 0,
                                    IsDiscountCalculated = item.IsDiscountCalculated ?? false,
                                    CouponCode = item.CouponCode ?? "",
                                    ProductAllSelectedAttributes = item.ProductAllSelectedAttributes
                                };

                                customerFinalOrderItemDataList.Add(customerFinalOrderItemData);
                            }
                        }
                        else if (!String.IsNullOrWhiteSpace(CouponCode))
                        {
                            bool IsDiscountExecuted = false;
                            foreach (var item in CartItems)
                            {
                                //--get product attributes by product id
                                var ApiConfigForProductAttributes = await this._apiOperationServicesDAL.GetAPIConfiguration("get-product-all-attributes-by-productId");
                                var requestParametersAllAttributes = new Dictionary<string, object>();
                                requestParametersAllAttributes.Add("ProductID", item?.ProductId ?? 0);
                                string? productAllAttributesJson = await this._apiOperationServicesDAL.GetApiData(requestParametersAllAttributes, ApiConfigForProductAttributes);

                                var _cartProductAllAttributes = JsonConvert.DeserializeObject<List<CartProductAllAttributes?>>(productAllAttributesJson ?? "[]");
                                var _productSelectedAttributes = cartCustomerProducts?.FirstOrDefault(x => x.ProductId == item?.ProductId)?.productSelectedAttributes;

                                item.Price = item.Price;
                                item.Quantity = Convert.ToInt32(cartCustomerProducts?.FirstOrDefault(x => x.ProductId == item.ProductId)?.Quantity);
                                item.ShippingCharges = item.ShippingCharges * item.Quantity;

                                decimal additionalAttributesCharges = 0;
                                if (_productSelectedAttributes != null)
                                {
                                    for (int index = 0; index < _productSelectedAttributes.Count(); index++)
                                    {
                                        var priceData = _cartProductAllAttributes?.Where(x => x.ProductAttributeID == _productSelectedAttributes[index].ProductAttributeID
                                                        && x.PrimaryKeyValue == _productSelectedAttributes[index].PrimaryKeyValue)?.FirstOrDefault();
                                        additionalAttributesCharges = Convert.ToDecimal(additionalAttributesCharges + priceData?.AdditionalPrice);
                                    }
                                }
                                additionalAttributesCharges = additionalAttributesCharges * item.Quantity;
                                //--Set All Selected attributes data for this row
                                item.ProductAllSelectedAttributes = new List<CartProductAllAttributes>();
                                if (_productSelectedAttributes != null)
                                {
                                    foreach (var attr in _productSelectedAttributes)
                                    {
                                        var fullDataAttribue = _cartProductAllAttributes.Where(x => x.ProductAttributeID == attr.ProductAttributeID && x.PrimaryKeyValue == attr.PrimaryKeyValue).FirstOrDefault();
                                        item.ProductAllSelectedAttributes.Add(fullDataAttribue);
                                    }
                                }

                                //--Fill final order item
                                CustomerFinalOrderItemData customerFinalOrderItemData = new CustomerFinalOrderItemData()
                                {
                                    ProductId = item.ProductId,
                                    Quantity = item.Quantity,
                                    Price = item.Price,
                                    ItemPriceTotal = item.Price * item.Quantity,
                                    IsShippingFree = item.IsShippingFree ?? true,
                                    ShippingChargesTotal = item.ShippingCharges ?? 0,
                                    OrderItemAttributeChargesTotal = additionalAttributesCharges,
                                    CouponCode = item.CouponCode ?? "",
                                    ProductAllSelectedAttributes = item.ProductAllSelectedAttributes
                                };

                                //--If discount is applied from the coupon to a product then do not execute again for each product
                                if (item.IsDiscountAllowed == true && IsDiscountExecuted == false)
                                {
                                    var couponDiscount = await _calculationHelper.CalculateCouponDiscountValueForProduct(item.ProductId, item.Price, CouponCode, cartJsonData);
                                    if (couponDiscount != null)
                                    {
                                        decimal DiscountValueAfterCouponApplied = Convert.ToDecimal(couponDiscount["DiscountValueAfterCouponApplied"].ToString());
                                        customerFinalOrderItemData.DiscountId = Convert.ToInt32(couponDiscount["DiscountId"].ToString());
                                        customerFinalOrderItemData.CouponCode = CouponCode;
                                        item.OrderItemDiscount = DiscountValueAfterCouponApplied;
                                        item.DiscountedPrice = (item.Price - (DiscountValueAfterCouponApplied < item.Price ? DiscountValueAfterCouponApplied : 0));
                                        customerFinalOrderItemData.DiscountedPrice = item.DiscountedPrice ?? 0;
                                        customerFinalOrderItemData.IsDiscountCalculated = true;

                                        if (Convert.ToInt32(couponDiscount["DiscountTypeId"].ToString()) == (short)DiscountTypesEnum.AppliedOnOrderTotal)
                                        {
                                            customerFinalOrderItemData.OrderItemDiscountTotal = ((item.OrderItemDiscount ?? 0));
                                        }
                                        else
                                        {
                                            customerFinalOrderItemData.OrderItemDiscountTotal = ((item.OrderItemDiscount ?? 0) * item.Quantity);
                                        }

                                        //--set the flag to true
                                        IsDiscountExecuted = DiscountValueAfterCouponApplied > 0 ? true : false;
                                    }
                                }

                                item.ItemSubTotal = Convert.ToDecimal((item.DiscountedPrice > 0 ? item.DiscountedPrice : item.Price) * (item.Quantity));
                                item.ItemSubTotal = item.ItemSubTotal + (item.ShippingCharges ?? 0);
                                item.ItemSubTotal = item.ItemSubTotal + (additionalAttributesCharges);

                                customerFinalOrderItemData.ItemSubTotal = Convert.ToDecimal(item.ItemSubTotal);
                                OrderTotal = Convert.ToDecimal(OrderTotal + (item.ItemSubTotal));
                                customerFinalOrderItemDataList.Add(customerFinalOrderItemData);
                            }
                        }

                        else
                        {
                            foreach (var item in CartItems)
                            {
                                //--get product attributes by product id
                                var ApiConfigForProductAttributes = await this._apiOperationServicesDAL.GetAPIConfiguration("get-product-all-attributes-by-productId");
                                var requestParametersAllAttributes = new Dictionary<string, object>();
                                requestParametersAllAttributes.Add("ProductID", item?.ProductId ?? 0);
                                string? productAllAttributesJson = await this._apiOperationServicesDAL.GetApiData(requestParametersAllAttributes, ApiConfigForProductAttributes);

                                var _cartProductAllAttributes = JsonConvert.DeserializeObject<List<CartProductAllAttributes?>>(productAllAttributesJson ?? "[]");
                                var _productSelectedAttributes = cartCustomerProducts?.FirstOrDefault(x => x.ProductId == item?.ProductId)?.productSelectedAttributes;
                                item.Price = item.Price;
                                item.Quantity = Convert.ToInt32(cartCustomerProducts?.FirstOrDefault(x => x.ProductId == item.ProductId)?.Quantity);
                                decimal additionalAttributesCharges = 0;
                                if (_productSelectedAttributes != null)
                                {
                                    for (int index = 0; index < _productSelectedAttributes.Count(); index++)
                                    {
                                        var priceData = _cartProductAllAttributes?.Where(x => x.ProductAttributeID == _productSelectedAttributes[index].ProductAttributeID
                                                        && x.PrimaryKeyValue == _productSelectedAttributes[index].PrimaryKeyValue)?.FirstOrDefault();
                                        additionalAttributesCharges = Convert.ToDecimal(additionalAttributesCharges + priceData?.AdditionalPrice);
                                    }
                                }
                                additionalAttributesCharges = additionalAttributesCharges * item.Quantity;

                                if (item.DiscountId > 0 && item.DiscountedPrice != null && item.DiscountedPrice > 0)
                                {
                                    item.DiscountedPrice = item.DiscountedPrice;
                                    item.OrderItemDiscount = item.Price - item.DiscountedPrice;
                                    item.OrderItemDiscount = item.OrderItemDiscount * item.Quantity;
                                }

                                item.ShippingCharges = item.ShippingCharges * item.Quantity;


                                item.ItemSubTotal = Convert.ToDecimal((item.DiscountedPrice > 0 ? item.DiscountedPrice : item.Price) * (item.Quantity));
                                item.ItemSubTotal = item.ItemSubTotal + (item.ShippingCharges ?? 0);
                                item.ItemSubTotal = item.ItemSubTotal + (additionalAttributesCharges);
                                OrderTotal = Convert.ToDecimal(OrderTotal + (item.ItemSubTotal));

                                //--Set All Selected attributes data for this row
                                item.ProductAllSelectedAttributes = new List<CartProductAllAttributes>();
                                if (_productSelectedAttributes != null)
                                {
                                    foreach (var attr in _productSelectedAttributes)
                                    {
                                        var fullDataAttribue = _cartProductAllAttributes.Where(x => x.ProductAttributeID == attr.ProductAttributeID && x.PrimaryKeyValue == attr.PrimaryKeyValue).FirstOrDefault();
                                        item.ProductAllSelectedAttributes.Add(fullDataAttribue);
                                    }
                                }

                                //--Fill final order item
                                CustomerFinalOrderItemData customerFinalOrderItemData = new CustomerFinalOrderItemData()
                                {
                                    ProductId = item.ProductId,
                                    Quantity = item.Quantity,
                                    Price = item.Price,
                                    ItemPriceTotal = item.Price * item.Quantity,
                                    ItemSubTotal = item.ItemSubTotal ?? 0,
                                    IsShippingFree = item.IsShippingFree ?? true,
                                    ShippingChargesTotal = item.ShippingCharges ?? 0,
                                    OrderItemAttributeChargesTotal = additionalAttributesCharges,
                                    DiscountId = item.DiscountId ?? 0,
                                    DiscountedPrice = item.DiscountedPrice ?? 0,
                                    OrderItemDiscountTotal = item.OrderItemDiscount ?? 0,
                                    IsDiscountCalculated = item.IsDiscountCalculated ?? false,
                                    CouponCode = item.CouponCode ?? "",
                                    ProductAllSelectedAttributes = item.ProductAllSelectedAttributes
                                };

                                customerFinalOrderItemDataList.Add(customerFinalOrderItemData);
                            }
                        }

                        if (OrderTotal == null || OrderTotal == 0 || OrderTotal < 0)
                        {
                            throw new InvalidOperationException("Invalid order total amount!");
                        }

                        //--Get Api Configuration
                        var ApiConfiguration = await this._apiOperationServicesDAL.GetAPIConfiguration(UrlName);

                        requestParameters.Add("CurrencyCode", CommonConversionHelper.GetDefaultCurrencyCode()?.ToLower() ?? "usd");
                        requestParameters.Add("OrderTotal", OrderTotal);
                        requestParameters["cartJsonData"] = JsonConvert.SerializeObject(customerFinalOrderItemDataList);

                        if (PaymentMethod == (short)PaymentMethodsEnum.Stripe)
                        {
                            if (String.IsNullOrWhiteSpace(paymentToken))
                            {
                                throw new InvalidOperationException("stripe payment token is empty!");
                            }

                            string currency = CommonConversionHelper.GetDefaultCurrencyCode()?.ToLower() ?? "usd";
                            var options = new ChargeCreateOptions
                            {
                                Amount = currency == "usd" ? (long)(OrderTotal * 100) : (long)OrderTotal,
                                Currency = currency,
                                Description = Description,
                                Source = paymentToken,
                            };
                            var service = new ChargeService();
                            var charge = service.Create(options);

                            if (charge.Status == "succeeded")
                            {
                                requestParameters.Add("Description", Description);
                                requestParameters.Add("StripeStatus", charge.Status);
                                requestParameters.Add("StripeResponseJson", charge.StripeResponse.Content);
                                requestParameters.Add("StripeBalanceTransactionId", charge.BalanceTransactionId);
                                requestParameters.Add("StripeChargeId", charge.Id);
                                requestParameters.Add("PayPalResponseJson", string.Empty);

                                //--save the information in data base
                                data = await _orderHelper.SaveCustomerOrderInDbWithRetry(requestParameters, ApiConfiguration);

                                #region result
                                result.Data = data;
                                result.StatusCode = 200;
                                result.StatusMessage = "Ok";
                                result.ErrorMessage = String.Empty;
                                apiActionResult = new APIActionResult(result);
                                #endregion
                            }
                            else
                            {
                                #region result
                                result.Data = "[]";
                                result.StatusCode = 501;
                                result.StatusMessage = "Error";
                                result.ErrorMessage = "An error occured. Please try again";
                                apiActionResult = new APIActionResult(result);
                                #endregion
                            }
                        }
                        else if (PaymentMethod == (short)PaymentMethodsEnum.CashOnDelivery)
                        {
                            requestParameters.Add("Description", Description);
                            requestParameters.Add("StripeStatus", "");
                            requestParameters.Add("StripeResponseJson", "");
                            requestParameters.Add("StripeBalanceTransactionId", "");
                            requestParameters.Add("StripeChargeId", "");
                            requestParameters.Add("PayPalResponseJson", string.Empty);

                            //--save the information in data base
                            data = await _orderHelper.SaveCustomerOrderInDbWithRetry(requestParameters, ApiConfiguration);
                            #region result
                            result.Data = data;
                            result.StatusCode = 200;
                            result.StatusMessage = "Ok";
                            result.ErrorMessage = String.Empty;
                            apiActionResult = new APIActionResult(result);
                            #endregion
                        }
                        else if (PaymentMethod == (short)PaymentMethodsEnum.PayPal)
                        {
                            requestParameters.Add("Description", Description);
                            requestParameters.Add("StripeStatus", "");
                            requestParameters.Add("StripeResponseJson", "");
                            requestParameters.Add("StripeBalanceTransactionId", "");
                            requestParameters.Add("StripeChargeId", "");
                            requestParameters.Add("PayPalResponseJson", requestParameters["payPalOrderConfirmJson"].ToString() ?? "");
                            //--save the information in data base
                            data = await _orderHelper.SaveCustomerOrderInDbWithRetry(requestParameters, ApiConfiguration);
                            #region result
                            result.Data = data;
                            result.StatusCode = 200;
                            result.StatusMessage = "Ok";
                            result.ErrorMessage = String.Empty;
                            apiActionResult = new APIActionResult(result);
                            #endregion
                        }
                        else
                        {
                            #region result
                            result.Data = "[]";
                            result.StatusCode = 501;
                            result.StatusMessage = "Error";
                            result.ErrorMessage = "No payment method specified";
                            apiActionResult = new APIActionResult(result);
                            #endregion
                        }

                        #region Send email to customer if order placed successfully
                        try
                        {
                            if (!string.IsNullOrEmpty(Request.Headers["UserID"]) && result.StatusCode == 200)
                            {
                                int UserID = Convert.ToInt32(Request.Headers["UserID"].ToString());
                                var userData = _basicDataServicesDAL.GetUserDataByUserID(UserID);

                                List<EmailAddressEntity> emailAddresses = new List<EmailAddressEntity>();
                                string content = String.Format("Your order has been placed successfully. Order total amount is: {0} {1}. {2}Please check your order history page for further details. {2}{2} Thanks", (CommonConversionHelper.GetDefaultCurrencyCode()?.ToLower() ?? "USD"), OrderTotal, Environment.NewLine);
                                emailAddresses.Add(new EmailAddressEntity { DisplayName = "User", Address = userData?.EmailAddress });
                                string SiteTitle = _configuration.GetSection("AppSetting").GetSection("WebsiteTitle").Value;
                                var message = new EmailMessage(emailAddresses, "New Order Placed", content, String.Format("{0} , New Order Placed", SiteTitle));
                                _emailSender.SendEmail(message);



                            }
                        }
                        catch (Exception ex)
                        {
                            //-- Do nothing
                            var noThing = ex.Message;
                        }
                        #endregion
                    }
                    else
                    {
                        #region result
                        result.Data = "[]";
                        result.StatusCode = 501;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "No cart itme selected!";
                        apiActionResult = new APIActionResult(result);
                        #endregion
                    }
                }
                else
                {
                    #region result
                    result.Data = "[]";
                    result.StatusCode = 501;
                    result.StatusMessage = "Error";
                    result.ErrorMessage = "No cart itme selected!";
                    apiActionResult = new APIActionResult(result);
                    #endregion
                }

                #endregion
            }
            catch (Exception ex)
            {
                #region log error
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);
                #endregion

                result.StatusCode = 501;
                result.StatusMessage = "Error";
                result.ErrorMessage = "An error is occured while processing your request.";
                apiActionResult = new APIActionResult(result);
            }

            return apiActionResult;
        }

        [Route("get-strp-pub-key/{UrlName?}")]
        [ServiceFilter(typeof(CustomerApiCallsAuthorization))]
        public async Task<APIActionResult> GetStripePublishableKey(string? UrlName, [FromBody] Dictionary<string, object> param)
        {
            #region Basic declaration
            //--Api result type declared in resultType variable
            string resultType = "json";

            NoorAppAPIResult result = new NoorAppAPIResult();
            APIActionResult apiActionResult;

            result.ActionType = (ActionTypeEnum)Enum.Parse(typeof(ActionTypeEnum), resultType, true);

            //--This data variable will be used for storing data
            string? data = string.Empty;
            #endregion
            try
            {
                //-- 4. return user success and lets user enter otp that he recieve in email and password and confirm password
                #region result
                string StripePublishableKey = _constants.GetAppSettingKeyValue("AppSetting", "StripePublishableKey");
                Dictionary<string, string> StripeDic = new Dictionary<string, string>();
                StripeDic.Add("strpK", "StripePublishableKey");

                result.Data = JsonConvert.SerializeObject(StripeDic);
                result.StatusCode = 200;
                result.StatusMessage = "Ok";
                result.Message = "Sent Successfully";
                result.ErrorMessage = String.Empty;
                apiActionResult = new APIActionResult(result);

                #endregion
            }
            catch (Exception ex)
            {
                #region log error
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);
                #endregion

                result.StatusCode = 501;
                result.StatusMessage = "Error";
                result.ErrorMessage = "An error is occured while processing your request.";
                apiActionResult = new APIActionResult(result);
            }
            return apiActionResult;
        }

        [Route("get-customer-cart-items/{UrlName?}")]
        [ServiceFilter(typeof(CustomerApiCallsAuthorization))]
        public async Task<APIActionResult> GetCustomerLatestCartItems(string UrlName, [FromBody] Dictionary<string, object> param)
        {
            #region Basic declaration
            //--Api result type declared in resultType variable
            string resultType = "json";

            NoorAppAPIResult result = new NoorAppAPIResult();
            APIActionResult apiActionResult;

            result.ActionType = (ActionTypeEnum)Enum.Parse(typeof(ActionTypeEnum), resultType, true);

            //--This data variable will be used for storing data
            string? data = string.Empty;
            #endregion
            try
            {
                Dictionary<string, object>? requestParameters = new Dictionary<string, object>();
                if (param != null && param.Count != 0)
                {
                    if (param.ContainsKey("requestParameters"))
                    {
                        string? ParamKeyValue = param["requestParameters"].ToString();
                        if (!String.IsNullOrWhiteSpace(ParamKeyValue))
                        {
                            requestParameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(ParamKeyValue);
                        }
                    }
                }
                #region calcualtion
                //-- get customer cart data
                string cartJsonData = "[]";
                cartJsonData = requestParameters != null ? requestParameters["cartJsonData"].ToString() ?? "[]" : "[]";
                var apiResponse = await _calculationHelper.CalcualteProductsTotalAndAdditionalPrices(cartJsonData);
                #endregion
                #region result
                result.Data = JsonConvert.SerializeObject(apiResponse);
                result.StatusCode = 200;
                result.StatusMessage = "Ok";
                result.ErrorMessage = String.Empty;
                apiActionResult = new APIActionResult(result);
                #endregion
            }
            catch (Exception ex)
            {
                #region log error
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);
                #endregion

                result.StatusCode = 501;
                result.StatusMessage = "Error";
                result.ErrorMessage = "An error is occured while processing your request.";
                apiActionResult = new APIActionResult(result);
            }

            return apiActionResult;
        }

        [Route("get-coupon-code-discount-value/{UrlName?}")]
        [ServiceFilter(typeof(CustomerApiCallsAuthorization))]
        public async Task<APIActionResult> GetCouponCodeDiscountedValue(string UrlName, [FromBody] Dictionary<string, object> param)
        {
            #region Basic declaration
            //--Api result type declared in resultType variable
            string resultType = "json";

            NoorAppAPIResult result = new NoorAppAPIResult();
            APIActionResult apiActionResult;

            result.ActionType = (ActionTypeEnum)Enum.Parse(typeof(ActionTypeEnum), resultType, true);

            //--This data variable will be used for storing data
            string? data = string.Empty;
            #endregion
            try
            {
                Dictionary<string, object>? requestParameters = new Dictionary<string, object>();
                if (param != null && param.Count != 0)
                {
                    if (param.ContainsKey("requestParameters"))
                    {
                        string? ParamKeyValue = param["requestParameters"].ToString();
                        if (!String.IsNullOrWhiteSpace(ParamKeyValue))
                        {
                            requestParameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(ParamKeyValue);
                        }
                    }
                }

                #region new
                string CouponCode = requestParameters["CouponCode"].ToString() ?? "";
                string cartJsonData = "[]";
                cartJsonData = requestParameters != null ? requestParameters["cartJsonData"].ToString() ?? "[]" : "[]";

                var cartCustomerProducts = new List<CartCustomerProducts>();
                cartCustomerProducts = JsonConvert.DeserializeObject<List<CartCustomerProducts>>(cartJsonData);
                if (cartCustomerProducts != null)
                {
                    List<ProductsIds>? ProductIds = new List<ProductsIds>();

                    foreach (var item in cartCustomerProducts)
                    {
                        var rowData = new ProductsIds();
                        rowData.ProductId = item.ProductId;
                        ProductIds.Add(rowData);
                    }

                    string ProductIdsJson = JsonConvert.SerializeObject(ProductIds);

                    //-- get products list by ids
                    Dictionary<string, object>? requestParametersAllProducts = new Dictionary<string, object>();
                    requestParametersAllProducts.Add("ProductsIds", ProductIdsJson);
                    var ApiConfigurationForGetAllProducts = await this._apiOperationServicesDAL.GetAPIConfiguration("get-products-list-by-ids");
                    string? allProductsDataJson = "[]";
                    if (ApiConfigurationForGetAllProducts != null)
                    {
                        allProductsDataJson = await this._apiOperationServicesDAL.GetApiData(requestParametersAllProducts, ApiConfigurationForGetAllProducts);
                    }

                    //--Calcualte Discount for products
                    string productsAfterDiscount = await _calculationHelper.CalculateDiscountsForProducts((allProductsDataJson ?? "[]"));
                    var CartItems = JsonConvert.DeserializeObject<List<ApiProductEntity?>>(productsAfterDiscount ?? "[]");

                    if (CartItems != null)
                    {
                        bool IsDiscountExecuted = false;
                        Dictionary<string, object> apiResponse = new Dictionary<string, object>();
                        foreach (var item in CartItems)
                        {
                            //--If discount is applied from the coupon to a product then do not execute again for each product
                            if (item.IsDiscountAllowed == true && IsDiscountExecuted == false)
                            {
                                var couponDiscount = await _calculationHelper.CalculateCouponDiscountValueForProduct(item.ProductId, item.Price, CouponCode, cartJsonData);
                                if (couponDiscount != null)
                                {
                                    item.Quantity = Convert.ToInt32(cartCustomerProducts?.FirstOrDefault(x => x.ProductId == item.ProductId)?.Quantity);
                                    decimal DiscountValueAfterCouponApplied = Convert.ToDecimal(couponDiscount["DiscountValueAfterCouponApplied"].ToString());
                                    apiResponse.Add("DiscountValueAfterCouponApplied", DiscountValueAfterCouponApplied);
                                    apiResponse.Add("DiscountId", Convert.ToInt32(couponDiscount["DiscountId"].ToString()));


                                    if (Convert.ToInt32(couponDiscount["DiscountTypeId"].ToString()) == (short)DiscountTypesEnum.AppliedOnOrderTotal)
                                    {
                                        apiResponse.Add("DiscountValueAfterCouponAppliedWithQuantity", (DiscountValueAfterCouponApplied));
                                    }
                                    else
                                    {
                                        apiResponse.Add("DiscountValueAfterCouponAppliedWithQuantity", (DiscountValueAfterCouponApplied * item.Quantity));
                                    }

                                    //--set the flag to true
                                    IsDiscountExecuted = DiscountValueAfterCouponApplied > 0 ? true : false;
                                }
                            }



                        }

                        #region result
                        result.Data = JsonConvert.SerializeObject(apiResponse);
                        result.StatusCode = 200;
                        result.StatusMessage = "Ok";
                        result.ErrorMessage = String.Empty;
                        apiActionResult = new APIActionResult(result);
                        #endregion

                    }
                    else
                    {
                        #region result
                        result.Data = "[]";
                        result.StatusCode = 501;
                        result.StatusMessage = "Error";
                        result.ErrorMessage = "No cart itme selected!";
                        apiActionResult = new APIActionResult(result);
                        #endregion

                    }
                }
                else
                {
                    #region result
                    result.Data = "[]";
                    result.StatusCode = 501;
                    result.StatusMessage = "Error";
                    result.ErrorMessage = "No cart itme selected!";
                    apiActionResult = new APIActionResult(result);
                    #endregion
                }

                #endregion
            }
            catch (Exception ex)
            {
                #region log error
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);
                #endregion

                result.StatusCode = 501;
                result.StatusMessage = "Error";
                result.ErrorMessage = "An error is occured while processing your request.";
                apiActionResult = new APIActionResult(result);
            }

            return apiActionResult;
        }

        [Route("localization-cstm-portal/{UrlName?}")]
        [ServiceFilter(typeof(CustomerApiCallsAuthorization))]
        public async Task<APIActionResult> GetLocalizationControlsJsonDataCstmPortal(string UrlName, [FromBody] Dictionary<string, object> param)
        {
            #region Basic declaration
            //--Api result type declared in resultType variable
            string resultType = "json";

            NoorAppAPIResult result = new NoorAppAPIResult();
            APIActionResult apiActionResult;

            result.ActionType = (ActionTypeEnum)Enum.Parse(typeof(ActionTypeEnum), resultType, true);

            //--This data variable will be used for storing data
            string? data = string.Empty;
            #endregion

            try
            {
                Dictionary<string, object>? requestParameters = new Dictionary<string, object>();
                if (param != null && param.Count != 0)
                {
                    if (param.ContainsKey("requestParameters"))
                    {
                        string? ParamKeyValue = param["requestParameters"].ToString();
                        if (!String.IsNullOrWhiteSpace(ParamKeyValue))
                        {
                            requestParameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(ParamKeyValue);
                        }
                    }
                }

                #region new
                int EntityId = 0;
                string langCode = string.Empty;
                if (requestParameters != null)
                {
                    EntityId = Convert.ToInt32(requestParameters["entityId"].ToString() ?? "0");
                    langCode = requestParameters["languageCode"].ToString() ?? "en";
                }

                ScrnsLocalizationEntity scrnsLocalization = new ScrnsLocalizationEntity()
                {
                    ScreenId = EntityId,
                    AppModuleId = (short)AppModulesEnum.CustomerPortal,
                    LanguageId = CommonConversionHelper.GetLanguageIdbyLanguageCode(langCode)
                };
                var resultLocalization = await _commonServicesDAL.GetScreenLocalizationJsonDataDAL(scrnsLocalization);


                if (resultLocalization != null && !String.IsNullOrWhiteSpace(resultLocalization.LabelsJsonData))
                {
                    #region result
                    result.Data = resultLocalization.LabelsJsonData;
                    result.StatusCode = 200;
                    result.StatusMessage = "Ok";
                    result.ErrorMessage = String.Empty;
                    apiActionResult = new APIActionResult(result);
                    #endregion
                }
                else
                {

                    #region result
                    result.Data = "[]";
                    result.StatusCode = 501;
                    result.StatusMessage = "Error";
                    result.ErrorMessage = "No cart itme selected!";
                    apiActionResult = new APIActionResult(result);
                    #endregion

                }

                #endregion
            }
            catch (Exception ex)
            {
                #region log error
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);
                #endregion

                result.StatusCode = 501;
                result.StatusMessage = "Error";
                result.ErrorMessage = "An error is occured while processing your request.";
                apiActionResult = new APIActionResult(result);
            }

            return apiActionResult;
        }

        [Route("en-ur-drow-pass-rndom/{UrlName?}")]//--For security reason, just keep url not readable
        [ServiceFilter(typeof(CustomerApiCallsAuthorization))]
        public async Task<APIActionResult> EncryptPassword(string UrlName, [FromBody] Dictionary<string, object> param)
        {
            #region Basic declaration
            //--Api result type declared in resultType variable
            string resultType = "json";

            NoorAppAPIResult result = new NoorAppAPIResult();
            APIActionResult apiActionResult;

            result.ActionType = (ActionTypeEnum)Enum.Parse(typeof(ActionTypeEnum), resultType, true);

            //--This data variable will be used for storing data
            string? data = string.Empty;
            #endregion

            try
            {
                Dictionary<string, object>? requestParameters = new Dictionary<string, object>();
                if (param != null && param.Count != 0)
                {
                    if (param.ContainsKey("requestParameters"))
                    {
                        string? ParamKeyValue = param["requestParameters"].ToString();
                        if (!String.IsNullOrWhiteSpace(ParamKeyValue))
                        {
                            requestParameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(ParamKeyValue);
                        }
                    }
                }

                #region new
                string Password = String.Empty;
                if (requestParameters != null && requestParameters.ContainsKey("Password"))
                {
                    Password = requestParameters["Password"].ToString() ?? "";
                    Password = CommonConversionHelper.Encrypt(Password);
                }

                Dictionary<string, string> responseDic = new Dictionary<string, string>();
                responseDic.Add("Password", Password);

                #region result
                result.Data = JsonConvert.SerializeObject(responseDic);
                result.StatusCode = 200;
                result.StatusMessage = "Ok";
                result.ErrorMessage = String.Empty;
                apiActionResult = new APIActionResult(result);
                #endregion


                #endregion
            }
            catch (Exception ex)
            {
                #region log error
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);
                #endregion

                result.StatusCode = 501;
                result.StatusMessage = "Error";
                result.ErrorMessage = "An error is occured while processing your request.";
                apiActionResult = new APIActionResult(result);
            }

            return apiActionResult;
        }

        [Route("download-digital-file/{order_item_id}/{user_id}")]
        [ServiceFilter(typeof(CustomerApiCallsAuthorization))]
        [HttpGet]
        public async Task<IActionResult> DownloadDigitalFile(int order_item_id, int user_id)
        {
            try
            {
                var digitalOrderInfo = await this._salesServicesDAL.GetDigitalOrderInfoForCustomerByIdDAL(order_item_id, user_id);
                if (digitalOrderInfo != null && digitalOrderInfo.IsDigitalProduct == true && !String.IsNullOrWhiteSpace(digitalOrderInfo.DigitalFileDownloadUrl))
                {
                    if (digitalOrderInfo.DigitalFileDownloadUrl.StartsWith("https://") || digitalOrderInfo.DigitalFileDownloadUrl.StartsWith("http://"))
                    {
                        string path = digitalOrderInfo.DigitalFileDownloadUrl;
                        string fileName = Path.GetFileName(path);
                        string fileExtension = Path.GetExtension(path);
                        string contentType = await this._filesHelpers.GetFileContentTypeForFileExtension(fileExtension);

                        byte[]? fileBytes = null;
                        using (var httpClient = new HttpClient())
                        {
                            var response = await httpClient.GetAsync(path);
                            if (response.IsSuccessStatusCode)
                            {
                                fileBytes = await response.Content.ReadAsByteArrayAsync();
                            }
                            else
                            {
                                throw new Exception("Failed to download file.");
                            }
                        }

                        var fileStream = new MemoryStream(fileBytes);
                        return File(fileStream, contentType ?? "application/octet-stream", fileName);
                    }
                    else
                    {
                        string path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + digitalOrderInfo.DigitalFileDownloadUrl);
                        string fileName = Path.GetFileName(path);
                        string fileExtension = Path.GetExtension(path);
                        var file = System.IO.File.ReadAllBytes(path);
                        var fileStream = new MemoryStream(file);

                        string contentType = await this._filesHelpers.GetFileContentTypeForFileExtension(fileExtension);
                        return File(fileStream, contentType ?? "application/octet-stream", fileName);
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception ex)
            {
                #region log error
                await this._commonServicesDAL.LogRunTimeExceptionDAL(ex.Message, ex.StackTrace, ex.StackTrace);
                #endregion
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
