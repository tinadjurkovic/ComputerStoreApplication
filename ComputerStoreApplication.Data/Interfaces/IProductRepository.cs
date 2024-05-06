using System.Collections.Generic;
using ComputerStoreApplication.Data.Entities;

namespace ComputerStoreApplication.Data.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();

        Product GetProductById(int id);

        void AddProduct(Product product);

        void UpdateProduct(Product product, Product newProduct);

        bool DeleteProduct(Product product);

        Category GetCategoryByName(string name);
        void AddCategory(Category category);
    }

}
