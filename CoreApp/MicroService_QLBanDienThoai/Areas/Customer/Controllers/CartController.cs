using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MicroService_QLBanDienThoai.Areas.Customer.Models;
using MicroService_QLBanDienThoai.BUS;
using MicroService_QLBanDienThoai.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.BUS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MicroService_QLBanDienThoai.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {

            BUS.ProductBUS _bus = new BUS.ProductBUS();
            string this_cartname = HttpContext.Session.GetString("CartName");
            if (String.IsNullOrEmpty(this_cartname))
            {
                string cartname = HttpContext.Session.GetString("TenDangNhap");
                if (String.IsNullOrEmpty(cartname))
                {
                    cartname = Guid.NewGuid().ToString().Replace('-', 'x');
                }
                HttpContext.Session.SetString("CartName", cartname);
            }
            string final_cartname = HttpContext.Session.GetString("CartName");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/cart/" + final_cartname).Result;
                string strData = response.Content.ReadAsStringAsync().Result;
                List<Cart> temp = JsonConvert.DeserializeObject<List<Cart>>(strData);
                if (temp.Count == 0)
                    return View();
                List<ViewCart> result = new List<ViewCart>();
                foreach (var item in temp)
                {
                    ViewCart index = new ViewCart();
                    index.Product_Id = _bus.GetSanPham(item.Product_Id.ToString());
                    index.quantity = item.quantity;
                    result.Add(index);
                }
                return View(result);
            }
        }
        public IActionResult RemoveProduct(string id,string quantity)
        {
            string final_cartname = HttpContext.Session.GetString("CartName");
            if (String.IsNullOrEmpty(final_cartname))
            {
                return Json("Lỗi giỏ hàng");
            }
            int pid = int.Parse(id);
            int pquantity = int.Parse(quantity);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var obj = new
                {

                };
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(contentData);
                string _url = "/api/cart/remove/" + final_cartname + "-" + pid + "-" + pquantity;
                HttpResponseMessage response = client.PostAsync(_url, contentData).Result;
                if (response.Content.ReadAsStringAsync().Result == "Error Add")
                {
                    return Json("Xóa thất bại");
                }
                else
                {
                    return Json("Xoá thành công");
                }
            }
        }
        public IActionResult Checkout() {
            string check = HttpContext.Session.GetString("TenDangNhap");
            if (String.IsNullOrEmpty(check))
            {
                ViewBag.Login = "Vui lòng đăng nhập để thanh toán";
                return RedirectToAction("Index");
            }
            BUS.ProductBUS _bus = new BUS.ProductBUS();
            string this_cartname = HttpContext.Session.GetString("CartName");
            if (String.IsNullOrEmpty(this_cartname))
            {
                string cartname = HttpContext.Session.GetString("TenDangNhap");
                if (String.IsNullOrEmpty(cartname))
                {
                    cartname = Guid.NewGuid().ToString().Replace('-', 'x');
                }
                HttpContext.Session.SetString("CartName", cartname);
            }
            string final_cartname = HttpContext.Session.GetString("CartName");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/cart/" + final_cartname).Result;
                string strData = response.Content.ReadAsStringAsync().Result;
                List<Cart> temp = JsonConvert.DeserializeObject<List<Cart>>(strData);
                if (temp.Count == 0)
                    return View();
                List<ViewCart> result = new List<ViewCart>();
                foreach (var item in temp)
                {
                    ViewCart index = new ViewCart();
                    index.Product_Id = _bus.GetSanPham(item.Product_Id.ToString());
                    index.quantity = item.quantity;
                    result.Add(index);
                }
                return View(result);
            }
        }
        public IActionResult ConfirmCheckOut(string address,string phone,string description,string amount)
        {
            string final_cartname = HttpContext.Session.GetString("CartName");
            float _amount = float.Parse(amount);
            AccountBUS _bus = new AccountBUS();
            ProductBUS productBUS = new ProductBUS();
            string a_name = HttpContext.Session.GetString("TenDangNhap");
            Account _account = _bus.GetAccountByName(a_name);
            List<Cart> _cart = new List<Cart>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/cart/" + final_cartname).Result;
                string strData = response.Content.ReadAsStringAsync().Result;
                List<Cart> temp = JsonConvert.DeserializeObject<List<Cart>>(strData);
                foreach (var item in temp)
                {
                    _cart.Add(item);
                    Product toUpdate = productBUS.GetSanPham(item.Product_Id.ToString());
                    toUpdate.Quantity -= item.quantity;
                    productBUS.UpdateProduct(toUpdate);
                }
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var obj = new
                {
                    AccountID = _account.AccountId,
                    Amount = _amount,
                    Address = address,
                    Phone = phone,
                    Description = description,
                    OrderDate = DateTime.Now,
                    RequireDate = DateTime.Now.AddDays(7),
                    Status =1,
                    IsPaid=1,
                };
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(contentData);
                HttpResponseMessage response = client.PostAsync("/api/cart/checkout/"+final_cartname, contentData).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                if (result == "\"Thanh toan thanh cong\"")
                {
                    return Json("Thanh toán thành công");
                }
                else
                {
                    foreach (var item in _cart)
                    {
                        Product toUpdate = productBUS.GetSanPham(item.Product_Id.ToString());
                        toUpdate.Quantity += item.quantity;
                        productBUS.UpdateProduct(toUpdate);
                    }
                    return Json("Thanh toán thất bại");
                }
            }
        }
    }
}