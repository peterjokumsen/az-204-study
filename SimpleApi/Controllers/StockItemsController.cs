using Microsoft.AspNetCore.Mvc;
using SimpleApi.Models;

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("stock-items")]
    public class StockItemsController : ControllerBase
    {
        private static readonly List<ManufacturingStockItem> Items =
        [
            new ManufacturingStockItem
            {
                Id = Guid.Parse("7c5cf7de-99bb-4a62-b2b2-6de66020fadb"),
                PartNumber = "PN-001",
                Name = "Widget A",
                QuantityAvailable = 100
            },
            new ManufacturingStockItem
            {
                Id = Guid.NewGuid(),
                PartNumber = "PN-002",
                Name = "Widget B",
                QuantityAvailable = 50
            },
            new ManufacturingStockItem
            {
                Id = Guid.NewGuid(),
                PartNumber = "PN-003",
                Name = "Widget C",
                QuantityAvailable = 75
            },
        ];

        [HttpGet(Name = "GetStock")]
        public IEnumerable<ManufacturingStockItem> GetItems()
        {
            return Items.ToArray();
        }

        [HttpGet("{id:guid}", Name = "GetStockById")]
        public ActionResult<ManufacturingStockItem> GetItemById(Guid id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item is null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost(Name = "AddStockItem")]
        public ActionResult<ManufacturingStockItem> AddItem([FromBody] ManufacturingStockItem newItem)
        {
            var existing = Items.FirstOrDefault(i => i.PartNumber == newItem.PartNumber);
            if (existing is not null)
            {
                return Conflict($"An item with PartNumber {newItem.PartNumber} already exists.");
            }

            var itemToAdd = new ManufacturingStockItem
            {
                Id = Guid.NewGuid(),
                PartNumber = newItem.PartNumber,
                Name = newItem.Name,
                QuantityAvailable = newItem.QuantityAvailable
            };
            Items.Add(itemToAdd);
            return CreatedAtAction(nameof(GetItemById), new { id = itemToAdd.Id }, itemToAdd);
        }

        [HttpPut("{id:guid}", Name = "UpdateStockItem")]
        public ActionResult<ManufacturingStockItem> UpdateItem(Guid id, [FromBody] ManufacturingStockItem updatedItem)
        {
            var index = Items.FindIndex(i => i.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            var itemToUpdate = new ManufacturingStockItem
            {
                Id = id,
                PartNumber = updatedItem.PartNumber,
                Name = updatedItem.Name,
                QuantityAvailable = updatedItem.QuantityAvailable
            };
            Items[index] = itemToUpdate;
            return itemToUpdate;
        }
    }
}
