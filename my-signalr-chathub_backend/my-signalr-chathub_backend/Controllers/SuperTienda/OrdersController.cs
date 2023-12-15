﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_signalr_chathub_backend.Models.SuperTienda;

namespace my_signalr_chathub_backend.Controllers.SuperTienda
{
    //[Authorize]
    [Route("api/supertienda/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SuperTiendaContext _context;
        private readonly IMapper _mapper;

        public OrdersController(SuperTiendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // TO-DO --> Abstract the logic to a service


        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
          var orders = await _context.Orders.ToListAsync();
          var ordersDto = _mapper.Map<List<OrderDto>>(orders);
          return Ok(ordersDto);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(string id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
          var order = await _context.Orders.FindAsync(id);

          if (order == null)
          {
              return NotFound();
          }

          var orderDto = _mapper.Map<OrderDto>(order);

          return orderDto;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(string id, OrderDto orderDto)
        {
            if (id != orderDto.IdPedido)
            {
                return BadRequest();
            }
            var order = _mapper.Map<Order>(orderDto);
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'SuperTiendaContext.Orders'  is null.");
          }
          _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.IdPedido))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrder", new { id = order.IdPedido }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(string id)
        {
            return (_context.Orders?.Any(e => e.IdPedido == id)).GetValueOrDefault();
        }
    }
}
