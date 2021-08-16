using StoreInventory.Model;
using System.Collections.Generic;

namespace StoreInventory.Interfaces
{
    public interface ICategoryRepository
    {
        void AddingCategory(string categoryName);
        void DeletingCategory(int categaryId);
        void EditingCategory(int categoryId, string editedName);
        List<ICategory> GetCategories();
        ICategory GetCategory(int categoryId);
    }
}