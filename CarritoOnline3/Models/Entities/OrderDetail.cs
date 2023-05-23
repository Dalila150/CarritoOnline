using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarritoOnline3.Models.Entities
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
