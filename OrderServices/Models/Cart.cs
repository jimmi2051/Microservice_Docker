using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Order_servies.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{get;set;}
        public string Cart_Name{get;set;}
        
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}