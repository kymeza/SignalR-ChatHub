namespace my_signalr_chathub_backend.Models.Business.Orders
{
    public class OrdersCompositeDto
    {
        public OrderDto Order { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
        
    }
}
