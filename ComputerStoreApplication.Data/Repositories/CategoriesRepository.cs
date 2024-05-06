using System;
using System.Collections.Generic;
using System.Linq;
using ComputerStoreApplication.Data.Entities;
using ComputerStoreApplication.Data.Interfaces;

namespace ComputerStoreApplication.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ComputerStoreDataContext _dataContext;

        public CategoryRepository(ComputerStoreDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddCategory(Category category)
        {
            _dataContext.Categories.Add(category);
            _dataContext.SaveChanges();
        }

        public bool DeleteCategory(Category category)
        {
            try
            {
                _dataContext.Categories.Remove(category);
                _dataContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Category GetCategoryById(int id)
        {
            return _dataContext.Categories.FirstOrDefault(x => x.Id == id);
        }
        public Category GetCategoryByName(string name)
        {
            return _dataContext.Categories.FirstOrDefault(x => x.Name == name);
        }
        public IEnumerable<Category> GetCategories()
        {
            return _dataContext.Categories.ToList();
        }

        public void UpdateCategory(Category category)
        {
            _dataContext.Categories.Update(category);
            _dataContext.SaveChanges();
        }
    }
}
