using API.eCom.EC;
using Libary.eCom.Models;
using Microsoft.AspNetCore.Mvc;



namespace API.eCom.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        public CartController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item?> Get()
        {
            return new CartEC().Get();
        }


        [HttpDelete("{id}/{count}")]
        public Item? RemoveOrDelete(int id, int count)
        {
            return new CartEC().RemoveOrDelete(id, count);
        }

        [HttpPost]
        public void add(Item item)
        {
            new CartEC().add(item.Id,item.Count);
        }

        [HttpGet("/checkout")]
        public List<double> checkout()
        {
            return new CartEC().checkOut();
        }

        [HttpGet("/checkout/confirm")]
        public void confirm()
        {
            new CartEC().ClearCart();
        }
    }
}
