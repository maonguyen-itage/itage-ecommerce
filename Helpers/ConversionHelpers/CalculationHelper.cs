using DAL.Repository.IServices;
using Entities.CommonModels.AccountsModule;
using Entities.DBInheritedModels;
using Entities.DBModels;
using Helpers.ApiHelpers;
using Helpers.CommonHelpers.Enums;
using Helpers.ConversionHelpers.IConversionHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.ConversionHelpers
{
    public class CalculationHelper : ICalculationHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IApiOperationServicesDAL _apiOperationServicesDAL;
        private readonly IProductServicesDAL _productServicesDAL;

        public CalculationHelper(IConfiguration configuration, IApiOperationServicesDAL apiOperationServicesDAL, IProductServicesDAL productServicesDAL)
        {
            _configuration = configuration;
            _apiOperationServicesDAL = apiOperationServicesDAL;
            _productServicesDAL = productServicesDAL;   
        }

        public async Task<string> CalculateDiscountsForProducts(string JsonData)
        {
            string result = "";
            try
            {
                result = JsonData;
                var ApiProductList = new List<ApiProductEntity?>();
                ApiProductList = JsonConvert.DeserializeObject<List<ApiProductEntity?>>(JsonData ?? "[]");
                var ProductIds = String.Join(",", ApiProductList?.Where(p => p?.IsDiscountAllowed == true)?.Select(p => p.ProductId)?.ToArray());

                if (!String.IsNullOrWhiteSpace(ProductIds))
                {
                    //--Get Api Configuration
                    var ApiConfigurationFurtherCalculation = await this._apiOperationServicesDAL.GetAPIConfiguration("get-products-discounts-calculations");
                    var requestParametersFurtherCalculation = new Dictionary<string, object>();
                    requestParametersFurtherCalculation.Add("ProductIds", ProductIds);
                    string dataFurtherCalculation = await this._apiOperationServicesDAL.GetApiData(requestParametersFurtherCalculation, ApiConfigurationFurtherCalculation);

                    var ApiProductListFurtherCalculation = new List<ApiProductEntity?>();
                    ApiProductListFurtherCalculation = JsonConvert.DeserializeObject<List<ApiProductEntity>?>(dataFurtherCalculation ?? "[]");

                    if (ApiProductListFurtherCalculation?.Count() > 0)
                    {
                        foreach (var discountProduct in ApiProductListFurtherCalculation)
                        {
                            if (discountProduct != null)
                            {
                                ApiProductList?.Where(p => p.ProductId == discountProduct.ProductId).Select(prdObject => { prdObject.DiscountId = discountProduct.DiscountId; return prdObject; }).ToList();
                                ApiProductList?.Where(p => p.ProductId == discountProduct.ProductId).Select(prdObject => { prdObject.DiscountedPrice = discountProduct.DiscountedPrice; return prdObject; }).ToList();
                                ApiProductList?.Where(p => p.ProductId == discountProduct.ProductId).Select(prdObject => { prdObject.IsDiscountCalculated = discountProduct.IsDiscountCalculated; return prdObject; }).ToList();
                                ApiProductList?.Where(p => p.ProductId == discountProduct.ProductId).Select(prdObject => { prdObject.CouponCode = discountProduct.CouponCode; return prdObject; }).ToList();
                            }
                        }
                        result = JsonConvert.SerializeObject(ApiProductList);
                    }
                }
                await Task.FromResult(result);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Dictionary<string, object>> CalcualteProductsTotalAndAdditionalPrices(string cartJsonData)
        {
            try
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
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
                    string productsAfterDiscount = await CalculateDiscountsForProducts((allProductsDataJson ?? "[]"));
                    //--Calculate additional price for product attributes
                    var ApiProductList = JsonConvert.DeserializeObject<List<ApiProductEntity?>>(productsAfterDiscount ?? "[]");
                    decimal CartSubTotalDummy = 0;
                    decimal ShippingSubTotalDummuy = 0;
                    decimal OrderTotalDummu = 0;

                    if (ApiProductList != null)
                    {
                        foreach (var item in ApiProductList)
                        {
                            //--get product attributes by product id
                            var ApiConfigForProductAttributes = await this._apiOperationServicesDAL.GetAPIConfiguration("get-product-all-attributes-by-productId");
                            var requestParametersAllAttributes = new Dictionary<string, object>();
                            requestParametersAllAttributes.Add("ProductID", item?.ProductId ?? 0);
                            string? productAllAttributesJson = await this._apiOperationServicesDAL.GetApiData(requestParametersAllAttributes, ApiConfigForProductAttributes);

                            var _cartProductAllAttributes = JsonConvert.DeserializeObject<List<CartProductAllAttributes?>>(productAllAttributesJson ?? "[]");

                            var _productSelectedAttributes = cartCustomerProducts?.FirstOrDefault(x => x.ProductId == item?.ProductId)?.productSelectedAttributes;
                            decimal additionalPrice = 0;
                            if (_productSelectedAttributes != null)
                            {
                                for (int index = 0; index < _productSelectedAttributes.Count(); index++)
                                {
                                    var priceData = _cartProductAllAttributes?.Where(x => x.ProductAttributeID == _productSelectedAttributes[index].ProductAttributeID
                                                    && x.PrimaryKeyValue == _productSelectedAttributes[index].PrimaryKeyValue)?.FirstOrDefault();
                                    additionalPrice = Convert.ToDecimal(additionalPrice + priceData?.AdditionalPrice);
                                }
                            }
                            item.Price = item.Price + additionalPrice;
                            if (item.DiscountId > 0)
                            {
                                item.DiscountedPrice = item.DiscountedPrice + additionalPrice;
                            }
                            item.Quantity = Convert.ToInt32(cartCustomerProducts?.FirstOrDefault(x => x.ProductId == item.ProductId).Quantity); ;
                            item.ItemSubTotal = Convert.ToDecimal((item.DiscountedPrice > 0 ? item.DiscountedPrice : item.Price) * (item.Quantity));

                            CartSubTotalDummy = Convert.ToDecimal(CartSubTotalDummy + item.ItemSubTotal);
                            ShippingSubTotalDummuy = ShippingSubTotalDummuy + ((item.ShippingCharges ?? 0) * item.Quantity);
                            OrderTotalDummu = Convert.ToDecimal(OrderTotalDummu + (item.ItemSubTotal + ((item.ShippingCharges ?? 0) * item.Quantity)));

                            //--Set All Selected attributes data for this row
                            item.ProductAllSelectedAttributes = new List<CartProductAllAttributes>();

                            if (_productSelectedAttributes!=null)
                            {
                                foreach (var attr in _productSelectedAttributes)
                                {
                                    var fullDataAttribue = _cartProductAllAttributes?.Where(x => x.ProductAttributeID == attr.ProductAttributeID).ToList();
                                    if (fullDataAttribue!=null)
                                    {
                                        item.ProductAllSelectedAttributes.AddRange(fullDataAttribue);
                                    }
                                }
                            }
                        }
                    }

                    //--Set CartSubTotal
                    result.Add("cartSubTotal", CartSubTotalDummy);
                    //--Set ShippingSubTotal
                    result.Add("shippingSubTotal", ShippingSubTotalDummuy);
                    //--Set OrderTotal
                    result.Add("orderTotal", OrderTotalDummu);
                    //--Set Products Data
                    result.Add("productsData", ApiProductList ?? new List<ApiProductEntity?>());
                }
                else
                {
                    //--Set CartSubTotal
                    result.Add("cartSubTotal", 0);
                    //--Set ShippingSubTotal
                    result.Add("shippingSubTotal", 0);
                    //--Set OrderTotal
                    result.Add("orderTotal", 0);
                    //--Set Products Data
                    result.Add("productsData", new List<ApiProductEntity>());
                }

                await Task.FromResult(result);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Dictionary<string, object>> CalculateCouponDiscountValueForProduct(int ProductId, decimal ProductPrice, string CouponCode, string cartJsonData)
        {
            try
            {
                Dictionary<string, object>? CouponCodeRequestDic = new Dictionary<string, object>();
                string UrlName = "get-coupon-code-data";
                var CouponCodeApiConfiguration = await _apiOperationServicesDAL.GetAPIConfiguration(UrlName);
                CouponCodeRequestDic.Add("CouponCode", CouponCode);
                CouponCodeRequestDic.Add("cartJsonData", cartJsonData ?? "[]");
                var CopounDataJson = await this._apiOperationServicesDAL.GetApiData(CouponCodeRequestDic, CouponCodeApiConfiguration);
                DiscountEntity? couponObj = JsonConvert.DeserializeObject<List<DiscountEntity?>>(CopounDataJson ?? "{}")?.FirstOrDefault();

                #region return params
                Dictionary<string, object> result = new Dictionary<string, object>();
                int DiscountId = 0;
                decimal DiscountValueAfterCouponApplied = 0;

                #endregion

                if (couponObj != null && couponObj.DiscountValue > 0)
                {
                    if (couponObj.DiscountTypeId == (short)DiscountTypesEnum.AppliedOnProducts)//-- case one
                    {
                        if (couponObj.ProductId == ProductId)
                        {
                            if (couponObj.DiscountValueType == (short)DiscountValueTypeEnum.FixedAmount) //--FixedValue
                            {
                                DiscountValueAfterCouponApplied = couponObj.DiscountValue;
                            }
                            else if (couponObj.DiscountValueType == (short)DiscountValueTypeEnum.PercentageAmount) // --Percentage
                            {
                                DiscountValueAfterCouponApplied = (couponObj.DiscountValue * ProductPrice) / 100;
                            }

                            DiscountId = couponObj.DiscountId;
                        }
                    }
                    else if (couponObj.DiscountTypeId == (short)DiscountTypesEnum.AppliedOCategories)//-- case two
                    {
                        //--Read product categories
                        var productCategories = await this._productServicesDAL.ReadProductCategoriesById(ProductId);
                        if (productCategories != null && productCategories.Any(x => x.CategoryId == couponObj.CategoryID))
                        {
                            if (couponObj.DiscountValueType == (short)DiscountValueTypeEnum.FixedAmount) //--FixedValue
                            {
                                DiscountValueAfterCouponApplied = couponObj.DiscountValue;
                            }
                            else if (couponObj.DiscountValueType == (short)DiscountValueTypeEnum.PercentageAmount) // --Percentage
                            {
                                DiscountValueAfterCouponApplied = (couponObj.DiscountValue * ProductPrice) / 100;
                            }
                            DiscountId = couponObj.DiscountId;
                        }
                    }
                    else if (couponObj.DiscountTypeId == (short)DiscountTypesEnum.AppliedOnOrderTotal)
                    {

                        if (couponObj.DiscountValueType == (short)DiscountValueTypeEnum.FixedAmount) //--FixedValue
                        {
                            DiscountValueAfterCouponApplied = couponObj.DiscountValue;
                        }
                        else if (couponObj.DiscountValueType == (short)DiscountValueTypeEnum.PercentageAmount) // --Percentage
                        {
                            DiscountValueAfterCouponApplied = (couponObj.DiscountValue * ProductPrice) / 100;
                        }

                        DiscountId = couponObj.DiscountId;
                    }
                }

                result.Add("DiscountId", DiscountId);
                result.Add("CouponCode", CouponCode);
                result.Add("DiscountValueAfterCouponApplied", DiscountValueAfterCouponApplied);
                result.Add("DiscountTypeId", couponObj == null ? 0 : couponObj.DiscountTypeId);


                await Task.FromResult(result);
                return result;

            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
