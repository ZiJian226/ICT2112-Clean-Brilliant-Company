using CleanBrilliant.Domain.Control;
using CleanBrilliant.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CleanBrilliant.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryManager _control;

        public CategoryController(CategoryManager control)
        {
            _control = control;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _control.ListAllCategory();
            var result = categories.Select(c => new
            {
                categoryId = c.Id,
                name = c.Name,
                description = c.Description,
                activityStatus = c.ActivityStatus
            });

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await _control.GetCategoryById(id);
            if (category == null) return NotFound();

            return Ok(new
            {
                categoryId = category.CategoryId,
                name = category.Name,
                description = category.Description,
                activityStatus = category.ActivityStatus
            });
        }

    }
}
