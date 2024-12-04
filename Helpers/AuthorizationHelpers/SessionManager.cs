using DAL.Repository.IServices;
using Entities.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Entities.DBInheritedModels;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Helpers.AuthorizationHelpers
{
    public class SessionManager : ISessionManager
    {
        private readonly IBasicDataServicesDAL _basicData;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contx;
        private readonly ICommonServicesDAL _commonServicesDAL;
        public SessionManager(IBasicDataServicesDAL basicData, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ICommonServicesDAL commonServicesDAL)
        {
            _basicData = basicData;
            _configuration = configuration;
            _contx = httpContextAccessor;
            _commonServicesDAL = commonServicesDAL;
        }

        //this method is getting login user info data from session
        public UserEntity? GetLoginUserFromSession()
        {
            UserEntity? user = new UserEntity();
            try
            {
                var Users = _contx != null && _contx.HttpContext != null && _contx.HttpContext.Session != null ? _contx.HttpContext.Session.GetString("User") : null;
                if (!string.IsNullOrEmpty(Users))
                {
                    user = JsonConvert.DeserializeObject<UserEntity>(Users);
                    if (user == null)
                    {
                        //get user data from cookie and set user session
                        string? UserID = "-1";
                        UserID = _contx.HttpContext.Request.Cookies["UserId"] == null ? "-1" : _contx.HttpContext.Request.Cookies["UserId"];

                        user = _basicData.GetUserDataByUserID(Convert.ToInt32(UserID));
                        if (user != null)
                        {
                            if (user.UserId > 0)
                            {
                                SetUserDataInSession(user);
                                SetMenusInSession();
                                SetUserRightsInSession(user.UserId);

                                #region set app configs values
                                AppConfigEntity appConfigEntity = new AppConfigEntity()
                                {
                                    PageNo = 1,
                                    PageSize = 200,
                                    AppConfigKey = "AdminPanelLogo"  //-- You can pass here comma seperated values with out space

                                };
                                SetAdminPanelBasicAppConfigsInSession(appConfigEntity);
                                #endregion
                            }
                        }
                        else
                        {
                            user = null;
                        }
                    }
                }
                else
                {
                    //get user data from cookie and set user session
                    string? UserId = "-1";
                    UserId = _contx.HttpContext.Request.Cookies["UserId"] == null ? "-1" : _contx.HttpContext.Request.Cookies["UserId"];

                    user = _basicData.GetUserDataByUserID(Convert.ToInt32(UserId));
                    if (user != null)
                    {
                        if (user.UserId > 0)
                        {
                            SetUserDataInSession(user);
                            SetMenusInSession();
                            SetUserRightsInSession(user.UserId);

                            #region set app configs values
                            AppConfigEntity appConfigEntity = new AppConfigEntity()
                            {
                                PageNo = 1,
                                PageSize = 200,
                                AppConfigKey = "AdminPanelLogo"  //-- You can pass here comma seperated values with out space

                            };
                            SetAdminPanelBasicAppConfigsInSession(appConfigEntity);
                            #endregion
                        }
                    }
                    else
                    {
                        user = null;
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                user = null;
            }
            return user;
        }

        public AppConfigEntity? GetAdminPanelBasicAppConfigsFromSession(string AppConfigKey)
        {
            AppConfigEntity? AppConfigEntity = new AppConfigEntity();

            var appConfigJsonData = _contx != null && _contx.HttpContext != null ? _contx.HttpContext.Session.GetString("AppConfigsListSession") : null;

            if (!String.IsNullOrEmpty(appConfigJsonData))
            {
                var AppConfigList = JsonConvert.DeserializeObject<List<AppConfigEntity>>(appConfigJsonData);
                AppConfigEntity = AppConfigList?.FirstOrDefault(x=>x.AppConfigKey == AppConfigKey);
            }
            else
            {
                AppConfigEntity = null;
            }

            return AppConfigEntity;
        }
        public void SetUserDataInSession(UserEntity model)
        {
            //set session
            var user = JsonConvert.SerializeObject(model);
            _contx.HttpContext.Session.SetString("User", user);
            _contx.HttpContext.Session.SetString("UserId", model.UserId < 1 ? "" : model.UserId.ToString());
            _contx.HttpContext.Session.SetString("UserName", string.IsNullOrWhiteSpace(model.UserName) ? "" : model.UserName);

            //set cookies
            CookieOptions options = new CookieOptions();

            int CookiesTimeInDays = !string.IsNullOrEmpty(_configuration.GetSection("AppSetting").GetSection("CookiesExpiryDurationInDays").Value) ? Convert.ToInt32(_configuration.GetSection("AppSetting").GetSection("CookiesExpiryDurationInDays").Value) : 1;
            options.Expires = DateTime.Now.AddDays(CookiesTimeInDays);
            _contx.HttpContext.Response.Cookies.Append("UserName", string.IsNullOrWhiteSpace(model.UserName) ? "" : model.UserName, options);
            _contx.HttpContext.Response.Cookies.Append("UserId", model.UserId < 1 ? "" : model.UserId.ToString(), options);
        }

        public void SetMenusInSession()
        {
            //get main menus 
            var NavMenuList = _basicData.GetNavMenusList(new MenuNavigation());
            //---Saving menus in sessions
            var MainMenu = JsonConvert.SerializeObject(NavMenuList);
            _contx.HttpContext.Session.SetString("MainMenu", MainMenu);
        }

        public void SetUserRightsInSession(int UserID)
        {
            //get roles rights
            var roleRights = _basicData.GetUserRoleRightsForSession(UserID);
            //---Saving menus in sessions
            var roleRightsJsonData = JsonConvert.SerializeObject(roleRights);
            _contx.HttpContext.Session.SetString("UserRoleRights", roleRightsJsonData);
        }

        public void SetAdminPanelBasicAppConfigsInSession(AppConfigEntity? FormData)
        {
            AppConfigEntity appConfigEntity = new AppConfigEntity()
            {
                PageNo = FormData != null && FormData.PageNo > 0 ? FormData.PageNo : 1,
                PageSize = FormData != null && FormData.PageSize > 0 ? FormData.PageSize : 100,
                AppConfigKey = FormData != null && !String.IsNullOrWhiteSpace(FormData.AppConfigKey) ? FormData.AppConfigKey : ""
            };
         
            var AppConfigsList = _commonServicesDAL.GetAppConfigsValuesDAL(appConfigEntity);

            //-- Save App Configs in Session
            var AppConfigsListJsonData = JsonConvert.SerializeObject(AppConfigsList);
            _contx.HttpContext.Session.SetString("AppConfigsListSession", AppConfigsListJsonData);
        }

        public async Task<int?> GetLoginUserIdFromSession()
        {
            try
            {
                int UserId = Convert.ToInt32(_contx.HttpContext.Session.GetString("UserId"));

                await Task.FromResult(UserId);
                return UserId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<List<RoleRightEntity>?> GetUserRoleRightsFromSession()
        {
            List<RoleRightEntity>? RoleRighstList = new List<RoleRightEntity>();
            try
            {
                //---Get user role rights from session
                var UserRoleRightsString = _contx != null && _contx.HttpContext != null ? _contx.HttpContext.Session.GetString("UserRoleRights") : null;
               
                if (!String.IsNullOrEmpty(UserRoleRightsString))
                {
                    RoleRighstList = JsonConvert.DeserializeObject<List<RoleRightEntity>>(UserRoleRightsString);
                }
                else
                {
                    RoleRighstList = new List<RoleRightEntity>();
                }

                await Task.FromResult(RoleRighstList);
                return RoleRighstList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SetViewSelfRightForLoginUserInSession()
        {
            _contx.HttpContext.Session.SetInt32("IsViewSelfRight",1);   //--1 is for set, 0 is for not set or null
        }
        public async Task<bool> GetViewSelfRightForLoginUserFromSession()
        {
            bool result = false;
            try
            {
                int IsViewSelfRight = Convert.ToInt32(_contx.HttpContext.Session.GetInt32("IsViewSelfRight"));
                result = IsViewSelfRight==1 ? true : false;
              
                await Task.FromResult(result);
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public void SetLanguageCodeInSession(string LangCode)
        {
            try
            {
                LangCode = string.IsNullOrWhiteSpace(LangCode) ? "en" : LangCode;
                _contx.HttpContext.Session.SetString("LangCode", LangCode);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> GetLanguageCodeFromSession()
        {
            string result = string.Empty;
            try
            {
                result = _contx.HttpContext.Session.GetString("LangCode");
                result = String.IsNullOrWhiteSpace(result) ? "en" : result;

                await Task.FromResult(result);
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
    }

    public interface ISessionManager
    {
        UserEntity? GetLoginUserFromSession();
        void SetUserDataInSession(UserEntity model);
        Task<int?> GetLoginUserIdFromSession();
        void SetMenusInSession();
        void SetUserRightsInSession(int UserID);
        Task<List<RoleRightEntity>?> GetUserRoleRightsFromSession();
        void SetViewSelfRightForLoginUserInSession();
        Task<bool> GetViewSelfRightForLoginUserFromSession();
       void SetAdminPanelBasicAppConfigsInSession(AppConfigEntity? FormData);
        AppConfigEntity? GetAdminPanelBasicAppConfigsFromSession(string AppConfigKey);
        void SetLanguageCodeInSession(string LangCode);
        Task<string> GetLanguageCodeFromSession();
    }
}
