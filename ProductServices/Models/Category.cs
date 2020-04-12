using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Product_Services.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CategoryID{get;set;}
        public string CategoryName{get;set;}
        public int Quantity{get;set;}
        public Boolean Is_Active{get;set;}
        public Boolean Archive{get;set;}

        public virtual ICollection<Product_Category> Product_Categories { get; set; }
    }
}