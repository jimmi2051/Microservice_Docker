using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Order_servies.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public String Name { get; set; }

        public int Quality { get; set; }

        public float PriceBuy { get; set; }

        public float PriceSell { get; set; }


        public virtual ICollection<Order_detail> Order_Details { get; set; }
    }
}