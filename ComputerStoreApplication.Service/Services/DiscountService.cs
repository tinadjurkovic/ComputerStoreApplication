using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStoreApplication.Service.DTOs;

namespace ComputerStoreApplication.Service.Services
{
    public class DiscountService
    {
        public DiscountResponseDTO CalculateDiscount(List<ProductDTO> products)
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

                return new DiscountResponseDTO
                {
                    Message = message,
                    TotalPrice = totalPrice,
                    TotalDiscount = totalDiscount
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while calculating discount.", ex);
            }
        }
    }
}
