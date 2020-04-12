using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Rating_services.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public String Name { get; set; }

        public int Quality { get; set; }


        [DataType("bit")]
        public int Status { get; set; }

        public virtual ICollection<Order_detail> Order_Details { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}