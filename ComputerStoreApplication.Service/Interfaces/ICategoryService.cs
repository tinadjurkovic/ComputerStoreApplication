using System.Collections.Generic;
using ComputerStoreApplication.Service.DTOs;

namespace ComputerStoreApplication.Service.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDTO> GetCategories();
        CategoryDTO GetCategoryById(int id);
        CategoryDTO AddCategory(CategoryDTO category);
        CategoryDTO UpdateCategory(int id, CategoryDTO category);
        bool DeleteCategory(int id);
    }
}
