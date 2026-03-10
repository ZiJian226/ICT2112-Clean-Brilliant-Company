using CleanBrilliant.Domain.Control;
using CleanBrilliant.Domain.Entity;
using CleanBrilliant.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CleanBrilliant.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductManager _control;

        public ProductController(ProductManager control)
        {
            _control = control;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _control.ListProductCatalog();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var product = await _control.GetProduct(id);
                return Ok(new
                {
                    productId = product.ProductId,
                    name = product.Name,
                    description = product.Description,
                    categoryId = product.CategoryId,
                    volume = product.Volume,
                    weight = product.Weight,
                    ingredients = product.Ingredients,
                    unitCost = product.UnitCost,
                    sellPrice = product.SellPrice
                    // toxicPercentage = product.ToxicPercentage
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Product with ID {id} not found" });
            }
        }

        [HttpGet("category/{categoryId:int}")]
        public async Task<IActionResult> GetByCategory([FromRoute] int categoryId)
        {
            var products = await _control.ListProductsByCategory(categoryId);
            var result = products.Select(p => new
            {
                productId = p.ProductId,
                name = p.Name,
                description = p.Description,
                categoryId = p.CategoryId,
                volume = p.Volume,
                weight = p.Weight,
                ingredients = p.Ingredients,
                unitCost = p.UnitCost,
                sellPrice = p.SellPrice
                // toxicPercentage = p.ToxicPercentage
            });

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Keyword is required" });

            var products = await _control.SearchProducts(keyword);
            var result = products.Select(p => new
            {
                productId = p.ProductId,
                name = p.Name,
                description = p.Description,
                categoryId = p.CategoryId,
                volume = p.Volume,
                weight = p.Weight,
                ingredients = p.Ingredients,
                unitCost = p.UnitCost,
                sellPrice = p.SellPrice
                // toxicPercentage = p.ToxicPercentage
            });

            return Ok(result);
        }
    }
}
