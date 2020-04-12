using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order_servies.Models;
using Microsoft.AspNetCore.Mvc;
using Order_servies.Repository;
using Order_servies.Infastructure;
//curl -i -H "Content-Type: application/json" -X POST -d '{"AccountID":"admin","AccountName":"Nguyễn Văn Admin","Password":"123456","PhoneNumber":"0978956043","Address":"HCM City","Email":"admin@gmail.com","AccountType":"Admin"}' http://localhost:5000/api/account
namespace Order_servies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepository _service;
        public AccountController (IAccountRepository service)
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
        public IActionResult GetAccount(string id)
        {
            var target = _service.GetSingleByCondition(c=>c.AccountID.Equals(id));
            return Ok(target);
        }
        [HttpPost]
        public IActionResult Create([FromBody]Account model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _service.Add(model);
            return Ok(model);
        }
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody]Account model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var account = _service.GetSingleByCondition(c=>c.AccountID.Equals(id));
            if (account == null)
            {
                return NotFound();
            }
            _service.Update(model);
            return Ok(account);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var account = _service.GetSingleByCondition(c=>c.AccountID.Equals(id));
            if (account == null)
            {
                return NotFound();
            }
            _service.Delete(account);
            return Ok(account);
        }
    }
}