using MyLibrary.Extentions;
using StoreInventory.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoreInventory.Services
{
    public class CategoryService
    {
        private ICategoryRepository _categoryRepos;

        public CategoryService(ICategoryRepository categoryRepos)
        {
            _categoryRepos = categoryRepos;
        }

        public ObservableCollection<DTO.Category> GetCategories()
        {
            return Categories().ToObservableCollection();
        }
        private IEnumerable<DTO.Category> Categories()
        {
            var modelcategories = _categoryRepos.GetCategories();
            foreach (var category in modelcategories)
            {
                DTO.Category dtoCategory = (DTO.Category)(Model.Category)category;
                yield return dtoCategory;
            }
        }
    }
}