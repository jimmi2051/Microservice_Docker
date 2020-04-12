using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Product_Services.Models
{
    public class Product_Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Product")]
        [Required(ErrorMessage = "Product is required")]
        public int ProductID{get;set;}
        [ForeignKey("Image")]
        [Required(ErrorMessage = "Username is required")]
        public int ImageID{get;set;}
        public virtual Product Product { get; set; }
        public virtual Image Image {get;set;}

    }
}