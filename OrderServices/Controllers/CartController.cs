using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order_servies.Models;
using Microsoft.AspNetCore.Mvc;
using Order_servies.Repository;
using Order_servies.Infastructure;
using Order_servies.Helpers;
using Order_servies.Servies;

namespace Order_servies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private Servies_Order _service;
        public CartController(IOrderRepository order, ICartRepository items, IOrder_detailRepository order_detail, IAccountRepository account, IProductRepository product)
        {
            _service = new Servies_Order(order, items, order_detail, account, product);
        }
        [Route("{cartname}")]
        [HttpGet]
        public IActionResult Get(String cartName)
        {
            var result = _service.GetCarts(cartName);
            return Ok(result);
        }
        [Route("add/{cartname}-{productid}-{quantity}")]
        [HttpPost]
        public IActionResult AddToCart(string cartname, int productid, int quantity)
        {
            if (_service.AddProduct(cartname, productid, quantity))
            {
                Cart result = _service.GetCart(cartname, productid);
                return Ok(result);
            }
            return Ok("Error Add");
        }
        [Route("remove/{cartname}-{productid}-{quantity}")]
        [HttpPost]
        public IActionResult RemoveFromCart(string cartname, int productid, int quantity)
        {
            if (_service.RemoveProduct(cartname, productid, quantity))
            {
                var result = _service.GetCarts(cartname);
                return Ok(result);
            }
            return Ok("Error Add");
        }
        [Route("checkout/{cartname}")]
        [HttpPost]
        public IActionResult CheckOut(string cartname, [FromBody]Order model)
        {

            if (_service.CheckOut(cartname,model))
            {
                return Ok("Thanh toan thanh cong");
            }
            return Ok("Loi: Thanh toan that bai");
        }
        [Route("orders")]
        [HttpGet]
        public IActionResult getOrder()
        {
            return Ok(_service.getAllOrder());
        }
        [Route("order/{id}")]
        [HttpGet]
        public IActionResult getOrderbyID(int id)
        {
            return Ok(_service.getOrderbyID(id));
        }
        [Route("detail_order")]
        [HttpGet]
        public IActionResult getDetail()
        {
            return Ok(_service.getalldetail());
        }
        [Route("detail_order/{order_id}")]
        public IActionResult getOrderByID(int order_id)
        {
            return Ok(_service.getAllDetailByID(order_id));
        }
    }
}