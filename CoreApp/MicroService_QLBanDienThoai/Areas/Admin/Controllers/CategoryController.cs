using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.BUS;
using MicroService_QLBanDienThoai.Models;
using MicroService_QLBanDienThoai.BUS;

namespace MicroService_QLBanDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        public IActionResult Index(string thongbao)
        {
            //Thông báo
            if (thongbao != null)
            {
                ViewBag.ThongBao = thongbao;
            }
            //List          
            CategoryBUS bus = new CategoryBUS();
            List<Category> list = bus.GetCategory();
            //ViewBag
            ViewBag.TrangThai = "index";
            return View(list);
        }
        [HttpPost]
        public IActionResult CreateCategory(string item_them_CategoryID, string item_them_CategoryName, string item_them_Quantity, string item_them_IsActive, string item_them_Archive)
        {
            int tinhtrang = 0;
            if (item_them_IsActive == "Khoá")
            {
                tinhtrang = 0;
            }
            else tinhtrang = 1;
            CategoryBUS Category = new CategoryBUS();
            string thongbao = Category.CreateCategory(item_them_CategoryID, item_them_CategoryName, 0, tinhtrang, 1);
            return RedirectToAction("Index", "Category", new { thongbao = thongbao });
        }
        public IActionResult EditCategory(string item_sua_CategoryID, string item_sua_CategoryName, string item_sua_Quantity, string item_sua_IsActive, string item_sua_Archive)
        {
            int tinhtrang = 0;
            if (item_sua_IsActive == "Khoá")
            {
                tinhtrang = 0;
            }
            else tinhtrang = 1;
            CategoryBUS Category = new CategoryBUS();
            string thongbao = Category.EditCategory(item_sua_CategoryID, item_sua_CategoryName, 0, tinhtrang, 1);
            return RedirectToAction("Index", "Category", new { thongbao = thongbao });
        }
        public IActionResult DeleteCategory(string CategoryId)
        {
            CategoryBUS Category = new CategoryBUS();
            string thongbao = Category.DeleteCategory(CategoryId);
            return RedirectToAction("Index", "Category", new { thongbao = thongbao });
        }
        public IActionResult Search(string search)
        {
            CategoryBUS bus = new CategoryBUS();
            List<Category> list = bus.SearchCategory(search);
            ViewBag.TrangThai = "search";
            ViewBag.Search = search;
            return View("Index", list);
        }

    }
}