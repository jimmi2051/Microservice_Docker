using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Rating_services.Models
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Product"),Column(Order = 0)]
        [Required(ErrorMessage = "Product is required")]
        public int ProductID { get; set; }
        [ForeignKey("Account"),Column(Order = 1)]
        [Required(ErrorMessage = "Username is required")]
        public int AccountID { get; set; }
        public string Title{get;set;}
        public int StarRating{get;set;}
        public string Comment{get;set;}
        public DateTime Created{get;set;}
        //Trang thai hien thi rating --- 
        public int Status{get;set;}
        public virtual Product Product { get; set; }
        public virtual Account Account { get; set; }
    }
}