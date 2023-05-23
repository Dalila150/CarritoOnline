namespace CarritoOnline3.Models
{
    public class OrderViewModel
    {
        public IEnumerable<CarritoOnline3.Models.Entities.Order> orders { get; set; }
        public IEnumerable<CarritoOnline3.Models.Entities.OrderDetail> orderDetails { get; set; }
    }
}
