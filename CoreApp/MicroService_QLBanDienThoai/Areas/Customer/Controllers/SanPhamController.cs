using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService_QLBanDienThoai.BUS;
using MicroService_QLBanDienThoai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using MicroService_QLBanDienThoai.Areas.Customer.Models;
using Models.BUS;

namespace MicroService_QLBanDienThoai.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class SanPhamController : Controller
    {
        public IActionResult Index(string id)
        {
            string idd = id ?? "001";
            //BUS
            ProductBUS sanphambus = new ProductBUS();
            Product sanpham = sanphambus.GetSanPham(idd);
            int pid = int.Parse(id);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/rating/" + id).Result;
                string strData = response.Content.ReadAsStringAsync().Result;
                List<ViewRating> temp = JsonConvert.DeserializeObject<List<ViewRating>>(strData);
                List<ViewRating> result = new List<ViewRating>();                
                foreach (var item in temp)
                {
                    if (item.StarRating > 0 && item.Status == 1)
                    {
                        result.Add(item);
                    }
                }
                ViewBag.ViewRating = result;
                float aveRat = 0;
                float[,] rating = new float[5, 5];
                foreach (var item in result)
                {
                    aveRat += item.StarRating;
                    if (item.StarRating == 1)
                    {
                        rating[0, 0]++;
                        rating[0, 1] += item.StarRating;
                    }
                    if (item.StarRating == 2)
                    {
                        rating[1, 0]++;
                        rating[1, 1] += item.StarRating;
                    }
                    if (item.StarRating == 3)
                    {
                        rating[2, 0]++;
                        rating[2, 1] += item.StarRating;
                    }
                    if (item.StarRating == 4)
                    {
                        rating[3, 0]++;
                        rating[3, 1] += item.StarRating;
                    }
                    if (item.StarRating == 5)
                    {
                        rating[4, 0]++;
                        rating[4, 1] += item.StarRating;
                    }

                }
                if (result.Count > 0)
                {
                    rating[0, 1] = (rating[0, 0] / result.Count) * 100;
                    rating[1, 1] = (rating[1, 0] / result.Count) * 100;
                    rating[2, 1] = (rating[2, 0] / result.Count) * 100;
                    rating[3, 1] = (rating[3, 0] / result.Count) * 100;
                    rating[4, 1] = (rating[4, 0] / result.Count) * 100;
                    aveRat = (aveRat / result.Count);
                }
                ViewBag.AveRat = aveRat;
                ViewBag.DetailRat = rating;
                return View(sanpham);
            }  
        }
        public IActionResult AddCart(string id, string quantity)
        {
            int pid = int.Parse(id);
            int pquantity = int.Parse(quantity);
            ProductBUS _bus = new ProductBUS();
            Product toCheck = _bus.GetSanPham(id);
            if (toCheck.Quantity < pquantity)
            {
                return Json("-1");
            }
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
                var obj = new
                {
                  
                };
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                 System.Diagnostics.Debug.WriteLine(contentData);
                string _url = "/api/cart/add/" + final_cartname + "-" + pid + "-" + pquantity;
                HttpResponseMessage response = client.PostAsync(_url, contentData).Result;
                if (response.Content.ReadAsStringAsync().Result == "\"Error Add\"")
                {
                    return Json("Them that bai");
                }
                else {
                    return Json("Them Thanh Cong");
                }
            }
        }
        public IActionResult RatingProduct(string ProductID,string StarRating,string Comment)
        {
            string check = HttpContext.Session.GetString("TenDangNhap");
            if (String.IsNullOrEmpty(check))
            {
                return Redirect("/");
            }
            AccountBUS _bus = new AccountBUS();
            Account _account = _bus.GetAccountByName(check);
            var obj = new
            {
                ProductID = int.Parse(ProductID),
                AccountID = _account.AccountId,
                Title = "Rating_" + DateTime.Now.ToShortDateString(),
                StarRating = int.Parse(StarRating),
                Comment = Comment,
                Created=DateTime.Now.ToShortDateString(),
                Status=1,
            };
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(contentData);
                string _url = "/api/rating/AddRating";
                HttpResponseMessage response = client.PostAsync(_url, contentData).Result;
                if (response.Content.ReadAsStringAsync().Result == "\"Error: Rating fail\"")
                {
                    return Json("Them that bai");
                }
                else
                {
                    return Json("Them Thanh Cong");
                }
            }
        }
    }
}