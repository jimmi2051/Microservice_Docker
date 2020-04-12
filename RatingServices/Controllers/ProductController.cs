using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rating_services.Models;
using Microsoft.AspNetCore.Mvc;
using Rating_services.Repository;
using Rating_services.Infastructure;
/* 
Add: curl -i -H "Content-Type: application/json" -X POST -d '{"Name": "Iphone XS","Detail":"Thong so ky thuat","Images":"none","Quality":10,"PriceBuy":10000000,"PriceSell":12000000,"Status":1,"Created":"12/12/2018","Modified":"12/13/2018"}' http://localhost:5000/api/product
*/
namespace Rating_services.Controllers
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
