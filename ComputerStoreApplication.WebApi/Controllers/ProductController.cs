using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ComputerStoreApplication.Service.DTOs;
using ComputerStoreApplication.Service.Interfaces;

namespace ComputerStoreApplication.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public IEnumerable<ProductDTO> GetProducts()
        {
            var products = _productService.GetProducts();

            return products;
        }

        [HttpGet]
        [Route("GetProductById")]
        public IActionResult GetProductById(int id)
        {
            ProductDTO product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound("Product with that id does not exist!");
            }

            return Ok(product);
        }

        [HttpPost]
        [Route("AddProduct")]
        public IActionResult Post([FromBody] ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                var newProduct = _productService.AddProduct(product);
                return Created($"Product with id {newProduct.Id} has been created!", newProduct.Id);
            }

            return UnprocessableEntity(ModelState);
        }

        [HttpPut]
        [Route("UpdateProduct/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                product.Id = id;
                var result = _productService.UpdateProduct(product);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }

            return BadRequest();
        }

        [HttpDelete("RemoveProduct/{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(_productService.DeleteProduct(id));
            }

            return BadRequest();
        }
    }
}
