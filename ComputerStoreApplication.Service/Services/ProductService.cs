using AutoMapper;
using ComputerStoreApplication.Data.Entities;
using ComputerStoreApplication.Data.Interfaces;
using ComputerStoreApplication.Service.DTOs;
using ComputerStoreApplication.Service.Interfaces;
using System.Collections.Generic;

namespace ComputerStoreApplication.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public ProductDTO AddProduct(ProductDTO product)
        {
            foreach (var categoryName in product.CategoryList)
            {
                var existingCategory = _categoryRepository.GetCategoryByName(categoryName);
                if (existingCategory == null)
                {
                    var newCategory = new Category { Name = categoryName, Description = "No Description" };
                    _categoryRepository.AddCategory(newCategory);
                }
            }

            product.Id = 0; 

            Product newProduct = _mapper.Map<Product>(product);

            _productRepository.AddProduct(newProduct);

            return _mapper.Map<ProductDTO>(newProduct);
        }


        public bool DeleteProduct(int id)
        {
            var product = _productRepository.GetProductById(id);

            if (product != null)
            {
                return _productRepository.DeleteProduct(product);
            }

            return false;
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public List<ProductDTO> GetProducts()
        {
            var products = _productRepository.GetProducts();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public ProductDTO UpdateProduct(ProductDTO product)
        {
            Product newProduct = _mapper.Map<Product>(product);
            Product oldProduct = _productRepository.GetProductById(product.Id);

            if (oldProduct != null)
            {
                _productRepository.UpdateProduct(oldProduct, newProduct);
            }

            return _mapper.Map<ProductDTO>(newProduct);
        }
    }
}
