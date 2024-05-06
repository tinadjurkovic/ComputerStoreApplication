using AutoMapper;
using System.Collections.Generic;
using ComputerStoreApplication.Data.Entities;
using ComputerStoreApplication.Data.Interfaces;
using ComputerStoreApplication.Service.DTOs;
using ComputerStoreApplication.Service.Interfaces;

namespace ComputerStoreApplication.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public CategoryDTO AddCategory(CategoryDTO category)
        {
            Category newCategory = _mapper.Map<Category>(category);
            _categoryRepository.AddCategory(newCategory);
            return _mapper.Map<CategoryDTO>(newCategory);
        }

        public bool DeleteCategory(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category != null)
            {
                return _categoryRepository.DeleteCategory(category);
            }
            return false;
        }

        public CategoryDTO GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public List<CategoryDTO> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        public CategoryDTO UpdateCategory(int id, CategoryDTO category)
        {
            var oldCategory = _categoryRepository.GetCategoryById(id);
            if (oldCategory != null)
            {
                oldCategory.Name = category.Name;
                oldCategory.Description = category.Description;
                _categoryRepository.UpdateCategory(oldCategory);
                return _mapper.Map<CategoryDTO>(oldCategory);
            }
            return null;
        }


    }
}
