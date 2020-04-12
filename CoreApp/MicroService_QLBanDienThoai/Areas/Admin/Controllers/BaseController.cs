using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MicroService_QLBanDienThoai.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string session = HttpContext.Session.GetString("TenDangNhap");
            
            if (String.IsNullOrEmpty(session))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new Microsoft.AspNetCore.Routing.RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" })
                    );
            }
            base.OnActionExecuting(filterContext);
        }
    }
}