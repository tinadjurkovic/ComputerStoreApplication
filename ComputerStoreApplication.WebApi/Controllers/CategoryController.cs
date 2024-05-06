using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ComputerStoreApplication.Service.DTOs;
using ComputerStoreApplication.Service.Interfaces;

namespace ComputerStoreApplication.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public IEnumerable<CategoryDTO> GetCategories()
        {
            var categories = _categoryService.GetCategories();
            return categories;
        }

        [HttpGet]
        [Route("GetCategoryById")]
        public IActionResult GetCategoryById(int id)
        {
            CategoryDTO category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound("Category with that id does not exist!");
            }

            return Ok(category);
        }

        [HttpPost]
        [Route("AddCategory")]
        public IActionResult AddCategory(CategoryDTO category)
        {
            try
            {
                var newCategory = _categoryService.AddCategory(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, newCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding category: {ex.Message}");
                return StatusCode(500, "An error occurred while adding the category.");
            }
        }

        [HttpPut]
        [Route("UpdateCategory/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDTO category)
        {
            try
            {
                var updatedCategory = _categoryService.UpdateCategory(id, category);
                if (updatedCategory == null)
                {
                    return NotFound("Category with that id does not exist!");
                }
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating category: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the category.");
            }
        }


        [HttpDelete]
        [Route("DeleteCategory")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                if (_categoryService.DeleteCategory(id))
                {
                    return Ok("Category deleted successfully.");
                }
                else
                {
                    return NotFound("Category with that id does not exist!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting category: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the category.");
            }
        }
    }
}
