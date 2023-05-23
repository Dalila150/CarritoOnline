namespace CarritoOnline3.Models
{
    public class SummaryViewModel
    {
        public CarritoOnline3.Models.Entities.Order order { get; set; }
        public IEnumerable<CarritoOnline3.Models.Entities.OrderDetail> orderDetails { get; set; }
    }
}
