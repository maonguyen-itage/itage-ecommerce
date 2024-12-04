using Entities.DBInheritedModels;
using Entities.DBModels;
using Helpers.CommonHelpers.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.AuthorizationHelpers
{
    public class RolesRightsAuthorizationHelper : ActionFilterAttribute
    {
        private readonly int _entityID;
        private readonly int _addRight;
        private readonly int _updateRight;
        private readonly int _deleteRight;
        private readonly int _viewAllRight;
        private readonly int _viewSelfRight;

        public RolesRightsAuthorizationHelper(int EntityID, int AddRight, int UpdateRight, int DeleteRight, int ViewAllRight, int ViewSelfRight)
        {
            _entityID = EntityID;
            _addRight = AddRight;
            _updateRight = UpdateRight;
            _deleteRight = DeleteRight;
            _viewAllRight = ViewAllRight;
            _viewSelfRight = ViewSelfRight;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            try
            {
                bool IsValid = false;
                IConfiguration? _configuration = filterContext.HttpContext.RequestServices.GetService<IConfiguration>();
                ISessionManager? _sessionManager = filterContext.HttpContext.RequestServices.GetService<ISessionManager>();
                List<RoleRightEntity>? UserRolesRights = _sessionManager != null ? await _sessionManager.GetUserRoleRightsFromSession() : new List<RoleRightEntity>();

                //--check if roles rigts enables
                int IsRoleRightsEnables = 0;
                IsRoleRightsEnables = _configuration != null ? Convert.ToInt32(_configuration?.GetSection("AppSetting")?.GetSection("RoleRightsEnables").Value) : 1;

                if (IsRoleRightsEnables == 1)
                {
                    if (_viewAllRight > 0 || _viewSelfRight > 0)
                    {
                        if (_viewAllRight > 0)
                        {
                            IsValid = UserRolesRights != null && UserRolesRights.Count > 0 && UserRolesRights.Any(x => x.EntityId == _entityID && x.RightId == _viewAllRight) ? true : false;
                        }

                        //-- if _viewAllRight is valid then no need to check for individual (self) right
                        if (_viewSelfRight > 0 && IsValid == false)
                        {
                            IsValid = UserRolesRights != null && UserRolesRights.Count > 0 && UserRolesRights.Any(x => x.EntityId == _entityID && x.RightId == _viewSelfRight) ? true : false;
                            if (IsValid)
                            {
                                _sessionManager.SetViewSelfRightForLoginUserInSession();
                            }
                        }
                    }
                    else if (_addRight > 0 && _updateRight > 0)
                    {
                        var addUpdateRights = UserRolesRights?.Where(x => (x.RightId == (short)UserRightsEnum.Add || x.RightId == (short)UserRightsEnum.Update) && (x.EntityId == _entityID)).ToList();

                        if ((addUpdateRights != null && addUpdateRights.Count() > 0))
                        {
                            //--get the parameter of action result method of any conroller that hitted in this call
                            var filterControllerContextJson = JsonConvert.SerializeObject(((Microsoft.AspNetCore.Mvc.ControllerBase)filterContext.Controller).ModelState?.Values);

                            if (!String.IsNullOrWhiteSpace(filterControllerContextJson))
                            {
                                var ControllerModelStatesValues = JsonConvert.DeserializeObject<List<FilterContextControllerModelStateValues>?>(filterControllerContextJson ?? "[]");

                                string? DataOperationType = ControllerModelStatesValues?.FirstOrDefault(x => x.Key == "DataOperationType")?.RawValue;
                                if (DataOperationType != null && Convert.ToInt32(DataOperationType) == (short)CommonHelpers.Enums.DataOperationType.Insert && _addRight > 0)
                                {
                                    IsValid = UserRolesRights != null && UserRolesRights.Count > 0 && UserRolesRights.Any(x => x.EntityId == _entityID && x.RightId == _addRight) ? true : false;
                                }
                                else if (!String.IsNullOrWhiteSpace(DataOperationType)  && Convert.ToInt32(DataOperationType) == (short)CommonHelpers.Enums.DataOperationType.Update && _updateRight > 0)
                                {
                                    IsValid = UserRolesRights != null && UserRolesRights.Count > 0 && UserRolesRights.Any(x => x.EntityId == _entityID && x.RightId == _updateRight) ? true : false;
                                }
                            }
                        }
                    } else if (_addRight > 0)
                    {
                        IsValid = UserRolesRights != null && UserRolesRights.Count > 0 && UserRolesRights.Any(x => x.EntityId == _entityID && x.RightId == _addRight) ? true : false;
                    }
                    else if (_updateRight > 0)
                    {
                        IsValid = UserRolesRights != null && UserRolesRights.Count > 0 && UserRolesRights.Any(x => x.EntityId == _entityID && x.RightId == _updateRight) ? true : false;
                    }

                    else if (_deleteRight > 0)
                    {
                        //--if specified entity id passed then do below step else get entity id from the FilterContextController in the else part
                        if (_entityID > 0)
                        {
                            IsValid = UserRolesRights != null && UserRolesRights.Count > 0 && UserRolesRights.Any(x => x.EntityId == _entityID && x.RightId == _deleteRight) ? true : false;
                        }
                        else
                        {
                            //--get the parameter of action result method of any conroller that hitted in this call
                            var filterControllerContextJson = JsonConvert.SerializeObject(((Microsoft.AspNetCore.Mvc.ControllerBase)filterContext.Controller).ModelState?.Values);

                            if (!String.IsNullOrWhiteSpace(filterControllerContextJson))
                            {
                                var ControllerModelStatesValues = JsonConvert.DeserializeObject<List<FilterContextControllerModelStateValues>?>(filterControllerContextJson ?? "[]");

                                string? EntityIdFromControllerContext = ControllerModelStatesValues?.FirstOrDefault(x => x.Key == "EntityId")?.RawValue;
                                if (!String.IsNullOrWhiteSpace(EntityIdFromControllerContext) && Convert.ToInt32(EntityIdFromControllerContext) > 0)
                                {
                                    IsValid = UserRolesRights != null && UserRolesRights.Count > 0 && UserRolesRights.Any(x => x.EntityId == Convert.ToInt32(EntityIdFromControllerContext) && x.RightId == _deleteRight) ? true : false;
                                }
                            }
                        }
                    }
                    else
                    {
                        IsValid = true;
                    }

                    if (IsValid == false)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Authentication",
                            action = "Login",
                            area = ""
                        }));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await base.OnActionExecutionAsync(filterContext, next);
            }
        }
    }

    public class FilterContextControllerModelStateValues
    {
        public string? Key { get; set; }
        public object? SubKey { get; set; }
        public bool? IsContainerNode { get; set; }
        public string? RawValue { get; set; }
        public string? AttemptedValue { get; set; }
    }
}
