using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Models.BUS;
using MicroService_QLBanDienThoai.BUS;
using MicroService_QLBanDienThoai.Models;
namespace MicroService_QLBanDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SyncAllAccount()
        {
            AccountBUS _bus = new AccountBUS();
            foreach (Account item in _bus.GetAccount())
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8080");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    var obj = new
                    {
                        AccountID = item.AccountId,
                        AccountName = item.AccountName,
                        AccountType = item.AccountType,
                    };
                    string stringData = JsonConvert.SerializeObject(obj);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(contentData);
                    HttpResponseMessage response = client.PostAsync("/api/account", contentData).Result;
                }
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8081");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    var obj = new
                    {
                        AccountID = item.AccountId,
                        AccountName = item.AccountName,
                        AccountType = item.AccountType,
                    };
                    string stringData = JsonConvert.SerializeObject(obj);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(contentData);
                    HttpResponseMessage response = client.PostAsync("/api/account", contentData).Result;
                }
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8082");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    var obj = new
                    {
                        AccountName = item.AccountName,
                        AccountType = item.AccountType,
                    };
                    string stringData = JsonConvert.SerializeObject(obj);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(contentData);
                    HttpResponseMessage response = client.PostAsync("/api/account", contentData).Result;
                }
            }
            return Ok("Success");
        }
        public IActionResult SyncAllProduct() {
            ProductBUS _bus = new ProductBUS();
            foreach (Product item in _bus.GetProduct())
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8080");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    var obj = new
                    {
                        Name = item.Name,
                        Quality = item.Quantity,
                        PriceBuy = item.Price,
                        PriceSell = item.Price,
                    };
                    string stringData = JsonConvert.SerializeObject(obj);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(contentData);
                    HttpResponseMessage response = client.PostAsync("/api/product", contentData).Result;
                }
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8081");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    var obj = new
                    {
                        Name = item.Name,
                        Quality = item.Quantity,
                        PriceBuy = item.Price,
                        PriceSell = item.Price,
                    };
                    string stringData = JsonConvert.SerializeObject(obj);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(contentData);
                    HttpResponseMessage response = client.PostAsync("/api/product", contentData).Result;
                }
                //Dong
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8082");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    var obj = new
                    {
                        Name = item.Name,
                        Quantity = item.Quantity,
                        Price = item.Price,
                    };
                    string stringData = JsonConvert.SerializeObject(obj);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(contentData);
                    HttpResponseMessage response = client.PostAsync("/api/product", contentData).Result;
                }
                
            }
            return Ok("Success");
            
        }
    }
}