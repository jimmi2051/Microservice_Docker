using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order_servies.Models;
using Microsoft.AspNetCore.Mvc;
using Order_servies.Repository;
using Order_servies.Infastructure;
using Order_servies.Servies;
using Order_servies.Helpers;
//curl -i -H "Content-Type: application/json" -X POST -d '{"AccountID":"admin","Amount":500000,"Address":"NCC","Phone":"0978956043","Description":"Thong so ky thuat","OrderDate":"12/12/2018","RequireDate":"12/12/2018","IsPaid":0,"Status":0}' http://localhost:5000/api/order

namespace Order_servies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // private Servies_Order _service;
        // public OrderController(IOrderRepository order, IOrder_detailRepository order_detail, IAccountRepository account, IProductRepository product)
        // {
        //     _service = new Servies_Order(order, order_detail, account, product);
        // }
        // // GET api/values
        // [HttpGet]
        // public IActionResult Get()
        // {
        //     var model = _service.getAllOrder();
        //     return Ok(model);
        // }
        // [HttpGet("{id}")]
        // public IActionResult GetOrder(int id)
        // {
        //     var target = _service.GetOrder(id);
        //     return Ok(target);
        // }
        // Order createOrder()
        // {
        //     Order result = new Order();
        //     result.AccountID = "Admin";
        //     result.OrderDate = DateTime.Now;
        //     result.RequireDate = DateTime.Now.AddDays(7);
        //     result.Status = 0;
        //     result.IsPaid = 0;
        //     return result;
        // }
        // [HttpPost]
        // public IActionResult Create([FromBody]Order model)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);
        //     if (_service.CreateOrder(model))
        //     {
        //         var cart = SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart");
        //         if(cart == null)
        //             return Ok("Error");
        //         _service.submitOrder(cart, model);
        //     }
        //     else
        //     {
        //         return Ok("Error");
        //     }
        //     return Ok(model);
        // }
        // [HttpPut("{id}")]
        // public IActionResult Update(int id, [FromBody]Order model)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);
        //     var product = _service.GetSingleByCondition(c=>c.Id.Equals(id));
        //     if (product == null)
        //     {
        //         return NotFound(); 
        //     }
        //     _service.Update(model);
        //     return Ok(product);
        // }
        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id)
        // {
        //     var product = _service.GetSingleByCondition(c=>c.IsPaid.Equals(id));
        //     if (product == null)
        //     {
        //         return NotFound();
        //     }
        //     _service.Delete(product);
        //     return Ok(product);
        // }
    }
}