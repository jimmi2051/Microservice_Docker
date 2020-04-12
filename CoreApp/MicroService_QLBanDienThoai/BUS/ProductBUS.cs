using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MicroService_QLBanDienThoai.Models;
using Newtonsoft.Json;

namespace MicroService_QLBanDienThoai.BUS
{
    public class ProductBUS
    {
        private readonly QLBanDienThoaiContext context;
        public ProductBUS()
        {
            this.context = new QLBanDienThoaiContext();
        }

        public ProductBUS(QLBanDienThoaiContext context)
        {
            this.context = context;
        }

        public List<Product> GetProduct()
        {
            List<Product> list = context.Product.ToList();
            return list;
        }



        //------------------------------------------------------ THEM SUA XOA -----------------------------------------------------------------
        public string CreateProduct(string ProductID, string Name, string Detail, double Price, int Quantity, string Image, int Is_Visible, int Is_Active)
        {

            Product Product = new Product();

            //----------------------- chuan hoa du lieu ----------------------- 
            //----------------------- kiem tra ma -----------------------
            //Product = context.Product.Where(temp => temp.ProductId == ProductID).SingleOrDefault();
            //if (Product != null)
            //{
            //    return "Mã sản phẩm đã tồn tại";
            //}
            //----------------------- kiem tra ten -----------------------
            //Product = context.Product.Where(temp => temp.ProductName == ProductName).SingleOrDefault();
            //if (Product != null)
            //{
            //    return "Tên Product đã tồn tại";
            //}
            //----------------------- them -----------------------
            Product = new Product();
            //Product.ProductId = ProductID;
            Product.Name = Name;
            Product.Detail = Detail;
            Product.Price = Convert.ToDecimal(Price);
            Product.Quantity = Quantity;
            Product.Image = Image;
            Product.IsVisible = Is_Visible;
            Product.IsActive = Is_Active;

            context.Product.Add(Product);
            context.SaveChanges();
            ////Thanh
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var obj = new
                {
                    Name = Product.Name,
                    Quality = Product.Quantity,
                    PriceBuy = Product.Price,
                    PriceSell = Product.Price,
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
                    Name = Product.Name,
                    Quality = Product.Quantity,
                    PriceBuy = Product.Price,
                    PriceSell = Product.Price,
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
                    Name = Product.Name,
                    Quantity = Product.Quantity,
                    Price = Product.Price,
                };
                string stringData = JsonConvert.SerializeObject(obj);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(contentData);
                HttpResponseMessage response = client.PostAsync("/api/product", contentData).Result;
            }
            return "Thêm thành công";
        }
        public string EditProduct(string ProductID, string Name, string Detail, double Price, int Quantity, string Image, int Is_Visible, int Is_Active)
        {
            Product Product = new Product();

            //----------------------- chuan hoa du lieu ----------------------- 
            //----------------------- kiem tra ma -----------------------
            int tempid = Int32.Parse(ProductID);
            Product = context.Product.Where(temp => temp.ProductId == tempid).SingleOrDefault();

            if (Name != null)
            {
                Product.Name = Name;
            }
            if (Detail != null)
            {
                Product.Detail = Detail;
            }
            if (Convert.ToDecimal(Price) != Product.Price)
            {
                Product.Price = Convert.ToDecimal(Price);
            }
            if (Quantity != Product.Quantity)
            {
                Product.Quantity = Quantity;
                ////Thanh
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8080");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    var obj = new
                    {
                        Id = Product.ProductId,
                        Quality = Product.Quantity,
                    };
                    string stringData = JsonConvert.SerializeObject(obj);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync("/api/product/" + obj.Id, contentData).Result;
                }
                //Update product Api - Dong
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8082");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    var obj = new
                    {
                        ProductID = Product.ProductId,
                        Quantity = Product.Quantity,
                    };
                    string stringData = JsonConvert.SerializeObject(obj);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync("/api/product/" + obj.ProductID, contentData).Result;
                }
                //End update 
            }
            if (Image != null)
            {
                Product.Image = Image;
            }
            if (Is_Visible != Product.IsVisible)
            {
                Product.IsVisible = Is_Visible;
            }
            if (Is_Active != Product.IsActive)
            {
                Product.IsActive = Is_Active;
            }
            context.SaveChanges();
            return "Sửa thành công";
        }

        public string DeleteProduct(string ProductID)
        {
            int tempid = Int32.Parse(ProductID);
            Product Product = context.Product.Where(temp => temp.ProductId == tempid).SingleOrDefault();
            context.Product.Remove(Product);
            context.SaveChanges();
            return "Xoá thành công";
        }

        public List<Product> SearchProduct(string search)
        {
            List<Product> list = new List<Product>();
            if (search == null)
            {
                list = GetProduct();
            }
            else
            {
                list = context.Product.Where(temp=>temp.Name.Contains(search)).ToList();
            }
            return list;
        }

        public Product GetSanPham(string id)
        {
            int tempid = Int32.Parse(id);
            Product sanpham = context.Product.Where(sp => sp.ProductId == tempid && sp.IsActive == 1).SingleOrDefault();
            return sanpham;
        }
        public Product UpdateProduct(Product entity)
        {
            Product target = context.Product.Where(c => c.ProductId == entity.ProductId).FirstOrDefault();
            context.Entry(target).CurrentValues.SetValues(entity);
            context.SaveChanges();
            return entity;
        }
    }
}
