using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rating_services.Models;
using Microsoft.AspNetCore.Mvc;
using Rating_services.Repository;
using Rating_services.Infastructure;
//curl -i -H "Content-Type: application/json" -X POST -d '{"AccountID":"admin","AccountName":"Nguyễn Văn Admin","Password":"123456","PhoneNumber":"0978956043","Address":"HCM City","Email":"admin@gmail.com","AccountType":"Admin"}' http://localhost:5000/api/account
namespace Rating_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order_detailController : ControllerBase
    {
        private IOrder_detailRepository _service;
        public Order_detailController (IOrder_detailRepository service)
        {
            _service = service;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var model = _service.GetAll();
            return Ok(model);
        }
        [HttpGet]
        [Route("getbyorder-{id}")]
        public IActionResult GetByOrder(int id){
            var model = _service.GetMulti(c=>c.Id.Equals(id));
            return Ok(model);
        }
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id,int product)
        {
            var target = _service.GetSingleByCondition(c=>c.Id==id && c.ProductID==product);
            return Ok(target);
        }
        [HttpPost]
        public IActionResult Create([FromBody]Order_detail model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _service.Add(model);
            return Ok(model);
        }
        [HttpPut("{id}-{product}")]
        public IActionResult Update(int id,int product, [FromBody]Order_detail model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var order = _service.GetSingleByCondition(c=>c.Id==id && c.ProductID==product);
            if (order == null)
            {
                return NotFound();
            }
            _service.Update(model);
            return Ok(order);
        }
        [HttpDelete("{id}-{product}")]
        public IActionResult Delete(int id,int product)
        {
            var order = _service.GetSingleByCondition(c=>c.Id==id && c.ProductID==product);
            if (order == null)
            {
                return NotFound();
            }
            _service.Delete(order);
            return Ok(order);
        }
    }
}