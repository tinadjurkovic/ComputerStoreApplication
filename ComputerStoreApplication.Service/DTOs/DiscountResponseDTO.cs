using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Service.DTOs
{
    public class DiscountResponseDTO
    {
        public string Message { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
    }
}
