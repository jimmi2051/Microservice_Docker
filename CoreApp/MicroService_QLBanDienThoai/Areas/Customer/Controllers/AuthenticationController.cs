using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroService_QLBanDienThoai.BUS;
using Models.BUS;
using MicroService_QLBanDienThoai.Models;
using Microsoft.AspNetCore.Http;

namespace MicroService_QLBanDienThoai.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AuthenticationController : Controller
    {
        public IActionResult LogIn(string tendangnhap, string matkhau)
        {
            AccountBUS tkbus = new AccountBUS();
            Account taikhoan = tkbus.CheckTaiKhoan(tendangnhap, matkhau, "customer");
            if (taikhoan == null)
            {
                return Json("Thông tin đăng nhập không đúng");
            }
            //if (taikhoan.TinhTrang == "Chưa kích hoạt")
            //{
            //    thongbao = "Tài khoản chưa được kích hoạt, vui lòng kích hoạt thông qua email";
            //    return RedirectToAction("Index", "Home", new { thongbao = thongbao });
            //}
            //if (taikhoan.TinhTrang == "Khoá")
            //{
            //    thongbao = "Tài khoản đã bị khoá, vui lòng liên hệ Webmaster để mở khoá";
            //    return RedirectToAction("Index", "Home", new { thongbao = thongbao });
            //}
            HttpContext.Session.SetString("TenDangNhap", tendangnhap);
            ViewBag.tendangnhap= HttpContext.Session.GetString("TenDangNhap");
            return Json("Đăng nhập thành công");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("TenDangNhap");
            return RedirectToAction("Index", "Home");
        }
    }
}