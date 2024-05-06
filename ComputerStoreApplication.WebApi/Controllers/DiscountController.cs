using ComputerStoreApplication.Service.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputerStoreApplication.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : ControllerBase
    {
        [HttpPost("Calculate")]
        public IActionResult CalculateDiscount([FromBody] List<ProductDTO> products)
        {
            try
            {
                decimal totalPrice = 0;
                decimal totalDiscount = 0;
                string message = "";

                foreach (var product in products)
                {
                    if (product.Quantity > 1 && product.CategoryList.Count > 0)
                    {
                        var category = product.CategoryList.First();
                        decimal itemDiscount = (product.Price * 0.05m * (product.Quantity - 1));
                        totalDiscount += itemDiscount;
                        message += $"You've received a 5% discount on {product.Name} ({product.Quantity} items of {category}). ";
                    }
                    else
                    {
                        message += $"No discount applied to {product.Name} ({product.Quantity} items). ";
                    }

                    totalPrice += product.Price * product.Quantity;
                }

                var response = new
                {
                    Message = message,
                    TotalPrice = totalPrice,
                    TotalDiscount = totalDiscount
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while calculating discount.");
            }
        }
    }
}
