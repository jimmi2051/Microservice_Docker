using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rating_services.Models
{
    public class Account
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{get;set;}
        [Required]
        [StringLength(50), DataType("varchar")]   
        public string AccountID { get; set; }
        public string AccountName { get; set; }
        public string AccountType{get;set;}
        public virtual ICollection<Rating> Ratings { get; set; }

    }
}