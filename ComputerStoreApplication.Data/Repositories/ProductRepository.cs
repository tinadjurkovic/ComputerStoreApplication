using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStoreApplication.Data.Entities;
using ComputerStoreApplication.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerStoreApplication.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ComputerStoreDataContext _dataContext;

        public ProductRepository(ComputerStoreDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddProduct(Product product)
        {
            _dataContext.Products.Add(product);
            _dataContext.SaveChanges();
        }

        public bool DeleteProduct(Product product)
        {
            try
            {
                _dataContext.Products.Remove(product);
                _dataContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Product GetProductById(int id)
        {
            return _dataContext.Products.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dataContext.Products.ToList();
        }

        public void UpdateProduct(Product product)
        {
            _dataContext.Products.Update(product);
            _dataContext.SaveChanges();
        }

        public void UpdateProduct(Product product, Product newProduct)
        {
            var existingProduct = _dataContext.Products.FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = newProduct.Name;
                existingProduct.Description = newProduct.Description;
                existingProduct.Price = newProduct.Price;
                existingProduct.Quantity = newProduct.Quantity;

                _dataContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"Product with id {product.Id} not found.");
            }
        }

        public Category GetCategoryByName(string name)
        {
            return _dataContext.Categories.FirstOrDefault(c => c.Name == name);
        }

        public void AddCategory(Category category)
        {
            _dataContext.Categories.Add(category);
            _dataContext.SaveChanges();
        }
    }
}
