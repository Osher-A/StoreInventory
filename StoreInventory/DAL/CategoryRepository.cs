using StoreInventory.Interfaces;
using StoreInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreInventory.DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<ICategory> GetCategories()
        {
            List<ICategory> categories;
            using (var db = new StoreContext())
            {
                categories = db.Categories.OrderBy(c => c.Name).ToList<ICategory>();
            }
            return categories;
        }

        public ICategory GetCategory(int categoryId)
        {
            using (var db = new StoreContext())
            {
                return db.Categories.Find(categoryId);
            }
        }
        public void AddingCategory(string categoryName)
        {
            var newCategory = new Category();
            newCategory.Name = categoryName;
            using (var db = new StoreContext())
            {
                db.Categories.Add(newCategory);
                db.SaveChanges();
            }
        }
        public void UpdateCategory(int categoryId, string editedName)
        {
            using (var db = new StoreContext())
            {
                db.Categories.Find(categoryId).Name = editedName;
                db.SaveChanges();
            }
        }
        public void DeletingCategory(int categaryId)
        {
            using (var db = new StoreContext())
            {
                db.Categories.Remove(db.Categories.Find(categaryId));
                db.SaveChanges();
            }
        }

    }
}
