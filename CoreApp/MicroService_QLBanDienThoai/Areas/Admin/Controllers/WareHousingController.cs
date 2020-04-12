using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MicroService_QLBanDienThoai.Areas.Admin.Models;
using MicroService_QLBanDienThoai.Areas.Customer.Models;
using MicroService_QLBanDienThoai.BUS;
using MicroService_QLBanDienThoai.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.BUS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MicroService_QLBanDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WareHousingController : BaseController
    {
        public IActionResult Index()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/receipt_note").Result;
                string strData = response.Content.ReadAsStringAsync().Result;
                List<ViewReceipt> result = JsonConvert.DeserializeObject<List<ViewReceipt>>(strData);
                if (result.Count == 0)
                    return View();
                return View(result);
            }
        }
        public IActionResult Create()
        {
            ProductBUS _product = new ProductBUS();
            ViewBag.Product = _product.GetProduct();
            return View();
        }
        public IActionResult Detail(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/detail_receipt/").Result;
                string strData = response.Content.ReadAsStringAsync().Result;
                List<Detail_Adapter> temp = JsonConvert.DeserializeObject<List<Detail_Adapter>>(strData);
                if (temp.Count == 0)
                    return View();
                decimal amount = 0;
                ProductBUS productBUS = new ProductBUS();
                List<Detail_Adapter> result = new List<Detail_Adapter>();
                foreach (var item in temp)
                {
                    if (item.Receipt_ID == (int.Parse(id)))
                    {
                        amount += productBUS.GetSanPham(item.Product_ID.ToString()).Price * item.Quantity;
                        result.Add(item);
                    }
                }
                ViewBag.Amount = amount;
                return View(result);
            }
        }
        public IActionResult GetProduct(string id)
        {
            
            ProductBUS _bus = new ProductBUS();
            Product temp = _bus.GetSanPham(id);

            var result = new {
                Id=new Guid(),
                ProductId=temp.ProductId,
                Name=temp.Name,
                Image=temp.Image,
                Quantity=temp.Quantity,
                Price=temp.Price,
            };
            return Json(result);
        }
        public IActionResult CreateReceipt(string[] product, string[] quantity, string amount)
        {
            string check = HttpContext.Session.GetString("TenDangNhap");
            if (String.IsNullOrEmpty(check))
            {
                return Redirect("/");
            }
            ProductBUS _product_bus = new ProductBUS();
            List<ViewDetail_receipt> _list = new List<ViewDetail_receipt>();
            for (int i = 0; i < product.Length; i++)
            {
                ViewDetail_receipt toInsert = new ViewDetail_receipt();
                toInsert.Product_ID = int.Parse(product[i]);
                toInsert.Quantity = int.Parse(quantity[i]);
                _list.Add(toInsert);
                Product toUpdate = _product_bus.GetSanPham(product[i]);
                toUpdate.Quantity += int.Parse(quantity[i]);
                _product_bus.UpdateProduct(toUpdate);
            }

            float _amount = float.Parse(amount);
            AccountBUS _bus = new AccountBUS();
            Account _account = _bus.GetAccountByName(check);
            var obj = new
            {
                AccountID = _account.AccountId,
                Amount = _amount,
                State_Receipt = 1,
                Success_At = DateTime.Now,
                Is_Active = "True",
                Archive = "True",
                Detail_Receipts = _list,
            };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(contentData);
                HttpResponseMessage response = client.PostAsync("/api/receipt_note/", contentData).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                if (result == "\"Thanh cong\"")
                {
                    return Json("Thanh toán thành công");
                }
                else
                {
                    for (int i = 0; i < product.Length; i++)
                    {
                        Product toUpdate = _product_bus.GetSanPham(product[i]);
                        toUpdate.Quantity -= int.Parse(quantity[i]);
                        _product_bus.UpdateProduct(toUpdate);
                    }
                    return Json(result);
                }
            }
        }
    }
}