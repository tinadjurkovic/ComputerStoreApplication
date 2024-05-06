using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreApplication.Service.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
    }
}