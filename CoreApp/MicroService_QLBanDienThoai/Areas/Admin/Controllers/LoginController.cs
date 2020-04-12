using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService_QLBanDienThoai.BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_QLBanDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            string session = HttpContext.Session.GetString("TenDangNhap");
            if (String.IsNullOrEmpty(session))
            {
                return View();
            }
            return Redirect("/admin/");
        }
        public IActionResult GetLogin(string tendangnhap, string matkhau)
        {
            LoginBUS bus = new LoginBUS();
            bool res = bus.getLogin(tendangnhap, matkhau);
            if (res == true)
            {
                HttpContext.Session.SetString("TenDangNhap", tendangnhap);
                ViewBag.tendangnhap = HttpContext.Session.GetString("TenDangNhap");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Đăng nhập thất bại!");
            }
            return View("Index");

        }
    }
}