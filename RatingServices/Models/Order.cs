using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Rating_services.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Account")]
        [Required(ErrorMessage = "Username is required")]
        public int AccountID { get; set; }
        
        [DataType("bit")]
        public int IsPaid{get;set;}

        [DataType("bit")]
        public int Status { get; set; }

        public virtual ICollection<Order_detail> Order_Detail { get; set; }

        public virtual Account Account { get; set; }

    }
}