using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                        {
                                { "langCode", filterContext.RouteData.Values[ "langCode" ] },
                                { "controller", "Account" },
                                { "action", "Login" },
                                { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                        });
            }
        }
    }
}