using MicroService_QLBanDienThoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService_QLBanDienThoai.BUS
{
    public class ProductCategoryBUS
    {
        private QLBanDienThoaiContext context;

        public ProductCategoryBUS()
        {
            this.context = new QLBanDienThoaiContext();
        }
        public ProductCategoryBUS(QLBanDienThoaiContext context)
        {
            this.context = context;
        }

        public List<ProductCategory> GetProductCategory()
        {
            List<ProductCategory> list = context.ProductCategory.ToList();
            return list;
        }



        //------------------------------------------------------ THEM SUA XOA -----------------------------------------------------------------
        public string CreateProductCategory(string CategoryID, string ProductID)
        {
            int tempID = Int32.Parse(CategoryID);
            int tempID2 = Int32.Parse(ProductID);
            ProductCategory check = context.ProductCategory.Where(temp=>temp.ProductId == tempID2).SingleOrDefault();
            if (check !=null)
            {
                return "Thông tin này đã tồn tại";
            }

            ProductCategory productCategory = new ProductCategory();
            productCategory.ProductId = tempID2;
            productCategory.CategoryId = tempID;
            context.ProductCategory.Add(productCategory);
            context.SaveChanges();

            return "Thêm thành công";
        }
        public string EditProductCategory(string CategoryID, string ProductID)
        {
            int tempID = Int32.Parse(CategoryID);
            int tempID2 = Int32.Parse(ProductID);

            ProductCategory check = context.ProductCategory.Where(temp => (temp.CategoryId == tempID) && (temp.ProductId == tempID2)).SingleOrDefault();
            if (check != null)
            {
                return "Thông tin này đã tồn tại";
            }
            ProductCategory productCategory = context.ProductCategory.Where(temp => temp.ProductId == tempID2).SingleOrDefault();

            productCategory.CategoryId = tempID;
            context.SaveChanges();

            context.SaveChanges();
            return "Sửa thành công";
        }

        public List<ProductCategory> SearchProductCategory(string search)
        {
            List<ProductCategory> list = new List<ProductCategory>();
            if (search == null)
            {
                list = GetProductCategory();
            }
            else
            {
                //list = context.ProductCategory.Where(temp => temp.ProductId.Contains(search)).ToList();
            }
            return list;
        }
    }

}
