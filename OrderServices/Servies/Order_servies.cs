using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Order_servies.Models;
using Order_servies.Repository;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Order_servies.Servies
{
    public class Servies_Order
    {
        private static IOrderRepository _order;
        private static IOrder_detailRepository _order_detail;
        private static IAccountRepository _account;
        private static IProductRepository _product;
        private static ICartRepository _items;
        public Servies_Order()
             : this(_order, _items, _order_detail, _account, _product)
        { }
        public Servies_Order(IOrderRepository order, ICartRepository items, IOrder_detailRepository order_detail, IAccountRepository account, IProductRepository product)
        {
            _order = order;
            _order_detail = order_detail;
            _account = account;
            _product = product;
            _items = items;
        }
        public IEnumerable<Product> listAll()
        {
            return _product.GetAll();
        }
        public Product GetProduct(int id)
        {
            return _product.GetSingleById(id);
        }
        public IEnumerable<Order> getAllOrder()
        {
            return _order.GetAll();
        }
        public Order GetOrder(int id)
        {
            return _order.GetSingleById(id);
        }
        public IEnumerable<Order> getOrderbyID(int id)
        {
            return _order.GetMulti(c => c.AccountID == id);
        }
        public bool CreateOrder(Order order)
        {
            try
            {
                _order.Add(order);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public Cart GetCart(string cart_name, int id_product)
        {
            return _items.GetSingleByCondition(c => c.Cart_Name.Equals(cart_name) && c.Product_Id == id_product);
        }
        public bool AddProduct(string cart_name, int id_product, int quantity)
        {
            int Available = _product.GetSingleById(id_product).Quality;
            if (quantity > Available)
                return false;
            Cart _cart = GetCart(cart_name, id_product);
            try
            {
                if (_cart == null)
                {
                    Cart _insert = new Cart();
                    _insert.Cart_Name = cart_name;
                    _insert.Product_Id = id_product;
                    _insert.Quantity = quantity;
                    _items.Add(_insert);
                }
                else
                {
                    if (_cart.Quantity + quantity > Available)
                        return false;
                    _cart.Quantity += quantity;
                    _items.Update(_cart);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool RemoveProduct(string cart_name, int id_product, int quantity)
        {
            Cart _cart = _items.GetSingleByCondition(c => c.Cart_Name.Equals(cart_name) && c.Product_Id.Equals(id_product));
            int Available = _cart.Quantity;
            if (_cart == null)
                return false;
            if (Available < quantity)
                return false;
            _cart.Quantity -= quantity;
            _items.Update(_cart);
            if (_cart.Quantity == 0)
                _items.Delete(_cart);
            return true;
        }
        public IEnumerable<Cart> GetCarts(string cart_name)
        {
            return _items.GetMulti(c => c.Cart_Name.Equals(cart_name));
        }
        public bool CheckOut(string cart_name, Order target)
        {
            try
            {
                if (GetOrder(target.Id) == null)
                {
                    _order.Add(target);
                }
                List<Cart> _list = GetCarts(cart_name).ToList();
                if (_list.Count < 1)
                    return false;
                float Amount = 0;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8081");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    var obj = new
                    {
                        AccountID = target.AccountID,
                        IsPaid = target.IsPaid,
                        Status = target.Status,
                    };
                    string stringData = JsonConvert.SerializeObject(obj);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(contentData);
                    HttpResponseMessage response = client.PostAsync("/api/order", contentData).Result;
                }
                foreach (var item in _list)
                {
                    Order_detail _insert = new Order_detail();
                    _insert.Id = target.Id;
                    _insert.ProductID = item.Product_Id;
                    _insert.Price = _product.GetSingleById(item.Product_Id).PriceSell;
                    _insert.Quality = item.Quantity;
                    Amount += _insert.Quality * _insert.Price;
                    _order_detail.Add(_insert);
                    Product toUpdate = _product.GetSingleById(item.Product_Id);
                    toUpdate.Quality -= item.Quantity;
                    //Update product Api - Dong
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:8082");
                        MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Add(contentType);
                        var obj = new
                        {
                            ProductID = toUpdate.Id,
                            Quantity = toUpdate.Quality,
                        };
                        string stringData = JsonConvert.SerializeObject(obj);
                        var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PutAsync("/api/product/" + obj.ProductID,contentData).Result;
                    }
                    //End update 
                }
                target.Amount = Amount;
                target.Status = 2;
                _order.Update(target);
                _items.DeleteMulti(c => c.Cart_Name.Equals(cart_name));
            }
            catch
            {
                return false;
            }
            return true;
        }
        public IEnumerable<Order_detail> getalldetail()
        {
            return _order_detail.GetAll();
        }
        public IEnumerable<Order_detail> getAllDetailByID(int id)
        {
            return _order_detail.GetMulti(c => c.Id == id);
        }
    }
}