using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rating_services.Models
{
    public class Order_detail
    {   
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ProductID { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}