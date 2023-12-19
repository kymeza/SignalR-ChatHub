using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_signalr_chathub_backend.DTOs.Business.Orders;
using my_signalr_chathub_backend.Models.SuperTienda;

namespace my_signalr_chathub_backend.Controllers.Business
{ 
    //[Authorize]
    [Route("api/business/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SuperTiendaContext _context;
        private readonly IMapper _mapper;


        public OrdersController(IMapper mapper, SuperTiendaContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public ActionResult CreateOrder(OrdersCompositeDto ordersCompositeDto)
        {
            // TO-DO --> Abstract this logic to a service
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Map OrderDto to Order entity
                var order = _mapper.Map<Order>(ordersCompositeDto.Order);
                _context.Orders.Add(order);
                _context.SaveChanges(); // Save to get the order ID for the details

                // Map List<OrderDetailDto> to List<OrderDetail> entities
                var orderDetails = _mapper.Map<List<OrderDetail>>(ordersCompositeDto.OrderDetails);
                foreach (var detail in orderDetails)
                {
                    // Set the foreign key to the newly created order
                    detail.IdPedido = order.IdPedido;
                    _context.OrderDetails.Add(detail);
                }

                _context.SaveChanges();
                transaction.Commit(); // Commit transaction if all is well

                // Map back to DTO to return the created order with details
                var createdOrderDto = _mapper.Map<OrderDto>(order);
                var createdOrderDetailsDto = _mapper.Map<List<OrderDetailDto>>(orderDetails);
                var result = new OrdersCompositeDto
                {
                    Order = createdOrderDto,
                    OrderDetails = createdOrderDetailsDto
                };

                return CreatedAtAction(nameof(CreateOrder), new { id = order.IdPedido }, result);
            }
            catch
            {
                // Rollback the transaction if something goes wrong
                transaction.Rollback();
                throw;
            }
        }



    }
}
