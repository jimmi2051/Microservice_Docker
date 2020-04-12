using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService_QLBanDienThoai.BUS;
using MicroService_QLBanDienThoai.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_QLBanDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {


        public IActionResult Index(string thongbao)
        {
            //Thông báo
            if (thongbao != null)
            {
                ViewBag.ThongBao = thongbao;
            }
            //List          
            ProductCategoryBUS bus = new ProductCategoryBUS();
            List<ProductCategory> list = bus.GetProductCategory();
            //ViewBag
            ViewBag.TrangThai = "index";
            return View(list);
        }
        [HttpPost]
        public IActionResult CreateProductCategory(string item_them_ProductID, string item_them_CategoryID)
        {

            ProductCategoryBUS ProductCategory = new ProductCategoryBUS();
            string thongbao = ProductCategory.CreateProductCategory(item_them_CategoryID, item_them_ProductID);
            return RedirectToAction("Index", "ProductCategory", new { thongbao = thongbao });
        }
        public IActionResult EditProductCategory(string item_sua_ProductID, string item_sua_CategoryID)
        {
            ProductCategoryBUS ProductCategory = new ProductCategoryBUS();
            string thongbao = ProductCategory.EditProductCategory(item_sua_CategoryID, item_sua_ProductID);
            return RedirectToAction("Index", "ProductCategory", new { thongbao = thongbao });
        }

        public IActionResult Search(string search)
        {
            ProductCategoryBUS bus = new ProductCategoryBUS();
            List<ProductCategory> list = bus.SearchProductCategory(search);
            ViewBag.TrangThai = "search";
            ViewBag.Search = search;
            return View("Index", list);
        }
    }
}