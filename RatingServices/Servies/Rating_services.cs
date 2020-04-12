using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rating_services.Models;
using Rating_services.Repository;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rating_services.Servies
{
    public class RatingService
    {
        private static IOrderRepository _order;
        private static IOrder_detailRepository _order_detail;
        private static IAccountRepository _account;
        private static IProductRepository _product;
        public static IRatingRepository _rating;
        public RatingService()
             : this(_order, _order_detail, _account, _product, _rating)
        { }
        public RatingService(IOrderRepository order, IOrder_detailRepository order_detail, IAccountRepository account, IProductRepository product, IRatingRepository rating)
        {
            _order = order;
            _order_detail = order_detail;
            _account = account;
            _product = product;
            _rating = rating;
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

        public IEnumerable<Order_detail> getalldetail()
        {
            return _order_detail.GetAll();
        }
        public IEnumerable<Order_detail> getAllDetailByID(int id)
        {
            return _order_detail.GetMulti(c => c.Id == id);
        }

        /* Rating Service */
        public IEnumerable<Rating> GetRatingsByProduct(int id)
        {
            return _rating.GetMulti(c => c.ProductID == id);
        }
        public bool ValidateRating(Rating target)
        {
            if (target.StarRating > 10 || target.StarRating < 0)
                return false;
            if (_product.GetSingleById(target.ProductID) == null)
                return false;
            if (_account.GetSingleById(target.AccountID) == null)
                return false;
            if (!Regex.IsMatch(target.Title, @"\w") || !Regex.IsMatch(target.Comment, @"\w"))
                return false;
            if (_order.GetSingleByCondition(c => c.AccountID == target.AccountID) == null)
                return false;
            return true;
        }
        public bool AddRating(Rating target)
        {
            if (target == null)
                return false;
            if (!ValidateRating(target))
                return false;
            target.Created = DateTime.Now;
            _rating.Add(target);

            return true;
        }
        public bool UpdateRating(Rating target)
        {
            if (_rating.GetSingleById(target.Id) == null)
                return false;
            if (!ValidateRating(target))
                return false;
            try
            {
                _rating.Update(target);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool LockRating(int id)
        {
            Rating target = _rating.GetSingleById(id);
            if (target == null)
                return false;
            try
            {
                target.Status = -1;
                _rating.Update(target);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public Rating GetRating(int id)
        {
            return _rating.GetSingleById(id);
        }
    }
}