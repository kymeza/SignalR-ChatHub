using my_signalr_chathub_backend.Models.SuperTienda;
using AutoMapper;


namespace my_signalr_chathub_backend.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<Client, ClientDto>();
            CreateMap<Product, ProductDto>();
        }
    }
}
