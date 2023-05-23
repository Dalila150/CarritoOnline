using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarritoOnline3.Models.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual List<OrderDetail> OrderDetail { get; set; }
    }
}
