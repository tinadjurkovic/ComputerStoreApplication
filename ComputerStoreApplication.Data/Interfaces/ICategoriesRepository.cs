using System.Collections.Generic;
using ComputerStoreApplication.Data.Entities;

namespace ComputerStoreApplication.Data.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();

        Category GetCategoryById(int id);

        Category GetCategoryByName(string categoryName);

        void AddCategory(Category category);

        void UpdateCategory(Category newCategory);

        bool DeleteCategory(Category category);
    }
}
