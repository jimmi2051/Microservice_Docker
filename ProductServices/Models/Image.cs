using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Services.Models
{
    public class Image
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{get;set;}
        [Required]
        [StringLength(50), DataType("varchar")]   
        public string ImageName { get; set; }
        public string Url { get; set; }
        public virtual ICollection<Product_Image> Product_Images { get; set; } 
    }
}