using StoreInventory.Model;
using System.Collections.Generic;

namespace StoreInventory.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        void AddingCategory(string categoryName);
        void DeletingCategory(int categaryId);
        void EditingCategory(int categoryId, string editedName);
        List<Category> GetCategories();
        Category GetCategory(int categoryId);
    }
}