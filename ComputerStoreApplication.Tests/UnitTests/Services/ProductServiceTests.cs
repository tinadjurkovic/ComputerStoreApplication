using AutoMapper;
using ComputerStoreApplication.Data.Entities;
using ComputerStoreApplication.Data.Interfaces;
using ComputerStoreApplication.Service.DTOs;
using ComputerStoreApplication.Service.Services;
using Moq;

namespace ComputerStoreApplication.Tests.UnitTests.Services
{
    public class ProductServiceTests
    {
        IProductRepository productRepo;
        IMapper mapper;
        Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
        Product product;
        ProductDTO productDTO;
        Mock<IMapper> mapperMock = new Mock<IMapper>();
        List<ProductDTO> productDTOList = new List<ProductDTO>();
        List<Product> productList = new List<Product>();

        private Product GetProduct()
        {
            return new Product()
            {
                Id = 1,
                Name = "Iphone",
                Description = "15 Pro Max",
                Price = 1999,
                Quantity = 1,
                CategoryList = new List<string> { "Electronics", "Smartphone" }

            };
        }

        private ProductDTO GetProductDTO()
        {
            return new ProductDTO()
            {
                Id = 1,
                Name = "Iphone",
                Description = "15 Pro Max",
                Price = 1999,
                Quantity = 1,
                CategoryList = new List<string> { "Electronics", "Smartphone" }

            };
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
            new Product()
            {
                Id = 1,
                Name = "Iphone",
                Description = "15 Pro Max",
                Price = 1999,
                Quantity = 1,
                CategoryList = new List<string> { "Electronics", "Smartphone" }

            },
            new Product()
            {
                Id = 2,
                Name = "Samsung",
                Description = "Galaxy Ultra",
                Price = 1999,
                Quantity = 1,
                CategoryList = new List<string> { "Smartphone", "Electronics" }

            }
            };
        }


        private void SetupMocks()
        {
            productRepo = productRepositoryMock.Object;
            mapper = mapperMock.Object;
        }

        private void SetupProductDTOListMock()
        {
            productDTO = GetProductDTO();
            var productDTO2 = GetProductDTO();
            productDTO2.Id = 2;
            productDTO2.Name = "Test";

            productDTOList.Add(productDTO);
            productDTOList.Add(productDTO2);

            productList = GetProducts();

            mapperMock.Setup(o => o.Map<List<ProductDTO>>(productList)).Returns(productDTOList);

        }

        private void SetupProductDTOMock()
        {
            product = GetProduct();
            mapperMock.Setup(o => o.Map<ProductDTO>(product)).Returns(GetProductDTO());
        }

        [Fact]
        public void GetProducts_ReturnsListOfProducts()
        {
            productList = GetProducts();
            SetupMocks();
            SetupProductDTOListMock();

            productRepositoryMock.Setup(o => o.GetProducts()).Returns(productList);

            var productService = new ProductService(productRepo, null, mapper);

            var result = productService.GetProducts();


            Assert.True(result != null);

        }

        [Fact]
        public void GetProductById_WithValidId_ReturnsExpectedProduct()
        {
            product = GetProduct();
            SetupMocks();
            SetupProductDTOMock();

            productRepositoryMock.Setup(o => o.GetProductById(It.IsAny<int>())).Returns(product);

            var productService = new ProductService(productRepo, null, mapper);

            int id = 1;

            var result = productService.GetProductById(id);

            Assert.True(result != null);
            Assert.Equal(id, result.Id);

        }

        [Fact]
        public void AddProduct_WithValidProduct_ReturnsExpectedAddedProduct()
        {
            productDTO = GetProductDTO();
            product = GetProduct();

            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(repo => repo.GetCategoryByName(It.IsAny<string>())).Returns((Category)null);
            categoryRepositoryMock.Setup(repo => repo.AddCategory(It.IsAny<Category>()));

            productRepositoryMock.Setup(repo => repo.GetProductById(productDTO.Id)).Returns((Product)null);
            productRepositoryMock.Setup(repo => repo.AddProduct(It.IsAny<Product>()));

            mapperMock.Setup(mapper => mapper.Map<Product>(productDTO)).Returns(product);
            mapperMock.Setup(mapper => mapper.Map<ProductDTO>(product)).Returns(productDTO);

            var productService = new ProductService(productRepositoryMock.Object, categoryRepositoryMock.Object, mapperMock.Object);

            var result = productService.AddProduct(productDTO);

            Assert.Equal(productDTO.Id, result.Id);
            Assert.Equal(productDTO.Name, result.Name);
            Assert.Equal(productDTO.Description, result.Description);

            productRepositoryMock.Verify(repo => repo.AddProduct(product), Times.Once);
        }



        [Fact]
        public void UpdateProduct_WithExistingProduct_ReturnsExpectedUpdatedProduct()
        {
            productDTO = GetProductDTO();
            var oldProduct = GetProduct();
            var newProduct = GetProduct();
            newProduct.Description = "Updated Description";

            productRepositoryMock.Setup(repo => repo.GetProductById(productDTO.Id)).Returns(oldProduct);
            productRepositoryMock.Setup(repo => repo.UpdateProduct(oldProduct, newProduct));

            mapperMock.Setup(mapper => mapper.Map<Product>(productDTO)).Returns(newProduct);
            mapperMock.Setup(mapper => mapper.Map<ProductDTO>(newProduct)).Returns(productDTO);

            var productService = new ProductService(productRepositoryMock.Object, null, mapperMock.Object);

            var result = productService.UpdateProduct(productDTO);

            Assert.Equal(productDTO.Id, result.Id);
            Assert.Equal(productDTO.Name, result.Name);
            Assert.Equal(productDTO.Description, result.Description);

            productRepositoryMock.Verify(repo => repo.UpdateProduct(oldProduct, newProduct), Times.Once);
        }

        [Fact]
        public void DeleteProduct_WithValidId_ReturnsExpectedTrue()
        {
            var productId = 1;
            product = GetProduct();

            productRepositoryMock.Setup(repo => repo.GetProductById(productId)).Returns(product);
            productRepositoryMock.Setup(repo => repo.DeleteProduct(product)).Returns(true);

            var productService = new ProductService(productRepositoryMock.Object, null, mapperMock.Object);

            var result = productService.DeleteProduct(productId);

            Assert.True(result);

            productRepositoryMock.Verify(repo => repo.DeleteProduct(product), Times.Once);
        }

    }
}