using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rating_services.Models;
using Microsoft.AspNetCore.Mvc;
using Rating_services.Repository;
using Rating_services.Infastructure;
using Order_servies.Helpers;
using Rating_services.Servies;
//Add: curl -i -H "Content-Type: application/json" -X POST -d '{"Product_Id":1,"AccountID":1,"Title":"none",StarRating:5,"Comment":"Xin chao","Status":1}' http://localhost:5000/api/rating
namespace Rating_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private RatingService _service;
        public RatingController(IOrderRepository order, IOrder_detailRepository order_detail, IAccountRepository account, IProductRepository product,IRatingRepository rating)
        {
            _service = new RatingService(order, order_detail, account, product,rating);
        }
        [Route("{product_id}")]
        [HttpGet] 
        public IActionResult Get(int product_id)
        {
            var result = _service.GetRatingsByProduct(product_id);
            return Ok(result);
        }
        [Route("AddRating")]
        [HttpPost]
        public IActionResult Create([FromBody]Rating model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(_service.AddRating(model))
                return Ok(model);
            return Ok("Error: Rating fail");
        }
        [Route("EditRating/{id}")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Rating model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var _rating = _service.GetRating(id);
            if (_rating == null)
            {
                return NotFound();
            }
            if(_service.UpdateRating(model))
                return Ok(model);
            return Ok("Error");
        }
        [Route("LockRating/{id}")]
        [HttpPut("{id}")]
        public IActionResult LockRating(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var _rating = _service.GetRating(id);
            if (_rating == null)
            {
                return NotFound();
            }
            if(_service.LockRating(id))
                return Ok(_rating);
            return Ok("Error");
        }
    }
}