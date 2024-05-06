using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerStoreApplication.Service.DTOs;

namespace ComputerStoreApplication.Service.Interfaces
{
    public interface IProductService
    {
        List<ProductDTO> GetProducts();

        ProductDTO GetProductById(int id);

        ProductDTO AddProduct(ProductDTO productDTO);

        ProductDTO UpdateProduct(ProductDTO productDTO);

        bool DeleteProduct(int id);
    }
}
