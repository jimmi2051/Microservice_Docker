using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroService_QLBanDienThoai.Models;
namespace MicroService_QLBanDienThoai.BUS
{
    public class CategoryBUS
    {
        private readonly QLBanDienThoaiContext context;
        public CategoryBUS()
        {
            this.context = new QLBanDienThoaiContext();
        }

        public CategoryBUS(QLBanDienThoaiContext context)
        {
            this.context = context;
        }

        public List<Category> GetCategory()
        {
            List<Category> list = context.Category.ToList();
            return list;
        }



        //------------------------------------------------------ THEM SUA XOA -----------------------------------------------------------------
        public string CreateCategory(string CategoryID, string CategoryName, int Quantity, int IsActive, int Archive)
        {

            Category Category = new Category();
            //----------------------- chuan hoa du lieu ----------------------- 
            //----------------------- kiem tra ma -----------------------
            //Category = context.Category.Where(temp => temp.CategoryId == CategoryID).SingleOrDefault();
            //if (Category != null)
            //{
            //    return "Mã loại đã tồn tại";
            //}
            //----------------------- kiem tra ten -----------------------
            //Category = context.Category.Where(temp => temp.CategoryName == CategoryName).SingleOrDefault();
            //if (Category != null)
            //{
            //    return "Tên Category đã tồn tại";
            //}
            //----------------------- them -----------------------
            Category = new Category();
            //Category.CategoryId = CategoryID;
            Category.CategoryName = CategoryName;
            Category.Quantity = Quantity;
            Category.IsActive = IsActive;
            Category.Archive = Archive;

            context.Category.Add(Category);
            context.SaveChanges();

            return "Thêm thành công";
        }
        public string EditCategory(string CategoryID, string CategoryName, int Quantity, int IsActive, int Archive)
        {
            Category Category = new Category();
            int tempid = Int32.Parse(CategoryID);
            //----------------------- chuan hoa du lieu ----------------------- 
            //----------------------- kiem tra ma -----------------------
            Category = context.Category.Where(temp => temp.CategoryId == tempid).SingleOrDefault();

            if (CategoryName != null)
            {
                Category.CategoryName = CategoryName;
            }
            if (Quantity != Category.Quantity)
            {
                Category.Quantity = Quantity;
            }
            if (IsActive != Category.IsActive)
            {
                Category.IsActive = IsActive;
            }
            if (Archive != Category.Archive)
            {
                Category.Archive = Archive;
            }
            context.SaveChanges();
            return "Sửa thành công";
        }

        public string DeleteCategory(string CategoryID)
        {
            int tempid = Int32.Parse(CategoryID);
            Category Category = context.Category.Where(temp => temp.CategoryId == tempid).SingleOrDefault();
            context.Category.Remove(Category);
            context.SaveChanges();
            return "Xoá thành công";
        }

        public List<Category> SearchCategory(string search)
        {
            List<Category> list = new List<Category>();
            if (search == null)
            {
                list = GetCategory();
            }
            else
            {
                list = context.Category.Where(temp => temp.CategoryName.Contains(search)).ToList();
            }
            return list;
        }
    }
}
