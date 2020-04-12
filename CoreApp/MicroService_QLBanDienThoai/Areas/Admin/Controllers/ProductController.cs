using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.BUS;
using MicroService_QLBanDienThoai.Models;
using MicroService_QLBanDienThoai.BUS;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;

namespace MicroService_QLBanDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : BaseController
    {
        private IHostingEnvironment _hostingEnvironment;
        public ProductController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(string thongbao)
        {
            //Thông báo
            if (thongbao != null)
            {
                ViewBag.ThongBao = thongbao;
            }
            //List          
            ProductBUS bus = new ProductBUS();
            List<Product> list = bus.GetProduct();
            //ViewBag
            ViewBag.TrangThai = "index";
            return View(list);
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        [HttpPost]
        public IActionResult CreateProduct(string item_them_Name, string item_them_Detail, string item_them_Price, string item_them_Quantity, IFormFile item_them_Image, string item_them_IsVisible, string item_them_IsActive)
        {
            ProductBUS Product = new ProductBUS();
            string item_them_ProductID = "";
            //var uniqueFileName = GetUniqueFileName(item_them_Image.FileName);
            //var fileName = Path.GetFileName(item_them_Image.FileName);
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\HinhSanPham", fileName);
            //var filestream = new FileStream(filePath, FileMode.Create);
            //item_them_Image.CopyTo(filestream);
            //string hinh = fileName;
            //filestream.Close();
            try
            {
                var file = Request.Form.Files[0];
                //Ten folder luu Image
                string folderName = "HinhSanPham";
                //Duong dan mac dinh
                string webRootPath = _hostingEnvironment.WebRootPath;
                // Tao duong dan de luu file = duong dan mac dinh + folder/
                string newPath = Path.Combine(webRootPath, folderName);
                // Tao Thu muc moi
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    fullPath = Infastructure.Images.GetNewPathForDupes(fullPath, ref fileName);
                    //Copy file toi duong dan luu file
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    string thongbao = Product.CreateProduct(item_them_ProductID,
                                                 item_them_Name,
                                                 item_them_Detail,
                                                 Double.Parse(item_them_Price),
                                                 Int32.Parse(item_them_Quantity), fileName,
                                                 Int32.Parse(item_them_IsVisible),
                                                 Int32.Parse(item_them_IsActive));
                    return RedirectToAction("Index", "Product", new { thongbao = thongbao });
                }
            }
            catch (System.Exception ex)
            {
               return  RedirectToAction("Index", "Product", new { thongbao = "Lỗi hệ thống" });
            }
            return RedirectToAction("Index", "Product", new { thongbao = "Lỗi hệ thống" });
        }
        public IActionResult EditProduct(string item_sua_ProductID, string item_sua_Name, string item_sua_Detail, string item_sua_Price, string item_sua_Quantity, string item_sua_Image, string item_sua_IsVisible, string item_sua_IsActive)
        {
            ProductBUS Product = new ProductBUS();
            string thongbao = Product.EditProduct(item_sua_ProductID, item_sua_Name, item_sua_Detail, Double.Parse(item_sua_Price), Int32.Parse(item_sua_Quantity), item_sua_Image, Int32.Parse(item_sua_IsVisible), Int32.Parse(item_sua_IsActive));
            return RedirectToAction("Index", "Product", new { thongbao = thongbao });
        }
        public IActionResult DeleteProduct(string ProductId)
        {
            ProductBUS Product = new ProductBUS();
            string thongbao = Product.DeleteProduct(ProductId);
            return RedirectToAction("Index", "Product", new { thongbao = thongbao });
        }
        public IActionResult Search(string search)
        {
            ProductBUS bus = new ProductBUS();
            List<Product> list = bus.SearchProduct(search);
            ViewBag.TrangThai = "search";
            ViewBag.Search = search;
            return View("Index", list);
        }

    }
}