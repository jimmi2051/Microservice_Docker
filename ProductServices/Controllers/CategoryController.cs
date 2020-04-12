using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Product_Services.Repository;
using Product_Services.Infastructure;

/*
{
  "CategoryID":"CAT001",
  "CategoryName":"Sách giáo khoa",
  "Quantity":0,
  "Is_Active":"True",
  "Archive":"True"
}
{
  "CategoryID":"CAT002",
  "CategoryName":"Sách tiếng việt",
  "Quantity":0,
  "Is_Active":"True",
  "Archive":"True"
}
 */
namespace Product_Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _service;
        public CategoryController(ICategoryRepository service)
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
        [Route("GetAvaiable")]
        [HttpGet]
        public IActionResult GetAvailable()
        {
            var model = _service.GetMulti(c => c.Is_Active);
            return Ok(model);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var target = _service.GetSingleById(id);
            return Ok(target);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Category model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _service.Add(model);
            return Ok(model);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Category model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Category = _service.GetSingleById(id);
            if (Category == null)
            {
                return NotFound();
            }
            _service.Update(model);
            return Ok(model);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Category = _service.GetSingleById(id);
            if (Category == null)
            {
                return NotFound();
            }
            Category.Is_Active = false;
            _service.Update(Category);
            return Ok(Category);
        }
    }
}