using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product_Services.Models;
using Microsoft.AspNetCore.Mvc;
using Product_Services.Repository;
using Product_Services.Infastructure;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
/*
  {
  "product":
      {
      "ProductID":"SP001",
      "Name":"Sách giáo khoa 1",
      "Detail":"Chương trình lớp 1",
      "Languge":"VietNam",
      "Quantity":5,
      "Author":"Mr Thành",
      "Is_Active":"True",
      "Archive":"True"
    },
  "images":[2,3],
  "categories":[1],
  "discount":
  {
    "percent":5,
    "Date_start":"1/1/2018",
    "Date_end":"12/12/2018",
    "Active":"True"
  }
}
  {
  "product":
      {
      "ProductID":"SP002",
      "Name":"Sách giáo khoa 2",
      "Detail":"Sách giáo khoa 2",
      "Languge":"VietNam",
      "Quantity":5,
      "Author":"Mr Thành",
      "Is_Active":"True",
      "Archive":"True"
    },
  "images":[2,3],
  "categories":[1]
}
 */
namespace Product_Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _service;
        private ICategoryRepository _catrepository;
        private IProduct_ImageRepository _service_img;
        private IProduct_CategoryRepository _service_cat;
        private IDiscountRepository _service_discount;
        public ProductController(ICategoryRepository catrepository, IProductRepository service, IProduct_ImageRepository service_img, IProduct_CategoryRepository service_cat, IDiscountRepository service_discount)
        {
            _catrepository = catrepository;
            _service = service;
            _service_img = service_img;
            _service_cat = service_cat;
            _service_discount = service_discount;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var model = _service.GetAll();
            return Ok(model);
        }
        [Route("GetAllAvailable")]
        [HttpGet]
        public IActionResult GetAllAvailable()
        {
            var model = _service.GetMulti(c => c.Is_Active);
            return Ok(model);
        }
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var target = _service.GetSingleById(id);
            return Ok(target);
        }

        [HttpPost]
        public IActionResult Create([FromBody]JObject data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Product model = data["product"].ToObject<Product>();
            var _listImg = data["images"].ToList();
            var _listCat = data["categories"].ToList();

            Product Created = _service.Add(model);
            if (data["discount"] != null)
            {
                Discount _objDiscount = data["discount"].ToObject<Discount>();
                _objDiscount.ProductID = Created.Id;
                _service_discount.Add(_objDiscount);
            }
            for (int i = 0; i < _listImg.Count; i++)
            {
                Product_Image toCreate = new Product_Image()
                {
                    ImageID = (int)_listImg[i],
                    ProductID = Created.Id,
                };
                _service_img.Add(toCreate);
            }
            for (int i = 0; i < _listCat.Count; i++)
            {
                Product_Category toCreate = new Product_Category()
                {
                    ProductID = Created.Id,
                    CategoryID = (int)_listCat[i],
                };
                _service_cat.Add(toCreate);
                Category toUpdate = _catrepository.GetSingleById(toCreate.CategoryID);
                toUpdate.Quantity++;
                _catrepository.Update(toUpdate);
            }
            return Ok("Success");
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]JObject data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Product = _service.GetSingleById(id);
            if (Product == null)
            {
                return NotFound();
            }
            Product model = data["product"].ToObject<Product>();
            var _listImg = data["images"].ToList();
            var _listCat = data["categories"].ToList();
            if (data["discount"] != null)
            {
                Discount _objDiscount = data["discount"].ToObject<Discount>();
                _service_discount.Update(_objDiscount);
            }
            //Add tat' ca hinh anh vao
            for (int i = 0; i < _listImg.Count; i++)
            {
                Product_Image toCreate = new Product_Image()
                {
                    ImageID = (int)_listImg[i],
                    ProductID = id,
                };
                _service_img.Add(toCreate);
            }
            //Check lai danh muc xem co cai nao bi xoa k - co thi delete no ra. 
            foreach (var item in _service_img.GetMulti(c => c.ProductID == id))
            {
                bool flag = true;
                for (int i = 0; i < _listImg.Count; i++)
                {
                    if (item.ImageID == (int)_listImg[i])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    _service_img.Delete(item);
                }
            }
            //Add lai tat ca cac danh muc 
            for (int i = 0; i < _listCat.Count; i++)
            {
                Product_Category toCreate = new Product_Category()
                {
                    CategoryID = (int)_listCat[i],
                    ProductID = id,
                };
                _service_cat.Add(toCreate);
                Category toUpdate = _catrepository.GetSingleById(toCreate.CategoryID);
                toUpdate.Quantity++;
                _catrepository.Update(toUpdate);
            }
            //Check lai danh muc xem co cai nao bi xoa k - co thi delete no ra. 
            foreach (var item in _service_cat.GetMulti(c => c.ProductID == id))
            {
                bool flag = true;
                for (int i = 0; i < _listCat.Count; i++)
                {
                    if (item.CategoryID == (int)_listCat[i])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    _service_cat.Delete(item);
                    Category toUpdate = _catrepository.GetSingleById(item.CategoryID);
                    toUpdate.Quantity--;
                    _catrepository.Update(toUpdate);
                }
            }
            _service.Update(model);
            return Ok(model);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Product = _service.GetSingleById(id);
            if (Product == null)
            {
                return NotFound();
            }
            Product.Is_Active = false;
            _service.Update(Product);
            return Ok(Product);
        }
        [Route("GetImgByProduct-{id}")]
        [HttpGet("{id}")]
        public IActionResult GetImg(int id)
        {
            return Ok(_service_img.GetMulti(c => c.ProductID == id));
        }
        [Route("GetCatByProduct-{id}")]
        [HttpGet("{id}")]
        public IActionResult GetCat(int id)
        {
            return Ok(_service_cat.GetMulti(c => c.ProductID == id));
        }
    }
}