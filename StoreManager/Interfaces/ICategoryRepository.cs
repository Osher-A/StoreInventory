using StoreManager.Model;
using System.Collections.Generic;

namespace StoreManager.Interfaces
{
    public interface ICategoryRepository
    {
        void AddingCategory(string categoryName);
        void DeletingCategory(int categaryId);
        void UpdateCategory(int categoryId, string editedName);
        List<ICategory> GetCategories();
        ICategory GetCategory(int categoryId);
    }
}