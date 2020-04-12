using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Product_Services.Models
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Product")]
        [Required(ErrorMessage = "Product is required")]
        public int ProductID { get; set; }
        public int percent{get;set;}
        public DateTime Date_start{get;set;}
        public DateTime Date_end{get;set;}
        public Boolean Active{get;set;}
        public virtual Product Product { get; set; }
    }
}