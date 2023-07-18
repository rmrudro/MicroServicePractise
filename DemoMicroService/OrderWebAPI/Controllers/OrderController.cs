using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Models;

namespace OrderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _dbContext;

        public OrderController(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetById(int orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult> Update(int orderId, Order order)
        {
            if (orderId != order.OrderId)
                return BadRequest();

            _dbContext.Entry(order).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(orderId))
                    return NotFound();
                else
                    throw;
            }

            return Ok();
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> Delete(int orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
                return NotFound();

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private bool OrderExists(int orderId)
        {
            return _dbContext.Orders.Any(o => o.OrderId == orderId);
        }
    }
}
