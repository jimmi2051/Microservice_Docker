using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order_servies.Models;
using Microsoft.AspNetCore.Mvc;
using Order_servies.Repository;
using Order_servies.Infastructure;
/* 
Add: curl -i -H "Content-Type: application/json" -X POST -d '{"ProductID": "SP002", "Name": "Iphone XS","Detail":"Thong so ky thuat","Images":"none","Quality":10,"PriceBuy":10000000,"PriceSell":12000000,"Status":1,"Created":"12/12/2018","Modified":"12/13/2018"}' http://localhost:5000/api/product
{
"AccountID":1,
  "Amount":50000,
  "Address":"123",
  "Phone":"123123",
  "Description":"Hello",
  "OrderDate":"12/12/2018",
  "RequireDate":"12/12/2018",
  "Status":1,
  "IsPaid":1
}
*/

namespace Order_servies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _service;
        public ProductController (IProductRepository service)
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
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var target = _service.GetSingleById(id);
            return Ok(target);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _service.Add(model);
            return Ok(model);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Product = _service.GetSingleById(id);
            if (Product == null)
            {
                return NotFound();
            }
            Product.Quality = model.Quality;
            _service.Update(Product);
            return Ok(Product);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Product = _service.GetSingleById(id);
            if (Product == null)
            {
                return NotFound();
            }
            _service.Delete(Product);
            return Ok(Product);
        }
    }
}