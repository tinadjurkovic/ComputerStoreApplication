using AutoMapper;
using ComputerStoreApplication.Data.Entities;
using ComputerStoreApplication.Data.Interfaces;
using ComputerStoreApplication.Service.DTOs;
using ComputerStoreApplication.Service.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ComputerStoreApplication.Tests.UnitTests.Services
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();

        private Category GetCategory()
        {
            return new Category
            {
                Id = 1,
                Name = "Laptops",
                Description = "Category for laptops"
            };
        }

        private CategoryDTO GetCategoryDTO()
        {
            return new CategoryDTO
            {
                Id = 1,
                Name = "Laptops",
                Description = "Category for laptops"
            };
        }

        private List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Laptops",
                    Description = "Category for laptops"
                },
                new Category
                {
                    Id = 2,
                    Name = "Smartphones",
                    Description = "Category for smartphones"
                }
            };
        }

        [Fact]
        public void GetCategories_ReturnsListOfCategories()
        {
            var categories = GetCategories();
            var categoryDTOs = categories.ConvertAll(c => mapperMock.Object.Map<CategoryDTO>(c));

            categoryRepositoryMock.Setup(repo => repo.GetCategories()).Returns(categories);
            mapperMock.Setup(mapper => mapper.Map<List<CategoryDTO>>(categories)).Returns(categoryDTOs);

            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);

            var result = categoryService.GetCategories();

            Assert.NotNull(result);
            Assert.Equal(categories.Count, result.Count);
            foreach (var category in result)
            {
                Assert.Contains(category, categoryDTOs);
            }
        }

        [Fact]
        public void GetCategoryById_WithValidId_ReturnsExpectedCategory()
        {
            var category = GetCategory();
            var categoryDTO = GetCategoryDTO();

            categoryRepositoryMock.Setup(repo => repo.GetCategoryById(category.Id)).Returns(category);
            mapperMock.Setup(mapper => mapper.Map<CategoryDTO>(category)).Returns(categoryDTO);

            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);

            var result = categoryService.GetCategoryById(category.Id);

            Assert.NotNull(result);
            Assert.Equal(categoryDTO.Id, result.Id);
            Assert.Equal(categoryDTO.Name, result.Name);
            Assert.Equal(categoryDTO.Description, result.Description);
        }

        [Fact]
        public void AddCategory_WithValidCategory_ReturnsExpectedAddedCategory()
        {
            var categoryDTO = GetCategoryDTO();
            var category = GetCategory();

            categoryRepositoryMock.Setup(repo => repo.GetCategoryByName(categoryDTO.Name)).Returns((Category)null);
            categoryRepositoryMock.Setup(repo => repo.AddCategory(It.IsNotNull<Category>()));

            mapperMock.Setup(mapper => mapper.Map<Category>(categoryDTO)).Returns(category);
            mapperMock.Setup(mapper => mapper.Map<CategoryDTO>(category)).Returns(categoryDTO);

            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);

            var result = categoryService.AddCategory(categoryDTO);

            Assert.NotNull(result);
            Assert.Equal(categoryDTO.Id, result.Id);
            Assert.Equal(categoryDTO.Name, result.Name);
            Assert.Equal(categoryDTO.Description, result.Description);

            categoryRepositoryMock.Verify(repo => repo.AddCategory(It.IsNotNull<Category>()), Times.Once);
        }

        [Fact]
        public void UpdateCategory_WithExistingCategory_ReturnsExpectedUpdatedCategory()
        {
            var categoryDTO = GetCategoryDTO();
            var oldCategory = GetCategory();
            var newCategory = GetCategory();
            newCategory.Description = "Updated description";

            categoryRepositoryMock.Setup(repo => repo.GetCategoryById(categoryDTO.Id)).Returns(oldCategory);
            categoryRepositoryMock.Setup(repo => repo.UpdateCategory(It.IsNotNull<Category>()));

            mapperMock.Setup(mapper => mapper.Map<Category>(categoryDTO)).Returns(newCategory);
            mapperMock.Setup(mapper => mapper.Map<CategoryDTO>(newCategory)).Returns(categoryDTO);

            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);

            var result = categoryService.UpdateCategory(categoryDTO.Id, categoryDTO);

            Assert.NotNull(result);
            Assert.Equal(categoryDTO.Id, result.Id);
            Assert.Equal(categoryDTO.Name, result.Name);
            Assert.Equal(categoryDTO.Description, result.Description);

            categoryRepositoryMock.Verify(repo => repo.UpdateCategory(It.IsNotNull<Category>()), Times.Once);
        }

        [Fact]
        public void DeleteCategory_WithValidId_ReturnsExpectedTrue()
        {
            var categoryId = 1;
            var category = GetCategory();

            categoryRepositoryMock.Setup(repo => repo.GetCategoryById(categoryId)).Returns(category);
            categoryRepositoryMock.Setup(repo => repo.DeleteCategory(category)).Returns(true);

            var categoryService = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object);

            var result = categoryService.DeleteCategory(categoryId);

           
            Assert.True(result);

            categoryRepositoryMock.Verify(repo => repo.DeleteCategory(category), Times.Once);
        }
    }
}
