using Microsoft.AspNetCore.Mvc;
using Libary.eCom.Models;
using API.eCom.EC;

namespace API.eCom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {

        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Item?> Get()
        {
            return new InventoryEC().Get();
        }

        [HttpGet("{id}")]
        public Item? GetById(int id)
        {
            return new InventoryEC().Get()
                .FirstOrDefault(i => i?.Id == id);
        }

        [HttpDelete("{id}")]
        public Item? Delete(int id)
        {
            return new InventoryEC().Delete(id);
        }

        [HttpPost]
        public Item? AddOrUpdate([FromBody] Item item)
        {

            var newItem = new InventoryEC().AddOrUpdate(item);
            return item;
        }
    }
}
