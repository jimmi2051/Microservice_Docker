using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroService_QLBanDienThoai;
using MicroService_QLBanDienThoai.Models;
using MicroService_QLBanDienThoai.BUS;
using Models.BUS;
using Microsoft.AspNetCore.Http;

namespace MicroService_QLBanDienThoai.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //HttpContext.Session.SetString("TenDangNhap", "ChosKhoa");
            ProductBUS bus = new ProductBUS();
            List<Product> listsp = bus.GetProduct();
            ViewBag.listsp = listsp;
            return View();
        }
        public IActionResult CreateAccount(string item_them_AccountID, string item_them_AccountName, string item_them_Password, string item_them_Email, string item_them_PhoneNumber, string item_them_Address, string item_them_AccountType)
        {
            AccountBUS Account = new AccountBUS();
            string thongbao = Account.CreateAccount(item_them_AccountID, item_them_AccountName, item_them_Password, item_them_PhoneNumber, item_them_Email, item_them_Address, item_them_AccountType);
            ViewBag.ThongBao = thongbao;
            return RedirectToAction("Index", "Home", new { thongbao = thongbao });
        }
        public IActionResult About()
        {
            HttpContext.Session.Remove("TenDangNhap");
            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }

        //public IActionResult LogIn()
        //{
        //    HttpContext.Session.SetString("TenDangNhap", "ChosKhoa2");
        //    ProductBUS bus = new ProductBUS();
        //    List<Product> listsp = bus.GetProduct();
        //    ViewBag.listsp = listsp;
        //    return View("Index");
        //}
    }
}