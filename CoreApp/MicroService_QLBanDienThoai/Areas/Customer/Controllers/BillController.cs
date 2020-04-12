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

namespace MicroService_QLBanDienThoai.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BillController : Controller
    {
        public IActionResult Index()
        {
            string check = HttpContext.Session.GetString("TenDangNhap");
            if (String.IsNullOrEmpty(check))
            {
                return Redirect("/");
            }
            AccountBUS _bus = new AccountBUS();
            Account _account = _bus.GetAccountByName(check);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/cart/order/" + _account.AccountId).Result;
                string strData = response.Content.ReadAsStringAsync().Result;
                List<ViewOrder> result = JsonConvert.DeserializeObject<List<ViewOrder>>(strData);
                if (result.Count == 0)
                    return View();
                return View(result);
            }
        }
        public IActionResult Detail(string id)
        {
            int oid = int.Parse(id);
            ViewOrder _order = new ViewOrder();
            string check = HttpContext.Session.GetString("TenDangNhap");
            if (String.IsNullOrEmpty(check))
            {
                return Redirect("/");
            }
            AccountBUS _bus = new AccountBUS();
            Account _account = _bus.GetAccountByName(check);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/cart/order/" + _account.AccountId).Result;
                string strData = response.Content.ReadAsStringAsync().Result;
                List<ViewOrder> temp = JsonConvert.DeserializeObject<List<ViewOrder>>(strData);
                foreach (var item in temp)
                {
                    if (item.Id == oid)
                    {
                        _order.Id = item.Id;
                        _order.Amount = item.Amount;
                        _order.AccountID = item.AccountID;
                        _order.Address = item.Address;
                        _order.OrderDate = item.OrderDate;
                        _order.RequireDate = item.RequireDate;
                        _order.Phone = item.Phone;
                    }
                }
            }
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:8080");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/cart/detail_order/" + oid).Result;
                string strData = response.Content.ReadAsStringAsync().Result;
                List<Detail_Order_Adapter> result = JsonConvert.DeserializeObject<List<Detail_Order_Adapter>>(strData);
                if (result.Count == 0)
                    return View();
                ViewBag.Order = _order;
                return View(result);
            }
        }
        public IActionResult GetProduct(string id)
        {

            ProductBUS _bus = new ProductBUS();
            Product temp = _bus.GetSanPham(id);

            var result = new
            {
                Id = new Guid(),
                ProductId = temp.ProductId,
                Name = temp.Name,
                Image = temp.Image,
                Quantity = temp.Quantity,
                Price = temp.Price,
            };
            return Json(result);
        }
    }
}
