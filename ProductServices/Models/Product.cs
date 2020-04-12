using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Product_Services.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProductID{get;set;}
        public String Name { get; set; }
        public string Detail {get;set;}
        public string Languge{get;set;}
        public int Quantity { get; set; }
        public String Author {get;set;}
        public Boolean Is_Active {get;set;}
        public Boolean Archive {get;set;}   
        public virtual ICollection<Product_Category> Product_Categories { get; set; } 
        public virtual ICollection<Product_Image> Product_Images { get; set; } 
    }

}